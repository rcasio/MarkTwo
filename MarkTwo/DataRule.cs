using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 참조 : http://msdn.microsoft.com/en-us/library/office/microsoft.office.interop.excel.aspx
using Excel = Microsoft.Office.Interop.Excel;

namespace MarkTwo
{
    /// <summary>
    /// [테이블_규칙]에 정의되어 있는 테이블 규칙을 객체화 한다.
    /// </summary>
    public class DataRule
    {
        Excel.Worksheet ruleSheet; // [테이블_규칙] 시트
        DataManager dataManager;

        // 행과 열의 주석을 나타낸다.
        public const int FIELD_COMMENTLINE = 1;    // 클라 및 서버에서 사용할 필드인지 나타내는 행
        public const int FIELDNAME_LINE = 2;    // 필드 이름이 기록된 행
        public const int DATATYPE_LINE = 3;    // 데이터 타입이 기록된 행
        public const int ROW_COMMENTFIELD = 1; // 데이터 행이 주석행인지 판별하는 첫번째 행
        public const int Default_RowComment_LineCount = 3;

        // 테이블 규칙 관련 (테이블에서 추출)
        public string commentFieldMark; // 코맨트 필드 마크
        public string commentFieldClientOnlyMark; // 코맨트 클라이언트 온리 마크
        public string commentRowMark; // 코멘트 로우 마크

        // 파일형식 지원여부 스트링 (On/Off) (테이블에서 추출)
        public string isSupportTextFile;
        public string isSupportBinary;
        public string isSupportJson;
        public string isSupprotCSV;
        public string isSupportXML;

        // 추출 형식 지원여부(테이블에서 추출)
        public bool isExtractionText = false; // 텍스트 파일을 추출할 것인가?(바이너리 파일로 변환된 파일을 텍스트로)
        public bool isExtractionBinary = false;
        public bool isExtractionJson = false;
        public bool isExtractionCSV = false;
        public bool isExtractionXML = false;

        /// <summary>
        /// 테이블 률 설정
        /// </summary>
        public DataRule(Excel.Worksheet ruleSheet, DataManager dataManager)
        {
            this.ruleSheet = ruleSheet;
            this.dataManager = dataManager;

            commentRowMark = this.ruleSheet.Range["B16"].Value;
            commentFieldMark = this.ruleSheet.Range["B17"].Value;
            commentFieldClientOnlyMark = this.ruleSheet.Range["B18"].Value;

            // 각 주석의 공백을 제거한다.
            commentFieldMark = commentFieldMark.Trim();
            commentFieldClientOnlyMark = commentFieldClientOnlyMark.Trim();
            commentRowMark = commentRowMark.Trim();
            
            try
            {
                isSupportTextFile = this.ruleSheet.Range["H23"].Value;
                isSupportBinary = this.ruleSheet.Range["H24"].Value;
                isSupportJson = this.ruleSheet.Range["H25"].Value;
                isSupprotCSV = this.ruleSheet.Range["H26"].Value;
                isSupportXML = this.ruleSheet.Range["H27"].Value;

                // 무결성 검사를 한다.
                if (isSupportTextFile.Equals("On") || isSupportTextFile.Equals("Off")) this.isExtractionText = (isSupportTextFile.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [Text 문서] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");

                if (isSupportBinary.Equals("On") || isSupportBinary.Equals("Off")) this.isExtractionBinary = (isSupportBinary.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [바이너리파일] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");

                if (isSupportJson.Equals("On") || isSupportJson.Equals("Off")) this.isExtractionJson = (isSupportJson.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [Json] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");

                if (isSupprotCSV.Equals("On") || isSupprotCSV.Equals("Off")) this.isExtractionCSV = (isSupprotCSV.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [CSV] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");

                if (isSupportXML.Equals("On") || isSupportXML.Equals("Off")) this.isExtractionXML = (isSupportXML.Equals("On")) ? true : false;
                else dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 \n[※ 추출할 파일 형식]의 [XML] 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");
            }
            catch (Exception)
            {
                dataManager.ShowCloseMSB("[테이블 규칙] 시트에서 [※ 추출할 파일 형식]의 양식이 잘못 입력되어 있습니다.\n\n(On/Off 로 입력하시기 바랍니다.)");
            }
        }
    }
}
