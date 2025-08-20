using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;
using System.Xml;
using MQPLUAIWrapper.Models;
using System.Net;
using MoleQ.Support.Wrapper.Common.Web;
using MoleQ.Support.Wrapper.Common.Util;
using MoleQ.Support.Wrapper.Common.Bean;
using System.Reflection;
using MoleQ.Support.Wrapper.Extension;

namespace MQPLUAIWrapper
{
    [Guid("4C75A80A-BD80-4d76-9275-14C605801FEA")]
    public interface AxMQActiveXCtrl
    {
        #region Properties

        string VerInfo { get; set; }
        long VerNo { get; set; }

        #endregion

        #region Methods

        string Version();

        void SetEnableLogger(bool enable);

        /********** 单任务模式功能和防止连续调用功能 ***********/
        //void SetBlockTimeMS(int millisecond);
         
        void Identify();

        void Learn(string plu);

        void OnCreate();

        void OnDestory();

        #endregion
    }


    [Guid("430DC0D0-8C50-412a-9EE2-DC1B319526E4")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface AxMQActiveXCtrlEvents
    {
        #region Events

        [DispId(1)]
        void DataReceived(string result);

        //[DispId(1)]
        //void DataError(string errMsg);
        //[DispId(2)]
        //void DataReceived(string result);
        //[DispId(3)]
        //void ImageReceived(int code, string result);
        //[DispId(4)]
        //void FileCnvtDone(string result);
        //[DispId(5)]
        //void FileCnvtError(string error_msg);

        #endregion
    }

    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(AxMQActiveXCtrlEvents))]
    [Guid("F69283F9-5BF6-45c9-96AF-3FE08BB618E4")]
    public partial class Tools : UserControl, AxMQActiveXCtrl, IFileCnvtCallback
    {

        private const string TAG = "MQPLUAIWrapper";


        #region ActiveX Control Registration

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComRegisterFunction()]
        public static void Register(Type t)
        {
            try
            {
                ActiveXCtrlHelper.RegasmRegisterControl(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw;  // Re-throw the exception
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComUnregisterFunction()]
        public static void Unregister(Type t)
        {
            try
            {
                ActiveXCtrlHelper.RegasmUnregisterControl(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw; // Re-throw the exception
            }
        }

        #endregion

        public Tools()
        {
            InitializeComponent();

            // These functions are used to handle Tab-stops for the ActiveX 
            // control (including its child controls) when the control is 
            // hosted in a container.
            this.LostFocus += new EventHandler(CSActiveXCtrl_LostFocus);
            this.ControlAdded += new ControlEventHandler(
                CSActiveXCtrl_ControlAdded);

            // Raise custom Load event
            this.OnCreateControl();
        }

        // This event will hook up the necessary handlers
        void CSActiveXCtrl_ControlAdded(object sender, ControlEventArgs e)
        {
            // Register tab handler and focus-related event handlers for 
            // the control and its child controls.
            ActiveXCtrlHelper.WireUpHandlers(e.Control, ValidationHandler);
        }

        // Ensures that the Validating and Validated events fire properly
        internal void ValidationHandler(object sender, System.EventArgs e)
        {
            if (this.ContainsFocus) return;

            this.OnLeave(e); // Raise Leave event

            if (this.CausesValidation)
            {
                CancelEventArgs validationArgs = new CancelEventArgs();
                this.OnValidating(validationArgs);

                if (validationArgs.Cancel && this.ActiveControl != null)
                    this.ActiveControl.Focus();
                else
                    this.OnValidated(e); // Raise Validated event
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand,
            Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_SETFOCUS = 0x7;
            const int WM_PARENTNOTIFY = 0x210;
            const int WM_DESTROY = 0x2;
            const int WM_LBUTTONDOWN = 0x201;
            const int WM_RBUTTONDOWN = 0x204;

            if (m.Msg == WM_SETFOCUS)
            {
                // Raise Enter event
                this.OnEnter(System.EventArgs.Empty);
            }
            else if (m.Msg == WM_PARENTNOTIFY && (
                m.WParam.ToInt32() == WM_LBUTTONDOWN ||
                m.WParam.ToInt32() == WM_RBUTTONDOWN))
            {
                if (!this.ContainsFocus)
                {
                    // Raise Enter event
                    this.OnEnter(System.EventArgs.Empty);
                }
            }
            else if (m.Msg == WM_DESTROY &&
                !this.IsDisposed && !this.Disposing)
            {
                // Used to ensure the cleanup of the control
                this.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            base.WndProc(ref m);
        }

        // Ensures that tabbing across the container and the .NET controls
        // works as expected
        void CSActiveXCtrl_LostFocus(object sender, EventArgs e)
        {
            ActiveXCtrlHelper.HandleFocus(this);
        }

        #region Properties

        public string VerInfo { get; set; }
        public long VerNo { get; set; }

        #endregion

        #region Methods

        ProcessingForm processingDialog = null;

        public void disposeProcessingForm()
        {
            if (processingDialog != null)
            {
                processingDialog.Dispose();
            }
        }

        private void OnEventDataReceived(string msg)
        {
            if (null != DataReceived)
                DataReceived(msg);

            //locked = false;
        }

        //private void OnEventDataError(string msg)
        //{
        //    if (null != DataError)
        //        DataError(msg);
        //}

        //private void OnEventImageReceived(int code, string msg)
        //{
        //    if (null != ImageReceived)
        //        ImageReceived(code, msg);
        //}

        //private void OnEventFileCnvtDone(string result)
        //{
        //    if (null != FileCnvtDone)
        //        FileCnvtDone(result);
        //}

        //private void OnEventFileCnvtError(string error_msg)
        //{
        //    if (null != FileCnvtError)
        //        FileCnvtError(error_msg);
        //}

        /********** 单任务模式功能和防止连续调用功能 ***********/
        //private int BLOCK_TIME_MS = 1500;
        //private System.Timers.Timer _blockTimer = null;
        //bool locked = false;
        /******************************************************/

        private MyBrain mBrain = new MyBrain();

        public string Version()
        {
            string AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return String.Format("Version {0}", AssemblyVersion);
        }

        /// <summary>
        /// 打开/关闭 日志功能
        /// </summary>
        /// <param name="enable"></param>
        public void SetEnableLogger(bool enable)
        {
            Logger.SetEnableLogger(enable);
        }

        /********** 单任务模式功能和防止连续调用功能 ***********/
        //public void SetBlockTimeMS(int millisecond)
        //{
        //    BLOCK_TIME_MS = millisecond;
        //}

        public void Identify()
        {  
            //if (locked)
            //{
            //    return;
            //}
            //locked = true;

            /********** 单任务模式功能和防止连续调用功能 ***********/
            //_blockTimer.Start();

            //if (IsBusy())
            //{
            //    return;
            //}

            if (mBrain != null)
            {
                mBrain.AsynIdentify();
            }
            else
            {
                NotifyError("The object is not initialized.");
            }
        }

        //学习
        public void Learn(string plu)
        {
            //if (SpecialUtil.IsFastClick())
            //{
            //    return;
            //}

            //if (locked)
            //{
            //    return;
            //}
            //locked = true;

            /********** 单任务模式功能和防止连续调用功能 ***********/
            //_blockTimer.Start();

            //if (IsBusy())
            //{
            //    return;
            //}

            if (mBrain != null)
            {
                mBrain.AsynLearn(plu);
            }
            else
            {
                NotifyError("The object is not initialized.");
            }
        }

        public void BrainDataReceived(string result)
        {
            /******************* 测试用 *********************/
            //StringBuilder sb = new StringBuilder();
            ////获取当前进程的完整路径，包含文件名(进程名)。
            //sb.AppendFormat("(1) 获取当前进程的完整路径: \r\n{0}\r\n\r\n", this.GetType().Assembly.Location);
            ////获取新的 Process 组件并将其与当前活动的进程关联的主模块的完整路径，包含文件名(进程名)。
            //sb.AppendFormat("(2) 获取新的 Process 组件并将其与当前活动的进程关联的主模块的完整路径 :\r\n{0}\r\n\r\n", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            ////获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            //sb.AppendFormat("(3) 获取和设置当前目录（即该进程从中启动的目录）的完全限定路径 :\r\n{0}\r\n\r\n", System.Environment.CurrentDirectory);
            ////获取当前 Thread 的当前应用程序域的基目录，它由程序集冲突解决程序用来探测程序集。
            //sb.AppendFormat("(4) 获取当前 Thread 的当前应用程序域的基目录，它由程序集冲突解决程序用来探测程序集 :\r\n{0}\r\n\r\n", System.AppDomain.CurrentDomain.BaseDirectory);
            ////获取和设置包含该应用程序的目录的名称。(推荐)
            //sb.AppendFormat("(5) 获取和设置包含该应用程序的目录的名称。(推荐) :\r\n{0}\r\n\r\n", System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            ////获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。
            //sb.AppendFormat("(6) 获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称 :\r\n{0}\r\n\r\n", System.Windows.Forms.Application.StartupPath);
            //result = string.Format("<PathDesc>\r\n{0}\r\n</PathDesc>\r\n", sb.ToString()) + result;
            /***********************************************/

            OnEventDataReceived(result);
        }

        public void AfterBrainShutdown()
        {
            if (mBrain != null)
            {
                mBrain.DataReceived -= BrainDataReceived;
                mBrain = null;
            }
        }

        public void OnCreate()
        {
            //ThreadPool.SetMinThreads(1, 1);
            //ThreadPool.SetMaxThreads(5, 5);

            /********** 单任务模式功能和防止连续调用功能 ***********/
            //if (_blockTimer == null)
            //{
            //    //设置定时间隔(毫秒为单位)
            //    int interval = BLOCK_TIME_MS;
            //    _blockTimer = new System.Timers.Timer(interval);
            //    //设置执行一次（false）还是一直执行(true)
            //    _blockTimer.AutoReset = false;
            //    //设置是否执行System.Timers.Timer.Elapsed事件
            //    _blockTimer.Enabled = true;
            //    //绑定Elapsed事件
            //    _blockTimer.Elapsed += delegate
            //      {
            //          locked = false;
            //          _blockTimer.Stop();
            //      }; 
            //}
            /******************************************************/ 

            if (mBrain == null)
            {
                mBrain = new MyBrain();
            }

            mBrain.DataReceived += BrainDataReceived;
            mBrain.AsynWakeUp();

        }

        public void OnDestory()
        {
            /********** 单任务模式功能和防止连续调用功能 ***********/
            //if (_blockTimer != null)
            //{
            //    _blockTimer.Enabled = false;
            //    _blockTimer.Stop();
            //    _blockTimer.Dispose();
            //    _blockTimer = null;
            //}
            /*****************************************************/

            if (mBrain != null)
            {
                mBrain.ShutdownEvent += AfterBrainShutdown;
                mBrain.AsynShutdown();
                //mBrain.DataReceived -= BrainDataReceived;
                //mBrain = null;
                //return;
            }
            else
            {
                OnEventDataReceived(new CommonResult<string>()
                {
                    Code = WrapperResultCode.FAIL_RELEASE,
                    Msg = string.Format("No object release."),
                    Data = string.Empty
                }.ToXML());
            }
        }

        //返回错误信息
        private void NotifyError(string errMsg)
        {
            OnEventDataReceived(new CommonResult<string>()
            {
                Code = WrapperResultCode.FAIL,
                Msg = errMsg,
                Data = string.Empty
            }.ToXML());
        }

       

        int workerThreads = 0;
        int completionPortThreads = 0;
        private bool IsBusy()
        {
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            return workerThreads > 0 ? false : true;
        }

        #endregion

        #region Events

        [ComVisible(false)]
        public delegate void DataReceivedEventHandler(string result);
        public event DataReceivedEventHandler DataReceived = null;

        //[ComVisible(false)]
        //public delegate void DataErrordEventHandler(string errMsg);
        //public event DataErrordEventHandler DataError = null;

        //[ComVisible(false)]
        //public delegate void ImageReceivedEventHandler(int code, string result);
        //public event ImageReceivedEventHandler ImageReceived = null;

        //[ComVisible(false)]
        //public delegate void FileCnvtDoneEventHandler(string result);
        //public event FileCnvtDoneEventHandler FileCnvtDone = null;

        //[ComVisible(false)]
        //public delegate void FileCnvtErrorEventHandler(string error_msg);
        //public event FileCnvtErrorEventHandler FileCnvtError = null;


        #endregion



        #region IFileCnvtCallback 成员

        void IFileCnvtCallback.FileConvertError(string errorMessage)
        {
            //OnEventFileCnvtError(errorMessage);
        }

        void IFileCnvtCallback.FileConvertSuccess(string result)
        {
            //OnEventFileCnvtDone(result);
        }

        #endregion

        public string TestCallBask()
        {
            if (null != DataReceived)
            {
                return "DataReceived is not null.";
            }
            else
            {
                return "DataReceived is null.";
            } 
        }
    }
}
