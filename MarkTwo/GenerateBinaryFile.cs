using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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

        Thread multilingualThread;    // 다국어 바이너리 파일
        Thread clientThread;    // 클라이언트 바이너리 파일

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

                // INFO : 쓰레드는 해당 객체에 동시 접근하면 안된다. 
                // INFO : 만약 동시 접근하는 객체가 있다면 당연히 쓰레드는 동작하지 않는다.
                // https://msdn.microsoft.com/ko-kr/library/3dasc8as(v=vs.80).aspx 참조로 쓰레드를 만들 것!

                // 다국어 쓰레드
                //multilingualThread = new Thread(new ThreadStart(() => clientSheetDatas.Add(SheetName.Multilingual, new SheetData(this.dataManager.sheets, SheetName.Multilingual, this.dataRule))));
                //multilingualThread.Start();

                //// 다국어를 제외한 클라이언트 쓰레드
                //clientThread = new Thread(new ThreadStart(() => this.GenerateOtherSheetDatas()));
                //clientThread.Start();

                // 쓰레드 쌤플
                //multilingualThread = new Thread(new ThreadStart(
                //    () => {
                //        for (int i = 0; i < 10000; i++)
                //        {
                //            Console.WriteLine("aaa : " + i);
                //        }
                //    }
                //));
                //multilingualThread.Start();

                //clientThread = new Thread(new ThreadStart(
                //    () => {
                //        for (int i = 0; i < 10000; i++)
                //        {
                //            Console.WriteLine("bbb : " + i);
                //        }
                //    }
                //));
                //clientThread.Start();
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
