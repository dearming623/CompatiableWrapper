using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MQPaxWrapperTester
{
    public class UIHandler<UIControl, PARAM> where UIControl : Control
    {
        public delegate void OnUpdateEventHandler(UIControl control, PARAM res);
        //public event OnUpdateEventHandler OnAfterUpdate = null;

        private UIControl component;
        private PARAM param;

        public UIHandler(UIControl component, PARAM param)
        {
            this.component = component;
            this.param = param;
        }

        private void SetControlVal(UIControl component, PARAM resp)
        {

        }

        private void SetControlVal(OnUpdateEventHandler eventHandler)
        {
            if (eventHandler != null)
            {
                eventHandler(component, param);
            }
        }

        public UIHandler<UIControl, PARAM> SetControl(UIControl component)
        {
            this.component = component;
            return this;
        }

        public UIHandler<UIControl, PARAM> SetParams(PARAM param)
        {
            this.param = param;
            return this;
        }

        public void SetControlValueBy(OnUpdateEventHandler eventHandler)
        {
            if (component == null)
            {
                throw new Exception("Control is not null.");
            }

            if (param == null)
            {
                throw new Exception("Params is not null.");
            }

            if (component.InvokeRequired)
            {
                Action<UIControl, PARAM> actionDelegate = delegate(UIControl ctrl, PARAM val)
                {
                    //SetControlVal(ctrl, resp);
                    SetControlVal(eventHandler);
                };
                component.Invoke(actionDelegate, component, param);
            }
            else
            {
                //SetControlVal(ctrl, resp);
                SetControlVal(eventHandler);
            }
        }


    }
}
