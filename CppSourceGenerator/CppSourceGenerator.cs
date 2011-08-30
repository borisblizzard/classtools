using System.IO;
using System.Collections.Generic;

using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools
{
    public class CppSourceGenerator : IPlugin
    {
        #region Fields
        private string name = "C++ Source Generator";
        private string description = "Generates C++ source code and headers.";
        private string author = "Boris Mikić";
        private string version = "0.9";
        private string toolId = "ClassMaker";
        private string path = string.Empty;
        private StreamWriter writer;
        private int indent;
        #endregion

        #region Properties
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public string Author { get { return author; } }
        public string Version { get { return version; } }
        public string ToolId { get { return toolId; } }
        #endregion

        #region Main
        public void Create() { }
        public void Destroy() { }

        public string Execute(Base data, string path)
        {
            Model model = (Model)data;
            this.path = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath;
            foreach (MetaClass metaClass in model.Classes)
            {
                this.indent = 0;
                fullPath = string.Format("{0}/{1}.h", this.path, metaClass.Path);
                this.createFilePath(fullPath);
                this.writer = new StreamWriter(new FileStream(fullPath, FileMode.Create));
                this.generateHeaderClass(metaClass);
                this.writer.Close();
                fullPath = string.Format("{0}/{1}.cpp", this.path, metaClass.Path);
                this.createFilePath(fullPath);
                this.writer = new StreamWriter(new FileStream(fullPath, FileMode.Create));
                this.generateImplementationClass(metaClass);
                this.writer.Close();
            }
            return "Code Generation was successful.";
        }
        #endregion

        #region Generate
        private void generateHeaderClass(MetaClass metaClass)
        {
            this.writeLine("/// @file");
            this.writeLine("/// @author {0}", this.Name);
            this.writeLine("/// @version {0}", this.Version);
            this.writeLine();
            this.writeLine("#ifndef {0}_{1}_H", metaClass.Module.ToUpper(), metaClass.Name.ToUpper());
            this.writeLine("#ifndef {0}_{1}_H", metaClass.Module.ToUpper(), metaClass.Name.ToUpper());
            this.writeLine("#ifndef {0}_{1}_H", metaClass.Module.ToUpper(), metaClass.Name.ToUpper());
            this.writeLine("#define {0}_{1}_H", metaClass.Module.ToUpper(), metaClass.Name.ToUpper());
            this.writeLine();
            if (metaClass.CanSerialize)
            {
                this.writeLine("#include <liteser/liteser.h>");
                this.writeLine();
            }
            this.writeLine("namespace {0}", metaClass.Module);
            this.openBrackets();
            string classDef = string.Format("class {0}", metaClass.Name);
            if (metaClass.HasSuperClass)
            {
                if (metaClass.Module == metaClass.SuperClass.Module)
                {
                    classDef += string.Format(" : {0}", metaClass.SuperClass.Name);
                }
                else
                {
                    classDef += string.Format(" : {0}", metaClass.SuperClass.GetNameWithModule());
                }
            }
            this.writeLine(classDef);
            this.writeLine("{");
            this.writeLine(Constants.NAMES_ACCESS[(int)EAccessType.Public]);
            this.increaseIndent();
            if (metaClass.CanSerialize)
            {
                this.writeLine("LS_MAKE_SERIALIZABLE;");
            }
            this.writeLine("{0}();", metaClass.Name);
            if (metaClass.HasSuperClass)
            {
                this.writeLine("~{0}();", metaClass.Name);
            }
            else
            {
                this.writeLine("virtual ~{0}();", metaClass.Name);
            }
            this.writeLine();
            this.generateHeaderMembers(metaClass);
            this.closeBrackets();
            this.closeBrackets();
            this.writeLine("#endif");
        }

        private void generateHeaderMembers(MetaClass metaClass)
        {
            this.generateHeaderVariables(metaClass, EAccessType.Public);
            this.generateHeaderGettersSetters(metaClass);
            this.generateHeaderMethods(metaClass, EAccessType.Public);
            EAccessType accessType;
            List<MetaVariable> metaVariables;
            List<MetaMethod> metaMethods;
            for (int i = 1; i < Constants.NAMES_ACCESS.Count; i++)
            {
                accessType = (EAccessType)i;
                metaVariables = metaClass.Variables.FindAll(v => v.AccessType == accessType);
                metaMethods = metaClass.Methods.FindAll(m => m.AccessType == accessType);
                if (metaVariables.Count > 0 || metaMethods.Count > 0)
                {
                    this.decreaseIndent();
                    this.writeLine("{0}:", Constants.NAMES_ACCESS[i]);
                    this.increaseIndent();
                    this.generateHeaderVariables(metaVariables);
                    this.generateHeaderMethods(metaMethods);
                }
            }
        }

        private void generateHeaderVariables(MetaClass metaClass, EAccessType accessType)
        {
            this.generateHeaderVariables(metaClass.Variables.FindAll(v => v.AccessType == accessType));
        }

        private void generateHeaderMethods(MetaClass metaClass, EAccessType accessType)
        {
            this.generateHeaderMethods(metaClass.Methods.FindAll(m => m.AccessType == accessType));
        }

        private void generateHeaderVariables(List<MetaVariable> metaVariables)
        {
            foreach (MetaVariable metaVariable in metaVariables)
            {
                this.writeLine("{0}{1} {2};", metaVariable.Type.GetNameWithModule(), metaVariable.Prefix, metaVariable.Name);
            }
            if (metaVariables.Count > 0)
            {
                this.writeLine();
            }
        }

        private void generateHeaderMethods(List<MetaMethod> metaMethods)
        {
            foreach (MetaMethod metaMethod in metaMethods)
            {
                this.writeLine("{0}{1} {2}({3});", metaMethod.Type.GetNameWithModule(), metaMethod.Prefix, metaMethod.Name, this.getParameterString(metaMethod));
            }
            if (metaMethods.Count > 0)
            {
                this.writeLine();
            }
        }

        private void generateHeaderGettersSetters(MetaClass metaClass)
        {
            List<MetaVariable> metaVariables = metaClass.Variables.FindAll(v => v.Getter || v.Setter);
            foreach (MetaVariable variable in metaVariables)
            {
                string name = variable.Name.ToUpper().Substring(0, 1) + variable.Name.Substring(1);
                if (variable.Getter)
                {
                    if (variable.Type.Name != "bool")
                    {
                        this.writeLine("{0}{1} get{2}() {{ return this->{3}; }}", variable.Type.GetNameWithModule(), variable.Prefix, name, variable.Name);
                    }
                    else
                    {
                        this.writeLine("{0}{1} is{2}() {{ return this->{3}; }}", variable.Type.GetNameWithModule(), variable.Prefix, name, variable.Name);
                    }
                }
                if (variable.Setter)
                {
                    this.writeLine("void set{0}({1}{2} value) {{ this->{3} = value; }}", name, variable.Type.GetNameWithModule(), variable.Prefix, variable.Name);
                }
            }
            if (metaVariables.Count > 0)
            {
                this.writeLine();
            }
        }

        private void generateHeaderVariable(MetaVariable metaVariable)
        {
            this.writeLine("{0}{1} {2};", metaVariable.Type.GetNameWithModule(), metaVariable.Prefix, metaVariable.Name);
        }

        private void generateHeaderMethod(MetaMethod metaMethod)
        {
            this.writeLine("{0}{1} {2}({3});", metaMethod.Type.GetNameWithModule(), metaMethod.Prefix, metaMethod.Name, this.getParameterString(metaMethod));
        }

        private void generateImplementationClass(MetaClass metaClass)
        {
            this.writeLine("/// @file");
            this.writeLine("/// @author {0}", this.Name);
            this.writeLine("/// @version {0}", this.Version);
            this.writeLine();
            if (metaClass.CanSerialize)
            {
                this.writeLine("#include <liteser/liteser.h>", metaClass.Path);
                this.writeLine();
            }
            this.writeLine("#include \"{0}.h\"", metaClass.Path);
            this.writeLine();
            this.writeLine("namespace {0}", metaClass.Module);
            this.openBrackets();
            string constructorDef = string.Format("{0}::{1}()", metaClass.Name, metaClass.Name);
            List<string> initializers = new List<string>();
            if (metaClass.HasSuperClass)
            {
                if (metaClass.Module == metaClass.SuperClass.Module)
                {
                    initializers.Add(string.Format("{0}()", metaClass.SuperClass.Name));
                }
                else
                {
                    initializers.Add(string.Format("{0}()", metaClass.SuperClass.GetNameWithModule()));
                }
            }
            List<MetaVariable> metaVariables = metaClass.Variables.FindAll(v => v.DefaultValue != string.Empty);
            for (int i = 1; i < metaVariables.Count; i++)
            {
                initializers.Add(string.Format("{0}({1})", metaVariables[i].Name, metaVariables[i].DefaultValue));
            }
            if (initializers.Count > 0)
            {
                constructorDef += " : " + string.Join(", ", initializers.ToArray());
            }
            this.writeLine(constructorDef);
            this.openBrackets();
            this.closeBrackets();
            this.writeLine();
            this.writeLine("{0}::~{1}()\r\n", metaClass.Name, metaClass.Name);
            this.openBrackets();
            this.closeBrackets();
            this.writeLine();
            foreach (MetaMethod metaMethod in metaClass.Methods)
            {
                this.writeLine("{0}{1} {2}::{3}({4})", metaMethod.Type.GetNameWithModule(), metaMethod.Prefix, metaClass.Name, metaMethod.Name, this.getParameterString(metaMethod));
                this.openBrackets();
                string[] lines = metaMethod.Implementation.Replace("\r", string.Empty).Split('\n');
                if (lines.Length > 1 || lines.Length > 0 && lines[0] != string.Empty)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        this.writeLine(lines[i]);
                    }
                }
                this.closeBrackets();
                this.writeLine();
            }
            this.closeBrackets();
        }
        #endregion

        #region Utility
        private void createFilePath(string filePath)
        {
            if (filePath.Contains("/"))
            {
                List<string> folders = new List<string>(filePath.Split('/'));
                folders.RemoveAt(folders.Count - 1);
                string path = string.Join("/", folders.ToArray());
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        private string getParameterString(MetaMethod metaMethod)
        {
            if (metaMethod.Parameters.Count == 0)
            {
                return string.Empty;
            }
            string result = string.Format("{0} {1}", metaMethod.Parameters[0].Type.Name, metaMethod.Parameters[0].Name);
            for (int i = 1; i < metaMethod.Parameters.Count; i++)
            {
                result += string.Format(", {0} {1}", metaMethod.Parameters[0].Type.Name, metaMethod.Parameters[0].Name);
            }
            return result;
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

        private void increaseIndent()
        {
            this.indent++;
        }

        private void decreaseIndent()
        {
            this.indent--;
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
        #endregion

    }
}
