using System;
using System.Collections.Generic;
using System.Text;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Drawing.Imaging;

namespace MoleQ.QRCode
{
    public class BarcodeGenerator
    {
        ///// <summary>
        ///// 生成二维码,保存成图片
        ///// </summary>
        //public static void CreateQRCode(string text, string fileName, int width, int height)
        //{
        //    BarcodeWriter writer = new BarcodeWriter();
        //    writer.Format = BarcodeFormat.QR_CODE;
        //    QrCodeEncodingOptions options = new QrCodeEncodingOptions();
        //    options.DisableECI = true;
        //    //设置内容编码
        //    options.CharacterSet = "UTF-8";
        //    //设置二维码的宽度和高度
        //    options.Width = 500;
        //    options.Height = 500;
        //    //设置二维码的边距,单位不是固定像素
        //    options.Margin = 1;
        //    writer.Options = options;

        //    Bitmap map = writer.Write(text);
        //    map.Save(fileName, ImageFormat.Jpeg);
        //    map.Dispose();
        //}



        /// <summary>
        /// 生成二维码,保存成图片
        /// </summary>
        public static Bitmap CreateQRCode(string text, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            //设置内容编码
            options.CharacterSet = "UTF-8";
            //设置二维码的宽度和高度
            options.Width = 500;
            options.Height = 500;
            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;
            writer.Options = options;

            return writer.Write(text);
        }
    }
}
