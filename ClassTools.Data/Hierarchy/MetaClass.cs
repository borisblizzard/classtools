using System;
using System.Collections.Generic;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaClass : MetaType, IEquatable<MetaClass>
    {
        #region Fields
        protected string module;
        protected MetaClass superClass;
        protected MetaList<MetaVariable> variables;
        protected MetaList<MetaMethod> methods;
        protected bool canSerialize;
        #endregion

        #region Properties
        public override bool IsClass
        {
            get { return true; }
        }

        public string Module
        {
            get { return this.module; }
            set { this.module = value; }
        }

        public MetaClass SuperClass
        {
            get { return this.superClass; }
            set { this.superClass = value; }
        }

        public bool HasSuperClass
        {
            get { return (this.superClass != null); }
        }

        public MetaList<MetaVariable> Variables
        {
            get { return this.variables; }
        }

        public MetaList<MetaMethod> Methods
        {
            get { return this.methods; }
        }

        public MetaList<MetaVariable> AllVariables
        {
            get
            {
                MetaList<MetaVariable> variables = new MetaList<MetaVariable>();
                if (this.HasSuperClass)
                {
                    variables.AddRange(this.superClass.AllVariables);
                }
                variables.AddRange(this.variables);
                return variables;
            }
        }

        public MetaList<MetaMethod> AllMethods
        {
            get
            {
                MetaList<MetaMethod> methods = new MetaList<MetaMethod>();
                if (this.HasSuperClass)
                {
                    methods.AddRange(this.superClass.AllMethods);
                }
                methods.AddRange(this.methods);
                return methods;
            }
        }

        public override bool CanSerialize
        {
            get { return this.canSerialize; }
            set { this.canSerialize = value; }
        }

        public string Path
        {
            get { return (this.module != string.Empty ? string.Format("{0}/{1}", this.module, this.name) : this.name); }
        }

        #endregion

        #region Construct
        public MetaClass(Model model)
            : base(model, "ANON_CLASS")
        {
            this.module = "ANON_MODULE";
            this.superClass = null;
            this.variables = new MetaList<MetaVariable>();
            this.methods = new MetaList<MetaMethod>();
            this.canSerialize = false;
        }
        #endregion

        #region Equals
        public bool Equals(MetaClass other)
        {
            if (!base.Equals(other)) return false;
            if (!this.module.Equals(other.module)) return false;
            if ((this.superClass != null) != (other.superClass != null)) return false;
            if (this.superClass != null && !this.superClass.Equals(other.superClass)) return false;
            if (!this.variables.Equals(other.variables)) return false;
            if (!this.methods.Equals(other.methods)) return false;
            if (!this.canSerialize.Equals(other.canSerialize)) return false;
            return true;
        }
        #endregion

        #region Methods
        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            base.UpdateType(oldType, newType);
            if (this.HasSuperClass && this.SuperClass == oldType)
            {
                this.SuperClass = (newType.IsClass ? (MetaClass)newType : null);
            }
            for (int j = 0; j < this.Variables.Count; j++)
            {
                this.Variables[j].UpdateType(oldType, oldType);
            }
            for (int j = 0; j < this.Methods.Count; j++)
            {
                this.Variables[j].UpdateType(oldType, oldType);
            }
        }

        public override string GetNameWithModule(string separator)
        {
            string name = base.GetNameWithModule(separator);
            if (this.module != string.Empty)
            {
                name = this.module + separator + name;
            }
            return name;
        }

        public override string ToString()
        {
            return this.GetNameWithModule();
        }
        #endregion

        #region Variable Methods
        public MetaVariable CreateNewVariable(int index)
        {
            MetaVariable variable = new MetaVariable(this.model);
            this.variables.Insert(index, variable);
            return variable;
        }

        public void DeleteVariableAt(int index)
        {
            this.variables.RemoveAt(index);
        }

        public void ReplaceVariableAt(int index, MetaVariable variable)
        {
            this.Variables[index] = variable;
            variable.Model = this.model;
            variable.Type = this.model.AllTypes.Find(t => t.Equals(variable.Type));
        }

        public bool TryVariableMoveUp(int index)
        {
            return this.variables.TryMoveUp(index);
        }

        public bool TryVariableMoveDown(int index)
        {
            return this.variables.TryMoveDown(index);
        }

        public void SortVariables()
        {
            this.variables.Sort(new Comparison<MetaVariable>((a, b) => a.Name.CompareTo(b.Name)));
        }
        #endregion

        #region Method Methods
        public MetaMethod CreateNewMethod(int index)
        {
            MetaMethod method = new MetaMethod(this.model);
            this.methods.Insert(index, method);
            return method;
        }

        public void DeleteMethodAt(int index)
        {
            this.methods.RemoveAt(index);
        }

        public void ReplaceMethodAt(int index, MetaMethod method)
        {
            this.Methods[index] = method;
            method.Model = this.model;
            method.Type = this.model.AllTypes.Find(t => t.Equals(method.Type));
            for (int i = 0; i < method.Parameters.Count; i++)
            {
                method.ReplaceParameterAt(i, method.Parameters[i]);
            }
        }

        public bool TryMethodMoveUp(int index)
        {
            return this.methods.TryMoveUp(index);
        }

        public bool TryMethodMoveDown(int index)
        {
            return this.methods.TryMoveDown(index);
        }

        public void SortMethods()
        {
            this.methods.Sort(new Comparison<MetaMethod>((a, b) => a.Name.CompareTo(b.Name)));
        }
        #endregion

    }
}
