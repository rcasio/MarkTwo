using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;

using static MarkTwo.TagManager;

namespace MarkTwo
{
    public class DataExtraction : IThread
    {
        private DataManager dataManager;
        private ExcelData excelData;

        private Thread multilingualThread; // 다국에 스레드01
        private Thread clientThread01; // 클라이언트 스레드01
        private Thread clientThread02; // 클라이언트 스레드02
        private Thread clientThread03; // 클라이언트 스레드03
        private Thread clientThread04; // 클라이언트 스레드04
        private Thread clientThread05; // 클라이언트 스레드05
        private Thread clientThread06; // 클라이언트 스레드06
        private Thread clientThread07; // 클라이언트 스레드07
        private Thread clientThread08; // 클라이언트 스레드08
        private Thread clientThread09; // 클라이언트 스레드09
        private Thread clientThread10; // 클라이언트 스레드10

        private Thread serverThread01; // 클라이언트 스레드01
        private Thread serverThread02; // 클라이언트 스레드02

        Dictionary<string, Thread> threads; // 쓰레드
        
        // 데이터 추출을 스레드에서 처리하기 위한 세분화 데이터
        private Dictionary<string, SheetData> multilingualData = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData01 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData02 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData03 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData04 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData05 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData06 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData07 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData08 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData09 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData10 = new Dictionary<string, SheetData>();

        private Dictionary<string, SheetData> serverData01 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> serverData02 = new Dictionary<string, SheetData>();

        private System.Timers.Timer timerConvertChecker = new System.Timers.Timer(200); //0.2초마다 한번씩 체크한다.

        private bool isExtractioneMultilingual = false;
        private bool isExtractionClient01 = false;
        private bool isExtractionClient02 = false;
        private bool isExtractionClient03 = false;
        private bool isExtractionClient04 = false;
        private bool isExtractionClient05 = false;
        private bool isExtractionClient06 = false;
        private bool isExtractionClient07 = false;
        private bool isExtractionClient08 = false;
        private bool isExtractionClient09 = false;
        private bool isExtractionClient10 = false;

        private bool isExtreactionServer01 = false;
        private bool isExtreactionServer02 = false;

        private Action NextAction; // 스레드 다음 액션
        
        public DataExtraction(DataManager dataManager, Action<RichTextBox, string> SetRichText, Action<ProgressBar, int> SetProgressBar, Action NextAction)
        {
            this.dataManager = dataManager;
            this.excelData = dataManager.excelData;
            this.NextAction = NextAction;

            // 데이터를 할당한다.
            this.multilingualData = this.excelData.multilingualSheetDatas;
            this.clientData01 = this.excelData.client01SheetDatas;
            this.clientData02 = this.excelData.client02SheetDatas;
            this.clientData03 = this.excelData.client03SheetDatas;
            this.clientData04 = this.excelData.client04SheetDatas;
            this.clientData05 = this.excelData.client05SheetDatas;
            this.clientData06 = this.excelData.client06SheetDatas;
            this.clientData07 = this.excelData.client07SheetDatas;
            this.clientData08 = this.excelData.client08SheetDatas;
            this.clientData09 = this.excelData.client09SheetDatas;
            this.clientData10 = this.excelData.client10SheetDatas;
            this.serverData01 = this.excelData.server01SheetDats;
            this.serverData02 = this.excelData.server02SheetDats;
            
            // 다국어 스레드
            this.multilingualThread = new Thread(new ThreadStart(
                () =>
                {
                    this.multilingualData[SheetName.Multilingual].Create(SetRichText, 
                                                                        this.dataManager.converterWindow.MultiligualThreadText,
                                                                        (p,s) => this.dataManager.converterWindow.SetProgressBar(p,s),
                                                                        this.dataManager.converterWindow.MultiligualThreadLabelProgressBar,
                                                                        (p,s) => this.dataManager.converterWindow.SetTableLabel(p,s),
                                                                        this.dataManager.converterWindow.MultilingualThreadTableLabel);

                    this.isExtractioneMultilingual = true;
                }
            ));
            this.multilingualThread.Start();

            // 클라이언트 스레드01
            this.clientThread01 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData01.Keys)
                    {
                        this.clientData01[key].Create(SetRichText, 
                                                      this.dataManager.converterWindow.ClientThread01Text,
                                                      (p,s) => this.dataManager.converterWindow.SetProgressBar(p,s),
                                                      this.dataManager.converterWindow.ClientThread01progressBar,
                                                      (p,s) => this.dataManager.converterWindow.SetTableLabel(p,s),
                                                      this.dataManager.converterWindow.ClientThread01TableLabel);
                    }

                    this.isExtractionClient01 = true;
                }
            ));
            this.clientThread01.Start();

            // 클라이언트 스레드02
            this.clientThread02 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData02.Keys)
                    {
                        this.clientData02[key].Create(SetRichText, 
                                                      this.dataManager.converterWindow.ClientThread02Text,
                                                      (p,s) => this.dataManager.converterWindow.SetProgressBar(p,s),
                                                      this.dataManager.converterWindow.ClientThread02progressBar,
                                                      (p,s) => this.dataManager.converterWindow.SetTableLabel(p,s),
                                                      this.dataManager.converterWindow.ClientThread02TableLabel);
                    }

                    this.isExtractionClient02 = true;
                }
            ));
            this.clientThread02.Start();

            // 클라이언트 스레드03
            this.clientThread03 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData03.Keys)
                    {
                        // TODO : ConvertWindow.cs에서 이름을 변경한 뒤 변경된 이름 기준으로 작업할 것
                        this.clientData03[key].Create(SetRichText,
                                                      this.dataManager.converterWindow.ClientThread03Text,
                                                      (p, s) => this.dataManager.converterWindow.SetProgressBar(p, s),
                                                      this.dataManager.converterWindow.ClientThread03progressBar,
                                                      (p, s) => this.dataManager.converterWindow.SetTableLabel(p, s),
                                                      this.dataManager.converterWindow.ClientThread03TableLabel);
                    }

                    this.isExtractionClient03 = true;
                }
            ));
            this.clientThread03.Start();

            // 클라이언트 스레드04
            this.clientThread04 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData04.Keys)
                    {
                        // TODO : ConvertWindow.cs에서 이름을 변경한 뒤 변경된 이름 기준으로 작업할 것
                        this.clientData04[key].Create(SetRichText,
                                                      this.dataManager.converterWindow.ClientThread04Text,
                                                      (p, s) => this.dataManager.converterWindow.SetProgressBar(p, s),
                                                      this.dataManager.converterWindow.ClientThread04progressBar,
                                                      (p, s) => this.dataManager.converterWindow.SetTableLabel(p, s),
                                                      this.dataManager.converterWindow.ClientThread04TableLabel);
                    }

                    this.isExtractionClient04 = true;
                }
            ));
            this.clientThread04.Start();

            // 클라이언트 스레드05
            this.clientThread05 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData05.Keys)
                    {
                        // TODO : ConvertWindow.cs에서 이름을 변경한 뒤 변경된 이름 기준으로 작업할 것
                        this.clientData05[key].Create(SetRichText,
                                                      this.dataManager.converterWindow.ClientThread05Text,
                                                      (p, s) => this.dataManager.converterWindow.SetProgressBar(p, s),
                                                      this.dataManager.converterWindow.ClientThread05progressBar,
                                                      (p, s) => this.dataManager.converterWindow.SetTableLabel(p, s),
                                                      this.dataManager.converterWindow.ClientThread05TableLabel);
                    }

                    this.isExtractionClient05 = true;
                }
            ));
            this.clientThread05.Start();

            // 클라이언트 스레드06
            this.clientThread06 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData06.Keys)
                    {
                        // TODO : ConvertWindow.cs에서 이름을 변경한 뒤 변경된 이름 기준으로 작업할 것
                        this.clientData06[key].Create(SetRichText,
                                                      this.dataManager.converterWindow.ClientThread06Text,
                                                      (p, s) => this.dataManager.converterWindow.SetProgressBar(p, s),
                                                      this.dataManager.converterWindow.ClientThread06progressBar,
                                                      (p, s) => this.dataManager.converterWindow.SetTableLabel(p, s),
                                                      this.dataManager.converterWindow.ClientThread06TableLabel);
                    }

                    this.isExtractionClient06 = true;
                }
            ));
            this.clientThread06.Start();

            // 클라이언트 스레드07
            this.clientThread07 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData07.Keys)
                    {
                        // TODO : ConvertWindow.cs에서 이름을 변경한 뒤 변경된 이름 기준으로 작업할 것
                        this.clientData07[key].Create(SetRichText,
                                                      this.dataManager.converterWindow.ClientThread07Text,
                                                      (p, s) => this.dataManager.converterWindow.SetProgressBar(p, s),
                                                      this.dataManager.converterWindow.ClientThread07progressBar,
                                                      (p, s) => this.dataManager.converterWindow.SetTableLabel(p, s),
                                                      this.dataManager.converterWindow.ClientThread07TableLabel);
                    }

                    this.isExtractionClient07 = true;
                }
            ));
            this.clientThread07.Start();

            // 클라이언트 스레드08
            this.clientThread08 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData08.Keys)
                    {
                        // TODO : ConvertWindow.cs에서 이름을 변경한 뒤 변경된 이름 기준으로 작업할 것
                        this.clientData08[key].Create(SetRichText,
                                                      this.dataManager.converterWindow.ClientThread08Text,
                                                      (p, s) => this.dataManager.converterWindow.SetProgressBar(p, s),
                                                      this.dataManager.converterWindow.ClientThread08progressBar,
                                                      (p, s) => this.dataManager.converterWindow.SetTableLabel(p, s),
                                                      this.dataManager.converterWindow.ClientThread08TableLabel);
                    }

                    this.isExtractionClient08 = true;
                }
            ));
            this.clientThread08.Start();

            // 클라이언트 스레드09
            this.clientThread09 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData09.Keys)
                    {
                        // TODO : ConvertWindow.cs에서 이름을 변경한 뒤 변경된 이름 기준으로 작업할 것
                        this.clientData09[key].Create(SetRichText,
                                                      this.dataManager.converterWindow.ClientThread09Text,
                                                      (p, s) => this.dataManager.converterWindow.SetProgressBar(p, s),
                                                      this.dataManager.converterWindow.ClientThread09progressBar,
                                                      (p, s) => this.dataManager.converterWindow.SetTableLabel(p, s),
                                                      this.dataManager.converterWindow.ClientThread09TableLabel);
                    }

                    this.isExtractionClient09 = true;
                }
            ));
            this.clientThread09.Start();

            // 클라이언트 스레드10
            this.clientThread10 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.clientData10.Keys)
                    {
                        // TODO : ConvertWindow.cs에서 이름을 변경한 뒤 변경된 이름 기준으로 작업할 것
                        this.clientData10[key].Create(SetRichText,
                                                      this.dataManager.converterWindow.ClientThread10Text,
                                                      (p, s) => this.dataManager.converterWindow.SetProgressBar(p, s),
                                                      this.dataManager.converterWindow.ClientThread10progressBar,
                                                      (p, s) => this.dataManager.converterWindow.SetTableLabel(p, s),
                                                      this.dataManager.converterWindow.ClientThread10TableLabel);
                    }

                    this.isExtractionClient10 = true;
                }
            ));
            this.clientThread10.Start();

            // 서버 스레드01
            this.serverThread01 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.serverData01.Keys)
                    {
                        this.serverData01[key].Create(SetRichText, 
                                                      this.dataManager.converterWindow.ServerThread01Text,
                                                      (p,s) => this.dataManager.converterWindow.SetProgressBar(p,s),
                                                      this.dataManager.converterWindow.ServerThread01ProgressBar,
                                                      (p,s) => this.dataManager.converterWindow.SetTableLabel(p,s),
                                                      this.dataManager.converterWindow.ServerThread01TableLabel);
                    }

                    this.isExtreactionServer01 = true;
                }
            ));
            this.serverThread01.Start();

            // 서버 스레드02
            this.serverThread02 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var key in this.serverData02.Keys)
                    {
                        this.serverData02[key].Create(SetRichText, 
                                                      this.dataManager.converterWindow.ServerThread02Text,
                                                      (p,s) => this.dataManager.converterWindow.SetProgressBar(p,s),
                                                      this.dataManager.converterWindow.ServerThread02ProgressBar,
                                                      (p,s) => this.dataManager.converterWindow.SetTableLabel(p,s),
                                                      this.dataManager.converterWindow.ServerThread02TableLabel);
                    }

                    this.isExtreactionServer02 = true;
                }
            ));
            this.serverThread02.Start();
            
            // 변환작업 쓰레드 종료를 체크하는 타이머를 구동한다.
            timerConvertChecker.Elapsed += new System.Timers.ElapsedEventHandler(CheckThreads); // 이벤트를 등록한다.
            timerConvertChecker.Start();
        }

        public void CheckThreads(object soruce, ElapsedEventArgs e) // 스레드를 체크한다.
        {
            // 추출 작업 체크
            if (this.isExtractioneMultilingual && 
                this.isExtractionClient01 &&
                this.isExtractionClient02 &&
                this.isExtractionClient03 &&
                this.isExtractionClient04 &&
                this.isExtractionClient05 &&
                this.isExtractionClient06 &&
                this.isExtractionClient07 &&
                this.isExtractionClient08 &&
                this.isExtractionClient09 &&
                this.isExtractionClient10 &&
                this.isExtreactionServer01 &&
                this.isExtreactionServer02)
            {

                this.CloseThreads(); // 스레드를 닫는다.

                this.excelData.SetTotlaDictionary(); // totalClientSheetDatas, totalServerSheetDatas을 설정 한다.
                this.excelData.CloseExcel(); // 엑셀을 닫는다.

                this.NextAction(); // 다음 액션을 한다.
            }
        }

        public void CloseThreads() // 스레드를 닫는다.
        {
            // 스레드 종료
            this.multilingualThread.Abort();
            this.clientThread01.Abort();
            this.clientThread02.Abort();
            this.clientThread03.Abort();
            this.clientThread04.Abort();
            this.serverThread01.Abort();
            this.serverThread02.Abort();

            // 스레드 체커 종료
            this.timerConvertChecker.Close();
        }
    }
}
