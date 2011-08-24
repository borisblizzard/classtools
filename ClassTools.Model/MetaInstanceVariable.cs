using System;

namespace ClassTools.Model
{
    [Serializable]
    public class MetaInstanceVariable : MetaModelDatabaseBase
    {
        #region Fields
        protected string type;
        protected string name;
        protected string valueString;
        protected object valueObject;
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
                this.valueObject = null;
                this.valueString = value;
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
                this.valueObject = null;
                this.valueString = value.ToString().Replace(',', '.');
            }
        }

        public bool ValueBool
        {
            get { return (this.valueString != "0" && this.valueString.ToLower() != "false"); }
            set
            {
                this.valueObject = null;
                this.valueString = (value ? "true" : "false");
            }
        }

        public object ValueObject
        {
            get { return this.valueObject; }
            set
            {
                this.valueString = "";
                this.valueObject = value;
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
            this.valueObject = null;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaInstanceVariable other)
        {
            if (!base.Equals(other)) return false;
            if (this.type != other.type) return false;
            if (this.name != other.name) return false;
            if (this.valueString != other.valueString) return false;
            if (this.valueObject != other.valueObject) return false;
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
