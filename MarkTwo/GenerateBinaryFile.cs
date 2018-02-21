using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;

using static MarkTwo.TagManager;

namespace MarkTwo
{
    public class GenerateBinaryFile
    {
        string originalBinaryFilePath; // 클라이언트 바이너리 파일 경로를 나타낸다.
        //string BINARY_FILE_EXENAME_FOR_UNITY = "bytes"; // 유니티에서 사용하는 바이너리 파일 확장자명
        string targetPathDB_Binary;

        private FileStream fileStream;    // 클라이언트 바이너리 파일
        private BinaryWriter binaryWriter; // 클라이언트 DB를 위한 바이너리라이터

        private DataManager dataManager;
        private string name;
        private SheetData sheetData;
        private SheetType sheetType;
        private DataRule dataRule;

        static public Dictionary<string, string> clientBinaryFiles = new Dictionary<string, string>();
        static public Dictionary<string, string> serverBinaryFiles = new Dictionary<string, string>();

        public GenerateBinaryFile(string name, DataManager dataManager, SheetData sheetData, SheetType sheetType)
        {
            this.dataManager = dataManager;
            this.sheetData = sheetData;
            this.sheetType = sheetType;
            this.dataRule = this.dataManager.dataRule;

            this.name = name + "_" + this.sheetType; // 이름 설정

            if (sheetType == SheetType.Multilingual ||
                sheetType == SheetType.Client)
            {
                clientBinaryFiles.Add(name, this.name);
            }
            else
            {
                serverBinaryFiles.Add(name, this.name);
            }

            
            originalBinaryFilePath = Application.StartupPath + "\\" + this.name + "." + FileExtensionName.Binary;  // 생성할 클라이언트의 바이너리 파일 경로를 만든다.
            fileStream = new FileStream(originalBinaryFilePath, FileMode.Create);
            binaryWriter = new BinaryWriter(fileStream);

            // 타겟 패스를 설정한다 (바이너리 파일이 위치할 곳)
            if (this.sheetType == SheetType.Multilingual ||
                this.sheetType == SheetType.Client)
            {
                targetPathDB_Binary = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + this.dataRule.clientDBPath + "\\" + this.name + "." + FileExtensionName.Binary;
            }
            else if (this.sheetType == SheetType.Server)
            {
                targetPathDB_Binary = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + this.dataRule.serverDBPath + "\\" + this.name + "." + FileExtensionName.Binary;
            }
        }

        // 클라이언트 DB에 바이너리 파일을 쓴다.
        public void Write(string data, string dataType, string tableName, int row, int column)
        {
            try
            {
                if (dataType.Equals("Bit"))
                {
                    binaryWriter.Write(Convert.ToBoolean(data));
                }
                else if (dataType.Equals("TinyInt"))
                {
                    binaryWriter.Write(Convert.ToByte(data));
                }
                else if (dataType.Equals("SmallInt"))
                {
                    binaryWriter.Write(Convert.ToInt16(data));
                }
                else if (dataType.Equals("Int"))
                {
                    binaryWriter.Write(Convert.ToInt32(data));
                }
                else if (dataType.Equals("BigInt"))
                {
                    binaryWriter.Write(Convert.ToInt64(data));
                }
                else if (dataType.Equals("Float"))
                {
                    binaryWriter.Write(Convert.ToSingle(data));
                }
                else if (dataType.Equals("Double"))
                {
                    binaryWriter.Write(Convert.ToDouble(data));
                }
                else if (dataType.StartsWith("Char") || dataType.StartsWith("VarChar"))
                {
                    if (string.IsNullOrEmpty(data)) data = "";
                    binaryWriter.Write(Convert.ToString(data));
                }
                else if (this.dataManager.dataType.CheckMySQLType(dataType)) // enum 체크
                {
                    if (data.Equals(Enum.Parse(this.dataManager.dataType.mySQLTypes[dataType], data).ToString()))
                    {
                        binaryWriter.Write(Convert.ToString(data)); // enum일 경우 string으로 기록한다.
                    }
                    else
                    {
                        MessageBox.Show("[테이블_규칙] 및 [Tag] 테이블에 정의되지 않는 자료형이 입력되었습니다(0). \n[테이블 : " + tableName + "] [ 필드 : " + column + " ] [ 레코드 : " + row + " ] \n[ 레이블 : " + data + " ]");
                        this.sheetData.Close();
                    }
                }
                else
                {
                    MessageBox.Show("[테이블_규칙] 및 [Tag] 테이블에 정의되지 않는 자료형이 입력되었습니다(1). \n[테이블 : " + tableName + "] [ 필드 : " + column + " ] [ 레코드 : " + row + " ] \n[ 레이블 : " + data + " ]");
                    this.sheetData.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("[테이블_규칙] 및 [Tag] 테이블에 정의되지 않는 자료형이 입력되었습니다(2). \n[테이블 : " + tableName + "] [ 필드 : " + column + " ] [ 레코드 : " + row + " ] \n[ 레이블 : " + data + " ]");
                this.sheetData.Close();
            }
        }

        public void Close()
        {
            binaryWriter.Close(); // 바이너리 파일을 닫는다.

            // 파일을 이동시킨다.
            if (File.Exists(targetPathDB_Binary)) File.Delete(targetPathDB_Binary); // 파일이 존재한다면 삭제한다.
            File.Move(originalBinaryFilePath, targetPathDB_Binary);  // 파일을 이동시킨다.
        }
    }
}
