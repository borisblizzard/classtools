using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaInstanceVariable : MetaBase, IEquatable<MetaInstanceVariable>
    {
        #region Fields
        protected string name;
        protected string prefix;
        protected bool nullable;
        protected MetaValue value;
        #endregion

        #region Properties
        public string Name
        {
            get { return this.name; }
        }

        public string Prefix
        {
            get { return this.prefix; }
        }

        public bool Nullable
        {
            get { return this.nullable; }
        }

        public MetaValue Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        #endregion

        #region Construct
        public MetaInstanceVariable(MetaVariable metaVariable)
            : base()
        {
            this.name = metaVariable.Name;
            this.prefix = metaVariable.Prefix;
            this.nullable = metaVariable.Nullable;
            switch (metaVariable.Type.CategoryType)
            {
                case ECategoryType.Integral:
                    this.value = new MetaValue(metaVariable.Type, metaVariable.DefaultValue);
                    break;
                case ECategoryType.Class:
                    MetaClass metaClass = (MetaClass)metaVariable.Type;
                    this.value = new MetaValue(metaClass, metaVariable.Nullable ? null : new MetaInstance(metaClass));
                    break;
                case ECategoryType.List:
                    this.value = new MetaValue(metaVariable.Type, new MetaList<MetaValue>());
                    break;
                case ECategoryType.Dictionary:
                    this.value = new MetaValue(metaVariable.Type, new MetaDictionary<MetaValue, MetaValue>());
                    break;
            }
        }
        #endregion

        #region Equals
        public bool Equals(MetaInstanceVariable other)
        {
            if (!base.Equals(other)) return false;
            if (!this.name.Equals(other.name)) return false;
            if (!this.prefix.Equals(other.prefix)) return false;
            if (!this.nullable.Equals(other.nullable)) return false;
            if (!this.value.Equals(other.value)) return false;
            return true;
        }
        #endregion

        #region Methods
        public override bool Update(Model model)
        {
            if (!base.Update(model))
            {
                return false;
            }
            return this.value.Update(model);
        }

        public override void ReplaceType(MetaType oldType, MetaType newType)
        {
            base.ReplaceType(oldType, newType);
            this.value.ReplaceType(oldType, newType);
        }

        public override string ToString()
        {
            return string.Format("{0}{1} {2}", this.Value.Type.GetNameWithModule(), this.prefix, this.name);
        }
        #endregion

    }
}
