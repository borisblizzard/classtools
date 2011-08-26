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
        public MetaClass CreateNewClass(int index)
        {
            MetaClass metaClass = new MetaClass(this);
            this.classes.Insert(index, metaClass);
            return metaClass;
        }

        public void DeleteClass(MetaClass metaClass)
        {
            this.classes.Remove(metaClass);
        }

        public void DeleteClassAt(int index)
        {
            this.classes.RemoveAt(index);
        }

        public void ReplaceClassAt(int index, MetaClass metaClass)
        {
            MetaClass oldClass = this.classes[index];
            this.classes[index] = metaClass;
            metaClass.Model = this;
            switch (metaClass.Category)
            {
                case ECategory.Normal:
                    metaClass.SubType1 = null;
                    metaClass.SubType2 = null;
                    break;
                case ECategory.Collection:
                    metaClass.SubType1 = this.types.Find(t => t.Equals(metaClass.SubType1));
                    metaClass.SubType2 = null;
                    break;
                case ECategory.Dictionary:
                    metaClass.SubType1 = this.types.Find(t => t.Equals(metaClass.SubType1));
                    metaClass.SubType2 = this.types.Find(t => t.Equals(metaClass.SubType2));
                    break;
            }
            if (metaClass.HasSuperClass)
            {
                metaClass.SuperClass = this.classes.Find(c => c.Equals(metaClass.SuperClass));
            }
            for (int i = 0; i < metaClass.Variables.Count; i++)
            {
                metaClass.ReplaceVariableAt(i, metaClass.Variables[i]);
            }
            for (int i = 0; i < metaClass.Methods.Count; i++)
            {
                metaClass.ReplaceMethodAt(i, metaClass.Methods[i]);
            }
            for (int i = 0; i < this.types.Count; i++)
            {
                this.types[i].UpdateType(oldClass, metaClass);
            }
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
        public MetaType CreateNewType(int index)
        {
            MetaType type = new MetaType(this);
            this.types.Insert(index, type);
            return type;
        }

        public void DeleteType(MetaType metaType)
        {
            this.types.Remove(metaType);
        }

        public void DeleteTypeAt(int index)
        {
            this.types.RemoveAt(index);
        }

        public void ReplaceTypeAt(int index, MetaType metaType)
        {
            MetaType oldType = this.types[index];
            this.types[index] = metaType;
            metaType.Model = this;
            switch (metaType.Category)
            {
                case ECategory.Normal:
                    metaType.SubType1 = null;
                    metaType.SubType2 = null;
                    break;
                case ECategory.Collection:
                    metaType.SubType1 = this.types.Find(t => t.Equals(metaType.SubType1));
                    metaType.SubType2 = null;
                    break;
                case ECategory.Dictionary:
                    metaType.SubType1 = this.types.Find(t => t.Equals(metaType.SubType1));
                    metaType.SubType2 = this.types.Find(t => t.Equals(metaType.SubType2));
                    break;
            }
            for (int i = 0; i < this.types.Count; i++)
            {
                this.types[i].UpdateType(oldType, metaType);
            }
        }

        public bool TypeExists(MetaType metaType)
        {
            return this.types.Exists(c => c.Equals(metaType));
        }
        #endregion

    }
}
