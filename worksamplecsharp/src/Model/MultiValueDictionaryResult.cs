using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionary.src.Model
{
    public class MultiValueDictionaryResult
    {
        public string Message { get; set; }

        public string OutputValue { get; set; }

        public bool IsSuccess { get; set; }

        public MultiValueDictionaryResult(string message, bool isSuccess, string outputValue = "" )
        {
            this.Message = message;
            this.OutputValue = outputValue;
            this.IsSuccess = isSuccess;
        }
    }
}
