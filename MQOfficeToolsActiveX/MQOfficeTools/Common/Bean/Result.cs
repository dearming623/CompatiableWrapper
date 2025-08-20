using System;
using System.Collections.Generic;
using System.Text;

namespace MQOfficeTools.Common.Bean
{
    public class Result<T>
    {
        public string Code { set; get; }
        public string Msg { set; get; }
        public T Data { set; get; }
    }
}
