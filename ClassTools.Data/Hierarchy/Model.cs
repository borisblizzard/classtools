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
                this.types.Add(new MetaType(name));
            }
            foreach (string name in Constants.TYPES_INT)
            {
                this.types.Add(new MetaType(name));
            }
            foreach (string name in Constants.TYPES_FLOAT)
            {
                this.types.Add(new MetaType(name));
            }
            foreach (string name in Constants.TYPES_BOOL)
            {
                this.types.Add(new MetaType(name));
            }
            foreach (string name in Constants.TYPES_CHAR)
            {
                this.types.Add(new MetaType(name));
            }
            foreach (string name in Constants.TYPES_STRING)
            {
                this.types.Add(new MetaType(name));
            }
        }
        #endregion

        #region Equals
        public bool Equals(Model other)
        {
            if (Object.ReferenceEquals(this, other)) return true;
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

        public MetaType FindMatchingType(MetaType metaType)
        {
            if (metaType.CategoryType == ECategoryType.Class)
            {
                MetaClass metaClass = (MetaClass)metaType;
                return this.classes.Find(c => c.Equals(metaClass));
            }
            return this.types.Find(t => t.Equals(metaType));
        }

        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            base.UpdateType(oldType, newType);
            if (oldType.CategoryType == ECategoryType.Class && newType.CategoryType == ECategoryType.Class)
            {
                this.classes[this.classes.IndexOf((MetaClass)oldType)] = (MetaClass)newType;
            }
            else if (oldType.CategoryType != ECategoryType.Class && newType.CategoryType != ECategoryType.Class)
            {
                this.types[this.types.IndexOf(oldType)] = newType;
            }
            foreach (MetaType metaType in this.AllTypes)
            {
                metaType.UpdateType(oldType, newType);
            }
        }

        public override void UpdateVariable(MetaType metaType, MetaVariable oldVariable, MetaVariable newVariable)
        {
            base.UpdateVariable(metaType, oldVariable, newVariable);
            foreach (MetaClass metaClass in this.classes)
            {
                metaClass.UpdateVariable(metaType, oldVariable, newVariable);
            }
        }

        public override void RemoveVariable(MetaType metaType, MetaVariable metaVariable)
        {
            base.RemoveVariable(metaType, metaVariable);
            foreach (MetaClass metaClass in this.classes)
            {
                metaClass.RemoveVariable(metaType, metaVariable);
            }
        }

        public override void UpdateParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter oldParameter, MetaParameter newParameter)
        {
            base.UpdateParameter(metaType, metaMethod, oldParameter, newParameter);
            foreach (MetaClass metaClass in this.classes)
            {
                metaClass.UpdateParameter(metaType, metaMethod, oldParameter, newParameter);
            }
        }

        public override void RemoveParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter metaParameter)
        {
            base.RemoveParameter(metaType, metaMethod, metaParameter);
            foreach (MetaClass metaClass in this.classes)
            {
                metaClass.RemoveParameter(metaType, metaMethod, metaParameter);
            }
        }
        #endregion

        #region Class Methods
        public void CreateNewClass(int index)
        {
            int i = 1;
            string name = "ANON_CLASS";
            while (this.classes.Exists(c => c.Module == "" && c.Name == name))
            {
                name = "ANON_CLASS_" + i.ToString();
                i++;
            }
            this.classes.Insert(index, new MetaClass(name));
        }

        public void DeleteClassAt(int index)
        {
            this.classes.RemoveAt(index);
        }

        public void SortClasses()
        {
            this.classes.Sort(new Comparison<MetaClass>((a, b) => a.Name.CompareTo(b.Name)));
        }
        #endregion

        #region Type Methods
        public void CreateNewType(int index)
        {
            int i = 1;
            string name = "ANON_TYPE";
            while (this.types.Exists(t => t.Name == name))
            {
                name = "ANON_TYPE_" + i.ToString();
                i++;
            }
            this.types.Insert(index, new MetaType(name));
        }

        public void DeleteTypeAt(int index)
        {
            this.types.RemoveAt(index);
        }
        #endregion

    }
}
