<div align="center">
	<h1>Tiny Time</h1>
    <a href="https://github.com/Gael-Lopes-Da-Silva/tiny.time">https://github.com/Gael-Lopes-Da-Silva/tiny.time</a>
</div>


Description
------------------------------------------------------------------

This is a Rust application meant to check execution time of terminal command and application.


Usage
------------------------------------------------------------------

~~~
USAGE: tiny_time "<command>"

  -e, --excode     Show exit code of the command
  -i, --input      Show the inputed command
  -p, --precision  Deplay time with more digits
  -n, --nooutput   Don't display the output of the command
  -o, --valueonly  Display only values, no label
  -v, --version    Print the application version
~~~


Build From Source
------------------------------------------------------------------

Make sure to have a ready to use installation of rust. More info [here](https://www.rust-lang.org/tools/install).

~~~
git clone https://github.com/Gael-Lopes-Da-Silva/tiny.time.git
cd tiny.time
cargo build
~~~
