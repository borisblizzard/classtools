using System;
using System.Collections.Generic;

namespace ClassTools.Model
{
    static class Utility
    {
        public static bool ListEquals<T>(List<T> a, List<T> b)
        {
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
            {
                if (!a[i].Equals(b[i])) return false;
            }
            return true;
        }

        public static bool DictionaryEquals<K, V>(Dictionary<K, V> a, Dictionary<K, V> b)
        {
            List<K> aKeys = getKeys(a);
            aKeys.Sort();
            List<K> bKeys = getKeys(b);
            bKeys.Sort();
            if (!Utility.ListEquals(aKeys, bKeys)) return false;
            foreach (K key in aKeys)
            {
                if (!a[key].Equals(b[key])) return false;
            }
            return true;
        }

        private static List<K> getKeys<K, V>(Dictionary<K, V> dictionary)
        {
            Dictionary<K, V>.KeyCollection keyCollection = dictionary.Keys;
            K[] keyArray = new K[keyCollection.Count];
            keyCollection.CopyTo(keyArray, 0);
            return new List<K>(keyArray);
        }

    }

}
