using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkTwo
{
    public static class TagManager
    {
        // 시트를 구분하기 위해서 사용된다.
        public enum SheetType { Client, Server }
        
        // 시트 이름
        public class SheetName
        {
            public const string Multilingual = "Multilingual"; // 다국어 시트
        }
    }

    
}
