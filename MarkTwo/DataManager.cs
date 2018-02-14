using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
// 참조 : http://msdn.microsoft.com/en-us/library/office/microsoft.office.interop.excel.aspx
using Excel = Microsoft.Office.Interop.Excel;

using static MarkTwo.TagManager;

namespace MarkTwo
{
    /// <summary>
    /// 정상적인 데이터인지 체크합니다.
    /// </summary>
    public class DataManager
    {
        public ConverterWindow converterWindow { get; set; }
        public TableDataS tableData { get; set; }
        
        static string tableName;
        static int columnNumber;
        static int rowNumber;
        static object data;
        
        /// <summary>
        /// 첫번째 행 데이터 중복 처리를 위한 리스트
        /// </summary>
        List<string> rowFirstColumns = new List<string>();

        //======== 데이터 관리 부분 
        public Excel.Application excelApp; // 엑셀 어플리 케이션
        public Excel.Workbook workBook; // 워크 북
        public Excel.Sheets sheets; // 시트들
        
        public Excel.Worksheet ruleSheet; // [테이블_규칙] 시트
        public Excel.Worksheet dataTableSheet; // 테이블 관리 시트
        public Excel.Worksheet dataTypeSheet; // 테이블 관리 시트

        public static ExcelData clientExcelData; // 클라이언트 엑셀 데이터
        public static ExcelData serverExcelData; // 서버 엑셀 데이터

        //TODO : 스레드 풀은 멀티랭귀어 테이블이 중점적으로 할당되는지(최우선 순위)를 테스트 한 다음에 진행하도록 한다.
        List<Thread> threadSheets = new List<Thread>(6); // 최대 6개의 스레드를 지원하도록 한다.

        public DataRule dataRule; // [테이블_규칙] 시트
        public DataType dataType; // 데이터 타입
        public DataTableList dataTableList; // 데이터 리스트
        public ExcelData excelData; // 엑셀 데이타
        public DataExtraction dataExtraction; // 데이터 추출

        /// <summary>
        /// 엑셀 데이터를 생성한다.
        /// </summary>
        /// <param name="sheetType"> 엑셀 타입 </param>
        public void CreateExcelData(Action<DataRule> SetFormDataRule)
        {
            Console.WriteLine("===== 엑셀 데이터 생성");
            this.excelApp       = new Excel.Application();
            this.workBook       = excelApp.Workbooks.Open(this.ExcelFilePath(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            this.sheets         = this.workBook.Sheets;

            this.ruleSheet      = sheets["테이블_규칙"] as Excel.Worksheet; // [테이블_규칙] 시트를 할당한다.
            this.dataTableSheet = sheets["테이블관리"] as Excel.Worksheet; // [테이블관리] 시트를 할당한다.
            this.dataTypeSheet  = sheets["Tag"] as Excel.Worksheet; // [테이블관리] 시트를 할당한다.

            this.dataRule       = new DataRule(this.ruleSheet, this); // [테이블_규칙] 시트를 기준으로 데이터 룰 객체를 만든다.
            this.dataType       = new DataType(this.ruleSheet, dataTypeSheet, this , this.dataRule); // [테이블_규칙]과 [Tag] 시트를 기반으로 데이터 타입을 만든다.
            this.dataTableList  = new DataTableList(this.dataTableSheet, this.sheets); // 테이블 리스트를 만든다.
            this.excelData      = new ExcelData(this); // 엑셀 데이터를 추출한다.
            this.dataExtraction = new DataExtraction(this); // 데이터 추출

            // TODO : ExcelData 객체에 SheetData 객체를 기반으로 엑셀에서 데이터를 추출한다.
            
            SetFormDataRule(dataRule); // 데이터 룰 UI를 세팅한다.
            // TODO : 지원하는 타입( 사용자 enum을 포함 )을 폼에 표시한다.
        }

        // 엑셀 파일 패스
        public string ExcelFilePath()
        {
            return Application.StartupPath + "\\ExcelionDB.xlsm";
        }

        /// <summary>
        /// 주석 데이터의 오류를 체크한다.
        /// </summary>
        public void CheckData(string tableName, int rowNumber, int columnNumber, object data)
        {
            DataManager.tableName = tableName;
            DataManager.columnNumber = columnNumber;
            DataManager.rowNumber = rowNumber;

            DataManager.data = data;

            if (DataManager.rowNumber == ConverterWindow.FIELD_COMMENTLINE) this.CheckFirstLineData(); // 첫번째 행일 경우
            if (DataManager.rowNumber == ConverterWindow.FIELDNAME_LINE) this.CheckSecondLineData(); // 두번째 행일 경우
            if (DataManager.rowNumber == ConverterWindow.DATATYPE_LINE) this.CheckThirdLineData(); // 세번째 행일 경우
        }

        /// <summary>
        /// 첫번째 데이터를 체크한다.
        /// 1. 셀에 데이터가 반드시 존재해야 한다.
        /// </summary>
        public void CheckFirstLineData()
        {
            // 데이터가 없을 경우
            if (data == null)
            {
                MessageBox.Show("[" + tableName + "] 테이블 첫번째 행의 [" + columnNumber + "] 번째 필드 데이터가 입력되어 있지 않습니다.", "엑셀 필드 오류");
                converterWindow.Close();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 두번째 데이터를 체크한다. (DB 이름필드)
        /// 1. 자료형이 입력되어 있는지 검사한다. (추출된 클래스에서 오류가 난다.)
        /// </summary>
        public void CheckSecondLineData()
        {
            foreach (string type in converterWindow.clientDataTypes)
            {
                // 데이터가 없을 경우
                if (data == null)
                {
                    MessageBox.Show("[" + tableName + "] 테이블 두번째 행\n[" + columnNumber + "] 번째 필드의 필드이름이 입력되어 있지 않습니다.", "엑셀 필드 오류");
                    converterWindow.Close();
                    Environment.Exit(0);
                }

                // 타입이 다를 경우
                if (data.ToString().Equals(type))
                {
                    MessageBox.Show("[" + tableName + "] 테이블 두번째 행의 [" + columnNumber + "] 번째 필드 이름이 [" + data + "]로 클라이언트 자료형과 충돌합니다.\n\n※ [테이블 규칙] 시트의 C# 자료형을 참조하여 필드 이름을 클라이언트 자료형과 충돌하지 않도록 변경하시기 바랍니다.", "엑셀 필드타입 입력 오류");
                    converterWindow.Close();
                    Environment.Exit(0);
                }
            }
	    }

        /// <summary>
        /// 세번째 데이터를 체크한다. (DB 자료형)
        /// </summary>
        public void CheckThirdLineData()
        {
            foreach (string type in converterWindow.clientDataTypes)
            {
                // 데이터가 없을 경우
                if (data == null)
                {
                    MessageBox.Show("[" + tableName + "] 테이블 세번째 행\n[" + columnNumber + "] 번째 필드의 필드 자료형이 입력되어 있지 않습니다.", "엑셀 필드 오류");
                    converterWindow.Close();
                    Environment.Exit(0);
                }

                // 타입이 다를 경우
                if (data.Equals(type))
                {
                    MessageBox.Show("[" + tableName + "] 테이블 세번째 행의 [" + columnNumber + "] 번째 자료형이 [" + data + "]로 잘못 입력되어 있습니다.\n\n※ [테이블 규칙] 시트의 MSSSQL 자료형을 참조하시기 바랍니다.", "엑셀 데이터타입 입력 오류");
                    converterWindow.Close();
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// 행 주석을 감별해야할 경우
        /// </summary>
        public void CheckData_RowComment_IfNull(string tableName, int rowNumber, object data)
        {
            // 데이터가 없을 경우
            if (data == null)
            {
                MessageBox.Show("[" + DataManager.tableName + "] 테이블 첫번째 필드의 [" + DataManager.rowNumber + "] 번째 줄의 데이터가 입력되어 있지 않습니다.", "엑셀 행 오류");
                converterWindow.Close();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 창을 닫는 메세지 박스를 호출한다.
        /// </summary>
        /// <param name="msg"></param>
        public void ShowCloseMSB(string msg)
        {
            MessageBox.Show(msg);
            Environment.Exit(0);
        }
    }
}
