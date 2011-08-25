using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools;
using ClassTools.Common;
using ClassTools.Common.Forms;
using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.DataMaker.Forms
{
    public partial class Main : Form, IRefreshable
    {
        #region Constants
        const string savePromptNew = "There are unsaved changes. Do you want to save before creating a new file?";
        const string savePromptOpen = "There are unsaved changes. Do you want to save before opening another file?";
        const string savePromptExit = "There are unsaved changes. Do you want to save before exiting?";

        const string warningModelNotMatching = "The imported Class Model does not match the Database's Class Model. Do you want to continue anyway?";
        #endregion

        #region Fields
        private Model model;
        private Repository repository;
        private Repository lastRepository;
        string lastFilename;
        private bool refreshing;
        #endregion

        #region Construct
        public Main(string[] args)
        {
            InitializeComponent();
            this.lastFilename = string.Empty;
            this.model = null;
            this.newMenuItem.Enabled = false;
            this.saveMenuItem.Enabled = false;
            this.saveAsMenuItem.Enabled = false;
            this.bEdit.Enabled = false;
        }
        #endregion

        #region Save / Load
        private void openMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(savePromptOpen);
            if (result != DialogResult.Cancel)
            {
                this.ofdDatabase.FileName = string.Empty;
                result = this.ofdDatabase.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream stream = this.ofdDatabase.OpenFile();
                    Repository newRepository = Serializer.Deserialize(stream, this.repository);
                    this.lastFilename = this.ofdDatabase.FileName;
                    stream.Close();
                    if (this.model == null)
                    {
                        this.model = newRepository.Model;
                    }
                    else if (!newRepository.Model.Equals(this.model))
                    {
                        result = MessageBox.Show(warningModelNotMatching, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                    if (result == DialogResult.OK)
                    {
                        this.repository = newRepository;
                        this.lastRepository = Serializer.Clone(this.repository);
                        this.repository.UpdateModel(this.model);
                        this.lastRepository.UpdateModel(this.model);
                        this.RefreshData();
                    }
                }
            }
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            this.showSaveDialog();
            this.lastFilename = this.sfdDatabase.FileName;
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lastFilename != string.Empty)
            {
                using (FileStream stream = new FileStream(this.lastFilename, FileMode.Truncate))
                {
                    this.save(stream);
                }
            }
            else
            {
                this.showSaveDialog();
            }
        }

        private void newMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(savePromptNew);
            if (result != DialogResult.Cancel)
            {
                this.repository = new Repository(this.model);
                this.lastRepository = Serializer.Clone(this.repository);
                this.lastRepository.UpdateModel(this.model);
                this.lastFilename = string.Empty;
                this.RefreshData();
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(savePromptExit);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            About f = new About();
            f.ShowDialog();
        }

        private void importMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(savePromptOpen);
            if (result != DialogResult.Cancel)
            {
                this.ofdModel.FileName = string.Empty;
                result = this.ofdModel.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream stream = this.ofdModel.OpenFile();
                    this.model = Serializer.Deserialize(stream, this.model);
                    stream.Close();
                    if (this.repository != null)
                    {
                        if (!this.repository.Model.Equals(this.model))
                        {
                            result = MessageBox.Show(warningModelNotMatching, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        }
                        if (result == DialogResult.OK)
                        {
                            this.repository.UpdateModel(this.model);
                            this.lastRepository = Serializer.Clone(this.repository);
                            this.lastRepository.UpdateModel(this.model);
                            this.RefreshData();
                        }
                    }
                    else
                    {
                        this.repository = new Repository(this.model);
                        this.lastRepository = new Repository(this.model);
                        this.lastFilename = string.Empty;
                    }
                    this.RefreshData();
                }
            }
        }

        private DialogResult showSaveChangesDialog(string text)
        {
            if (this.model == null)
            {
                return DialogResult.No;
            }
            DialogResult result = DialogResult.OK;
            if (!this.repository.Equals(this.lastRepository))
            {
                result = MessageBox.Show(text, "Unsaved Changes", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    result = this.showSaveDialog();
                }
            }
            return result;
        }

        private DialogResult showSaveDialog()
        {
            if (this.lastFilename != string.Empty)
            {
                this.sfdDatabase.FileName = this.lastFilename;
            }
            DialogResult result = this.sfdDatabase.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (Stream stream = this.sfdDatabase.OpenFile())
                {
                    this.save(stream);
                }
            }
            return result;
        }

        private void save(Stream stream)
        {
            Serializer.Serialize(stream, this.repository);
            this.lastRepository = Serializer.Clone(this.repository);
        }
        #endregion

        #region Refresh
        public void RefreshData()
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            if (this.model != null)
            {
                this.newMenuItem.Enabled = true;
                this.saveMenuItem.Enabled = true;
                this.saveAsMenuItem.Enabled = true;
            }
            Utility.ApplyNewDataSource(this.lbClasses, new List<MetaClass>(this.model.Classes), this.model.Classes.Count);
            if (this.model.Classes.Count > 0)
            {
                if (this.lbClasses.SelectedIndex < 0)
                {
                    this.lbClasses.SelectedIndex = 0;
                }
            }
            if (this.repository != null)
            {
                this.icInstances.ClearData();
                MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
                if (metaClass != null)
                {
                    this.icInstances.SetData(this, this.repository, metaClass, this.repository.GetInstances(metaClass));
                    this.icInstances.RefreshData();
                }
            }
            this.refreshing = false;
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            /*
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            if (metaClass != null)
            {
                Form form = new InstanceCollection(this.repository, metaClass, this.repository.GetInstances(metaClass));
                form.ShowDialog();
            }
            */
        }
        #endregion

        #region Tools
        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.CopyInstance();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.PasteInstance();
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.AddNewInstance();
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.DeleteInstance();
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.MoveUpInstance();
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.MoveDownInstance();
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Generator f = new Generator();
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                f.Execute(this.model, this.repository);
            }
        }
        #endregion

        private void lbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }

    }
}
