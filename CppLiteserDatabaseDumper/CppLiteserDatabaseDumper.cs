using System.IO;
using System.Collections.Generic;

using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

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

        public string Execute(Model model, Repository repository, string path)
        {
            this.path = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            /*
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
             * */
            return "Code Generation was successful.";
        }

        #endregion

    }
}
