using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class Repository : Base, IEquatable<Repository>
    {
        #region Fields
        protected Model model = null;
        protected MetaDictionary<MetaClass, MetaList<MetaValue>> values = null;
		protected MetaList<MetaClass> visibleClasses = null;
		#endregion

		#region Properties
		public Model Model
        {
            get { return this.model; }
        }

        public MetaDictionary<MetaClass, MetaList<MetaValue>> Values
        {
            get { return this.values; }
        }

		public MetaList<MetaClass> VisibleClasses
		{
			get { return this.visibleClasses; }
			set { this.visibleClasses = value; }
		}
		#endregion

		#region Construct
		public Repository(Model model)
            : base()
        {
            this.model = model;
            this.values = new MetaDictionary<MetaClass, MetaList<MetaValue>>();
			this.visibleClasses = new MetaList<MetaClass>(model.LeafClasses);
            foreach (MetaClass metaClass in this.visibleClasses)
            {
                this.values[metaClass] = new MetaList<MetaValue>();
            }
        }
        #endregion

        #region Equals
        public bool Equals(Repository other)
        {
            if (Object.ReferenceEquals(this, other)) return true;
            if (!base.Equals(other)) return false;
            if (!this.model.Equals(other.model)) return false;
            if (!this.values.Equals(other.values)) return false;
			if (!this.visibleClasses.Equals(other.visibleClasses)) return false;
			return true;
        }
        #endregion

        #region Methods
        public override bool Update(Model model)
        {
            this.model = model;
			if (this.visibleClasses == null)
			{
				this.visibleClasses = new MetaList<MetaClass>(model.LeafClasses);
            }
			MetaList<MetaClass> oldVisibleClasses = new MetaList<MetaClass>(this.visibleClasses);
            this.visibleClasses.Clear();
            MetaList<MetaClass> keys = this.values.GetKeys();
            MetaList<MetaClass> classes = new MetaList<MetaClass>(model.LeafClasses);
            MetaClass keyClass;
            MetaList<MetaValue> values;
            foreach (MetaClass metaClass in classes)
            {
                // cannot use "ContainsKey" because "GetHashCode" was not implemented
                // and ContainsKey seems to use it to determine object identity rather than "Equals"
                if (!keys.Contains(metaClass))
                {
                    this.values[metaClass] = new MetaList<MetaValue>();
					this.visibleClasses.Add(metaClass);
				}
				else
                {
                    keyClass = keys[keys.IndexOf(metaClass)];
                    values = this.values[keyClass];
                    this.values.Remove(keyClass);
                    this.values[metaClass] = values;
					if (oldVisibleClasses.Contains(keyClass))
					{
						this.visibleClasses.Add(metaClass);
					}
				}
			}
			foreach (KeyValuePair<MetaClass, MetaList<MetaValue>> pair in this.values)
            {
                foreach (MetaValue value in pair.Value)
                {
                    if (!value.Update(model))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            this.model.UpdateType(oldType, newType);
            if (oldType.CategoryType == ECategoryType.Class && newType.CategoryType == ECategoryType.Class)
            {
                MetaClass oldClass = (MetaClass)oldType;
                MetaClass newClass = (MetaClass)newType;
                if (this.values.GetKeys().Contains(oldClass))
                {
                    MetaList<MetaValue> values = this.values[oldClass];
                    this.values.Remove(oldClass);
                    this.values[newClass] = values;
					if (this.visibleClasses.Contains(oldClass))
					{
						this.visibleClasses.Remove(oldClass);
                        this.visibleClasses.Add(newClass);
					}
				}
			}
            foreach (KeyValuePair<MetaClass, MetaList<MetaValue>> pair in this.values)
            {
                foreach (MetaValue value in pair.Value)
                {
                    value.UpdateType(oldType, newType);
                }
            }
        }

        public override void UpdateVariable(MetaType metaType, MetaVariable oldVariable, MetaVariable newVariable)
        {
            this.model.UpdateVariable(metaType, oldVariable, newVariable);
            foreach (KeyValuePair<MetaClass, MetaList<MetaValue>> pair in this.values)
            {
                foreach (MetaValue value in pair.Value)
                {
                    value.UpdateVariable(metaType, oldVariable, newVariable);
                }
            }
        }

        public override void RemoveVariable(MetaType metaType, MetaVariable metaVariable)
        {
            this.model.RemoveVariable(metaType, metaVariable);
            foreach (KeyValuePair<MetaClass, MetaList<MetaValue>> pair in this.values)
            {
                foreach (MetaValue value in pair.Value)
                {
                    value.RemoveVariable(metaType, metaVariable);
                }
            }
        }

        public override void UpdateParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter oldParameter, MetaParameter newParameter)
        {
            this.model.UpdateParameter(metaType, metaMethod, oldParameter, newParameter);
        }

        public override void RemoveParameter(MetaType metaType, MetaMethod metaMethod, MetaParameter metaParameter)
        {
            this.model.RemoveParameter(metaType, metaMethod, metaParameter);
        }
        #endregion

        #region Values
        public void CreateNewValue(MetaClass metaClass, int index)
        {
            this.values[metaClass].Insert(index, new MetaValue(metaClass, new MetaInstance(metaClass)));
        }

        public void DeleteValueAt(MetaClass metaClass, int index)
        {
            this.values[metaClass].RemoveAt(index);
        }

        public bool TryValueMoveUp(MetaClass metaClass, int index)
        {
            return this.values[metaClass].TryMoveUp(index);
        }

        public bool TryValueMoveDown(MetaClass metaClass, int index)
        {
            return this.values[metaClass].TryMoveDown(index);
        }
        #endregion

    }

}
