using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRU
{
    public class LRUCache
    {
        private int _capacity;
        private Dictionary<object, object> _dic;

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _dic = new Dictionary<object, object>();
        }


        public void Add() { 
        
        }

        public void Remove()
        {

        }

    }
}
