using System;
using System.Collections.Generic;
using System.Text;

namespace MoleQ.Support.Wrapper.Common.Bean
{
    public class CommonResult<T> :BaseObject
    {
        public string Code { set; get; }
        public string Msg { set; get; }
        public T Data { set; get; }
    }
}
