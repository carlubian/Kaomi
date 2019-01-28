[![Build Status](https://carlubian.visualstudio.com/GitHub%20Interop/_apis/build/status/Kaomi%20Build?branchName=master)](https://carlubian.visualstudio.com/GitHub%20Interop/_build/latest?definitionId=19?branchName=master)

# Kaomi
<strong>Remote assembly loader as an ASP.NET WebAPI.</strong>

## Overview
Kaomi (Hawaian for 'Skeleton') is a system that enables the user to load DLL assemblies from the cloud into a remote machine, instantiate and run processes from within it, and get their results.

## Requirements
The current iteration of Kaomi has the following requirements:

For the host machine:
* .NET Core 3 Preview, found [here](https://dotnet.microsoft.com/download/dotnet-core/3.0)

For the client machine:
* Internet access to the host machine, to make REST calls
* If using Kaomi.ConsoleClient, .NET Core 3 Preview

## Kaomi Process lifecycle
This diagram represents how processes behave within Kaomi:

![Process lifecycle is supposed to go here...](https://i.imgur.com/yLWlGoj.png)

* <strong>Initialize</strong>: Invoked once when Kaomi loads the process into memory. Processes can use this step to load configuration files, connect to a database or acquire resources.
* <strong>Execute interation</strong>: Invoked on every iteration. Processes are supposed to use this step to execute a cycle of workload using the acquired resources.
* <strong>Process user messages</strong>: Invoked on every iteration if there are pending messages. If the process can respond to user commands, it should do so here. Any message can be received, so processes should not assume a particular format will arrive.
* <strong>Wait interval</strong>: Invoked after every iteration if the process is not one-time. Longer intervals can be broken down into several smaller intervals to respond to user messages in a timely fashion.
* <strong>Finalize</strong>: Invoked once when Kaomi unloads the process from memory. Processes can use this step to dispose resources or close connections and files.


