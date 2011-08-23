using System;

namespace ClassTools.Model
{
    [Serializable]
    public class MetaModelDatabaseBase
    {
        #region Fields
        protected ModelDatabase database;
        #endregion

        #region Properties
        public ModelDatabase Database
        {
            get { return this.database; }
            set { this.database = value; }
        }
        #endregion

        #region Constructors
        public MetaModelDatabaseBase(ModelDatabase database)
        {
            this.database = database;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaModelDatabaseBase other)
        {
            return true;
        }
        #endregion

        #region Methods
        public virtual void UpdateModel(ClassModel model)
        {
        }
        #endregion

    }
}
