using System;
using System.Collections.Generic;
using System.Text;

namespace MoleQ.Support.Wrapper.Common.Util
{
    public class SpecialUtil
    {
        // 两次点击按钮之间的点击间隔不能少于1500毫秒
        private static int MIN_CLICK_DELAY_TIME = 1500;
        private static long lastClickTime;

        public static bool IsFastClick()
        {
            bool flag = false;
            // DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond  = System.currentTimeMillis() 
            long curClickTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond; 
            if ((curClickTime - lastClickTime) < MIN_CLICK_DELAY_TIME)
            {
                flag = true;
            }
            lastClickTime = curClickTime;
            return flag;
        }
    }
}
