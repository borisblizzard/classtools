using System;
using System.Collections.Generic;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaMember : MetaBase, IEquatable<MetaMember>
    {
        #region Fields
        protected MetaType type;
        protected EAccessType accessType;
        #endregion

        #region Properties
        public MetaType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public EAccessType AccessType
        {
            get { return this.accessType; }
            set { this.accessType = value; }
        }
        #endregion

        #region Construct
        public MetaMember(Model model, string name)
            : base(model, name)
        {
            this.type = model.AllTypes[0];
            this.accessType = EAccessType.Public;
        }
        #endregion

        #region Equals
        public bool Equals(MetaMember other)
        {
            if (!base.Equals(other)) return false;
            if (!this.type.Equals(other.type)) return false;
            if (!this.accessType.Equals(other.accessType)) return false;
            return true;
        }
        #endregion

        #region Methods
        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            base.UpdateType(oldType, newType);
            if (this.Type == oldType)
            {
                this.Type = oldType;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1} {2}", this.type.ToString(), this.prefix, base.ToString());
        }
        #endregion

    }
}
