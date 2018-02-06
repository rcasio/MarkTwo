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
        public int totalDataCount; // 데이터 카운트

        public List<string> sheetNames; // 시트 이름
        public Dictionary<string, Excel.Worksheet> originalSeetDatas; // 원본 서버 데이터
        public Dictionary<string, SheetData> sheetDatas; // 클라이언트 시트 데이터
    }
}
