using System;
using System.Collections.Generic;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaMember : MetaBase
    {
        #region Fields
        protected MetaType type;
        protected EAccess access;
        #endregion

        #region Properties
        public MetaType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public EAccess Access
        {
            get { return this.access; }
            set { this.access = value; }
        }
        #endregion

        #region Constructors
        public MetaMember(Model model, string name)
            : base(model, name)
        {
            this.type = model.Types[0];
            this.access = EAccess.Public;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaMember other)
        {
            if (!base.Equals(other)) return false;
            if (!this.type.Equals(other.type)) return false;
            if (this.access != other.access) return false;
            return true;
        }
        #endregion

        #region Behavior
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
