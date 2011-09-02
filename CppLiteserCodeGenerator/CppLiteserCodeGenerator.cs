using System.IO;
using System.Collections.Generic;

using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;
using ClassTools.Plugin;

namespace ClassTools
{
    public class CppLiteserSerializerGenerator : IPlugin
    {
        #region Fields
        private string name = "C++ Lite Serializer Code Generator";
        private string description = "Generates liteser serialization/deserialization code in C++ for liteser.";
        private string author = "Boris Mikić";
        private string version = "1.0";
        private string toolId = "ClassMaker";
        private StreamWriter writer;
        private int indent;
        private Dictionary<string, string> loadTypeConversions;
        #endregion

        #region Properties
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public string Author { get { return author; } }
        public string Version { get { return version; } }
        public string ToolId { get { return toolId; } }
        #endregion

        #region Main
        public void Create()
        {
            this.loadTypeConversions = new Dictionary<string, string>();
            this.loadTypeConversions[Constants.TYPE_BOOL] = Constants.TYPE_BOOL;
            this.loadTypeConversions[Constants.TYPE_CHAR] = Constants.TYPE_CHAR;
            this.loadTypeConversions[Constants.TYPE_DOUBLE] = Constants.TYPE_DOUBLE;
            this.loadTypeConversions[Constants.TYPE_FLOAT] = Constants.TYPE_FLOAT;
            this.loadTypeConversions[Constants.TYPE_INT] = Constants.TYPE_INT;
            this.loadTypeConversions[Constants.TYPE_LONG] = Constants.TYPE_LONG;
            this.loadTypeConversions[Constants.TYPE_SHORT] = Constants.TYPE_SHORT;
            this.loadTypeConversions[Constants.TYPE_UCHAR] = "uchar";
            this.loadTypeConversions[Constants.TYPE_UINT] = "uint";
            this.loadTypeConversions[Constants.TYPE_ULONG] = "ulong";
            this.loadTypeConversions[Constants.TYPE_USHORT] = "ushort";
        }

        public void Destroy() { }

        public string Execute(Base data, string path)
        {
            Model model = (Model)data;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            this.writer = new StreamWriter(new FileStream(path + "/CppLiteserCodeGenerator.cpp", FileMode.Create));
            this.indent = 0;
            foreach (MetaClass metaClass in model.Classes)
            {
                if (metaClass.CanSerialize)
                {
                    this.generateSerializeClass(metaClass);
                    this.generateDeserializeClass(metaClass);
                }
            }
            this.writer.Close();
            return "Code generation was successful.";
        }
        #endregion

        #region Generate
        private void generateSerializeClass(MetaClass metaClass)
        {
            if (metaClass.HasSuperClass)
            {
                this.writeLine("LS_SER_BEGIN({0}, {1})", metaClass.GetNameWithModule(), metaClass.SuperClass.GetNameWithModule());
            }
            else
            {
                this.writeLine("LS_SER_BEGIN({0}, liteser::Serializable)", metaClass.GetNameWithModule());
            }
            this.increaseIndent();
            foreach (MetaVariable metaVariable in metaClass.Variables)
            {
                if (metaVariable.CanSerialize)
                {
                    this.generateSerializeVariable(metaVariable);
                }
            }
            this.decreaseIndent();
            this.writeLine("LS_SER_END");
            this.writeLine();
        }

        private void generateDeserializeClass(MetaClass metaClass)
        {
            if (metaClass.HasSuperClass)
            {
                this.writeLine("LS_DES_BEGIN({0}, {1})", metaClass.GetNameWithModule(), metaClass.SuperClass.GetNameWithModule());
            }
            else
            {
                this.writeLine("LS_DES_BEGIN({0}, liteser::Serializable)", metaClass.GetNameWithModule());
            }
            this.increaseIndent();
            foreach (MetaVariable metaVariable in metaClass.Variables)
            {
                if (metaVariable.CanSerialize)
                {
                    this.generateDeserializeVariable(metaVariable);
                }
            }
            this.decreaseIndent();
            this.writeLine("LS_DES_END");
            this.writeLine();
        }

        private void generateSerializeVariable(MetaVariable metaVariable)
        {
            if (!metaVariable.CanSerialize)
            {
                return;
            }
            MetaType type = metaVariable.Type;
            MetaType subType1 = metaVariable.Type.SubType1;
            MetaType subType2 = metaVariable.Type.SubType2;
            switch (type.CategoryType)
            {
                case ECategoryType.Integral:
                    this.generateSer("", metaVariable.Name);
                    break;
                case ECategoryType.Class:
                    this.generateSer((!metaVariable.Nullable ? "_OBJ" : "_OBJ_PTR"), metaVariable.Name);
                    break;
                case ECategoryType.List:
                    if (subType1.CategoryType == ECategoryType.Integral)
                    {
                        this.generateSer("_HARRAY", metaVariable.Name);
                    }
                    else if (type.Suffix1 == string.Empty)
                    {
                        this.generateSer("_HARRAY_OBJ", metaVariable.Name);
                    }
                    else
                    {
                        this.generateSer("_HARRAY_OBJ_PTR", metaVariable.Name);
                    }
                    break;
                case ECategoryType.Dictionary:
                    if (subType1.CategoryType == ECategoryType.Integral)
                    {
                        if (subType2.CategoryType == ECategoryType.Integral)
                        {
                            this.generateSer("_HMAP", metaVariable.Name, subType1.Name, subType2.Name);
                        }
                        else if (type.Suffix2 == string.Empty)
                        {
                            this.generateSer("_HMAP_V_OBJ", metaVariable.Name, subType1.Name, subType2.GetNameWithModule());
                        }
                        else
                        {
                            this.generateSer("_HMAP_V_OBJ_PTR", metaVariable.Name, subType1.Name, subType2.GetNameWithModule());
                        }
                    }
                    else if (type.Suffix1 == string.Empty)
                    {
                        if (subType2.CategoryType == ECategoryType.Integral)
                        {
                            this.generateSer("_HMAP_K_OBJ", metaVariable.Name, subType1.GetNameWithModule(), subType2.Name);
                        }
                        else if (type.Suffix2 == string.Empty)
                        {
                            this.generateSer("_HMAP_K_OBJ_V_OBJ", metaVariable.Name, subType1.GetNameWithModule(), subType2.GetNameWithModule());
                        }
                        else
                        {
                            this.generateSer("_HMAP_K_OBJ_V_OBJ_PTR", metaVariable.Name, subType1.GetNameWithModule(), subType2.GetNameWithModule());
                        }
                    }
                    else
                    {
                        if (subType2.CategoryType == ECategoryType.Integral)
                        {
                            this.generateSer("_HMAP_K_OBJ_PTR", metaVariable.Name, subType1.GetNameWithModule(), subType2.Name);
                        }
                        else if (type.Suffix2 == string.Empty)
                        {
                            this.generateSer("_HMAP_K_OBJ_PTR_V_OBJ", metaVariable.Name, subType1.GetNameWithModule(), subType2.GetNameWithModule());
                        }
                        else
                        {
                            this.generateSer("_HMAP_K_OBJ_PTR_V_OBJ_PTR", metaVariable.Name, subType1.GetNameWithModule(), subType2.GetNameWithModule());
                        }
                    }
                    break;
            }
        }

        private void generateDeserializeVariable(MetaVariable metaVariable)
        {
            if (!metaVariable.CanSerialize)
            {
                return;
            }
            MetaType type = metaVariable.Type;
            MetaType subType1 = metaVariable.Type.SubType1;
            MetaType subType2 = metaVariable.Type.SubType2;
            switch (type.CategoryType)
            {
                case ECategoryType.Integral:
                    this.generateDes("", metaVariable.Name, this.convert(type.Name));
                    break;
                case ECategoryType.Class:
                    this.generateDes((!metaVariable.Nullable ? "_OBJ" : "_OBJ_PTR"), metaVariable.Name, type.GetNameWithModule());
                    break;
                case ECategoryType.List:
                    if (subType1.CategoryType == ECategoryType.Integral)
                    {
                        this.generateDes("_HARRAY", metaVariable.Name, this.convert(subType1.Name));
                    }
                    else if (type.Suffix1 == string.Empty)
                    {
                        this.generateDes("_HARRAY_OBJ", metaVariable.Name);
                    }
                    else
                    {
                        this.generateDes("_HARRAY_OBJ_PTR", metaVariable.Name, subType1.GetNameWithModule());
                    }
                    break;
                case ECategoryType.Dictionary:
                    if (subType1.CategoryType == ECategoryType.Integral)
                    {
                        if (subType2.CategoryType == ECategoryType.Integral)
                        {
                            this.generateDes("_HMAP", metaVariable.Name, subType1.Name, this.convert(subType1.Name), subType2.Name, this.convert(subType2.Name));
                        }
                        else if (type.Suffix2 == string.Empty)
                        {
                            this.generateDes("_HMAP_V_OBJ", metaVariable.Name, subType1.Name, this.convert(subType1.Name), subType2.GetNameWithModule());
                        }
                        else
                        {
                            this.generateDes("_HMAP_V_OBJ_PTR", metaVariable.Name, subType1.Name, this.convert(subType1.Name),subType2.GetNameWithModule());
                        }
                    }
                    else if (type.Suffix1 == string.Empty)
                    {
                        if (subType2.CategoryType == ECategoryType.Integral)
                        {
                            this.generateDes("_HMAP_K_OBJ", metaVariable.Name,subType1.GetNameWithModule(), subType2.Name, this.convert(subType2.Name));
                        }
                        else if (type.Suffix2 == string.Empty)
                        {
                            this.generateDes("_HMAP_K_OBJ_V_OBJ", metaVariable.Name, subType1.GetNameWithModule(), subType2.GetNameWithModule());
                        }
                        else
                        {
                            this.generateDes("_HMAP_K_OBJ_V_OBJ_PTR", metaVariable.Name, subType1.GetNameWithModule(), subType2.GetNameWithModule());
                        }
                    }
                    else
                    {
                        if (subType2.CategoryType == ECategoryType.Integral)
                        {
                            this.generateDes("_HMAP_K_OBJ_PTR", metaVariable.Name, subType1.GetNameWithModule(), subType2.Name, this.convert(subType2.Name));
                        }
                        else if (type.Suffix2 == string.Empty)
                        {
                            this.generateDes("_HMAP_K_OBJ_PTR_V_OBJ", metaVariable.Name, subType1.GetNameWithModule(), subType2.GetNameWithModule());
                        }
                        else
                        {
                            this.generateDes("_HMAP_K_OBJ_PTR_V_OBJ_PTR", metaVariable.Name, subType1.GetNameWithModule(), subType2.GetNameWithModule());
                        }
                    }
                    break;
            }
        }
        #endregion

        #region Utility
        private string convert(string key)
        {
            if (this.loadTypeConversions.ContainsKey(key))
            {
                return this.loadTypeConversions[key];
            }
            return "hstr";
        }

        private void writeLine()
        {
            this.writer.WriteLine(this.getIndentation());
        }

        private void writeLine(string str)
        {
            this.writer.WriteLine(this.getIndentation() + str);
        }

        private void writeLine(string format, params string[] args)
        {
            this.writer.WriteLine(this.getIndentation() + format, args);
        }

        private void generateSer(string suffix, params string[] args)
        {
            this.writeLine("LS_SER{0}({1})", suffix, string.Join(", ", args));
        }

        private void generateDes(string suffix, params string[] args)
        {
            this.writeLine("LS_DES{0}({1})", suffix, string.Join(", ", args));
        }

        private string getIndentation()
        {
            string result = string.Empty;
            for (int i = 0; i < this.indent; i++)
            {
                result += "\t";
            }
            return result;
        }

        private void increaseIndent()
        {
            this.indent++;
        }

        private void decreaseIndent()
        {
            this.indent--;
        }
        #endregion

    }
}
