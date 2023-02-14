<h3 align="center">
    Runned
</h3>

---

<p align="center">
    ⚙️ Runned is a simple tool to check the execution time of terminal commands.
</p>

---

### ❓ How to use
~~~
USAGE: Runned.exe <your commands>

OPTIONS:
  -ec --exitcode    # Display the exit code of the executed command.
  -si --showinput   # Display the input given by the user.
~~~

### ❓ How to build
To build, you will first need to download the nim compiler. You can find it [here](https://nim-lang.org/install.html).

Then, run the following command. You will need to change the os and cpu type to math your computer.

```console
$ nimble install --define:release --opt:speed --app:console

# or

$ nimble install runned
```

If you want to download a build, check the [Build](./Build/) folder. For now, there is only a windows build because it's hard to build for multiple platform without owning one of them.
