using Iko.Runners;
using Nett;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Iko
{
    class Program
    {
        private static IDictionary _runnerMap = new Dictionary<string, IRunner>
        {
            { "chrome", new ChromeRunner() },
            { "vs15", new VS15Runner() },
            { "vs17", new VS17Runner() },
            { "explorer", new ExplorerRunner() },
            { "vim", new VimRunner() },
            { "vscode", new VSCodeRunner() }
        };

        static void Main(string[] args)
        {
            var cmd = args.ToList().FirstOrDefault();

            if (cmd == null)
                return;

            var configPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "iko.toml"
                );

            //TODO Check for file exist

            var config = Toml.ReadFile(configPath);

            if (!config.Keys.Contains(cmd))
                return; //TODO handle case where cmd doesn't exist

            //TODO how to handle this case insensitively?
            var table = config.Get<TomlTable>(cmd);

            Run(table);
        }

        private static void Run(TomlTable table)
        {
            var command = table.Get<string>("cmd");

            var runner = (IRunner)_runnerMap[command];

            runner.Run(table);
        }
    }
}
