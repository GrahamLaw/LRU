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
            cache.ClearCache();
            cache.UpdateThreshold(3);

            cache.AddToCache(1, 999);

            var result = cache.GetFromCache(1);

            Assert.IsTrue(cache.CacheLength() == 1);
            Assert.IsTrue(Convert.ToInt32(result) == 999);
        }

        [TestMethod()]
        public void LRUCacheGetNonExisitingItemTest()
        {
            var cache = LRUCache.Instance;
            cache.ClearCache();
            cache.UpdateThreshold(3);
            cache.AddToCache(1, 999);

            var result = cache.GetFromCache(5);

            // Assert.IsTrue(Convert.ToInt32(result) == -1);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void LRUCacheGetEmptyTest()
        {
            var cache = LRUCache.Instance;
            cache.ClearCache();
            cache.UpdateThreshold(3);

            var result = cache.GetFromCache(999);
            // Assert.IsTrue(Convert.ToInt32(result)  == -1);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void LRUCacheCapacityTest()
        {
            var cache = LRUCache.Instance;
            cache.ClearCache();
            cache.UpdateThreshold(2);

            cache.AddToCache(1, 999);
            cache.AddToCache(888, 2);

            // Over cap so 999 should be removed
            cache.AddToCache(3, 777);

            Assert.IsTrue(cache.CacheLength() == 2);

            var result = cache.GetFromCache(999);
            //Assert.IsTrue(Convert.ToInt32(result) == -1);
            Assert.IsNull(result);
        }


        [TestMethod()]
        public void LRUCacheMultipleActionTest()
        {
            var cache = LRUCache.Instance;
            cache.ClearCache();
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

            var result = cache.GetFromCache(888);
            // Assert.IsTrue(Convert.ToInt32(result) == -1);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void LRUCacheSingletonTest()
        {
            var cache = LRUCache.Instance;

            var cache2 = LRUCache.Instance;

            Assert.IsTrue(Object.ReferenceEquals(cache, cache2));
        }

        [TestMethod()]
        public void LRUCacheChangeCapacityTest()
        {
            var cache = LRUCache.Instance;
            cache.ClearCache();      

            cache.UpdateThreshold(20);
            // should be set to 20
            Assert.IsTrue(cache.CurrentThreshold() == 20);

            cache.UpdateThreshold(-10);
            // invalid number - should remain 20
            Assert.IsTrue(cache.CurrentThreshold() == 20);

        }


        [TestMethod()]
        public void LRUCacheChangeCapacityWithPopulatedCacheTest()
        {
            var cache = LRUCache.Instance;
            cache.ClearCache();
            cache.UpdateThreshold(4);

            cache.AddToCache(1, 999);
            cache.AddToCache(2, 222);
            cache.AddToCache(3, 333);
            cache.AddToCache(4, 444);

            cache.UpdateThreshold(1);

            // item 1 should have been removed due to resize
            var result = cache.GetFromCache(1);           
            Assert.IsNull(result);

            // item 3 should have been removed due to resize
            result = cache.GetFromCache(3);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void LRUCacheRemoveFlagTest()
        {
            bool flagCheck = false;
            var cache = LRUCache.Instance;
            cache.ClearCache();
            cache.UpdateThreshold(5);

            cache.AddToCache(1, 999);
            cache.AddToCache(2, 222);
            cache.AddToCache(3, 333);
            cache.AddToCache(4, 444);
            flagCheck = cache.AddToCache(5, 555);            
            Assert.IsFalse(flagCheck);

            flagCheck = cache.AddToCache(11, 1100);
            Assert.IsTrue(flagCheck);
            
        }


        //[TestMethod()]
        //public void LRUCacheAddItemWithExistingKeyTest()
        //{
        //    bool flagCheck = false;
        //    var cache = LRUCache.Instance;
        //    //cache.ClearCache();
        //    cache.UpdateThreshold(5);

        //    cache.AddToCache(1, 999);
        //    cache.AddToCache(2, 222);
        //    cache.AddToCache(1, 333);
            
            
        //    Assert.Fail();

        //}

    }
}