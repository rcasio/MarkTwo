using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkTwo
{
    /// <summary>
    /// 정상적인 데이터인지 체크합니다.
    /// </summary>
    class DataManager
    {
        public ConverterWindow converterWindow { get; set; }
        public TableData tableData { get; set; }

        static string tableName;
        static int columnNumber;
        static int rowNumber;
        static object data;
        
        /// <summary>
        /// 첫번째 행 데이터 중복 처리를 위한 리스트
        /// </summary>
        List<string> rowFirstColumns = new List<string>();

        /// <summary>
        /// 주석 데이터의 오류를 체크한다.
        /// </summary>
        public void CheckData(string tableName, int rowNumber, int columnNumber, object data)
        {
            DataManager.tableName = tableName;
            DataManager.columnNumber = columnNumber;
            DataManager.rowNumber = rowNumber;

            DataManager.data = data;

            if (DataManager.rowNumber == ConverterWindow.FIELD_COMMENTLINE) this.CheckFirstLineData(); // 첫번째 행일 경우
            if (DataManager.rowNumber == ConverterWindow.FIELDNAME_LINE) this.CheckSecondLineData(); // 두번째 행일 경우
            if (DataManager.rowNumber == ConverterWindow.DATATYPE_LINE) this.CheckThirdLineData(); // 세번째 행일 경우
        }

        /// <summary>
        /// 첫번째 데이터를 체크한다.
        /// 1. 셀에 데이터가 반드시 존재해야 한다.
        /// </summary>
        void CheckFirstLineData()
        {
            // 데이터가 없을 경우
            if (data == null)
            {
                MessageBox.Show("[" + tableName + "] 테이블 첫번째 행의 [" + columnNumber + "] 번째 필드 데이터가 입력되어 있지 않습니다.", "엑셀 필드 오류");
                converterWindow.Close();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 두번째 데이터를 체크한다. (DB 이름필드)
        /// 1. 자료형이 입력되어 있는지 검사한다. (추출된 클래스에서 오류가 난다.)
        /// </summary>
        void CheckSecondLineData()
        {
            foreach (string type in converterWindow.clientDataTypes)
            {
                // 데이터가 없을 경우
                if (data == null)
                {
                    MessageBox.Show("[" + tableName + "] 테이블 두번째 행\n[" + columnNumber + "] 번째 필드의 필드이름이 입력되어 있지 않습니다.", "엑셀 필드 오류");
                    converterWindow.Close();
                    Environment.Exit(0);
                }

                // 타입이 다를 경우
                if (data.ToString().Equals(type))
                {
                    MessageBox.Show("[" + tableName + "] 테이블 두번째 행의 [" + columnNumber + "] 번째 필드 이름이 [" + data + "]로 클라이언트 자료형과 충돌합니다.\n\n※ [테이블 규칙] 시트의 C# 자료형을 참조하여 필드 이름을 클라이언트 자료형과 충돌하지 않도록 변경하시기 바랍니다.", "엑셀 필드타입 입력 오류");
                    converterWindow.Close();
                    Environment.Exit(0);
                }
            }
	    }

        /// <summary>
        /// 세번째 데이터를 체크한다. (DB 자료형)
        /// </summary>
        void CheckThirdLineData()
        {
            foreach (string type in converterWindow.clientDataTypes)
            {
                // 데이터가 없을 경우
                if (data == null)
                {
                    MessageBox.Show("[" + tableName + "] 테이블 세번째 행\n[" + columnNumber + "] 번째 필드의 필드 자료형이 입력되어 있지 않습니다.", "엑셀 필드 오류");
                    converterWindow.Close();
                    Environment.Exit(0);
                }

                // 타입이 다를 경우
                if (data.Equals(type))
                {
                    MessageBox.Show("[" + tableName + "] 테이블 세번째 행의 [" + columnNumber + "] 번째 자료형이 [" + data + "]로 잘못 입력되어 있습니다.\n\n※ [테이블 규칙] 시트의 MSSSQL 자료형을 참조하시기 바랍니다.", "엑셀 데이터타입 입력 오류");
                    converterWindow.Close();
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// 행 주석을 감별해야할 경우
        /// </summary>
        public void CheckData_RowComment_IfNull(string tableName, int rowNumber, object data)
        {
            // 데이터가 없을 경우
            if (data == null)
            {
                MessageBox.Show("[" + DataManager.tableName + "] 테이블 첫번째 필드의 [" + DataManager.rowNumber + "] 번째 줄의 데이터가 입력되어 있지 않습니다.", "엑셀 행 오류");
                converterWindow.Close();
                Environment.Exit(0);
            }
        }
    }
}
