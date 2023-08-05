package main

import (
	"fmt"
	"os"
	"os/exec"
	"runtime"
	"strings"
	"time"
)

const VERSION = "v0.4"

const RESET = "\033[0m"
const RED = "\033[31m"
const GREEN = "\033[32m"
const GREY = "\033[90m"

var g_exitCode = false
var g_input = false

func extract_commands(args []string) string {
	commands := ""

	for _, arg := range args {
		switch arg {
		case "--version", "-v":
			fmt.Println(VERSION)
			os.Exit(0)
		case "--github", "-g":
			fmt.Print("https://github.com/Gael-Lopes-Da-Silva/Runned\n")
			os.Exit(0)
		case "--exitcode", "-e":
			g_exitCode = true
		case "--input", "-i":
			g_input = true
		default:
			commands += arg + " "
		}
	}

	if commands == "" {
		fmt.Print(RED + "ERROR" + RESET + ": Missing commands\n")
		os.Exit(1)
	}

	return strings.TrimSpace(commands)
}

func execute_command(commands string) int {
	var shell *exec.Cmd
	if runtime.GOOS == "windows" {
		shell = exec.Command("cmd", "/C", commands)
	} else {
		shell = exec.Command("sh", "-c", commands)
	}

	start := time.Now()
	output, error := shell.CombinedOutput()
	elapsed := time.Since(start)

	fmt.Printf("%s\n", output)
	fmt.Printf("%sTIME%s: %s\n", GREEN, RESET, elapsed)

	exitCode := 0
	if exitError, ok := error.(*exec.ExitError); ok {
		exitCode = exitError.ExitCode()
	}

	if g_exitCode {
		if exitCode == 0 {
			fmt.Printf("%sEXIT_CODE%s: %d\n", GREEN, RESET, exitCode)
		} else {
			fmt.Printf("%sEXIT_CODE%s: %d\n", RED, RESET, exitCode)
		}
	}

	if g_input {
		fmt.Printf("%sINPUT%s: %s", GREY, RESET, commands)
	}

	return exitCode
}

func main() {
	args := os.Args[1:]
	argc := len(args)

	if argc <= 0 {
		fmt.Print(
			GREEN+"Runned\n"+RESET,
			"\n",
			GREY+"USAGE"+RESET+": runned your_command\n",
			"\n",
			"--version   -v     Print the version\n",
			"--github    -g     Give the GitHub link\n",
			"--exitcode  -e     Display the exit code of the executed command or application\n",
			"--input     -i     Display the input given by the user\n",
		)
		os.Exit(2)
	} else {
		os.Exit(execute_command(extract_commands(args)))
	}
}
