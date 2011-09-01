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
        const string SAVE_PROMPT_NEW = "There are unsaved changes. Do you want to save before creating a new file?";
        const string SAVE_PROMPT_OPEN = "There are unsaved changes. Do you want to save before opening another file?";
        const string SAVE_PROMPT_EXIT = "There are unsaved changes. Do you want to save before exiting?";

        const string WARNING_MODEL_NOT_MATCHING = "The imported Class Model does not match the Database's Class Model. Do you want to continue anyway?";
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
        }
        #endregion

        #region Save / Load
        private void openMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(SAVE_PROMPT_OPEN);
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
                        result = MessageBox.Show(WARNING_MODEL_NOT_MATCHING, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            result = DialogResult.OK;
                        }
                    }
                    if (result == DialogResult.OK)
                    {
                        this.repository = newRepository;
                        this.lastRepository = Serializer.Clone(this.repository);
                        this.repository.Update(this.model);
                        this.lastRepository.Update(this.model);
                        InternalClipboard.Clear();
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
            DialogResult result = this.showSaveChangesDialog(SAVE_PROMPT_NEW);
            if (result != DialogResult.Cancel)
            {
                this.repository = new Repository(this.model);
                this.lastRepository = new Repository(this.model);
                InternalClipboard.Clear();
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
            DialogResult result = this.showSaveChangesDialog(SAVE_PROMPT_EXIT);
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

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Generator f = new Generator("DataMaker");
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                f.Execute(this.repository);
            }
        }

        private void importMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(SAVE_PROMPT_OPEN);
            if (result != DialogResult.Cancel)
            {
                this.ofdModel.FileName = string.Empty;
                result = this.ofdModel.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream stream = this.ofdModel.OpenFile();
                    Model newModel = Serializer.Deserialize(stream, this.model);
                    stream.Close();
                    if (this.repository != null)
                    {
                        if (!this.repository.Model.Equals(newModel))
                        {
                            result = MessageBox.Show(WARNING_MODEL_NOT_MATCHING, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                result = DialogResult.OK;
                            }
                        }
                        if (result == DialogResult.OK)
                        {
                            this.model = newModel;
                            this.repository.Update(this.model);
                            this.lastRepository = Serializer.Clone(this.repository);
                            this.lastRepository.Update(this.model);
                            InternalClipboard.Clear();
                            this.RefreshData();
                        }
                    }
                    else
                    {
                        this.model = newModel;
                        this.repository = new Repository(this.model);
                        this.lastRepository = new Repository(this.model);
                        InternalClipboard.Clear();
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
                result = MessageBox.Show(text, "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
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
            this.repository.Update(this.model);
            this.lastRepository.Update(this.model);
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
            MetaList<MetaClass> classes = this.model.LeafClasses;
            Utility.ApplyNewDataSource(this.lbClasses, classes, classes.Count);
            if (classes.Count > 0)
            {
                if (this.lbClasses.SelectedIndex < 0)
                {
                    this.lbClasses.SelectedIndex = 0;
                }
            }
            if (this.repository != null)
            {
                this.vlValues.ClearData();
                MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
                if (metaClass != null)
                {
                    this.vlValues.SetData(this, this.repository, metaClass, this.repository.Values[metaClass]);
                    this.vlValues.RefreshData();
                }
            }
            this.refreshing = false;
        }
        #endregion

        #region Tools
        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.CopyValue();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.PasteValue();
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.AddNewValue();
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.DeleteValue();
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.MoveUpValue();
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.MoveDownValue();
        }
        #endregion

        #region Events
        private void lbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }
        #endregion

    }
}
