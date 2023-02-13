#[
@author: Gael Lopes Da Silva
@project: Runned
@github: https://github.com/Gael-Lopes-Da-Silva/Runned
@gitlab: https://gitlab.com/Gael-Lopes-Da-Silva/Runned
]#

import std/[os, terminal, strutils, times]

var
  exitCode: bool = false
  showInput: bool = false

proc echoColorLine(messageType: string, message: string, color: ForegroundColor, bright: bool): void =
  stdout.setForegroundColor(color, bright)
  stdout.write(messageType.toUpper())
  stdout.resetAttributes()
  stdout.write(": " & message & '\n')

proc echoColor(messageType: string, color: ForegroundColor, bright: bool): void =
  stdout.setForegroundColor(color, bright)
  stdout.write(messageType.toUpper())
  stdout.resetAttributes()
  stdout.write(":\n")

proc executeCommand(commands: string): int =
  let
    timerStart: float = cpuTime()
    returnValue: int = execShellCmd(commands)
    timerEnd: float = cpuTime() - timerStart
  
  echoColorLine "\ntime", $timerEnd & " s", fgGreen, false

  if exitCode: echoColorLine "exitcode", $returnValue, fgBlack, true
  if showInput: echoColorLine "input", commands.strip(trailing = true, chars = Newlines), fgBlack, true

  return returnValue

proc extractArguments(args: seq[string]): string =
  var arguments: string = ""

  for arg in args:
    case arg.toLower():
    of "--exitcode", "-ec", "-e": exitCode = true
    of "--showinput", "-si", "-i": showInput = true
    else: arguments.add(arg & " ")

  return arguments.strip(trailing = true)

proc main(args: seq[string]): int =
  if (len(args) > 0):
    try:
      return executeCommand(extractArguments(args))
    except Exception:
      echoColorLine "error", "The following problem has occured: " & getCurrentExceptionMsg(), fgRed, false
  else:
    echo "Runned v0.2"
    echo "A simple tool to check execution time of commands.\n"
    echoColorLine "usage", "Runned.exe <your commands>\n", fgBlack, true
    echoColor "options", fgBlack, true
    echo "  -e -ec --exitcode    Display the exit code of the executed command."
    echo "  -i -si --showinput   Display the input given by the user."

  return QuitSuccess

when isMainModule: quit main(commandLineParams())
