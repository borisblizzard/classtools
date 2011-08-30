using System;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaBase : Base, IEquatable<MetaBase>
    {
        #region Construct
        public MetaBase()
            : base()
        {
        }
        #endregion

        #region Equals
        public bool Equals(MetaBase other)
        {
            if (!base.Equals(other)) return false;
            return true;
        }
        #endregion

        #region Methods
        public virtual void ReplaceType(MetaType oldType, MetaType newType)
        {
        }
        #endregion

    }
}
