<h3 align="center">Runned</h3>

---

<p align="center">⚙️ Runned is a simple tool to check the execution time of terminal commands.</p>

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

```shell
$ nim compile --define:release --opt:speed --app:console --os:<your os> --cpu:<your cpu> Runned.nim

# windows: i386;amd64
# linux: i386;amd64;powerpc64;arm;sparc;mips;powerpc
# macosx: i386;amd64;powerpc64
# solaris: i386;amd64;sparc
# freebsd: i386;amd64
# netbsd: i386;amd64
# openbsd: i386;amd64
# haiku: i386;amd64
# android: arm
```

If you want to download a build, check the <a href="./Build/">Build</a> folder. For now, there is only a windows build because it's hard to build for multiple platform without owning one of them.
