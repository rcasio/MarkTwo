using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkTwo
{
    public class GenerateBinaryFile
    {
        // 싱글톤
        private static GenerateBinaryFile instance;
        public static GenerateBinaryFile Instance
        {
            get
            {
                if (instance == null) { instance = new GenerateBinaryFile(); }
                return instance;
            }
        }

        DataManager dataManager;
        
        public void Create(DataManager dataManager) // 바이너리 파일을 생성한다.
        { 
            this.dataManager = dataManager;

            // 개별적으로 바이너리 파일을 만든다.
            Dictionary<string, SheetData> clientSheetDatas = dataManager.excelData.clientSheetDatas;

            Console.WriteLine("");
            Console.WriteLine("============ 바이너리 변환을 시작합니다.");

            foreach (var key in clientSheetDatas.Keys)
            {
                SheetData sheetData = clientSheetDatas[key];
                Console.WriteLine("");
                Console.WriteLine("========= 바이너리 변환 : " + sheetData.name + " 테이블");

                // TODO : 쓰레드를 사용해서 바이너리 파일을 만든다.
                // TODO : 바이너리 파일은 테이블별로 개별적으로 만들며
                // TODO : 쓰레드는 다국어 2,  클라이언트2, 서버2 로 구성되도록 한다.
                // TODO : 다국어는 언어별로 바이너리 파일로 쪼개도록 한다.
            }

            /*
            // 클라이언트 DB에 바이너리 파일을 쓴다.
            try
            {
                if (dataType.Equals("Bit")) binaryWriter_ForClientDB.Write(Convert.ToBoolean(data_ExchangedString));
                else if (dataType.Equals("TinyInt")) binaryWriter_ForClientDB.Write(Convert.ToByte(data_ExchangedString));
                else if (dataType.Equals("SmallInt")) binaryWriter_ForClientDB.Write(Convert.ToInt16(data_ExchangedString));
                else if (dataType.Equals("Int")) binaryWriter_ForClientDB.Write(Convert.ToInt32(data_ExchangedString));
                else if (dataType.Equals("BigInt")) binaryWriter_ForClientDB.Write(Convert.ToInt64(data_ExchangedString));
                else if (dataType.Equals("Float")) binaryWriter_ForClientDB.Write(Convert.ToSingle(data_ExchangedString));
                else if (dataType.Equals("Double")) binaryWriter_ForClientDB.Write(Convert.ToDouble(data_ExchangedString));
                else if (dataType.StartsWith("Char") || dataType.StartsWith("VarChar"))
                {
                    if (string.IsNullOrEmpty(data_ExchangedString)) data_ExchangedString = "";
                    binaryWriter_ForClientDB.Write(Convert.ToString(data_ExchangedString));
                }
                else
                {
                    MessageBox.Show("클라이언트DB시트에 정의되지 않는 자료형이 입력되었습니다. [테이블 : " + tableData.name + "] [행 : " + row + "] [열 : " + column + "]");
                    Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("클라이언트DB시트에 데이터 입력이 잘못되었습니다. [테이블 : " + tableData.name + "] [필드 이름 : " + tableData.fieldNameList[column - 1] + "]" + "] [행 : " + row);
                Close();
            }
            EditFormLabel_ClientThread(Client_FileSize, "누적 바이트 : " + binaryWriter_ForClientDB.BaseStream.Length.ToString("###,###") + " Byte");
            */
        }
    }
}
