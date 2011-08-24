using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools;
using ClassTools.Common;
using ClassTools.Common.Forms;
using ClassTools.Model;

namespace ClassTools.DataMaker.Forms
{
    public partial class Main : Form, IRefreshable
    {
        #region Constants
        const string savePromptNew = "There are unsaved changes. Do you want to save be save before creating a new model?";
        const string savePromptOpen = "There are unsaved changes. Do you want to save be save before opening another file?";
        const string savePromptExit = "There are unsaved changes. Do you want to save be save before existing?";

        const string warningModelNotMatching = "The imported Class Model does not match the Database's Class Model. Do you want to continue anyway?";
        #endregion

        #region Fields
        private ClassModel classModel;
        private ModelDatabase database;
        private ModelDatabase lastDatabase;
        string lastFilename;
        private bool refreshing;
        #endregion

        #region Constructors
        public Main(string[] args)
        {
            InitializeComponent();
            this.lastFilename = string.Empty;
            this.classModel = null;
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
                    ModelDatabase newDatabase = Serializer.Deserialize(stream, this.database);
                    this.lastFilename = this.ofdDatabase.FileName;
                    stream.Close();
                    if (this.classModel == null)
                    {
                        this.classModel = newDatabase.Model;
                    }
                    else if (!newDatabase.Model.Equals(this.classModel))
                    {
                        result = MessageBox.Show(warningModelNotMatching, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                    if (result == DialogResult.OK)
                    {
                        this.database = newDatabase;
                        this.lastDatabase = (ModelDatabase)Serializer.Clone(this.database);
                        this.database.UpdateModel(this.classModel);
                        this.lastDatabase.UpdateModel(this.classModel);
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
                this.database = new ModelDatabase(this.classModel);
                this.lastDatabase = (ModelDatabase)Serializer.Clone(this.database);
                this.lastDatabase.UpdateModel(this.classModel);
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
            FormAbout f = new FormAbout();
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
                    this.classModel = Serializer.Deserialize(stream, this.classModel);
                    stream.Close();
                    if (this.database != null)
                    {
                        if (!this.database.Model.Equals(this.classModel))
                        {
                            result = MessageBox.Show(warningModelNotMatching, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        }
                        if (result == DialogResult.OK)
                        {
                            this.database.UpdateModel(this.classModel);
                            this.lastDatabase = (ModelDatabase)Serializer.Clone(this.database);
                            this.lastDatabase.UpdateModel(this.classModel);
                            this.RefreshData();
                        }
                    }
                    else
                    {
                        this.database = new ModelDatabase(this.classModel);
                        this.lastDatabase = new ModelDatabase(this.classModel);
                        this.lastFilename = string.Empty;
                    }
                    this.RefreshData();
                }
            }
        }

        private DialogResult showSaveChangesDialog(string text)
        {
            if (this.classModel == null)
            {
                return DialogResult.No;
            }
            DialogResult result = DialogResult.OK;
            if (!this.database.Equals(this.lastDatabase))
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
            Serializer.Serialize(stream, this.database);
            this.lastDatabase = (ModelDatabase)Serializer.Clone(this.database);
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
            if (this.classModel != null)
            {
                this.newMenuItem.Enabled = true;
                this.saveMenuItem.Enabled = true;
                this.saveAsMenuItem.Enabled = true;
            }
            Utility.ApplyNewDataSource(this.lbClasses, new List<MetaClass>(this.classModel.Classes), this.classModel.Classes.Count);
            if (this.classModel.Classes.Count > 0)
            {
                if (this.lbClasses.SelectedIndex < 0)
                {
                    this.lbClasses.SelectedIndex = 0;
                }
                //this.icInstances.Enabled = true;
            }
            else
            {
                //this.icInstances.Enabled = false;
            }
            if (this.database != null)
            {
                this.icInstances.ClearData();
                MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
                if (metaClass != null)
                {
                    this.icInstances.SetData(this, this.database, metaClass, this.database.GetInstances(metaClass));
                    this.icInstances.RefreshData();
                    //this.icInstances.MetaInstances = ;
                }
            }
            this.refreshing = false;
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            /*
            MetaClass classe = (MetaClass)this.lbClasses.SelectedItem;
            if (classe != null)
            {
                Form form = new InstanceCollection(this.database, classe, this.database.GetInstances(classe));
                form.ShowDialog();
            }
            */
        }
        #endregion

        #region Tools
        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGenerator f = new FormGenerator();
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                IPlugin generator = f.Generator;
                if (generator != null)
                {
                    result = MessageBox.Show("Run generator in safe-mode?", generator.ToString() + " ready",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    //2DO - implement folder selection dialog
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            string message = generator.Execute(null, this.database, "gen");
                            MessageBox.Show(message, generator.ToString() + " is done",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, generator.ToString() + " encountered an exception",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        string message = generator.Execute(null, this.database, "gen");
                        MessageBox.Show(message, generator.ToString() + " is done",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        #endregion

        private void lbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }

    }
}
