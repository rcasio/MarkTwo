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

        public Dictionary<string, SheetData> necessarySheetDatas = new Dictionary<string, SheetData>(); // 필수 시트 데이터
        
        public Dictionary<string, SheetData> client01SheetDatas = new Dictionary<string, SheetData>(); // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client02SheetDatas = new Dictionary<string, SheetData>(); // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> server01SheetDats = new Dictionary<string, SheetData>(); // 서버 시트 데이터
        public Dictionary<string, SheetData> server02SheetDats = new Dictionary<string, SheetData>(); // 서버 시트 데이터

        public ExcelData(DataManager dataManager)
        {
            this.dataManager = dataManager;
            this.dataRule = dataManager.dataRule;
            this.dataTableList = dataManager.dataTableList;

            // TODO : 콜백을 사용해서 프로그래스 진행상황 나타내기

            Console.WriteLine("");
            Console.WriteLine("========= 필수 데이터 기반 정보 추출 시작");
            
            for (int i = 0; i < this.dataTableList.necessaryList.Count; i++)
            {
                string sheetName = this.dataTableList.necessaryList[i];

                //client01SheetDatas.Add(this.dataTableList.necessaryList[i], new SheetData(this.dataManager.sheets, this.dataTableList.necessaryList[i], this.dataRule));
                client01SheetDatas.Add(sheetName, new SheetData(this.dataTableList.necessarySheetList[i], sheetName, this.dataRule));
            }

            Console.WriteLine("");
            Console.WriteLine("========= 클라이언트 데이터01 기반 정보 추출 시작");
            
            for (int i = 0; i < this.dataTableList.clientList01.Count; i++)
            {
                string sheetName = this.dataTableList.clientList01[i];

                //client01SheetDatas.Add(this.dataTableList.clientList01[i], new SheetData(this.dataManager.sheets, this.dataTableList.clientList01[i], this.dataRule));
                client01SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList01[i], sheetName, this.dataRule));
            }

            Console.WriteLine("");
            Console.WriteLine("========= 클라이언트 데이터02 기반 정보 추출 시작");
            
            for (int i = 0; i < this.dataTableList.clientList02.Count; i++)
            {
                string sheetName = this.dataTableList.clientList02[i];

                //client01SheetDatas.Add(this.dataTableList.clientList02[i], new SheetData(this.dataManager.sheets, this.dataTableList.clientList02[i], this.dataRule));
                client01SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList02[i], sheetName, this.dataRule));
            }

            Console.WriteLine("");
            Console.WriteLine("========= 서버 데이터01 기반 정보 추출 시작");
            
            for (int i = 0; i < this.dataTableList.serverList01.Count; i++)
            {
                string sheetName = this.dataTableList.serverList01[i];

                //client01SheetDatas.Add(this.dataTableList.serverList01[i], new SheetData(this.dataManager.sheets, this.dataTableList.serverList01[i], this.dataRule));
                client01SheetDatas.Add(sheetName, new SheetData(this.dataTableList.serverSheetList01[i], sheetName, this.dataRule));
            }

            Console.WriteLine("");
            Console.WriteLine("========= 서버 데이터02 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.serverList02.Count; i++)
            {
                string sheetName = this.dataTableList.serverList02[i];

                //client01SheetDatas.Add(this.dataTableList.serverList02[i], new SheetData(this.dataManager.sheets, this.dataTableList.serverList02[i], this.dataRule));
                client01SheetDatas.Add(sheetName, new SheetData(this.dataTableList.serverSheetList02[i], sheetName, this.dataRule));
            }
        }

        // 엑셀을 닫는다.
        public void CloseExcel()
        {
            // TODO : 엑셀이 닫히지 않는 부분 수정할 것
            this.dataManager.excelApp.DisplayAlerts = false;
            this.dataManager.workBook.Close(0);
            this.dataManager.excelApp.Quit();
        }
    }
}
