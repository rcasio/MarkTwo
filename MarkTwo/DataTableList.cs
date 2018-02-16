using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

using static MarkTwo.TagManager;

namespace MarkTwo
{
    // 사용할 테이블 리스트
    public class DataTableList
    {
        public Excel.Worksheet tableManagerSheet; // 테이블 관리 시트

        public List<string> totalList = new List<string>(); // 스레드 프로그래스 진행을 위해 사용된다.

        public List<string> necessaryList = new List<string>(); // 픽수 테이블
        public List<Excel.Worksheet> necessarySheetList = new List<Excel.Worksheet>(); // 픽수 테이블

        public List<string> clientList01 = new List<string>(); // 사용되는 테이블 리스트
        public List<Excel.Worksheet> clientsSheetList01 = new List<Excel.Worksheet>(); // 클라이언트 시트 리스트
        public List<string> clientList02 = new List<string>(); // 사용되는 테이블 리스트
        public List<Excel.Worksheet> clientsSheetList02 = new List<Excel.Worksheet>(); // 클라이언트 시트 리스트

        public List<string> serverList01 = new List<string>(); // 사용되는 클라이언트 리스트
        public List<Excel.Worksheet> serverSheetList01 = new List<Excel.Worksheet>(); // 서버 시트 리스트
        public List<string> serverList02 = new List<string>(); // 사용되는 클라이언트 리스트
        public List<Excel.Worksheet> serverSheetList02 = new List<Excel.Worksheet>(); // 서버 시트 리스트

        public DataTableList(Excel.Worksheet tableManagerSheet, Excel.Sheets sheets, Action<int> SetExtreactionProgressBar, Action<string, bool> SetProgressText)
        {
            SetProgressText("====== 테이블 리스트 설정 \n[테이블_관리] 시트에서 테이블 시트 리스트 추출을 시작합니다.", false);

            this.tableManagerSheet = tableManagerSheet as Excel.Worksheet;
            
            SetProgressText("", false);
            SetProgressText("시트 리스트 추가 작업 시작", false);

            SetProgressText("", false);
            SetProgressText("필수 테이블리스트 추가", false);
            // 필수 테이블리스트
            try
            {
                foreach (string sheetName in tableManagerSheet.get_Range("B8", "B50").Value)
                {
                    if (sheetName == null) break;

                    if (sheetName.Equals(SheetName.Multilingual))
                    {
                        this.necessaryList.Add(sheetName);
                        this.necessarySheetList.Add(sheets[sheetName] as Excel.Worksheet);
                    }
                    else if (sheetName.Equals(SheetName.PR))
                    {
                        necessaryList.Add(sheetName);
                        this.necessarySheetList.Add(sheets[sheetName] as Excel.Worksheet);
                    }
                    else if (sheetName.Equals(SheetName.Tag))
                    {
                        necessaryList.Add(sheetName);
                        this.necessarySheetList.Add(sheets[sheetName] as Excel.Worksheet);
                    }
                    else
                    {
                        MessageBox.Show("[테이블 규칙] 시트에서 [필수 테이블]이 Multilingual, PR, Tag 외의 다른 테이블이 들어가 있습니다.");
                        Environment.Exit(0);
                    }

                    totalList.Add(sheetName);
                    SetProgressText("- 필수 테이블 시트 이름 : " + sheetName, false);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("[테이블 규칙] 시트에서 [필수 테이블] 리스트에서 오류가 발생했습니다.");
                Environment.Exit(0);
            }

            SetExtreactionProgressBar(50);
            
            SetProgressText("", false);
            SetProgressText("클라이언트 스레드01 추가", false);
            // 클라이언트 스레드01 테이블리스트
            foreach (string sheetName in tableManagerSheet.get_Range("C8", "C50").Value)
            {
                if (sheetName == null) break;

                clientList01.Add(sheetName);
                try
                {
                    this.clientsSheetList01.Add(sheets[sheetName] as Excel.Worksheet);
                }
                catch (Exception)
                {
                    MessageBox.Show("[테이블 규칙] 시트에서 [클라이언트 스레드01]에 정의되지 않는 테이블이 들어가 있습니다. \n\n오류 시트 이름 :" + sheetName);
                    Environment.Exit(0);
                }

                totalList.Add(sheetName);
                Console.WriteLine("=== 클라이언트 스레드01 시트 이름 : {0}", sheetName);
                SetProgressText("- 클라이언트 스레드01 시트 이름 : " + sheetName, false);
            }

            SetExtreactionProgressBar(60);
            
            SetProgressText("", false);
            SetProgressText("클라이언트 스레드02 추가", false);
            // 클라이언트 스레드02 테이블리스트
            foreach (string sheetName in tableManagerSheet.get_Range("D8", "D50").Value)
            {
                if (sheetName == null) break;

                clientList02.Add(sheetName);
                try
                {
                    this.clientsSheetList02.Add(sheets[sheetName] as Excel.Worksheet);
                }
                catch (Exception)
                {
                    MessageBox.Show("[테이블 규칙] 시트에서 [클라이언트 스레드02]에 정의되지 않는 테이블이 들어가 있습니다. \n\n오류 시트 이름 :" + sheetName);
                    Environment.Exit(0);
                }

                totalList.Add(sheetName);
                SetProgressText("- 클라이언트 스레드02 시트 이름 : " + sheetName, false);
            }

            SetExtreactionProgressBar(70);
            
            SetProgressText("", false);
            SetProgressText("서버 스레드01 추가", false);
            // 서버 스레드01 테이블리스트
            foreach (string sheetName in tableManagerSheet.get_Range("E8", "E50").Value)
            {
                if (sheetName == null) break;

                serverList01.Add(sheetName);
                try
                {
                    this.serverSheetList01.Add(sheets[sheetName] as Excel.Worksheet);
                }
                catch (Exception)
                {
                    MessageBox.Show("[테이블 규칙] 시트에서 [서버 스레드01]에 정의되지 않는 테이블이 들어가 있습니다. \n\n오류 시트 이름 :" + sheetName);
                    Environment.Exit(0);
                }

                totalList.Add(sheetName);
                SetProgressText("- 서버 스레드01 시트 이름 : " + sheetName, false);
            }

            SetExtreactionProgressBar(80);
            
            SetProgressText("", false);
            SetProgressText("서버 스레드02 추가", false);
            // 서버 스레드01 테이블리스트
            foreach (string sheetName in tableManagerSheet.get_Range("F8", "F50").Value)
            {
                if (sheetName == null) break;

                serverList02.Add(sheetName);
                try
                {
                    this.serverSheetList02.Add(sheets[sheetName] as Excel.Worksheet);
                }
                catch (Exception)
                {
                    MessageBox.Show("[테이블 규칙] 시트에서 [서버 스레드01]에 정의되지 않는 테이블이 들어가 있습니다. \n\n오류 시트 이름 :" + sheetName);
                    Environment.Exit(0);
                }

                totalList.Add(sheetName);
                SetProgressText("- 서버 스레드02 시트 이름 : " + sheetName, false);
            }

            SetExtreactionProgressBar(90);
            SetProgressText("====== 완료", true);
        }
    }
}
