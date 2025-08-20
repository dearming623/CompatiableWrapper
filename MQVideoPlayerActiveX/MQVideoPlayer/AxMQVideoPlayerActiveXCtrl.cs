using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MQVideoPlayer
{
    [Guid("F50F1E85-7898-4cce-BA2E-F730B8FE4390")]
    public interface AxMQVideoPlayerActiveXCtrl
    {
        #region Properties

        //int sampleInt { get; set; }
        //string sampleString { get; set; } 
        //bool samplebool { get; set; } 

        #endregion

        #region Methods

        void OnPlay(string path);
        void OnStop();

        #endregion
    }
}
