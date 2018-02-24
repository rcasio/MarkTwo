using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
// 참조 : http://msdn.microsoft.com/en-us/library/office/microsoft.office.interop.excel.aspx
using Excel = Microsoft.Office.Interop.Excel;

using static MarkTwo.TagManager;

namespace MarkTwo
{
    /// <summary>
    /// 템플릿 메소드 패턴이 아닌, 템플릿 클래스 패턴;;;;;;;;;;;;
    /// </summary>
    public class DataManager
    {
        public ConverterWindow converterWindow { get; set; }
        
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

        public DataManager(ConverterWindow converterWindow)
        {
            this.converterWindow = converterWindow;
        }

        /// <summary>
        /// 엑셀 데이터를 생성한다.
        /// </summary>
        /// <param name="sheetType"> 엑셀 타입 </param>
        public void CreateExcelData(Action<DataRule> SetFormDataRule, 
                                    Action<int> SetExtreactionProgressBar, 
                                    Action<RichTextBox, string> SetRichText,
                                    Action<ProgressBar, int> SetMultilingualProgressBar,
                                    Action NextAction)
        {
            this.excelApp       = new Excel.Application();
            this.workBook       = excelApp.Workbooks.Open(this.ExcelFilePath(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            this.sheets         = this.workBook.Sheets;

            this.ruleSheet      = sheets["테이블_규칙"] as Excel.Worksheet; // [테이블_규칙] 시트를 할당한다.
            this.dataTableSheet = sheets["테이블관리"] as Excel.Worksheet; // [테이블관리] 시트를 할당한다.
            this.dataTypeSheet  = sheets["Tag"] as Excel.Worksheet; // [테이블관리] 시트를 할당한다.

            this.dataRule       = new DataRule(this.ruleSheet, this , SetExtreactionProgressBar, SetRichText); // [테이블_규칙] 시트를 기준으로 데이터 룰 객체를 만든다.
            this.dataType       = new DataType(this.ruleSheet, dataTypeSheet, this , this.dataRule, SetExtreactionProgressBar, SetRichText); // [테이블_규칙]과 [Tag] 시트를 기반으로 데이터 타입을 만든다.
            this.dataTableList  = new DataTableList(this.dataTableSheet, this.sheets, SetExtreactionProgressBar, SetRichText, this.converterWindow.ExtreactionReadyText); // 테이블 리스트를 만든다.
            this.excelData      = new ExcelData(this, SetExtreactionProgressBar, SetRichText); // 엑셀 데이터를 추출한다.
            this.dataExtraction = new DataExtraction(this, SetRichText, SetMultilingualProgressBar, NextAction); // 데이터 추출
            
            SetFormDataRule(dataRule); // 데이터 룰 UI를 세팅한다.
            // TODO : 지원하는 타입( 사용자 enum을 포함 )을 폼에 표시한다.
        }

        // 엑셀 파일 패스
        public string ExcelFilePath()
        {
            return Application.StartupPath + "\\ExcelionDB.xlsm";
        }
        
        /// <summary>
        /// 창을 닫는 메세지 박스를 호출한다.
        /// </summary>
        /// <param name="msg"></param>
        public void ShowCloseMSB(string msg)
        {
            MessageBox.Show(msg);
            converterWindow.Close();
            Environment.Exit(0);
        }
    }
}
