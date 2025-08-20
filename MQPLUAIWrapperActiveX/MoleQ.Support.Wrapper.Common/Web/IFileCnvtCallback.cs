using System;
using System.Collections.Generic;
using System.Text;

namespace MoleQ.Support.Wrapper.Common.Web
{
    public interface IFileCnvtCallback
    {
        void FileConvertError(string errorMessage);

        void FileConvertSuccess(string result);
    }
}
