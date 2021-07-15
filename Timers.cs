using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public static class Timers
{
    private static readonly Dictionary<string, Stopwatch> _stopwatches;

    static Timers()
    {
        _stopwatches = new Dictionary<string, Stopwatch>();
    }
    
    public static void Start(string name)
    {
        if (_stopwatches.ContainsKey(name))
        {
            Console.WriteLine($"'{name}' timer already running");
            return;
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _stopwatches.Add(name, stopwatch);
    }

    public static void Check(string name, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        if (!_stopwatches.ContainsKey(name))
        {
            var error = $"'{name}' timer does not exist";
            if (lineNumber > 0)
                error += $", called from line {lineNumber}";
            if (!string.IsNullOrWhiteSpace(memberName))
                error += $", in function '{memberName}'";
            
            Console.WriteLine(error);
            return;
        }

        var msg = $"'{name}' timer has been running for {_stopwatches[name].ElapsedMilliseconds} ms";
        Console.WriteLine(msg);
    }
    
    public static void Stop(string name)
    {
        if (!_stopwatches.Remove(name, out var stopwatch))
        {
            Console.WriteLine($"'{name}' timer does not exist");
            return;
        }
        
        stopwatch.Stop();
        
        Console.WriteLine($"'{name}' timer took {stopwatch.ElapsedMilliseconds} ms");
    }
}