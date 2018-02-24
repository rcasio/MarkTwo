using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MarkTwo
{
    //class Create_PHPCode
    //{
    //    public const string TABLECONVERTER_FILENAME = "TableDB.php";

    //    ConverterWindow m_ConverterWindow;

    //    // 싱글톤
    //    private static Create_PHPCode instance;
    //    public static Create_PHPCode Instance
    //    {
    //        get
    //        {
    //            if (instance == null) { instance = new Create_PHPCode(); }
    //            return instance;
    //        }
    //    }

    //    // 문자열을 편집할 때 사용됩니다.
    //    static StringBuilder edit_string = new StringBuilder();

    //    // 코드를 작성할 파일스트림
    //    StreamWriter m_TableConverter;

    //    // 최초 생성 시 기본적인 코드를 짜도록 한다.
    //    public Create_PHPCode()
    //    {
    //        m_TableConverter = new StreamWriter(new FileStream(this.Create_FilePath_TableConverter(), FileMode.Create));

    //        m_TableConverter.WriteLine("<?php");
    //        m_TableConverter.WriteLine("/********************************************************/");
    //        m_TableConverter.WriteLine("/* 이 문서는 VisualDesign에 의해 자동으로 업데이트 됩니다.");
    //        m_TableConverter.WriteLine("/* 문서를 반드시 툴에 의해 수정되어야 하며");
    //        m_TableConverter.WriteLine("/* 그 외의 인위적인 상황에서 수정되면 안됩니다.");
    //        m_TableConverter.WriteLine("/********************************************************/");
    //        m_TableConverter.WriteLine("");
    //        m_TableConverter.WriteLine("require_once \"DB.php\";");
    //        m_TableConverter.WriteLine("require_once(\"../../data/Game_Info.php\");");
    //        m_TableConverter.WriteLine("");
    //        m_TableConverter.WriteLine("$TableDB;");
    //        m_TableConverter.WriteLine("");
    //        m_TableConverter.WriteLine("$TableDB = DB_Controller::getInstance();");
    //        m_TableConverter.WriteLine("");

    //        // 테이블의 데이터를 담을 배열을 선언한다.
    //        foreach (string sheetName in ConverterWindow.serverSheetNames)
    //        {
    //            m_TableConverter.WriteLine(AddString("$Data_", sheetName, ";"));
    //        }

    //        m_TableConverter.WriteLine("");

    //        // 실행함수를 작성한다.
    //        m_TableConverter.WriteLine("function Load_TableData()");
    //        m_TableConverter.WriteLine("{");
    //        m_TableConverter.WriteLine("    global $TableDB;");
    //        m_TableConverter.WriteLine("");

    //        // 데이터를 담을 배열을 글로벌로 선언한다.
    //        foreach (string sheetName in ConverterWindow.serverSheetNames)
    //        {
    //            m_TableConverter.WriteLine(AddString("    global $Data_", sheetName, ";"));
    //        }

    //        m_TableConverter.WriteLine("");
    //        m_TableConverter.WriteLine("    $TableDB->DB_Connect();");
    //        m_TableConverter.WriteLine("");
    //        m_TableConverter.WriteLine("    $TableDB->DB_SendQuery(\"USE \".DBName::$Excel_Server_Table);   // 다른 DB를 선택한다.");
    //        m_TableConverter.WriteLine("");
    //    }

    //    // 테이블의 시트 정보를 참조하여 테이블 변환용 코드를 만든다.
    //    public void WriteCode_TableConverter_PHP(TableDataS tableData)
    //    {
    //        // 데이터를 생성하는 코드를 짠다.
    //        m_TableConverter.WriteLine(AddString("    $Data_", tableData.name, " = Query_SEND_TableData(\"", tableData.name, "\");"));
    //    }

    //    // 파일 경로를 반환한다.
    //    string Create_FilePath_TableConverter() { return Application.StartupPath + "\\" + TABLECONVERTER_FILENAME; }

    //    // 문자열을 편집할 때 사용됩니다.
    //    string AddString(params string[] string_List)
    //    {
    //        // 유니티에서는 Clear() 함수를 지원하지 않는다.
    //        edit_string.Clear();

    //        int i = 0;
    //        int length = string_List.Length;

    //        while (i < length)
    //        {
    //            if (string.IsNullOrEmpty(string_List[i]) == false)
    //            {
    //                edit_string.Append(string_List[i]);
    //            }
    //            i++;
    //        }
    //        return edit_string.ToString();
    //    }

    //    public void Close_StreamWrite()
    //    {
    //        m_TableConverter.WriteLine("");
    //        m_TableConverter.WriteLine("    $TableDB->DB_Disconnection();");
    //        m_TableConverter.WriteLine("}");
    //        m_TableConverter.WriteLine("");
    //        m_TableConverter.WriteLine("function Query_SEND_TableData($tableName)");
    //        m_TableConverter.WriteLine("{");
    //        m_TableConverter.WriteLine("    global $TableDB;");
    //        m_TableConverter.WriteLine("    $send_query = \"SELECT * FROM  $tableName\";");
    //        m_TableConverter.WriteLine("    $TableDB->DB_SendQuery($send_query);");
    //        m_TableConverter.WriteLine("    if($TableDB->m_ReceiveData == false) // 오류 처리");
    //        m_TableConverter.WriteLine("    {");
    //        m_TableConverter.WriteLine("        echo \"<BR> QueryError : $send_query\";");
    //        m_TableConverter.WriteLine("        die(mysql_error());");
    //        m_TableConverter.WriteLine("    }");
    //        m_TableConverter.WriteLine("");
    //        m_TableConverter.WriteLine("    while($value = mysqli_fetch_assoc($TableDB->m_ReceiveData)) // mysqli_fetch_assoc()는 배열로 한 행씩 리턴한다 http://php.net/manual/en/function.mysql-fetch-assoc.php");
    //        m_TableConverter.WriteLine("    {");
    //        m_TableConverter.WriteLine("        $values[] = $value; // 키값 = 테이블 이름, 벨류 = 테이블의 배열");
    //        m_TableConverter.WriteLine("    }");
    //        m_TableConverter.WriteLine("");
    //        m_TableConverter.WriteLine("    return $values;");
    //        m_TableConverter.WriteLine("}");
    //        m_TableConverter.WriteLine("?>");
    //        m_TableConverter.Close();
    //    }
    //}
}
