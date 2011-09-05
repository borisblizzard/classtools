using System;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaBase : Base, IEquatable<MetaBase>
    {
        #region Fields
        protected string name;
        protected string prefix;
        #endregion

        #region Properties
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Prefix
        {
            get { return this.prefix; }
            set { this.prefix = value; }
        }
        #endregion

        #region Construct
        public MetaBase(string name)
            : base()
        {
            this.name = name;
            this.prefix = string.Empty;
        }
        #endregion

        #region Equals
        public bool Equals(MetaBase other)
        {
            if (!base.Equals(other)) return false;
            if (!this.name.Equals(other.name)) return false;
            if (!this.prefix.Equals(other.prefix)) return false;
            return true;
        }
        #endregion

        #region Methods
        public override bool Update(Model model)
        {
            return base.Update(model);
        }

        public override string ToString()
        {
            return this.Name;
        }
        #endregion

    }
}
