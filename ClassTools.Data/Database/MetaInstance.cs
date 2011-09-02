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
            MetaType metaType = model.FindMatchingType(this.type);
            if (metaType == null)
            {
                return false;
            }
            this.type = metaType;
            // TODO update the existing instance variables!
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                if (!metaInstanceVariable.Update(model))
                {
                    return false;
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
            // TODO update the existing instance variables!
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                metaInstanceVariable.UpdateType(oldType, newType);
            }
        }

        public override string ToString()
        {
            foreach (MetaInstanceVariable metaInstanceVariable in this.instanceVariables)
            {
                if ((metaInstanceVariable.Name == "Name" || metaInstanceVariable.Name == "name") && metaInstanceVariable.Value.String.Trim('"') != string.Empty)
                {
                    return metaInstanceVariable.Value.String.Trim('"');
                }
            }
            return this.type.Name;
        }
        #endregion

    }
}
