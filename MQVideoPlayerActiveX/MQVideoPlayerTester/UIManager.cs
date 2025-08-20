using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MQPaxWrapperTester
{
    class UIManager
    {
        public void RunOnUiThread(Control ctrl, Action actionDelegate)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(actionDelegate);
            }
            else
            {
                actionDelegate();
            }
        }
    }
}
