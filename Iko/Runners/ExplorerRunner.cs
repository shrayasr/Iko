using Iko.Utils;
using Nett;
using System;
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
                table.TryGetValue("path", out string path);

                if (path.IsEmpty())
                    throw new ArgumentException("'Path' cannot be empty for an explorer task");

                Path = path;
            }
        }
    }
}
