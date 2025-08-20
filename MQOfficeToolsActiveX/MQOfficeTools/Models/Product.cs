using System;
using System.Collections.Generic;
using System.Text;

namespace MQOfficeTools.Models
{
    public class Product
    {
        public string UPC { get; set; }
        public string Name { get; set; }
        //public string OtherName1 { get; set; }
        //public string OtherName2 { get; set; }

        private string _OtherName1;
        public string OtherName1
        {
            get { return _OtherName1; }
            set
            {
                //string v = value.Trim();
                //byte[] src = Encoding.Default.GetBytes(v);
                //_OtherName1 = Encoding.GetEncoding("gb2312").GetString(src);

                string val = value.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    _OtherName1 = null;
                }
                else
                {
                    /*

                    byte[] src = Encoding.Default.GetBytes(val);
                    byte[] uft8Bytes = Encoding.UTF8.GetBytes(val);
                    byte[] unicodeBytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, uft8Bytes);
                    byte[] asciiBytes = Encoding.Convert(Encoding.UTF8, Encoding.ASCII, uft8Bytes);


                   // val = Encoding.UTF8.GetString(uft8Bytes);
                    Logger.error("Logger-> ","utf-8:" + Encoding.UTF8.GetString(uft8Bytes));

                    val = Encoding.Unicode.GetString(unicodeBytes);
                    Logger.error("Logger-> ", "Unicode:" + Encoding.Unicode.GetString(unicodeBytes));

                    //val = Encoding.ASCII.GetString(asciiBytes);
                    Logger.error("Logger-> ", "ASCII:" + Encoding.ASCII.GetString(asciiBytes));

                    char[]  asciichars = new char[ Encoding.ASCII.GetCharCount(asciiBytes,0,asciiBytes.Length)];
                     Encoding.ASCII.GetChars(asciiBytes,0,asciiBytes.Length,asciichars,0);
                     string asciiString = new string(asciichars);
                     Logger.error("Logger-> ", "Ascii converted string:" + asciiString);
                    */

                    //byte[] src = Encoding.Default.GetBytes(val);
                    //val = Encoding.GetEncoding("big5").GetString(src);
                    //Logger.error("Logger-> ", "big5 converted string:" + val);
                    _OtherName1 = val;
                }
            }
        }

        private string _OtherName2;
        public string OtherName2
        {
            get { return _OtherName2; }
            set
            {
                //string v = value.Trim();
                //byte[] src = Encoding.Default.GetBytes(v);
                //_OtherName2 = Encoding.GetEncoding("gb2312").GetString(src);

                string val = value.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    _OtherName2 = null;
                }
                else
                {
                    /*
                    byte[] src = Encoding.Default.GetBytes(val);
                    byte[] uft8Bytes = Encoding.UTF8.GetBytes(val); 
                    byte[] unicodeBytes = Encoding.Convert(Encoding.UTF8,Encoding.Unicode,uft8Bytes);
                    byte[] asciiBytes = Encoding.Convert(Encoding.UTF8,Encoding.ASCII,uft8Bytes);


                    // val = Encoding.UTF8.GetString(uft8Bytes);
                    Logger.error("Logger-> ", "utf-8:" + Encoding.UTF8.GetString(uft8Bytes));

                    val = Encoding.Unicode.GetString(unicodeBytes);
                    Logger.error("Logger-> ", "Unicode:" + Encoding.Unicode.GetString(unicodeBytes));

                    //val = Encoding.ASCII.GetString(asciiBytes);
                    Logger.error("Logger-> ", "ASCII:" + Encoding.ASCII.GetString(asciiBytes));

                    char[] asciichars = new char[Encoding.ASCII.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                    Encoding.ASCII.GetChars(asciiBytes, 0, asciiBytes.Length, asciichars, 0);
                    string asciiString = new string(asciichars);
                    Logger.error("Logger-> ", "Ascii converted string:" + asciiString);

                    val = Encoding.ASCII.GetString(Encoding.Default.GetBytes("A名字A"));
                     */

                    _OtherName2 = val;
                }
            }
        }

        private string _OtherName3;
        public string OtherName3
        {
            get { return _OtherName3; }
            set
            {
                string val = value.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    _OtherName3 = null;
                }
                else
                {
                    _OtherName3 = val;
                }
            }
        }

        private string _OtherName4;
        public string OtherName4
        {
            get { return _OtherName4; }
            set
            {
                string val = value.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    _OtherName4 = null;
                }
                else
                {
                    _OtherName4 = val;
                }
            }
        }

        private string _OtherName5;
        public string OtherName5
        {
            get { return _OtherName5; }
            set
            {
                string val = value.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    _OtherName5 = null;
                }
                else
                {
                    _OtherName5 = val;
                }
            }
        }

        private string _Measurement;
        public string Measurement
        {
            get { return _Measurement; }
            set
            {
                string val = value.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    _Measurement = null;
                }
                else
                {
                    _Measurement = val;
                }
            }
        }

        private string _Unit;
        public string Unit
        {
            get { return _Unit; }
            set
            {
                string val = value.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    _Unit = null;
                }
                else
                {
                    _Unit = val;
                }
            }
        }


        private string _ItemImage;
        public string ItemImage
        {
            get { return _ItemImage; }
            set
            {
                string val = value.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    _ItemImage = null;
                }
                else
                {
                    _ItemImage = val;
                }
            }
        }


        private string _ImageFile;
        public string ImageFile
        {
            get { return _ImageFile; }
            set
            {
                string val = value.Trim();
                if (string.IsNullOrEmpty(val))
                {
                    _ImageFile = null;
                }
                else
                {
                    _ImageFile = val;
                }
            }
        }
    }
}
