using MultiValueDictionary.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionary.src.Services
{
    public interface IMultiValueDataReadDictionary
    {
        public MultiValueDictionaryResult GetAllKeys();

        public MultiValueDictionaryResult GetAllMembersForAKey(string key);

        public MultiValueDictionaryResult GetAllMembers();

        public MultiValueDictionaryResult GetAllKeysWithMembers();
    }
}
