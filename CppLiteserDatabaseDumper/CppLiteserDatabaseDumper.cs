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
        private string version = "0.8";
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
                fullPath = path + "/" + pair.Key.Replace("::", "/") + ".dmr";
                this.createFilePath(fullPath);
                this.writer = new FileStream(fullPath, FileMode.Create);
                this.dump((byte)1);
                this.dump((byte)0);
                this.dump(pair.Value);
                this.writer.Close();
            }
            return "Code Generation was successful.";
        }
        #endregion

        #region Dump Complex Type
        private void dump(MetaList<MetaInstance> metaInstances)
        {
            this.dump(metaInstances.Count);
            foreach (MetaInstance metaInstance in metaInstances)
            {
                this.dump(metaInstance);
            }
        }

        private void dump(MetaInstance metaInstance)
        {
            if (metaInstance != null)
            {
                this._lsIds++;
                this.dump(this._lsIds);
                foreach (MetaInstanceVariable metaInstanceVariable in metaInstance.InstanceVariables)
                {
                    this.dump(metaInstanceVariable);
                }
            }
            else
            {
                this.dump(0);
            }
        }

        private void dump(MetaInstanceVariable metaInstanceVariable)
        {
            switch (metaInstanceVariable.ValueType)
            {
                case EValueType.Integral:
                    switch (metaInstanceVariable.Type)
                    {
                        case Constants.TYPE_INT:
                            this.dump(metaInstanceVariable.ValueInt);
                            break;
                        case Constants.TYPE_UINT:
                            this.dump(metaInstanceVariable.ValueUInt);
                            break;
                        case Constants.TYPE_LONG:
                            this.dump(metaInstanceVariable.ValueLong);
                            break;
                        case Constants.TYPE_ULONG:
                            this.dump(metaInstanceVariable.ValueULong);
                            break;
                        case Constants.TYPE_SHORT:
                            this.dump(metaInstanceVariable.ValueShort);
                            break;
                        case Constants.TYPE_USHORT:
                            this.dump(metaInstanceVariable.ValueUShort);
                            break;
                        case Constants.TYPE_UCHAR:
                            this.dump(metaInstanceVariable.ValueByte);
                            break;
                        case Constants.TYPE_FLOAT:
                            this.dump(metaInstanceVariable.ValueFloat);
                            break;
                        case Constants.TYPE_DOUBLE:
                            this.dump(metaInstanceVariable.ValueDouble);
                            break;
                        case Constants.TYPE_BOOL:
                            this.dump(metaInstanceVariable.ValueBool);
                            break;
                        case Constants.TYPE_CHAR:
                            this.dump(metaInstanceVariable.ValueSByte);
                            break;
                        default:
                            this.dump(metaInstanceVariable.ValueString);
                            break;
                    }
                    break;
                case EValueType.Object:
                    this.dump(metaInstanceVariable.ValueInstance);
                    break;
                case EValueType.List:
                    this.dump(metaInstanceVariable.ValueInstanceList);
                    break;
                case EValueType.Dictionary:
                    this.dump(metaInstanceVariable.ValueInstanceDictionary);
                    break;
            }
        }

        private void dump(MetaList<MetaInstanceVariable> metaInstanceVariables)
        {
            this.dump(metaInstanceVariables.Count);
            foreach (MetaInstanceVariable metaInstanceVariable in metaInstanceVariables)
            {
                this.dump(metaInstanceVariable);
            }
        }

        private void dump(MetaDictionary<MetaInstanceVariable, MetaInstanceVariable> metaInstanceVariables)
        {
            MetaList<MetaInstanceVariable> keys = metaInstanceVariables.GetKeys();
            MetaList<MetaInstanceVariable> values = metaInstanceVariables.GetValues(keys);
            this.dump(keys);
            this.dump(values);
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
        #endregion

        #region Dump Integral Type
        private void dump(byte b)
        {
            this.writer.WriteByte(b);
        }

        private void dump(sbyte b)
        {
            this.writer.WriteByte((byte)b);
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
