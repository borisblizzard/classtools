using System;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaBase
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

        #region Constructors
        public MetaBase(Repository repository)
        {
            this.repository = repository;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaBase other)
        {
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
