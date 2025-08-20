using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;
using MoleQ.Support.Wrapper.Common.Util;
using MoleQ.Support.Wrapper.Common.Bean;
using MoleQ.Support.Wrapper.Extension.YuanMang;
using MoleQ.Support.Wrapper.Extension;
using MQPLUAIWrapper.Models;

namespace MQPLUAIWrapper
{
    public class MyBrain
    {

        private const string TAG = "MyBrain";

        //private string sessionId = "";
        private HashSet<string> productCodeMap = new HashSet<string>();

        //保留5张图片文件
        private int KeepFileCount = 1;

        //临时保存文件夹名称
        private string TemporaryImageFolderName = "PluPic";

        //是否使用备份功能
        private bool IsBackupTrainData = false;

        //备份训练数据文件夹名称
        private string BackupTrainDataFolderName = "train-data";

        public delegate void DataReceivedEventHandler(string result);
        public event DataReceivedEventHandler DataReceived = null;

        public delegate void ShutdownEventHandler();
        public event ShutdownEventHandler ShutdownEvent = null;

        private bool isInit = false;


        public string tenant = "MoleQ";
        public string posCode = "MoleQ_Ming";
        public string snNo = "76BC1F00-FDF4-45B7-A8D4-52D397FCB3D3";


        private IdentifyResult AutoIdentify()
        {
            IdentifyResult identifyResult = null;
            Logger.Debug(TAG, string.Format("{0}::AutoIdentify -> Start", TAG));
            Stopwatch stopwatch = new Stopwatch();
            StringBuilder productCodes = new StringBuilder(500);
            StringBuilder sessionId = new StringBuilder(200);
            stopwatch.Start();
            int res = -1;
            try
            {
                Logger.Debug(TAG, string.Format("{0}::AutoIdentify -> Prepare to call a third-party function.", TAG));

                //int res = NativeApi.SetCameraId(0);
                //Logger.debug(TAG, string.Format("YuanMangAI::SetCameraId {0} [ResultCode: {1}]", NativeApi.ParserResultCode(res),res));

                res = NativeApi.AutoDetect(productCodes, sessionId);

                //由于AutoDetect连续调用存在Bug导致程序直接Crash，增加延时解决此问题
                //Thread.Sleep(1000);

                Logger.Debug(TAG, string.Format("YuanMangAI::AutoDetect {0} [ResultCode: {1}]", NativeApi.ParserResultCode(res), res));

                //res = NativeApi.Close();
                //Logger.debug(TAG, string.Format("YuanMangAI::Close {0} [ResultCode: {1}]", NativeApi.ParserResultCode(res),res));
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, string.Format("{0}::AutoIdentify -> AutoDetect \r\n{1}", TAG, ex.Message));
            }
            stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间 
            Logger.Debug(TAG, string.Format("Auto identify elapsed time: {0} sec. ", timespan.TotalMilliseconds / 1000));

            if (res == NativeApi.SUCC)
            {
                if (sessionId != null)
                {
                    Logger.Debug(TAG, string.Format("{0}::AutoIdentify -> sessionId: {1}", TAG, sessionId.ToString().Trim()));
                }

                if (productCodes != null)
                {
                    Logger.Debug(TAG, string.Format("{0}::AutoIdentify -> productCodes: {1}", TAG, productCodes.ToString().Trim()));

                    //修正返回多余的逗号
                    string productMatchCodes = productCodes.ToString().Trim();
                    if (productMatchCodes.EndsWith(","))
                    {
                        productMatchCodes = productMatchCodes.Substring(0, productMatchCodes.Length - 1);
                    }
                    identifyResult = new IdentifyResult()
                    {
                        SessionId = sessionId.ToString(),
                        MatchCodes = productMatchCodes
                    };
                }
            }

            Logger.Debug(TAG, string.Format("{0}::AutoIdentify -> Stop", TAG));
            return identifyResult;
        }

        //识别，此方法返回字符串
        private void Identify()
        {
            this.productCodeMap.Clear();

            //CommonResult<ProdInfo> response = new CommonResult<ProdInfo>();
            //response.Code = WrapperResultCode.FAIL;
            //response.Msg = "Fail to identify. Please check the log";
            //response.Data = string.Empty;

            string responseCode = WrapperResultCode.FAIL;
            string responseMsg = "Fail to identify. Please check the log";
            ProdInfo responseData = new ProdInfo();

            Logger.Info(TAG, "========== Start Identify ==========");
            var ymResult = AutoIdentify();
            if (ymResult != null)
            {
                Logger.Info(TAG, string.Format("SessionId: {0}", ymResult.SessionId));
                Logger.Info(TAG, string.Format("Result match codes: {0}", ymResult.MatchCodes));

                //获取图片位置
                string prodPicPath = GetProdPic(ymResult);
                string sessionId = ymResult.SessionId;

                //string json = $"\"data\":\"{productMatchCodes}\"";
                string matchNames = ymResult.MatchCodes.ToString().Trim();

                if (!string.IsNullOrEmpty(matchNames))
                {
                    Console.WriteLine("==============  匹配的商品代码 ==============");
                    //收集匹配的代码
                    CollectMatchCodes(matchNames);
                    responseCode = WrapperResultCode.SUCC_IDENTIFY;
                    responseMsg = "Identify successfully.";
                    responseData = new ProdInfo()
                    {
                        ProdCode = matchNames,
                        ProdPic = prodPicPath
                    };

                    Console.WriteLine("============================================");

                    if (productCodeMap.Count == 1)
                    {
                        var uniqueCode = productCodeMap.ToList()[0];
                        //Console.WriteLine($" 用 {uniqueCode} 搜索数据库item"); 
                    }
                    else
                    {
                        //Console.WriteLine($" 识别后多个结果: {json}  ");
                    }
                }
                else
                {
                    //匹配商品代码不成功
                    responseData = new ProdInfo()
                    {
                        ProdCode = "",
                        ProdPic = prodPicPath
                    };
                }
            }

            //返回结果
            //ThreadPool.QueueUserWorkItem(new WaitCallback(ResultCallback), new string[] { JSON.ToString(response) });
            //OnEventDataReceived(JSON.ToString(response));
            //OnEventDataReceived(response.ToXML());
            ResultOf<ProdInfo>(responseCode, responseMsg, responseData);

            Logger.Info(TAG, "========== End Identify ==========");
        }

        private void CollectMatchCodes(string matchNames)
        {
            string[] codes = matchNames.Split(',');

            foreach (var code in codes)
            {
                if (!string.IsNullOrEmpty(code))
                {
                    productCodeMap.Add(code);
                }
            }
        }

        public void AsynIdentify()
        {
            if (!VaildateDirectory())
            {
                return;
            }

            WaitCallback cb = new WaitCallback(delegate(object x)
            {
                try
                {
                    Identify();
                }
                catch (Exception ex)
                {
                    Logger.Error(TAG, string.Format("{0}::AsynIdentify \r\n{1}", TAG, ex.Message));
                    ResultOf(WrapperResultCode.ERR_EXCEPTION, ex.Message);
                }

            });
            ThreadPool.QueueUserWorkItem(cb);
        }

        //private void ResultCallback(object state)
        //{
        //    string[] param = (string[])state;
        //    string json = param[0];
        //    //string jsonFile = param[1];
        //    //string saveFile = param[2];
        //    Logger.Debug(TAG, string.Format("{0}::ResultCallback -> Identify return ({1})", TAG, json));

        //    OnEventDataReceived(json);
        //}

        private void OnEventDataReceived(string msg)
        {
            //Result 改为 Response
            string res = msg.Replace("<CommonResult>", "<Response>").Replace("</CommonResult>", "</Response>");
            Logger.Debug(TAG, string.Format("{0}::OnEventDataReceived -> {1}", TAG, res));
            if (null != DataReceived)
            {
                DataReceived(res);
            }
        }

        /// <summary>
        ///  赋予名称
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="uniqueCode"></param>
        /// <param name="uniqueName"></param>
        /// <param name="isHit">是否命中商品</param>
        /// <returns></returns>
        public bool AssignName(string sessionId, string uniqueCode, string uniqueName, bool isHit)
        {
            bool result = false;
            try
            {
                var code = Encoding.UTF8.GetBytes(uniqueCode);
                var sid = new StringBuilder(sessionId);
                var name = Encoding.UTF8.GetBytes(uniqueName);
                int res = NativeApi.SetFeedBack(code, sid, isHit, name);
                Logger.Debug(TAG, string.Format("YuanMangAI::SetFeedBack {0} [ResultCode: {1}]", NativeApi.ParserResultCode(res), res));
                if (res == NativeApi.SUCC)
                {
                    result = true;
                }

                //int res2 = NativeApi.Close();
                //Logger.debug(TAG, string.Format("YuanMangAI::Close Result: {0}", NativeApi.ParserResultCode(res2)));

            }
            catch (Exception ex)
            {
                Logger.Error(TAG, string.Format("{0}::AssignName \r\n{1}", TAG, ex.Message));
            }
            return result;
        }

        public bool AssignName(string sessionId, string uniqueCode, string uniqueName)
        {
            return AssignName(sessionId, uniqueCode, uniqueName, false);
        }

        //学习
        private void Learn(string plu)
        {
            if (!VaildateDirectory())
            {
                return;
            }

            string respCode = WrapperResultCode.FAIL;
            string respMsg = string.Format("Fail to learn PLU({0})", plu);
            ProdInfo respData = null;

            Logger.Info(TAG, "========== Start Learn ==========");
            var ymResult = AutoIdentify();
            if (ymResult != null)
            {
                string sessionId = ymResult.SessionId;
                string matchCodes = ymResult.MatchCodes;
                string picUrl = GetProdPic(ymResult);

                respData = new ProdInfo()
                {
                    ProdCode = matchCodes,
                    ProdPic = picUrl
                };

                Logger.Info(TAG, string.Format("SessionId: {0}", sessionId));
                Logger.Info(TAG, string.Format("Match codes: {0}", matchCodes));

                var succ = AssignName(ymResult.SessionId, plu, "");

                Logger.Info(TAG, string.Format("AssignName -> succ: {0}", succ));
                if (succ)
                {
                    if (IsBackupTrainData)
                    {
                        BackupTrainData(plu, ymResult.SessionId);
                    }

                    respCode = WrapperResultCode.SUCC_LEARN;
                    //response.Msg = string.Format("Learn PLU({0}) successfully {1}", plu, saveMsg);
                    respMsg = string.Format("Learn PLU({0}) successfully.", plu);

                }
                else
                {
                    Logger.Info(TAG, string.Format("{0}::Learn -> Fail to learn PLU({1})", TAG, plu));

                    respCode = WrapperResultCode.FAIL;
                    respMsg = string.Format("Fail to learn PLU({0})", plu);
                    //Logger.info(TAG, string.Format("{0}::Learn -> Response: {1}", TAG, JSON.ToString(response)));
                }
            }

            ResultOf<ProdInfo>(respCode, respMsg, respData);
            Logger.Info(TAG, "========== End Learn ==========");
        }

        private void BackupTrainData(string folderName, string fileNameWithoutExt)
        {
            var trainDataPath = Path.Combine(Environment.CurrentDirectory, BackupTrainDataFolderName);
            //FileUtil.BuiderDir("train-data"); 
            FileUtil.BuildDir(trainDataPath);
            var croppedImagePath = Path.Combine(Environment.CurrentDirectory, "WMPhoto");

            string saveMsg = string.Empty;
            //获取时间最新的图片
            var lastImageFileInfo = FileUtil.GetLastFile(croppedImagePath);
            if (lastImageFileInfo != null)
            {
                var pluPath = Path.Combine(trainDataPath, folderName);
                if (!Directory.Exists(pluPath))
                {
                    FileUtil.BuildDir(pluPath);
                }
                //lastImageFileInfo.Rename($"{sessionId}.jpg");
                //FileUtil.Move(lastImageFileInfo.FullName, Path.Combine(pluPath, sessionId));
                string trainPluPath = Path.Combine(pluPath, string.Format("{0}{1}", fileNameWithoutExt, lastImageFileInfo.Extension));
                File.Move(lastImageFileInfo.FullName, trainPluPath);
                saveMsg = string.Format(" \r\n It has been saved to {0}", trainPluPath);
            }

            Logger.Info(TAG, string.Format("{0}::BackupTrainData -> Backup PLU({1}) SUCC. {2}", TAG, folderName, saveMsg));
        }

        public void AsynLearn(string plu)
        {
            WaitCallback cb = new WaitCallback(delegate(object x)
            {
                try
                {
                    string param = (string)x;
                    Learn(param);
                }
                catch (Exception ex)
                {
                    Logger.Error(TAG, string.Format("{0}::AsynLearn \r\n{1}", TAG, ex.Message));
                    ResultOf(WrapperResultCode.ERR_EXCEPTION, ex.Message);
                }
            });
            ThreadPool.QueueUserWorkItem(cb, plu);
        }

        private bool VaildateDirectory()
        {
            string devFileName = "dev.ini";
            string devFilePath = Path.Combine(Environment.CurrentDirectory, devFileName);
            if (!File.Exists(devFilePath))
            {
                ResultOfFileNotFound(devFileName);
                return false;
            }

            string coordinateFileName = "coordinate.json";
            string coordinateFilePath = Path.Combine(Environment.CurrentDirectory, coordinateFileName);
            if (!File.Exists(coordinateFilePath))
            {
                ResultOfFileNotFound(coordinateFileName);
                return false;
            }

            return true;
        }

        private void ResultOfFileNotFound(string fileName)
        {
            //CommonResult<string> response = new CommonResult<string>()
            //{
            //    Code = WrapperResultCode.FILE_NOT_FOUND,
            //    Msg = string.Format("\'{0}\' file not found. Please check the execution directory.", fileName),
            //    Data = string.Empty
            //};
            //OnEventDataReceived(response.ToXML());
            ResultOf(WrapperResultCode.FILE_NOT_FOUND, string.Format("\'{0}\' file not found. Please check the execution directory.", fileName));
        }


        private void WakeUp()
        {
            //测试查看线程使用
            //int maxThreadNum, portThreadNum, minThreadNum;
            //ThreadPool.GetMaxThreads(out maxThreadNum, out portThreadNum);
            //ThreadPool.GetMaxThreads(out minThreadNum, out portThreadNum);
            //ResultOf(WrapperResultCode.DEBUG_MESSAGE, string.Format("maxThreadNum: {0} \r\nminThreadNum{1} ", maxThreadNum, minThreadNum));

            //小心设置线程数目
            //https://docs.microsoft.com/en-us/dotnet/api/system.threading.threadpool.setmaxthreads?view=netframework-2.0#system-threading-threadpool-setmaxthreads(system-int32-system-int32)
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(1, 10);

            bool initComplete = false;
            try
            {
                Logger.Debug(TAG, string.Format("{0}::WakeUp -> Invoke YuanMangAI::Init ", TAG));
                int resultCodeOfInit = NativeApi.Init();
                Logger.Debug(TAG, string.Format("YuanMangAI::Init -> {0} [ResultCode: {1}]", NativeApi.ParserResultCode(resultCodeOfInit), resultCodeOfInit));
                if (IsSuccess(resultCodeOfInit))
                {
                    isInit = true; 
                }
 
                if (isInit && 
                    IsSuccess(InitPos(this.tenant, this.posCode, this.snNo)) &&
                    IsSuccess(SetCameraId()))
                {
                    initComplete = true;
                } 

                if (initComplete)
                {
                    ResultOf(WrapperResultCode.SUCC_INIT, "Create object successfully.");
                }
                else
                {
                    ResultOf(WrapperResultCode.FAIL_INIT, "Fail to create object.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, string.Format("{0}::WakeUp -> SetCameraId \r\n{1}", TAG, ex.Message));
                ResultOf(WrapperResultCode.FAIL_INIT, ex.Message);
            }
        }

        private static int SetCameraId()
        {
            Logger.Debug(TAG, string.Format("{0}::WakeUp -> Invoke YuanMangAI::SetCameraId ", TAG));
            int res = NativeApi.SetCameraId(0);
            Logger.Debug(TAG, string.Format("YuanMangAI::SetCameraId -> {0} [ResultCode: {1}]", NativeApi.ParserResultCode(res), res));
            return res;
        }

        private int InitPos(string tenant, string posCode, string snNo)
        {
            StringBuilder sbTenant = new StringBuilder(tenant);
            StringBuilder sbPosCode = new StringBuilder(posCode);
            StringBuilder sbSnNo = new StringBuilder(snNo);
            Logger.Debug(TAG, string.Format("{0}::WakeUp -> Invoke YuanMangAI::InitPos ", TAG));
            int res = NativeApi.InitPos(sbTenant, sbPosCode, sbSnNo);
            Logger.Debug(TAG, string.Format("YuanMangAI::InitPos -> {0} [ResultCode: {1}]", NativeApi.ParserResultCode(res), res));
            return res;
        }

        private bool IsSuccess(int resultCode)
        {
            if (resultCode == NativeApi.SUCC)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AsynWakeUp()
        {
            WaitCallback cb = new WaitCallback(delegate(object x)
            {
                try
                {
                    WakeUp();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Logger.Error(TAG, string.Format("{0}::AsynWakeUp \r\n{1}", TAG, ex.Message));
                    ResultOf(WrapperResultCode.ERR_EXCEPTION, ex.Message);
                }
            });
            ThreadPool.QueueUserWorkItem(cb);
        }

        private void Shutdown()
        {
            if (!isInit)
            {
                isInit = false;
                ResultOf(WrapperResultCode.SUCC_RELEASE, "Release object successfully.");
                return;
            }

            try
            {
                Logger.Debug(TAG, string.Format("{0}::Shutdown -> Invoke YuanMangAI::Close ", TAG));
                int res = NativeApi.Close();
                Logger.Debug(TAG, string.Format("YuanMangAI::Close -> {0} [ResultCode: {1}]", NativeApi.ParserResultCode(res), res));
                if (res == NativeApi.SUCC)
                {
                    ResultOf(WrapperResultCode.SUCC_RELEASE, "Release object successfully.");
                }
                else
                {
                    ResultOf(WrapperResultCode.FAIL_RELEASE, "Fail to release object.");
                }
            }
            catch (Exception ex)
            {
                ResultOf(WrapperResultCode.FAIL_RELEASE, ex.Message);
            }


            if (null != ShutdownEvent)
            {
                ShutdownEvent();
            }
        }

        public void AsynShutdown()
        {
            WaitCallback cb = new WaitCallback(delegate(object x)
            {
                try
                {
                    Shutdown();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Logger.Error(TAG, string.Format("{0}::AsynShutdown \r\n{1}", TAG, ex.Message));
                    ResultOf(WrapperResultCode.ERR_EXCEPTION, ex.Message);
                }
            });
            ThreadPool.QueueUserWorkItem(cb);
        }

        private string GetProdPic(IdentifyResult result)
        {
            var picPath = Path.Combine(Environment.CurrentDirectory, TemporaryImageFolderName);
            FileUtil.BuildDir(picPath);
            var croppedImagePath = Path.Combine(Environment.CurrentDirectory, "WMPhoto");

            string relativePath = string.Empty;
            var lastImageFileInfo = FileUtil.GetLastFile(croppedImagePath);
            if (lastImageFileInfo != null)
            {
                string destPath = Path.Combine(picPath, string.Format("{0}{1}", result.SessionId, lastImageFileInfo.Extension));
                File.Copy(lastImageFileInfo.FullName, destPath, true);
                relativePath = destPath.Replace(Path.Combine(Environment.CurrentDirectory, ""), "");
                string slash = Path.DirectorySeparatorChar.ToString();
                if (relativePath.StartsWith(slash))
                {
                    relativePath = relativePath.Remove(0, 1);
                }
            }

            FileUtil.KeepFiles(picPath, KeepFileCount);
            return relativePath;
        }

        private void ResultOf(string code, string msg)
        {
            ResultOf<string>(code, msg, string.Empty);
        }

        private void ResultOf<T>(string code, string msg, T data)
        {
            OnEventDataReceived(new CommonResult<T>()
            {
                Code = code,
                Msg = msg,
                Data = data
            }.ToXML());
        }
    }
}
