using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOS.Web.UI.Common
{
    public interface ICacheProvider
    {
        void Store(string key, object data);
        void Destroy(string key);
        T Get<T>(string key);
    }
      
    public class RequestCacheProvider : ICacheProvider
    {
        public void Store(string key, object data)
        {
            bool exists = false;
            foreach (object s in HttpContext.Current.Items.Keys)
            {
                if (s.ToString() == key)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
                HttpContext.Current.Items[key] = data;
            else
                HttpContext.Current.Items.Add(key, data);
            
        }

        public void Destroy(string key)
        {
            HttpContext.Current.Items.Remove(key);
        }

        public T Get<T>(string key)
        {
            T item = default(T);
            if (HttpContext.Current.Items[key] != null)
            {
                item = (T)HttpContext.Current.Items[key];
            }
            return item;
        }
    }
    
    public class AppCacheProvider : ICacheProvider
    {
        public void Store(string key, object data)
        {
            bool exists = false;
            foreach (string s in HttpContext.Current.Application.AllKeys)
            {
                if (s == key)
                {
                    exists = true;
                    break;
                }
            }
            
            HttpContext.Current.Application.Lock();

            if (exists)
                HttpContext.Current.Application[key] = data;
            else
                HttpContext.Current.Application.Add(key, data);

            HttpContext.Current.Application.UnLock();

        }

        public void Destroy(string key)
        {

            HttpContext.Current.Application.Lock();

            HttpContext.Current.Application.Remove(key);

            HttpContext.Current.Application.UnLock();
        }

        public void Clear()
        {
            
            HttpContext.Current.Application.Lock();
            
            HttpContext.Current.Application.Clear();
            
            HttpContext.Current.Application.UnLock();
            
        }

        public T Get<T>(string key)
        {
            T item = default(T);
            if (HttpContext.Current.Application[key] != null)
            {
                item = (T)HttpContext.Current.Application[key];
            }
            return item;
        }
    }

    public class CacheProvider : ICacheProvider
    {

        public void Store(string key, object data)
        {

            bool exists = false;
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Key == key)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
                HttpContext.Current.Cache[key] = data;
            else 
                HttpContext.Current.Cache.Insert(key, data);
               
        }

        public void Store(string key, object data, int secondsToStore, bool slidingExpiration = false)
        {

            bool exists = false;
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if(enumerator.Key==key)
                {
                    exists = true;
                    break;
                }    
            }

            if (exists)
                HttpContext.Current.Cache[key] = data;
            else
                if (secondsToStore > 0)
                    HttpContext.Current.Cache.Insert(key, data, null,
                        (slidingExpiration == false) ? DateTime.Now.AddSeconds(secondsToStore) : DateTime.Now.AddDays(1),
                        (slidingExpiration == false) ? TimeSpan.Zero : TimeSpan.FromSeconds(secondsToStore)
                        );
                else
                    HttpContext.Current.Cache.Insert(key, data);
                    
        }

        public void Destroy(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public void Clear()
        {
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
            } 
        }

        public T Get<T>(string key)
        {
            T item = default(T);
            if (HttpContext.Current.Cache[key] != null)
            {
                item = (T)HttpContext.Current.Cache[key];
            }
            return item;
        }


       
    }


    public class RequestCacheManager
    {

        public static void Store(string key, object data)
        {
            new RequestCacheProvider().Store(key, data);
        }

        public static void Destroy(string key)
        {
            new RequestCacheProvider().Destroy(key);
        }

        public static T Get<T>(string key)
        {
            return new RequestCacheProvider().Get<T>(key);
        }
    }

    public class CacheManager
    {

        private static System.Web.Caching.Cache WebCache
        {
            get
            {
               // return new System.Web.Caching.Cache();
                return HttpContext.Current.Cache;
            }
        }

        public static void Store(string key, object data)
        {
            new CacheProvider().Store(key, data);
        }

        public static void Store(string key, object data, int minutes2Sotre, bool slidingExpiration = false)
        {
            if(minutes2Sotre>0)
                new CacheProvider().Store(key,data,minutes2Sotre, slidingExpiration);
            else
                new CacheProvider().Store(key, data);
        }

        public static void Destroy(string key)
        {
            new CacheProvider().Destroy(key);
        }

        public static void Clear()
        {
            new CacheProvider().Clear();
        }


        public static T Get<T>(string key)
        {
            return new CacheProvider().Get<T>(key);
        }

        /// <summary>
        /// Removes all the Cache Enteries of a specific Web Page.
        /// </summary>
        /// <param name="page">The current ASPX Page</param>
        public static void Clear(string curSessionID = "")
        { 
            IDictionaryEnumerator cacheEnumerator = WebCache.GetEnumerator();

            //Dictionery cacheElement will be used for testing purpose only
            Dictionary<string, bool> cacheElements = new Dictionary<string, bool>();


            while (cacheEnumerator.MoveNext())
            {
                cacheElements.Add(cacheEnumerator.Key.ToString(),false);

                if (curSessionID=="")
                {
                   WebCache.Remove(cacheEnumerator.Key.ToString());    
                }
                else
                {
                    if(cacheEnumerator.Key.ToString().StartsWith("["+curSessionID+"]__"))
                    {
                        WebCache.Remove(cacheEnumerator.Key.ToString());

                        KeyValuePair<string, bool> element = cacheElements.Last();
                        element = new KeyValuePair<string, bool>(cacheEnumerator.Key.ToString(),true);
                        cacheElements.Remove(cacheEnumerator.Key.ToString());
                        cacheElements.Add(cacheEnumerator.Key.ToString(), true);

                    }
                }
                
            }


        }

        public static void Clear(List<string> validSessions)
        {
            IDictionaryEnumerator cacheEnumerator = WebCache.GetEnumerator();
            bool valid = false;

            //Dictionery cacheElement will be used for testing purpose only
            Dictionary<string, bool> cacheElements = new Dictionary<string, bool>(); 


            while (cacheEnumerator.MoveNext())
            {
                if(cacheEnumerator==null || cacheEnumerator.Key==null || cacheEnumerator.Key.ToString()=="")
                    continue;


                cacheElements.Add(cacheEnumerator.Key.ToString(), false);


                foreach(string sessionID in validSessions)
                {
                    if (cacheEnumerator.Key.ToString().StartsWith("["+sessionID+"]__")){
                        valid = true;
                        break;
                    }
                }

                if(!valid)
                {
                      WebCache.Remove(cacheEnumerator.Key.ToString());

                      KeyValuePair<string, bool> element = cacheElements.Last();
                      element = new KeyValuePair<string, bool>(cacheEnumerator.Key.ToString(), true);
                      cacheElements.Remove(cacheEnumerator.Key.ToString());
                      cacheElements.Add(cacheEnumerator.Key.ToString(), true);
                }
                 

            }
        }



    }
    
    public class AppCacheManager
    { 

        public static void Store(string key, object data)
        {
            new AppCacheProvider().Store(key, data);
        }

        public static void Destroy(string key)
        {
            new AppCacheProvider().Destroy(key);
        }


        public static void Clear()
        {
            new AppCacheProvider().Clear();
        }

        public static T Get<T>(string key)
        {
            return new AppCacheProvider().Get<T>(key);
        }
    }

}