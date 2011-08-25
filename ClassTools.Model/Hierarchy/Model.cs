﻿using System;
using System.Collections.Generic;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class Model
    {
        #region Fields
        protected List<MetaClass> classes;
        protected List<MetaType> types;
        #endregion

        #region Properties
        public List<MetaClass> Classes
        {
            get { return classes; }
        }

        public List<MetaType> Types
        {
            get { return types; }
        }

        public List<MetaType> TypesOnly
        {
            get
            {
                List<MetaType> types = new List<MetaType>(this.types);
                foreach (MetaClass metaClass in this.classes)
                {
                    types.Remove(metaClass);
                }
                return types;
            }
        }
        #endregion

        #region Constructors
        public Model() : base()
        {
            this.classes = new List<MetaClass>();
            this.types = new List<MetaType>();
            this.types.Add(new MetaType(this, "void"));
            this.types.Add(new MetaType(this, "int"));
            this.types.Add(new MetaType(this, "unsigned int"));
            this.types.Add(new MetaType(this, "long"));
            this.types.Add(new MetaType(this, "unsigned long"));
            this.types.Add(new MetaType(this, "short"));
            this.types.Add(new MetaType(this, "unsigned short"));
            this.types.Add(new MetaType(this, "char"));
            this.types.Add(new MetaType(this, "unsigned char"));
            this.types.Add(new MetaType(this, "float"));
            this.types.Add(new MetaType(this, "double"));
            this.types.Add(new MetaType(this, "bool"));
            this.types.Add(new MetaType(this, "string"));
        }
        #endregion

        #region Behavior
        public virtual bool Equals(Model other)
        {
            if (!Utility.ListEquals(this.classes, other.classes)) return false;
            if (!Utility.ListEquals(this.TypesOnly, other.TypesOnly)) return false;
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
            switch (metaClass.TypeCategory)
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
            if (index > 0)
            {
                MetaClass metaClass = this.classes[index];
                this.classes[index] = this.classes[index - 1];
                this.classes[index - 1] = metaClass;
                return true;
            }
            return false;
        }

        public bool TryClassMoveDown(int index)
        {
            if (index < this.classes.Count - 1)
            {
                MetaClass metaClass = this.classes[index];
                this.classes[index] = this.classes[index + 1];
                this.classes[index + 1] = metaClass;
                return true;
            }
            return false;
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
            switch (metaType.TypeCategory)
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
            List<MetaType> metaTypes = this.TypesOnly;
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