using System.IO;
using System.Collections.Generic;

using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools
{
    public class CppBinSerializerGenerator : IPlugin
    {
        #region Fields
        private string name = "C++ Binary Serialization Code Generator";
        private string description = "Generates binary serialization/deserialization code in C++ for liteser.";
        private string author = "Boris Mikić";
        private string version = "0.9";
        private StreamWriter writer;
        private int indent;
        #endregion

        #region Properties
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public string Author { get { return author; } }
        public string Version { get { return version; } }
        #endregion

        #region Main
        public void Create() { }
        public void Destroy() { }

        public string Execute(Model model, Repository repository, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            this.writer = new StreamWriter(new FileStream(string.Format("{0}/CppBinSerializerGenerator.cpp", path), FileMode.Create));
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
            return "Code Generation was successful.";
        }
        #endregion

        #region Generate
        private void generateSerializeClass(MetaClass metaClass)
        {
            this.writeLine("namespace {0}", metaClass.Module);
            this.openBrackets();
            this.writeLine("void {0}::serialize(hfile* file)", metaClass.Name);
            this.openBrackets();
            if (metaClass.HasSuperClass)
            {
                this.generateSerialize("{0}::", metaClass.SuperClass.Name);
            }
            foreach (MetaVariable metaVariable in metaClass.Variables)
            {
                if (metaVariable.CanSerialize)
                {
                    this.generateSerializeVariable(metaVariable);
                }
            }
            this.closeBrackets();
            this.closeBrackets();
            this.writeLine();
        }

        private void generateDeserializeClass(MetaClass metaClass)
        {
            this.writeLine("namespace {0}", metaClass.Module);
            this.openBrackets();
            this.writeLine("void {0}::deserialize(hfile* file)", metaClass.Name);
            this.openBrackets();
            this.writeLine("int number = 0;");
            this.writeLine("bool exists = false;");
            this.writeLine("hstr name = \"\";");
            if (metaClass.HasSuperClass)
            {
                this.generateDeserialize("{0}::", metaClass.SuperClass.Name);
            }
            foreach (MetaVariable metaVariable in metaClass.Variables)
            {
                if (metaVariable.CanSerialize)
                {
                    this.generateDeserializeVariable(metaVariable);
                }
            }
            this.closeBrackets();
            this.closeBrackets();
            this.writeLine();
        }

        private void generateSerializeVariable(MetaVariable metaVariable)
        {
            switch (metaVariable.Type.CategoryType)
            {
                case ECategoryType.Normal:
                    if (!metaVariable.Type.IsClass)
                    {
                        this.generateWrite("this->{0}", metaVariable.Name);
                    }
                    else if (metaVariable.Type.CanSerialize && metaVariable.Prefix == string.Empty)
                    {
                        this.generateSerialize("this->{0}.", metaVariable.Name);
                    }
                    else
                    {
                        this.writeIf("this->{0} != NULL", metaVariable.Name);
                        this.generateWrite("true");
                        this.generateWrite("this->{0}->Name", metaVariable.Name);
                        this.writeElse();
                        this.generateWrite("false");
                        this.closeBrackets();
                    }
                    break;
                case ECategoryType.List:
                    this.generateWrite("this->{0}.size()", metaVariable.Name);
                    this.writeLine("foreach ({0}{1}, it, this->{2})", metaVariable.Type.SubType1.GetNameWithModule(), metaVariable.Type.Prefix, metaVariable.Name);
                    this.openBrackets();
                    if (!metaVariable.Type.SubType1.IsClass)
                    {
                        this.generateWrite("(*it)");
                    }
                    else if (metaVariable.Type.SubType1.CanSerialize)
                    {
                        if (metaVariable.Prefix == string.Empty)
                        {
                            this.generateSerialize("it->");
                        }
                        else
                        {
                            this.generateSerialize("(*it)->");
                        }
                    }
                    else
                    {
                        this.generateWrite("(*it)->Name");
                    }
                    this.closeBrackets();
                    break;
            }
        }

        private void generateDeserializeVariable(MetaVariable metaVariable)
        {
            switch (metaVariable.Type.CategoryType)
            {
                case ECategoryType.Normal:
                    if (!metaVariable.Type.IsClass)
                    {
                        this.generateRead(metaVariable.Type, "this->{0}", metaVariable.Name);
                    }
                    else if (metaVariable.Type.CanSerialize && metaVariable.Prefix == string.Empty)
                    {
                        this.generateDeserialize("this->{0}.", metaVariable.Name);
                    }
                    else
                    {
                        this.generateReadBool("exists");
                        this.writeIf("exists");
                        this.generateReadString("name");
                        if (metaVariable.Type.CanSerialize)
                        {
                            this.writeLine("this->{0} = System::game->{1}ByName(name);", metaVariable.Name, this.getCamelCaseGetter(metaVariable));
                        }
                        else
                        {
                            this.writeLine("this->{0} = System::dataManager->{1}s[name];", metaVariable.Name, this.getCamelCaseVariable(metaVariable));
                        }
                        this.writeElse();
                        this.writeLine("this->{0} = NULL;", metaVariable.Name);
                        this.closeBrackets();
                    }
                    break;
                case ECategoryType.List:
                    this.generateReadInt("number");
                    this.writeLine("{0}{1} _{2};", metaVariable.Type.SubType1.GetNameWithModule(), metaVariable.Type.Prefix, metaVariable.Name);
                    this.writeLine("for (int i = 0; i < number; i++)");
                    this.openBrackets();
                    if (!metaVariable.Type.SubType1.IsClass)
                    {
                        this.generateRead(metaVariable.Type.SubType1, metaVariable.Name);
                        this.writeLine("this->{0} += _{0};", metaVariable.Name);
                    }
                    else if (metaVariable.Type.SubType1.CanSerialize)
                    {
                        if (metaVariable.Prefix == string.Empty)
                        {
                            this.writeLine("_{0} = {1}();", metaVariable.Name, metaVariable.Type.SubType1.GetNameWithModule());
                            this.generateDeserialize("_{0}.", metaVariable.Name);
                        }
                        else
                        {
                            this.writeLine("_{0} = new {1}();", metaVariable.Name, metaVariable.Type.SubType1.GetNameWithModule());
                            this.generateDeserialize("_{0}->", metaVariable.Name);
                        }
                        this.writeLine("this->{0} += _{0};", metaVariable.Name);
                    }
                    else
                    {
                        this.generateReadString("name");
                        this.writeLine("this->{0} += System::dataManager->{1}s[name];", metaVariable.Name, this.getCamelCaseVariable(metaVariable));
                    }
                    this.closeBrackets();
                    break;
            }
        }
        #endregion

        #region Utility
        private string getCamelCaseVariable(MetaVariable metaVariable)
        {
            string name = string.Empty;
            switch (metaVariable.Type.CategoryType)
            {
                case ECategoryType.Normal:
                    name = metaVariable.Type.Name;
                    break;
                case ECategoryType.List:
                    name = metaVariable.Type.SubType1.Name;
                    break;
                case ECategoryType.Dictionary:
                    name = metaVariable.Type.SubType2.Name;
                    break;
            }
            name = name.ToUpper().Substring(0, 1) + name.Substring(1);
            if (name.Substring(name.Length - 1) == "y")
            {
                name = string.Format("{0}ie", name.Substring(0, name.Length - 1));
            }
            else
            {
                name = string.Format("{0}", name);
            }
            return name;
        }

        private string getCamelCaseGetter(MetaVariable metaVariable)
        {
            return ("get" + this.getCamelCaseVariable(metaVariable));
        }

        private void writeLine()
        {
            this.writer.WriteLine(this.getIndentation());
        }

        private void writeLine(string str)
        {
            this.writer.WriteLine(this.getIndentation() + str);
        }

        private void writeLine(string format, params object[] args)
        {
            this.writer.WriteLine(this.getIndentation() + format, args);
        }

        private void generateWrite(string line)
        {
            this.writeLine("file->dump({0});", line);
        }

        private void generateWrite(string format, params object[] args)
        {
            this.writeLine(string.Format("file->dump({0});", format), args);
        }

        private void generateReadInt(string line)
        {
            this.writeLine(string.Format("{0} = file->load_int();", line));
        }

        private void generateReadBool(string line)
        {
            this.writeLine(string.Format("{0} = file->load_bool();", line));
        }

        private void generateReadString(string line)
        {
            this.writeLine(string.Format("{0} = file->load_hstr();", line));
        }

        private void generateRead(string line, MetaType metaType)
        {
            this.writeLine(string.Format("{0} = file->load_{1}();", line, metaType.Name));
        }

        private void generateRead(MetaType metaType, string format, params object[] args)
        {
            this.writeLine(string.Format("{0} = file->load_{1}();", format, metaType.Name), args);
        }

        private void generateSerialize(string line)
        {
            this.writeLine("{0}serialize(file);", line);
        }

        private void generateSerialize(string format, params object[] args)
        {
            this.writeLine(string.Format("{0}serialize(file);", format), args);
        }

        private void generateDeserialize(string line)
        {
            this.writeLine("{0}deserialize(file);", line);
        }

        private void generateDeserialize(string format, params object[] args)
        {
            this.writeLine(string.Format("{0}deserialize(file);", format), args);
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

        private void openBrackets()
        {
            this.writeLine("{");
            this.indent++;
        }

        private void closeBrackets()
        {
            this.indent--;
            this.writeLine("}");
        }

        private void writeIf(string condition)
        {
            this.writeLine("if ({0})", condition);
            this.openBrackets();
        }

        private void writeIf(string format, params object[] args)
        {
            this.writeLine(string.Format("if ({0})", format), args);
            this.openBrackets();
        }

        private void writeElse()
        {
            this.closeBrackets();
            this.writeLine("else");
            this.openBrackets();
        }
        #endregion

    }
}
