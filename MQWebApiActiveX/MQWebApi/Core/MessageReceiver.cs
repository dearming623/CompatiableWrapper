using System;
using System.Collections.Generic;
using System.Text;
using NamedPipeServer;
using System.Threading;

namespace MQWebApi.Core
{
    class MessageReceiver
    {

        class DataReceiverActionImpl : DataReceiverAction
        {
            private string _message;
            private Action<string> _action;
            public DataReceiverActionImpl(string message, Action<string> action)
            {
                _message = message;
                _action = action;
            }
            public void OnProcess()
            {
                //Console.WriteLine(string.Format("用[{0}]执行任务 ",_message));
                //Thread.Sleep(1000); // 模拟任务耗时
                if (_action != null)
                {
                    _action.Invoke(_message);
                }
            }
        }


        public void BindFolder(string folderPath)
        {
            _folder = folderPath;
        }

        private string _folder;
        private DataReceiver dataReceiver = null;
        private TaskQueue queue = new TaskQueue();
        public Action<string> MessageReceive;
        public void OnStart()
        {
            queue.Start();

            dataReceiver = new DataReceiver(_folder);
            dataReceiver.DataReceive = OnDataReceive;
            dataReceiver.Start();
            Console.WriteLine("Service started.");
        }

        private void OnDataReceive(string message)
        {
            queue.Enqueue(new DataReceiverActionImpl(message, MessageReceive));
        }

        public void OnStop()
        {
            if (dataReceiver != null)
            {
                dataReceiver.Stop();
            }
            queue.Stop();
            Console.WriteLine("Service stoped.");
        }

         
    }
}
