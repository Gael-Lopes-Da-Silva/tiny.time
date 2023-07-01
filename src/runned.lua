--[[ 
author: Gael Lopes Da Silva
project: Runned
github: https://github.com/Gael-Lopes-Da-Silva/Runned
]]

BLACK = "\27[30m"
RED   = "\27[31m"
GREEN = "\27[32m"
RESET = "\27[0m"

local exit_code     = false
local show_inpout   = false
local time_accuracy = false

---Print and format a string with colors.
---@param message_type string
---@param message string
---@param color string
local function print_colored(message_type, message, color)
    print(string.format("%s%s%s: %s", color, string.upper(message_type), RESET, message))
end

---Execute a command and returns it's exit code.
---@param commands string
---@return integer? exit_code
local function execute_command(commands)
    local timer_start = os.clock()
    local _, _, return_value = os.execute(commands)
    local timer_end = os.clock()

    print_colored("\ntime", string.format(time_accuracy and "%.20f s" or "%.3f s", timer_end - timer_start), GREEN)

    if (exit_code) then print_colored("exit_code", string.format("%d", return_value), return_value == 0 and GREEN or RED) end
    if (show_inpout) then print_colored("input", commands, BLACK) end

    return return_value
end

---Extract specific arguments from a string and return a formated version without those arguments.
---@param args string[]
---@return string
local function extract_arguments(args)
    local commands = ""

    for _, arg in ipairs(args) do
        arg = string.lower(arg)

        if (arg == "--exitcode" or arg == "-e") then
            exit_code = true
        elseif (arg == "--input" or arg == "-i") then
            show_inpout = true
        elseif (arg == "--accuracy" or arg == "-a") then
            time_accuracy = true
        else
            commands = commands .. arg .. " "
        end
    end

    return commands
end

local function main(args)
    if (#args > 0) then
        return execute_command(extract_arguments(args))
    else
        print("Runned v0.3")
        print("A simple tool to check execution time of commands.\n")
        print_colored("usage", "runned.exe <your command>\n", BLACK)
        print_colored("options", "", BLACK)
        print("  -e --exitcode    Display the exit code of the executed command.")
        print("  -i --input       Display the input given by the user.")
        print("  -a --accuracy    Display time with more precision.")
    end

    return 0
end

os.exit(main(arg))
