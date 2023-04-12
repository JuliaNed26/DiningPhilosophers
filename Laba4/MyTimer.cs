using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4
{
    internal sealed class MyTimer
    {
        private Stopwatch sw = new();
        public void Start() => sw.Start();
        public bool CanEat() => sw.ElapsedMilliseconds < ConstantsTime.MaxRunningProgramTime;
    }
}
