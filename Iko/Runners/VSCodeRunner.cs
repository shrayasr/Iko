using Iko.Utils;
using Nett;
using System;
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
                table.TryGetValue("path", out string path);

                if (path.IsEmpty())
                    throw new ArgumentException("'Path' cannot be empty for a VS15 task");

                Path = path;
            }
        }
    }
}
