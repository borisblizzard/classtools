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
        public MetaMember(string name, MetaType metaType)
            : base(name)
        {
            this.type = metaType;
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
        public override bool Update(Model model)
        {
            if (!base.Update(model))
            {
                return false;
            }
            MetaType metaType = model.FindMatchingType(this.type);
            if (metaType == null)
            {
                return false;
            }
            this.type = metaType;
            return this.type.Update(model);
        }

        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            base.UpdateType(oldType, newType);
            if (this.type.Equals(oldType))
            {
                this.type = newType;
            }
        }

        public override void UpdateVariable(MetaVariable oldVariable, MetaVariable newVariable)
        {
            base.UpdateVariable(oldVariable, newVariable);
            this.type.UpdateVariable(oldVariable, newVariable);
        }

        public override void RemoveVariable(MetaVariable metaVariable)
        {
            base.RemoveVariable(metaVariable);
            this.type.RemoveVariable(metaVariable);
        }

        public override string ToString()
        {
            return string.Format("{0}{1} {2}", this.type.ToString(), this.prefix, base.ToString());
        }
        #endregion

    }
}
