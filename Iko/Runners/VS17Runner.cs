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

            var path = GetVisualStudioInstallationFolder();

            Process.Start("cmd", $@"/c start "+path+" "+@params.Path);
        }

        //Requires admin privileges 
        private string GetVisualStudioInstallationFolder()
        {
            try
            {
                var vs17exe=string.Empty;
                var vs17BaseDirectory = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\VisualStudio\\SxS\\VS7", true);

                if (vs17BaseDirectory.GetValue("15.0") != null)
                {
                    vs17exe = vs17BaseDirectory.GetValue("15.0") + @"Common7\IDE\devenv.exe";
                    return vs17exe;
                }
                else
                {
                    throw new ArgumentException("Could not find the path for VS17. Registry value does not exist");
                }
                    
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
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
