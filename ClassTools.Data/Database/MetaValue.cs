﻿using System;
using System.Collections.Generic;

using ClassTools.Data.Hierarchy;

namespace ClassTools.Data.Database
{
    [Serializable]
    public class MetaValue : MetaBase, IEquatable<MetaValue>
    {
        #region Fields
        protected string typeName;
        protected EValueType valueType;
        protected string valueString;
        protected MetaInstance instance;
        protected MetaList<MetaValue> list;
        protected MetaDictionary<MetaValue, MetaValue> dictionary;
        #endregion

        #region Properties
        public string TypeName
        {
            get { return this.typeName; }
        }

        public EValueType ValueType
        {
            get { return this.valueType; }
        }

        public string String
        {
            get { return this.valueString.Trim('"'); }
            set
            {
                this.valueType = EValueType.Integral;
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
                this.valueType = EValueType.Integral;
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
                this.valueType = EValueType.Integral;
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
                this.valueType = EValueType.Object;
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
                this.valueType = EValueType.List;
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
                this.valueType = EValueType.Dictionary;
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
        public MetaValue(Repository repository, MetaType metaType, string defaultValue = "")
            : base(repository)
        {
            this.typeName = metaType.Name;
            this.valueType = EValueType.Integral;
            this.String = defaultValue;
        }

        public MetaValue(Repository repository, MetaClass metaClass, MetaInstance metaInstance)
            : base(repository)
        {
            this.typeName = metaClass.Name;
            this.valueType = EValueType.Object;
            this.Instance = metaInstance;
        }

        public MetaValue(Repository repository, MetaType metaType, MetaList<MetaValue> list)
            : base(repository)
        {
            this.typeName = metaType.Name;
            this.valueType = EValueType.List;
            this.List = list;
        }

        public MetaValue(Repository repository, MetaType metaType, MetaDictionary<MetaValue, MetaValue> dictionary)
            : base(repository)
        {
            this.typeName = metaType.Name;
            this.valueType = EValueType.Dictionary;
            this.Dictionary = dictionary;
        }
        #endregion

        #region Equals
        public bool Equals(MetaValue other)
        {
            if (!base.Equals(other)) return false;
            if (!this.typeName.Equals(other.typeName)) return false;
            if (!this.valueType.Equals(other.valueType)) return false;
            switch (valueType)
            {
                case EValueType.Integral:
                    if (!this.valueString.Equals(other.valueString)) return false;
                    break;
                case EValueType.Object:
                    if ((this.instance != null) != (other.instance != null)) return false;
                    if (this.instance != null && !this.instance.Equals(other.instance)) return false;
                    break;
                case EValueType.List:
                    if (!this.list.Equals(other.list)) return false;
                    break;
                case EValueType.Dictionary:
                    if (!this.dictionary.Equals(other.dictionary)) return false;
                    break;
            }
            return true;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            switch (this.valueType)
            {
                case EValueType.Integral:
                    return this.valueString;
                case EValueType.Object:
                    return (this.instance != null ? this.instance.ToString() : "null");
                case EValueType.List:
                    return "List";
                case EValueType.Dictionary:
                    return "Dictionary";
            }
            return "unknown value";
        }
        #endregion

    }
}
