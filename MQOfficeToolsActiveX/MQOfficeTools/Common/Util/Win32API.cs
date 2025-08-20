using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MQOfficeTools.Common.Util
{
    public class Win32API
    {
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name);
    }
}
