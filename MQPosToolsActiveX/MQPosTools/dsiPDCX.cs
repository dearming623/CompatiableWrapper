using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace MQPosTools
{

    [Guid("A5B72FD8-7A59-4cea-9E70-1559007CE1AA")]
    public interface MQPosTools_Interface
    {

        [DispId(1)]
        int pdcxSigToPic(string fileName, string sigData, int maxX, int maxY);
       

        [DispId(2)]
        decimal pdcxGetPointByMberID(string ipAddress, string memberID);

    }


    [Guid("62CA739F-417B-4c01-B8FE-CE374214BB7F"),
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface MQPosTools_Events
    {

    }  


    [Guid("01A697A9-2EC0-491f-819B-20E4D11A974B"),
    ClassInterface(ClassInterfaceType.None),
    ComSourceInterfaces(typeof(MQPosTools_Events))]
    public class dsiPDCX : MQPosTools_Interface
    {

        public int pdcxSigToPic(string fileName, string sigData, int maxX,int maxY )
        {
            
            //bool isfile = false;
            //bool ispath = false;
            if (fileName == null || fileName == "")
                return 1;

            //if (fileName.IndexOf('.') > 0)
            //    isfile = true;

            //if (!isfile)
            //{
                Regex reg = new Regex(@"[a-zA-Z]\:[\\a-zA-Z0-9_\\]+[\.]?[a-zA-Z0-9_]+");
                if (!reg.IsMatch(fileName))
                    return 1;

                string dir = System.IO.Path.GetDirectoryName(fileName);
                if (!System.IO.Directory.Exists(dir))
                    return 1;

                string name = System.IO.Path.GetFileName(fileName);
                if (name == null || name == "")
                    return 1;
                
                //if (reg.IsMatch(fileName))
                //{
                //    //ispath = true;
                //}
                //else
                //{
                //    return 1;
                //}
            //}
          

            //if (isfile && !ispath)
            //{
            //    imagePath = Assembly.GetExecutingAssembly().Location + "\\" + fileName;
            //}
            //else if (!isfile && ispath)
            //{
            //    imagePath = ispath + "\\" + "sign.jpg";
            //}
            //else
            //{
            //    imagePath = fileName;
            //}

            if (sigData == null || sigData == "")
                return 2;


            if (maxX <= 0 && maxY <= 0 )
                return 3;


            //sigData = "151,16:144,26:136,36:131,40:128,44:125,48:122,48:124,46:127,40:130,34:133,28:137,22:143,18:149,14:154,14:160,18:183,42:180,42:179,42:#,#:145,26:142,26:154,26:158,24:163,22:#,#:198,18:197,22:201,38:204,38:203,32:201,26:198,22:200,20:206,20:209,18:232,18:235,22:232,26:228,30:224,30:221,32:218,32:214,34:211,34:221,34:224,36:231,36:234,38:238,40:238,44:235,46:232,48:229,50:225,50:221,50:217,50:213,50:210,50:207,50:204,50:201,48:200,46:#,#:295,20:292,20:289,20:286,20:283,20:280,20:277,20:274,20:271,20:268,20:265,24:262,26:259,28:259,34:263,36:279,36:296,36:#,#:331,18:331,20:341,36:#,#:307,17:325,17:344,17:353,19:368,19:372,27:369,31:366,33:362,35:359,37:348,41:342,43:338,45:335,45:332,45:328,47:325,47:321,47:318,47:315,47:312,47:#,#:382,17:406,17:431,17:428,17:425,17:422,17:419,17:415,17:412,19:408,19:405,19:402,21:399,21:396,23:391,29:388,35:388,43:391,43:394,45:415,45:437,45:439,43:#,#:419,26:416,26:413,26:410,26:407,26:404,26:401,26:429,26:435,24:442,24:#,#:467,18:497,18:528,18:#,#:476,19:474,23:474,35:475,39:475,43:#,#:472,28:477,28:480,26:503,26:507,24:526,24:#,#:";
            //maxX = 538;
            //maxY = 60;

            string[] pointArray = sigData.Split(':');

         
            Image imgTemp = new Bitmap(maxX, maxY);
            Graphics graphics = Graphics.FromImage(imgTemp); //graphics.Clear(Color.Black);

            graphics.FillRectangle(Brushes.White, new RectangleF(0, 0, maxX, maxY));
            Pen myPen = new Pen(Color.Black, 2);

            int leftOffset = 50;
            int x1, y1, x2, y2;

            if (pointArray.Length > 0)
            {
                string[] point = pointArray[0].Split(',');
                int min = Convert.ToInt32(point[0]);
                List<SignaturePoint> points = new List<SignaturePoint>();
                for (int i = 0; i < pointArray.Length; i++)
                {
                    if (i + 1 >= pointArray.Length)
                        continue;

                    if (pointArray[i] == "#,#")
                    {
                        points.Add(new SignaturePoint(-1, -1));
                        continue;
                    }

                    point = pointArray[i].Split(',');
                    x1 = Convert.ToInt32(point[0]);
                    y1 = Convert.ToInt32(point[1]);
                    points.Add(new SignaturePoint(x1, y1));

                    if (min > x1)
                    {
                        min = x1;
                    }
                }

                if (min <= leftOffset)
                {
                    leftOffset = min - 1;
                }

                for (int i = 0; i < points.Count; i++)
                {
                    if (i + 1 >= points.Count)
                        continue;

                    x1 = points[i].x;
                    y1 = points[i].y;

                    x2 = points[i + 1].x;
                    y2 = points[i + 1].y;

                    if (x1 == -1 && y1 == -1 || x2 == -1 && y2 == -1)
                        continue;
 
                    graphics.DrawLine(myPen, x1 - leftOffset, y1, x2 - leftOffset, y2);

                }

                //for (int i = 0; i < pointArray.Length; i++)
                //{
                //    if (i + 1 >= pointArray.Length)
                //        continue;

                //    if (pointArray[i] == "#,#" || pointArray[i + 1] == "#,#")
                //        continue;

                //    point = pointArray[i].Split(',');
                //    x1 = Convert.ToInt32(point[0]);
                //    y1 = Convert.ToInt32(point[1]);

                //    point = pointArray[i + 1].Split(',');
                //    x2 = Convert.ToInt32(point[0]);
                //    y2 = Convert.ToInt32(point[1]);

                //    graphics.DrawLine(myPen, x1 - leftOffset, y1, x2 - leftOffset, y2);

                //}
            }
            

            try
            {
                imgTemp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {

                //StreamWriter log = new StreamWriter(@"C:\\log.txt", true);

                //log.WriteLine("Exception: " + ex.Message);

                //log.Close(); 
            }
           
            imgTemp.Dispose();
            myPen.Dispose();
            graphics.Dispose();
               
            return 0;
        }


        public decimal pdcxGetPointByMberID(string ipAddress, string memberID)
        {

            try
            {
                string url = "http://"+ipAddress+"/loyaltyWebService.asmx";

                string[] args = new string[1];
                args[0] = memberID;

                object result = WebServiceHelper.InvokeWebService(url, "getPointsByMberID", args);


                //StreamWriter log = new StreamWriter(@"C:\\log.txt", true);

                //log.WriteLine("OK return result: " + Convert.ToDecimal(result));

                //log.Close(); 


                return Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {

                StreamWriter log = new StreamWriter(@"C:\\log.txt", true);

                log.WriteLine("Exception: " + ex.Message);

                log.Close(); 
            }


            return -1;

        }


    }



    class SignaturePoint
    {
        public int x;
        public int y;

        public SignaturePoint(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
