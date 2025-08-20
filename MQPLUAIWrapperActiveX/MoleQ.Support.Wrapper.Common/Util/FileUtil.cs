using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MoleQ.Support.Wrapper.Common.Util;

namespace MoleQ.Support.Wrapper.Common.Util
{
    public class FileUtil
    {
        public static bool CompressJsonFile(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                object jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.None);
                File.WriteAllText(path, output);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("FileUtil", "Error compress file.\r\n" + ex.Message);
                return false;
            }
        }

        public static bool IsValidJSON(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                object jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.None);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("IsValidJSON", "Invalid json file.\r\n" + ex.Message);
                return false;
            }
        }

        public static bool ConvertTo(Encoding orgEncoding, Encoding dstEncoding, string srcPath, string destPath)
        {
            try
            {
                string txt = File.ReadAllText(srcPath, orgEncoding);
                File.WriteAllText(destPath, txt, dstEncoding);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("FileUtil", "Faild to convert encoding.\r\n" + ex.Message);
                return false;
            }
        }

        public static void BuildDir(string TrainDataPath)
        {
            if (!Directory.Exists(TrainDataPath))
            {
                Directory.CreateDirectory(TrainDataPath);
            }
        }

        public static void BuildDirInCurrentDir(string subdir)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), subdir);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static FileInfo GetLastFile(string croppedImagePath)
        {
            var directory = new DirectoryInfo(croppedImagePath);
            if (!directory.Exists)
            {
                return null;
            }
            FileInfo[] files = directory.GetFiles();
            DateTime lastWrite = DateTime.MinValue;
            FileInfo lastWritenFile = null;

            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastWrite)
                {
                    lastWrite = file.LastWriteTime;
                    lastWritenFile = file;
                }
            }
            return lastWritenFile;
        } 

        /// <summary>
        /// 根据文件头判断文件类型
        /// </summary>
        /// <param name="filePath">filePath是文件的完整路径 </param>
        /// <param name="ext">ext是文件扩展名类型( 255216是jpg; 7173是gif; 6677是BMP,13780是PNG; 7790是exe, 8297是rar )</param>
        /// <returns>返回true或false</returns>
        public static bool CheckFileExtension(string filePath, string ext)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(ext))
                return false;

            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);
                string fileClass;
                byte buffer;
                buffer = reader.ReadByte();
                fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                fileClass += buffer.ToString();
                reader.Close();
                fs.Close();
                if (fileClass == ext)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void KeepFiles(string path, int cnt)
        {
            var directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                return;
            }
            FileInfo[] files = directory.GetFiles();

            if (files.Length >= cnt)
            {
                Array.Sort(files, (x, y) => Comparer<DateTime>.Default.Compare(y.LastWriteTime, x.LastWriteTime));
                FileInfo[] Slice = new List<FileInfo>(files).GetRange(cnt, files.Length - cnt).ToArray();
                foreach (FileInfo file in Slice)
                {
                    file.Delete();
                }
            }
        }
    }
}
