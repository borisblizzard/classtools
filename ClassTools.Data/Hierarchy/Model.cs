using System;
using System.Collections.Generic;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class Model : Base, IEquatable<Model>
    {
        #region Fields
        protected MetaList<MetaClass> classes;
        protected MetaList<MetaType> types;
        #endregion

        #region Properties
        public MetaList<MetaClass> Classes
        {
            get { return classes; }
        }

        public MetaList<MetaType> Types
        {
            get { return types; }
        }

        public MetaList<MetaType> AllTypes
        {
            get
            {
                MetaList<MetaType> result = new MetaList<MetaType>(this.types);
                result.AddRange(this.classes.ToArray());
                return result;
            }
        }

        public MetaList<MetaClass> LeafClasses
        {
            get { return new MetaList<MetaClass>(this.classes.FindAll(c => c.FindSubClasses(this.classes).Count == 0)); }
        }
        #endregion

        #region Construct
        public Model()
            : base()
        {
            this.classes = new MetaList<MetaClass>();
            this.types = new MetaList<MetaType>();
            foreach (string name in Constants.TYPES_VOID)
            {
                this.types.Add(new MetaType(this, name));
            }
            foreach (string name in Constants.TYPES_INT)
            {
                this.types.Add(new MetaType(this, name));
            }
            foreach (string name in Constants.TYPES_FLOAT)
            {
                this.types.Add(new MetaType(this, name));
            }
            foreach (string name in Constants.TYPES_BOOL)
            {
                this.types.Add(new MetaType(this, name));
            }
            foreach (string name in Constants.TYPES_CHAR)
            {
                this.types.Add(new MetaType(this, name));
            }
            foreach (string name in Constants.TYPES_STRING)
            {
                this.types.Add(new MetaType(this, name));
            }
        }
        #endregion

        #region Equals
        public bool Equals(Model other)
        {
            if (!base.Equals(other)) return false;
            if (!this.classes.Equals(other.classes)) return false;
            if (!this.types.Equals(other.types)) return false;
            return true;
        }
        #endregion

        #region Methods
        public MetaList<MetaType> FindTypeMismatches(Model other)
        {
            MetaList<MetaType> result = new MetaList<MetaType>();
            result.AddRange(this.types.FindAll(t => !other.types.Contains(t)));
            result.AddRange(this.classes.FindAll(c => !other.classes.Contains(c)).ToArray());
            return result;
        }

        public MetaType FindMatchingType(MetaType type)
        {
            if (type.CategoryType == ECategoryType.Class)
            {
                MetaClass metaClass = (MetaClass)type;
                return this.classes.Find(c => c.Equals(metaClass));
            }
            return this.types.Find(t => t.Equals(type));
        }

        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            int index = -1;
            if (oldType.CategoryType == ECategoryType.Class && newType.CategoryType == ECategoryType.Class)
            {
                index = this.classes.IndexOf((MetaClass)oldType);
            }
            else if (oldType.CategoryType != ECategoryType.Class && newType.CategoryType != ECategoryType.Class)
            {
                index = this.types.IndexOf((MetaClass)oldType);
            }
            if (oldType.CategoryType == ECategoryType.Class)
            {
                this.classes.Remove((MetaClass)oldType);
            }
            else
            {
                this.types.Remove(oldType);
            }
            if (newType.CategoryType == ECategoryType.Class)
            {
                if (index >= 0)
                {
                    this.classes.Insert(index, (MetaClass)newType);
                }
                else
                {
                    this.classes.Add((MetaClass)newType);
                }
            }
            else
            {
                if (index >= 0)
                {
                    this.types.Insert(index, (MetaClass)newType);
                }
                else
                {
                    this.types.Add((MetaClass)newType);
                }
            }
            base.UpdateType(oldType, newType);
        }
        #endregion

        #region Class Methods
        public void CreateNewClass(int index)
        {
            this.classes.Insert(index, new MetaClass(this));
        }

        public void DeleteClassAt(int index)
        {
            this.classes.RemoveAt(index);
        }

        public void SortClasses()
        {
            this.classes.Sort(new Comparison<MetaClass>((a, b) => a.Name.CompareTo(b.Name)));
        }

        public bool ClassExists(MetaClass metaClass)
        {
            return this.classes.Exists(c => c.Equals(metaClass));
        }
        #endregion

        #region Type Methods
        public void CreateNewType(int index)
        {
            this.types.Insert(index, new MetaType(this));
        }

        public void DeleteTypeAt(int index)
        {
            this.types.RemoveAt(index);
        }

        public bool TypeExists(MetaType metaType)
        {
            return this.types.Exists(c => c.Equals(metaType));
        }
        #endregion

    }
}
