using System;
using System.Collections.Generic;
using System.Text;

namespace MQOfficeTools.Models
{
    public class MQCentralItem
    {

        public string Gateway { get; set; }

        //public string LocalImagePath { get; set; }

        public Request Request { get; set; }

        public Response Response { get; set; }
    }
}
