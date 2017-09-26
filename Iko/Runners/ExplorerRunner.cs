using Nett;
using System.Diagnostics;

namespace Iko.Runners
{
    public class ExplorerRunner : IRunner
    {
        public void Run(TomlTable table)
        {
            var @params = new ExplorerParams(table);

            Process.Start(
                    "cmd",
                  $@"/c explorer {@params.Path}"
                );
        }

        class ExplorerParams
        {
            public string Path { get; set; }

            public ExplorerParams(TomlTable table)
            {
                table.TryGetValue<string>("path", out string path);
                Path = path;
            }
        }
    }
}
