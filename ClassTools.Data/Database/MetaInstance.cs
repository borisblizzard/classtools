using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaInstance : MetaBase, IEquatable<MetaInstance>
    {
        #region Fields
        protected string className;
        protected MetaList<MetaInstanceVariable> instanceVariables;
        #endregion

        #region Properties
        public string ClassName
        {
            get { return this.className; }
            set { this.className = value; }
        }

        public MetaList<MetaInstanceVariable> InstanceVariables
        {
            get { return this.instanceVariables; }
        }
        #endregion

        #region Construct
        public MetaInstance(Repository repository, MetaClass metaClass)
            : base(repository)
        {
            this.className = metaClass.GetNameWithModule();
            this.instanceVariables = new MetaList<MetaInstanceVariable>();
            foreach (MetaVariable metaVariable in metaClass.AllVariables)
            {
                this.instanceVariables.Add(new MetaInstanceVariable(repository, metaVariable));
            }
        }
        #endregion

        #region Equals
        public bool Equals(MetaInstance other)
        {
            if (!base.Equals(other)) return false;
            if (!this.className.Equals(other.className)) return false;
            if (!this.instanceVariables.Equals(other.instanceVariables)) return false;
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
