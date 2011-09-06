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
        public MetaMethod(string name, MetaType metaType)
            : base(name, metaType)
        {
            this.parameters = new MetaList<MetaVariable>();
            this.implementation = string.Empty;
        }
        #endregion

        #region Equals
        public bool Equals(MetaMethod other)
        {
            if (!base.Equals(other)) return false;
            if (!this.parameters.Equals(other.parameters)) return false;
            if (!this.implementation.Equals(other.implementation)) return false;
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
            foreach (MetaVariable parameter in this.parameters)
            {
                if (!parameter.Update(model))
                {
                    return false;
                }
            }
            return true;
        }

        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            base.UpdateType(oldType, newType);
            foreach (MetaVariable parameter in this.parameters)
            {
                parameter.UpdateType(oldType, newType);
            }
        }

        public override void UpdateVariable(MetaVariable oldVariable, MetaVariable newVariable)
        {
            base.UpdateVariable(oldVariable, newVariable);
            foreach (MetaVariable parameter in this.parameters)
            {
                parameter.UpdateVariable(oldVariable, newVariable);
            }
        }

        public override void RemoveVariable(MetaVariable metaVariable)
        {
            base.RemoveVariable(metaVariable);
            foreach (MetaVariable parameter in this.parameters)
            {
                parameter.RemoveVariable(metaVariable);
            }
        }
        #endregion

        #region Parameters
        public void CreateNewParameter(int index, MetaType metaType)
        {
            int i = 1;
            string name = "ANON_PARAM";
            while (this.parameters.Exists(v => v.Name == name))
            {
                name = "ANON_PARAM_" + i.ToString();
                i++;
            }
            this.parameters.Insert(index, new MetaVariable(name, metaType));
        }

        public void DeleteParameterAt(int index)
        {
            this.parameters.RemoveAt(index);
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
