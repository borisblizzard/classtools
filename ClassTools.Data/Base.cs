using System;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data
{
    [Serializable]
    public class Base : IEquatable<Base>
    {
        #region Equals
        public bool Equals(Base other)
        {
            if (other == null) return false;
            return true;
        }
        #endregion

        #region Methods
        public virtual bool Update(Model model)
        {
            return true;
        }

        public virtual void UpdateType(MetaType oldType, MetaType newType)
        {
        }

        public virtual void UpdateVariable(MetaType metaType, MetaVariable oldVariable, MetaVariable newVariable)
        {
        }

        public virtual void RemoveVariable(MetaType metaType, MetaVariable metaVariable)
        {
        }

        public virtual void UpdateParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter oldParameter, MetaParameter newParameter)
        {
        }

        public virtual void RemoveParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter metaParameter)
        {
        }
        #endregion

    }
}
