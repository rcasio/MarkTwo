using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

using static MarkTwo.TagManager;

namespace MarkTwo
{
    public class DataExtraction : IThread
    {
        private DataManager dataManager;
        private ExcelData excelData;

        private Thread multilingualThread01; // 다국에 스레드01
        private Thread multilingualThread02; // 다국에 스레드02
        private Thread clientThread01; // 클라이언트 스레드01
        private Thread clientThread02; // 클라이언트 스레드02

        private Dictionary<string, SheetData> clientSheetDatas; // 클라이언트 시트 딕셔너리
        private Dictionary<string, SheetData> serverSheetDats; // 서버 시트 딕셔너리

        // 데이터 추출을 스레드에서 처리하기 위한 세분화 데이터
        private Dictionary<string, FieldData> multilingualData01 = new Dictionary<string, FieldData>();
        private Dictionary<string, FieldData> multilingualData02 = new Dictionary<string, FieldData>();
        private Dictionary<string, SheetData> clientData01 = new Dictionary<string, SheetData>();
        private Dictionary<string, SheetData> clientData02 = new Dictionary<string, SheetData>();

        private System.Timers.Timer timerConvertChecker = new System.Timers.Timer(200); //0.2초마다 한번씩 체크한다.

        private bool isExtractioneMultilingual = false;
        private bool isExtractionClient = false;

        //private void StartConvertClient() { this.StartConversion(SheetType.Client); }

        public DataExtraction(DataManager dataManager)
        {
            this.dataManager = dataManager;
            this.excelData = dataManager.excelData;

            this.clientSheetDatas = excelData.clientSheetDatas;
            this.serverSheetDats = excelData.serverSheetDats;

            // TODO : 다국어 테이블을 필드별로 분류하여 스레드로 데이터를 추출한다
            // TODO : 다국어 제외 테이블을 시트별로 데이터를 추출한다.

            //this.clientData01

            //for (int i = 0; i < this.clientSheetDatas.Count; i++)
            //{
            //    if (this.clientSheetDatas[i].name.Equals(SheetName.Multilingual)) // 다국 시트 정보일 경우
            //    {
            //        if (i % 2 == 0)
            //        {
            //            this.multilingualData01.Add()
            //        }
            //        else
            //        {

            //        }
            //    }
            //}


            // 다국어 쓰레드
            multilingualThread01 = new Thread(new ThreadStart(
                () =>
                {
                    this.excelData.clientSheetDatas[SheetName.Multilingual].Create();
                    Console.WriteLine("다국어 완료");

                    this.isExtractioneMultilingual = true;
                }
            ));
            multilingualThread01.Start();

            // 다국어를 제외한 클라이언트 쓰레드
            clientThread01 = new Thread(new ThreadStart(
                () =>
                {
                    foreach (var item in clientSheetDatas.Keys)
                    {
                        if (!item.Equals(SheetName.Multilingual))
                        {
                            clientSheetDatas[item].Create();
                        }
                    }

                    Console.WriteLine("클라이언트 완료");
                    this.isExtractionClient = true;
                }
                ));
            clientThread01.Start();

            // 변환작업 쓰레드 종료를 체크하는 타이머를 구동한다.
            timerConvertChecker.Elapsed += new System.Timers.ElapsedEventHandler(CheckThrea1ds); // 이벤트를 등록한다.
            timerConvertChecker.Start();
        }

        public void CheckThrea1ds(object soruce, ElapsedEventArgs e) // 스레드를 체크한다.
        {
            // 추출 작업 체크
            if (this.isExtractioneMultilingual && this.isExtractionClient)
            {
                Console.WriteLine("추출 작업 완료");
                this.CloseThreads(); // 스레드를 닫는다.

                this.excelData.CloseExcel(); // 엑셀을 닫는다.
            }
        }

        public void CloseThreads() // 스레드를 닫는다.
        {
            // 스레드 종료
            this.multilingualThread01.Abort();
            this.clientThread01.Abort();

            // 스레드 체커 종료
            this.timerConvertChecker.Close();
        }
    }
}
