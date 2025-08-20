using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQVideoPlayer.Model
{
    public class ResultCode
    {
        //        doSignature
        //000001 - Success; 100001 - Error
        //public const string SUCC_DO_SIGNATURE = "000001";
        //public const string ERR_DO_SIGNATURE = "100001";
        public const string SUCC_DO_SIGNATURE = "0001";
        public const string ERR_DO_SIGNATURE = "2001";

        //manage
        //000002 - Success; 100002 - Error
        //public const string SUCC_MANAGE = "000002";
        //public const string ERR_MANAGE = "100002";
        public const string SUCC_MANAGE = "0002";
        public const string ERR_MANAGE = "2001";

        //payment
        //000003 - Success; 100003 - Error
        //public const string SUCC_PAYMENT = "000003";
        //public const string ERR_PAYMENT = "100003";
        public const string SUCC_PAYMENT = "0003";
        public const string ERR_PAYMENT = "2001";


        //readcard
        //000004 - Success; 100004 - Error
        //public const string SUCC_READ_CARD = "000004";
        //public const string ERR_READ_CARD = "100004";
        public const string SUCC_READ_CARD = "0004";
        public const string ERR_READ_CARD = "2001";

        //removecard
        //000005 - Success; 100005 - Error
        //public const string SUCC_REMOVE_CARD = "000005";
        //public const string REMOVE_CARD_ERR = "100005";
        public const string SUCC_REMOVE_CARD = "0005";
        public const string REMOVE_CARD_ERR = "2001";

        //inputtext
        //000006 - Success; 100006 - Error
        //public const string SUCC_INPUT_TEXT = "000006";
        //public const string ERR_INPUT_TEXT = "100006";
        public const string SUCC_INPUT_TEXT = "0006";
        public const string ERR_INPUT_TEXT = "2001";

        //reset
        //000007 - Success; 100007 - Error 
        //public const string SUCC_RESET = "000007";
        //public const string ERR_RESET = "100007";
        public const string SUCC_RESET = "0007";
        public const string ERR_RESET = "2001";

        //getvar
        //000008 - Success; 100008 - Error
        //public const string SUCC_GET_VAR = "000008";
        //public const string ERR_GET_VAR = "100008";
        public const string SUCC_GET_VAR = "0008";
        public const string ERR_GET_VAR = "2001";

        //report 
        public const string SUCC_REPORT = "0009";
        public const string ERR_REPORT = "2001";

        public const string ERR_ANY = "2001";


        public const string SUCC_SET_VAR = "0101";
        public const string ERR_SET_VAR = "2001";

        //pax error 
        //200000+
        public const string ERR_UNKNOWN_OF_PAX = "200000";
        public const string ERR_ACTION_TIMEOUT_OF_PAX = "200001";
        public const string ERR_PAYMENT_OF_PAX = "200002";
        public const string ERR_YOUREERRMSG3_OF_PAX = "200003";
        public const string ERR_YOUREERRMSG4_OF_PAX = "200004";


        public const string PAX_RESPONSE_NULL = "4"; // reponse object is null
        public const string PAX_TIME_OUT = "5"; // time out
        public const string PAX_INCORRECT_RESULT_TYPE = "6"; // No result type was specified on 


        // result code of common success 
        public const string COMM_SUCC = "0";

        public const string NOT_FOUND = "200001";
        public const string INCORRECT_FORMAT = "200002";


    }
}
