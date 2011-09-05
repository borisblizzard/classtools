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
            this.resetValue();
        }

        private void resetValue()
        {
            switch (this.variable.Type.CategoryType)
            {
                case ECategoryType.Integral:
                    this.value = new MetaValue(this.variable.Type, this.variable.DefaultValue);
                    break;
                case ECategoryType.Class:
                    MetaClass metaClass = (MetaClass)this.variable.Type;
                    this.value = new MetaValue(metaClass, this.variable.Nullable ? null : new MetaInstance(metaClass));
                    break;
                case ECategoryType.List:
                    this.value = new MetaValue(this.variable.Type, new MetaList<MetaValue>());
                    break;
                case ECategoryType.Dictionary:
                    this.value = new MetaValue(this.variable.Type, new MetaDictionary<MetaValue, MetaValue>());
                    break;
            }
        }

        private void resetValue(MetaValue metaValue)
        {
            switch (this.variable.Type.CategoryType)
            {
                case ECategoryType.Integral:
                    this.value = new MetaValue(this.variable.Type, metaValue.String);
                    break;
                case ECategoryType.Class:
                    MetaClass metaClass = (MetaClass)this.variable.Type;
                    this.value = new MetaValue(metaClass, (this.variable.Nullable || metaValue.Instance != null) ? metaValue.Instance : new MetaInstance(metaClass));
                    break;
                case ECategoryType.List:
                    this.value = new MetaValue(this.variable.Type, metaValue.List);
                    break;
                case ECategoryType.Dictionary:
                    this.value = new MetaValue(this.variable.Type, metaValue.Dictionary);
                    break;
            }
        }
        #endregion

        #region Equals
        public bool Equals(MetaInstanceVariable other)
        {
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

        public override void UpdateVariable(MetaVariable oldVariable, MetaVariable newVariable)
        {
            if (this.variable.Equals(oldVariable))
            {
                if (this.variable.Type.Equals(newVariable.Type))
                {
                    this.resetValue(this.value);
                }
                else
                {
                    this.resetValue();
                }
                this.variable = newVariable;
            }
            this.value.UpdateVariable(oldVariable, newVariable);
        }

        public override string ToString()
        {
            return string.Format("{0}{1} {2}", this.variable.Type.GetNameWithModule(), this.variable.Prefix, this.variable.Name);
        }
        #endregion

    }
}
