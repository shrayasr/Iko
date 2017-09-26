# Iko
> Japanese for "go"

Iko is a commandline based task runner for windows. This is written purely for
my purpose but if there is enough interest, I'd be happy to make it more
generic

## Motivation

Some everyday tasks, I find rather mundane:

- Open certain folders
- Open certain SLN files with certain Visual Studio versions
- Open Hosts file

And I didn't want to do any of this manually. So I wrote Iko

## Solution

Iko uses [Toml]() to define the commands that can be run. Assume you want to
perform 3 tasks

- Open a folder
- Open hosts file
- Open a SLN file

Then you'd define `~/iko.toml` like so:

```toml
[foobar]
cmd = 'explorer'
path = 'C:\path\to\selected\folder'

[hosts]
cmd = 'vim'
as-admin = true
path = 'C:\windows\system32\drivers\etc\hosts'

[iko]
cmd = 'vs17'
path = 'C:\Users\shrayasr\code\iko\iko.sln'
```

And from anywhere, after adding `iko` to path, you'd be able to execute any of
these tasks using their names. So:

- `iko foobar` opens the `C:\path\to\selected\folder` folder
- `iko hosts` opens the hosts file in a vim started as administrator
- `iko iko` opens Iko's solution with visual studio 2017

## Possible commands

Currently, the following values are allowed in the `cmd` section

- `chrome`
- `vs15`
- `vs17`
- `explorer`
- `vim`
- `vscode`

PRs are welcome for new runners :) 
