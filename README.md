[![Build Status](https://carlubian.visualstudio.com/GitHub%20Interop/_apis/build/status/Kaomi%20Build?branchName=master)](https://carlubian.visualstudio.com/GitHub%20Interop/_build/latest?definitionId=19?branchName=master)
[![BCH compliance](https://bettercodehub.com/edge/badge/carlubian/Kaomi?branch=master)](https://bettercodehub.com/)

# Kaomi
<strong>Remote assembly loader as an ASP.NET WebAPI.</strong>

## Overview
Kaomi (Hawaian for 'Skeleton') is a system that enables the user to load DLL assemblies from the cloud into a remote machine, instantiate and run processes from within it, and get their results.

## Requirements
The current iteration of Kaomi has the following requirements:

Host machines:
* .NET Core 3 Preview, found [here](https://dotnet.microsoft.com/download/dotnet-core/3.0).

Client machines:
* Internet access to the host machines, to make REST calls.

Clients using Kaomi.Client:
* Any .NET Standard 2 compatible framework.
