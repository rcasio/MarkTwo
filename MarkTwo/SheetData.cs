using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace MarkTwo
{
    public class SheetData 
    {
        public string name; // 시트 이름
        public Excel.Worksheet workSheet; // 워크 시트

        public DataRule dataRule; // 데이터 룰

        public int totalRowCount; // 테이블 행 숫자
        public int totalRowCountDeleteComment; // 테이블의
        public int totalColumnCount; // 테이블의 모든 열 숫자
        public int totalDataCount; // 모든 데이터 개수

        public List<int> fieldCommentList = new List<int>(); // 필드의 주석을 저장한다(필드 번호 순 ex) {2,7}이면 2열 7열이 주석필드)
        public List<int> rowCommentList = new List<int>(); // 행의 주석 번호를 저장한다.(행 번호 순 ex) {2,3}이면 2행 3행 주서행)

        public List<string> fieldDataTypeTable = new List<string>();
        public List<string> fieldDataTypeCSharp = new List<string>();
        public List<string> fieldNameList = new List<string>();
        
        public List<int> commentColumnNums = new List<int>(); // 주석처리 열 번호 리스트
        public List<int> commentRowNums = new List<int>(); // 주석처리 행 번호 리스트

        public Dictionary<string, FieldData> fieldDatas = new Dictionary<string, FieldData>(); // 필드 데이터 딕셔너리
        
        //public SheetData(Excel.Sheets sheets, string sheetName, DataRule dataRule)
        public SheetData(Excel.Worksheet sheet, string sheetName, DataRule dataRule, Action<RichTextBox, string> SetRichTextBox, RichTextBox richTextBox)
        {
            RichTextBox rb = richTextBox;
            Action<RichTextBox,string> srb = SetRichTextBox;

            // TODO : 쓰레드를 사용기 위해서는 Excel 관련 클래스를 사용하면 안된다.
            this.name = sheetName;
            
            srb(rb, "");
            srb(rb, "테이블 기반 정보 추출 : " + this.name);

            this.workSheet = sheet;

            this.dataRule = dataRule;

            this.totalColumnCount = this.workSheet.Cells.Find("*", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious, false, false, Type.Missing).Column;
            this.totalRowCount = this.workSheet.Cells.Find("*", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious, false, false, Type.Missing).Row;

            this.totalDataCount = this.totalColumnCount * this.totalRowCount; // 데이터 카운트
        }

        public void Create(Action<RichTextBox, string> SetRichTextBox,
                           RichTextBox richTextBox,
                           Action<ProgressBar, int> SetProgressBar = null,
                           ProgressBar progressBar = null,
                           Action<Label, string> SetTableLabel = null,
                           Label label = null)
        {
            RichTextBox rb = richTextBox;
            Action<RichTextBox, string> srb = SetRichTextBox;

            srb(rb, "");
            srb(rb, "테이블 기반 정보");
            srb(rb, "- 테이블 이름 : " + this.name);
            srb(rb, "- 테이블 필드 개수 : " + this.totalColumnCount);
            srb(rb, "- 테이블 레코드 개수 : " + this.totalRowCount);
            srb(rb, "- 테이블 데이터 총합 : " + this.totalDataCount.ToString("#,###"));

            // UI에 현재 진행중인 테이블 정보를 넣는다.
            if (SetTableLabel != null)
            {
                SetTableLabel(label, this.name);
            }

            for (int column = 1; column <= this.totalColumnCount; column++) // 필드 카운트
            {
                FieldData fieldData = new FieldData(); // 필드 데이터를 생성한다.

                srb(rb, "");
                srb(rb, "필드 정보");

                for (int row = 1; row <= this.totalRowCount; row++) // 로우 카운트
                {
                    string data = (this.workSheet.Cells[row, column] as Excel.Range).Text; // 해당 레이블의 데이터를 추출한다.

                    if (!data.StartsWith(this.dataRule.commentFieldMark))  // 필드 주석 "&" 이 아닐 경우
                    {
                        if (row.Equals(DataRule.FIELD_DESIGN_NAME))
                        {
                            fieldData.designName = data; // 필드 기획 이름설정일 경우
                            srb(rb, "- 필드 기획 설정 이름 : " + fieldData.designName);
                        }
                        else if (row.Equals(DataRule.FIELD_NAME))
                        {
                            fieldData.name = data; // 필드 이름일 경우
                            srb(rb, "- 필드 이름 : " + fieldData.name);
                        }
                        else if (row.Equals(DataRule.FIELD_DATA_TYPE))
                        {
                            fieldData.dataType = data; // 필드 데이터 타입일 경우

                            srb(rb, "- 필드 타입 : " + fieldData.dataType);
                            srb(rb, "");
                        }
                        else
                        {
                            if (column.Equals(1)) // 첫번째 필드일 경우 행에 대한 주석처리를 한다.                            
                            {
                                if (data.StartsWith(dataRule.commentRowMark)) // 행 주석일 경우 "//" 
                                {
                                    commentRowNums.Add(row); // 행 주석처리 번호를 추가한다.
                                }
                                else
                                {
                                    // TODO : 타입에 맞는 데이터 형인지 체크한다.
                                    // TODO : 바이너리 데이터에 기록한다.

                                    fieldData.Add(data); // 데이터를 추가한다.
                                    srb(rb, "- 레이블 [" + column + "],[" + row + "] : " + data);
                                }
                            }
                            else // 첫번째 필드가 아닐 경우
                            {
                                if (!commentRowNums.Contains(row)) // 주석 행이 아닐 경우
                                {
                                    // TODO : 타입에 맞는 데이터 형인지 체크한다.

                                    fieldData.Add(data); // 데이터를 추가한다.
                                    srb(rb, " - 레이블 [" + column + "],[" + row + "] : " + data);
                                }
                            }
                        }
                    }
                    else // 주석 필드일 경우
                    {
                        commentColumnNums.Add(column); // 주석 필드 번호를 기록한다.
                        srb(rb, "- 주석 필드 [필드 : " + column + "]");
                        break; // 루프틑 탈출해서 더이상 데이터를 읽지 못하게 한다.
                    }

                    if (SetProgressBar != null) // 프로그래스바가 할당되지 않는다면
                    {
                        // 잰행도 구하기
                        int p = (int)(((double)(((column - 1) * this.totalRowCount) + row) / this.totalDataCount) * 1000);

                        SetProgressBar(progressBar, p); // 프록래스바에 표시한다.
                    }
                }

                if (!commentColumnNums.Contains(column)) // 주석 필드가 아니라면
                {
                    fieldDatas.Add(fieldData.name, fieldData); // 필드 데이터스에 등록한다.
                }
            }

            srb(rb, "");
            srb(rb, "====== [" + this.name + "]테이블 변환완료");
        }
    }
}
