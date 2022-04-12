using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using MultiValueDictionary.src.Model;
using MultiValueDictionary.Exceptions;

namespace MultiValueDictionary.src.Services
{
    public class MultiValueReadWriteDictionaryService : IMultiValueDataWriteDictionary, IMultiValueDataReadDictionary
    {
        private ConcurrentDictionary<string, List<string>> _readWriteDictionary;
        private static MultiValueReadWriteDictionaryService _readWriteInstance;
        private static object _lock = new object();

        private MultiValueReadWriteDictionaryService()
        {
            _readWriteDictionary = new ConcurrentDictionary<string, List<string>>();
            _readWriteInstance = null;
        }

        public static MultiValueReadWriteDictionaryService Instance
        {
            get
            {
                if(_readWriteInstance == null)
                {
                    lock(_lock)
                    {
                        _readWriteInstance = new MultiValueReadWriteDictionaryService();
                        return _readWriteInstance;
                    }
                }
                else
                {
                    return _readWriteInstance;
                }
            }
        }
        /// <summary>
        /// Adds key and Member to the dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public MultiValueDictionaryResult AddMemberForAKey(string key, string member)
        {
            try
            {
                if (_readWriteDictionary.TryGetValue(key, out var values))
                {
                    //check for existing member
                    if (values.Contains(member))
                    {
                        return new MultiValueDictionaryResult($"ERROR, Member {member} already exists for the key {key}", false);

                    }
                    else
                    {
                        values.Add(member);
                        _readWriteDictionary[key] = values;
                        return new MultiValueDictionaryResult($"Successfully added to existing key", true, $"Updated: {key}, {member}");
                    }
                }
                //if key is not found add new key and its member
                _readWriteDictionary.TryAdd(key, new List<string> { member });
                return new MultiValueDictionaryResult($"Successfully added to new key", true, $"Added: {key}, {member}");
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }

        /// <summary>
        /// Remove a key and member from dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public MultiValueDictionaryResult RemoveMembersAndKey(string key, string members)
        {
            try
            {
                if (!_readWriteDictionary.TryGetValue(key, out var values))
                {
                    return new MultiValueDictionaryResult($"ERROR, Key {key} does not exists", false);

                }
                if (!values.Contains(members))
                {
                    return new MultiValueDictionaryResult($"ERROR, memeber {members} does not exists", false);
                }
                if (values.Count == 1)
                {
                    var result = RemoveAllMembersAndKey(key);
                    result.Message = "Last member and its key is removed";
                    return result;
                }
                values.Remove(members);
                _readWriteDictionary[key] = values;
                return new MultiValueDictionaryResult($"Value is removed", true, $"Value {members} is removed for the key {key}");
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }

        /// <summary>
        /// Removing all the members for a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public  MultiValueDictionaryResult RemoveAllMembersAndKey(string key)
        {
            try
            {
                if (_readWriteDictionary.TryRemove(key, out var _))
                {
                    return new MultiValueDictionaryResult($"Removed key and its members", true, $"Key {key} is removed");
                }
                return new MultiValueDictionaryResult($"ERROR, Key does not exists", false);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }

        /// <summary>
        /// Clear all the keys and members from the dictionary 
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResult RemoveAllMembersAndKeys()
        {
            try
            {
                _readWriteDictionary.Clear();
                return new MultiValueDictionaryResult($"Removed key and its members", true, $"Clear dictionary");
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }

        /// <summary>
        /// Check if given key exists in the dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MultiValueDictionaryResult IfKeyExists(string key)
        {
            try
            {
                if (_readWriteDictionary.ContainsKey(key))
                {
                    return new MultiValueDictionaryResult($"key exists", true, $"Key {key} exists");
                }
                return new MultiValueDictionaryResult($"ERROR, key does not exists", false);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }

        /// <summary>
        /// Check if member exists for a given key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public MultiValueDictionaryResult IfMemberExists(string key, string members)
        {
            try
            {
                if (!_readWriteDictionary.TryGetValue(key, out var values))
                {
                    return new MultiValueDictionaryResult($"ERROR, key does not exists", false);
                }
                if (values.Contains(members))
                {
                    return new MultiValueDictionaryResult($"Member exists within key", true);
                }
                else
                {
                    return new MultiValueDictionaryResult($"ERROR, Member does not exists within key", false);
                }
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }

        /// <summary>
        /// Get all keys from dictionary
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResult GetAllKeys()
        {
            try
            {
                var keys = _readWriteDictionary.Keys;
                if (keys.Any())
                {
                    var result = string.Join("\n", keys);
                    return new MultiValueDictionaryResult($"all keys", true, result);
                }

                return new MultiValueDictionaryResult($"Empty Dictionary",true);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }

        }

        /// <summary>
        /// Get all the members for a given key from the dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MultiValueDictionaryResult GetAllMembersForAKey(string key)
        {
            try
            {
                if (_readWriteDictionary.TryGetValue(key, out var values))
                {
                    var result = string.Join("\n", values);
                    return new MultiValueDictionaryResult($"all members for a key {key}", true, result);

                }

                return new MultiValueDictionaryResult($"ERROR, key not found", false);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
            
        }

        /// <summary>
        /// Get all the members of a dictionary
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResult GetAllMembers()
        {
            try
            {
                var values = _readWriteDictionary.SelectMany(x => x.Value).ToList();
                if (values.Any())
                {
                    var result = string.Join("\n", _readWriteDictionary.SelectMany(x => x.Value).ToList());
                    return new MultiValueDictionaryResult($"all members", true, result);

                }

                return new MultiValueDictionaryResult($"no members", true);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }

        /// <summary>
        /// Get all the keys with its members from dictionary
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResult GetAllKeysWithMembers()
        {
            try
            {
                var result = string.Join("\n", _readWriteDictionary.SelectMany(x => x.Value.Select(r => x.Key + " : " + r)));
                
                if (!string.IsNullOrEmpty(result))
                {
                    return new MultiValueDictionaryResult($"all keys and members", true, result);
                }

                return new MultiValueDictionaryResult($"no keys and members", true);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }
    }
}