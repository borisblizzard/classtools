using System;

namespace ClassTools.Model
{
    [Serializable]
    public class MetaInstanceVariable : MetaModelDatabaseBase
    {
        #region Fields
        protected string type;
        protected string name;
        protected string value;
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

        public string Value
        {
            get { return this.value; }
            set { this.value  = value; }
        }
        #endregion

        #region Constructors
        public MetaInstanceVariable(ModelDatabase database, MetaVariable metaVariable)
            : base(database)
        {
            this.type = metaVariable.Type.GetNameWithModule();
            this.name = metaVariable.Name;
            this.value = metaVariable.DefaultValue;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaInstanceVariable other)
        {
            if (!base.Equals(other)) return false;
            if (this.type != other.type) return false;
            if (this.name != other.name) return false;
            if (this.value != other.value) return false;
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
