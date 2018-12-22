[![Build Status](https://carlubian.visualstudio.com/GitHub%20Interop/_apis/build/status/Kaomi%20Build?branchName=master)](https://carlubian.visualstudio.com/GitHub%20Interop/_build/latest?definitionId=19?branchName=master)

# Kaomi
<strong>Remote assembly loader as an ASP.NET WebAPI.</strong>

## Overview
Kaomi (Hawaian for 'Skeleton') is a system that enables the user to load a DLL assembly from the cloud into a remote machine, instantiate and run processes from within it, and get their results.

![System diagram is supposed to go here...](https://i.imgur.com/8Pi5XVT.png)

## Requirements
The current iteration of Kaomi has the following requirements:

For the host machine:
* .NET Core 3 Preview, found [here](https://dotnet.microsoft.com/download/dotnet-core/3.0)

For the client machine:
* Internet access to the host machine, to make REST calls
* If using Kaomi.ConsoleClient, .NET Core 3 Preview
