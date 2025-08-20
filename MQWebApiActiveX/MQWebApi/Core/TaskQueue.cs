using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MQWebApi.Core; 

namespace NamedPipeServer
{
    public class TaskQueue
    {
        private Queue<WorkItem> _queue = new Queue<WorkItem>();
        //private AutoResetEvent _signal = new AutoResetEvent(false);
        private Thread _workerThread;
        private bool _running = true;

        public void Start()
        {
            _running = true;

            _workerThread = new Thread(WorkerLoop);
            _workerThread.IsBackground = true;
            _workerThread.Start();
        }

        public void Enqueue(DataReceiverAction action)
        {
            lock (_queue)
            {
                _queue.Enqueue(new WorkItem(action));
            }
            //_signal.Set(); // 唤醒工作线程
        }

        private void WorkerLoop()
        {
            while (_running)
            {
                //_signal.WaitOne(); // 等待任务信号

                WorkItem item = null;
                lock (_queue)
                {
                    if (_queue.Count > 0)
                        item = _queue.Dequeue();
                }

                if (item != null)
                {
                    try
                    {
                        item.Execute();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("任务执行异常: " + ex.Message);
                    }
                }
            }
        }

        public void Stop()
        {
            _running = false;
            //_signal.Set(); // 唤醒线程以退出
            _workerThread = null;
        }

        private class WorkItem
        {
            private DataReceiverAction _action;

            public WorkItem(DataReceiverAction action)
            {
                _action = action;
            }

            public void Execute()
            {
                _action.OnProcess();
            }
        }
    }
}
