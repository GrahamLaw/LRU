using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRU.Tests
{
    [TestClass()]
    public class LRUCacheTests
    {


        [TestMethod()]
        public void LRUCacheAddGetTest()
        {
            var cache = LRUCache.Instance;
            cache.UpdateThreshold(3);

            cache.AddToCache(1, 999);
            
            var result = cache.GetFromCache(1);

            Assert.IsTrue(cache.CacheLength() == 1);
            Assert.IsTrue(result == 999);
        }

        [TestMethod()]
        public void LRUCacheGetNonExisitingItemTest()
        {
            var cache = LRUCache.Instance;
            cache.UpdateThreshold(3);
            cache.AddToCache(1, 999);

            var result = cache.GetFromCache(5);

            Assert.IsTrue(result == -1);
        }

        [TestMethod()]
        public void LRUCacheGetEmptyTest()
        {
            var cache = LRUCache.Instance;
            cache.UpdateThreshold(3);

            Assert.IsTrue(cache.GetFromCache(999) == -1);
        }

        [TestMethod()]
        public void LRUCacheCapacityTest()
        {
            var cache = LRUCache.Instance;
            cache.UpdateThreshold(2);

            cache.AddToCache(1, 999);
            cache.AddToCache(888, 2);            

            // Over cap so 999 should be removed
            cache.AddToCache(3, 777);
            
            Assert.IsTrue(cache.CacheLength() == 2);
            Assert.IsTrue(cache.GetFromCache(5) == -1);
        }


        [TestMethod()]
        public void LRUCacheMultipleActionTest()
        {
            var cache = LRUCache.Instance;
            cache.UpdateThreshold(5);

            cache.AddToCache(1, 999);
            cache.AddToCache(888, 2);
            cache.AddToCache(3, 777);
            cache.GetFromCache(888);
            cache.AddToCache(666, 4);
            cache.GetFromCache(3);
            cache.AddToCache(5, 555);
            cache.GetFromCache(5);
            cache.GetFromCache(1);

            // Over cap so 888 should be removed as current least used
            cache.AddToCache(6, 123);

            Assert.IsTrue(cache.CacheLength() == 5);
            Assert.IsTrue(cache.GetFromCache(888) == -1);
        }

        [TestMethod()]
        public void LRUCacheSingletonTest()
        {
            var cache = LRUCache.Instance;

            var cache2 = LRUCache.Instance;

            Assert.IsTrue(Object.ReferenceEquals(cache, cache2));
        }
    }
}