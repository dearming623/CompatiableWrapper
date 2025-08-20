using System;
using System.Collections.Generic;
using System.Text;
using MQOfficeTools.Common.Bean;

namespace MQOfficeTools.Models
{
    public class Response : BaseObject
    {
        public string ResultCode { get; set; }
        public string Message { get; set; }

        //public List<Product> Products { get; set; }
        public Products Products { get; set; }

     
    }
}
