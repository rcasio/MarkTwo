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
using System.Diagnostics;

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

        Stopwatch sw = new Stopwatch(); // 시간체크
        
        DataManager dataManager;  // 데이터를 관리한다.(오류 등)
        GenerateCSharpCode generateCSharpCode; //c# 코드를 생성한다.
        
        SoundPlayer endFileConvert = new SoundPlayer(Properties.Resources._416631__alpersez__acoustic_guitar); // 변환이 종료되었을 때 나오는 사운드
        
        // 레디스 접속
        bool IsConnectServer = true; // 서버에 접속할 것인가? (엑셀 테이블에서 IP주소를 입력하지 않으면 false가 된다.)
        string RedisConnect_Ip;
        int RedisConnect_Port;
        string RedisConnect_PassWord = "passWord";
        static RedisConnection conn;

        string m_Reids_Num; // 레디스에 Num을 할당하기 위해 사용되는 변수
        
        public ConverterWindow()
        {
            InitializeComponent(); // 컴포넌트를 초기화 한다.

            this.SetProgressbar(); // 프로그래스바를 설정한다.

            this.FormClosed += this.ClostMarkTwo; // 죵료 시 실행되는 콜백함수 설정
            AppDomain.CurrentDomain.ProcessExit += (s, e) => this.CloseExcel();
        }
        #region 리펙토링 부분

        private void InitializeForm() // 폼을 초기화 한다.
        {
            TimerLabel.Text = "0";
            //Excel_Directory.Text = "엑셀 경로 : " + dataManager.ExcelFilePath(); // 엑셀 경로 표시
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

        // 폼이 시작되고 실행된다.
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.InitializeForm(); // 폼을 초기화 한다.

            TimerObj.Start();
            TimerObj.Interval = 10;

            sw.Start();

            this.dataManager = new DataManager(this);

            this.dataManager.converterWindow = this;

            // 엑셀 데이터를 생성한다.
            this.dataManager.CreateExcelData((p) => this.SetFormDataRule(p),
                                             (p) => this.SetExtreactionProgressBar(p),
                                             (p, a) => this.SetRichText(p, a),
                                             (p, a) => this.SetProgressBar(p, a),
                                             () => this.NextAction());
        }

        /// <summary>
        /// 파일 추출 스레드가 종료된 후 작업
        /// </summary>
        public void NextAction()
        {
            this.generateCSharpCode = new GenerateCSharpCode(this.dataManager); // c# 파일을 생성한다

            this.Invoke(new Action(() => this.SetTotal()));

            this.CompleateForm();
        }

       public void SetTotal()
        {
            this.ClientDataCount.Text = this.dataManager.excelData.TotalClientData.ToString();
            this.ServerDataCount.Text = this.dataManager.excelData.TotalServerData.ToString();
        }

        /// <summary>
        /// 추출 프로그래스 바 진행을 나타낸다.
        /// </summary>
        /// <param name="progress"></param>
        public void SetExtreactionProgressBar(int progress)
        {
            this.ExtreactionReadyProgressBar.Value = progress;
        }
        
        // 리치 텍스트 박스 설정
        public void SetRichText(RichTextBox richTextBox, string text)
        {
            if (this.dataManager.dataRule != null)
            {
                if (this.dataManager.dataRule.isSpeedUp) return; // 고속모드일 경우 텍스트를 추출하지 않는다.
            }

            if (richTextBox.InvokeRequired)
            {
                Action<RichTextBox, string> richTextCallBack = new Action<RichTextBox, string>(SetRichText);
                Invoke(richTextCallBack, new object[] { richTextBox, text });
            }
            else
            {
                richTextBox.AppendText(text + "\n");
                richTextBox.ScrollToCaret();
            }
        }
        
        // 프로그래스바 설정
        public void SetProgressBar(ProgressBar progressBar, int value)
        {
            if (progressBar.InvokeRequired)
            {
                Action<ProgressBar, int> progressBarCallBack = new Action<ProgressBar, int>(SetProgressBar);
                Invoke(progressBarCallBack, new object[] { progressBar, value });
            }
            else
            {
                progressBar.Value = value;
            }
        }

        // 테이블 이름 라벨 설정
        public void SetTableLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                Action<Label, string> labelCallBack = new Action<Label, string>(SetTableLabel);
                Invoke(labelCallBack, new object[] { label, text });
            }
            else
            {
                label.Text = "테이블 이름 [" + text + "]";
            }
        }

        //  프로그래스바를 설정한다.
        private void SetProgressbar()
        {
            ExtreactionReadyProgressBar.Style = ProgressBarStyle.Continuous;
            ExtreactionReadyProgressBar.Minimum = 0;
            ExtreactionReadyProgressBar.Maximum = 100;
            ExtreactionReadyProgressBar.Value = 0;

            MultiligualThreadLabelProgressBar.Style = ProgressBarStyle.Continuous;
            MultiligualThreadLabelProgressBar.Minimum = 0;
            MultiligualThreadLabelProgressBar.Maximum = 1000;
            MultiligualThreadLabelProgressBar.Value = 0;

            ClientThread01progressBar.Style = ProgressBarStyle.Continuous;
            ClientThread01progressBar.Minimum = 0;
            ClientThread01progressBar.Maximum = 1000;
            ClientThread01progressBar.Value = 0;

            ClientThread02progressBar.Style = ProgressBarStyle.Continuous;
            ClientThread02progressBar.Minimum = 0;
            ClientThread02progressBar.Maximum = 1000;
            ClientThread02progressBar.Value = 0;

            ServerThread01ProgressBar.Style = ProgressBarStyle.Continuous;
            ServerThread01ProgressBar.Minimum = 0;
            ServerThread01ProgressBar.Maximum = 1000;
            ServerThread01ProgressBar.Value = 0;

            ServerThread02ProgressBar.Style = ProgressBarStyle.Continuous;
            ServerThread02ProgressBar.Minimum = 0;
            ServerThread02ProgressBar.Maximum = 1000;
            ServerThread02ProgressBar.Value = 0;
        }
        
        public void CompleateForm() // 종료 
        {
            endFileConvert.Play();

            sw.Stop();

            MessageBox.Show("변환이 완료되었습니다.");

            this.Invoke(new Action(() => Close()));
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
        
        // 버전을 표시 합니다.
        void DisplayVersion()
        {
            // 버전 표시
            this.Text = "MarkTwo v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
        
        // 시작 버튼을 눌렀을 시
        private void StartButton_Click(object sender, EventArgs e)
        {
            //this.Start_Convert();
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
        
        // 프로그램이 닫힐 때 실행된다.
        private void ClostMarkTwo(object sender, FormClosedEventArgs e)
        {
            switch (e.CloseReason)
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

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerLabel.Text = sw.ElapsedMilliseconds.ToString("#,###ms");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }    
}
