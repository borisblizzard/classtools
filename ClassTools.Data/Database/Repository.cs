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
        protected MetaDictionary<string, MetaList<MetaValue>> values;
        #endregion

        #region Properties
        public Model Model
        {
            get { return this.model; }
        }

        public MetaDictionary<string, MetaList<MetaValue>> Values
        {
            get { return this.values; }
        }
        #endregion

        #region Construct
        public Repository(Model model)
            : base()
        {
            this.model = model;
            this.values = new MetaDictionary<string, MetaList<MetaValue>>();
            foreach (MetaClass metaClass in model.Classes)
            {
                this.values[metaClass.GetNameWithModule()] = new MetaList<MetaValue>();
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
        public MetaList<MetaValue> GetValues(MetaClass metaClass)
        {
            return this.values[metaClass.GetNameWithModule()];
        }

        public void UpdateModel(Model model)
        {
            this.model = model;
            string name;
            foreach (MetaClass metaClass in model.Classes)
            {
                name = metaClass.GetNameWithModule();
                if (!this.values.ContainsKey(name))
                {
                    this.values[name] = new MetaList<MetaValue>();
                }
            }
        }
        #endregion

        #region Values
        public void CreateNewValue(MetaClass metaClass, int index)
        {
            this.GetValues(metaClass).Insert(index, new MetaValue(this, metaClass, new MetaInstance(this, metaClass)));
        }

        public void DeleteInstance(MetaClass metaClass, MetaValue metaValue)
        {
            this.GetValues(metaClass).Remove(metaValue);
        }

        public void DeleteValueAt(MetaClass metaClass, int index)
        {
            this.GetValues(metaClass).RemoveAt(index);
        }

        public void ReplaceValueAt(MetaClass metaClass, int index, MetaValue metaValue)
        {
            MetaList<MetaValue> metaValues = this.GetValues(metaClass);
            metaValues[index] = metaValue;
            metaValue.Repository = this;
            for (int i = 0; i < metaValue.Instance.InstanceVariables.Count; i++)
            {
                metaValue.Instance.ReplaceInstanceVariableAt(i, metaValue.Instance.InstanceVariables[i]);
            }
        }

        public bool TryValueMoveUp(MetaClass metaClass, int index)
        {
            if (index > 0)
            {
                MetaList<MetaValue> metaValues = this.GetValues(metaClass);
                MetaValue metaValue = metaValues[index];
                metaValues[index] = metaValues[index - 1];
                metaValues[index - 1] = metaValue;
                return true;
            }
            return false;
        }

        public bool TryValueMoveDown(MetaClass metaClass, int index)
        {
            MetaList<MetaValue> metaValues = this.GetValues(metaClass);
            if (index < metaValues.Count - 1)
            {
                MetaValue metaValue = metaValues[index];
                metaValues[index] = metaValues[index + 1];
                metaValues[index + 1] = metaValue;
                return true;
            }
            return false;
        }

        #endregion

    }

}
