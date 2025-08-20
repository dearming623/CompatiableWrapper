using System;
using System.Collections.Generic;
using System.IO; 
using System.Text;
using System.Text.RegularExpressions;

namespace NamedPipeServer
{
    public class DataReceiver
    {

        private string _folder = string.Empty;
        private FileSystemWatcher watcher = null;
        public Action<string> DataReceive;
        public DataReceiver(string folder)
        {
            _folder = folder;
        }

        public void Start()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //string path = @"C:\path\to\your\directory"; // 要监控的目录路径
            //string path = @"D:\1";

            if (string.IsNullOrEmpty(_folder))
            {
                throw new ArgumentException("The '" + _folder + "'" + " is not null or empty.");
            }

            if (!IsValidFolderPath(_folder))
            {
                throw new ArgumentException("The '" + _folder + "'" + " is invaild folder.");
            }

            ProcessCacheFiles(_folder);

            ClearFolder(_folder);

            watcher = new FileSystemWatcher(_folder);

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.IncludeSubdirectories = true; // 设置为true以监控子目录

            // 添加事件处理程序
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // 开始监控
            watcher.EnableRaisingEvents = true;

            // 等待用户退出
            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;


        }

        private void ProcessCacheFiles(string folderPath)
        {
            Directory.CreateDirectory(folderPath); // 确保文件夹存在

            // 正则匹配符合格式的 JSON 文件
            Regex filePattern = new Regex(@"^\d{8}_\d{6}_[a-zA-Z0-9]{6}\.json$");

            // 获取所有符合格式的 JSON 文件并按文件名升序排序
            string[] allFiles = Directory.GetFiles(folderPath, "*.json");
            List<string> jsonFiles = new List<string>();

            foreach (string filePath in allFiles)
            {
                string fileName = Path.GetFileName(filePath);
                if (filePattern.IsMatch(fileName))
                {
                    jsonFiles.Add(fileName);
                }
            }

            // 按文件名升序排序
            jsonFiles.Sort();

            Console.WriteLine("读取符合格式的 JSON 文件：");

            foreach (var fileName in jsonFiles)
            {
                string fullPath = Path.Combine(folderPath, fileName);
                try
                {
                    string content = File.ReadAllText(fullPath);
                    OnDataReceive(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("读取失败：{0} - {1}",fileName,ex.Message)  );
                }
            }  
        }

        private void ClearFolder(string folderPath)
        {
            // 清空文件夹内容
            Console.WriteLine("\n清空文件夹内容...");
            foreach (var file in Directory.GetFiles(folderPath))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("删除失败：{0} - {1}",file,ex.Message));
                }
            }
        }

        public void Stop()
        {
            if (watcher != null)
            {
                // 释放资源
                watcher.Dispose();
                watcher = null;
            }

        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine($"OnCreated File: {e.FullPath} {e.ChangeType}");
        }

        // 文件或目录改变时触发
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine($"OnChanged File: {e.FullPath} {e.ChangeType}");
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine($"OnDeleted File: {e.FullPath} {e.ChangeType}");
        }


        // 文件或目录重命名时触发
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            //Console.WriteLine($"OnRenamed File: {e.OldFullPath} renamed to {e.FullPath}");

            var fullPath = e.FullPath;
            // 判断是否为 .json 文件
            if (Path.GetExtension(fullPath).Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    // 读取文件内容
                    string content = File.ReadAllText(fullPath);

                    OnDataReceive(content);
                    // 打印内容
                    Console.WriteLine("文件内容如下：");
                    Console.WriteLine(content);

                    // 删除文件
                    File.Delete(fullPath);
                    Console.WriteLine("文件已成功删除。");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"处理文件时发生错误：{ex.Message}");
                    Console.WriteLine(string.Format("处理文件时发生错误：{0}",ex.Message));
                }
            }
            else
            {
                Console.WriteLine("该文件不是 JSON 格式。");
            }
        }

        private void OnDataReceive(string content)
        {
            Console.WriteLine("================= 已经接收数据，实现逻辑 ==========================");
            Console.WriteLine(content);
            Console.WriteLine("===========================================");

            if (DataReceive != null)
            {
                DataReceive.Invoke(content);
            }
        }

        private bool IsValidFolderPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            char[] invalidChars = Path.GetInvalidPathChars();

            foreach (char c in path)
            {
                foreach (char invalid in invalidChars)
                {
                    if (c == invalid)
                        return false;
                }
            }

            // 可选：检查路径格式是否合理（例如是否包含至少一个反斜杠）
            if (!path.Contains("\\"))
                return false;

            return true;
        }

    }
}
