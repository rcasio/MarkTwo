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

        public ExcelData(DataManager dataManager)
        {
            this.dataManager = dataManager;
            this.dataRule = dataManager.dataRule;
            this.dataTableList = dataManager.dataTableList;

            Console.WriteLine("");
            Console.WriteLine("========= 클라이언트 데이터 추출 시작");

            Console.WriteLine("");
            Console.WriteLine("====== 데이터 정보 추출 ");

            for (int i = 0; i < this.dataTableList.clientList.Count; i++)
            {
                string sheetName = this.dataTableList.clientList[i];

                clientSheetDatas.Add(this.dataTableList.clientList[i], new SheetData(this.dataManager.sheets, this.dataTableList.clientList[i], this.dataRule));
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
