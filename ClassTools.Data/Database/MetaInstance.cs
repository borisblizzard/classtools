using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaInstance : MetaBase, IEquatable<MetaInstance>
    {
        #region Fields
        protected string typeName;
        protected MetaList<MetaInstanceVariable> instanceVariables;
        #endregion

        #region Properties
        public string TypeName
        {
            get { return this.typeName; }
            set { this.typeName = value; }
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
            this.typeName = metaClass.Name;
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
            if (!this.typeName.Equals(other.typeName)) return false;
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
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                if (!metaInstanceVariable.Update(model))
                {
                    return false;
                }
            }
            return true;
        }

        public override void ReplaceType(MetaType oldType, MetaType newType)
        {
            base.ReplaceType(oldType, newType);
            this.typeName = newType.GetNameWithModule();
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                metaInstanceVariable.ReplaceType(oldType, newType);
            }
        }

        public override string ToString()
        {
            string result = this.typeName;
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                if ((metaInstanceVariable.Name == "Name" || metaInstanceVariable.Name == "name") && metaInstanceVariable.Value.String.Trim('"') != string.Empty)
                {
                    return metaInstanceVariable.Value.String.Trim('"');
                }
            }
            return this.typeName;
        }
        #endregion

    }
}
