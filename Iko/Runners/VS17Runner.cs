using Iko.Utils;
using Nett;
using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace Iko.Runners
{
    public class VS17Runner : IRunner
    {
        
        public void Run(TomlTable table)
        {
            var @params = new VS17Params(table);

            var path = string.Format("\"{0}\"",GetVisualStudioInstallationFolder());
         
            Process.Start("cmd", $@"/c start "+path+" "+@params.Path);
        }

        //Requires admin privileges 
        private string GetVisualStudioInstallationFolder()
        {
            var vs17BaseRegistry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\VisualStudio\\SxS\\VS7", true);
            var vs17exePath = vs17BaseRegistry?.GetValue("15.0");
            if (vs17exePath != null)
            {
                    
                return vs17exePath + @"Common7\IDE\devenv.exe";
            }
            else
            {
                throw new ArgumentException("Could not find the path for VS17. Registry value does not exist");
            }        
        }

        class VS17Params
        {
            public string Path { get; set; }

            public VS17Params(TomlTable table)
            {
                table.TryGetValue("path", out string path);

                if (path.IsEmpty())
                    throw new ArgumentException("'Path' cannot be empty for a VS17 task");

                Path = path;
            }
        }
    }
}
