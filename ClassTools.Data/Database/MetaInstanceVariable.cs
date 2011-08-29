using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaInstanceVariable : MetaBase, IEquatable<MetaInstanceVariable>
    {
        #region Fields
        protected string type;
        protected string prefix;
        protected string name;
        protected MetaValue value;
        #endregion

        #region Properties
        public string Type
        {
            get { return this.type; }
        }

        public string Prefix
        {
            get { return this.prefix; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public MetaValue Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        #endregion

        #region Construct
        public MetaInstanceVariable(Repository repository, MetaVariable metaVariable)
            : base(repository)
        {
            this.type = metaVariable.Type.GetNameWithModule();
            this.prefix = metaVariable.Prefix;
            this.name = metaVariable.Name;
            switch (metaVariable.Type.CategoryType)
            {
                case ECategoryType.Integral:
                    this.value = new MetaValue(repository, metaVariable.Type, metaVariable.DefaultValue);
                    break;
                case ECategoryType.Class:
                    MetaClass metaClass = (MetaClass)metaVariable.Type;
                    this.value = new MetaValue(repository, metaClass, metaVariable.Nullable ? null : new MetaInstance(repository, metaClass));
                    break;
                case ECategoryType.List:
                    this.value = new MetaValue(repository, metaVariable.Type, new MetaList<MetaValue>());
                    break;
                case ECategoryType.Dictionary:
                    this.value = new MetaValue(repository, metaVariable.Type, new MetaDictionary<MetaValue, MetaValue>());
                    break;
            }
        }
        #endregion

        #region Equals
        public bool Equals(MetaInstanceVariable other)
        {
            if (!base.Equals(other)) return false;
            if (!this.type.Equals(other.type)) return false;
            if (!this.name.Equals(other.name)) return false;
            if (!this.prefix.Equals(other.prefix)) return false;
            if (!this.value.Equals(other.value)) return false;
            return true;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format("{0}{1} {2}", this.type, this.prefix, this.name);
        }
        #endregion

    }
}
