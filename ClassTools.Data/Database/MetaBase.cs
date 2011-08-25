using System;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaBase : Base, IEquatable<MetaBase>
    {
        #region Fields
        protected Repository repository;
        #endregion

        #region Properties
        public Repository Repository
        {
            get { return this.repository; }
            set { this.repository = value; }
        }
        #endregion

        #region Construct
        public MetaBase(Repository repository)
            : base()
        {
            this.repository = repository;
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
        public virtual void UpdateModel(Model model)
        {
        }
        #endregion

    }
}
