using ClassTools.Model;

namespace ClassTools.ClassMaker
{
    public static class InternalClipboard
    {
        #region Fields
        private static MetaType cachedType = null;
        private static MetaClass cachedClass = null;
        private static MetaVariable cachedVariable = null;
        private static MetaMethod cachedMethod = null;
        #endregion

        #region Properties
        public static MetaType Type
        {
            get { return (MetaType)Serializer.Clone(cachedType); }
            set { cachedType = (MetaType)Serializer.Clone(value); }
        }

        public static bool ContainsType
        {
            get { return (cachedType != null); }
        }

        public static MetaClass Class
        {
            get { return (MetaClass)Serializer.Clone(cachedClass); }
            set { cachedClass = (MetaClass)Serializer.Clone(value); }
        }

        public static bool ContainsClass
        {
            get { return (cachedClass != null); }
        }

        public static MetaVariable Variable
        {
            get { return (MetaVariable)Serializer.Clone(cachedVariable); }
            set { cachedVariable = (MetaVariable)Serializer.Clone(value); }
        }

        public static bool ContainsVariable
        {
            get { return (cachedVariable != null); }
        }

        public static MetaMethod Method
        {
            get { return (MetaMethod)Serializer.Clone(cachedMethod); }
            set { cachedMethod = (MetaMethod)Serializer.Clone(value); }
        }

        public static bool ContainsMethod
        {
            get { return (cachedMethod != null); }
        }
        #endregion

        #region Methods
        public static void Clear()
        {
            cachedType = null;
            cachedClass = null;
            cachedVariable = null;
            cachedMethod = null;
        }
        #endregion
    }
}
