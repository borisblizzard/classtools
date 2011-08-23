using ClassTools.Model;

namespace ClassTools
{
    public interface IPlugin
    {
        void Create();
        void Destroy();
        string Execute(ClassModel model, ModelDatabase database, string path);

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

    }

}
