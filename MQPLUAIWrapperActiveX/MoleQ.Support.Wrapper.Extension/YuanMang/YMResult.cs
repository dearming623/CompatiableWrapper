using System;
using System.Collections.Generic;
using System.Text;

namespace MoleQ.Support.Wrapper.Extension.YuanMang
{
    /// <summary>
    /// 元芒返回错误代码模型
    /// </summary>
    public class YMResult
    {
        public int Code { get; set; }
        public string Memo { get; set; }
        public string Solution { get; set; }
    }
}
