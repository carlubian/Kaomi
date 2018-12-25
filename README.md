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

## Kaomi Process template
```c#
/// <summary>
/// This is a Custom Kaomi Process that will iterate
/// continuously. If it is only meant to do one
/// iteration, it should extend OneTimeProcess instead.
/// </summary>
public class CustomProcess : Kaomi.Core.Model.KaomiProcess
{
    private int n;

    /// <summary>
    /// OnInitialize is invoked once when the process
    /// is loaded into the Task Host. It can be used
    /// to set up resources or read configuration.
    /// </summary>
    public override void OnInitialize()
    {
        n = 1;

        // Adjust how frequently iterations happen
        base.IterationDelay = TimeSpan.FromSeconds(5);
    }

    /// <summary>
    /// OnIteration is invoked on every iteration.
    /// If the logic held within is heavy, further
    /// iterations or user commands will be delayed.
    /// </summary>
    public override void OnIteration()
    {
        Console.WriteLine($"Iteration {n}");
        n++;

        // A process can request to be finalized. No further
        // iterations will take place.
	if (n is 10)
	    base.RequestFinalization = true;
    }

    /// <summary>
    /// OnUserMessage is invoked when the user sends a
    /// command to this process. The current iteration
    /// must finish before commands are handled.
    /// </summary>
    public override void OnUserMessage(string message)
    {
        // If a process does not handle user commands, leave this empty.
    }

    /// <summary>
    /// OnFinalize is invoked once when the process has
    /// finished iterating and is about to be unloaded.
    /// It can be used to dispose resources.
    /// </summary>
    public override void OnFinalize()
    {
        // Note that none of these method should throw exceptions.
        // Failure to comply will result on undefined behaviour.
    }
}
```
