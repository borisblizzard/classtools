using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaInstance : MetaBase, IEquatable<MetaInstance>
    {
        #region Fields
        protected MetaType type;
        protected MetaList<MetaInstanceVariable> instanceVariables;
        #endregion

        #region Properties
        public MetaType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public MetaList<MetaInstanceVariable> InstanceVariables
        {
            get { return this.instanceVariables; }
        }
        #endregion

        #region Construct
        public MetaInstance(MetaClass metaClass)
            : base()
        {
            this.type = metaClass;
            this.instanceVariables = new MetaList<MetaInstanceVariable>();
            foreach (MetaVariable metaVariable in metaClass.AllVariables)
            {
                this.instanceVariables.Add(new MetaInstanceVariable(metaVariable));
            }
        }
        #endregion

        #region Equals
        public bool Equals(MetaInstance other)
        {
            if (!base.Equals(other)) return false;
            if (!this.type.Equals(other.type)) return false;
            if (!this.instanceVariables.Equals(other.instanceVariables)) return false;
            return true;
        }
        #endregion

        #region Methods
        public override bool Update(Model model)
        {
            if (!base.Update(model))
            {
                return false;
            }
            MetaClass oldClass = (MetaClass)this.type;
            MetaType metaType = model.FindMatchingType(this.type);
            if (metaType == null)
            {
                return false;
            }
            this.type = metaType;
            MetaClass newClass = (MetaClass)this.type;
            MetaInstanceVariable instanceVariable;
            MetaList<MetaInstanceVariable> metaInstanceVariables = new MetaList<MetaInstanceVariable>(this.instanceVariables);
            this.instanceVariables.Clear();
            foreach (MetaVariable metaVariable in newClass.AllVariables)
            {
                instanceVariable = metaInstanceVariables.Find(iv => iv.Variable.Equals(metaVariable));
                if (instanceVariable != null)
                {
                    this.instanceVariables.Add(instanceVariable);
                }
                else
                {
                    instanceVariable = metaInstanceVariables.Find(iv => iv.Variable.Equals(metaVariable));
                    this.instanceVariables.Add(new MetaInstanceVariable(metaVariable));
                }
            }
            return true;
        }

        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            base.UpdateType(oldType, newType);
            if (this.type.Matches(oldType, newType))
            {
                this.type = newType;
            }
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                metaInstanceVariable.UpdateType(oldType, newType);
            }
        }

        public override void UpdateVariable(MetaVariable oldVariable, MetaVariable newVariable)
        {
            base.UpdateVariable(oldVariable, newVariable);
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                metaInstanceVariable.UpdateVariable(oldVariable, newVariable);
            }
        }

        public override void RemoveVariable(MetaVariable metaVariable)
        {
            base.RemoveVariable(metaVariable);
            for (int i = 0; i < this.instanceVariables.Count; i++)
            {
                if (this.instanceVariables[i].Variable.Equals(metaVariable))
                {
                    this.instanceVariables.RemoveAt(i);
                    i--;
                }
                else
                {
                    this.instanceVariables[i].RemoveVariable(metaVariable);
                }
            }
        }

        public override string ToString()
        {
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                if ((metaInstanceVariable.Variable.Name == "Name" || metaInstanceVariable.Variable.Name == "name") && metaInstanceVariable.Value.String.Trim('"') != string.Empty)
                {
                    return metaInstanceVariable.Value.String.Trim('"');
                }
            }
            return this.type.Name;
        }
        #endregion

    }
}
