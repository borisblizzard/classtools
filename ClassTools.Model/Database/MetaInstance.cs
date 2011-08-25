using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaInstance : MetaBase
    {
        #region Fields
        protected string className;
        protected List<MetaInstanceVariable> instanceVariables;
        #endregion

        #region Properties
        public string ClassName
        {
            get { return this.className; }
            set { this.className = value; }
        }

        public List<MetaInstanceVariable> InstanceVariables
        {
            get { return this.instanceVariables; }
        }
        #endregion

        #region Constructors
        public MetaInstance(Repository repository, MetaClass metaClass)
            : base(repository)
        {
            this.className = metaClass.GetNameWithModule();
            this.instanceVariables = new List<MetaInstanceVariable>();
            foreach (MetaVariable metaVariable in metaClass.AllVariables)
            {
                this.instanceVariables.Add(new MetaInstanceVariable(repository, metaVariable));
            }
        }
        #endregion

        #region Behavior
        public bool Equals(MetaInstance other)
        {
            if (!base.Equals(other)) return false;
            if (this.className != other.className) return false;
            if (!Utility.ListEquals(this.instanceVariables, other.instanceVariables)) return false;
            return true;
        }
        #endregion

        #region Methods
        public void ReplaceInstanceVariableAt(int index, MetaInstanceVariable instanceVariable)
        {
            this.instanceVariables[index] = instanceVariable;
            instanceVariable.Repository = this.repository;
        }

        public override string ToString()
        {
            string result = this.className;
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                if ((metaInstanceVariable.Name == "Name" || metaInstanceVariable.Name == "name") && metaInstanceVariable.ValueString.Trim('"') != string.Empty)
                {
                    return metaInstanceVariable.ValueString.Trim('"');
                }
            }
            return this.className;
        }
        #endregion

    }
}
