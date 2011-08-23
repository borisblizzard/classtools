using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace ClassTools.Common
{
    public class PluginManager
    {
        #region Fields

        protected List<AvailablePlugin> availablePlugins;

        #endregion

        #region Properties

        public List<AvailablePlugin> AvailablePlugins
        {
            get { return availablePlugins; }
        }

        public List<string> Paths
        {
            get
            {
                List<string> result = new List<string>();
                foreach (AvailablePlugin plugin in this.availablePlugins)
                {
                    result.Add(plugin.Path);
                }
                return result;
            }
        }

        #endregion

        #region Construct

        public PluginManager()
        {
            this.availablePlugins = new List<AvailablePlugin>();
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
            availablePlugins.Clear();
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
                    typeInterface = pluginType.GetInterface("ClassTools.IPlugin", true);
                    if (typeInterface != null)
                    {
                        AvailablePlugin availablePlugin = new AvailablePlugin();
                        availablePlugin.Plugin = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
                        availablePlugin.Path = filename;
                        this.availablePlugins.Add(availablePlugin);
                    }
                }
            }
        }

        public void DestroyPlugins()
        {
            foreach (AvailablePlugin availablePlugin in this.availablePlugins)
            {
                availablePlugin.Plugin.Destroy();
                availablePlugin.Plugin = null;
            }
            availablePlugins.Clear();
        }

        #endregion

    }
}


