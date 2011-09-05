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
        
        public virtual void UpdateVariable(MetaVariable oldVariable, MetaVariable newVariable)
        {
        }
        #endregion

    }
}
