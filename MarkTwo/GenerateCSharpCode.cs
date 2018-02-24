using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using static MarkTwo.TagManager;

namespace MarkTwo
{
    class GenerateCSharpCode
    {
        public const string TABLECONVERTER_FILENAME = "TableConverter.cs";
        public const string TABLECLASSLIST_FILENAME = "TableClassList.cs";
        public const string TABLETAGLIST_FILENAME = "TableTagList.cs";

        public List<string> multilingual = new List<string>();

        private DataManager dataManager;

        // PR 데이터를 담기 위한 기본 클래스, PR 테이블의 필드와 동일해야 한다.
        public class PR
        {
            public int Num;
            public string Type;
            public string Key;
            public string Value;
            public string Comment;
        }

        public Dictionary<int,PR> dicPR = new Dictionary<int, PR>(); // PR데이터를 담기위한 딕셔너리

        // Tag 테이블을 담기위한 클래스
        public class Tag
        {
            public string TagTitle;
            public List<string> TagCompoments;
        }
    
        public Dictionary<string, Tag> dicTag = new Dictionary<string, Tag>(); // Tag 데이터를 담기위한 딕셔너리

        public int CurrentPRNum; // 현재 PR 넘버
        
        // 문자열을 편집할 때 사용됩니다.
        static StringBuilder edit_string = new StringBuilder();

        // 코드를 작성할 파일스트림
        StreamWriter tableConverter;

        // 테이블 시트를 클래스로 정의한 파일>
        StreamWriter tableClassList;

        // 테이블용 태그를 정리한 파일
        StreamWriter tableTagList;

        public List<string> totlaClientList; // 모든 클라이언트 테이블 리스트
        public Dictionary<string, SheetData> totalClientSheetDatas; // 클라이언트 최종 시트 데이터
        public Dictionary<string, SheetData> totalServerSheetDatas; // 클라이언트 최종 시트 데이터
        
        // PR 테이블 데이터를 만드기 위한 함수를 만든다.
        public void SetDicPR()
        {
            //IEnumerator<KeyValuePair<string, FieldData>> e = this.dataManager.excelData.totalClientSheetDatas[SheetName.PR].fieldDatas.GetEnumerator();
            //while (e.MoveNext())
            //{
            //    FieldData fieldData = e.Current.Value;

            //    // Num이라면
            //    if (fieldData.name.Equals("Num"))
            //    {
            //        // pr을 생성하고 딕셔너리에 넣는다.
            //        PR pr = new PR(); // pr을 생성한다.
            //        CurrentPRNum = int.Parse(e.Current.Value.contents); // 현재 Num을 저장한다.

            //        pr.Num = CurrentPRNum; // pr값을 대입한다.
            //        dicPR.Add(CurrentPRNum, pr); // pr을 딕셔너리에 넣는다.
            //    }
            //    else
            //    {
            //        // 딕셔너리에서 해
            //        PR pr = dicPR[CurrentPRNum]; // 대입할 PR클래스를 가져온다.

            //        // pr을 구성한다.
            //        if (fieldName.Equals("Type")) pr.Type = dataExchangedString;
            //        else if (fieldName.Equals("Key")) pr.Key = dataExchangedString;
            //        else if (fieldName.Equals("Value")) pr.Value = dataExchangedString;
            //        else if (fieldName.Equals("Comment")) pr.Comment = dataExchangedString;

            //        dicPR[CurrentPRNum] = pr; // 수정된 PR 클래스를 대입한다.
            //    }
            //}
        }

        // Tag 딕셔너리를 구성을 위한 함수
        public void SetDicTag(string fieldName, string dataExchangedString)
        {
            // Num일 경우는 태그를 만들지 않는다.(인덱싱이기 때문에 만들 이유가 없음)
            if (!fieldName.Equals("Num"))
            {
                // Tag 테이블의 필드가 dicTag의 키가 된다.
                if (!dicTag.ContainsKey(fieldName)) // 만약 키(field)가 없다면
                {
                    Tag tag = new Tag(); // 태그 클래스를 생성한다.
                    tag.TagTitle = fieldName; // 태그 이름을 넣는다.
                    tag.TagCompoments = new List<string>(); // 리스트를 생성한다.
                    tag.TagCompoments.Add(dataExchangedString); // 최초 데이터를 넣는다.

                    dicTag.Add(fieldName, tag); // 해당 태그 객체를 추가한다.
                }
                else // 태그가 있다면
                {
                    Tag tag = dicTag[fieldName]; // 해당 Tag 객체를 불러온다.

                    // 데이터가 null이 아닐 경우 ( 테이블에서 스트링으로 구성되기 때문에 null일 수 있다)
                    if (!string.IsNullOrEmpty(dataExchangedString))
                    {
                        tag.TagCompoments.Add(dataExchangedString); // 태그를 추가한다.
                    }
                }
            }
        }
        
        // 최초 생성 시 기본적인 코드를 짜도록 한다.
        public GenerateCSharpCode(DataManager dataManager)
        {
            this.dataManager = dataManager;
            this.totlaClientList = this.dataManager.dataTableList.totlaClientList;
            this.totalClientSheetDatas = this.dataManager.excelData.totalClientSheetDatas;
            this.totalServerSheetDatas = this.dataManager.excelData.totalServerSheetDatas;

            this.tableConverter = new StreamWriter(new FileStream(this.Create_FilePath_TableConverter(), FileMode.Create));
            this.tableClassList = new StreamWriter(new FileStream(this.Create_FilePath_TableClassList(), FileMode.Create));
            this.tableTagList = new StreamWriter(new FileStream(this.Create_FilePath_TableTagList(), FileMode.Create));

            this.WriteTableConverter();     // TableConverter.cs를 작성한다.
            this.WriteTableClassList();     // TableClassList.cs를 작성한다.
            this.WriteTableTagList();       // TableTagList.cs를 작성한다.
        }

        // TableClassList.cs를 작성한다.
        private void WriteTableClassList()
        {
            IEnumerator<KeyValuePair<string, SheetData>> e = this.totalClientSheetDatas.GetEnumerator();
            while (e.MoveNext())
            {
                // 테이블 단위의 개별 클래스를 만든다.
                tableClassList.WriteLine("[System.Serializable] "); // 에디터에서 확인할 수 있게 [System.Serializable]을 하도록 한다.
                tableClassList.WriteLine("public class " + e.Current.Key);
                tableClassList.WriteLine("{");
                
                for (int i = 0; i < e.Current.Value.fieldNameList.Count; i++)
                {
                    tableClassList.WriteLine(AddString("\tpublic ", this.ChangeType(e.Current.Value.fieldDataTypeList[i]), " ", e.Current.Value.fieldNameList[i], ";"));
                }

                tableClassList.WriteLine("}");
                tableClassList.WriteLine("");
            }
        }

        // TableConverter.cs를 작성한다.
        private void WriteTableConverter()
        {
            tableConverter.WriteLine("using UnityEngine;");
            tableConverter.WriteLine("using System.Collections;");
            tableConverter.WriteLine("using System.Collections.Generic;");
            tableConverter.WriteLine("using System.IO;");
            tableConverter.WriteLine("using System;");

            tableConverter.WriteLine("");
            tableConverter.WriteLine("public class Table");
            tableConverter.WriteLine("{");

            // 테이블 시트를 담을 클래스 리스트를 선언한다.
            foreach (string sheetName in this.totlaClientList)
            {
                tableConverter.WriteLine(AddString("\tpublic static Dictionary<int, ", sheetName, "> ", sheetName, ";"));
            }

            tableConverter.WriteLine("}");
            tableConverter.WriteLine("");
            tableConverter.WriteLine("public class TableLoad");
            tableConverter.WriteLine("{");

            // GetBinaryReader() 함수를 작성한다.
            this.WriteGetBinaryReader();

            tableConverter.WriteLine("");
            // 테이블을 최초로 로딩하는 함수
            tableConverter.WriteLine("\tpublic TableLoad()");
            tableConverter.WriteLine("\t{");

            IEnumerator<KeyValuePair<string, SheetData>> e = this.totalClientSheetDatas.GetEnumerator();
            while (e.MoveNext())
            {
                //m_TableConverter.WriteLine(AddString("\tTable.", e.Current.Key, " = new Dictionary<int, ", e.Current.Key, ">(", e.Current.Value.RowCount.ToString(), ")"));
                tableConverter.WriteLine(AddString("\t\t// ", e.Current.Key));
                tableConverter.WriteLine(AddString("\t\tTable.", e.Current.Key, " = new Dictionary<int, ", e.Current.Key, ">();"));
                tableConverter.WriteLine(AddString("\t\tBinaryReader ", e.Current.Key.ToLower(), "BinaryReader = this.GetBinaryReader(\"", GenerateBinaryFile.clientBinaryFiles[e.Current.Key], "\");"));
                tableConverter.WriteLine("");
                tableConverter.WriteLine(AddString("\t\tfor (int i = 0; i < ", e.Current.Value.RowCount.ToString(), "; i++)"));
                tableConverter.WriteLine("\t\t{");
                tableConverter.WriteLine(AddString("\t\t\t", e.Current.Key, " ", e.Current.Key.ToLower(), " = new ", e.Current.Key, "();"));
                tableConverter.WriteLine("");

                // 필드에 따른 데이터를 설정한다.
                for (int i = 0; i < e.Current.Value.fieldDataTypeList.Count; i++)
                {
                    string read = null;
                    string fieldDataType = e.Current.Value.fieldDataTypeList[i];
                    string fieldName = e.Current.Value.fieldNameList[i];

                    if (fieldDataType.Equals("Bit")) read = this.AddString(e.Current.Key, "BinaryReader.", "ReadBoolean();");
                    else if (fieldDataType.Equals("TinyInt")) read = this.AddString(e.Current.Key, "BinaryReader.", "ReadByte();");
                    else if (fieldDataType.Equals("SmallInt")) read = this.AddString(e.Current.Key, "BinaryReader.", "ReadInt16();");
                    else if (fieldDataType.Equals("Int")) read = this.AddString(e.Current.Key, "BinaryReader.", "ReadInt32();");
                    else if (fieldDataType.Equals("Float")) read = this.AddString(e.Current.Key, "BinaryReader.", "ReadSingle();");
                    else if (fieldDataType.Equals("Double")) read = this.AddString(e.Current.Key, "BinaryReader.", "ReadDouble();");
                    else if (fieldDataType.Equals("Bigint")) read = this.AddString(e.Current.Key, "BinaryReader.", "ReadInt64();");
                    else if (fieldDataType.StartsWith("VarChar")) read = this.AddString(e.Current.Key, "BinaryReader.", "ReadString();");
                    else // enum 일 경우
                    {
                        read = this.AddString("(TagManager.Table.PR.", fieldName, ") Enum.Parse(typeof(TagManager.Table.PR.", fieldName, "), ", e.Current.Key, "BinaryReader.", "ReadString());");
                    }

                    tableConverter.WriteLine(AddString("\t\t\t", e.Current.Key.ToLower(), ".", fieldName, " = ", read));
                }

                tableConverter.WriteLine("\t\t}");
                tableConverter.WriteLine(AddString("\t\t", e.Current.Key.ToLower(), "BinaryReader.Close();"));
                tableConverter.WriteLine("");
            }
        }

        /// <summary>
        /// GetBinaryReader 함수를 작성한다.
        /// </summary>
        private void WriteGetBinaryReader()
        {
            tableConverter.WriteLine("\tprivate BinaryReader GetBinaryReader(string fileName)");
            tableConverter.WriteLine("\t{");
            tableConverter.WriteLine("\t\tTextAsset textasset = Resources.Load(fileName) as TextAsset;");
            tableConverter.WriteLine("\t\tMemoryStream stream = new MemoryStream(textasset.bytes);");
            tableConverter.WriteLine("\t\tBinaryReader binaryReader = new BinaryReader(stream);");
            tableConverter.WriteLine("\t\tstream.Close();");
            tableConverter.WriteLine("");
            tableConverter.WriteLine("\t\treturn binaryReader;");
            tableConverter.WriteLine("\t}");
        }

        // 테이블 시트를 참조하여 Multilingual 및 PR 클래스를 만든다.
        public void WriteTableTagList()
        {
            tableTagList.WriteLine("// 테이블 태그를 사용하기 위한 클래스입니다."); // 사용확인을 위한 주석
            tableTagList.WriteLine("// 툴에의해 자동으로 생성됩니다.");
            tableTagList.WriteLine("// 2017년 09월 26일 고성규");
            tableTagList.WriteLine("");
            tableTagList.WriteLine("namespace TagManager"); // 태그를 관리하기 위한 네임스페이스를 만들도록 한다.
            tableTagList.WriteLine("{");
            tableTagList.WriteLine("\tpublic class Table"); // 테이블 클래스를 만든다.
            tableTagList.WriteLine("\t{");

            //****** 다국어 관련 태그
            tableTagList.WriteLine("\t\t// 다국어 대응 열거자 입니다. Multilingual 테이블을 기반으로 작성됩니다.");
            tableTagList.WriteLine("\t\tpublic enum Multilingual"); // 다국어 열거자를 만든다.
            tableTagList.WriteLine("\t\t{");
            
            List<string> mtFieldNameList= this.dataManager.excelData.totalClientSheetDatas[SheetName.Multilingual].fieldNameList;

            for (int i = 0; i < mtFieldNameList.Count; i++)
            {
                if (!mtFieldNameList[i].Equals("Num"))
                {
                    if (i.Equals(mtFieldNameList.Count - 1))
                    {
                        tableTagList.WriteLine("\t\t\t" + mtFieldNameList[i]); // ","를 빼도록 한다.
                    }
                    else
                    {
                        tableTagList.WriteLine("\t\t\t" + mtFieldNameList[i] + ",");
                    }
                }
            }

            tableTagList.WriteLine("\t\t};");
            tableTagList.WriteLine("");

            tableTagList.WriteLine("\t\t// PR 테이블 대응 클래스 입니다.");
            tableTagList.WriteLine("\t\t// PR 테이블은 개별 변수를 정의하기 위한 테이블입니다.");
            tableTagList.WriteLine("\t\tpublic class PR");
            tableTagList.WriteLine("\t\t{");

            bool isStruct = false;

            SheetData prSheet = this.dataManager.excelData.totalClientSheetDatas[SheetName.PR];
            for (int i = 0; i < prSheet.RowCount; i++)
            {
                string Num = prSheet.fieldDatas["Num"].contents[i];
                string type = prSheet.fieldDatas["Type"].contents[i];
                string key = prSheet.fieldDatas["Key"].contents[i];
                string value = prSheet.fieldDatas["Value"].contents[i];
                string comment = prSheet.fieldDatas["Comment"].contents[i];

                if (type.Equals("StructStart")) // 구조체 시작이라면
                {
                    isStruct = true;

                    tableTagList.WriteLine("");
                    tableTagList.WriteLine(this.AddString("\t\t\tpublic " , this.ChangeType(type), " ",  key, " // ", comment)); // 구조체 정의를 시작한다.
                    tableTagList.WriteLine("\t\t\t{");
                }
                else if (type.Equals("StructEnd")) // 구조체 종료
                {
                    isStruct = false; // 구조체 밖에 존재함을 알린다.
                    tableTagList.WriteLine("\t\t\t" + "}"); // 
                    tableTagList.WriteLine("");
                }
                else
                {
                    if (isStruct) // 구조체 안에 들어가 있다면 Tab으로 한번 더 띄운다.
                    {
                        tableTagList.WriteLine("\t\t\t\tpublic const " + this.ChangeType(type) + " " + key + " = " + this.ChangeValue(type, value) + " // " + comment + " ( Num : " + Num + " )");

                    }
                    else // 구조체에 들어가 있지 않다면
                    {
                        tableTagList.WriteLine("\t\t\tpublic const " + this.ChangeType(type) + " " + key + " = " +
                        ChangeValue(type, value) + " // " + comment + " ( Num : " + Num + " )");
                    }
                }
            }

            //foreach (var item in dicPR)
            //{
            //    string type = ChangeType(item.Value.Type); // 타입을 CSharp에 맞게 변화시킨다.

            //    if (type.Equals("struct")) // 구조체 시작이라면
            //    {
            //        isStruct = true; // 구조체 안쪽에 위치함을 인식시킨다.
            //        tableTagList.WriteLine("");
            //        tableTagList.WriteLine("            public " + type + " " + item.Value.Key + " // " + item.Value.Comment); // 구조체 정의를 시작한다.
            //        tableTagList.WriteLine("            {");
            //    }
            //    else if (type.Equals("}")) // 구조체 종료
            //    {
            //        isStruct = false; // 구조체 밖에 존재함을 알린다.
            //        tableTagList.WriteLine("            " + type); // 구조체 정의를 시작한다.
            //        tableTagList.WriteLine("");
            //    }
            //    else
            //    {
            //        if (isStruct) // 구조체 안에 들어가 있다면 Tab으로 한번 더 띄운다.
            //        {
            //            tableTagList.WriteLine("                public const " + type + " " + item.Value.Key + " = " +
            //            ChangeValue(type, item.Value.Value) + " // " + item.Value.Comment + " ( Num : " + item.Value.Num + " )");
            //        }
            //        else // 구조체에 들어가 있지 않다면
            //        {
            //            tableTagList.WriteLine("            public const " + type + " " + item.Value.Key + " = " +
            //            ChangeValue(type, item.Value.Value) + " // " + item.Value.Comment + " ( Num : " + item.Value.Num + " )");
            //        }
            //    }
            //}

            tableTagList.WriteLine("        }");
            tableTagList.WriteLine("");

            // 테이블 Tag로 구성되도록 한다.
            tableTagList.WriteLine("        public class Tag");
            tableTagList.WriteLine("        {");

            int icount = 0;
            foreach (KeyValuePair<string, FieldData> fieldData in this.dataManager.excelData.totalClientSheetDatas[SheetName.Tag].fieldDatas) // 태그 enum를 생성한다.
            {
                if (!fieldData.Key.Equals("Num"))
                {
                    icount++;
                    tableTagList.WriteLine("            public enum " + fieldData.Value.name);
                    tableTagList.WriteLine("            {");
                    
                    // 개별 Tag enum을 생성한다.
                    for (int i = 0; i < fieldData.Value.contents.Count; i++)
                    {
                        //string tagCom = item.Value.TagCompoments[i];
                        string tagType = fieldData.Value.contents[i];

                        if (!string.IsNullOrEmpty(tagType))
                        {
                            // Type에서 Char(50)과 VarChar(50)을 Char50, VarChar50으로 변경한다.
                            if (fieldData.Key.Equals("Type"))
                            {
                                if (tagType.Contains("Char")) // Char, VarChar이 포함되었을 경우
                                {
                                    // ( )를 삭제한다(코드에서 인식시키기 위해)
                                    tagType = tagType.Replace("(", "");
                                    tagType = tagType.Replace(")", "");
                                }
                            }

                            tableTagList.WriteLine("                " + tagType + ",");

                            //// 마지막에는 ","를 넣지 않는다.
                            //if (i.Equals(fieldData.Value.contents.Count - 1))
                            //{
                            //    tableTagList.WriteLine("                " + tagType);
                            //}
                            //else
                            //{
                            //    tableTagList.WriteLine("                " + tagType + ",");
                            //}
                        }
                    }

                    tableTagList.WriteLine("            }");

                    // 마지막 Tag Enum혹은 클래스는 줄을 띄우지 않는다.
                    if (!icount.Equals(fieldData.Value.contents.Count))
                    {
                        tableTagList.WriteLine("");
                    }
                }
            }

            /****** const string 으로 만드는 태그
            foreach (KeyValuePair<string, Tag> item in dicTag)
            {
                m_TableTagList.WriteLine("            public class " + item.Key);
                m_TableTagList.WriteLine("            {");
                
                foreach (string tagCom in item.Value.TagCompoments)
                {
                    m_TableTagList.WriteLine("                public const string " + tagCom + " = \"" + tagCom +"\";");
                }

                m_TableTagList.WriteLine("            }");
                m_TableTagList.WriteLine("");
            }
            */

            tableTagList.WriteLine("        }");
            tableTagList.WriteLine("    }");
            tableTagList.WriteLine("}"); // TagManager NameSpace를 종료한다.
        }

        // 테이블 시트 정보를 참조하여 테이블 변환용 코드를 만든다.
        public void Write(TableDataS tableData)
        {
            // 각각의 리스트를 할당하도록 합니다.
            tableConverter.WriteLine(AddString("      Table.Data_", tableData.name, " = new Dictionary<int, ", tableData.name, ">(", tableData.totalRowCountDeleteComment.ToString(),");"));
            tableConverter.WriteLine("");

            // Table 클래스를 이어서 만든다.
            // 클래스의 행을 담도록 한다.
            tableConverter.WriteLine(AddString("      for ( int i = 0; i < ", tableData.totalRowCountDeleteComment.ToString(), " ; i++ )"));
            tableConverter.WriteLine("      {");

            // 리스트에 담을 클래스를 생성한다.(리스트는 참조 형식이라 각각의 클래스가 존재해야 한다.)
            tableConverter.WriteLine(AddString("          row_", tableData.name, " = new ", tableData.name, "();"));
            tableConverter.WriteLine("");

            // 파일을 읽도록 한다.
            for (int i = 0; i < tableData.fieldNameList.Count; i++)
            {
                string readBinaryFunction = null;
                string fieldName = tableData.fieldNameList[i];
                string fieldType = tableData.fieldDataTypeCSharp[i];

                if (fieldType.Equals("bool")) readBinaryFunction = "m_TableDataBinary.ReadBoolean();";
                else if (fieldType.Equals("byte")) readBinaryFunction = "m_TableDataBinary.ReadByte();";
                else if (fieldType.Equals("short")) readBinaryFunction = "m_TableDataBinary.ReadInt16();";
                else if (fieldType.Equals("int")) readBinaryFunction = "m_TableDataBinary.ReadInt32();";
                else if (fieldType.Equals("float")) readBinaryFunction = "m_TableDataBinary.ReadSingle();";
                else if (fieldType.Equals("double")) readBinaryFunction = "m_TableDataBinary.ReadDouble();";
                else if (fieldType.Equals("long")) readBinaryFunction = "m_TableDataBinary.ReadInt64();";
                else if (fieldType.Equals("string")) readBinaryFunction = "m_TableDataBinary.ReadString();";

                tableConverter.WriteLine(AddString("          row_", tableData.name, ".", fieldName, " = ", readBinaryFunction));

                // ******* 다국어 Tag를 위한 Multilingual 필드 추출
                if (tableData.name.Equals("Multilingual")  // 만약 Multilingual 테이블 이라면 현재 지원하지 않은 언어일 경우 주석처리한다.
                    && !fieldName.Equals("Num")) // 그리고 필드가 Num이 아닐 경우
                {
                    multilingual.Add(fieldName); // 다국어 리스트를 추가한다.
                }

                ////****** 다국어 부분 코드 작성
                //if (tableData.m_Name.Equals("Multilingual")  // 만약 Multilingual 테이블 이라면 현재 지원하지 않은 언어일 경우 주석처리한다.
                //    && !fieldName.Equals("Num")) // 그리고 필드가 Num이 아닐 경우
                //{
                //    multilingual.Add(fieldName); // 다국어 리스트를 추가한다.

                //    // 모든 언어를 불러올지 아니면 특정 언어만 불러 올지를 결정한다.
                //    m_TableConverter.WriteLine(AddString("          if (setLanugage.Equals(null))")); // 언어 설정이 null이라면
                //    m_TableConverter.WriteLine(AddString("          {"));
                //    m_TableConverter.WriteLine(AddString("          row_", tableData.m_Name, ".", fieldName, " = ", readBinaryFunction)); // 모든 언어를 불러온다.
                //    m_TableConverter.WriteLine(AddString("          }"));
                //    m_TableConverter.WriteLine(AddString("          else"));
                //    m_TableConverter.WriteLine(AddString("          {"));
                //    m_TableConverter.WriteLine(AddString("              if (setLanugage.Equals( \"", fieldName, "\")) row_", tableData.m_Name, ".", fieldName, " = ", readBinaryFunction));
                //    m_TableConverter.WriteLine(AddString("              else m_TableDataBinary.ReadString();"));
                //    m_TableConverter.WriteLine(AddString("          {"));
                //}
                //else // 일반 테이블 코드 작성
                //{

                //}
            }

            // 리스트에 클래스를 참조값을 담는다.
            tableConverter.WriteLine("");
            tableConverter.WriteLine(AddString("          Table.Data_", tableData.name, ".Add(row_",tableData.name,".Num, row_", tableData.name, ");"));
            tableConverter.WriteLine("");

            tableConverter.WriteLine("      }");
            tableConverter.WriteLine("");
        }

        // 테이블 시트를 참조하여 시트 클래스를 만든다.
        public void WriteCode_TableClassList(TableDataS tableData)
        {   
            // 테이블 단위의 개별 클래스를 만든다.
            tableClassList.WriteLine("  [System.Serializable] "); // 에디터에서 확인할 수 있게 [System.Serializable]을 하도록 한다.
            tableClassList.WriteLine("  public class " + tableData.name);
            tableClassList.WriteLine("  {");

            int i = 0;

            foreach (string fieldName in tableData.fieldNameList)
            {
                string fieldType = tableData.fieldDataTypeCSharp[i];

                tableClassList.WriteLine(AddString("    public ", fieldType, " ", fieldName, ";"));

                i++;
            }

            tableClassList.WriteLine("  }");
        }

        // 테이블 시트를 참조하여 Multilingual 및 PR 클래스를 만든다.
        public void WriteCode_TableTagMultilingualPR(TableDataS tableData)
        {
            tableTagList.WriteLine("// 테이블 태그를 사용하기 위한 클래스입니다."); // 사용확인을 위한 주석
            tableTagList.WriteLine("// 툴에의해 자동으로 생성됩니다.");
            tableTagList.WriteLine("// 2017년 09월 26일 고성규");
            tableTagList.WriteLine("");
            tableTagList.WriteLine("namespace TagManager"); // 태그를 관리하기 위한 네임스페이스를 만들도록 한다.
            tableTagList.WriteLine("{");
            tableTagList.WriteLine("    public class Table"); // 테이블 클래스를 만든다.
            tableTagList.WriteLine("    {");

            //****** 다국어 관련 태그
            tableTagList.WriteLine("        // 다국어 대응 열거자 입니다. Multilingual 테이블을 기반으로 작성됩니다.");
            tableTagList.WriteLine("        public enum Multilingual"); // 다국어 열거자를 만든다.
            tableTagList.WriteLine("        {");

            // 다국어에 사용하는 언어를 기입한다.
            for (int i = 0; i < multilingual.Count; i++)
            {
                // 마지막 라인일 경우
                if (i.Equals(multilingual.Count - 1))
                {
                    tableTagList.WriteLine("            " + multilingual[i]); // ","를 빼도록 한다.
                }
                else // 마지막 라인일 경우
                {
                    tableTagList.WriteLine("            " + multilingual[i] + ",");
                }
            }

            tableTagList.WriteLine("        };");
            tableTagList.WriteLine("");

            //****** PR 관련 태그
            // PR 클래스를 만든다.
            // PR 테이블은 변수 대응하기 위한 테이블이다.
            if (tableData.name.Equals("PR"))
            {
                tableTagList.WriteLine("        // PR 테이블 대응 클래스 입니다.");
                tableTagList.WriteLine("        // PR 테이블은 개별 변수를 정의하기 위한 테이블입니다.");
                tableTagList.WriteLine("        public class PR");
                tableTagList.WriteLine("        {");

                bool isStruct = false;

                foreach (var item in dicPR)
                {
                    string type = ChangeType(item.Value.Type); // 타입을 CSharp에 맞게 변화시킨다.

                    if (type.Equals("struct")) // 구조체 시작이라면
                    {
                        isStruct = true; // 구조체 안쪽에 위치함을 인식시킨다.
                        tableTagList.WriteLine("");
                        tableTagList.WriteLine("            public " + type + " " + item.Value.Key + " // " + item.Value.Comment); // 구조체 정의를 시작한다.
                        tableTagList.WriteLine("            {");
                    }
                    else if (type.Equals("}")) // 구조체 종료
                    {
                        isStruct = false; // 구조체 밖에 존재함을 알린다.
                        tableTagList.WriteLine("            " + type); // 구조체 정의를 시작한다.
                        tableTagList.WriteLine("");
                    }
                    else
                    {
                        if (isStruct) // 구조체 안에 들어가 있다면 Tab으로 한번 더 띄운다.
                        {
                            tableTagList.WriteLine("                public const " + type + " " + item.Value.Key + " = " +
                            ChangeValue(type, item.Value.Value) + " // " + item.Value.Comment + " ( Num : " + item.Value.Num + " )");
                        }
                        else // 구조체에 들어가 있지 않다면
                        {
                            tableTagList.WriteLine("            public const " + type + " " + item.Value.Key + " = " +
                            ChangeValue(type, item.Value.Value) + " // " + item.Value.Comment + " ( Num : " + item.Value.Num + " )");
                        }
                    }
                }

                tableTagList.WriteLine("        }");
                tableTagList.WriteLine("");

                // 이 밑으로 WriteCode_TableTag 함수를 사용하여 Tag 테이블이 추가된다.
            }
        }

        public string ChangeValue(string type, string value)
        {
            string tValue = value;

            if (type.Equals("float"))
            {
                tValue = tValue.Replace(" ","") + "f;";
            }
            else if (type.Equals("string"))
            {
                tValue = "\"" + tValue + "\";";
            }
            else
            {
                tValue += ";";
            }

            return tValue;
        }

        public string ChangeType(string type)
        {
            string rtype = null;

            if (type.Equals("Bit")) rtype = "bool";
            else if (type.Equals("TinyInt")) rtype = "byte";
            else if (type.Equals("SmallInt")) rtype = "short";
            else if (type.Equals("Int")) rtype = "int";
            else if (type.Equals("BigInt")) rtype = "long";
            else if (type.Equals("Float")) rtype = "float";
            else if (type.Equals("Double")) rtype = "double";
            else if (type.Contains("Char")) rtype = "string";
            else if (type.Contains("VarChar")) rtype = "string";
            else if (type.Contains("StructStart")) rtype = "struct";
            else if (type.Contains("StructEnd")) rtype = "}";
            else // enum 타입
            {
                rtype = type;
                //MessageBox.Show("[ PR ] 테이블에서 [ Type ]필드의 자료형 입력이 잘못되어 있습니다. 입력된 자료형 : " + type);
            }

            return rtype;
        }

        // Tag 테이블을 참조하여 추가 Tag 클래스를 만든다.
        public void WriteCode_TableTag(TableDataS tableData)
        {
            // 테이블 Tag로 구성되도록 한다.
            tableTagList.WriteLine("        public class Tag");
            tableTagList.WriteLine("        {");

            int icount = 0;
            foreach (KeyValuePair<string, Tag> item in dicTag) // 태그 enum를 생성한다.
            {
                icount++;
                tableTagList.WriteLine("            public enum " + item.Key);
                tableTagList.WriteLine("            {");

                // 개별 Tag enum을 생성한다.
                for (int i = 0; i < item.Value.TagCompoments.Count; i++)
                {
                    string tagCom = item.Value.TagCompoments[i];

                    // Type에서 Char(50)과 VarChar(50)을 Char50, VarChar50으로 변경한다.
                    if (item.Key.Equals("Type"))
                    {
                        if (tagCom.Contains("Char")) // Char, VarChar이 포함되었을 경우
                        {
                            // ( )를 삭제한다(코드에서 인식시키기 위해)
                            tagCom = tagCom.Replace("(", "");
                            tagCom = tagCom.Replace(")", "");
                        }
                    }

                    // 마지막에는 ","를 넣지 않는다.
                    if (i.Equals(item.Value.TagCompoments.Count - 1))
                    {
                        tableTagList.WriteLine("                " + tagCom);
                    }
                    else
                    {
                        tableTagList.WriteLine("                " + tagCom + ",");
                    }
                }
                
                tableTagList.WriteLine("            }");

                // 마지막 Tag Enum혹은 클래스는 줄을 띄우지 않는다.
                if (!icount.Equals(dicTag.Count))
                {
                    tableTagList.WriteLine("");
                }
            }

            /****** const string 으로 만드는 태그
            foreach (KeyValuePair<string, Tag> item in dicTag)
            {
                m_TableTagList.WriteLine("            public class " + item.Key);
                m_TableTagList.WriteLine("            {");
                
                foreach (string tagCom in item.Value.TagCompoments)
                {
                    m_TableTagList.WriteLine("                public const string " + tagCom + " = \"" + tagCom +"\";");
                }

                m_TableTagList.WriteLine("            }");
                m_TableTagList.WriteLine("");
            }
            */

            tableTagList.WriteLine("        }");
            tableTagList.WriteLine("    }");
            tableTagList.WriteLine("}"); // TagManager NameSpace를 종료한다.
        }

        // 파일 경로를 반환한다.
        private string Create_FilePath_TableConverter() { return Application.StartupPath + "\\" + TABLECONVERTER_FILENAME; }

        // 파일 경로를 반환한다.
        private string Create_FilePath_TableClassList() { return Application.StartupPath + "\\" + TABLECLASSLIST_FILENAME; }

        // 파일 경로를 반환한다.
        private string Create_FilePath_TableTagList() { return Application.StartupPath + "\\" + TABLETAGLIST_FILENAME; }

        // 문자열을 편집할 때 사용됩니다.
        private string AddString(params string[] string_List)
        {
            // 유니티에서는 Clear() 함수를 지원하지 않는다.
            edit_string.Clear();

            int i = 0;
            int length = string_List.Length;

            while (i < length)
            {
                if (string.IsNullOrEmpty(string_List[i]) == false)
                {
                    edit_string.Append(string_List[i]);
                }
                i++;
            }
            return edit_string.ToString();
        }

        public void Close()
        {
            //m_TableConverter.WriteLine("        m_TableDataBinary.Close();");
            tableConverter.WriteLine("    }");
            tableConverter.WriteLine("}");

            tableConverter.Close();
            tableClassList.Close();
            tableTagList.Close();
        }
    }
}
