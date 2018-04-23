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

            var path = string.Format("\"{0}\"", GetVisualStudioInstallationFolder());
           
            Process.Start("cmd", $@"/c start " + path + " " + @params.Path);
        }

        //Requires admin privileges 
        private string GetVisualStudioInstallationFolder()
        {
            var vs15BaseRegistry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\VisualStudio\\14.0", true);

            var vs15exePath = vs15BaseRegistry?.GetValue("InstallDir");

            if (vs15exePath!= null)
            {
                return vs15exePath +"devenv.exe";
            }
            else
            {
                vs15BaseRegistry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\VisualStudio\\14.0");
                vs15exePath = vs15BaseRegistry?.GetValue("InstallDir");

                if (vs15exePath != null)
                {
                    return vs15exePath + "devenv.exe";
                }
                else
                    throw new ArgumentException("Could not find the path for VS15. Registry value does not exist");
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
