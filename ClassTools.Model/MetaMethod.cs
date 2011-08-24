using System;
using System.Collections.Generic;

namespace ClassTools.Model
{
    [Serializable]
    public class MetaMethod : MetaMember
    {
        #region Fields
        protected List<MetaVariable> parameters;
        protected string implementation;
        #endregion

        #region Properties
        public List<MetaVariable> Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }

        public string Implementation
        {
            get { return this.implementation; }
            set { this.implementation = value; }
        }
        #endregion

        #region Constructors
        public MetaMethod(ClassModel model)
            : base(model, "ANON_METHOD")
        {
            this.parameters = new List<MetaVariable>();
            this.implementation = string.Empty;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaMethod other)
        {
            if (!base.Equals(other)) return false;
            if (!Utility.ListEquals(this.parameters, other.parameters)) return false;
            if (this.implementation != other.implementation) return false;
            return true;
        }
        #endregion

        #region Methods
        public override void UpdateType(MetaType oldType, MetaType newType)
        {
 	         base.UpdateType(oldType, newType);
             for (int i = 0; i < this.Parameters.Count; i++)
             {
                 this.Parameters[i].UpdateType(oldType, newType);
             }
        }
        #endregion

        #region Parameters
        public MetaVariable CreateNewParameter(int index)
        {
            MetaVariable parameter = new MetaVariable(this.model);
            this.parameters.Insert(index, parameter);
            return parameter;
        }

        public void DeleteParameterAt(int index)
        {
            this.parameters.RemoveAt(index);
        }

        public void ReplaceParameterAt(int index, MetaVariable parameter)
        {
            this.Parameters[index] = parameter;
            parameter.Model = this.model;
            parameter.Type = this.model.Types.Find(t => t.Equals(parameter.Type));
        }

        public bool TryParameterMoveUp(int index)
        {
            if (index > 0)
            {
                MetaVariable parameter = this.parameters[index];
                this.parameters[index] = this.parameters[index - 1];
                this.parameters[index - 1] = parameter;
                return true;
            }
            return false;
        }

        public bool TryParameterMoveDown(int index)
        {
            if (index < this.parameters.Count - 1)
            {
                MetaVariable parameter = this.parameters[index];
                this.parameters[index] = this.parameters[index + 1];
                this.parameters[index + 1] = parameter;
                return true;
            }
            return false;
        }
        #endregion

    }
}
