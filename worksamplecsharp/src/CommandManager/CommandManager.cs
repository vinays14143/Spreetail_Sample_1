using MultiValueDictionary.src.Enums;
using MultiValueDictionary.src.Model;
using MultiValueDictionary.src.Services;

namespace MultiValueDictionary.src.CommandManager
{
    public class CommandManager
    {
        private MultiValueReadWriteDictionaryService _multiValueReadWriteDictionary = MultiValueReadWriteDictionaryService.Instance;

        public MultiValueDictionaryResult MultiValueDictionaryOperation(MultiValueDictionaryCommand input, string key, string value)
        {
            switch(input)
            {
                case MultiValueDictionaryCommand.KEYS:
                    return _multiValueReadWriteDictionary.GetAllKeys();

                case MultiValueDictionaryCommand.MEMBERS:
                    return _multiValueReadWriteDictionary.GetAllMembersForAKey(key);

                case MultiValueDictionaryCommand.ADD:
                    return _multiValueReadWriteDictionary.AddMemberForAKey(key, value);

                case MultiValueDictionaryCommand.REMOVE:
                    return _multiValueReadWriteDictionary.RemoveMembersAndKey(key,value);

                case MultiValueDictionaryCommand.REMOVEALL:
                    return _multiValueReadWriteDictionary.RemoveAllMembersAndKey(key);

                case MultiValueDictionaryCommand.CLEAR:
                    return _multiValueReadWriteDictionary.RemoveAllMembersAndKeys();

                case MultiValueDictionaryCommand.KEYEXISTS:
                    return _multiValueReadWriteDictionary.IfKeyExists(key);

                case MultiValueDictionaryCommand.MEMBEREXISTS:
                    return _multiValueReadWriteDictionary.IfMemberExists(key,value);

                case MultiValueDictionaryCommand.ALLMEMBERS:
                    return _multiValueReadWriteDictionary.GetAllMembers();

                case MultiValueDictionaryCommand.ITEMS:
                    return _multiValueReadWriteDictionary.GetAllKeysWithMembers();
            }
            return new MultiValueDictionaryResult("Invalid operation", false);
        }
    }
}
