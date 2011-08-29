using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.Common
{
    public static class InternalClipboard
    {
        #region Fields
        private static MetaType cachedType = null;
        private static MetaClass cachedClass = null;
        private static MetaVariable cachedVariable = null;
        private static MetaMethod cachedMethod = null;
        private static MetaValue cachedValue = null;
        #endregion

        #region Properties
        public static MetaType Type
        {
            get { return Serializer.Clone(cachedType); }
            set { cachedType = Serializer.Clone(value); }
        }

        public static bool ContainsType
        {
            get { return (cachedType != null); }
        }

        public static MetaClass Class
        {
            get { return Serializer.Clone(cachedClass); }
            set { cachedClass = Serializer.Clone(value); }
        }

        public static bool ContainsClass
        {
            get { return (cachedClass != null); }
        }

        public static MetaVariable Variable
        {
            get { return Serializer.Clone(cachedVariable); }
            set { cachedVariable = Serializer.Clone(value); }
        }

        public static bool ContainsVariable
        {
            get { return (cachedVariable != null); }
        }

        public static MetaMethod Method
        {
            get { return Serializer.Clone(cachedMethod); }
            set { cachedMethod = Serializer.Clone(value); }
        }

        public static bool ContainsMethod
        {
            get { return (cachedMethod != null); }
        }

        public static MetaValue Value
        {
            get { return Serializer.Clone(cachedValue); }
            set { cachedValue = Serializer.Clone(value); }
        }

        public static bool ContainsValue
        {
            get { return (cachedValue != null); }
        }
        #endregion

        #region Methods
        public static void Clear()
        {
            cachedType = null;
            cachedClass = null;
            cachedVariable = null;
            cachedMethod = null;
            cachedValue = null;
        }
        #endregion

    }
}
