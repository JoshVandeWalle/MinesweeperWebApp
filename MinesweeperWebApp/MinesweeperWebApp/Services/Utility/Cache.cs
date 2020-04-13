using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperWebApp.Services.Utility
{
    public class Cache
    {
        private static Cache Instance;
        private List<CacheItem> Data;

        private Cache()
        {
            Data = new List<CacheItem>();
        }

        public static Cache AccessCache()
        {
            if (Instance == null)
            {
                Instance = new Cache();
            }

            return Instance;
        }

        public void Put(string key, string data)
        {
            Data.Add(new CacheItem(key, data));
        }

        public string Get(string key)
        {
            foreach (CacheItem item in Data)
            {
                if (item.Key == key)
                    return item.Data;
            }

            return null;
        }

        public string Remove(string key)
        {
            foreach (CacheItem item in Data)
            {
                if (item.Key == key)
                {
                    Data.Remove(item);
                    return item.Data;
                }
            }

            return null;
        }

        public void Clear()
        {
            Data.RemoveRange(0, Data.Count);
        }

        public class CacheItem
        {
            public string Key { get; set; }
            public string Data { get; set; }

            public CacheItem(string key, string data)
            {
                Key = key;
                Data = data;
            }
        }
    }
}