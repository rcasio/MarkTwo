using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// 참조 : http://msdn.microsoft.com/en-us/library/office/microsoft.office.interop.excel.aspx
using Excel = Microsoft.Office.Interop.Excel;

using static MarkTwo.TagManager;

namespace MarkTwo
{
    public class ExcelData 
    {
        private DataManager dataManager;
        private DataRule dataRule;
        private DataTableList dataTableList;

        public int totalDataCount; // 데이터 카운트

        public Dictionary<string, SheetData> clientSheetDatas = new Dictionary<string, SheetData>(); // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> serverSheetDats = new Dictionary<string, SheetData>(); // 서버 시트 데이터

        Thread multilingualThread;    // 클라이언트의 데이터 변환 쓰레드
        Thread clientThread; // 클라이언트 쓰레드

        //private void StartConvertClient() { this.StartConversion(SheetType.Client); }

        public ExcelData(DataManager dataManager)
        {
            this.dataManager = dataManager;
            this.dataRule = dataManager.dataRule;
            this.dataTableList = dataManager.dataTableList;

            Console.WriteLine("");
            Console.WriteLine("========= 클라이언트 데이터 추출 시작");

            // TODO : 쓰레드를 사용하기 위해 Excel 관련 함수 및 객체는 사용하지 말아야 한다.(중복 사용)
            // TODO : 미리 해당 정보를 추출해 가지고 있어야 한다.

            // 다국어 쓰레드
            //multilingualThread = new Thread(new ThreadStart(() => clientSheetDatas.Add(SheetName.Multilingual, new SheetData(this.dataManager.sheets, SheetName.Multilingual, this.dataRule))));
            //multilingualThread.Start();

            //// 다국어를 제외한 클라이언트 쓰레드
            //clientThread = new Thread(new ThreadStart(() => this.GenerateOtherSheetDatas()));
            //clientThread.Start();

            // 다국어 제외 시트 데이터를 추출한다.
            //public void GenerateOtherSheetDatas()
            //{
            //    for (int i = 0; i < this.dataTableList.clientList.Count; i++)
            //    {
            //        string sheetName = this.dataTableList.clientList[i];

            //        if (!sheetName.Equals(SheetName.Multilingual))
            //        {
            //            clientSheetDatas.Add(this.dataTableList.clientList[i], new SheetData(this.dataManager.sheets, this.dataTableList.clientList[i], this.dataRule));
            //        }
            //    }

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
    }
}
