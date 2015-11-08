using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;


namespace Carrot.BLL
{
    public class SiteCache
    {
        private static readonly Cache _cache;
        public static readonly int DayFactor;
        private static int Factor;
        public static readonly int HourFactor;
        public static readonly int MinuteFactor;

        static SiteCache()
        {
            DayFactor = 17280;
            HourFactor = 720;
            MinuteFactor = 12;
            Factor = 5;
            _cache = HttpRuntime.Cache;
        }

        private SiteCache()
        {
        }

        public static void Clear()
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                _cache.Remove(enumerator.Key.ToString());
            }
        }

        public static object Get(string key)
        {
            return _cache[key];
        }

        public static void Insert(string key, object obj)
        {
            Insert(key, obj, null, 1);
        }

        public static void Insert(string key, object obj, int seconds)
        {
            Insert(key, obj, null, seconds);
        }

        public static void Insert(string key, object obj, CacheDependency dep)
        {
            Insert(key, obj, dep, HourFactor * 12);
        }

        public static void Insert(string key, object obj, int seconds, CacheItemPriority priority)
        {
            Insert(key, obj, null, seconds, priority);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds)
        {
            Insert(key, obj, dep, seconds, CacheItemPriority.Normal);
        }
        /// <summary>   
        /// 简单创建/修改Cache，前提是这个值是字符串形式的   
        /// </summary>   
        /// <param name="strCacheName">Cache名称</param>   
        /// <param name="strValue">Cache值</param>   
        /// <param name="iExpires">有效期，秒数（使用的是当前时间+秒数得到一个绝对到期值）</param>   
        /// <param name="priority">保留优先级，1最不会被清除，6最容易被内存管理清除（1:NotRemovable；2:High；3:AboveNormal；4:Normal；5:BelowNormal；6:Low）</param>   
        public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.Now.AddSeconds((double)(Factor * seconds)), TimeSpan.Zero, priority, null);
            }
        }

        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        public static void Max(string key, object obj, CacheDependency dep)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }

        public static void MicroInsert(string key, object obj, int secondFactor)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, null, DateTime.Now.AddSeconds((double)(Factor * secondFactor)), TimeSpan.Zero);
            }
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            Regex regex1 = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            while (enumerator.MoveNext())
            {
                if (regex1.IsMatch(enumerator.Key.ToString()))
                {
                    _cache.Remove(enumerator.Key.ToString());
                }
            }
        }

        public static void ReSetFactor(int cacheFactor)
        {
            Factor = cacheFactor;
        }
    }
}
