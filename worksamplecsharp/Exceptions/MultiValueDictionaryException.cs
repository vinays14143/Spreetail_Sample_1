using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionary.Exceptions
{
    public class MultiValueDictionaryException : Exception
    {
        public MultiValueDictionaryException(string message)
        : base(message) { }
    }
}
