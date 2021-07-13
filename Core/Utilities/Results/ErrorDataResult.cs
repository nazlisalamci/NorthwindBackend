using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
   public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(string message, T data) : base(false, message, data)
        {

        }
        public ErrorDataResult(T data) : base(false, data)
        {

        }
       

    }
}
