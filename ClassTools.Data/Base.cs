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
        #endregion

    }
}
