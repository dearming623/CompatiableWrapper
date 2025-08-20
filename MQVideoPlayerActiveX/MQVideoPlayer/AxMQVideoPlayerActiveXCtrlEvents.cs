using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MQVideoPlayer
{
    [Guid("39340FAD-D0EA-4c25-81E8-B6B1347A65E1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface AxMQVideoPlayerActiveXCtrlEvents
    {
        #region Events

        [DispId(1)]
        void OnResponse(string result);

        #endregion
    }
}
