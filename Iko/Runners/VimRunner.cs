using Iko.Utils;
using Nett;
using System;
using System.Diagnostics;

namespace Iko.Runners
{
    public class VimRunner : IRunner
    {
        public void Run(TomlTable table)
        {
            var @params = new VimParams(table);

            var pStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                Verb = @params.AsAdmin ? "runas" : "",
                FileName = "cmd",
                Arguments = $"/c gvim {@params.Path}"
            };

            Process.Start(pStartInfo);
        }

        class VimParams
        {
            public string Path { get; }
            public bool AsAdmin { get; }

            public VimParams(TomlTable table)
            {
                table.TryGetValue("path", out string path);
                table.TryGetValue("as-admin", out bool asAdmin);

                if (path.IsEmpty())
                    throw new ArgumentException("'path' cannot be empty for a VIM task");

                Path = path;
                AsAdmin = asAdmin;
            }
        }
    }
}
