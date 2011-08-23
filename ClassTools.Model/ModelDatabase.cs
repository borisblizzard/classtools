﻿using System;
using System.Collections.Generic;

namespace ClassTools.Model
{
    [Serializable]
    public class ModelDatabase
    {
        #region Fields
        protected ClassModel model;
        protected Dictionary<string, List<MetaInstance>> instances;
        #endregion

        #region Properties
        public ClassModel Model
        {
            get { return this.model; }
        }
        #endregion

        #region Constructors
        public ModelDatabase(ClassModel model)
        {
            this.model = model;
            this.instances = new Dictionary<string, List<MetaInstance>>();
            foreach (MetaClass classe in model.Classes)
            {
                this.instances[classe.GetNameWithModule()] = new List<MetaInstance>();
            }
        }
        #endregion

        #region Behavior
        public virtual bool Equals(ModelDatabase other)
        {
            if (!this.model.Equals(other.model)) return false;
            return true;
        }
        #endregion

        #region Methods
        public List<MetaInstance> GetInstances(MetaClass classe)
        {
            return this.instances[classe.GetNameWithModule()];
        }

        public void UpdateModel(ClassModel model)
        {
            this.model = model;
            //this.instances.
        }
        #endregion

        #region Instances
        public MetaInstance CreateNewInstance(MetaClass classe, int index)
        {
            MetaInstance instance = new MetaInstance(this, classe);
            this.instances[classe.GetNameWithModule()].Insert(index, instance);
            return instance;
        }

        public void DeleteInstance(MetaClass classe, MetaInstance instance)
        {
            this.instances[classe.GetNameWithModule()].Remove(instance);
        }

        public void DeleteInstanceAt(MetaClass classe, int index)
        {
            this.instances[classe.GetNameWithModule()].RemoveAt(index);
        }

        public void ReplaceInstanceAt(MetaClass classe, int index, MetaInstance instance)
        {
            List<MetaInstance> instances = this.instances[classe.GetNameWithModule()];
            MetaInstance oldInstance = instances[index];
            instances[index] = instance;
            instance.Database = this;
            for (int i = 0; i < instance.InstanceVariables.Count; i++)
            {
                instance.ReplaceInstanceVariableAt(i, instance.InstanceVariables[i]);
            }
        }

        public bool TryInstanceMoveUp(MetaClass classe, int index)
        {
            if (index > 0)
            {
                List<MetaInstance> instances = this.instances[classe.GetNameWithModule()];
                MetaInstance instance = instances[index];
                instances[index] = instances[index - 1];
                instances[index - 1] = instance;
                return true;
            }
            return false;
        }

        public bool TryInstanceMoveDown(MetaClass classe, int index)
        {
            List<MetaInstance> instances = this.instances[classe.GetNameWithModule()];
            if (index < instances.Count - 1)
            {
                MetaInstance instance = instances[index];
                instances[index] = instances[index + 1];
                instances[index + 1] = instance;
                return true;
            }
            return false;
        }

        #endregion

    }

}
