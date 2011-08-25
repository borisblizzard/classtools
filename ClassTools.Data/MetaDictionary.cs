﻿using System;
using System.Collections.Generic;

namespace ClassTools.Data
{
    [Serializable]
    public class MetaDictionary<K, V> : Dictionary<K, V>, IEquatable<MetaDictionary<K, V>>
        where K : IEquatable<K>
        where V : IEquatable<V>
    {
        #region Construct
        public MetaDictionary()
            : base()
        {
        }
        #endregion

        #region Equals
        public bool Equals(MetaDictionary<K, V> other)
        {
            if (other == null) return false;
            if (this.Count != other.Count) return false;
            MetaList<K> thisKeys = this.getKeys();
            MetaList<K> otherKeys = other.getKeys();
            if (thisKeys.Matches(otherKeys)) return false;
            otherKeys = thisKeys.Align(otherKeys);
            for (int i = 0; i < this.Count; i++)
            {
                if (!this[thisKeys[i]].Equals(other[otherKeys[i]])) return false;
            }
            return true;
        }
        #endregion

        #region Utility
        private MetaList<K> getKeys()
        {
            Dictionary<K, V>.KeyCollection keyCollection = this.Keys;
            K[] keyArray = new K[keyCollection.Count];
            keyCollection.CopyTo(keyArray, 0);
            return new MetaList<K>(keyArray);
        }
        #endregion

    }
}
