using System;

namespace ClassTools.Data.Hierarchy
{
    [Serializable]
    public class MetaType : MetaBase, IEquatable<MetaType>
    {
        #region Fields
        protected MetaType subType1;
        string suffix1;
        protected MetaType subType2;
        string suffix2;
        #endregion

        #region Properties
        public MetaType SubType1
        {
            get { return this.subType1; }
            set { this.subType1 = value; }
        }

        public string Suffix1
        {
            get { return this.suffix1; }
            set { this.suffix1 = value; }
        }

        public MetaType SubType2
        {
            get { return this.subType2; }
            set { this.subType2 = value; }
        }

        public string Suffix2
        {
            get { return this.suffix2; }
            set { this.suffix2 = value; }
        }

        public virtual ECategoryType CategoryType
        {
            get
            {
                if (this.subType1 == null && this.subType2 == null)
                {
                    return ECategoryType.Integral;
                }
                if (this.subType2 == null)
                {
                    return ECategoryType.List;
                }
                return ECategoryType.Dictionary;
            }
        }

        public virtual bool CanSerialize
        {
            get { return false; }
            set { }
        }
        #endregion

        #region Construct
        public MetaType(Model model, string name)
            : base(model, name)
        {
            this.subType1 = null;
            this.suffix1 = string.Empty;
            this.subType2 = null;
            this.suffix2 = string.Empty;
        }

        public MetaType(Model model)
            : base(model, "ANON_TYPE")
        {
            this.subType1 = null;
            this.suffix1 = string.Empty;
            this.subType2 = null;
            this.suffix2 = string.Empty;
            int i = 0;
            while (model.TypeExists(this))
            {
                this.name = "ANON_TYPE_" + i.ToString();
                i++;
            }
        }
        #endregion

        #region Equals
        public bool Equals(MetaType other)
        {
            if (!base.Equals(other)) return false;
            if (!this.CategoryType.Equals(other.CategoryType)) return false;
            if (this.CategoryType == ECategoryType.List)
            {
                if (!this.subType1.Equals(other.subType1)) return false;
                if (!this.suffix1.Equals(other.suffix1)) return false;
            }
            if (this.CategoryType == ECategoryType.Dictionary)
            {
                if (!this.subType1.Equals(other.subType1)) return false;
                if (!this.suffix1.Equals(other.suffix1)) return false;
                if (!this.subType2.Equals(other.subType2)) return false;
                if (!this.suffix2.Equals(other.suffix2)) return false;
            }
            return true;
        }
        #endregion

        #region Methods
        public override void UpdateType(MetaType oldType, MetaType newType)
        {
            base.UpdateType(oldType, newType);
            if (this.SubType1 == oldType)
            {
                this.SubType1 = newType;
            }
            if (this.SubType2 == oldType)
            {
                this.SubType2 = newType;
            }
        }

        public virtual string GetNameWithModule(string separator)
        {
            string result = this.name;
            switch (this.CategoryType)
            {
                case ECategoryType.List:
                    result += "<" + this.subType1.GetNameWithModule(separator) + this.suffix1 + ">";
                    break;
                case ECategoryType.Dictionary:
                    result += "<" + this.subType1.GetNameWithModule(separator) + this.suffix1 + ", " + this.subType2.GetNameWithModule(separator) + this.suffix2 + ">";
                    break;
            }
            return result;
        }

        public virtual string GetNameWithModule()
        {
            return this.GetNameWithModule("::");
        }

        public override string ToString()
        {
            string result = this.name;
            switch (this.CategoryType)
            {
                case ECategoryType.List:
                    result += "<" + this.subType1.ToString() + this.suffix1 + ">";
                    break;
                case ECategoryType.Dictionary:
                    result += "<" + this.subType1.ToString() + this.suffix1 + ", " + this.subType2.ToString() + this.suffix2 + ">";
                    break;
            }
            return result;
        }
        #endregion

    }
}
