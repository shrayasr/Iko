using Nett;
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
                table.TryGetValue<string>("path", out string path);
                table.TryGetValue<bool>("as-admin", out bool asAdmin);

                Path = path;
                AsAdmin = asAdmin;
            }
        }
    }
}
