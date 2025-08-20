using System;
using System.Collections.Generic;
using System.Text;
using MoleQ.Support.Wrapper.Common.Util;

namespace MQWebApi
{
    public class PageCodeConverter
    {

        public static string ToCodePage(string input, Encoding srcEncoding, Encoding dstEncoding)
        {
            string dstStr = ConvertToCodePage(input, Encoding.Default, srcEncoding);
            return EncodingToStr(dstStr, Encoding.Default, dstEncoding);  
        }

        public static string ToCodePage2(string str, Encoding strEncoding, Encoding dstEncoding)
        {
            string dstStr = ConvertByChar(str, Encoding.Default, strEncoding);
            //var afterSysEncodingStr = ConvertByChar(dstStr, Encoding.UTF8, Encoding.Default);
            var res = EncodingToStr(dstStr, Encoding.Default, dstEncoding);
            return res;
        }

        public static string ToCodePageTest(string input, Encoding srcEncoding, Encoding dstEncoding)
        {
            Encoding big5 = Encoding.GetEncoding("big5");
            Encoding gb2312 = Encoding.GetEncoding("gb2312");

            string big5_res_1 = ConvertToCodePage(input, srcEncoding, big5);
            string big5_res_2 = EncodingToStr(big5_res_1, big5, dstEncoding);
            string big5_res_3 = ConvertToCodePage(big5_res_1, big5, dstEncoding);

            string gb2312_1 = ConvertToCodePage(input, srcEncoding, gb2312);
            string gb2312_2 = EncodingToStr(gb2312_1, gb2312, dstEncoding);
            string gb2312_3 = ConvertToCodePage(gb2312_1, gb2312, dstEncoding);

            string dstStr = ConvertToCodePage(input,Encoding.Default, srcEncoding);
            string res = EncodingToStr(dstStr, Encoding.Default, dstEncoding);
            string res2 = EncodingToStr(input, Encoding.Default, dstEncoding);
            return res;
        }

        public static string ConvertToCodePage(string input, Encoding srcCodePage, Encoding dstCodePage)
        { 
            byte[] srcBytes = srcCodePage.GetBytes(input);
            byte[] dstBytes = Encoding.Convert(srcCodePage, dstCodePage, srcBytes);
            return dstCodePage.GetString(dstBytes);
        }

        private static string ConvertByChar(string srcStr, Encoding srcEncoding, Encoding dstEncoding)
        {
            byte[] srcBytes = srcEncoding.GetBytes(srcStr);
            byte[] dstBytes = Encoding.Convert(srcEncoding, dstEncoding, srcBytes);
            char[] resChars = new char[dstEncoding.GetCharCount(dstBytes, 0, dstBytes.Length)];
            dstEncoding.GetChars(dstBytes, 0, dstBytes.Length, resChars, 0);
            return new string(resChars);
        }

        private static string EncodingToStr(string input, Encoding srcEncode, Encoding dstEncode)
        {
            string res = string.Empty;
            try
            {
                byte[] srcBytes = srcEncode.GetBytes(input);
                res = dstEncode.GetString(srcBytes);
            }
            catch (Exception ex)
            {
                Logger.Error("EncodingConverter", "ConvertEncode -> " + ex.Message);
            }
            return res;
        }

        public static Encoding ISO_8859_1 = Encoding.GetEncoding("ISO-8859-1"); // pb7使用的编码方式

        public static string StringToUnicode(string input)
        {
            StringBuilder unicodeStringBuilder = new StringBuilder();
            foreach (char c in input)
            {
                unicodeStringBuilder.Append("\\u");
                unicodeStringBuilder.Append(((int)c).ToString("X4"));
            }
            return unicodeStringBuilder.ToString();
        }

        public static string UnicodeToString(string input)
        {
            string[] unicodeParts = input.Split(new[] { "\\u" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder decodedStringBuilder = new StringBuilder();
            foreach (string part in unicodeParts)
            {
                int value = Convert.ToInt32(part, 16);
                decodedStringBuilder.Append((char)value);
            }
            return decodedStringBuilder.ToString();
        }
 
 

    }
}
