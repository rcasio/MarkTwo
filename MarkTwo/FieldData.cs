using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkTwo
{
    public class FieldData
    {
        public string designName;
        public string name;
        public string dataType;

        public List<string> contents = new List<string>();

        public void Add(string obj)
        {
            contents.Add(obj);
        }
    }
}
