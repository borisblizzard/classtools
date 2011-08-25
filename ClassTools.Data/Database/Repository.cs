using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class Repository
    {
        #region Fields
        protected Model model;
        protected Dictionary<string, List<MetaInstance>> instances;
        #endregion

        #region Properties
        public Model Model
        {
            get { return this.model; }
        }
        #endregion

        #region Constructors
        public Repository(Model model)
        {
            this.model = model;
            this.instances = new Dictionary<string, List<MetaInstance>>();
            foreach (MetaClass metaClass in model.Classes)
            {
                this.instances[metaClass.GetNameWithModule()] = new List<MetaInstance>();
            }
        }
        #endregion

        #region Behavior
        public virtual bool Equals(Repository other)
        {
            if (!this.model.Equals(other.model)) return false;
            if (!Utility.DictionaryEquals(this.instances, other.instances)) return false;
            return true;
        }
        #endregion

        #region Methods
        public List<MetaInstance> GetInstances(MetaClass metaClass)
        {
            return this.instances[metaClass.GetNameWithModule()];
        }

        public void UpdateModel(Model model)
        {
            this.model = model;
            //this.instances.
        }
        #endregion

        #region Instances
        public MetaInstance CreateNewInstance(MetaClass metaClass, int index)
        {
            MetaInstance metaInstance = new MetaInstance(this, metaClass);
            this.instances[metaClass.GetNameWithModule()].Insert(index, metaInstance);
            return metaInstance;
        }

        public void DeleteInstance(MetaClass metaClass, MetaInstance metaInstance)
        {
            this.instances[metaClass.GetNameWithModule()].Remove(metaInstance);
        }

        public void DeleteInstanceAt(MetaClass metaClass, int index)
        {
            this.instances[metaClass.GetNameWithModule()].RemoveAt(index);
        }

        public void ReplaceInstanceAt(MetaClass metaClass, int index, MetaInstance metaInstance)
        {
            List<MetaInstance> metaInstances = this.instances[metaClass.GetNameWithModule()];
            metaInstances[index] = metaInstance;
            metaInstance.Repository = this;
            for (int i = 0; i < metaInstance.InstanceVariables.Count; i++)
            {
                metaInstance.ReplaceInstanceVariableAt(i, metaInstance.InstanceVariables[i]);
            }
        }

        public bool TryInstanceMoveUp(MetaClass metaClass, int index)
        {
            if (index > 0)
            {
                List<MetaInstance> metaInstances = this.instances[metaClass.GetNameWithModule()];
                MetaInstance metaInstance = metaInstances[index];
                metaInstances[index] = metaInstances[index - 1];
                metaInstances[index - 1] = metaInstance;
                return true;
            }
            return false;
        }

        public bool TryInstanceMoveDown(MetaClass metaClass, int index)
        {
            List<MetaInstance> metaInstances = this.instances[metaClass.GetNameWithModule()];
            if (index < metaInstances.Count - 1)
            {
                MetaInstance metaInstance = metaInstances[index];
                metaInstances[index] = metaInstances[index + 1];
                metaInstances[index + 1] = metaInstance;
                return true;
            }
            return false;
        }

        #endregion

    }

}
