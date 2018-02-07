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
        private GameData gameData;
        private DataRule dataRule;
        private DataTableList dataTableList;

        public int totalDataCount; // 데이터 카운트

        public Dictionary<string, SheetData> clientSheetDatas = new Dictionary<string, SheetData>(); // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> serverSheetDats = new Dictionary<string, SheetData>(); // 서버 시트 데이터
        
        public ExcelData(GameData gameData)
        {
            this.gameData = gameData;
            this.dataRule = gameData.dataRule;
            this.dataTableList = gameData.dataTableList;

            Console.WriteLine("");
            Console.WriteLine("========= 클라이언트 데이터 추출 시작");
            for (int i = 0; i < this.dataTableList.clientList.Count; i++)
            {
                clientSheetDatas.Add(this.dataTableList.clientList[i], new SheetData(this.gameData.sheets, this.dataTableList.clientList[i], this.dataRule));
            }
        }
    }
}
