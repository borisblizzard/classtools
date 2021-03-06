﻿using System;
using System.IO;
using System.Collections.Generic;

using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;
using ClassTools.Plugin;

namespace ClassTools
{
    public class CppLiteser2DatabaseDumper : IPlugin
    {
        #region Fields
        private string name = "C++ Lite Serializer 2 Database Dumper";
        private string description = "Dumps database into liteser serialization format 2.4.";
        private string author = "Boris Mikić";
        private string version = "1.4";
        private string toolId = "DataMaker";
        private string path = string.Empty;
        private FileStream writer;
        private uint _objectIds;
        private List<string> _strings = new List<string>();
        #endregion

        #region Properties
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public string Author { get { return author; } }
        public string Version { get { return version; } }
        public string ToolId { get { return toolId; } }
        #endregion

        #region Constants
        // taken from Type.h from Liteser 2.x
        //static byte NONE = 0x00; // never used
        static byte INT8 = 0x01;
        static byte UINT8 = 0x02;
        static byte INT16 = 0x03;
        static byte UINT16 = 0x04;
        static byte INT32 = 0x05;
        static byte UINT32 = 0x06;
		static byte INT64 = 0x07;
        static byte UINT64 = 0x08;
        static byte FLOAT = 0x21;
        static byte DOUBLE = 0x22;
        static byte BOOL = 0x41;
        static byte OBJECT = 0x61; // never used
        static byte OBJPTR = 0x62;
        static byte HSTR = 0x81;
        static byte HVERSION = 0x82;
		static byte HENUM = 0x83;
		static byte GRECT = 0x91;
        static byte GVEC2 = 0x92;
        static byte GVEC3 = 0x93;
        static byte HARRAY = 0xA1;
        static byte HMAP = 0xC1;

        static byte VERSION_MAJOR = 2;
        static byte VERSION_MINOR = 5;
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
            MetaDictionary<MetaClass, MetaList<MetaValue>> values = repository.Values;
            string fullPath;
            MetaList<MetaValue> metaValues;
            try
            {
                foreach (KeyValuePair<MetaClass, MetaList<MetaValue>> pair in values)
                {
                    this._objectIds = 0;
                    this._strings.Clear();
                    metaValues = pair.Value;
                    if (metaValues.Count > 0)
                    {
                        fullPath = path + "/" + pair.Key.GetNameWithModule("-") + ".ls2";
                        this.createFilePath(fullPath);
                        this.writer = new FileStream(fullPath, FileMode.Create);
                        this.dump((byte)'L');
                        this.dump((byte)'S');
                        this.dump(VERSION_MAJOR);
                        this.dump(VERSION_MINOR);
                        this.dump(metaValues.Count);
                        foreach (MetaValue metaValue in metaValues)
                        {
                            this.dump(metaValue);
                        }
                        this.writer.Close();
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "Code generation was successful.";
        }
        #endregion

        #region Dump Complex Type
        private void dump(MetaList<MetaValue> metaValues)
        {
            this.dump(metaValues.Count);
            if (metaValues.Count > 0)
            {
                this.dump((uint)1);
                this.dump(this._getLS2Type(metaValues[0].Type, true));
                foreach (MetaValue metaValue in metaValues)
                {
                    this.dump(metaValue);
                }
            }
        }

        private void dump(MetaDictionary<MetaValue, MetaValue> metaValues)
        {
            this.dump(metaValues.Count);
            if (metaValues.Count > 0)
            {
                this.dump((uint)2);
                MetaList<MetaValue> keys = metaValues.GetKeys();
                MetaList<MetaValue> values = metaValues.GetValues(keys);
                this.dump(this._getLS2Type(keys[0].Type, true));
                this.dump(this._getLS2Type(values[0].Type, true));
                this.dump(keys);
                this.dump(values);
            }
        }

        private void dump(MetaInstance metaInstance)
        {
            if (metaInstance != null)
            {
                this._objectIds++;
                this.dump(this._objectIds);
                this.dump(metaInstance.Type.GetNameWithModule());
                this.dump(metaInstance.InstanceVariables.Count);
                foreach (MetaInstanceVariable metaInstanceVariable in metaInstance.InstanceVariables)
                {
                    this.dump(metaInstanceVariable.Variable.Name);
                    this.dump(this._getLS2Type(metaInstanceVariable.Variable));
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
            switch (metaValue.Type.CategoryType)
            {
                case ECategoryType.Integral:
                    switch (metaValue.Type.Name)
                    {
                        case Constants.TYPE_INT:
                            this.dump(metaValue.AsInt);
                            break;
                        case Constants.TYPE_UINT:
                            this.dump(metaValue.AsUInt);
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
                        case Constants.TYPE_INT64:
                            this.dump(metaValue.AsInt64);
                            break;
                        case Constants.TYPE_UINT64:
                            this.dump(metaValue.AsUInt64);
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
                        case Constants.TYPE_VERSION:
                            this.dump(metaValue.AsUInt4);
                            break;
                        case Constants.TYPE_GVEC2:
                            this.dump(metaValue.AsFloat2);
                            break;
                        case Constants.TYPE_GVEC3:
                            this.dump(metaValue.AsFloat3);
                            break;
                        case Constants.TYPE_GRECT:
                            this.dump(metaValue.AsFloat4);
                            break;
                        default:
                            this.dump(metaValue.String);
                            break;
                    }
                    break;
                case ECategoryType.Class:
                    this.dump(metaValue.Instance);
                    break;
                case ECategoryType.List:
                    this.dump(metaValue.List);
                    break;
				case ECategoryType.Dictionary:
					this.dump(metaValue.Dictionary);
					break;
				case ECategoryType.Enum:
					this.dump(metaValue.AsInt);
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

        private bool _tryMapString(out uint id, string str)
        {
            if (str == "")
            {
                id = 0;
                return false;
            }
            if (!this._strings.Contains(str))
            {
                this._strings.Add(str);
                id = (uint)this._strings.Count;
                return true;
            }
            id = (uint)this._strings.IndexOf(str) + 1;
            return false;
        }

        protected byte _getLS2Type(MetaVariable variable)
        {
            byte result = this._getLS2Type(variable.Type);
            if (result == OBJPTR && !variable.Nullable)
            {
                result = OBJECT;
            }
            return result;
        }

        protected byte _getLS2Type(MetaType type, bool collectionType = false)
        {
            switch (type.CategoryType)
            {
                case ECategoryType.Integral:
                    switch (type.Name)
                    {
                        case Constants.TYPE_INT:
                            return INT32;
                        case Constants.TYPE_UINT:
                            return UINT32;
                        case Constants.TYPE_SHORT:
                            return INT16;
                        case Constants.TYPE_USHORT:
                            return UINT16;
                        case Constants.TYPE_CHAR:
                            return INT8;
                        case Constants.TYPE_UCHAR:
                            return UINT8;
                        case Constants.TYPE_INT64:
                            return INT64;
                        case Constants.TYPE_UINT64:
                            return UINT64;
                        case Constants.TYPE_FLOAT:
                            return FLOAT;
                        case Constants.TYPE_DOUBLE:
                            return DOUBLE;
                        case Constants.TYPE_BOOL:
                            if (!collectionType)
                            {
                                return BOOL;
                            }
                            break;
                        case Constants.TYPE_VERSION:
                            return HVERSION;
						case Constants.TYPE_GVEC2:
                            return GVEC2;
                        case Constants.TYPE_GVEC3:
                            return GVEC3;
                        case Constants.TYPE_GRECT:
                            return GRECT;
                        default:
                            return HSTR;
                    }
                    break;
                case ECategoryType.Class:
                    return OBJPTR;
                case ECategoryType.List:
                    return HARRAY;
                case ECategoryType.Dictionary:
                    return HMAP;
				case ECategoryType.Enum:
					return HENUM;
			}
			throw new Exception(String.Format("Warning! Type {0} is not supported in this context!", type.Name));
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

        private void dump(uint[] i)
        {
            for (int j = 0; j < i.Length; j++)
            {
                byte[] bytes = BitConverter.GetBytes(i[j]);
                this.writer.Write(bytes, 0, bytes.Length);
            }
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

        private void dump(Int64 i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            this.writer.Write(bytes, 0, bytes.Length);
        }

        private void dump(UInt64 i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            this.writer.Write(bytes, 0, bytes.Length);
        }

        private void dump(float f)
        {
            byte[] bytes = BitConverter.GetBytes(f);
            this.writer.Write(bytes, 0, bytes.Length);
        }

        private void dump(float[] f)
        {
            for (int i = 0; i < f.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(f[i]);
                this.writer.Write(bytes, 0, bytes.Length);
            }
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
            uint id = 0;
            bool added = this._tryMapString(out id, s);
            this.dump(id);
            if (added)
            {
                this.dump(s.Length);
                char[] chars = s.ToCharArray();
                for (int i = 0; i < s.Length; i++)
                {
                    this.dump((byte)chars[i]);
                }
            }
        }
        #endregion

    }
}
