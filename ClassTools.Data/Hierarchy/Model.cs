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
                foreach (MetaClass metaClass in this.classes)
                {
                    result.Add(metaClass);
                }
                return result;
            }
        }

        public MetaList<MetaClass> LeafClasses
        {
            get
            {
                MetaList<MetaClass> result = new MetaList<MetaClass>();
                foreach (MetaClass metaClass in this.classes)
                {
                    if (metaClass.FindSubClasses(this.classes).Count == 0)
                    {
                        result.Add(metaClass);
                    }
                }
                return result;
            }
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
