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

        public MetaList<MetaType> TypesOnly
        {
            get
            {
                MetaList<MetaType> types = new MetaList<MetaType>(this.types);
                for (int i = 0; i < 0; i++)
                {
                    types.Remove(this.classes[i]);
                }
                foreach (MetaClass metaClass in this.classes)
                {
                    types.Remove(metaClass);
                }
                return types;
            }
        }
        #endregion

        #region Construct
        public Model()
            : base()
        {
            this.classes = new MetaList<MetaClass>();
            this.types = new MetaList<MetaType>();
            for (int i = 0; i < Constants.TYPES_VOID.Length; i++)
            {
                this.types.Add(new MetaType(this, Constants.TYPES_VOID[i]));
            }
            for (int i = 0; i < Constants.TYPES_INT.Length; i++)
            {
                this.types.Add(new MetaType(this, Constants.TYPES_INT[i]));
            }
            for (int i = 0; i < Constants.TYPES_FLOAT.Length; i++)
            {
                this.types.Add(new MetaType(this, Constants.TYPES_FLOAT[i]));
            }
            for (int i = 0; i < Constants.TYPES_BOOL.Length; i++)
            {
                this.types.Add(new MetaType(this, Constants.TYPES_BOOL[i]));
            }
            for (int i = 0; i < Constants.TYPES_CHAR.Length; i++)
            {
                this.types.Add(new MetaType(this, Constants.TYPES_CHAR[i]));
            }
            for (int i = 0; i < Constants.TYPES_STRING.Length; i++)
            {
                this.types.Add(new MetaType(this, Constants.TYPES_STRING[i]));
            }
        }
        #endregion

        #region Equals
        public bool Equals(Model other)
        {
            if (!base.Equals(other)) return false;
            if (!this.classes.Equals(other.classes)) return false;
            if (!this.TypesOnly.Equals(other.TypesOnly)) return false;
            return true;
        }
        #endregion

        #region Class Methods
        public MetaClass CreateNewClass(int index)
        {
            MetaClass metaClass = new MetaClass(this);
            this.classes.Insert(index, metaClass);
            this.types = this.TypesOnly;
            foreach (MetaClass c in this.classes)
            {
                this.types.Add(c);
            }
            return metaClass;
        }

        public void DeleteClass(string name)
        {
            MetaClass metaClass = this.classes.Find(c => c.Name == name);
            if (metaClass != null)
            {
                this.DeleteClass(metaClass);
            }
        }

        public void DeleteClass(MetaClass metaClass)
        {
            this.classes.Remove(metaClass);
            this.types.Remove(metaClass);
        }

        public void DeleteClassAt(int index)
        {
            this.DeleteClass(this.classes[index]);
        }

        public void ReplaceClassAt(int index, MetaClass metaClass)
        {
            MetaClass oldClass = this.classes[index];
            int otherIndex = this.types.IndexOf(oldClass);
            this.classes[index] = metaClass;
            this.types[otherIndex] = metaClass;
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

        public bool TryClassMoveUp(int index)
        {
            return this.classes.TryMoveUp(index);
        }

        public bool TryClassMoveDown(int index)
        {
            return this.classes.TryMoveDown(index);
        }

        public void SortClasses()
        {
            this.classes.Sort(new Comparison<MetaClass>((a, b) => a.Name.CompareTo(b.Name)));
        }
        #endregion

        #region Type Methods
        public MetaType CreateNewType(int index)
        {
            MetaType type = new MetaType(this);
            this.types = this.TypesOnly;
            this.types.Insert(index, type);
            foreach (MetaClass metaClass in this.classes)
            {
                this.types.Add(metaClass);
            }
            return type;
        }

        public void DeleteType(string name)
        {
            foreach (MetaType metaType in this.types)
            {
                if (metaType.Name == name)
                {
                    this.DeleteType(metaType);
                    break;
                }
            }
        }

        public void DeleteType(MetaType metaType)
        {
            this.types.Remove(metaType);
        }

        public void DeleteTypeAt(int index)
        {
            this.DeleteType(this.types[index]);
        }

        public void ReplaceTypeAt(int index, MetaType metaType)
        {
            MetaType oldType = this.types[index];
            int otherIndex = this.types.IndexOf(oldType);
            this.types[index] = metaType;
            this.types[otherIndex] = metaType;
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

        public bool TryTypeMoveUp(int index)
        {
            if (index > 0)
            {
                this.types = this.TypesOnly;
                MetaType metaType = this.types[index];
                this.types[index] = this.types[index - 1];
                this.types[index - 1] = metaType;
                foreach (MetaClass metaClass in this.classes)
                {
                    this.types.Add(metaClass);
                }
                return true;
            }
            return false;
        }

        public bool TryTypeMoveDown(int index)
        {
            MetaList<MetaType> metaTypes = this.TypesOnly;
            if (index < metaTypes.Count - 1)
            {
                this.types = metaTypes;
                MetaType metaType = this.types[index];
                this.types[index] = this.types[index + 1];
                this.types[index + 1] = metaType;
                foreach (MetaClass c in this.classes)
                {
                    this.types.Add(c);
                }
                return true;
            }
            return false;
        }

        #endregion

    }
}
