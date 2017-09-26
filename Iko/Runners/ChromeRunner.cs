using System.Text;
using Nett;
using System.Diagnostics;

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
                //TODO handle required params being present

                table.TryGetValue("newtab", out bool newTab);
                NewTab = NewTab;

                table.TryGetValue("url", out string url);
                URL = url;
            }
        }
    }
}
