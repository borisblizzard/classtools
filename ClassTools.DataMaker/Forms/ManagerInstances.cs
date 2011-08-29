﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.DataMaker.Forms
{
    public partial class ManagerInstances : Form, IRefreshable
    {
        #region Fields
        private Repository repository;
        private MetaClass metaClass;
        private MetaList<MetaValue> metaValues;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaList<MetaValue> MetaValuess
        {
            get { return this.metaValues; }
        }
        #endregion

        #region Construct
        public ManagerInstances(Repository repository, MetaClass metaClass, MetaList<MetaValue> metaValues)
        {
            InitializeComponent();
            this.repository = repository;
            this.metaClass = metaClass;
            this.metaValues = metaValues;
            this.ilInstances.SetData(this, this.repository, this.metaClass, this.metaValues);
            this.RefreshData();
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
            this.refreshing = false;
        }
        #endregion

        #region Tools
        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            this.ilInstances.CopyValue();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            this.ilInstances.PasteValue();
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            this.ilInstances.AddNewValue();
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            this.ilInstances.DeleteValue();
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            this.ilInstances.MoveUpValue();
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            this.ilInstances.MoveDownValue();
        }
        #endregion

        #region Events
        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }
        #endregion

    }
}
