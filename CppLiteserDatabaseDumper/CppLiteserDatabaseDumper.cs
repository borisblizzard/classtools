using System;
using System.IO;
using System.Collections.Generic;

using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;
using ClassTools.Plugin;

namespace ClassTools
{
    public class CppLiteserDatabaseDumper : IPlugin
    {
        #region Fields
        private string name = "C++ Lite Serializer Database Dumper";
        private string description = "Dumps database into liteser serialization format.";
        private string author = "Boris Mikić";
        private string version = "0.8";
        private string toolId = "DataMaker";
        private string path = string.Empty;
        private FileStream writer;
        private uint _lsIds;
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
            Repository repository = (Repository)data;
            this.path = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            this._lsIds = 0;
            MetaDictionary<MetaClass, MetaList<MetaValue>> values = repository.Values;
            string fullPath;
            MetaList<MetaValue> metaValues;
            foreach (KeyValuePair<MetaClass, MetaList<MetaValue>> pair in values)
            {
                metaValues = pair.Value;
                if (metaValues.Count > 0)
                {
                    fullPath = path + "/" + pair.Key.GetNameWithModule("/") + ".lsb";
                    this.createFilePath(fullPath);
                    this.writer = new FileStream(fullPath, FileMode.Create);
                    this.dump((byte)1);
                    this.dump((byte)0);
                    this.dump(metaValues);
                    this.writer.Close();
                }
            }
            return "Code generation was successful.";
        }
        #endregion

        #region Dump Complex Type
        private void dump(MetaList<MetaValue> metaValues)
        {
            this.dump(metaValues.Count);
            foreach (MetaValue metaValue in metaValues)
            {
                this.dump(metaValue);
            }
        }

        private void dump(MetaDictionary<MetaValue, MetaValue> metaValues)
        {
            MetaList<MetaValue> keys = metaValues.GetKeys();
            MetaList<MetaValue> values = metaValues.GetValues(keys);
            this.dump(keys);
            this.dump(values);
        }

        private void dump(MetaInstance metaInstance)
        {
            if (metaInstance != null)
            {
                this._lsIds++;
                this.dump(this._lsIds);
                foreach (MetaInstanceVariable metaInstanceVariable in metaInstance.InstanceVariables)
                {
                    this.dump(metaInstanceVariable.Value);
                }
            }
            else
            {
                this.dump(0);
            }
        }

        private void dump(MetaValue metaValue)
        {
            switch (metaValue.ValueType)
            {
                case EValueType.Integral:
                    switch (metaValue.Type.Name)
                    {
                        case Constants.TYPE_INT:
                            this.dump(metaValue.AsInt);
                            break;
                        case Constants.TYPE_UINT:
                            this.dump(metaValue.AsUInt);
                            break;
                        case Constants.TYPE_LONG:
                            this.dump(metaValue.AsLong);
                            break;
                        case Constants.TYPE_ULONG:
                            this.dump(metaValue.AsULong);
                            break;
                        case Constants.TYPE_SHORT:
                            this.dump(metaValue.AsShort);
                            break;
                        case Constants.TYPE_USHORT:
                            this.dump(metaValue.AsUShort);
                            break;
                        case Constants.TYPE_UCHAR:
                            this.dump(metaValue.AsByte);
                            break;
                        case Constants.TYPE_FLOAT:
                            this.dump(metaValue.AsFloat);
                            break;
                        case Constants.TYPE_DOUBLE:
                            this.dump(metaValue.AsDouble);
                            break;
                        case Constants.TYPE_BOOL:
                            this.dump(metaValue.Bool);
                            break;
                        case Constants.TYPE_CHAR:
                            this.dump(metaValue.AsSByte);
                            break;
                        default:
                            this.dump(metaValue.String);
                            break;
                    }
                    break;
                case EValueType.Object:
                    this.dump(metaValue.Instance);
                    break;
                case EValueType.List:
                    this.dump(metaValue.List);
                    break;
                case EValueType.Dictionary:
                    this.dump(metaValue.Dictionary);
                    break;
            }
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
