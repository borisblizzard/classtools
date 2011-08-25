using System;
using System.Collections.Generic;

namespace ClassTools.Data
{
    static class Utility
    {
        #region Public Equals Comparer
        public static bool ListEquals<T>(List<T> a, List<T> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }
            for (int i = 0; i < a.Count; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ListMatches<T>(List<T> a, List<T> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }
            bool found = false;
            for (int i = 0; i < a.Count; i++)
            {
                found = false;
                for (int j = 0; j < b.Count; j++)
                {
                    if (a[i].Equals(b[j]))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DictionaryEquals<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            List<K> aKeys = getKeys(a);
            List<K> bKeys = getKeys(b);
            if (!Utility.ListMatches(aKeys, bKeys))
            {
                return false;
            }
            alignList(ref aKeys, ref bKeys);
            for (int i = 0; i < a.Count; i++)
            {
                if (!a[aKeys[i]].Equals(b[bKeys[i]]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ObjectEquals<T>(T a, T b)
        {
            if (a != null && b != null)
            {
                if (!a.Equals(b))
                {
                    return false;
                }
            }
            else if (a != null || b != null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Private Utility
        private static List<K> getKeys<K, V>(Dictionary<K, V> dictionary)
        {
            Dictionary<K, V>.KeyCollection keyCollection = dictionary.Keys;
            K[] keyArray = new K[keyCollection.Count];
            keyCollection.CopyTo(keyArray, 0);
            return new List<K>(keyArray);
        }

        private static void alignList<T>(ref List<T> a, ref List<T> b)
        {
            List<T> alignedList = new List<T>();
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    if (a[i].Equals(b[j]))
                    {
                        alignedList.Add(b[j]);
                        break;
                    }
                }
            }
            b = alignedList;
        }
        #endregion

    }
}
