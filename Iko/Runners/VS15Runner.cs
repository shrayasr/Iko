using Iko.Utils;
using Nett;
using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace Iko.Runners
{
    public class VS15Runner : IRunner
    {
        public void Run(TomlTable table)
        {
            var @params = new VS15Params(table);

            var path = GetVisualStudioInstallationFolder();

            Process.Start("cmd", $@"/c start " + path + " " + @params.Path);
        }

        //Requires admin privileges 
        private string GetVisualStudioInstallationFolder()
        {
            try
            {
                var vs15exe = string.Empty;
                var vs15BaseDirectory = Registry.LocalMachine.OpenSubKey("Computer\\HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Microsoft\\VisualStudio\\14.0", true);

                if (vs15BaseDirectory?.GetValue("InstallDir") != null)
                {
                    vs15exe = vs15BaseDirectory.GetValue("InstallDir") + "devenv.exe";
                    return vs15exe;
                }
                else
                {
                    vs15BaseDirectory = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\VisualStudio\\14.0");
                    if (vs15BaseDirectory?.GetValue("InstallDir") != null)
                    {
                        vs15exe = vs15BaseDirectory.GetValue("InstallDir") + "devenv.exe";
                        return vs15exe;
                    }
                    else
                        throw new ArgumentException("Could not find the path for VS15. Registry value does not exist");
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
        }


        class VS15Params
        {
            public string Path { get; set; }

            public VS15Params(TomlTable table)
            {
                table.TryGetValue("path", out string path);

                if (path.IsEmpty())
                    throw new ArgumentException("'Path' cannot be empty for a VS15 task");

                Path = path;
            }
        }
    }
}
