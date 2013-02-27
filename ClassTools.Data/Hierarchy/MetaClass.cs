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
        public MetaClass(string name)
            : base(name)
        {
            this.module = "";
            this.superClass = null;
            this.variables = new MetaList<MetaVariable>();
            this.methods = new MetaList<MetaMethod>();
            this.canSerialize = false;
        }
        #endregion

        #region Equals
        public bool Equals(MetaClass other)
        {
            if (Object.ReferenceEquals(this, other)) return true;
            if (!base.Equals(other)) return false;
            return true;
        }

        protected override bool checkClassMatch(MetaClass other)
        {
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
        public override bool Update(Model model)
        {
            if (!base.Update(model))
            {
                return false;
            }
            if (this.superClass != null)
            {
                MetaType metaType = model.FindMatchingType(this.superClass);
                if (metaType == null)
                {
                    return false;
                }
                this.superClass = (MetaClass)metaType;
            }
            foreach (MetaVariable variable in this.variables)
            {
                if (!variable.Update(model))
                {
                    return false;
                }
            }
            foreach (MetaMethod method in this.methods)
            {
                if (!method.Update(model))
                {
                    return false;
                }
            }
            return true;
        }

        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            base.UpdateType(oldType, newType);
            if (this.superClass != null && this.superClass.Equals(oldType))
            {
                this.superClass = (this.superClass.Matches(oldType, newType) ? (MetaClass)newType : null);
            }
            foreach (MetaVariable variable in this.variables)
            {
                variable.UpdateType(oldType, oldType);
            }
            foreach (MetaMethod method in this.methods)
            {
                method.UpdateType(oldType, oldType);
            }
        }

        public override bool Matches(MetaType oldType, MetaType newType)
        {
            return (oldType.CategoryType == ECategoryType.Class && newType.CategoryType == ECategoryType.Class && this.Equals((MetaClass)oldType));
        }

        public override MetaList<MetaVariable> FindVariableMismatches(MetaType metaType)
        {
            MetaList<MetaVariable> result = base.FindVariableMismatches(metaType);
            if (metaType.CategoryType == ECategoryType.Class)
            {
                MetaClass metaClass = (MetaClass)metaType;
                result.AddRange(this.variables.FindAll(v => !metaClass.variables.Contains(v)));
            }
            return result;
        }

        public MetaVariable FindMatchingVariable(MetaVariable metaVariable)
        {
            return this.variables.Find(v => v.Equals(metaVariable));
        }

        public override void UpdateVariable(MetaType metaType, MetaVariable oldVariable, MetaVariable newVariable)
        {
            base.UpdateVariable(metaType, oldVariable, newVariable);
            if (this.Equals(metaType))
            {
                for (int i = 0; i < this.variables.Count; i++)
                {
                    if (this.variables[i].Equals(oldVariable))
                    {
                        this.variables[i] = newVariable;
                    }
                    else
                    {
                        this.variables[i].UpdateVariable(metaType, oldVariable, newVariable);
                    }
                }
            }
        }

        public override void RemoveVariable(MetaType metaType, MetaVariable metaVariable)
        {
            base.RemoveVariable(metaType, metaVariable);
            if (this.Equals(metaType))
            {
                for (int i = 0; i < this.variables.Count; i++)
                {
                    if (this.variables[i].Equals(metaVariable))
                    {
                        this.variables.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public override void UpdateParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter oldParameter, MetaParameter newParameter)
        {
            base.UpdateParameter(metaType, metaMethod, oldParameter, newParameter);
            if (this.Equals(metaType))
            {
                foreach (MetaMethod method in this.methods)
                {
                    method.UpdateParameter(metaType, metaMethod, oldParameter, newParameter);
                }
            }
        }

        public override void RemoveParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter metaParameter)
        {
            base.RemoveParameter(metaType, metaMethod, metaParameter);
            if (this.Equals(metaType))
            {
                foreach (MetaMethod method in this.methods)
                {
                    method.RemoveParameter(metaType, metaMethod, metaParameter);
                }
            }
        }

        public override string GetNameWithModule(string separator)
        {
            string name = base.GetNameWithModule(separator);
            if (this.module != string.Empty)
            {
                List<string> strings = new List<string>(this.module.Split(':'));
                strings.RemoveAll(x => x == "");
                name = string.Join(separator, strings.ToArray()) + separator + name;
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
        public void CreateNewVariable(int index, MetaType metaType)
        {
            int i = 1;
            string name = "ANON_VAR";
            while (this.variables.Exists(v => v.Name == name))
            {
                name = "ANON_VAR_" + i.ToString();
                i++;
            }
            this.variables.Insert(index, new MetaVariable(name, metaType));
        }

        public void DeleteVariableAt(int index)
        {
            this.variables.RemoveAt(index);
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
        public void CreateNewMethod(int index, MetaType metaType)
        {
            int i = 1;
            string name = "ANON_METHOD";
            while (this.methods.Exists(v => v.Name == name))
            {
                name = "ANON_METHOD_" + i.ToString();
                i++;
            }
            this.methods.Insert(index, new MetaMethod(name, metaType));
        }

        public void DeleteMethodAt(int index)
        {
            this.methods.RemoveAt(index);
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
