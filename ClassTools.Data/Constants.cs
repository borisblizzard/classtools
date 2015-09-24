using System.Collections.Generic;

namespace ClassTools.Data
{
    public static class Constants
    {
        public const string ACCESS_PUBLIC = "public";
        public const string ACCESS_PROTECTED = "protected";
        public const string ACCESS_PRIVATE = "private";
        public static List<string> NAMES_ACCESS = new List<string>(new string[] { ACCESS_PUBLIC, ACCESS_PROTECTED, ACCESS_PRIVATE });

        public const string CATEGORY_NORMAL = "Normal";
        public const string CATEGORY_LIST = "List";
        public const string CATEGORY_DICTIONARY = "Dictionary";
		public const string CATEGORY_ENUM = "Enum";
		public static List<string> NAMES_CATEGORY = new List<string>(new string[] { CATEGORY_NORMAL, CATEGORY_LIST, CATEGORY_DICTIONARY, CATEGORY_ENUM });

        public const string TYPE_VOID = "void";
        public const string TYPE_INT = "int";
        public const string TYPE_UINT = "unsigned int";
        public const string TYPE_SHORT = "short";
        public const string TYPE_USHORT = "unsigned short";
        public const string TYPE_INT64 = "int64_t";
        public const string TYPE_UINT64 = "uint64_t";
        public const string TYPE_UCHAR = "unsigned char";
        public const string TYPE_FLOAT = "float";
        public const string TYPE_DOUBLE = "double";
        public const string TYPE_BOOL = "bool";
        public const string TYPE_CHAR = "char";
        public const string TYPE_STRING = "string";
        public const string TYPE_VERSION = "version";
		public const string TYPE_GVEC2 = "gvec2";
        public const string TYPE_GVEC3 = "gvec3";
        public const string TYPE_GRECT = "grect";
        public static List<string> TYPES_VOID = new List<string>(new string[] { TYPE_VOID });
        public static List<string> TYPES_INT = new List<string>(new string[] { TYPE_INT, TYPE_UINT, TYPE_SHORT, TYPE_USHORT, TYPE_UCHAR, TYPE_INT64, TYPE_UINT64 });
        public static List<string> TYPES_FLOAT = new List<string>(new string[] { TYPE_FLOAT, TYPE_DOUBLE });
        public static List<string> TYPES_BOOL = new List<string>(new string[] { TYPE_BOOL });
        public static List<string> TYPES_CHAR = new List<string>(new string[] { TYPE_CHAR });
        public static List<string> TYPES_STRING = new List<string>(new string[] { TYPE_STRING, TYPE_VERSION, TYPE_GVEC2, TYPE_GVEC3, TYPE_GRECT });

    }

}
