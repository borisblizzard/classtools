using System;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassTools.Model
{
    public static class Serializer
    {
        #region Serialize

        public static void Serialize(Stream stream, ClassModel model)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, model);
        }

        public static void Serialize(Stream stream, ModelDatabase database)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, database);
        }

        #endregion

        #region Deserialize

        public static ClassModel Deserialize(Stream stream, ClassModel model)
        {
            IFormatter formatter = new BinaryFormatter();
            model = (ClassModel)formatter.Deserialize(stream);
            return model;
        }

        public static ModelDatabase Deserialize(Stream stream, ModelDatabase database)
        {
            IFormatter formatter = new BinaryFormatter();
            database = (ModelDatabase)formatter.Deserialize(stream);
            return database;
        }

        #endregion

        #region Clone via serialization

        public static object Clone(object obj)
        {
            Stream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Position = 0;
            object result = formatter.Deserialize(stream);
            stream.Close();
            return result;
        }

        #endregion
    }
}
