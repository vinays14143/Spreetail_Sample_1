using MultiValueDictionary.src.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionary.src.Model
{
    public class Validate
    {
        public bool IsValid { get; set; }

        public MultiValueDictionaryCommand Command {get;set;}

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
