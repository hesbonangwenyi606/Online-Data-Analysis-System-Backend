using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOS.Web.UI.Common
{
    interface ISessionProvider
    {
        void Store(string key, object data);
        void Destroy(string key);
        T Get<T>(string key);
    }
 
    class SessionProvider : ISessionProvider
    {
        public void Store(string key, object data)
        {
            bool exists = false;
            foreach (string s in HttpContext.Current.Session.Keys)
            {
                if (s == key)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
                HttpContext.Current.Session[key] = data;
            else
                HttpContext.Current.Session.Add(key, data);


        }

        public void Destroy(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public T Get<T>(string key)
        {
            T item = default(T);
            if (HttpContext.Current.Session[key] != null)
            {
                item = (T)HttpContext.Current.Session[key];
            }
            return item;
        }

        public void Clear()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }
         
    }
     
    public class SessionManager
    {
        public static void Store(string key, object data)
        {
            new SessionProvider().Store(key, data);
        }

        public static void Destroy(string key)
        {
            new SessionProvider().Destroy(key);
        }

        public static T Get<T>(string key)
        {
            return new SessionProvider().Get<T>(key);
        }

        public static void Clear() {
            new SessionProvider().Clear();
        }
        //public static System.Data.DataTable GetAll()
        //{

        //}

    }

}