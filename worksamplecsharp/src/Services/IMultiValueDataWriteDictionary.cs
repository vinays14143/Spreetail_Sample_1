using MultiValueDictionary.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionary.src.Services
{
    public interface IMultiValueDataWriteDictionary
    {
        public MultiValueDictionaryResult AddMemberForAKey(string key, string member);

        public MultiValueDictionaryResult RemoveAllMembersAndKey(string key);

        public MultiValueDictionaryResult RemoveMembersAndKey(string key, string members);

        public MultiValueDictionaryResult RemoveAllMembersAndKeys();

        public MultiValueDictionaryResult IfKeyExists(string key);

        public MultiValueDictionaryResult IfMemberExists(string key, string members);

    }
}
