using System;
using System.Collections.Generic;
using System.Text;

namespace MQOfficeTools.Utils
{
    public class CodePage
    {
        /// <summary>
        /// 当前系统默认的编码
        /// </summary>
        public static Encoding SYS_DEFAULT = Encoding.Default;

        /// <summary>
        /// PowerBuilder 7 使用的字符编码
        /// 如果字符串是 PB7 传参的时候，需要使用 ISO-8859-1进行解码才能得到正确的字符
        /// </summary>
        public static Encoding ISO_8859_1 = Encoding.GetEncoding("ISO-8859-1");

        public static Encoding UTF8 = Encoding.UTF8;
    }
}
