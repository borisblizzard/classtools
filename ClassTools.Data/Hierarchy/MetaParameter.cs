using System;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaParameter : MetaVariable, IEquatable<MetaParameter>
    {
        #region Construct
        public MetaParameter(string name, MetaType metaType)
            : base(name, metaType)
        {
        }
        #endregion

        #region Equals
        public bool Equals(MetaParameter other)
        {
            if (Object.ReferenceEquals(this, other)) return true;
            if (!base.Equals(other)) return false;
            return true;
        }
        #endregion

    }
}
