﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools;
using ClassTools.Data;
using ClassTools.Plugin;

namespace ClassTools.Common.Forms
{
    public partial class Generator : Form
    {
        private PluginManager pluginManager;

        public IPlugin Plugin
        {
            get
            {
                int index = this.lbPlugins.SelectedIndex;
                if (index >= 0 && index < this.pluginManager.Entries.Count)
                {
                    return this.pluginManager.Entries[index].Plugin;
                }
                return null;
            }
        }

        public Generator(string toolId)
        {
            InitializeComponent();
            this.pluginManager = new PluginManager(toolId);
            this.pluginManager.FindPlugins();
            List<Entry> availablePlugins = this.pluginManager.Entries;
            if (availablePlugins.Count > 0)
            {
                List<string> names = new List<string>();
                foreach (Entry availablePlugin in availablePlugins)
                {
                    names.Add(string.Format("{0} ({1}) {2}", availablePlugin.Plugin.Name, availablePlugin.Plugin.Author, availablePlugin.Plugin.Version));
                }
                this.lbPlugins.DataSource = names;
            }
        }

        private void bOk_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void Execute(Base data)
        {
            IPlugin plugin = this.Plugin;
            if (plugin == null)
            {
                return;
            }
            DialogResult result = DialogResult.OK; // TODO - implement folder selection dialog
            if (result == DialogResult.Cancel)
            {
                return;
            }
            string path = "..\\generated";
            result = MessageBox.Show("Run generator in safe-mode?", plugin.ToString() + " ready", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (result == DialogResult.Yes)
            {
                try
                {
                    string message = plugin.Execute(data, path);
                    MessageBox.Show(message, plugin.Name + " is done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, plugin.Name + " encountered an exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (result == DialogResult.No)
            {
                string message = plugin.Execute(data, path);
                MessageBox.Show(message, plugin.Name + " is done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
