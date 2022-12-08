/* 
@author: Gael Lopes Da Silva
@project: Runned
@github: https://github.com/Gael-Lopes-Da-Silva/Runned
@gitlab: https://gitlab.com/Gael-Lopes-Da-Silva/Runned
*/

using System.Diagnostics;
using System.Runtime.InteropServices;

class Runned
{
    private static bool exitCode = false;
    private static bool noOutput = false;
    private static bool timeDetail = false;

    private static void WriteColoredLine(ConsoleColor color, string info, string message)
    {
        Console.ForegroundColor = color;
        Console.Write($"{info.ToUpper()}: ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    private static void ExecuteCommand(string commands)
    {
        Stopwatch timer = new();
        string systemTerminal = string.Empty;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            systemTerminal = "CMD.exe";
            commands = $"/c {commands}";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            systemTerminal = "/bin/bash";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            systemTerminal = "sh";
        }

        timer.Start();
        using Process process = new();
        process.StartInfo = new ProcessStartInfo(systemTerminal, commands)
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };
        process.Start();
        process.WaitForExit();
        timer.Stop();

        string input = commands;
        string output = process.StandardOutput.ReadToEnd();

        if (output == string.Empty) output = "None";

        if (!noOutput)
        {
            Console.WriteLine("---- Output ----");
            Console.WriteLine($"{output.Trim('\n')}");
            Console.WriteLine("----------------\n");
        }
        
        if (!timeDetail)
        {
            WriteColoredLine(ConsoleColor.Green, "time", $"{timer.Elapsed:ss\\.fffff}");
        }
        else
        {
            WriteColoredLine(ConsoleColor.Green, "time", $"{timer.Elapsed}");
        }

        if (exitCode) WriteColoredLine(ConsoleColor.DarkGray, "exitcode", $"{process.ExitCode}");
    }

    private static string ExtractArguments(string[] args)
    {
        string arguments = string.Empty;

        foreach (string arg in args)
        {
            switch (arg.ToLower())
            {
                case "--exitcode":
                case "-e":
                    exitCode = true;
                    break;

                case "--nooutput":
                case "-n":
                    noOutput = true;
                    break;

                case "--timedetail":
                case "-t":
                    timeDetail = true;
                    break;

                default:
                    arguments += $"{arg} ";
                    break;
            }

        }

        return arguments.TrimEnd(' ');
    }

    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            try
            {
                ExecuteCommand(ExtractArguments(args));
            }
            catch (Exception error)
            {
                WriteColoredLine(ConsoleColor.Red, "error", $"The following problem has occured: {error.Message}");
            }
        }
        else
        {
            Console.WriteLine("Runned v0.1");
            Console.WriteLine("A simple tool to check execution time of commands.\n");

            WriteColoredLine(ConsoleColor.DarkGray, "usage", "Runned.exe [<your commands>]\n");

            WriteColoredLine(ConsoleColor.DarkGray, "options", "");
            Console.WriteLine("  -e --exitcode    Display the exit code of the executed command.");
            Console.WriteLine("  -n --nooutput    Disable the output.");
            Console.WriteLine("  -t --timedetail  Add more detail to the elapsed time.");
        }
    }
}