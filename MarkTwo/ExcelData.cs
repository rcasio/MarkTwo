using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            for (int i = 0; i < this.dataTableList.clientList.Count; i++)
            {
                // TODO : 쓰레드 사용할 것
                // TODO : 쓰레드는 다국어 2,  클라이언트2, 서버2 로 구성되도록 한다.
                // TODO : 다국어는 언어별로 바이너리 파일로 쪼개도록 한다.

                // TODO : 마지막 쓰레드가 종료되면
                // TODO : 취합해서 clientSheetDatas 만들 것

                clientSheetDatas.Add(this.dataTableList.clientList[i], new SheetData(this.dataManager.sheets, this.dataTableList.clientList[i], this.dataRule));
            }
        }
    }
}
