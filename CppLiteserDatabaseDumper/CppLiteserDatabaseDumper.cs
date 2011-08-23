using System.IO;
using System.Collections.Generic;

using ClassTools.Model;

namespace ClassTools
{
    public class CppLiteserDatabaseDumper : IPlugin
    {
        #region Fields

        private string name = "C++ Lite Serializer Database Dumper";
        private string description = "Dumps database into liteser serialization format.";
        private string author = "Boris Mikić";
        private string version = "0.1";
        private string path = string.Empty;
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
            this.path = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath;
            foreach (MetaClass classe in model.Classes)
            {
                this.indent = 0;
                fullPath = string.Format("{0}/{1}.h", this.path, classe.Path);
                this.createFilePath(fullPath);
                this.writer = new StreamWriter(new FileStream(fullPath, FileMode.Create));
                this.generateHeaderClass(classe);
                this.writer.Close();
                fullPath = string.Format("{0}/{1}.cpp", this.path, classe.Path);
                this.createFilePath(fullPath);
                this.writer = new StreamWriter(new FileStream(fullPath, FileMode.Create));
                this.generateImplementationClass(classe);
                this.writer.Close();
            }
            return "Code Generation was successful.";
        }

        #endregion

        #region Generate

        private void generateHeaderClass(MetaClass classe)
        {
            this.writeLine("/// @file");
            this.writeLine("/// @author {0}", this.Name);
            this.writeLine("/// @version {0}", this.Version);
            this.writeLine();
            this.writeLine("#ifndef {0}_{1}_H", classe.Module.ToUpper(), classe.Name.ToUpper());
            this.writeLine("#ifndef {0}_{1}_H", classe.Module.ToUpper(), classe.Name.ToUpper());
            this.writeLine("#ifndef {0}_{1}_H", classe.Module.ToUpper(), classe.Name.ToUpper());
            this.writeLine("#define {0}_{1}_H", classe.Module.ToUpper(), classe.Name.ToUpper());
            this.writeLine();
            if (classe.CanSerialize)
            {
                this.writeLine("#include <liteser/liteser.h>");
                this.writeLine();
            }
            this.writeLine("namespace {0}", classe.Module);
            this.openBrackets();
            string classDef = string.Format("class {0}", classe.Name);
            if (classe.HasSuperClass)
            {
                if (classe.Module == classe.SuperClass.Module)
                {
                    classDef += string.Format(" : {0}", classe.SuperClass.Name);
                }
                else
                {
                    classDef += string.Format(" : {0}", classe.SuperClass.GetNameWithModule());
                }
            }
            this.writeLine(classDef);
            this.writeLine("{");
            this.writeLine(ClassModel.AccessorNames[(int)EAccessType.Public]);
            this.increaseIndent();
            if (classe.CanSerialize)
            {
                this.writeLine("LS_MAKE_SERIALIZABLE;");
            }
            this.writeLine("{0}();", classe.Name);
            if (classe.HasSuperClass)
            {
                this.writeLine("~{0}();", classe.Name);
            }
            else
            {
                this.writeLine("virtual ~{0}();", classe.Name);
            }
            this.writeLine();
            this.generateHeaderMembers(classe);
            this.closeBrackets();
            this.closeBrackets();
            this.writeLine("#endif");
        }

        private void generateHeaderMembers(MetaClass classe)
        {
            this.generateHeaderVariables(classe, EAccessType.Public);
            this.generateHeaderGettersSetters(classe);
            this.generateHeaderMethods(classe, EAccessType.Public);
            EAccessType accessType;
            List<MetaVariable> variables;
            List<MetaMethod> methods;
            for (int i = 1; i < ClassModel.AccessorNames.Length; i++)
            {
                accessType = (EAccessType)i;
                variables = classe.Variables.FindAll(v => v.AccessType == accessType);
                methods = classe.Methods.FindAll(m => m.AccessType == accessType);
                if (variables.Count > 0 || methods.Count > 0)
                {
                    this.decreaseIndent();
                    this.writeLine("{0}:", ClassModel.AccessorNames[i]);
                    this.increaseIndent();
                    this.generateHeaderVariables(variables);
                    this.generateHeaderMethods(methods);
                }
            }
        }

        private void generateHeaderVariables(MetaClass classe, EAccessType accessType)
        {
            this.generateHeaderVariables(classe.Variables.FindAll(v => v.AccessType == accessType));
        }

        private void generateHeaderMethods(MetaClass classe, EAccessType accessType)
        {
            this.generateHeaderMethods(classe.Methods.FindAll(m => m.AccessType == accessType));
        }

        private void generateHeaderVariables(List<MetaVariable> variables)
        {
            foreach (MetaVariable variable in variables)
            {
                this.writeLine("{0}{1} {2};", variable.Type.GetNameWithModule(), variable.Prefix, variable.Name);
            }
            if (variables.Count > 0)
            {
                this.writeLine();
            }
        }

        private void generateHeaderMethods(List<MetaMethod> methods)
        {
            foreach (MetaMethod method in methods)
            {
                this.writeLine("{0}{1} {2}({3});", method.Type.GetNameWithModule(), method.Prefix, method.Name, this.getParameterString(method));
            }
            if (methods.Count > 0)
            {
                this.writeLine();
            }
        }

        private void generateHeaderGettersSetters(MetaClass classe)
        {
            List<MetaVariable> variables = classe.Variables.FindAll(v => v.Getter || v.Setter);
            foreach (MetaVariable variable in variables)
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
            if (variables.Count > 0)
            {
                this.writeLine();
            }
        }

        private void generateHeaderVariable(MetaVariable variable)
        {
            this.writeLine("{0}{1} {2};", variable.Type.GetNameWithModule(), variable.Prefix, variable.Name);
        }

        private void generateHeaderMethod(MetaMethod method)
        {
            this.writeLine("{0}{1} {2}({3});", method.Type.GetNameWithModule(), method.Prefix, method.Name, this.getParameterString(method));
        }

        private void generateImplementationClass(MetaClass classe)
        {
            this.writeLine("/// @file");
            this.writeLine("/// @author {0}", this.Name);
            this.writeLine("/// @version {0}", this.Version);
            this.writeLine();
            if (classe.CanSerialize)
            {
                this.writeLine("#include <liteser/liteser.h>", classe.Path);
                this.writeLine();
            }
            this.writeLine("#include \"{0}.h\"", classe.Path);
            this.writeLine();
            this.writeLine("namespace {0}", classe.Module);
            this.openBrackets();
            string constructorDef = string.Format("{0}::{1}()", classe.Name, classe.Name);
            List<string> initializers = new List<string>();
            if (classe.HasSuperClass)
            {
                if (classe.Module == classe.SuperClass.Module)
                {
                    initializers.Add(string.Format("{0}()", classe.SuperClass.Name));
                }
                else
                {
                    initializers.Add(string.Format("{0}()", classe.SuperClass.GetNameWithModule()));
                }
            }
            List<MetaVariable> variables = classe.Variables.FindAll(v => v.DefaultValue != string.Empty);
            for (int i = 1; i < variables.Count; i++)
            {
                initializers.Add(string.Format("{0}({1})", variables[i].Name, variables[i].DefaultValue));
            }
            if (initializers.Count > 0)
            {
                constructorDef += " : " + string.Join(", ", initializers.ToArray());
            }
            this.writeLine(constructorDef);
            this.openBrackets();
            this.closeBrackets();
            this.writeLine();
            this.writeLine("{0}::~{1}()\r\n", classe.Name, classe.Name);
            this.openBrackets();
            this.closeBrackets();
            this.writeLine();
            foreach (MetaMethod method in classe.Methods)
            {
                this.writeLine("{0}{1} {2}::{3}({4})", method.Type.GetNameWithModule(), method.Prefix, classe.Name, method.Name, this.getParameterString(method));
                this.openBrackets();
                string[] lines = method.Implementation.Replace("\r", string.Empty).Split('\n');
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

        private string getParameterString(MetaMethod method)
        {
            if (method.Parameters.Count == 0)
            {
                return string.Empty;
            }
            string result = string.Format("{0} {1}", method.Parameters[0].Type.Name, method.Parameters[0].Name);
            for (int i = 1; i < method.Parameters.Count; i++)
            {
                result += string.Format(", {0} {1}", method.Parameters[0].Type.Name, method.Parameters[0].Name);
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
