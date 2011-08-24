using System;
using System.Collections.Generic;

namespace ClassTools.Model
{
    [Serializable]
    public class MetaInstance : MetaModelDatabaseBase
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
        public MetaInstance(ModelDatabase database, MetaClass metaClass)
            : base(database)
        {
            this.className = metaClass.GetNameWithModule();
            this.instanceVariables = new List<MetaInstanceVariable>();
            foreach (MetaVariable variable in metaClass.AllVariables)
            {
                this.instanceVariables.Add(new MetaInstanceVariable(database, variable));
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
            instanceVariable.Database = this.database;
        }

        public override string ToString()
        {
            string result = this.className;
            foreach (MetaInstanceVariable variable in this.instanceVariables)
            {
                if ((variable.Name == "Name" || variable.Name == "name") && variable.ValueString.Trim('"') != string.Empty)
                {
                    return variable.ValueString.Trim('"');
                }
            }
            return this.className;
        }
        #endregion

    }
}
