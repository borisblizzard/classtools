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
        protected MetaDictionary<string, MetaList<MetaInstance>> instances;
        #endregion

        #region Properties
        public Model Model
        {
            get { return this.model; }
        }

        public MetaDictionary<string, MetaList<MetaInstance>> Instances
        {
            get { return this.instances; }
        }
        #endregion

        #region Construct
        public Repository(Model model)
            : base()
        {
            this.model = model;
            this.instances = new MetaDictionary<string, MetaList<MetaInstance>>();
            foreach (MetaClass metaClass in model.Classes)
            {
                this.instances[metaClass.GetNameWithModule()] = new MetaList<MetaInstance>();
            }
        }
        #endregion

        #region Equals
        public bool Equals(Repository other)
        {
            if (!base.Equals(other)) return false;
            if (!this.model.Equals(other.model)) return false;
            if (!this.instances.Equals(other.instances)) return false;
            return true;
        }
        #endregion

        #region Methods
        public MetaList<MetaInstance> GetInstances(MetaClass metaClass)
        {
            return this.instances[metaClass.GetNameWithModule()];
        }

        public void UpdateModel(Model model)
        {
            this.model = model;
            string name;
            foreach (MetaClass metaClass in model.Classes)
            {
                name = metaClass.GetNameWithModule();
                if (!this.instances.ContainsKey(name))
                {
                    this.instances[name] = new MetaList<MetaInstance>();
                }
            }
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
            MetaList<MetaInstance> metaInstances = this.instances[metaClass.GetNameWithModule()];
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
                MetaList<MetaInstance> metaInstances = this.instances[metaClass.GetNameWithModule()];
                MetaInstance metaInstance = metaInstances[index];
                metaInstances[index] = metaInstances[index - 1];
                metaInstances[index - 1] = metaInstance;
                return true;
            }
            return false;
        }

        public bool TryInstanceMoveDown(MetaClass metaClass, int index)
        {
            MetaList<MetaInstance> metaInstances = this.instances[metaClass.GetNameWithModule()];
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
