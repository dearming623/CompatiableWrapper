using System;
using System.Collections.Generic;
using System.Text;

namespace MQPLUAIWrapper.Models
{
    public class Request
    {
       
        public string AccountID { get; set; }
        public string Password { get; set; }
        public string SearchType { get; set; }
        public string SearchContent { get; set; }


        private string _NumOfItem = "10"; //默认显示10个item
        public string NumOfItem {

            get
            {
                return _NumOfItem;
            }

            set
            {
                string v = value.Trim().Replace(" ", "");

                int res = 10;
                if (int.TryParse(v,out res))
                {

                }
                else
                {
                    res = 10;
                }

                _NumOfItem = Convert.ToString(res);
            }
        }
       
    }
}
