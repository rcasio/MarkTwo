using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace MarkTwo
{
    // 사용할 테이블 리스트
    public class DataTableList
    {
        public Excel.Worksheet tableManagerSheet; // 테이블 관리 시트

        public List<string> clientList = new List<string>(); // 사용되는 테이블 리스트
        public List<string> serverList = new List<string>(); // 사용되는 클라이언트 리스트

        public List<Excel.Worksheet> clientsSheets = new List<Excel.Worksheet>(); // 클라이언트 시트 리스트
        public List<Excel.Worksheet> serverSheets = new List<Excel.Worksheet>(); // 서버 시트 리스트

        public DataTableList(Excel.Worksheet tableManagerSheet, Excel.Sheets sheets)
        {
            Console.WriteLine("");
            Console.WriteLine("============ [테이블_관리] 시트에서 테이블 시트 리스트 추출을 시작합니다.");

            this.tableManagerSheet = tableManagerSheet as Excel.Worksheet;

            Console.WriteLine("");
            Console.WriteLine("========= 시트 리스트 추가 작업 시작");
            Console.WriteLine("");
            Console.WriteLine("====== 클라이언트 시트 추가");

            // 클라이언트 리스트
            foreach (string ClientSheets_Name in tableManagerSheet.get_Range("B8", "B50").Value)
            {
                if (ClientSheets_Name == null) break;

                clientList.Add(ClientSheets_Name); // 시트 리스트 추가 (문자열 리스트)
                clientsSheets.Add(sheets[ClientSheets_Name] as Excel.Worksheet); // 시트 리스트 추가 (시트 리스트)
                Console.WriteLine("=== 클라이언트 시트 이름 : " + ClientSheets_Name);
            }

            Console.WriteLine("");
            Console.WriteLine("====== 서버 시트 추가");
            // 서버 리스트
            foreach (string ServerSheets_Name in tableManagerSheet.get_Range("C8", "C50").Value)
            {
                if (ServerSheets_Name == null) break;

                serverList.Add(ServerSheets_Name);
                serverSheets.Add(sheets[ServerSheets_Name] as Excel.Worksheet); // 시트 리스트 추가 (시트 리스트)
                Console.WriteLine("=== 클라이언트 시트 이름 : " + ServerSheets_Name);
            }
        }
    }
}
