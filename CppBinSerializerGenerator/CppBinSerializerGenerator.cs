using System.IO;
using System.Collections.Generic;

using ClassTools.Model;

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

        public string Execute(ClassModel model, ModelDatabase database, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            this.writer = new StreamWriter(new FileStream(string.Format("{0}/CppBinSerializerGenerator.cpp", path), FileMode.Create));
            this.indent = 0;
            foreach (MetaClass classe in model.Classes)
            {
                if (classe.CanSerialize)
                {
                    this.generateSerializeClass(classe);
                }
            }
            this.writer.Close();
            return "Code Generation was successful.";
        }

        #endregion

        #region Generate
        private void generateSerializeClass(MetaClass classe)
        {
            this.writeLine("namespace {0}", classe.Module);
            this.openBrackets();
            this.writeLine("void {0}::serialize(hfile* file)", classe.Name);
            this.openBrackets();
            if (classe.HasSuperClass)
            {
                this.generateSerialize("{0}::", classe.SuperClass.Name);
            }
            foreach (MetaVariable variable in classe.Variables)
            {
                if (variable.CanSerialize)
                {
                    this.generateSerializeVariable(variable);
                }
            }
            this.closeBrackets();
            this.closeBrackets();
            this.writeLine();
        }

        private void generateDeserializeClass(MetaClass classe)
        {
            this.writeLine("namespace {0}", classe.Module);
            this.openBrackets();
            this.writeLine("void {0}::deserialize(hfile* file)", classe.Name);
            this.openBrackets();
            this.writeLine("int number = 0;");
            this.writeLine("bool exists = false;");
            this.writeLine("hstr name = \"\";");
            if (classe.HasSuperClass)
            {
                this.generateDeserialize("{0}::", classe.SuperClass.Name);
            }
            foreach (MetaVariable variable in classe.Variables)
            {
                if (variable.CanSerialize)
                {
                    this.generateDeserializeVariable(variable);
                }
            }
            this.closeBrackets();
            this.closeBrackets();
            this.writeLine();
        }

        private void generateSerializeVariable(MetaVariable variable)
        {
            switch (variable.Type.TypeCategory)
            {
                case ETypeCategory.Normal:
                    if (!variable.Type.IsClass)
                    {
                        this.generateWrite("this->{0}", variable.Name);
                    }
                    else if (variable.Type.CanSerialize && variable.Prefix == string.Empty)
                    {
                        this.generateSerialize("this->{0}.", variable.Name);
                    }
                    else
                    {
                        this.writeIf("this->{0} != NULL", variable.Name);
                        this.generateWrite("true");
                        this.generateWrite("this->{0}->Name", variable.Name);
                        this.writeElse();
                        this.generateWrite("false");
                        this.closeBrackets();
                    }
                    break;
                case ETypeCategory.Collection:
                    this.generateWrite("this->{0}.size()", variable.Name);
                    this.writeLine("foreach ({0}{1}, it, this->{2})", variable.Type.SubType1.GetNameWithModule(), variable.Type.Prefix, variable.Name);
                    this.openBrackets();
                    if (!variable.Type.SubType1.IsClass)
                    {
                        this.generateWrite("(*it)");
                    }
                    else if (variable.Type.SubType1.CanSerialize)
                    {
                        if (variable.Prefix == string.Empty)
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

        private void generateDeserializeVariable(MetaVariable variable)
        {
            switch (variable.Type.TypeCategory)
            {
                case ETypeCategory.Normal:
                    if (!variable.Type.IsClass)
                    {
                        this.generateRead(variable.Type, "this->{0}", variable.Name);
                    }
                    else if (variable.Type.CanSerialize && variable.Prefix == string.Empty)
                    {
                        this.generateDeserialize("this->{0}.", variable.Name);
                    }
                    else
                    {
                        this.generateReadBool("exists");
                        this.writeIf("exists");
                        this.generateReadString("name");
                        if (variable.Type.CanSerialize)
                        {
                            this.writeLine("this->{0} = System::game->{1}ByName(name);", variable.Name, this.getCamelCaseGetter(variable));
                        }
                        else
                        {
                            this.writeLine("this->{0} = System::dataManager->{1}s[name];", variable.Name, this.getCamelCaseVariable(variable));
                        }
                        this.writeElse();
                        this.writeLine("this->{0} = NULL;", variable.Name);
                        this.closeBrackets();
                    }
                    break;
                case ETypeCategory.Collection:
                    this.generateReadInt("number");
                    this.writeLine("{0}{1} _{2};", variable.Type.SubType1.GetNameWithModule(), variable.Type.Prefix, variable.Name);
                    this.writeLine("for (int i = 0; i < number; i++)");
                    this.openBrackets();
                    if (!variable.Type.SubType1.IsClass)
                    {
                        this.generateRead(variable.Type.SubType1, variable.Name);
                        this.writeLine("this->{0} += _{0};", variable.Name);
                    }
                    else if (variable.Type.SubType1.CanSerialize)
                    {
                        if (variable.Prefix == string.Empty)
                        {
                            this.writeLine("_{0} = {1}();", variable.Name, variable.Type.SubType1.GetNameWithModule());
                            this.generateDeserialize("_{0}.", variable.Name);
                        }
                        else
                        {
                            this.writeLine("_{0} = new {1}();", variable.Name, variable.Type.SubType1.GetNameWithModule());
                            this.generateDeserialize("_{0}->", variable.Name);
                        }
                        this.writeLine("this->{0} += _{0};", variable.Name);
                    }
                    else
                    {
                        this.generateReadString("name");
                        this.writeLine("this->{0} += System::dataManager->{1}s[name];", variable.Name, this.getCamelCaseVariable(variable));
                    }
                    this.closeBrackets();
                    break;
            }
        }
        #endregion

        #region Utility

        private string getCamelCaseVariable(MetaVariable variable)
        {
            string name = string.Empty;
            switch (variable.Type.TypeCategory)
            {
                case ETypeCategory.Normal:
                    name = variable.Type.Name;
                    break;
                case ETypeCategory.Collection:
                    name = variable.Type.SubType1.Name;
                    break;
                case ETypeCategory.Dictionary:
                    name = variable.Type.SubType2.Name;
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

        private string getCamelCaseGetter(MetaVariable variable)
        {
            return ("get" + this.getCamelCaseVariable(variable));
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

        private void generateRead(string line, MetaType type)
        {
            this.writeLine(string.Format("{0} = file->load_{1}();", line, type.Name));
        }

        private void generateRead(MetaType type, string format, params object[] args)
        {
            this.writeLine(string.Format("{0} = file->load_{1}();", format, type.Name), args);
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
