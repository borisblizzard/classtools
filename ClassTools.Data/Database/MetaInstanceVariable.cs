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
        protected string name;
        protected EValueType valueType;
        protected string valueString;
        protected MetaInstance valueInstance;
        protected MetaList<MetaInstanceVariable> valueInstanceCollection;
        protected MetaDictionary<MetaInstanceVariable, MetaInstanceVariable> valueInstanceDictionary;
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

        public EValueType ValueType
        {
            get { return this.valueType; }
        }

        public string ValueString
        {
            get { return this.valueString.Trim('"'); }
            set
            {
                this.valueType = EValueType.Integral;
                this.valueString = value;
                this.valueInstance = null;
                this.valueInstanceCollection = new MetaList<MetaInstanceVariable>();
                this.valueInstanceDictionary = new MetaDictionary<MetaInstanceVariable, MetaInstanceVariable>();
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
                this.valueType = EValueType.Integral;
                this.valueString = value.ToString().Replace(',', '.');
                this.valueInstance = null;
                this.valueInstanceCollection = new MetaList<MetaInstanceVariable>();
                this.valueInstanceDictionary = new MetaDictionary<MetaInstanceVariable, MetaInstanceVariable>();
            }
        }

        public bool ValueBool
        {
            get { return (this.valueString != "0" && this.valueString.ToLower() != "false"); }
            set
            {
                this.valueType = EValueType.Integral;
                this.valueString = (value ? "true" : "false");
                this.valueInstance = null;
                this.valueInstanceCollection = new MetaList<MetaInstanceVariable>();
                this.valueInstanceDictionary = new MetaDictionary<MetaInstanceVariable, MetaInstanceVariable>();
            }
        }

        public MetaInstance ValueInstance
        {
            get { return this.valueInstance; }
            set
            {
                this.valueType = EValueType.Object;
                this.valueInstance = value;
                this.valueString = "";
                this.valueInstanceCollection = new MetaList<MetaInstanceVariable>();
                this.valueInstanceDictionary = new MetaDictionary<MetaInstanceVariable, MetaInstanceVariable>();
            }
        }

        public MetaList<MetaInstanceVariable> ValueInstanceCollection
        {
            get { return this.valueInstanceCollection; }
            set
            {
                this.valueType = EValueType.Collection;
                this.valueInstanceCollection = value;
                this.valueString = "";
                this.valueInstance = null;
                this.valueInstanceDictionary = new MetaDictionary<MetaInstanceVariable, MetaInstanceVariable>();
            }
        }

        public MetaDictionary<MetaInstanceVariable, MetaInstanceVariable> ValueInstanceDictionary
        {
            get { return this.valueInstanceDictionary; }
            set
            {
                this.valueType = EValueType.Dictionary;
                this.valueInstanceDictionary = value;
                this.valueString = "";
                this.valueInstance = null;
                this.valueInstanceCollection = new MetaList<MetaInstanceVariable>();
            }
        }
        #endregion

        #region Construct
        public MetaInstanceVariable(Repository repository, MetaVariable variable)
            : base(repository)
        {
            this.type = variable.Type.GetNameWithModule();
            this.name = variable.Name;
            this.valueType = EValueType.Integral;
            this.valueString = variable.DefaultValue;
            this.valueInstance = null;
            this.valueInstanceCollection = new MetaList<MetaInstanceVariable>();
            this.valueInstanceDictionary = new MetaDictionary<MetaInstanceVariable, MetaInstanceVariable>();
        }
        #endregion

        #region Equals
        public bool Equals(MetaInstanceVariable other)
        {
            if (!base.Equals(other)) return false;
            if (!this.type.Equals(other.type)) return false;
            if (!this.name.Equals(other.name)) return false;
            if (!this.valueType.Equals(other.valueType)) return false;
            switch (valueType)
            {
                case EValueType.Integral:
                    if (!this.valueString.Equals(other.valueString)) return false;
                    break;
                case EValueType.Object:
                    if ((this.valueInstance != null) != (other.valueInstance != null)) return false;
                    if (this.valueInstance != null && !this.valueInstance.Equals(other.valueInstance)) return false;
                    break;
                case EValueType.Collection:
                    if (!this.valueInstanceCollection.Equals(other.valueInstanceCollection)) return false;
                    break;
                case EValueType.Dictionary:
                    if (!this.valueInstanceDictionary.Equals(other.valueInstanceDictionary)) return false;
                    break;
            }
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
