using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Media;
using System.IO;
// 참조 : http://msdn.microsoft.com/en-us/library/office/microsoft.office.interop.excel.aspx
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
// 참조 : https://code.google.com/p/booksleeve/ Redis 클라이언트
using BookSleeve;

using static MarkTwo.TagManager; // 태그 매니저

namespace MarkTwo
{
    /// <summary>
    /// 서버 바이너리 파일이 필요할 경우
    /// TODO 부분을 참조하여 수정하도록 한다.
    /// </summary>

    public partial class ConverterWindow : Form
    {
        Excel.Application excelApp;
        Excel.Workbook workBook;
        Excel.Worksheet workSheet;
        Excel.Sheets sheets;

        //// 시트를 구분하기 위해서 사용된다.
        //enum SheetType  { Client, Server }

        //TagManager.SheetType sheetType;

        // 클라이언트 바이너리 파일 만들기
        public const string CLIENT_BINARY_FILENAME = "TableDB"; // 클라이언트에서 사용하는 바이너리 파일 이름
        public const string CLIENT_BINARY_FILENAME_ForText = "TableDB_ForText"; // 바이너리 파일을 텍스트 파일로 추출하기 위한 파일이름
        const string CLIENT_BINARY_FILE_EXENAME_FOR_UNITY = "bytes"; // 유니티에서 사용하는 바이너리 파일 확장자명
        const string CLIENT_TEXT_FILE_EXTENSION = "txt"; // 텍스트 파일 확장자
        const int CLIENT_TYPE_COUNT = 8; // 클라이언트의 자료형 개수를 나타낸다. 만약 자료형이 추가 및 삭제된다면 이부분을 수정한다.

        // 서버 바이너리 파일 만들기
        public const string SERVER_BINARY_FILENAME = "Server_TableDB"; // 서버에서 사용하는 바이너리 파일 이름
        const string SERVER_BINARY_FILE_EXENAME_FORUNITY = "ksk"; // 서버에서 사용되는 바이너리 파일 확장자명
        const int SERVER_TYPE_COUNT = 8; // 서버의 자료형 개수를 나타낸다. 만약 자료형이 추가 및 삭제된다면 이부분을 수정한다.
       
        static public string originalPathClientDB_Binary; //  클라이언트 바이너리 파일 경로를 나타낸다.
        static public string targetPathClientDB_Text;   // 텍스트 파일을 추출하기 위한 경로 및 파일이름.
        static public string targetPathClientDB_Binary;    // 다운로드를 위한 클라이언트 DB를 위치를 나타낸다.

        static public string originalPathTableClassList_Unity; // TableClassList 클래스 생성위치 파일의 위치
        static public string originalPathTableConverter; // TableConverter 클래스 생성위치 파일의 위치
        static public string createdPathNameForTableTagList; // TableTagList 클래스 생성위치 파일의 위치

        static public string targetPathTableClassList_Unity; // 유니티에서 사용된는 TableClassList의 클래스 파일의 위치
        static public string targetPathTableConverter; // 유니티에서 사용된는 TableConverter의 클래스 파일의 위치
        static public string targetPathForTableTagList; // 유니티에서 사용된는 TableTagList의 클래스 파일의 위치

        static public string originalPathSeverDB_Binary; //  바이너리 파일 경로를 나타낸다.
        static public string targetPathServerDB_Binary;   // 서버의 바이너리 파일이 이동할 곳의 경로를 나타낸다.
        static public string targetPathSeverDB_PHP;   // 서버의 PHP파일이 이동할 곳의 경로를 나타낸다.

        FileStream clientFileStream;    // 클라이언트 바이너리 파일
        FileStream serverFileStream;    // 서버 바이너리 파일

        BinaryWriter binaryWriter_ForClientDB; // 클라이언트 DB를 위한 바이너리라이터
        BinaryWriter binaryWriter_ForServerDB; // 서버 DB를 위한 바이너리라이터
        BinaryReader binaryReader; // 파일을 읽는다.
        StreamWriter streamWriter;

        // 행과 열의 주석을 나타낸다.
        public const int FIELD_COMMENTLINE = 1;    // 클라 및 서버에서 사용할 필드인지 나타내는 행
        public const int FIELDNAME_LINE = 2;    // 필드 이름이 기록된 행
        public const int DATATYPE_LINE = 3;    // 데이터 타입이 기록된 행
        public const int ROW_COMMENTFIELD = 1; // 데이터 행이 주석행인지 판별하는 첫번째 행
        public const int m_Default_RowComment_LineCount = 3;

        string commentFieldMark; // 코맨트 필드 마크
        string commentFieldClientOnlyMark; // 코맨트 클라이언트 온리 마크
        string commentRowMark; // 코멘트 로우 마크

        // DB의 기본 구성
        int totalDataCountClient;
        int totalDataCountForServer;

        static public List<string> clientSheetNames = new List<string>(); // 클라이언트에서 사용하는 시트의 이름
        static public List<string> serverSheetNames = new List<string>(); // 서버에서 사용하는 시트 이름

        public List<string> clientDataTypes; // 클라이언트에서 사용하는 데이터 타입
        public List<string> serverDataType;  // 서버에서 사용하는 데이터 타입                                                

        // 테이블 데이터
        public TableDataS tableDataClient;
        public TableDataS tableDataServer;
        
        DataManager dataManager;  // 데이터를 관리한다.(오류 등)
        GenerateBinaryFile generateBinaryFile;

        Dictionary<string, TableDataS> tableDatasClient = new Dictionary<string, TableDataS>();    // 테이블 클라이언트시트를 클래스를 저장하는 딕셔너리
        Dictionary<string, TableDataS> tableDatasServer = new Dictionary<string, TableDataS>();    // 테이블 서버시트를 저장하는 딕셔너리

        SoundPlayer endFileConvert = new SoundPlayer(Properties.Resources._416631__alpersez__acoustic_guitar); // 변환이 종료되었을 때 나오는 사운드

        bool isEnd_ClientConvert = false; // 클라이언트 데이터 변환 작업이 끝났는가?
        bool isEnd_ServerConvert = false; // 서버 데이터 변환 작업이 끝났는가?

        Thread client_DataControl;    // 클라이언트의 데이터 변환 쓰레드
        Thread server_DataControl;    // 서버 데이터 변환 쓰레드

        // 레디스 접속
        bool IsConnectServer = true; // 서버에 접속할 것인가? (엑셀 테이블에서 IP주소를 입력하지 않으면 false가 된다.)
        string RedisConnect_Ip;
        int RedisConnect_Port;
        string RedisConnect_PassWord = "passWord";
        static RedisConnection conn;

        string m_Reids_Num; // 레디스에 Num을 할당하기 위해 사용되는 변수

        //폼을 쓰레드로 제어하기 위한 델리게이트
        delegate void callBackClient_SheetName(System.Windows.Forms.Label formLabel, string txt);
        delegate void callBackClient_Progress(System.Windows.Forms.ProgressBar formProgressbar, int para);
        delegate void callBackServer_SheetName(System.Windows.Forms.Label formLabel, string txt);
        delegate void callBackServer_Progress(System.Windows.Forms.ProgressBar formProgressbar, int para);

        //System.Timers.Timer progressbarTimer = new System.Timers.Timer(); // 프로그래스바 업데이트를 위한 타이머
        System.Timers.Timer timerConvertChecker = new System.Timers.Timer(200); //0.2초마다 한번씩 체크한다.

        bool isExtractionText = false; // 텍스트 파일을 추출할 것인가?(바이너리 파일로 변환된 파일을 텍스트로)
        bool isExtractionBinary = false;
        bool isExtractionJson = false;
        bool isExtractionCSV = false;
        bool isExtractionXML = false;

        public ConverterWindow()
        {
            this.dataManager = new DataManager(this);
            this.generateBinaryFile = new GenerateBinaryFile();

            this.dataManager.converterWindow = this;

            InitializeComponent(); // 컴포넌트를 초기화 한다.
            this.SetProgressbar(); // 프로그래스바를 설정한다.
            
            this.FormClosed += this.ClostMarkTwo; // 죵료 시 실행되는 콜백함수 설정
            AppDomain.CurrentDomain.ProcessExit += (s, e) => this.CloseExcel(); // 종료 시 호출되는 

            #region 변경 부분
;
            //this.InitializeForm(); // 폼을 초기화 한다.

            //// TODO : 주석을 제외한 엑셀 오리지날 데이터를 추출한다.
            //// TODO : 쓰레드에서 Excel을 사용할 수 없기 떄문
            //// TODO : 추출한 데이터를 중심으로 ExcelData를 추출하고
            //// TODO : 바이너리 파일을 제작하도록 한다.

            //this.dataManager.CreateExcelData((p) => SetFormDataRule(p)); // 엑셀 데이터를 생성한다.
            
            //generateBinaryFile.Create(this.dataManager); // 바이너리 파일을 생성한다.

            //if (sheetType == SheetType.Client)
            //{
            //    // 데이터를 바이너리로 변한한다. (테이블 행과열은 1부터 시작하기 때문에 list 자료를 순서대로 차출하기 위해서 1을 뺀다.)
            //this.Write_ToClientDB(tableData.fieldDataTypeTable[dataCount - 1], data_ExchangedString, tableData, row, column);

            //    // PR 테이블일 경우
            //    if (tableData.name.Equals("PR"))
            //    {
            //        // PR 태그 클래스를 만들기 위한 자료를 추출한다.
            //        Create_CSharpCode.Instance.SetDicPR(tableData.fieldNameList[dataCount - 1], data_ExchangedString);
            //    }

            //    // Tag 테이블일 경우
            //    if (tableData.name.Equals("Tag"))
            //    {
            //        Create_CSharpCode.Instance.SetDicTag(tableData.fieldNameList[dataCount - 1], data_ExchangedString);
            //    }
            //}


            //Create_CSharpCode.Instance.WriteCode_TableConverter(tableData);
            //Create_CSharpCode.Instance.WriteCode_TableClassList(tableData);


            #endregion
            
            // INFO : 기존코드 실행 부분
            /*
            this.GetExcelSheets(); // 엑셀 시트를 추출한다.
            this.GetClientSheetNames(); // 클라이언트 관련 시트이름을 추출한다.
            this.GetServerSheetNames(); // 서버 관련 시트 이름을 추출한다.
            this.GetRuleInfo(); // 규칙관련 정보를 추출한다.

            this.ConnectRedisServer(); // 레디스 서버 접속

            this.CreateFilePath(); // 파일경로를 만든다.
            this.CheckTagetFolder(); // 엑셀에서 설정한 타겟 폴더가 존재하는지 체크한다.
            
            this.GetClientCSharpType(); // C샵 타입을 추출한다.
            this.GetServerMySQLType(); // MYSQL 타입을 추가한다.

            this.DisplayVersion(); // 버전을 표시한다.
            
            this.SetTableDatas(); // 클라이어트와 서버 시트의 클래스를 만들어 딕셔너리에 저장한다.
            */
        }
        #region 리펙토링 부분

        private void InitializeForm() // 폼을 초기화 한다.
        {
            Excel_Directory.Text = "엑셀 경로 : " + dataManager.ExcelFilePath(); // 엑셀 경로 표시
        }

        private void SetFormDataRule(DataRule dataRule) // 폼 데이터 룰 관련 세팅을 한다.
        {
            //Console.WriteLine("");
            //Console.WriteLine("===== 폼 데이터 룰 세팅");

            //Default_RowComment_LineCount.Text = "기본 주석 행 : " + DataRule.Default_RowComment_LineCount.ToString() + " 행";
            //Field_Comment.Text = "필드 주석 : " + dataRule.commentFieldMark + "&"; // "&"만 하면 표시되지 않는다.
            //Field_Comment_ClientOnly.Text = "필드 주석(클라이언트 사용) : " + dataRule.commentFieldClientOnlyMark;
            //Row_Comment.Text = "행 주석 : " + dataRule.commentRowMark;

            //// 추출 데이터 포멧을 여부를 표시한다.
            //IsTextFile.Text = "Text 문서 : " + dataRule.isSupportTextFile;
            //IsBinary.Text = "바이너리파일 : " + dataRule.isSupportBinary;
            //IsJson.Text = "Json : " + dataRule.isSupportJson;
            //IsCSV.Text = "CSV : " + dataRule.isSupprotCSV;
            //IsXML.Text = "XML : " + dataRule.isSupportXML;
        }
        
        #endregion

        // DB 테이블 업데이트를 위해 레디스 서버에 접속한다.
        private void ConnectRedisServer() 
        {
            workSheet = sheets["테이블_규칙"] as Excel.Worksheet;

            RedisConnect_Ip = workSheet.Range["Q12"].Value; // 엑셀에서 IP를 받아온다.

            if (string.IsNullOrEmpty(RedisConnect_Ip))
            {
                IsConnectServer = false;
            }
            else
            {
                RedisConnect_Port = (int)workSheet.Range["Q13"].Value; // 엑셀에서 포트 번호를 받아온다.

                conn = new RedisConnection(RedisConnect_Ip, RedisConnect_Port, -1, RedisConnect_PassWord, allowAdmin: true);
                Task open = conn.Open();

                conn.Server.FlushDb(1); // 기존 데이터를 지운다.
            }
        }
        
        private void CreateFilePath() // 파일경로를 만든다.
        {
            originalPathClientDB_Binary = Application.StartupPath + "\\" + CLIENT_BINARY_FILENAME + "." + CLIENT_BINARY_FILE_EXENAME_FOR_UNITY;  // 생성할 클라이언트의 바이너리 파일 경로를 만든다.
            originalPathSeverDB_Binary = Application.StartupPath + "\\" + SERVER_BINARY_FILENAME + "." + SERVER_BINARY_FILE_EXENAME_FORUNITY;  // 생성할 서버 바이너리 파일의 경로를 만든다.
            
            targetPathServerDB_Binary = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q14"].Value + "\\" + SERVER_BINARY_FILENAME + "." + SERVER_BINARY_FILE_EXENAME_FORUNITY;
            targetPathClientDB_Binary = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q15"].Value + "\\" + CLIENT_BINARY_FILENAME + "." + CLIENT_TEXT_FILE_EXTENSION;

            targetPathClientDB_Text = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q15"].Value + "\\" + CLIENT_BINARY_FILENAME_ForText + "." + CLIENT_TEXT_FILE_EXTENSION; // 텍스트 파일 경로를 만든다.
            // TODO : Json, CSV, XML 등의 파일 경로를 만들도록 한다.
            
            targetPathSeverDB_PHP = workSheet.Range["Q5"].Value + "\\" + Create_PHPCode.TABLECONVERTER_FILENAME; // PHP를 위한 서버 DB경로

            // 파일 경로 생성
            this.CreatePathTableClassList(); // TableClassList.cs 파일 경로를 생성한다.
            this.CreatePathTableConverter(); // TableTagList.cs 파일 경로를 생성한다.
            this.CreatePathTableTagList(); // TableTagList.cs 파일 경로를 생성한다.

            // 라벨표시
            TargetPath.Text = "이동경로 : " + targetPathClientDB_Binary; // TODO : 상황별 이동경로를 만들도록 한다.
        }

        // 폼이 시작되고 실행된다.
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.InitializeForm(); // 폼을 초기화 한다.

            // TODO : 주석을 제외한 엑셀 오리지날 데이터를 추출한다.
            // TODO : 쓰레드에서 Excel을 사용할 수 없기 떄문
            // TODO : 추출한 데이터를 중심으로 ExcelData를 추출하고
            // TODO : 바이너리 파일을 제작하도록 한다.

            // 엑셀 데이터를 생성한다.
            this.dataManager.CreateExcelData((p) => this.SetFormDataRule(p), 
                                             (p) => this.SetExtreactionProgressBar(p),
                                             (s,b) => this.SetProgressText(s,b)); 

            this.generateBinaryFile.Create(this.dataManager); // 바이너리 파일을 생성한다.
            
            // INFO : 기존 코드에서 폼이 시작될 때 실행되는 부분

            /*
            client_DataControl = new Thread(new ThreadStart(StartConvertClient));
            client_DataControl.Start();

            server_DataControl = new Thread(new ThreadStart(StartConvertServer));
            server_DataControl.Start();

            // 변환작업 쓰레드 종료를 체크하는 타이머를 구동한다.
            timerConvertChecker.Elapsed += new System.Timers.ElapsedEventHandler(Check_EndConvertWork); // 이벤트를 등록한다.
            timerConvertChecker.Start();
            */
        }

        /// <summary>
        /// 추출 프로그래스 바 진행을 나타낸다.
        /// </summary>
        /// <param name="progress"></param>
        public void SetExtreactionProgressBar(int progress)
        {
            this.ExtreactionReadyProgressBar.Value = progress;
        }

        /// <summary>
        /// 진행상황 텍스트를 나타낸다.
        /// </summary>
        /// <param name="text"></param>
        public void SetProgressText(string text, bool isEndLine = true)
        {
            if (isEndLine) // 종료 라인이라면 두줄을 띄우도록 한다.
            {
                this.ProgressText.AppendText(text + "\n\n");
            }
            else
            {
                this.ProgressText.AppendText(text + "\n");
            }

            this.ProgressText.ScrollToCaret();
            
        }

        // 쓰레드로 함수가 사용될 경우 인자를 넘기려면 object로 해야 하는데 편의상 함수를 랩핑하기로 함
        // TODO : 다국어 변환용 쓰레드 분리 (다국어 시간이 올래걸려 불편함이 있음)
        private void StartConvertClient() {this.StartConversion(SheetType.Client);}
        private void StartConvertServer() {this.StartConversion(SheetType.Server);}

        // 쓰레드로 폼을 제어하기 위한 델리게이트 함수 // 참조 : http://mrbongdal.tistory.com/9
        // 폼의 라벨을 제어한다.
        private void EditFormLabel_ClientThread( System.Windows.Forms.Label formLabel, string text)
        {
            if (formLabel.InvokeRequired)
            {
                callBackClient_SheetName sheetName = new callBackClient_SheetName(EditFormLabel_ClientThread);
                Invoke(sheetName, new object[] {formLabel, text });
            }
            else
            {
                formLabel.Text = text;
            }
        }

        // 서버 표현 부분의 라벨을 제어한다.
        private void EditFormLabel_ServerThread( System.Windows.Forms.Label formLabel, string text )
        {
            if (Server_ProgressText.InvokeRequired)
            {
                callBackServer_SheetName sheetName = new callBackServer_SheetName(EditFormLabel_ServerThread);
                Invoke(sheetName, new object[] {formLabel ,text });
            }
            else
            {
                formLabel.Text = text;
            }
        }

        // 프로그래스 바를 제어한다.
        private void EditFormPorgressBar_ClientThread(System.Windows.Forms.ProgressBar progressBar, int para)
        {
            //if (Client_ProgressBar.InvokeRequired)
            //{
            //    callBackClient_Progress progress = new callBackClient_Progress(EditFormPorgressBar_ClientThread);
            //    Invoke(progress, new object[] {progressBar, para});
            //}
            //else
            //{
            //    progressBar.Value = para;
            //}
        }

        // 프로그래스 바를 제어한다.
        private void EditFormPorgressBar_ServerThread(System.Windows.Forms.ProgressBar progressBar, int para)
        {
            //if (Client_ProgressBar.InvokeRequired)
            //{
            //    callBackServer_Progress progress = new callBackServer_Progress(EditFormPorgressBar_ServerThread);
            //    Invoke(progress, new object[] { progressBar, para });
            //}
            //else
            //{
            //    progressBar.Value = para;
            //}
        }

        //  프로그래스바를 설정한다.
        private void SetProgressbar()
        {
            ExtreactionReadyProgressBar.Style = ProgressBarStyle.Continuous;
            ExtreactionReadyProgressBar.Minimum = 0;
            ExtreactionReadyProgressBar.Maximum = 100;
            ExtreactionReadyProgressBar.Value = 0;

            //Client_ProgressBar.Style = ProgressBarStyle.Continuous;
            //Client_ProgressBar.Minimum = 0;
            //Client_ProgressBar.Maximum = 1000;
            //Client_ProgressBar.Value = 0;

            //Server_ProgressBar.Style = ProgressBarStyle.Continuous;
            //Server_ProgressBar.Minimum = 0;
            //Server_ProgressBar.Maximum = 1000;
            //Server_ProgressBar.Value = 0;
        }

        // 변환작업이 종료되었는지 체크한다.
        private void Check_EndConvertWork(object soruce, ElapsedEventArgs e)
        {
            // 클라이언트와 서버의 변환작업이 종료되었을 경우
            if (isEnd_ClientConvert && isEnd_ServerConvert)
            {
                this.End_ApplicationAndThreads();
            }
        }

        // 쓰레드와 어플리케이션을 종료한다.
        private void End_ApplicationAndThreads()
        {
            // 엑셀파일을 닫는다.
            excelApp.DisplayAlerts = false;
            // 어프리케이션을 종료한다.
            Application.Exit();

            // 쓰레드를 종료한다.
            this.AbortThreads();
        }

        // 쓰레드를 종료한다.
        private void AbortThreads()
        {
            client_DataControl.Abort();
            server_DataControl.Abort();
        }

        private void StartConversion(SheetType sheetType) // 변환을 시작한다.
        {
            // ******** 이건 시간이 넉넉할 때 ********//
            // TODO : ExcelData 클래스를 만들어 하위에 SheetData를 넣어서 처리하도록 한다. (SheetData는 현 Table 데이타)
            // TODO : ExcelData는 Excel의 모든 정보를 담고 있는 데이터이다.
            // TODO : 우선 ExcelData를 통해 모든 데이터를 읽고
            // TODO : ExcelData 클래스를 기반으로 바이너리파일, txt, json등으로 변환시킨다.
            // ***************************************//

            // 20180205 작업할 것
            // TODO : Tag에 설정되어 있는 Enum Type을 필드로 사용할 수 있게 변경한다. 

            List<string> sheetNames = null; // 시트이름들
            TableDataS tableData = new TableDataS();  // 데이터 테이블
            int totalCommentDataCount = 0;  // 전체 주석 개수
            int accumulate_ConvertedData = 0; // 누적된 데이터변환 개수
            double convert_ProcessPercent = 0; // 데이터 변경 진행도
            
            // 각각을 구분해서 데이터를 넣는다.
            switch (sheetType)
            {
                case SheetType.Client: // 데이터 기반을 클라이언트로 구성한다.
                    {
                        sheetNames = clientSheetNames;   // 클라이언트시트이름 리스트를 준비한다.
                        this.CreateClientBinaryFile(CLIENT_BINARY_FILE_EXENAME_FOR_UNITY); // 클라이언트 바이너리 파일을 최초 생성한다.

                        if (isExtractionText)  // 텍스트 파일을 추출할 것인가?
                        {
                            if (File.Exists(targetPathClientDB_Text)) File.Delete(targetPathClientDB_Text); // 파일이 존재한다면 삭제한다.

                            //File.Copy(clientDBFilePathAndName, targetPathForClientDB_Text);  // 파일을 복사한다.
                        }

                        // 클라이언트의 바이너리 파일을 읽을 때 사용한다.
                        //this.SetReadBinaryFileClient(); 

                        break;
                    }
                case SheetType.Server:  // 데이터 기반을 서버로 구성한다.
                    {
                        sheetNames = serverSheetNames;   // 서버시트이름 리스트를 준비한다.
                        //this.Create_ServerBinaryFile(SERVER_BINARY_FILE_EXENAME_FORUNITY); // 바이너리 파일을 최초 생성한다.
                        break;
                    }
            }

            string data_ExchangedString = "";

            // 서버 및 클라이언트에서 사용하는 시트 이름으로 하나씩 검색한다.
            foreach (string SheetsName in sheetNames)
            {
                if (sheetType == SheetType.Client)
                {
                    tableData = tableDatasClient[SheetsName];
                    dataManager.tableData = tableData;

                    // 현재 진행 시트의 이름을 폼에 보여준다.
                    this.EditFormLabel_ClientThread(Client_ProgressText, "진행 테이블 : " + tableData.name);
                }
                else if (sheetType == SheetType.Server)
                {
                    tableData = tableDatasServer[SheetsName];
                    dataManager.tableData = tableData;

                    // 현재 진행 시트의 이름을 폼에 보여준다.
                    this.EditFormLabel_ServerThread(Server_ProgressText, "진행 테이블 : " + tableData.name);
                }
                
                // 현재 시트를 할당한다.
                tableData.workSheet = sheets[SheetsName] as Excel.Worksheet;

                // 실제 사용되는 데이터 라인의 개수를 저장한다.(위의 3행(주석라인) 삭제된 행렬 개수)
                tableData.totalRowCountDeleteComment = tableData.totalRowCount - (tableData.rowCommentList.Count + m_Default_RowComment_LineCount);

                //************** 1. 주석 필드 및 행을 찾는다. 2. 자료형을 리스트에 담는다.********************
                for (int i = 1; i <= tableData.totalColumnCount; i++)
                {
                    // 필드 주석, 이름, 자료형을 저장한다.
                    object data_FieldComment = (tableData.workSheet.Cells[FIELD_COMMENTLINE, i] as Excel.Range).Value;
                    object data_FieldName = (tableData.workSheet.Cells[FIELDNAME_LINE, i] as Excel.Range).Value;
                    object data_FieldType = (tableData.workSheet.Cells[DATATYPE_LINE, i] as Excel.Range).Value;

                    // 해당 데이터의 오류를 검색한다.
                    dataManager.CheckData(tableData.name, FIELD_COMMENTLINE, i, data_FieldComment);

                    // 클라이언트 주석필드가 아닐경우 데이터를 처리한다.
                    if (!data_FieldComment.ToString().StartsWith(commentFieldMark))
                    {
                        // 서버라면 클라이언트 온리일 경우를 제외한다.
                        if (sheetType == SheetType.Server &&
                            data_FieldComment.ToString().StartsWith(commentFieldClientOnlyMark))
                        {
                            break;
                        }
                        dataManager.CheckData(tableData.name, FIELDNAME_LINE, i, data_FieldName);
                        dataManager.CheckData(tableData.name, DATATYPE_LINE, i, data_FieldType);

                        // 필드 이름을 리스트에 담는다.
                        tableData.fieldNameList.Add(data_FieldName.ToString());

                        // 필드 자료형을 리스트에 담는다.
                        tableData.fieldDataTypeCSharp.Add(this.ConvertToCSharpTypeFromExcel(data_FieldType.ToString()));
                        tableData.fieldDataTypeTable.Add(data_FieldType.ToString());
                    }
                    else
                    {
                        // 주석 필드를 저장한다.
                        tableData.fieldCommentList.Add(i); // TODO : 작동 확인해 볼 것
                    }
                }

                //************* 1. 행의 첫번째 필드를 검사하여 주석 행을 배열화 한다.
                for (int i = 1; i <= tableData.totalRowCount; i++)
                {
                    object data_RowComment = (tableData.workSheet.Cells[i, ROW_COMMENTFIELD] as Excel.Range).Value;

                    // 행 주석문이 Null인지 검사한다.
                    dataManager.CheckData_RowComment_IfNull(tableData.name, i, data_RowComment);

                    // 행 주석문을 검사하고 주석문을 리스트에 담는다.
                    if (data_RowComment.ToString().StartsWith(commentRowMark))
                    {
                        tableData.rowCommentList.Add(i);
                        tableData.totalRowCountDeleteComment--;
                    }
                }

                // 주석처리 카운트(첫번째 3줄을 주석으로 처리한다)
                totalCommentDataCount += tableData.totalColumnCount * m_Default_RowComment_LineCount;

                // C# 코드에 해당 테이블 코드를 작성한다.
                if (sheetType == SheetType.Client)
                {
                    // 테스트 텍스트 파일에 변경된 파일 뿌려본다.
                    //this.ReadBinaryFileForClient(tableData);

                    //Create_CSharpCode.Instance.WriteCode_TableConverter(tableData);
                    //Create_CSharpCode.Instance.WriteCode_TableClassList(tableData);
                }
                else if (sheetType == SheetType.Server) // PHP 코드를 작성한다.
                {
                    //Create_PHPCode.Instance.WriteCode_TableConverter_PHP(tableData);
                }

                //*****데이터를 클라이언트 및 서버의 바이너리 파일로 변환한다.****************************************
                // 행을 검사한다.
                for (int row = m_Default_RowComment_LineCount + 1; row <= tableData.totalRowCount; row++)
                {
                    int dataCount = 0; // 데이터 타입 저장시 필요한 카운트(데이터 타입은 리스트화 되어 있다.)

                    // 주석 행이라면 다음 행으로 넘어간다.
                    if (tableData.rowCommentList.Contains(row))
                    {
                        totalCommentDataCount += tableData.totalColumnCount; // 주석처리 카운트를 센다.

                        //if (sheetType == SheetType.Client) EditFormLabel_ClientThread(Client_Count_CommentData, "주석처리 데이터 : " + (totalCommentDataCount).ToString("#,###") + " 개");
                        //else if (sheetType == SheetType.Server) EditFormLabel_ClientThread(Server_Count_CommentData, "주석처리 데이터 : " + (totalCommentDataCount).ToString("#,###") + " 개");

                        continue;
                    }

                    // 필드를 검사한다.
                    for (int column = 1; column <= tableData.totalColumnCount; column++)
                    {
                        // 주석 필드라면 다음 필드로 넘어간다.
                        if (tableData.fieldCommentList.Contains(column))
                        {
                            ++totalCommentDataCount; // 주석처리 카운를 1증가시킨다.

                            //if (sheetType == SheetType.Client) EditFormLabel_ClientThread(Client_Count_CommentData, "주석처리 데이터 : " + (totalCommentDataCount).ToString("#,###") + " 개");
                            //else if (sheetType == SheetType.Server) EditFormLabel_ClientThread(Server_Count_CommentData, "주석처리 데이터 : " + (totalCommentDataCount).ToString("#,###") + " 개");
                        }
                        // 주석 필드가 아니라면
                        else
                        {
                            // 데이터 타입의 저장 카운트를 상승시킨다.
                            dataCount++;

                            // 데이터를 넣는다.
                            //object data = (tableData.m_WorkSheet.Cells[row, column] as Excel.Range).Value2;
                            object data = (tableData.workSheet.Cells[row, column] as Excel.Range).Text; // 엑셀에 있는 텍스트 그대로 읽어온다 만약 ###로 되어 있다면(자리가 부족해서 나타나는) ### 그대로 읽어온다.
                            // 데이터의 수를 샌다.
                            ++accumulate_ConvertedData;

                            //m_Data_Management.Check_RowComment_Data();



                            // 데이터를 변환하기 쉽도록 string 문으로 처리한다.
                            if (data != null)
                            {
                                data_ExchangedString = data.ToString();

                                // INFO : 컬러 값 넣는 부분에 의해서 주석처를 하지 않는다.
                                // TODO : 이 부분을 주석처리할 지 엑셀 시작 페이지에 값을 넣어 체크하도록 한다.
                                // # 오류 처리
                                //if (data_ExchangedString.Contains("#"))
                                //{
                                //    MessageBox.Show("데이터에 #이 들어가 있습니다. [테이블 : " + tableData.m_Name + "] [행 : " + row + "] [열 : " + column + "]");
                                //    Close();
                                //}
                            }
                            else
                            {
                                if (!tableData.fieldDataTypeCSharp[dataCount - 1].Equals("string"))
                                {
                                    MessageBox.Show(tableData.fieldDataTypeTable[dataCount - 1] + "의 자료형에 \"null\"값이 들어가 있습니다. [테이블 : " + tableData.name + "] [행 : " + row + "] [열 : " + column + "]");
                                    Close();
                                }
                                else data_ExchangedString = null;
                            }

                            if (sheetType == SheetType.Client)
                            {
                                // 데이터를 바이너리로 변한한다. (테이블 행과열은 1부터 시작하기 때문에 list 자료를 순서대로 차출하기 위해서 1을 뺀다.)
                                this.Write_ToClientDB(tableData.fieldDataTypeTable[dataCount - 1], data_ExchangedString, tableData, row, column);

                                // PR 테이블일 경우
                                if (tableData.name.Equals("PR"))
                                {
                                    // PR 태그 클래스를 만들기 위한 자료를 추출한다.
                                    Create_CSharpCode.Instance.SetDicPR(tableData.fieldNameList[dataCount - 1], data_ExchangedString);
                                }

                                // Tag 테이블일 경우
                                if (tableData.name.Equals("Tag"))
                                {
                                    Create_CSharpCode.Instance.SetDicTag(tableData.fieldNameList[dataCount - 1], data_ExchangedString);
                                }
                            }
                            else if (sheetType == SheetType.Server)
                            {
                                //this.Write_ToServerDB(tableData.m_FieldDataType_Table[dataTypeCount - 1], data_ExchangedString, tableData, row, column);
                                this.Query_TableData(tableData.fieldDataTypeTable[dataCount - 1], tableData.fieldNameList[dataCount - 1], data_ExchangedString, tableData, row, column); 
                            }
                        }

                        // 폼에 나타낸기
                        if (sheetType == SheetType.Client)
                        {
                            // 진행도 계산
                            convert_ProcessPercent = (double)(accumulate_ConvertedData + totalCommentDataCount) / totalDataCountClient;

                            // 폼에서 나타나는 부분
                            //EditFormLabel_ClientThread(Client_Target_Data, "데이터 : " + data_ExchangedString);
                            //EditFormLabel_ClientThread(Client_Leftover_Data, "남은 데이터 : " + (totalDataCountClient - (accumulate_ConvertedData + totalCommentDataCount)).ToString("0,###") + " 개");
                            //EditFormLabel_ClientThread(Client_Count_ConvertData, "변경된 데이터 : " + (accumulate_ConvertedData).ToString("#,###") + " 개");
                            //EditFormLabel_ClientThread(Client_Total_ProcessPercent, "진행도 : " + convert_ProcessPercent.ToString("0.#####%"));
                             
                            //EditFormPorgressBar_ClientThread(Client_ProgressBar, (int)(convert_ProcessPercent * 1000));
                        }
                        else if (sheetType == SheetType.Server)
                        {
                            convert_ProcessPercent = (double)(accumulate_ConvertedData + totalCommentDataCount) / totalDataCountForServer;

                            // 폼에서 나타나는 부분
                            //EditFormLabel_ClientThread(Server_Target_Data, "데이터 : " + data_ExchangedString);
                            //EditFormLabel_ClientThread(Server_Leftover_Data, "남은 데이터 : " + (totalDataCountForServer - (accumulate_ConvertedData + totalCommentDataCount)).ToString("0,###") + " 개");
                            //EditFormLabel_ClientThread(Server_Count_ConvertData, "변경된 데이터 : " + (accumulate_ConvertedData).ToString("#,###") + " 개");
                            //EditFormLabel_ClientThread(Server_Total_ProcessPercent, "진행도 : " + convert_ProcessPercent.ToString("0.#####%"));

                            //EditFormPorgressBar_ServerThread(Server_ProgressBar, (int)(convert_ProcessPercent * 1000));
                        }
                    }
                }

                // Multilingual, PR Tag 클래스를 만든다.
                if (tableData.name.Equals("PR"))
                {
                    Create_CSharpCode.Instance.WriteCode_TableTagMultilingualPR(tableData);
                }
                else if (tableData.name.Equals("Tag")) // tag 테이블을 기반으로 Class를 작성한다.
                {
                    Create_CSharpCode.Instance.WriteCode_TableTag(tableData);
                }
            }

            if (sheetType == SheetType.Client)
            {
                Create_CSharpCode.Instance.Close_StreamWrite(); // C# 파일 스트림을 닫는다.

                binaryWriter_ForClientDB.Close(); // 바이너리 파일을 닫는다.

                /*
                // 바이너리 파일을 유니티 Resources에 파일을 복사한다.
                if (File.Exists(m_TargetPathForClientDB)) File.Delete(m_TargetPathForClientDB); // 파일이 존재한다면 삭제한다.
                //File.Copy(m_ClientDBFilePathAndName, m_TargetPathForClientDB);  // 파일을 이동시킨다.
                File.Move(m_ClientDBFilePathAndName, m_TargetPathForClientDB);  // 파일을 이동시킨다.
                */

                // 바이너리 파일을 서버를 통한 버전 업데이트(최초 스토어의 업데이트 이후의 클라이언트의 테이블은 서버로부터 다운로드 시킨다)를 위한 복사
                if (File.Exists(targetPathClientDB_Binary)) File.Delete(targetPathClientDB_Binary); // 파일이 존재한다면 삭제한다.
                File.Move(originalPathClientDB_Binary, targetPathClientDB_Binary);  // 파일을 이동시킨다.

                // TableConverter.cs 파일을 해당 위치로 복사한다.
                if (File.Exists(targetPathTableConverter)) File.Delete(targetPathTableConverter);
                File.Move(originalPathTableConverter, targetPathTableConverter);

                // TableClassList.cs 파일을 해당 위치로 복사한다.
                if (File.Exists(targetPathTableClassList_Unity)) File.Delete(targetPathTableClassList_Unity);
                File.Move(originalPathTableClassList_Unity, targetPathTableClassList_Unity);

                // TableTagList.cs 파일을 해당 위치로 복사한다.
                if (File.Exists(targetPathForTableTagList)) File.Delete(targetPathForTableTagList);
                File.Move(createdPathNameForTableTagList, targetPathForTableTagList);

                isEnd_ClientConvert = true; // 클라이언트 변환 작업이 종료되었음을 알린다.
            }
            if (sheetType == SheetType.Server)
            {
                //Create_PHPCode.Instance.Close_StreamWrite();  //PHP 코드를 작성하는 파일 스트림을 닫는다.

                //m_BinaryWriter_ForServerDB.Close(); // 바이너리 파일을 닫는다.

                //DirectoryInfo di = new DirectoryInfo(m_WorkSheet.Range["J6"].Value);    // 이동할 폴더가 존재하는지 검사한다.

                //if (File.Exists(m_TargetPathForServerDB)) File.Delete(m_TargetPathForServerDB); // 바이너리 파일이 존재한다면 삭제한다.
                //if (File.Exists(m_ServerDBFilePathAndName)) File.Delete(m_ServerDBFilePathAndName);

                isEnd_ServerConvert = true; // 서버 변환 작업이 종료되었음을 알린다.

                /*  차후 사용하도록 한다.
                if (di.Exists)
                {
                    if (File.Exists(m_TargetPathForServerDB)) File.Delete(m_TargetPathForServerDB); // 바이너리 파일이 존재한다면 삭제한다.
                    if (File.Exists(m_TargetPathForPHPFile)) File.Delete(m_TargetPathForPHPFile);

                    File.Move(m_ServerDBFilePathAndName, m_TargetPathForServerDB);  // 파일을 이동시킨다.
                    File.Move(Application.StartupPath + "\\" + Create_PHPCode.TABLECONVERTER_FILENAME, m_TargetPathForPHPFile);  // PHP 파일을 이동시킨다.

                    m_IsEnd_ServerConvert = true; // 서버 변환 작업이 종료되었음을 알린다.
                }
                else // 존재하지 않다면 오류창을 팝업시킨다.
                {
                    m_Client_DataControl.Abort();   // 클라이언트 쓰레드를 종료시킨다. (오류가 난 상황에서 클라이언트 변환 작업이 진행되고 있는게 보이면 시각적으로 이상하기 때문)
                    MessageBox.Show("서버데이터DB : " + SERVER_BINARY_FILENAME + "." + SERVER_BINARY_FILE_EXENAME_FORUNITY + " 가 들어갈 폴더가 존재하지 않습니다.\n 폴더경로 : " + m_WorkSheet.Range["J6"].Value);
                    this.End_ApplicationAndThreads();
                }
                */
            }
        }

        public void CloseExcel() // 엑셀을 닫는다.
        {
            // TODO : 실행 완료 후 정상 종료일 때 두번 호출되는 부분 수정할 것

            try
            {   
                workBook.Close(0);
                excelApp.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        string ConvertToCSharpTypeFromExcel(string tableDataType)
        {
            string cSharpDataType = null;

            if (tableDataType.Equals("Bit")) cSharpDataType = "bool";
            else if (tableDataType.Equals("TinyInt")) cSharpDataType = "byte";
            else if (tableDataType.Equals("SmallInt")) cSharpDataType = "short";
            else if (tableDataType.Equals("Int")) cSharpDataType = "int";
            else if (tableDataType.Equals("BigInt")) cSharpDataType = "long";
            else if (tableDataType.Equals("Float")) cSharpDataType = "float"; 
            else if (tableDataType.Equals("Double")) cSharpDataType = "double"; 
            else if (tableDataType.StartsWith("Char")) cSharpDataType = "string";
            else if (tableDataType.StartsWith("VarChar")) cSharpDataType = "string";
            else
            {
                MessageBox.Show("정의되지 않는 자료형이 입력되었습니다. [테이블 : " + tableDataClient.name + "] [자료형 : " + tableDataType + "]");
                Close();
            }

            return cSharpDataType;
        }

        // 버전을 표시 합니다.
        void DisplayVersion()
        {
            // 버전 표시
            this.Text = "MarkTwo v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        private void SetTableDatas() // 데이터 테이블(딕셔너리)를 설정한다.
        {
            // 클라이언트 테이블 정보를 설정한다.
            foreach (string clientSheetName in clientSheetNames)
            {
                TableDataS tableDataClient = new TableDataS();

                Excel.Worksheet clientDB_Sheet = sheets[clientSheetName] as Excel.Worksheet;

                // tableData 객체에 테이블 시트의 기본 정보를 할당한다
                tableDataClient.name = clientSheetName;
                tableDataClient.totalColumnCount = clientDB_Sheet.Cells.Find("*", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious, false, false, Type.Missing).Column;
                tableDataClient.totalRowCount = clientDB_Sheet.Cells.Find("*", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious, false, false, Type.Missing).Row;
                
                tableDatasClient.Add(tableDataClient.name, tableDataClient); // 설정된 테이블 클래스를 딕셔너리에 할당한다.
                
                totalDataCountClient = totalDataCountClient + (tableDataClient.totalColumnCount * tableDataClient.totalRowCount); // 총 데이터의 개수를 구한다.
            }

            // 서버 테이블 정보를 설정한다.
            foreach (string serverSheetName in serverSheetNames)
            {
                TableDataS tableDataServer = new TableDataS();

                Excel.Worksheet serverDB_Sheet = sheets[serverSheetName] as Excel.Worksheet;

                // tableData 객체에 정보를 할당한다
                tableDataServer.name = serverSheetName;
                tableDataServer.totalColumnCount = serverDB_Sheet.Cells.Find("*", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious, false, false, Type.Missing).Column;
                tableDataServer.totalRowCount = serverDB_Sheet.Cells.Find("*", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious, false, false, Type.Missing).Row;

                // 설정된 테이블 클래스를 딕셔너리에 할당한다.
                tableDatasServer.Add(tableDataServer.name, tableDataServer);

                // 총 데이터의 개수를 구한다.
                totalDataCountForServer = totalDataCountForServer + (tableDataServer.totalColumnCount * tableDataServer.totalRowCount);
            }

            // 폼의 클라이언트 진행도 부분을 초기화 한다.
            //Client_TotalCount_Table.Text = "전체 테이블 : " + tableDatasClient.Count.ToString() + " 개";
            //Client_TotalCount_Data.Text = "전체 데이터 : " + totalDataCountClient.ToString("#,###") + " 개";
            //Client_Leftover_Data.Text = "남은 데이터 : " + totalDataCountClient.ToString("#,###") + " 개";
            //Client_Count_CommentData.Text = "주석처리 데이터 : " + "0 개";
            //Client_Count_ConvertData.Text = "변경된 데이터 : " + "0 개";
            //Client_Total_ProcessPercent.Text = "진행도 :  " + "0%";

            //// 폼의 서버 진행도 부분을 초기화 한다.
            //Server_TotalCount_Table.Text = "전체 테이블 : " + tableDatasServer.Count.ToString() + " 개";
            //Server_TotalCount_Data.Text = "전체 데이터 : " + totalDataCountForServer.ToString("#,###") + " 개";
            //Server_Leftover_Data.Text = "남은 데이터 : " + totalDataCountForServer.ToString("#,###") + " 개";
            //Server_Count_CommentData.Text = "주석처리 데이터 : " + "0 개";
            //Server_Count_ConvertData.Text = "변경된 데이터 : " + "0 개";
            //Server_Total_ProcessPercent.Text = "진행도 :  " + "0%";
        }

        // 시작 버튼을 눌렀을 시
        private void StartButton_Click(object sender, EventArgs e)
        {
            //this.Start_Convert();
        }

        // 엑셀 시트를 추출한다.
        private void GetExcelSheets()
        {
            this.excelApp = new Excel.Application();
            this.workBook = this.excelApp.Workbooks.Open(this.ExcelFilePath(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            this.sheets = workBook.Sheets;
        }

        // 엑셀의 파일경로를 기록한다.
        private string ExcelFilePath()
        {
            string Current_FilePath;

            Current_FilePath = Application.StartupPath + "\\ExcelionDB.xlsm";
            Excel_Directory.Text = "엑셀 경로 : " + Current_FilePath;

            return Current_FilePath;
        }

        // 클라이언트에서 사용할 시트 이름을 추출한다.
        private void GetClientSheetNames()
        {
            workSheet = sheets["테이블관리"] as Excel.Worksheet;

            foreach (string ClientSheets_Name in workSheet.get_Range("B8", "B50").Value)
            {
                if (ClientSheets_Name == null) break;

                clientSheetNames.Add(ClientSheets_Name);
            }
        }

        // 서버에서 사용될 시트 이름을 추출한다.
        public void GetServerSheetNames()
        {
            foreach (string ServerSheets_Name in workSheet.get_Range("C8", "C50").Value)
            {
                if (ServerSheets_Name == null) break;

                serverSheetNames.Add(ServerSheets_Name);
            }
        }

        // 규칙 관련 정보를 추출한다.
        private void GetRuleInfo()
        {
            workSheet = sheets["테이블_규칙"] as Excel.Worksheet;

            commentRowMark = workSheet.Range["B16"].Value;
            commentFieldMark = workSheet.Range["B17"].Value;
            commentFieldClientOnlyMark = workSheet.Range["B18"].Value;
            
            // 각 주석의 공백을 제거한다.
            commentFieldMark = commentFieldMark.Trim();
            commentFieldClientOnlyMark = commentFieldClientOnlyMark.Trim();
            commentRowMark = commentRowMark.Trim();

            //Default_RowComment_LineCount.Text = "기본 주석 행 : " + m_Default_RowComment_LineCount.ToString() + " 행";
            //Field_Comment.Text = "필드 주석 : " + commentFieldMark + "&"; // "&"만 하면 표시되지 않는다.
            //Field_Comment_ClientOnly.Text = "필드 주석(클라이언트 사용) : " + commentFieldClientOnlyMark;
            //Row_Comment.Text = "행 주석 : " + commentRowMark;
            
            try
            {
                string isTextFile = workSheet.Range["H23"].Value;
                string isBinary = workSheet.Range["H24"].Value;
                string isJson = workSheet.Range["H25"].Value;
                string isCSV = workSheet.Range["H26"].Value;
                string isXML = workSheet.Range["H27"].Value;

                // 무결성 검사를 한다.
                if (isTextFile.Equals("On") || isTextFile.Equals("Off")) this.isExtractionText = (isTextFile.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [Text 문서] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");

                if (isBinary.Equals("On") || isBinary.Equals("Off")) this.isExtractionBinary = (isBinary.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [바이너리파일] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");

                if (isJson.Equals("On") || isJson.Equals("Off")) this.isExtractionJson = (isJson.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [Json] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");

                if (isCSV.Equals("On") || isCSV.Equals("Off")) this.isExtractionCSV = (isCSV.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [CSV] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");

                if (isXML.Equals("On") || isXML.Equals("Off")) this.isExtractionXML = (isXML.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [XML] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");

                // Form에 표시한다.
                //IsTextFile.Text = "Text 문서 : " + isTextFile;
                //IsBinary.Text = "바이너리파일 : " + isBinary;
                //IsJson.Text = "Json : " + isJson;
                //IsCSV.Text = "CSV : " + isCSV;
                //IsXML.Text = "XML : " + isXML;
            }
            catch (Exception)
            {
                dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 [※ 추출할 파일 형식]의 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");
            }
        }

        private void GetClientCSharpType()
        {
            clientDataTypes = new List<string>();
            //ClientTypeList.Text = null;

            // 8개의 자료형을 가진다. 만약 엑셀에서 자료형을 추가한다면 이 부분을 수정해야 한다.
            for (int i = 0; i < CLIENT_TYPE_COUNT; i++)
            {
                string range = "D" + (22 + i).ToString();
                string type = workSheet.Range[range].Value;

                clientDataTypes.Add(type); // 리스트에 자료형을 등록한다.

                //ClientTypeList.Text += type + "\n"; // 라벨에 표시한다.
            }
        }

        // MySQL 자료형을 등록한다.
        private void GetServerMySQLType()
        {
            serverDataType = new List<string>();
            //ServerTypeList.Text = null;

            // 8개의 자료형을 가진다. 만약 엑셀에서 자료형을 추가한다면 이 부분을 수정해야 한다.
            for (int i = 0; i < SERVER_TYPE_COUNT; i++)
            {
                string range = "C" + (22 + i).ToString();
                string type = workSheet.Range[range].Value;

                serverDataType.Add(type);

                //ServerTypeList.Text += type + "\n";
            }
        }

        // 타겟 폴더가 존재하는지 체크한다.
        void CheckTagetFolder()
        {
            string severDBDirectory = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q14"].Value;
            string clientDBDirectory = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q15"].Value;
            string csFile = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q16"].Value;

            // 서버 DB 저장경로
            if (!Directory.Exists(severDBDirectory))
            {
                dataManager.ShowCloseMSB("서버관련 DB 파일이 위치할 폴더 경로가 존재하지 않습니다. " +
                    "\n" +
                    "\n테이블_규칙에 설정된 폴더 : " + workSheet.Range["Q14"].Value +
                    "\n전체경로 : " + severDBDirectory +
                    "\n " +
                    "\n[ 테이블_규칙 ] 시트에서 [ 서버 ClientDB 저장경로 ]를 확인하시기 바랍니다.");
            }

            // 클라이언트 ClientDB 저장경로
            if (!Directory.Exists(clientDBDirectory))
            {
                dataManager.ShowCloseMSB("클라이언트 바이너리 파일이 위치할 폴더 경로가 존재하지 않습니다. " +
                    "\n" +
                    "\n테이블_규칙에 설정된 폴더 : " + workSheet.Range["Q15"].Value +
                    "\n전체경로 : " + clientDBDirectory +
                    "\n " +
                    "\n[ 테이블_규칙 ] 시트에서 [ 클라이언트 ClientDB 저장경로 ]를 확인하시기 바랍니다.");
            }

            // TableClass 및 TableConverter 등 파일 저장경로
            if (!Directory.Exists(csFile))
            {
                dataManager.ShowCloseMSB("TableClass.cs, TableConverter.cs, TableTagList.cs 파일이 위치할 폴더 경로가 존재하지 않습니다. " +
                    "\n" +
                    "\n테이블_규칙에 설정된 폴더 : " + workSheet.Range["Q16"].Value +
                    "\n전체경로 : " + csFile +
                    "\n " +
                    "\n[ 테이블_규칙 ] 시트에서 [ TableClass 및 TableConverter 등 파일 저장경로 ]를 확인하시기 바랍니다.");
            }
        }

        // 클라이언트의 바이너리 파일을 생성한다.
        void CreateClientBinaryFile(string exeName = "ksk")
        {
            //m_ClientDBFilePathAndName = Application.StartupPath + "\\" + CLIENT_BINARY_FILENAME + "." + exeName;
            clientFileStream = new FileStream(originalPathClientDB_Binary, FileMode.Create);
            binaryWriter_ForClientDB = new BinaryWriter(clientFileStream);
        }

        // 서버의 바이너리 파일을 생성한다.
        void Create_ServerBinaryFile(string exeName = "ksk")
        {
            //m_ServerDBFilePathAndName = Application.StartupPath + "\\" + SERVER_BINARY_FILENAME + "." + exeName;
            serverFileStream = new FileStream(originalPathSeverDB_Binary, FileMode.Create);
            binaryWriter_ForServerDB = new BinaryWriter(serverFileStream);
        }

        // 클라이언트의 바이너리 파일이 이동할 곳의 경로 및 파일이름을 만든다..
        //void Create_TargetPathForClientDB(string exeName = "dat")
        //{
        //    targetPathForClientDB = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q15"].Value + "\\" + CLIENT_BINARY_FILENAME + "." + exeName;
        //}

        // 서버의 바이너리 파일이 이동할 곳의 경로 및 파일이름을 만든다.
        //void CreateTargetPathForServerDB(string exeName = "dat")
        //{
            // TODO : 위치를 변경하도록 한다.
            //targetPathForServerDB = Application.StartupPath + workSheet.Range["Q14"].Value + "\\" + SERVER_BINARY_FILENAME + "." + exeName;
        //}

        // PHP 파일이 이동할 위치를 만든다.
        //void Create_TargetPathForPHP(string exeName = "dat")
        //{
        //    targetPathForPHPFile = workSheet.Range["Q5"].Value + "\\" + Create_PHPCode.TABLECONVERTER_FILENAME;
        //}

        // 클라이언트 dB의 업데이트를 위한 경로 및 파일이름을 만든다
        //void Create_TargetPathForClientDB_Update(string exeName = "dat")
        //{
        //    targetPathClientDB_Binary = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q15"].Value + "\\" + CLIENT_BINARY_FILENAME + "." + exeName;
        //}

        // 유니티에서 사용되는 TableClassList.cs 파일 경로를 생성한다.
        private void CreatePathTableClassList()
        {
            originalPathTableClassList_Unity = Application.StartupPath + "\\" + Create_CSharpCode.TABLECLASSLIST_FILENAME; // 현재위치
            targetPathTableClassList_Unity = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q16"].Value + "\\" + Create_CSharpCode.TABLECLASSLIST_FILENAME; // 복사할 위치
        }

        // 유니티에서 사용되는 TableConverter.cs 파일 경로를 생성한다.
        private void CreatePathTableConverter()
        {
            originalPathTableConverter = Application.StartupPath + "\\" + Create_CSharpCode.TABLECONVERTER_FILENAME; // 현재위치
            targetPathTableConverter = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q16"].Value + "\\" + Create_CSharpCode.TABLECONVERTER_FILENAME; // 복사할 위치
        }

        // 유니티에서 사용되는 TableTagList 클래스가 복사될 파일 경로를 나타낸다.
        private void CreatePathTableTagList()
        {
            createdPathNameForTableTagList = Application.StartupPath + "\\" + Create_CSharpCode.TABLETAGLIST_FILENAME; // 현재위치
            targetPathForTableTagList = Application.StartupPath.Replace("\\ADDesign", "") + "\\" + workSheet.Range["Q16"].Value + "\\" + Create_CSharpCode.TABLETAGLIST_FILENAME; // 복사할 위치
        }

        // 클라이언트 DB에 바이너리 파일을 쓴다.
        private void Write_ToClientDB(string dataType, string data_ExchangedString, TableDataS tableData, int row, int column)
        {
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
                MessageBox.Show("클라이언트DB시트에 데이터 입력이 잘못되었습니다. [테이블 : " + tableData.name + "] [필드 이름 : " + tableData.fieldNameList[column - 1] + "]" +"] [행 : " + row);
                Close();
            }
            //EditFormLabel_ClientThread(Client_FileSize, "누적 바이트 : " + binaryWriter_ForClientDB.BaseStream.Length.ToString("###,###") + " Byte");
        }

        // 서버 DB에 바이너리 파일을 쓴다.
        void Write_ToServerDB(string dataType, string data_ExchangedString, TableDataS tableData, int row, int column)
        {
            try
            {
                if (dataType.Equals("Bit")) binaryWriter_ForServerDB.Write(Convert.ToBoolean(data_ExchangedString));
                else if (dataType.Equals("TinyInt")) binaryWriter_ForServerDB.Write(Convert.ToByte(data_ExchangedString));
                else if (dataType.Equals("SmallInt")) binaryWriter_ForServerDB.Write(Convert.ToInt16(data_ExchangedString));
                else if (dataType.Equals("Int")) binaryWriter_ForServerDB.Write(Convert.ToInt32(data_ExchangedString));
                else if (dataType.Equals("BigInt")) binaryWriter_ForServerDB.Write(Convert.ToInt64(data_ExchangedString));
                else if (dataType.Equals("Float")) binaryWriter_ForServerDB.Write(Convert.ToSingle(data_ExchangedString));
                else if (dataType.Equals("Double")) binaryWriter_ForServerDB.Write(Convert.ToDouble(data_ExchangedString));
                else if (dataType.StartsWith("Char") || dataType.StartsWith("VarChar"))
                {
                    if (string.IsNullOrEmpty(data_ExchangedString)) data_ExchangedString = "";
                    binaryWriter_ForServerDB.Write(Convert.ToString(data_ExchangedString));
                }
                else
                {
                    MessageBox.Show("서버DB시트에 정의되지 않는 자료형이 입력되었습니다. [테이블 : " + tableData.name + "] [행 : " + row + "] [열 : " + column + "]");
                    Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("서버DB시트에 데이터 입력이 잘못되었습니다. [테이블 : " + tableData.name + "] [필드 이름 : " + tableData.fieldNameList[column - 1] + "]" + "] [행 : " + row);
                Close();
            }
            //EditFormLabel_ClientThread(Server_FileSize, "누적 바이트 : " + binaryWriter_ForServerDB.BaseStream.Length.ToString("###,###") + " Byte");
        }

        void Query_TableData(string dataType, string fieldName, string data_ExchangedString, TableDataS tableData, int row, int column)
        {
            try
            {
                if (fieldName.Equals("Num")) //필드가 Num일 경우
                {
                    m_Reids_Num = data_ExchangedString; // 레디스의 핃드 번호를 저장하도록 하며, 필드 번호저장이기 때문에 데이터를 전송하지는 않는다.
                }
                else
                {
                    if (IsConnectServer) // 서버가 비활성일 경우 통신하지 않는다.
                    {
                        conn.Hashes.Set(1, tableData.name + ":" + m_Reids_Num, fieldName, data_ExchangedString); // 필드 번호와 함께 데이터를 전송한다.
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("서버DB시트에 데이터 입력이 잘못되었습니다. [테이블 : " + tableData.name + "] [필드 이름 : " + tableData.fieldNameList[column - 1] + "]" + "] [행 : " + row);
                Close();
            }
        }

        // 단축키 설정 오버로드 함수
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!base.ProcessCmdKey(ref msg, keyData))
            {
                // r 단축키를 눌렀을 경우 실행된다.
                //this.Start_Convert(SheetType.Client);

                if (keyData.Equals(Keys.R)) return true;
                else return false;
            }
            else return true;
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void test_Click(object sender, EventArgs e)
        {

        }

        void SetReadBinaryFileClient()
        {
            if (!isExtractionText) return;  // 텍스트 추출 모드인가?

            //m_ClientDBFilePathAndName = Application.StartupPath + "\\" + CLIENT_BINARY_FILENAME + "." + CLIENT_BINARY_FILE_EXENAME_FORUNITY;
            string textFile = Application.StartupPath + "\\" + CLIENT_BINARY_FILENAME + "." + "txt";

            binaryReader = new BinaryReader(new FileStream(targetPathClientDB_Text, FileMode.Open));
            streamWriter = new StreamWriter(new FileStream(textFile, FileMode.Create));
        }

        // TODO : 잘못된 코드 수저할 것(쓰레드를 돌려서 처리할 것)
        void ReadBinaryFileForClient(TableDataS tableData)
        {
            if (!isExtractionText) return;  // 텍스트 추출 모드인가?

            for (int column = 0; column < tableData.rowCommentList.Count; column++)
            {
                int row = 0;

                try
                {
                    for (row = 0; row < tableData.fieldDataTypeCSharp.Count; row++)
                    {
                        string fieldName = tableData.fieldNameList[row];
                        string fieldType = tableData.fieldDataTypeCSharp[row];
                        string data = null;

                        if (fieldType.Equals("bool")) data = binaryReader.ReadBoolean().ToString();
                        else if (fieldType.Equals("byte")) data = binaryReader.ReadByte().ToString();
                        else if (fieldType.Equals("short")) data = binaryReader.ReadInt16().ToString();
                        else if (fieldType.Equals("int")) data = binaryReader.ReadInt32().ToString();
                        else if (fieldType.Equals("float")) data = binaryReader.ReadSingle().ToString();
                        else if (fieldType.Equals("double")) data = binaryReader.ReadDouble().ToString();
                        else if (fieldType.Equals("long")) data = binaryReader.ReadInt64().ToString();
                        else if (fieldType.Equals("string")) data = binaryReader.ReadString().ToString();

                        streamWriter.WriteLine(data);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("데이터 입력이 잘못되었습니다. [테이블 : " + tableData.name + "] [필드 이름 : " + tableData.fieldNameList[column - 1] + "]" + "] [행 : " + row);
                    Close();
                }
            }
        }

        // 프로그램이 닫힐 때 실행된다.
        private void ClostMarkTwo(object sender, FormClosedEventArgs e)
        {
            switch(e.CloseReason)
            {
                case CloseReason.WindowsShutDown : this.CloseExcel(); break;
                case CloseReason.FormOwnerClosing : this.CloseExcel(); break;
                case CloseReason.MdiFormClosing : this.CloseExcel(); break;
                case CloseReason.TaskManagerClosing : this.CloseExcel(); break;
                case CloseReason.UserClosing : this.CloseExcel(); break;
                case CloseReason.None: this.CloseExcel(); break;
                case CloseReason.ApplicationExitCall:
                    {
                        this.CloseExcel();

                        if (isEnd_ClientConvert && isEnd_ServerConvert) // 변환 작업이 정상적으로 종료되었는가?
                        {
                            endFileConvert.Play();

                            MessageBox.Show("변환이 완료되었습니다.");
                        }

                        break;
                    }
            }
        }

        private void TargetPath_Click(object sender, EventArgs e)
        {

        }

        private void Excel_Directory_Click(object sender, EventArgs e)
        {

        }

        private void ServerTypeList_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ConverterWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Server_Target_Data_Click(object sender, EventArgs e)
        {

        }
    }

    /// 클라이언트 엑셀시트 데이터를 저장하는 클래스
    public class TableDataS
    {
        public Excel.Worksheet workSheet; // 시트 정보

        public string name;
        public int totalRowCount; // 테이블의 행 숫자
        public int totalRowCountDeleteComment; // 테이블의
        public int totalColumnCount; // 테이블의 모든 열 숫자

        public List<int> fieldCommentList = new List<int>(); // 필드의 주석을 저장한다(필드 번호 순 ex) {2,7}이면 2열 7열이 주석필드)
        public List<int> rowCommentList = new List<int>(); // 행의 주석 번호를 저장한다.(행 번호 순 ex) {2,3}이면 2행 3행 주서행)

        public List<string> fieldDataTypeTable = new List<string>();
        public List<string> fieldDataTypeCSharp = new List<string>();
        public List<string> fieldNameList = new List<string>();
    }
}
