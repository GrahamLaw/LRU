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
        private Dictionary<int, LinkedListNode<KeyValuePair<int, object>>> _dic;
        private LinkedList<KeyValuePair<int, object>> _cache = new LinkedList<KeyValuePair<int, object>>();

        //public LRUCache(int capacity)
        //{
        //    _capacity = capacity;
        //    _dic = new Dictionary<int, LinkedListNode<KeyValuePair<int, int>>>();
        //    _cache = new LinkedList<KeyValuePair<int, int>>();
        //}

        private LRUCache()
        {
            _capacity = _defaultCapcity;
            _dic = new Dictionary<int, LinkedListNode<KeyValuePair<int, object>>>();
            _cache = new LinkedList<KeyValuePair<int, object>>();
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

        /// <summary>
        /// Returns bool indicating if an item was removed from the cache       
        /// </summary>
        public bool AddToCache(int key, int value) {

            bool removed = false;
            // If value exists then make it he most recently access item
            if (_dic.ContainsKey(key))
            {
                // look up the node from the dic
                var node = _dic[key];
                
                // Make the node the most recently accessed and update dic
                _cache.Remove(node);
                _cache.AddFirst(node);                
            }
            else 
            {
                removed = ResizeCache();
                // Add to cache and dic
                _dic.Add(key, _cache.AddFirst(new KeyValuePair<int, object>(key, value)));
            }

            return removed;
        }

        private bool ResizeCache()
        {
            bool removed = false;
            // check if the cache is at capacity
            while (_cache.Count >= _capacity)
            {
                // need to remove least used node to make room 
                // var xxx = _cache.Last();
                _dic.Remove(_cache.Last().Key);
                _cache.RemoveLast();
                removed = true;
            }

            return removed;
        }

        public object GetFromCache(int key)
        {            
            // check in the value required exists in the cache
            if (!_dic.ContainsKey(key))
                return null;

            // look up the node from the dic
            var node = _dic[key];
            
            // Make the node the most recently accessed
            _cache.Remove(node);            
            _cache.AddFirst(node);
            
            return node.Value.Value;
        }

        public void ClearCache()
        {         
            while (_cache.Count > 0)
            {
                // need to remove least used node to make room 
                var xxx = _cache.Last();
                _dic.Remove(_cache.Last().Key);
                _cache.RemoveLast();               
            }            
        }



        public int CacheLength()
        {            
            return _cache.Count;
        }

        public void UpdateThreshold(int cacheSize)
        {
            if (cacheSize > 0)
            {
                _capacity = cacheSize;
                ResizeCache();
            }
        }

        public int CurrentThreshold()
        {
            return _capacity;
        }

        // ******************
        // used for debug only 
        private void DisplayCache()
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

        // ******************
        // used for debug only 
        private void DisplayDictionary()
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
