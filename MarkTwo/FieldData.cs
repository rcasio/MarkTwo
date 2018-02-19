using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkTwo
{
    public class FieldData
    {
        DataManager dataManager;

        public string designName;
        public string name;
        public string dataType;

        public List<string> contents = new List<string>();

        public FieldData(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public void Add(string obj)
        {
            contents.Add(obj);
        }
    }
}
