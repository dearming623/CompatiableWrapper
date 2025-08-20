using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;

namespace MoleQ.QRCode.Util
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

        public static ImageFormat GetImageFormat(string extension)
        {
            switch (extension.ToLower())
            {
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".gif":
                    return ImageFormat.Gif;
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                default:
                    return null;
            }
        }

    }
}
