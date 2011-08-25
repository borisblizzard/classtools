using System;
using System.Collections.Generic;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaMethod : MetaMember, IEquatable<MetaMethod>
    {
        #region Fields
        protected MetaList<MetaVariable> parameters;
        protected string implementation;
        #endregion

        #region Properties
        public MetaList<MetaVariable> Parameters
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

        #region Construct
        public MetaMethod(Model model)
            : base(model, "ANON_METHOD")
        {
            this.parameters = new MetaList<MetaVariable>();
            this.implementation = string.Empty;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaMethod other)
        {
            if (!base.Equals(other)) return false;
            if (!this.parameters.Equals(other.parameters)) return false;
            if (!this.implementation.Equals(other.implementation)) return false;
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
            return this.parameters.TryMoveUp(index);
        }

        public bool TryParameterMoveDown(int index)
        {
            return this.parameters.TryMoveDown(index);
        }
        #endregion

    }
}
