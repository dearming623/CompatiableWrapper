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
using System.Collections;
using System.Reflection;
using System.Drawing.Imaging;
using MQVideoPlayer.Model;
using MQVideoPlayer.Exceptions;
using AXVLC;

namespace MQVideoPlayer
{ 

    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(AxMQVideoPlayerActiveXCtrlEvents))]
    [Guid("61F986FA-8EFB-4a03-B802-9F0DAF07DDF9")]
    public partial class DisplayControl : UserControl, AxMQVideoPlayerActiveXCtrl, CommWrapper
    {

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

        public DisplayControl()
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

        //public int errorCode { get; set; }
        //public string errorMessage { get; set; } 
        //public bool enableWrapperLog { get; set; } 
       
        #endregion

        private const string TAG = "MQVideoPlayer"; 

        #region Methods

        public void Test(string val)
        {
            MessageBox.Show("Object is OK。 i receive value: " + val);
        }

       
   
        private void ResultOf(string resultCode, string msg)
        {
            ResultOf(resultCode, msg, string.Empty);
        }

        private void ResultOf(string resultCode, string msg, string dataSet)
        {
            ReportDataReceived(new CommResponse() { Code = resultCode, Msg = msg, Data = dataSet }.ToXML());
        }

           
        public void ReportDataReceived(string res)
        {
            if (OnResponse != null)
            {
                OnResponse(res);
            }
        }

        public void ReportDataReceived(CommResponse resp)
        {
            ReportDataReceived(resp.GetCode());
        }

        public void OnPlay(string path)
        {
            //ReportDataReceived("OK");
            if (!File.Exists(path))
            {
                ResultOf(ResultCode.NOT_FOUND, "'" + path + "' not found.");
                return;
            }

            FileInfo fileInfo = new FileInfo(path);
            var correctMp4 = fileInfo.Extension.ToLower() == ".mp4" && fileInfo.Length > 0;
            if (!correctMp4)
            {
                ResultOf(ResultCode.INCORRECT_FORMAT, "Incorrect format.");
                return;
            }

            
            //axVLCPlugin1.playEvent +=new EventHandler(axVLCPlugin1_playEvent);
            //axVLCPlugin1.pauseEvent += new EventHandler(axVLCPlugin1_playEvent2);

          
            try
            {
                axVLCPlugin1.stop();
                axVLCPlugin1.playlistClear();
                object options = new string[2] { ":input-repeat=9999", ":no-audio" };
                axVLCPlugin1.addTarget(path, options, AXVLC.VLCPlaylistMode.VLCPlayListAppendAndGo, 0);
                axVLCPlugin1.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("axVLCPlugin1_stopEvent");
                ReportDataReceived(ex.Message);
            }
            
        }

        public void OnStop()
        {

            axVLCPlugin1.stop();
            axVLCPlugin1.playlistClear();
            //ReportDataReceived("OnStop");
        }
 
        #endregion


        #region Events 
         
        [ComVisible(false)]
        public delegate void OnResponseEventHandler(string res);
        public event OnResponseEventHandler OnResponse = null;

        #endregion

        private void axVLCPlugin1_playEvent(object sender, EventArgs e)
        {
            Console.WriteLine("aaaaaaaaaaaaaaaaa");
            ReportDataReceived("axVLCPlugin1_playEvent");
        }

        private void axVLCPlugin1_pauseEvent(object sender, EventArgs e)
        {
            Console.WriteLine("axVLCPlugin1_pauseEvent");
            ReportDataReceived("pauseEvent");
        }

        private void axVLCPlugin1_Enter(object sender, EventArgs e)
        {
            Console.WriteLine("axVLCPlugin1_Enter");
        }

        private void axVLCPlugin1_stopEvent(object sender, EventArgs e)
        {
            Console.WriteLine("axVLCPlugin1_stopEvent");
        }


       
    }
}
