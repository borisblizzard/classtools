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
        public override ECategoryType CategoryType
        {
            get { return ECategoryType.Class; }
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

        public List<MetaClass> SuperClasses
        {
            get
            {
                List<MetaClass> classes = new List<MetaClass>();
                if (this.HasSuperClass)
                {
                    classes.Add(this.superClass);
                    classes.AddRange(this.superClass.SuperClasses);
                }
                return classes;
            }
        }
        #endregion

        #region Construct
        public MetaClass(Model model)
            : base(model, "ANON_CLASS")
        {
            this.module = "";
            this.superClass = null;
            this.variables = new MetaList<MetaVariable>();
            this.methods = new MetaList<MetaMethod>();
            this.canSerialize = false;
            int i = 0;
            while (model.ClassExists(this))
            {
                this.name = "ANON_CLASS_" + i.ToString();
                i++;
            }
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
                this.SuperClass = (newType.CategoryType == ECategoryType.Class ? (MetaClass)newType : null);
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

        public List<MetaClass> FindSubClasses(List<MetaClass> metaClasses, bool includeThis = false)
        {
            List<MetaClass> result = new List<MetaClass>();
            List<MetaClass> superClasses;
            foreach (MetaClass classe in metaClasses)
            {
                if (classe == this)
                {
                    if (includeThis)
                    {
                        result.Add(this);
                    }
                }
                else
                {
                    superClasses = classe.SuperClasses;
                    for (int i = 0; i < superClasses.Count; i++)
                    {
                        if (superClasses[i] == this)
                        {
                            result.Add(classe);
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public override string ToString()
        {
            return this.GetNameWithModule();
        }
        #endregion

        #region Variable Methods
        public MetaVariable CreateNewVariable(int index)
        {
            MetaVariable variable = new MetaVariable(this.model, this);
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

        public bool VariableExists(MetaVariable metaVariable)
        {
            return this.variables.Exists(c => c.Equals(metaVariable));
        }
        #endregion

        #region Method Methods
        public MetaMethod CreateNewMethod(int index)
        {
            MetaMethod method = new MetaMethod(this.model, this);
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

        public bool MethodExists(MetaMethod metaMethod)
        {
            return this.methods.Exists(c => c.Equals(metaMethod));
        }
        #endregion

    }
}
