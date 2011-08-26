using System;

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

    }
}
