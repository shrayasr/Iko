using Nett;
using System.Diagnostics;

namespace Iko.Runners
{
    public class VS15Runner : IRunner
    {
        public void Run(TomlTable table)
        {
            var @params = new VS15Params(table);

            Process.Start(
                    "cmd",
                  $@"/c start ""C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe"" {@params.Path}"
                );
        }

        class VS15Params
        {
            public string Path { get; set; }

            public VS15Params(TomlTable table)
            {
                table.TryGetValue<string>("path", out string path);
                Path = path;
            }
        }
    }
}
