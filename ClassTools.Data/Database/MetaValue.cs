using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaValue : MetaBase, IEquatable<MetaValue>
    {
        #region Fields
        protected MetaType type;
        protected string valueString;
        protected MetaInstance instance;
        protected MetaList<MetaValue> list;
        protected MetaDictionary<MetaValue, MetaValue> dictionary;
        #endregion

        #region Properties
        public MetaType Type
        {
            get { return this.type; }
        }

        public string String
        {
            get { return this.valueString.Trim('"'); }
            set
            {
                this.valueString = value;
                this.instance = null;
                this.list = new MetaList<MetaValue>();
                this.dictionary = new MetaDictionary<MetaValue, MetaValue>();
            }
        }

        public decimal Decimal
        {
            get
            {
                decimal result = decimal.Zero;
                decimal.TryParse(this.valueString, out result);
                return result;
            }
            set
            {
                this.valueString = value.ToString().Replace(',', '.');
                this.instance = null;
                this.list = new MetaList<MetaValue>();
                this.dictionary = new MetaDictionary<MetaValue, MetaValue>();
            }
        }

        public bool Bool
        {
            get { return (this.valueString != "0" && this.valueString.ToLower() != "false"); }
            set
            {
                this.valueString = (value ? "true" : "false");
                this.instance = null;
                this.list = new MetaList<MetaValue>();
                this.dictionary = new MetaDictionary<MetaValue, MetaValue>();
            }
        }

        public MetaInstance Instance
        {
            get { return this.instance; }
            set
            {
                this.instance = value;
                this.valueString = "";
                this.list = new MetaList<MetaValue>();
                this.dictionary = new MetaDictionary<MetaValue, MetaValue>();
            }
        }

        public MetaList<MetaValue> List
        {
            get { return this.list; }
            set
            {
                this.list = value;
                this.valueString = "";
                this.instance = null;
                this.dictionary = new MetaDictionary<MetaValue, MetaValue>();
            }
        }

        public MetaDictionary<MetaValue, MetaValue> Dictionary
        {
            get { return this.dictionary; }
            set
            {
                this.dictionary = value;
                this.valueString = "";
                this.instance = null;
                this.list = new MetaList<MetaValue>();
            }
        }

        public int AsInt
        {
            get
            {
                int result = 0;
                int.TryParse(this.valueString, out result);
                return result;
            }
        }

        public uint AsUInt
        {
            get
            {
                uint result = 0;
                uint.TryParse(this.valueString, out result);
                return result;
            }
        }

        public long AsLong
        {
            get
            {
                long result = 0;
                long.TryParse(this.valueString, out result);
                return result;
            }
        }

        public ulong AsULong
        {
            get
            {
                ulong result = 0;
                ulong.TryParse(this.valueString, out result);
                return result;
            }
        }

        public short AsShort
        {
            get
            {
                short result = 0;
                short.TryParse(this.valueString, out result);
                return result;
            }
        }

        public ushort AsUShort
        {
            get
            {
                ushort result = 0;
                ushort.TryParse(this.valueString, out result);
                return result;
            }
        }

        public byte AsByte
        {
            get
            {
                byte result = 0;
                byte.TryParse(this.valueString, out result);
                return result;
            }
        }

        public sbyte AsSByte
        {
            get
            {
                sbyte result = 0;
                sbyte.TryParse(this.valueString, out result);
                return result;
            }
        }

        public float AsFloat
        {
            get
            {
                float result = 0.0f;
                float.TryParse(this.valueString, out result);
                return result;
            }
        }

        public double AsDouble
        {
            get
            {
                double result = 0.0;
                double.TryParse(this.valueString, out result);
                return result;
            }
        }

        #endregion

        #region Construct
        public MetaValue(MetaType metaType, string defaultValue = "")
            : base()
        {
            this.type = metaType;
            this.String = defaultValue;
        }

        public MetaValue(MetaClass metaClass, MetaInstance metaInstance)
            : base()
        {
            this.type = metaClass;
            this.Instance = metaInstance;
        }

        public MetaValue(MetaType metaType, MetaList<MetaValue> list)
            : base()
        {
            this.type = metaType;
            this.List = list;
        }

        public MetaValue(MetaType metaType, MetaDictionary<MetaValue, MetaValue> dictionary)
            : base()
        {
            this.type = metaType;
            this.Dictionary = dictionary;
        }

        public MetaValue(MetaValue other)
            : base()
        {
            this.type = other.type;
            switch (this.type.CategoryType)
            {
                case ECategoryType.Integral:
                    this.String = other.String;
                    break;
                case ECategoryType.Class:
                    this.Instance = other.Instance;
                    break;
                case ECategoryType.List:
                    this.List = other.List;
                    break;
                case ECategoryType.Dictionary:
                    this.Dictionary = other.Dictionary;
                    break;
            }
        }
        #endregion

        #region Equals
        public bool Equals(MetaValue other)
        {
            if (!base.Equals(other)) return false;
            if (!this.type.Equals(other.type)) return false;
            switch (this.type.CategoryType)
            {
                case ECategoryType.Integral:
                    if (!this.valueString.Equals(other.valueString)) return false;
                    break;
                case ECategoryType.Class:
                    if ((this.instance != null) != (other.instance != null)) return false;
                    if (this.instance != null && !this.instance.Equals(other.instance)) return false;
                    break;
                case ECategoryType.List:
                    if (!this.list.Equals(other.list)) return false;
                    break;
                case ECategoryType.Dictionary:
                    if (!this.dictionary.Equals(other.dictionary)) return false;
                    break;
            }
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
            MetaType newType = model.FindMatchingType(this.type);
            if (newType == null)
            {
                return false;
            }
            this.type = newType;
            switch (this.type.CategoryType)
            {
                case ECategoryType.Integral:
                    this.String = this.String;
                    break;
                case ECategoryType.Class:
                    this.Instance = this.Instance;
                    if (this.instance != null)
                    {
                        return this.instance.Update(model);
                    }
                    break;
                case ECategoryType.List:
                    this.List = this.List;
                    foreach (MetaValue metaValue in this.list)
                    {
                        if (!metaValue.Update(model))
                        {
                            return false;
                        }
                    }
                    break;
                case ECategoryType.Dictionary:
                    this.Dictionary = this.Dictionary;
                    foreach (KeyValuePair<MetaValue, MetaValue> pair in this.dictionary)
                    {
                        if (!pair.Key.Update(model) || !pair.Value.Update(model))
                        {
                            return false;
                        }
                    }
                    break;
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
            switch (this.type.CategoryType)
            {
                case ECategoryType.Integral:
                    this.String = this.String;
                    break;
                case ECategoryType.Class:
                    this.Instance = this.Instance;
                    if (this.instance != null)
                    {
                        this.instance.UpdateType(oldType, newType);
                    }
                    break;
                case ECategoryType.List:
                    this.List = this.List;
                    foreach (MetaValue metaValue in this.list)
                    {
                        metaValue.UpdateType(oldType, newType);
                    }
                    break;
                case ECategoryType.Dictionary:
                    this.Dictionary = this.Dictionary;
                    foreach (KeyValuePair<MetaValue, MetaValue> pair in this.dictionary)
                    {
                        pair.Key.UpdateType(oldType, newType);
                        pair.Value.UpdateType(oldType, newType);
                    }
                    break;
            }
        }

        public override void UpdateVariable(MetaVariable oldVariable, MetaVariable newVariable)
        {
            switch (this.type.CategoryType)
            {
                case ECategoryType.Class:
                    if (this.instance != null)
                    {
                        this.instance.UpdateVariable(oldVariable, newVariable);
                    }
                    break;
                case ECategoryType.List:
                    foreach (MetaValue metaValue in this.list)
                    {
                        metaValue.UpdateVariable(oldVariable, newVariable);
                    }
                    break;
                case ECategoryType.Dictionary:
                    foreach (KeyValuePair<MetaValue, MetaValue> pair in this.dictionary)
                    {
                        pair.Key.UpdateVariable(oldVariable, newVariable);
                        pair.Value.UpdateVariable(oldVariable, newVariable);
                    }
                    break;
            }
        }

        public override string ToString()
        {
            switch (this.type.CategoryType)
            {
                case ECategoryType.Integral:
                    return (this.valueString != string.Empty ? this.valueString : "EMPTY");
                case ECategoryType.Class:
                    return (this.instance != null ? this.instance.ToString() : "NULL");
                case ECategoryType.List:
                    return this.type.Name;
                case ECategoryType.Dictionary:
                    return this.type.Name;
            }
            return "unknown value";
        }
        #endregion

    }
}
