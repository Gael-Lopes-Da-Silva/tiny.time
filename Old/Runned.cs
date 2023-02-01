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
    private static bool showInput = false;

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
        string systemConsole = string.Empty;
        string processCommands = string.Empty;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            systemConsole = "CMD.exe";
            processCommands = $"/c {commands}";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            systemConsole = "/bin/bash";
            processCommands = $"-c \"{commands}\"";
        }

        timer.Start();
        using Process process = new();
        process.StartInfo = new ProcessStartInfo(systemConsole, processCommands)
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };
        process.Start();
        process.WaitForExit();
        timer.Stop();

        string input = commands.Trim('\n');
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
        if (showInput) WriteColoredLine(ConsoleColor.DarkGray, "input", $"{input}");
    }

    private static string ExtractArguments(string[] args)
    {
        string arguments = string.Empty;

        foreach (string arg in args)
        {
            switch (arg.ToLower())
            {
                case "--exitcode":
                case "-ec":
                    exitCode = true;
                    break;

                case "--nooutput":
                case "-no":
                    noOutput = true;
                    break;

                case "--timedetail":
                case "-td":
                    timeDetail = true;
                    break;

                case "--showinput":
                case "-si":
                    showInput = true;
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
            Console.WriteLine("  -ec --exitcode    Display the exit code of the executed command.");
            Console.WriteLine("  -no --nooutput    Disable the output.");
            Console.WriteLine("  -td --timedetail  Add more detail to the elapsed time.");
            Console.WriteLine("  -si --showinput   Display the input given by the user.");
        }
    }
}