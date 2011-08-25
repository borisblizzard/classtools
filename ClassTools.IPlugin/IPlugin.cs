using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools
{
    public interface IPlugin
    {
        void Create();
        void Destroy();
        string Execute(Model model, Repository repository, string path);

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

    }

}
