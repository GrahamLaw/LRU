using System;

namespace LRU
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            
            var cache = LRUCache.Instance;
            cache.UpdateThreshold(3);

            cache.AddToCache(1, 1);
            cache.AddToCache(2, 999);
            cache.AddToCache(3, 444);


            var got = cache.GetFromCache(1);
            Console.WriteLine("Return " + got);
        }
    }
}
