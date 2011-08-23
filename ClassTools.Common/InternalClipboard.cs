using ClassTools.Model;

namespace ClassTools.Common
{
    public static class InternalClipboard
    {
        #region Fields
        private static MetaType cachedType = null;
        private static MetaClass cachedClass = null;
        private static MetaVariable cachedVariable = null;
        private static MetaMethod cachedMethod = null;
        private static MetaInstance cachedInstance = null;
        private static MetaInstanceVariable cachedInstanceVariable = null;
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

        public static MetaInstance Instance
        {
            get { return (MetaInstance)Serializer.Clone(cachedInstance); }
            set { cachedInstance = (MetaInstance)Serializer.Clone(value); }
        }

        public static bool ContainsInstance
        {
            get { return (cachedInstance != null); }
        }

        public static MetaInstanceVariable InstanceVariable
        {
            get { return (MetaInstanceVariable)Serializer.Clone(cachedInstanceVariable); }
            set { cachedInstanceVariable = (MetaInstanceVariable)Serializer.Clone(value); }
        }

        public static bool ContainsInstanceVariable
        {
            get { return (cachedInstanceVariable != null); }
        }
        #endregion

        #region Methods
        public static void Clear()
        {
            cachedType = null;
            cachedClass = null;
            cachedVariable = null;
            cachedMethod = null;
            cachedInstance = null;
            cachedInstanceVariable = null;
        }
        #endregion

    }
}
