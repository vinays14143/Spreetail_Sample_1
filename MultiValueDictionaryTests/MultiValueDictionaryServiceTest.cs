using MultiValueDictionary.src.Services;
using NUnit.Framework;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MultiValueDictionaryTests
{
    public class MultiValueDictionaryServiceTest
    {
        private MultiValueReadWriteDictionaryService _multiValueReadWriteDictionary = MultiValueReadWriteDictionaryService.Instance;
        private Dictionary<string, string> _testData = new Dictionary<string, string>();
        
        [SetUp]
        public void Setup()
        {
            AddData();
        }

        [Test]
        public void GetAllKeys_From_Dictionary()
        {
            var result = _multiValueReadWriteDictionary.GetAllKeys();
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.OutputValue.Split("\n").ToList().Count, _testData.Count);
        }

        [Test]
        public void AddKeys_To_Dictionary()
        {
            var result = _multiValueReadWriteDictionary.AddMemberForAKey("spreetrail","test");
            var keys = _multiValueReadWriteDictionary.GetAllKeys();
            Assert.IsTrue(result.IsSuccess);
            Assert.Contains("spreetrail", keys.OutputValue.Split('\n'));
        }

        [Test]
        public void AddKeys_Return_False_If_Member_Already_Exists()
        {
            var result = _multiValueReadWriteDictionary.AddMemberForAKey("foo", "bar");
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Remove_Memebers_Returns_False_If_Member_Exists_For_A_Key()
        {
            var result = _multiValueReadWriteDictionary.RemoveMembersAndKey("foo", "baz");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"ERROR, memeber baz does not exists", result.Message);

        }

        [Test]
        public void Remove_Memebers_Removes_LastMember_With_Key()
        {
            var result = _multiValueReadWriteDictionary.RemoveMembersAndKey("foo", "bar");
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"Last member and its key is removed", result.Message);
            var keys = _multiValueReadWriteDictionary.GetAllKeys();
            Assert.IsFalse(keys.OutputValue.Split('\n').Contains("foo"));
        }

        [TearDown]
        public void Test_Tear_Down()
        {
            _multiValueReadWriteDictionary.RemoveAllMembersAndKeys();
            _testData.Clear();
        }
        private void AddData()
        {
            
            _testData.TryAdd("foo", "bar");
            _testData.TryAdd("boom", "pow");
            _testData.TryAdd("bang", "baz");
            foreach(var item in _testData)
            {
                _multiValueReadWriteDictionary.AddMemberForAKey(item.Key, item.Value);
            }
        }
    }
}