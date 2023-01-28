<h3 align="center">Runned</h3>

---

<p align="center">⚙️ Runned is a simple tool to check the execution time of terminal commands.</p>

---

### ❓ How to use

~~~shell
USAGE: Runned.exe <your commands>

OPTIONS:
  -ec --exitcode    # Display the exit code of the executed command.
  -no --nooutput    # Disable the output.
  -td --timedetail  # Add more detail to the elapsed time.
  -si --showinput   # Display the input given by the user.
~~~

### ❓ How to build

The simple way to build the project is to use Visual Studio, you just need to open the sln or csproj file and then make a build or run the project.

<details> <summary>Build manualy</summary>
<p>If you want to build manualy, you will need to install the latest dotnet SDK <a href="https://dotnet.microsoft.com/en-us/download">here</a>.</p>

<p>Then you just need to run the following cli command. You will need to choose your OS if you want to run it.</p>

```shell
# windows
$ dotnet publish -c Release -o ./Build -r win-x64 --self-contained true
$ dotnet publish -c Release -o ./Build -r win-x86 --self-contained true

# linux
$ dotnet publish -c Release -o ./Build -r linux-x64 --self-contained true
$ dotnet publish -c Release -o ./Build -r linux-arm --self-contained true

# macos
$ dotnet publish -c Release -o ./Build -r osx-x64 --self-contained true
```

</details>

If you want to download a build, check the <a href="./Build/">Build</a> folder.
