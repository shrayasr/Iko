using System.Text;
using Nett;
using System.Diagnostics;
using System;
using Iko.Utils;

namespace Iko.Runners
{
    public class ChromeRunner : IRunner
    {
        public void Run(TomlTable table)
        {
            var @params = new ChromeParams(table);

            var command = new StringBuilder("chrome");

            if (@params.NewTab)
                command.Append(" --newtab");

            command.Append($" {@params.URL}");

            Process.Start("cmd", $"/c start {command.ToString()}");
        }

        class ChromeParams
        {
            public bool NewTab { get; set; }
            public string URL { get; set; }

            public ChromeParams(TomlTable table)
            {
                table.TryGetValue("newtab", out bool newTab);
                table.TryGetValue("url", out string url);

                if (url.IsEmpty())
                    throw new ArgumentException("'URL' cannot be empty for a chrome task");

                NewTab = NewTab;
                URL = url;
            }
        }
    }
}
