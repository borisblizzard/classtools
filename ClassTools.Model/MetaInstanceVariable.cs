using System;
using System.Collections.Generic;

namespace ClassTools.Model
{
    [Serializable]
    public class MetaInstanceVariable : MetaModelDatabaseBase
    {
        #region Fields
        protected string type;
        protected string name;
        protected string valueString;
        protected MetaInstance valueInstance;
        protected List<MetaInstanceVariable> valueInstanceCollection;
        protected Dictionary<MetaInstanceVariable, MetaInstanceVariable> valueInstanceDictionary;
        #endregion

        #region Properties
        public string Type
        {
            get { return this.type; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string ValueString
        {
            get { return this.valueString.Trim('"'); }
            set
            {
                this.valueString = value;
                this.valueInstance = null;
                this.valueInstanceCollection = null;
                this.valueInstanceDictionary = null;
            }
        }

        public decimal ValueDecimal
        {
            get
            {
                decimal result = decimal.Zero;
                decimal.TryParse(this.valueString, out result);
                return result;
            }
            set
            {
                this.valueString = value.ToString().Replace(',', '.');
                this.valueInstance = null;
                this.valueInstanceCollection = null;
                this.valueInstanceDictionary = null;
            }
        }

        public bool ValueBool
        {
            get { return (this.valueString != "0" && this.valueString.ToLower() != "false"); }
            set
            {
                this.valueString = (value ? "true" : "false");
                this.valueInstance = null;
                this.valueInstanceCollection = null;
                this.valueInstanceDictionary = null;
            }
        }

        public MetaInstance ValueInstance
        {
            get { return this.valueInstance; }
            set
            {
                this.valueInstance = value;
                this.valueString = "";
                this.valueInstanceCollection = null;
                this.valueInstanceDictionary = null;
            }
        }

        public List<MetaInstanceVariable> ValueInstanceCollection
        {
            get { return this.valueInstanceCollection; }
            set
            {
                this.valueInstanceCollection = value;
                this.valueString = "";
                this.valueInstance = null;
                this.valueInstanceDictionary = null;
            }
        }

        public Dictionary<MetaInstanceVariable, MetaInstanceVariable> ValueInstanceDictionary
        {
            get { return this.valueInstanceDictionary; }
            set
            {
                this.valueInstanceDictionary = value;
                this.valueString = "";
                this.valueInstance = null;
                this.valueInstanceCollection = null;
            }
        }
        #endregion

        #region Constructors
        public MetaInstanceVariable(ModelDatabase database, MetaVariable metaVariable)
            : base(database)
        {
            this.type = metaVariable.Type.GetNameWithModule();
            this.name = metaVariable.Name;
            this.valueString = metaVariable.DefaultValue;
            this.valueInstance = null;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaInstanceVariable other)
        {
            if (!base.Equals(other)) return false;
            if (this.type != other.type) return false;
            if (this.name != other.name) return false;
            if (this.valueString != other.valueString) return false;
            if (this.valueInstance != null && other.valueInstance != null)
            {
                if (!this.valueInstance.Equals(other.valueInstance)) return false;
            }
            else if (this.valueInstance != null || other.valueInstance != null) return false;

            if (this.valueInstance != null && other.valueInstance != null)
            {
                if (!this.valueInstance.Equals(other.valueInstance)) return false;
            }
            else if (this.valueInstance != null || other.valueInstance != null) return false;
            return true;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return (this.type + " " + this.name);
        }
        #endregion

    }
}
