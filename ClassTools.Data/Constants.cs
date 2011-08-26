using System.Collections.Generic;

namespace ClassTools.Data
{
    public static class Constants
    {
        public static List<string> NAMES_ACCESS = new List<string>(new string[] { "public", "protected", "private" });
        public static List<string> NAMES_CATEGORY = new List<string>(new string[] { "Normal", "Collection", "Dictionary" });

        public static List<string> TYPES_VOID = new List<string>(new string[] { "void" });
        public static List<string> TYPES_INT = new List<string>(new string[] { "int", "unsigned int", "long", "unsigned long", "short", "unsigned short", "unsigned char" });
        public static List<string> TYPES_FLOAT = new List<string>(new string[] { "float", "double" });
        public static List<string> TYPES_BOOL = new List<string>(new string[] { "bool" });
        public static List<string> TYPES_CHAR = new List<string>(new string[] { "char" });
        public static List<string> TYPES_STRING = new List<string>(new string[] { "string" });

    }

}
