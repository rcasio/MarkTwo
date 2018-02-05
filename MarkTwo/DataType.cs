using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Reflection.Emit;

namespace MarkTwo
{
    public class DataType
    {
        Excel.Worksheet ruleSheet; // [테이블_규칙] 시트
        DataManager dataManager;

        const int SUPPROT_TYPE_COUNT = 8; // 클라이언트의 자료형 개수를 나타낸다. 만약 자료형이 추가 및 삭제된다면 이부분을 수정한다.

        public Dictionary<string, Type> cSharpTypes = new Dictionary<string, Type>(); // c# 자료형
        public Dictionary<string, Type> mySQLTypes = new Dictionary<string, Type>(); // MySQL 자료형

        //enum item {Weapon, Armor, Etc }

        public DataType(Excel.Worksheet ruleSheet, DataManager dataManager)
        {
            this.ruleSheet = ruleSheet;
            this.dataManager = dataManager;

            // 8개의 자료형을 가진다. 만약 엑셀에서 자료형을 추가한다면 이 부분을 수정해야 한다.
            for (int i = 0; i < SUPPROT_TYPE_COUNT; i++)
            {
                string range = null;
                string typeText = null;

                // C# 자료형 추출
                range = "D" + (22 + i).ToString();
                typeText = ruleSheet.Range[range].Value;

                cSharpTypes.Add(typeText, this.GetCShapType(typeText)); // 리스트에 자료형을 등록한다.
                //cSharpTypes.Add("Item", typeof(item));
                // TODO : enum 을 등록한다.// TODO : enum 을 등록한다.
                

                // MYSQL 자료형 추출
                range = "C" + (22 + i).ToString();
                typeText = ruleSheet.Range[range].Value;

                mySQLTypes.Add(typeText, this.GetMySQLType(typeText));
                //ClientTypeList.Text += type + "\n"; // 라벨에 표시한다.
            }

            Console.WriteLine("");
            Console.WriteLine("====== 엑셀 지원 자료형");
            Console.WriteLine("===c# 자료형");
            foreach (var cSharpType in cSharpTypes.Keys)
            {
                Console.WriteLine("엑셀 스트링 : " + cSharpType + " => 시스템 자료형 : " + cSharpTypes[cSharpType]);
            }
            
            Console.WriteLine("===MySQL 자료형");
            foreach (var mySQType in mySQLTypes.Keys)
            {
                Console.WriteLine("엑셀 스트링 : " + mySQType + " => 시스템 자료형 : " + mySQLTypes[mySQType]);
            }
        }

        /// <summary>
        /// MySQL 자료형을 추출한다.
        /// </summary>
        /// <param name="text">엑셀에 기록되어 있는 MySQL 타입</param>
        /// <returns>MySQL Type</returns>
        private Type GetMySQLType(string text)
        {
            Type type = null;

            if (text.Equals("Bit")) type = typeof(bool);
            if (text.Equals("TinyInt")) type = typeof(byte);
            if (text.Equals("SmallInt")) type = typeof(short);
            if (text.Equals("Int")) type = typeof(int);
            if (text.Equals("Float")) type = typeof(float);
            if (text.Equals("Double")) type = typeof(double);
            if (text.Equals("Bigint")) type = typeof(long);
            if (text.Equals("Char")) type = typeof(string);

            return type;
        }

        /// <summary>
        /// c# 타입을 리턴한다.
        /// </summary>
        /// <param name="text"> 엑셀에 기록되어 있는 c# 타입</param>
        /// <returns>C# type</returns>
        public Type GetCShapType(string text)
        {
            Type type = null;

            if (text.Equals("bool")) type = typeof(bool);
            if (text.Equals("byte")) type = typeof(byte);
            if (text.Equals("short")) type = typeof(short);
            if (text.Equals("int")) type = typeof(int);
            if (text.Equals("float")) type = typeof(float);
            if (text.Equals("double")) type = typeof(double);
            if (text.Equals("long")) type = typeof(long);
            if (text.Equals("string")) type = typeof(string);

            return type;
        }

        // TODO : 동적 Enum 생성하는 부분 분석하고 정리할 것
        // https://stackoverflow.com/questions/25173769/getting-enum-from-a-dynamically-created-enum
        // https://stackoverflow.com/questions/725043/dynamic-enum-in-c-sharp/792332#792332
        public Type GenerateEnumerations(List<string> lEnumItems, string assemblyName)
        {   
            // Create Base Assembly Objects
            AppDomain appDomain = AppDomain.CurrentDomain;
            AssemblyName asmName = new AssemblyName(assemblyName);
            AssemblyBuilder asmBuilder = appDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);

            // Create Module and Enumeration Builder Objects
            ModuleBuilder modBuilder = asmBuilder.DefineDynamicModule(assemblyName + "_module");
            EnumBuilder enumBuilder = modBuilder.DefineEnum(assemblyName, TypeAttributes.Public, typeof(int));
            enumBuilder.DefineLiteral("None", 0);

            int flagCnt = 1;

            foreach (string fmtObj in lEnumItems)
            {
                enumBuilder.DefineLiteral(fmtObj, flagCnt);
                flagCnt++;
            }

            var retEnumType = enumBuilder.CreateType();
            //asmBuilder.Save(asmName.Name + ".dll");
            return retEnumType;
        }
    }
}
