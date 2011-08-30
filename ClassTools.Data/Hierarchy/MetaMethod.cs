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
        public MetaMethod(Model model, MetaClass metaClass)
            : base(model, "ANON_METHOD")
        {
            this.parameters = new MetaList<MetaVariable>();
            this.implementation = string.Empty;
            int i = 0;
            while (metaClass.MethodExists(this))
            {
                this.name = "ANON_METHOD_" + i.ToString();
                i++;
            }
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

        public override void ReplaceType(MetaType oldType, MetaType newType)
        {
            base.ReplaceType(oldType, newType);
            foreach (MetaVariable parameter in this.parameters)
            {
                parameter.ReplaceType(oldType, newType);
            }
        }
        #endregion

        #region Parameters
        public void CreateNewParameter(int index)
        {
            this.parameters.Insert(index, new MetaVariable(this.model, this));
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

        public bool ParameterExists(MetaVariable metaVariable)
        {
            return this.parameters.Exists(c => c.Equals(metaVariable));
        }
        #endregion

    }
}
