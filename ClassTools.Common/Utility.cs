using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

using ClassTools.Data;

namespace ClassTools.Common
{
    public static class Utility
    {
        #region Data Update
        public static void ApplyNewDataSource(ListControl container, object dataSource, int count)
        {
            int index = container.SelectedIndex;
            container.DataSource = null;
            container.DataSource = dataSource;
            if (count == 0)
            {
                index = -1;
            }
            else if (index < 0)
            {
                index = 0;
            }
            else if (index >= count)
            {
                index = count - 1;
            }
            container.SelectedIndex = index;
        }

        public static bool TryMoveUp<T>(ref List<T> objects, int index)
        {
            if (index > 0)
            {
                T obj = objects[index];
                objects[index] = objects[index - 1];
                objects[index - 1] = obj;
                return true;
            }
            return false;
        }

        public static bool TryMoveDown<T>(ref List<T> objects, int index)
        {
            if (index < objects.Count - 1)
            {
                T obj = objects[index];
                objects[index] = objects[index + 1];
                objects[index + 1] = obj;
                return true;
            }
            return false;
        }
        #endregion

        #region Assembly
        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyTitleAttribute)attributes[0]).Title;
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyFileVersionAttribute)attributes[0]).Version;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        #endregion

    }
}
