using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class Repository : Base, IEquatable<Repository>
    {
        #region Fields
        protected Model model;
        protected MetaDictionary<MetaClass, MetaList<MetaValue>> values;
        #endregion

        #region Properties
        public Model Model
        {
            get { return this.model; }
        }

        public MetaDictionary<MetaClass, MetaList<MetaValue>> Values
        {
            get { return this.values; }
        }
        #endregion

        #region Construct
        public Repository(Model model)
            : base()
        {
            this.model = model;
            this.values = new MetaDictionary<MetaClass, MetaList<MetaValue>>();
            MetaList<MetaClass> classes = model.LeafClasses;
            foreach (MetaClass metaClass in classes)
            {
                this.values[metaClass] = new MetaList<MetaValue>();
            }
        }
        #endregion

        #region Equals
        public bool Equals(Repository other)
        {
            if (!base.Equals(other)) return false;
            if (!this.model.Equals(other.model)) return false;
            if (!this.values.Equals(other.values)) return false;
            return true;
        }
        #endregion

        #region Methods
        public override bool Update(Model model)
        {
            this.model = model;
            // TODO - if values does not have a key anymore, allow user to swap classes
            MetaList<MetaClass> keys = this.values.GetKeys();
            MetaList<MetaClass> classes = model.LeafClasses;
            foreach (MetaClass metaClass in classes)
            {
                // cannot use "ContainsKey" because "GetHashCode" was not implemented
                // and ContainsKey seems to use it to determine object identity rather than "Equals"
                if (!keys.Contains(metaClass))
                {
                    this.values[metaClass] = new MetaList<MetaValue>();
                }
            }
            foreach (KeyValuePair<MetaClass, MetaList<MetaValue>> pair in this.values)
            {
                foreach (MetaValue value in pair.Value)
                {
                    if (!value.Update(model))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Values
        public void CreateNewValue(MetaClass metaClass, int index)
        {
            this.values[metaClass].Insert(index, new MetaValue(metaClass, new MetaInstance(metaClass)));
        }

        public void DeleteValueAt(MetaClass metaClass, int index)
        {
            this.values[metaClass].RemoveAt(index);
        }

        public bool TryValueMoveUp(MetaClass metaClass, int index)
        {
            return this.values[metaClass].TryMoveUp(index);
        }

        public bool TryValueMoveDown(MetaClass metaClass, int index)
        {
            return this.values[metaClass].TryMoveDown(index);
        }

        #endregion

    }

}
