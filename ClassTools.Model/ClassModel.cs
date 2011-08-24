using System;
using System.Collections.Generic;

namespace ClassTools.Model
{
    [Serializable]
    public class ClassModel
    {
        #region Constants
        public static string[] AccessorNames = new string[] { "public", "protected", "private" };
        #endregion

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
                foreach (MetaClass classe in this.classes)
                {
                    types.Remove(classe);
                }
                return types;
            }
        }
        #endregion

        #region Constructors
        public ClassModel() : base()
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
        public virtual bool Equals(ClassModel other)
        {
            if (!Utility.ListEquals(this.classes, other.classes)) return false;
            if (!Utility.ListEquals(this.TypesOnly, other.TypesOnly)) return false;
            return true;
        }
        #endregion

        #region Class Methods
        public MetaClass CreateNewClass(int index)
        {
            MetaClass classe = new MetaClass(this);
            this.classes.Insert(index, classe);
            this.types = this.TypesOnly;
            foreach (MetaClass c in this.classes)
            {
                this.types.Add(c);
            }
            return classe;
        }

        public void DeleteClass(string name)
        {
            MetaClass classe = this.classes.Find(c => c.Name == name);
            if (classe != null)
            {
                this.DeleteClass(classe);
            }
        }

        public void DeleteClass(MetaClass classe)
        {
            this.classes.Remove(classe);
            this.types.Remove(classe);
        }

        public void DeleteClassAt(int index)
        {
            this.DeleteClass(this.classes[index]);
        }

        public void ReplaceClassAt(int index, MetaClass classe)
        {
            MetaClass oldClass = this.classes[index];
            int otherIndex = this.types.IndexOf(oldClass);
            this.classes[index] = classe;
            this.types[otherIndex] = classe;
            classe.Model = this;
            switch (classe.TypeCategory)
            {
                case ETypeCategory.Normal:
                    classe.SubType1 = null;
                    classe.SubType2 = null;
                    break;
                case ETypeCategory.Collection:
                    classe.SubType1 = this.types.Find(t => t.Equals(classe.SubType1));
                    classe.SubType2 = null;
                    break;
                case ETypeCategory.Dictionary:
                    classe.SubType1 = this.types.Find(t => t.Equals(classe.SubType1));
                    classe.SubType2 = this.types.Find(t => t.Equals(classe.SubType2));
                    break;
            }
            if (classe.HasSuperClass)
            {
                classe.SuperClass = this.classes.Find(c => c.Equals(classe.SuperClass));
            }
            for (int i = 0; i < classe.Variables.Count; i++)
            {
                classe.ReplaceVariableAt(i, classe.Variables[i]);
            }
            for (int i = 0; i < classe.Methods.Count; i++)
            {
                classe.ReplaceMethodAt(i, classe.Methods[i]);
            }
            for (int i = 0; i < this.types.Count; i++)
            {
                this.types[i].UpdateType(oldClass, classe);
            }
        }

        public bool TryClassMoveUp(int index)
        {
            if (index > 0)
            {
                MetaClass classe = this.classes[index];
                this.classes[index] = this.classes[index - 1];
                this.classes[index - 1] = classe;
                return true;
            }
            return false;
        }

        public bool TryClassMoveDown(int index)
        {
            if (index < this.classes.Count - 1)
            {
                MetaClass classe = this.classes[index];
                this.classes[index] = this.classes[index + 1];
                this.classes[index + 1] = classe;
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
            foreach (MetaClass classe in this.classes)
            {
                this.types.Add(classe);
            }
            return type;
        }

        public void DeleteType(string name)
        {
            foreach (MetaType typee in this.types)
            {
                if (typee.Name == name)
                {
                    this.DeleteType(typee);
                    break;
                }
            }
        }

        public void DeleteType(MetaType type)
        {
            this.types.Remove(type);
        }

        public void DeleteTypeAt(int index)
        {
            this.DeleteType(this.types[index]);
        }

        public void ReplaceTypeAt(int index, MetaType type)
        {
            MetaType oldType = this.types[index];
            int otherIndex = this.types.IndexOf(oldType);
            this.types[index] = type;
            this.types[otherIndex] = type;
            type.Model = this;
            switch (type.TypeCategory)
            {
                case ETypeCategory.Normal:
                    type.SubType1 = null;
                    type.SubType2 = null;
                    break;
                case ETypeCategory.Collection:
                    type.SubType1 = this.types.Find(t => t.Equals(type.SubType1));
                    type.SubType2 = null;
                    break;
                case ETypeCategory.Dictionary:
                    type.SubType1 = this.types.Find(t => t.Equals(type.SubType1));
                    type.SubType2 = this.types.Find(t => t.Equals(type.SubType2));
                    break;
            }
            for (int i = 0; i < this.types.Count; i++)
            {
                this.types[i].UpdateType(oldType, type);
            }
        }

        public bool TryTypeMoveUp(int index)
        {
            if (index > 0)
            {
                this.types = this.TypesOnly;
                MetaType type = this.types[index];
                this.types[index] = this.types[index - 1];
                this.types[index - 1] = type;
                foreach (MetaClass c in this.classes)
                {
                    this.types.Add(c);
                }
                return true;
            }
            return false;
        }

        public bool TryTypeMoveDown(int index)
        {
            List<MetaType> types = this.TypesOnly;
            if (index < types.Count - 1)
            {
                this.types = types;
                MetaType type = this.types[index];
                this.types[index] = this.types[index + 1];
                this.types[index + 1] = type;
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
