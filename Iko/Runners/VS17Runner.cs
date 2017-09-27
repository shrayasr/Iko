using Iko.Utils;
using Nett;
using System;
using System.Diagnostics;

namespace Iko.Runners
{
    public class VS17Runner : IRunner
    {
        public void Run(TomlTable table)
        {
            var @params = new VS17Params(table);

            Process.Start(
                    "cmd",
                  $@"/c start ""C:\Program Files(x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe"" {@params.Path}"
                );
        }

        class VS17Params
        {
            public string Path { get; set; }

            public VS17Params(TomlTable table)
            {
                table.TryGetValue("path", out string path);

                if (path.IsEmpty())
                    throw new ArgumentException("'Path' cannot be empty for a VS15 task");

                Path = path;
            }
        }
    }
}
