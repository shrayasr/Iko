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
            { "browser", new DefaultBrowserRunner() },
            { "vs15", new VS15Runner() },
            { "vs17", new VS17Runner() },
            { "folder", new FolderRunner() },
            { "vim", new VimRunner() },
            { "vscode", new VSCodeRunner() }
        };

        static int Main(string[] args)
        {
            var cmd = args.ToList().FirstOrDefault();

            if (cmd == null)
            {
                Console.Error.WriteLine("No command specified");
                return (int)ReturnCodes.NoCommandSpecified;
            }

            var configPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "iko.toml"
                );

            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Config file not found at '{configPath}'");
                return (int)ReturnCodes.ConfigFileNotFound;
            }

            var config = Toml.ReadFile(configPath);

            var insensitiveKeyMap = config
                                        .Keys
                                        .Aggregate(
                                            new Dictionary<string, string>(),
                                            (acc, val) =>
                                            {
                                                acc.Add(val.ToUpper(), val);
                                                return acc;
                                            }
                                        );

            if (!insensitiveKeyMap.ContainsKey(cmd.ToUpper()))
            {
                Console.Error.WriteLine($"Command '{cmd}' isn't defined in the config file");
                return (int)ReturnCodes.CommandNotFound;
            }
            else
            {
                cmd = insensitiveKeyMap[cmd.ToUpper()];
            }

            var table = config.Get<TomlTable>(cmd);

            try
            {
                Run(table);
                return (int)ReturnCodes.Success;
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return (int)ReturnCodes.ImproperTaskDefinition;
            }
            catch (NotSupportedException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return (int)ReturnCodes.NoRunnerAvailable;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return (int)ReturnCodes.UnhandledException;
            }
        }

        private static void Run(TomlTable table)
        {
            var command = table.Get<string>("cmd");
            var runner = (IRunner)_runnerMap[command];

            if (runner == null)
            {
                throw new NotSupportedException($"Runner not implemented for '{command}'");
            }
            else
            {
                runner.Run(table);
            }
        }

        enum ReturnCodes
        {
            Success = 0,
            NoCommandSpecified,
            ConfigFileNotFound,
            CommandNotFound,
            ImproperTaskDefinition,
            UnhandledException,
            NoRunnerAvailable
        }
    }
}
