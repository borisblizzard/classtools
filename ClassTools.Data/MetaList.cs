using System;
using System.Collections.Generic;

namespace ClassTools.Data
{
    [Serializable]
    public class MetaList<T> : List<T>, IEquatable<MetaList<T>>
       where T : IEquatable<T>
    {
        #region Construct
        public MetaList()
            : base()
        {
        }

        public MetaList(int capacity)
            : base(capacity)
        {
        }

        public MetaList(IEnumerable<T> collection)
            : base(collection)
        {
        }
        #endregion

        #region Equals
        public bool Equals(MetaList<T> other)
        {
            if (other == null) return false;
            if (this.Count != other.Count) return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (!this[i].Equals(other[i])) return false;
            }
            return true;
        }
        #endregion

        #region Methods
        public bool TryMoveUp(int index)
        {
            if (index > 0)
            {
                T obj = this[index];
                this[index] = this[index - 1];
                this[index - 1] = obj;
                return true;
            }
            return false;
        }

        public bool TryMoveDown(int index)
        {
            if (index < this.Count - 1)
            {
                T obj = this[index];
                this[index] = this[index + 1];
                this[index + 1] = obj;
                return true;
            }
            return false;
        }

        public MetaList<T> Align(MetaList<T> other)
        {
            MetaList<T> alignedList = new MetaList<T>();
            for (int i = 0; i < this.Count; i++)
            {
                for (int j = 0; j < other.Count; j++)
                {
                    if (this[i].Equals(other[j]))
                    {
                        alignedList.Add(other[j]);
                        break;
                    }
                }
            }
            return alignedList;
        }

        public bool Matches(MetaList<T> other)
        {
            if (this.Count != other.Count)
            {
                return false;
            }
            bool found = false;
            for (int i = 0; i < this.Count; i++)
            {
                found = false;
                for (int j = 0; j < other.Count; j++)
                {
                    if (this[i].Equals(other[j]))
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
        #endregion

    }
}
