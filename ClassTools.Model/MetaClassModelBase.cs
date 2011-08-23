using System;

namespace ClassTools.Model
{
    [Serializable]
    public class MetaClassModelBase
    {
        #region Fields
        protected ClassModel model;
        protected string name;
        protected string prefix;
        #endregion

        #region Properties
        public ClassModel Model
        {
            get { return this.model; }
            set { this.model = value; }
        }

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

        #region Constructors
        public MetaClassModelBase(ClassModel model, string name)
        {
            this.model = model;
            this.name = name;
            this.prefix = string.Empty;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaClassModelBase other)
        {
            if (this.name != other.name) return false;
            if (this.prefix != other.prefix) return false;
            return true;
        }
        #endregion

        #region Methods
        public virtual void UpdateType(MetaType oldType, MetaType newType)
        {
        }

        public override string ToString()
        {
            return this.Name;
        }
        #endregion
    }
}
