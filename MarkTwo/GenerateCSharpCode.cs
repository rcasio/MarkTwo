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
        
        private DataManager dataManager;
        
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
            this.Close();
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
            
            tableTagList.WriteLine("        }");
            tableTagList.WriteLine("    }");
            tableTagList.WriteLine("}"); // TagManager NameSpace를 종료한다.
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
            tableConverter.WriteLine("    }");
            tableConverter.WriteLine("}");

            tableConverter.Close();
            tableClassList.Close();
            tableTagList.Close();
        }
    }
}
