using System;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaVariable : MetaMember, IEquatable<MetaVariable>
    {
        #region Fields
        protected string defaultValue;
        protected bool getter;
        protected bool setter;
        protected bool canSerialize;
        #endregion

        #region Properties
        public string DefaultValue
        {
            get { return this.defaultValue; }
            set { this.defaultValue = value; }
        }

        public bool Getter
        {
            get { return this.getter; }
            set { this.getter = value; }
        }

        public bool Setter
        {
            get { return this.setter; }
            set { this.setter = value; }
        }

        public bool CanSerialize
        {
            get { return this.canSerialize; }
            set { this.canSerialize = value; }
        }
        #endregion

        #region Construct
        public MetaVariable(Model model)
            : base(model, "ANON_VARIABLE")
        {
            this.defaultValue = string.Empty;
            this.getter = false;
            this.setter = false;
        }
        #endregion

        #region Equals
        public bool Equals(MetaVariable other)
        {
            if (!base.Equals(other)) return false;
            if (!this.defaultValue.Equals(other.defaultValue)) return false;
            if (!this.getter.Equals(other.getter)) return false;
            if (!this.setter.Equals(other.setter)) return false;
            if (!this.canSerialize.Equals(other.canSerialize)) return false;
            return true;
        }
        #endregion

    }
}
