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
        private static MetaInstance cachedInstance = null;
        private static MetaInstanceVariable cachedInstanceVariable = null;
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

        public static MetaInstance Instance
        {
            get { return Serializer.Clone(cachedInstance); }
            set { cachedInstance = Serializer.Clone(value); }
        }

        public static bool ContainsInstance
        {
            get { return (cachedInstance != null); }
        }

        public static MetaInstanceVariable InstanceVariable
        {
            get { return Serializer.Clone(cachedInstanceVariable); }
            set { cachedInstanceVariable = Serializer.Clone(value); }
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
