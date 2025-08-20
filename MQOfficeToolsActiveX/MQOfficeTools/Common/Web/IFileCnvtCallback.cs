using System;
using System.Collections.Generic;
using System.Text;

namespace MQOfficeTools.Common.Web
{
    public interface IFileCnvtCallback
    {
        void FileConvertError(string errorMessage);

        void FileConvertSuccess(string result);
    }
}
