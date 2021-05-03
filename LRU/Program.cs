using System;

namespace LRU
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create Cache");
            
            var cache = LRUCache.Instance;

            cache.AddToCache(1, 111);
            cache.AddToCache(2, 222);
            cache.AddToCache(3, 333);
            cache.AddToCache(4, 444);

            var result = cache.GetFromCache(1);
            Console.WriteLine("Returned " + result);

            cache.AddToCache(5, 555);
            cache.AddToCache(6, 666);
            
            result = cache.GetFromCache(5);
            Console.WriteLine("Returned " + result);
            
            cache.AddToCache(7, 777);

            result = cache.GetFromCache(4);
            Console.WriteLine("Returned " + result);

            cache.AddToCache(8, 888);
            cache.AddToCache(9, 999);

            result = cache.GetFromCache(8);
            Console.WriteLine("Returned " + result);

            cache.AddToCache(10, 1000);

            cache.AddToCache(11, 1100);
            cache.AddToCache(12, 1200);

            result = cache.GetFromCache(1);
            Console.WriteLine("Returned " + result);

            cache.AddToCache(13, 1300);


            result = cache.GetFromCache(2);
            Console.WriteLine("Returned " + result);

            Console.WriteLine("Cache count " + cache.CacheLength());

            cache.UpdateThreshold(5);

            result = cache.GetFromCache(3);
            Console.WriteLine("Returned " + result);

            Console.WriteLine("Cache count " + cache.CacheLength());


        }
    }
}
