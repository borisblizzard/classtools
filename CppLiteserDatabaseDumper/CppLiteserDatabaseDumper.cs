using System;
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
        private string version = "0.2";
        private string path = string.Empty;
        private FileStream writer;
        private uint _lsIds;
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
            this._lsIds = 0;
            MetaDictionary<string, MetaList<MetaInstance>> instances = repository.Instances;
            string fullPath;
            foreach (KeyValuePair<string, MetaList<MetaInstance>> pair in instances)
            {
                fullPath = pair.Key.Replace("::", "/") + ".dmr";
                this.createFilePath(fullPath);
                this.writer = new FileStream(fullPath, FileMode.Create);
                this.dump(pair.Value);
                this.writer.Close();
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

        #region Dump Complex Type
        private void dump(MetaList<MetaInstance> instances)
        {
            this.dump(instances.Count);
            foreach (MetaInstance metaInstance in instances)
            {
                this.dump(metaInstance);
            }
        }

        private void dump(MetaInstance instance)
        {
            this._lsIds++;
            this.dump(this._lsIds);
            foreach (MetaInstanceVariable metaInstanceVariable in instance.InstanceVariables)
            {
                this.dump(metaInstanceVariable);
            }
        }

        private void dump(MetaInstanceVariable instance)
        {
            //if ()

            /*
            this._lsIds++;
            this.dump(this._lsIds);
            foreach (MetaInstanceVariable metaInstanceVariable in instance.InstanceVariables)
            {
                this.dump(metaInstanceVariable);
            }
             * */
        }

        /*
        private void dump<T>(MetaList<T> objects)
            where T : Base
        {
        }
         * 
        private void dump<T>(MetaList<T> values)
            where T : struct
        {
        }
         * */
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
        #endregion

        #region Dump Integral Type
        private void dump(string type, string value)
        {
            //switch 
        }

        private void dump(char c)
        {
            this.writer.WriteByte((byte)c);
        }

        private void dump(byte b)
        {
            this.writer.WriteByte(b);
        }

        private void dump(int i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            this.writer.Write(bytes, 0, bytes.Length);
        }

        private void dump(uint i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            this.writer.Write(bytes, 0, bytes.Length);
        }

        private void dump(long l)
        {
            this.dump((int)l);
        }

        private void dump(ulong l)
        {
            this.dump((uint)l);
        }

        private void dump(short s)
        {
            byte[] bytes = BitConverter.GetBytes(s);
            this.writer.Write(bytes, 0, bytes.Length);
        }

        private void dump(ushort s)
        {
            byte[] bytes = BitConverter.GetBytes(s);
            this.writer.Write(bytes, 0, bytes.Length);
        }

        private void dump(float f)
        {
            byte[] bytes = BitConverter.GetBytes(f);
            this.writer.Write(bytes, 0, bytes.Length);
        }

        private void dump(double d)
        {
            byte[] bytes = BitConverter.GetBytes(d);
            this.writer.Write(bytes, 0, bytes.Length);
        }

        private void dump(bool b)
        {
            this.writer.WriteByte((byte)(b ? 1 : 0));
        }

        private void dump(string s)
        {
            this.dump(s.Length);
            char[] chars = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                this.dump((byte)chars[i]);
            }
        }
        #endregion

    }
}
