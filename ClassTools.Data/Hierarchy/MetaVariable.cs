﻿using System;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaVariable : MetaMember, IEquatable<MetaVariable>
    {
        #region Fields
        protected string defaultValue;
        protected bool getter;
        protected bool setter;
        protected bool nullable;
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

        public bool Nullable
        {
            get { return this.nullable; }
            set { this.nullable = value; }
        }

        public bool CanSerialize
        {
            get { return this.canSerialize; }
            set { this.canSerialize = value; }
        }
        #endregion

        #region Construct
        public MetaVariable(string name, MetaType metaType)
            : base(name, metaType)
        {
            this.defaultValue = string.Empty;
            this.getter = false;
            this.setter = false;
            this.nullable = false;
            this.canSerialize = false;
        }
        #endregion

        #region Equals
        public bool Equals(MetaVariable other)
        {
            if (Object.ReferenceEquals(this, other)) return true;
            if (!base.Equals(other)) return false;
            if (!this.defaultValue.Equals(other.defaultValue)) return false;
            if (!this.getter.Equals(other.getter)) return false;
            if (!this.setter.Equals(other.setter)) return false;
            if (!this.nullable.Equals(other.nullable)) return false;
            if (!this.canSerialize.Equals(other.canSerialize)) return false;
            return true;
        }
        #endregion

    }
}
