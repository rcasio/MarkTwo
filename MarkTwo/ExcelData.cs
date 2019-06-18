using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

using static MarkTwo.TagManager;

namespace MarkTwo
{
    public class ExcelData
    {
        private DataManager dataManager;
        private DataRule dataRule;
        private DataTableList dataTableList;

        private int totalClientData; // 클라이언트 데이터 카운트
        public int TotalClientData {
            get
            {
                foreach (KeyValuePair<string,SheetData> sheet in this.totalClientSheetDatas)
                {
                    this.totalClientData += sheet.Value.totalDataCount;
                }
                return this.totalClientData;
            }
        }
        private int totalServerData; // 서버 데이터 카운트
        public int TotalServerData {
            get
            {
                foreach (KeyValuePair<string,SheetData> sheet in this.totalServerSheetDatas)
                {
                    this.totalServerData += sheet.Value.totalDataCount;
                }
                return this.totalServerData;
            }
        }

        public Dictionary<string, SheetData> multilingualSheetDatas = new Dictionary<string, SheetData>(); // 다국어 테이블
        
        public Dictionary<string, SheetData> client01SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client02SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client03SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client04SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client05SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client06SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client07SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client08SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client09SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터
        public Dictionary<string, SheetData> client10SheetDatas = new Dictionary<string, SheetData>();  // 클라이언트 시트 데이터

        public Dictionary<string, SheetData> server01SheetDats  = new Dictionary<string, SheetData>();  // 서버 시트 데이터
        public Dictionary<string, SheetData> server02SheetDats  = new Dictionary<string, SheetData>();  // 서버 시트 데이터

        public Dictionary<string, SheetData> totalClientSheetDatas = new Dictionary<string, SheetData>(); // 클라이언트 최종 시트 데이터
        public Dictionary<string, SheetData> totalServerSheetDatas = new Dictionary<string, SheetData>(); // 서버 최종 시트 데이터

        public ExcelData(DataManager dataManager, Action<int> SetExtreactionProgressBar, Action<RichTextBox, string> SetRichBox)
        {
            this.dataManager = dataManager;
            this.dataRule = dataManager.dataRule;
            this.dataTableList = dataManager.dataTableList;

            RichTextBox rb = dataManager.converterWindow.ExtreactionReadyText;
            
            SetRichBox(rb, "====== 엑셀 기반 데이터 추출 \n엑셀 데이터 추출을 위해 기반 데이터 추출을 시작합니다.");

            SetRichBox(rb, "");
            SetRichBox(rb, "필수 데이터 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.multilingual.Count; i++)
            {
                string sheetName = this.dataTableList.multilingual[i];
                SetRichBox(rb, "- 필수 데이터 기반 정보 추출 테이블 : " + sheetName);

                multilingualSheetDatas.Add(sheetName, new SheetData(this.dataTableList.necessarySheetList[i], 
                                                                    sheetName, 
                                                                    this.dataManager, 
                                                                    this.dataRule, 
                                                                    SetRichBox, 
                                                                    this.dataManager.converterWindow.ExtreactionReadyText,
                                                                    true,
                                                                    SheetType.Multilingual));

                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }
            
            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터01 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList01.Count; i++)
            {
                string sheetName = this.dataTableList.clientList01[i];
                SetRichBox(rb, "- 클라이언트 데이터01 기반 정보 추출 테이블 : " + sheetName);

                client01SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList01[i], 
                                                                sheetName, 
                                                                this.dataManager, 
                                                                this.dataRule, 
                                                                SetRichBox, 
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));

                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }
            
            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터02 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList02.Count; i++)
            {
                string sheetName = this.dataTableList.clientList02[i];
                SetRichBox(rb, "- 클라이언트 데이터02 기반 정보 추출 테이블 : " + sheetName);

                client02SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList02[i], 
                                                                sheetName, 
                                                                this.dataManager, 
                                                                this.dataRule, 
                                                                SetRichBox, 
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터03 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList03.Count; i++)
            {
                string sheetName = this.dataTableList.clientList03[i];
                SetRichBox(rb, "- 클라이언트 데이터03 기반 정보 추출 테이블 : " + sheetName);

                client03SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList03[i],
                                                                sheetName,
                                                                this.dataManager,
                                                                this.dataRule,
                                                                SetRichBox,
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터04 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList04.Count; i++)
            {
                string sheetName = this.dataTableList.clientList04[i];
                SetRichBox(rb, "- 클라이언트 데이터04 기반 정보 추출 테이블 : " + sheetName);

                client04SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList04[i],
                                                                sheetName,
                                                                this.dataManager,
                                                                this.dataRule,
                                                                SetRichBox,
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터05 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList05.Count; i++)
            {
                string sheetName = this.dataTableList.clientList05[i];
                SetRichBox(rb, "- 클라이언트 데이터05 기반 정보 추출 테이블 : " + sheetName);

                client05SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList05[i],
                                                                sheetName,
                                                                this.dataManager,
                                                                this.dataRule,
                                                                SetRichBox,
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터06 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList06.Count; i++)
            {
                string sheetName = this.dataTableList.clientList06[i];
                SetRichBox(rb, "- 클라이언트 데이터06 기반 정보 추출 테이블 : " + sheetName);

                client06SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList06[i],
                                                                sheetName,
                                                                this.dataManager,
                                                                this.dataRule,
                                                                SetRichBox,
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터07 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList07.Count; i++)
            {
                string sheetName = this.dataTableList.clientList07[i];
                SetRichBox(rb, "- 클라이언트 데이터07 기반 정보 추출 테이블 : " + sheetName);

                client07SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList07[i],
                                                                sheetName,
                                                                this.dataManager,
                                                                this.dataRule,
                                                                SetRichBox,
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터08 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList08.Count; i++)
            {
                string sheetName = this.dataTableList.clientList08[i];
                SetRichBox(rb, "- 클라이언트 데이터08 기반 정보 추출 테이블 : " + sheetName);

                client08SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList08[i],
                                                                sheetName,
                                                                this.dataManager,
                                                                this.dataRule,
                                                                SetRichBox,
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터09 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList09.Count; i++)
            {
                string sheetName = this.dataTableList.clientList09[i];
                SetRichBox(rb, "- 클라이언트 데이터09 기반 정보 추출 테이블 : " + sheetName);

                client09SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList09[i],
                                                                sheetName,
                                                                this.dataManager,
                                                                this.dataRule,
                                                                SetRichBox,
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetRichBox(rb, "");
            SetRichBox(rb, "클라이언트 데이터10 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.clientList10.Count; i++)
            {
                string sheetName = this.dataTableList.clientList10[i];
                SetRichBox(rb, "- 클라이언트 데이터10 기반 정보 추출 테이블 : " + sheetName);

                client10SheetDatas.Add(sheetName, new SheetData(this.dataTableList.clientsSheetList10[i],
                                                                sheetName,
                                                                this.dataManager,
                                                                this.dataRule,
                                                                SetRichBox,
                                                                this.dataManager.converterWindow.ExtreactionReadyText,
                                                                true,
                                                                SheetType.Client));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetRichBox(rb, "");
            SetRichBox(rb, "서버 데이터01 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.serverList01.Count; i++)
            {
                string sheetName = this.dataTableList.serverList01[i];
                SetRichBox(rb, "- 서버 데이터01 기반 정보 추출 테이블 : " + sheetName);

                server01SheetDats.Add(sheetName, new SheetData(this.dataTableList.serverSheetList01[i], 
                                                               sheetName, 
                                                               this.dataManager, 
                                                               this.dataRule, 
                                                               SetRichBox, 
                                                               this.dataManager.converterWindow.ExtreactionReadyText,
                                                               true,
                                                               SheetType.Server));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }
            
            SetRichBox(rb, "");
            SetRichBox(rb, "서버 데이터02 기반 정보 추출 시작");

            for (int i = 0; i < this.dataTableList.serverList02.Count; i++)
            {
                string sheetName = this.dataTableList.serverList02[i];
                SetRichBox(rb, "- 서버 데이터02 기반 정보 추출 테이블 : " + sheetName);

                server02SheetDats.Add(sheetName, new SheetData(this.dataTableList.serverSheetList02[i], 
                                                               sheetName, 
                                                               this.dataManager, 
                                                               this.dataRule, 
                                                               SetRichBox, 
                                                               this.dataManager.converterWindow.ExtreactionReadyText,
                                                               true,
                                                               SheetType.Server));
                SetRichBox(rb, "- 추출 완료 : " + sheetName);
            }

            SetExtreactionProgressBar(100);
            SetRichBox(rb, "====== 완료");
        }

        /// <summary>
        /// totalClientSheetDatas, totalServerSheetDatas을 설정합니다.
        /// </summary>
        public void SetTotlaDictionary()
        {
            IEnumerator<KeyValuePair<string, SheetData>> e;

            // totalClientSheetDatas 설정
            e = this.multilingualSheetDatas.GetEnumerator();
            while (e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client01SheetDatas.GetEnumerator();
            while(e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client02SheetDatas.GetEnumerator();
            while(e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client03SheetDatas.GetEnumerator();
            while (e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client04SheetDatas.GetEnumerator();
            while (e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client05SheetDatas.GetEnumerator();
            while (e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client06SheetDatas.GetEnumerator();
            while (e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client07SheetDatas.GetEnumerator();
            while (e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client08SheetDatas.GetEnumerator();
            while (e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client09SheetDatas.GetEnumerator();
            while (e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.client10SheetDatas.GetEnumerator();
            while (e.MoveNext()) this.totalClientSheetDatas.Add(e.Current.Key, e.Current.Value);

            // totalServerSheetDatas 설정
            e = this.server01SheetDats.GetEnumerator();
            while (e.MoveNext()) this.totalServerSheetDatas.Add(e.Current.Key, e.Current.Value);

            e = this.server02SheetDats.GetEnumerator();
            while (e.MoveNext()) this.totalServerSheetDatas.Add(e.Current.Key, e.Current.Value);
        }

        // 엑셀을 닫는다.
        public void CloseExcel()
        {
            // TODO : 엑셀이 닫히지 않는 부분 수정할 것
            this.dataManager.excelApp.DisplayAlerts = false;
            this.dataManager.workBook.Close(0);
            this.dataManager.excelApp.Quit();
        }
    }
}
