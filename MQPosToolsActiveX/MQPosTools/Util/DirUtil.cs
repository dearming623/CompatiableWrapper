using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MQPosTools.Util
{
    public class DirUtil
    {

        public static bool VaildatePath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            Regex reg = new Regex(@"[a-zA-Z]\:[\\a-zA-Z0-9_\\]+[\.]?[a-zA-Z0-9_]+");
            if (!reg.IsMatch(fileName))
                return false;

            string dir = System.IO.Path.GetDirectoryName(fileName);
            if (!System.IO.Directory.Exists(dir))
                return false;

            string name = System.IO.Path.GetFileName(fileName);
            if (name == null || name == "")
                return false;

            return true;
        }

    }
}
