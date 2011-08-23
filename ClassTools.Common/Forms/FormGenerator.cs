using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools;

namespace ClassTools.Common.Forms
{
    public partial class FormGenerator : Form
    {
        private PluginManager pluginManager;

        public IPlugin Generator
        {
            get
            {
                int index = this.lbGenerators.SelectedIndex;
                if (index >= 0 && index < this.pluginManager.AvailablePlugins.Count)
                {
                    return this.pluginManager.AvailablePlugins[index].Plugin;
                }
                return null;
            }
        }

        public FormGenerator()
        {
            InitializeComponent();
            this.pluginManager = new PluginManager();
            this.pluginManager.FindPlugins();
            List<AvailablePlugin> availablePlugins = this.pluginManager.AvailablePlugins;
            if (availablePlugins.Count > 0)
            {
                List<string> names = new List<string>();
                foreach (AvailablePlugin availablePlugin in availablePlugins)
                {
                    names.Add(string.Format("{0} ({1}) {2}", availablePlugin.Plugin.Name,
                        availablePlugin.Plugin.Author, availablePlugin.Plugin.Version));
                }
                this.lbGenerators.DataSource = names;
            }
        }

        private void bOk_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
