using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using MoleQ.Support.Wrapper.Common.Util;
using MoleQ.Support.Wrapper.Extension.YuanMang;

namespace MoleQ.Support.Wrapper.Extension
{
    /// <summary>
    /// 注意，此COM的工程特殊情况。需要引用第三方的DLL注意摆放位置
    /// 如下两种处理方法
    /// 1.  必须配置环境变量指向DLL的文件路径
    /// 2.  直接把DLL复制到C:\Windows\SysWOW64
    /// 
    /// 已经经过测试
    /// 首先DLL的引用关联当前工程目录下面的。
    /// </summary>
    public class NativeApi
    {
        private const string TAG =  "NativeApi";

        static List<YMResult> REUSLT_CODE_MAP = new List<YMResult>();

        //private const string NativeDllName = "WmAceKG-x86.dll";
        private const string NativeDllName = "WmAceKGV2-x86.dll";
        /// <summary>
        /// Preload the NativeApi DLL
        /// </summary>
        static NativeApi()
        {
            InitResultCodeInfo();

            // Check the size of the pointer
            String folder = IntPtr.Size == 8 ? "x64" : "x86";
            // Build the full library file name
            string location = Path.GetDirectoryName(typeof(NativeApi).Assembly.Location);
            String libraryFile = Path.Combine(location, folder);
            libraryFile = Path.Combine(libraryFile, NativeDllName);
            // Load the library
            var res = LoadLibrary(libraryFile);
            if (res == IntPtr.Zero)
            {
                //throw new InvalidOperationException("Failed to load the library.");
                Console.WriteLine("Failed to load the library.");
                //Logger.debug(TAG, string.Format("Failed to load the library. ({0})", libraryFile));
            }
            System.Diagnostics.Debug.WriteLine(libraryFile); 
        }

        /// <summary>
        /// 元芒返回错误代码结果列表  可以查看Windows端 DLL 接口说明文档 最后版本为v1.0.8
        /// </summary>
        private static void InitResultCodeInfo()
        {
            if (REUSLT_CODE_MAP != null)
            {
                REUSLT_CODE_MAP.Clear();
            }

            REUSLT_CODE_MAP.Add(new YMResult() { Code = 0, Memo = "成功", Solution = "无" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2001, Memo = "验证失败", Solution = "检查是否成功调用InitPos函数" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2002, Memo = "读取图片失败", Solution = "联系运维人员" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2003, Memo = "摄像头打开失败", Solution = "检查摄像头连接是否正常，系统自带相机是否能打开" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2004, Memo = "商品记录未找到", Solution = "检查是否成功调用AutoDetect函数" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2005, Memo = "sessionId未找到", Solution = "检查是否成功调用AutoDetect函数" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2006, Memo = "初始化pos失败", Solution = "检查执行目录是否有dev.ini" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2007, Memo = "打开文件失败", Solution = "检查传入传入路径是否正确" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2008, Memo = "保存坐标,像素长宽失败", Solution = "默认像素为640*480 请确认x+w <=640 y+h<=480" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2009, Memo = "没有设置摄像头剪辑坐标", Solution = "请使用SaveScaleSetting函数保存下坐标" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -2010, Memo = "摄像头尚未加载完毕", Solution = "摄像头加载根据硬件不同速度也不同,请在SetCameraId后 等待1秒在调用该函数" });

            REUSLT_CODE_MAP.Add(new YMResult() { Code = -4001, Memo = "snCode没有找到", Solution = "snCode没有找到" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -4002, Memo = "绑定pos机失败", Solution = "该posCode已经绑定过,并且绑定的mac地址和传入的mac地址不一致" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -4003, Memo = "绑定pos机失败", Solution = "此POS的MAC地址 绑定过其它POS机，请联系管理员确认POS机编号" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -4004, Memo = "租户没找到", Solution = "tenantCode没有找到" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -4005, Memo = "snCode没有绑定", Solution = "snCode并未绑定无需解绑" });
            REUSLT_CODE_MAP.Add(new YMResult() { Code = -4006, Memo = "解绑失败", Solution = "该pos现在的mac地址和服务器记录的mac地址不一致 无法解绑" });

        }

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = false)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        //private static readonly string LibsPath = Path.Combine(Environment.CurrentDirectory, "libs") + Path.DirectorySeparatorChar + "WmAceKG-x86.dll";
        //private const string NativeLibrary = LibsPath;
        //private const string NativeLibrary = "WmAceKG.dll";
        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int Init();

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int AutoDetect(StringBuilder productcode, StringBuilder sessionId);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int DemoDetect(byte[] imagePath, StringBuilder productcode, StringBuilder sessionId);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int SetFeedBack(byte[] code, StringBuilder sessionId, bool hit, byte[] productName);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int SetNoRecommend(byte[] code);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int InitPos(StringBuilder tenant, StringBuilder posCode, StringBuilder snNo);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int GetScaleBitmap(StringBuilder path1, StringBuilder path2);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int SetCameraId(int num);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int SaveScaleSetting(int x, int y, int width, int height);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int GetScaleSetting(ref int x, ref int y, ref int width, ref int height);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int ExportData(StringBuilder path);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int ImportData(StringBuilder path);

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int Close();

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int Test50000();

        [DllImport(NativeDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public unsafe extern static int UnBindPos(StringBuilder tenant, StringBuilder snNo);



        public static int SUCC = 0;


        public static string ParserResultCode(int resultCode)
        {
            YMResult result = REUSLT_CODE_MAP.Find(res => res.Code == resultCode);
            if (result == null)
            {
                return string.Format("未知错误, 错误代码：{0}", resultCode);
            }
            return string.Format("描述：{0}  解决方案：{1}", result.Memo, result.Solution);
        }
    }
}
