using System;
using System.Collections.Generic;
using System.Text;
using MoleQ.Support.Wrapper.Common.Bean;

namespace MQPLUAIWrapper.Models
{
    public class WrapperResultCode:ResultCode
    {
        //public static string SUCC_INIT = "10010";
        //public static string FAIL_INIT = "10011";

        //public static string SUCC_RELEASE = "10020";
        //public static string FAIL_RELEASE = "10021";


        //public static string SUCC_LEARN = "10030";
        //public static string FAIL_LEARN = "10031";

        //public static string SUCC_IDENTIFY = "10040";
        //public static string FAIL_IDENTIFY = "10041";


        public static string SUCC_INIT = "0";
        public static string FAIL_INIT = "10002";

        public static string SUCC_RELEASE = "0";
        public static string FAIL_RELEASE = "10003";


        public static string SUCC_LEARN = "0";
        public static string FAIL_LEARN = "10004";

        public static string SUCC_IDENTIFY = "0";
        public static string FAIL_IDENTIFY = "10005";

        //异常情况代码
        public static string ERR_EXCEPTION = "20000";
        public static string DEBUG_MESSAGE = "99999";

    }
}
