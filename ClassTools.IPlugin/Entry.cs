using ClassTools;

namespace ClassTools.Plugin
{
    public class Entry
    {
        #region Fields
        protected IPlugin plugin;
        protected string path;
        #endregion

        #region Properties
        public IPlugin Plugin
        {
            get { return this.plugin; }
            set { this.plugin = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }
        #endregion

        #region Construct
        public Entry()
        {
        }
        #endregion

    }
}


