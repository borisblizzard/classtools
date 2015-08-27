using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.DataMaker.Forms
{
	public partial class ManagerClasses : Form, IRefreshable
	{
		#region Fields
		private Repository repository;
		private MetaList<MetaClass> classes;
		private bool refreshing;
		#endregion

		#region Properties
		public MetaList<MetaClass> VisibleClasses
		{
			get
			{
				MetaList<MetaClass> result = new MetaList<MetaClass>();
				for (int i = 0; i < this.classes.Count; ++i)
				{
					if (this.clbClasses.GetItemChecked(i))
					{
						result.Add(this.classes[i]);
					}
				}
				return result;
			}
		}
		#endregion

		#region Construct
		public ManagerClasses(Repository repository)
		{
			InitializeComponent();
			this.repository = repository;
			this.refreshing = false;
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
			this.classes = new MetaList<MetaClass>(this.repository.Model.LeafClasses);
			Utility.ApplyNewDataSource(this.clbClasses, this.classes, this.classes.Count);
			for (int i = 0; i < this.classes.Count; ++i)
			{
				if (this.repository.VisibleClasses.Contains(this.classes[i]))
				{
					this.clbClasses.SetItemChecked(i, true);
				}
			}
			this.refreshing = false;
		}
		#endregion

	}
}
