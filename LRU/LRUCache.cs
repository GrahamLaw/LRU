using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRU
{
    public class LRUCache
    {
        private readonly int _defaultCapcity = 10;
        private int _capacity;
        private Dictionary<int, LinkedListNode<KeyValuePair<int, int>>> _dic;
        private LinkedList<KeyValuePair<int, int>> _cache = new LinkedList<KeyValuePair<int, int>>();

        //public LRUCache(int capacity)
        //{
        //    _capacity = capacity;
        //    _dic = new Dictionary<int, LinkedListNode<KeyValuePair<int, int>>>();
        //    _cache = new LinkedList<KeyValuePair<int, int>>();
        //}

        private LRUCache()
        {
            _capacity = _defaultCapcity;
            _dic = new Dictionary<int, LinkedListNode<KeyValuePair<int, int>>>();
            _cache = new LinkedList<KeyValuePair<int, int>>();
        }

        private static readonly object _lock = new object();
        private static LRUCache _instance = null;

        /// <summary>
        /// Creates a LRU Cache with a initial capacity of 10. Change capacity using UpdateThreshold()        
        /// </summary>
        public static LRUCache Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new LRUCache();
                    }
                    return _instance;
                }
            }
        }


        public void AddToCache(int key, int value) {

            // If value exists then make it he most recently access item
            if (_dic.ContainsKey(key))
            {
                // look up the node from the dic
                var node = _dic[key];

                DisplayCache();
                // Make the node the most recently accessed and update dic
                _cache.Remove(node);
                _cache.AddFirst(node);                
                // _dic[key] = _cache.AddFirst(new KeyValuePair<int, int>(key, value));

                DisplayCache();
            }
            else 
            {
                // check if the cache is at capacity
                if (_cache.Count >= _capacity)
                {
                    // need to remove least used node to make room 
                    var xxx = _cache.Last();
                    _dic.Remove(_cache.Last().Key);
                    _cache.RemoveLast();
                }

                // Add to cache and dic
                _dic.Add(key, _cache.AddFirst(new KeyValuePair<int, int>(key, value)));
            }
        }

        public int GetFromCache(int key)
        {
            DisplayDictionary();
            // check in the value required exists in the cache
            if (!_dic.ContainsKey(key))
                return -1;

            // look up the node from the dic
            var node = _dic[key];

            DisplayCache();
            // Make the node the most recently accessed
            _cache.Remove(node);
            DisplayCache();
            _cache.AddFirst(node);

            DisplayCache();

           //  _dic[key] = _cache.AddFirst(node.Value); 
            DisplayDictionary();

            return node.Value.Value;
        }


        public int CacheLength()
        {            
            return _cache.Count;
        }

        public void UpdateThreshold(int cacheSize)
        {
            _capacity = cacheSize;
        }

        public int CurrentThreshold()
        {
            return _capacity;
        }

        public void DisplayCache()
        {
            if (_cache.Count != 0)
            {
                Console.WriteLine("*** Cache **** ");
                foreach (var node in _cache)
                {
                    Console.WriteLine(node);                    
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public void DisplayDictionary()
        {
            if (_dic.Count != 0)
            {
                Console.WriteLine("*** Dictionary **** ");
                foreach (var item in _dic)
                {
                    Console.WriteLine(item.Key + "  " + item.Value.Value);
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

    }
}
