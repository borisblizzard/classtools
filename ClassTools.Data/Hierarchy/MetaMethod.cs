using System;
using System.Collections.Generic;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaMethod : MetaMember, IEquatable<MetaMethod>
    {
        #region Fields
        protected MetaList<MetaParameter> parameters;
        protected string implementation;
        #endregion

        #region Properties
        public MetaList<MetaParameter> Parameters
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
            this.parameters = new MetaList<MetaParameter>();
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

        public override void UpdateParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter oldParameter, MetaParameter newParameter)
        {
            base.UpdateParameter(metaType, metaMethod, oldParameter, newParameter);
            if (this.Equals(metaMethod))
            {
                foreach (MetaParameter parameter in this.parameters)
                {
                    parameter.UpdateParameter(metaType, metaMethod, oldParameter, newParameter);
                }
            }
        }

        public override void RemoveParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter metaParameter)
        {
            base.RemoveParameter(metaType, metaMethod, metaParameter);
            if (this.Equals(metaMethod))
            {
                for (int i = 0; i < this.parameters.Count; i++)
                {
                    if (this.parameters[i].Equals(metaParameter))
                    {
                        this.parameters.RemoveAt(i);
                        i--;
                    }
                }
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
            this.parameters.Insert(index, new MetaParameter(name, metaType));
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
