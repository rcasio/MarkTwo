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
        private Thread serverThread01; // 클라이언트 스레드01
        private Thread serverThread02; // 클라이언트 스레드02

        Dictionary<string, Thread> threads; // 쓰레드
        
        // 데이터 추출을 스레드에서 처리하기 위한 세분화 데이터
        private Dictionary<string, SheetData> multilingualData = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData01 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData02 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> serverData01 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> serverData02 = new Dictionary<string, SheetData>();

        private System.Timers.Timer timerConvertChecker = new System.Timers.Timer(200); //0.2초마다 한번씩 체크한다.

        private bool isExtractioneMultilingual = false;
        private bool isExtractionClient01 = false;
        private bool isExtractionClient02 = false;
        private bool isExtreactionServer01 = false;
        private bool isExtreactionServer02 = false;

        private Action NextAction; // 스레드 다음 액션

        //private void StartConvertClient() { this.StartConversion(SheetType.Client); }

        public DataExtraction(DataManager dataManager, Action<RichTextBox, string> SetRichText, Action<ProgressBar, int> SetProgressBar, Action NextAction)
        {
            this.dataManager = dataManager;
            this.excelData = dataManager.excelData;
            this.NextAction = NextAction;

            // 데이터를 할당한다.
            this.multilingualData = this.excelData.multilingualSheetDatas;
            this.clientData01 = this.excelData.client01SheetDatas;
            this.clientData02 = this.excelData.client02SheetDatas;
            this.serverData01 = this.excelData.server01SheetDats;
            this.serverData02 = this.excelData.server02SheetDats;

            // TODO : 툴에 스레드 진행상황 표시

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
                    Console.WriteLine("다국어 완료");

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
                    Console.WriteLine("클라이언트 스레드01 완료");

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
                    Console.WriteLine("클라이언트 스레드02 완료");

                    this.isExtractionClient02 = true;
                }
            ));
            this.clientThread02.Start();

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
                    Console.WriteLine("서버 스레드01 완료");

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
                    Console.WriteLine("서버 스레드02 완료");

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
                this.isExtreactionServer01 &&
                this.isExtreactionServer02)
            {
                Console.WriteLine("추출 작업 완료");

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
            this.serverThread01.Abort();
            this.serverThread02.Abort();

            // 스레드 체커 종료
            this.timerConvertChecker.Close();
        }
    }
}
