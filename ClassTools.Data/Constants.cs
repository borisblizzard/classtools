namespace ClassTools.Data
{
    public static class Constants
    {
        public static MetaList<string> NAMES_ACCESS = new MetaList<string>(new string[] { "public", "protected", "private" });
        public static MetaList<string> NAMES_CATEGORY = new MetaList<string>(new string[] { "Normal", "Collection", "Dictionary" });

        public static MetaList<string> TYPES_VOID = new MetaList<string>(new string[] { "void" });
        public static MetaList<string> TYPES_INT = new MetaList<string>(new string[] { "int", "unsigned int", "long", "unsigned long", "short", "unsigned short", "unsigned char" });
        public static MetaList<string> TYPES_FLOAT = new MetaList<string>(new string[] { "float", "double" });
        public static MetaList<string> TYPES_BOOL = new MetaList<string>(new string[] { "bool" });
        public static MetaList<string> TYPES_CHAR = new MetaList<string>(new string[] { "char" });
        public static MetaList<string> TYPES_STRING = new MetaList<string>(new string[] { "string" });

    }

}
