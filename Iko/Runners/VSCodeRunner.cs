using Nett;
using System.Diagnostics;

namespace Iko.Runners
{
    public class VSCodeRunner : IRunner
    {
        public void Run(TomlTable table)
        {
            var @params = new VSCodeParams(table);

            Process.Start(
                    "cmd",
                  $@"/c code {@params.Path}"
                );
        }

        class VSCodeParams
        {
            public string Path { get; set; }

            public VSCodeParams(TomlTable table)
            {
                table.TryGetValue<string>("path", out string path);
                Path = path;
            }
        }
    }
}
