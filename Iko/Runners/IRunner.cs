using Nett;

namespace Iko.Runners
{
    public interface IRunner
    {
        void Run(TomlTable table);
    }
}
