using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace ClassTools.Plugin
{
    public class PluginManager
    {
        #region Fields
        protected List<Entry> entries;
        protected string toolId;
        #endregion

        #region Properties
        public List<Entry> Entries
        {
            get { return entries; }
        }

        public List<string> Paths
        {
            get
            {
                List<string> result = new List<string>();
                foreach (Entry entry in this.entries)
                {
                    result.Add(entry.Path);
                }
                return result;
            }
        }
        #endregion

        #region Construct
        public PluginManager(string toolId)
        {
            this.entries = new List<Entry>();
            this.toolId = toolId;
        }

        ~PluginManager()
        {
            this.DestroyPlugins();
        }
        #endregion

        #region Methods
        public void FindPlugins()
        {
            this.FindPlugins("Plugins");
        }

        public void FindPlugins(string path)
        {
            path = AppDomain.CurrentDomain.BaseDirectory + "\\" + path;
            entries.Clear();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string file in Directory.GetFiles(path))
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension.Equals(".dll"))
                {
                    this.AddPlugin(file);
                }
            }
        }

        private void AddPlugin(string filename)
        {
            Type typeInterface = null;
            Assembly pluginAssembly = Assembly.LoadFrom(filename);
            foreach (Type pluginType in pluginAssembly.GetTypes())
            {
                if (pluginType.IsPublic && !pluginType.IsAbstract)
                {
                    typeInterface = pluginType.GetInterface("ClassTools.Plugin.IPlugin", true);
                    if (typeInterface != null)
                    {
                        Entry entry = new Entry();
                        entry.Plugin = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
                        entry.Path = filename;
                        if (entry.Plugin.ToolId == this.toolId)
                        {
                            entry.Plugin.Create();
                            this.entries.Add(entry);
                        }
                    }
                }
            }
        }

        public void DestroyPlugins()
        {
            foreach (Entry entry in this.entries)
            {
                entry.Plugin.Destroy();
                entry.Plugin = null;
            }
            entries.Clear();
        }
        #endregion

    }
}


