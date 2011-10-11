using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaInstanceVariable : MetaBase, IEquatable<MetaInstanceVariable>
    {
        #region Fields
        protected MetaVariable variable;
        protected MetaValue value;
        #endregion

        #region Properties
        public MetaVariable Variable
        {
            get { return this.variable; }
        }

        public MetaValue Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        #endregion

        #region Construct
        public MetaInstanceVariable(MetaVariable metaVariable)
            : base()
        {
            this.variable = metaVariable;
            this.value = new MetaValue(this.variable);
        }
        #endregion

        #region Equals
        public bool Equals(MetaInstanceVariable other)
        {
            if (Object.ReferenceEquals(this, other)) return true;
            if (!base.Equals(other)) return false;
            if (!this.variable.Equals(other.variable)) return false;
            if (!this.value.Equals(other.value)) return false;
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
            return this.value.Update(model);
        }

        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            base.UpdateType(oldType, newType);
            this.variable.UpdateType(oldType, newType);
            this.value.UpdateType(oldType, newType);
        }

        public override void UpdateVariable(MetaType metaType, MetaVariable oldVariable, MetaVariable newVariable)
        {
            base.UpdateVariable(metaType, oldVariable, newVariable);
            if (this.variable.Equals(oldVariable))
            {
                if (this.variable.Type.Equals(newVariable.Type))
                {
                    this.value.Reset(this.value);
                }
                else
                {
                    this.value.Reset(this.variable);
                }
                this.variable = newVariable;
            }
            this.value.UpdateVariable(metaType, oldVariable, newVariable);
        }

        public override void RemoveVariable(MetaType metaType, MetaVariable metaVariable)
        {
            base.RemoveVariable(metaType, metaVariable);
            this.value.RemoveVariable(metaType, metaVariable);
        }

        public override string ToString()
        {
            return string.Format("{0}{1} {2}", this.variable.Type.GetNameWithModule(), this.variable.Prefix, this.variable.Name);
        }
        #endregion

    }
}
