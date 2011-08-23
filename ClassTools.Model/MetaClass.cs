using System;
using System.Collections.Generic;

namespace ClassTools.Model
{
    [Serializable]
    public class MetaClass : MetaType
    {
        #region Fields
        protected string module;
        protected MetaClass superClass;
        protected List<MetaVariable> variables;
        protected List<MetaMethod> methods;
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

        public List<MetaVariable> Variables
        {
            get { return this.variables; }
        }

        public List<MetaMethod> Methods
        {
            get { return this.methods; }
        }

        public List<MetaVariable> AllVariables
        {
            get
            {
                List<MetaVariable> variables = new List<MetaVariable>();
                if (this.HasSuperClass)
                {
                    variables.AddRange(this.superClass.AllVariables);
                }
                variables.AddRange(this.variables);
                return variables;
            }
        }

        public List<MetaMethod> AllMethods
        {
            get
            {
                List<MetaMethod> methods = new List<MetaMethod>();
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

        #region Constructors
        public MetaClass(ClassModel model)
            : base(model, "ANON_CLASS")
        {
            this.module = "ANON_MODULE";
            this.superClass = null;
            this.variables = new List<MetaVariable>();
            this.methods = new List<MetaMethod>();
            this.canSerialize = false;
        }
        #endregion

        #region Behavior
        public bool Equals(MetaClass other)
        {
            if (!base.Equals(other)) return false;
            if (this.module != other.module) return false;
            if (this.HasSuperClass || other.HasSuperClass)
            {
                if (!this.HasSuperClass || !other.HasSuperClass) return false;
                if (!this.superClass.Equals(other.superClass)) return false;
            }
            if (this.variables.Count != other.variables.Count) return false;
            for (int i = 0; i < this.variables.Count; i++)
            {
                if (!this.variables[i].Equals(other.variables[i])) return false;
            }
            if (this.methods.Count != other.methods.Count) return false;
            for (int i = 0; i < this.methods.Count; i++)
            {
                if (!this.methods[i].Equals(other.methods[i])) return false;
            }
            if (this.canSerialize != other.canSerialize) return false;
            return true;
        }

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
            variable.Type = this.model.Types.Find(t => t.Equals(variable.Type));
        }

        public bool TryVariableMoveUp(int index)
        {
            if (index > 0)
            {
                MetaVariable variable = this.variables[index];
                this.variables[index] = this.variables[index - 1];
                this.variables[index - 1] = variable;
                return true;
            }
            return false;
        }

        public bool TryVariableMoveDown(int index)
        {
            if (index < this.variables.Count - 1)
            {
                MetaVariable variable = this.variables[index];
                this.variables[index] = this.variables[index + 1];
                this.variables[index + 1] = variable;
                return true;
            }
            return false;
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
            method.Type = this.model.Types.Find(t => t.Equals(method.Type));
            for (int i = 0; i < method.Parameters.Count; i++)
            {
                method.ReplaceParameterAt(i, method.Parameters[i]);
            }
        }

        public bool TryMethodMoveUp(int index)
        {
            if (index > 0)
            {
                MetaMethod method = this.methods[index];
                this.methods[index] = this.methods[index - 1];
                this.methods[index - 1] = method;
                return true;
            }
            return false;
        }

        public bool TryMethodMoveDown(int index)
        {
            if (index < this.methods.Count - 1)
            {
                MetaMethod method = this.methods[index];
                this.methods[index] = this.methods[index + 1];
                this.methods[index + 1] = method;
                return true;
            }
            return false;
        }

        public void SortMethods()
        {
            this.methods.Sort(new Comparison<MetaMethod>((a, b) => a.Name.CompareTo(b.Name)));
        }
        #endregion

    }
}
