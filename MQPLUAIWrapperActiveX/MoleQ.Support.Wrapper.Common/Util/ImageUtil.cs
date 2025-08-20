using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;
using MoleQ.Support.Wrapper.Common.Util;

namespace MoleQ.Support.Wrapper.Common.Util
{
    public class ImageUtil
    {
        private const string TAG = "ImageUtil";

        /// <summary>
        /// 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap ReadImageFile(string path)
        {
            FileStream fs = File.OpenRead(path); //OpenRead
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] image = new Byte[filelength]; //建立一个字节数组 
            fs.Read(image, 0, filelength); //按字节流读取 
            Image result = Image.FromStream(fs);
            fs.Close();
            Bitmap bit = new Bitmap(result);
            return bit;
        }


        public void DownloadImage(string url, string path)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ServicePoint.Expect100Continue = false;
            req.Method = "GET";
            req.KeepAlive = true;
            req.ContentType = "image/*";
            req.Timeout = 30 * 1000;
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            System.IO.Stream stream = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                Image.FromStream(stream).Save(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Error(TAG, "Failed to download image. URL: " + url);
                //Logger.error(TAG, ex.Message);
                throw ex;
            }
            finally
            {
                // 释放资源
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }
    }
}
