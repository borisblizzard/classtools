using ClassTools.Data;

namespace ClassTools
{
    public interface IPlugin
    {
        void Create();
        void Destroy();
        string Execute(Base data, string path);

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }
        string ToolId { get; }

    }

}
