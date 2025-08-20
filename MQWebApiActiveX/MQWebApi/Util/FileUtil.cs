using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MQWebApi.Util
{
    public class FileUtil
    { 

        // 自定义函数：将字符串写入文本文件
        public static void WriteStringToFile(string content, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("写入文件时出错： " + ex.Message);
            }
        }

        public static bool IsValidPath(string path)
        {
            try
            {
                // Check if the path is rooted (absolute)
                if (!Path.IsPathRooted(path))
                    return false;

                // Check for invalid characters in the path
                foreach (char invalidChar in Path.GetInvalidPathChars())
                {
                    if (path.Contains(invalidChar.ToString()))
                        return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
