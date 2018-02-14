using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MarkTwo
{
    interface IThread
    {
        void CheckThrea1ds(object soruce, ElapsedEventArgs e);
        void CloseThreads();
    }
}
