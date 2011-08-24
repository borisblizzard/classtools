using System;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassTools.Model
{
    public static class Serializer
    {
        #region Serialize
        public static void Serialize<T>(Stream stream, T model)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, model);
        }
        #endregion

        #region Deserialize
        public static T Deserialize<T>(Stream stream, T model)
        {
            IFormatter formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(stream);
        }
        #endregion

        #region Clone via serialization
        public static T Clone<T>(T obj)
        {
            Stream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Position = 0;
            object result = formatter.Deserialize(stream);
            stream.Close();
            return (T)result;
        }
        #endregion

    }
}
