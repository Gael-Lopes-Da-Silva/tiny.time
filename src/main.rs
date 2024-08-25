use std::env;

mod core;
mod options;
use core::colors::*;
use core::*;

fn main() {
    let args: Vec<String> = env::args().collect();

    if args.len() - 1 == 0 {
        println!("{FG_GREEN}{}{RESET}\n\n", "Tiny.Time");
        println!(
            "{FG_BRIGHT_BLACK}{}{RESET}: {} \"<command>\"\n",
            "USAGE", args[0]
        );
        println!(
            "{}\n{}\n{}\n{}\n{}\n{}",
            "  -e, --excode     Show exit code of the command",
            "  -i, --input      Show the inputed command",
            "  -p, --precision  Deplay time with more digits",
            "  -n, --nooutput   Don't display the output of the command",
            "  -o, --valueonly  Display only values, no label",
            "  -v, --version    Print the application version"
        );
    } else {
        if args.contains(&"--version".to_string()) || args.contains(&"-v".to_string()) {
            println!("{}", options::VERSION);
            return;
        }

        if args.contains(&"--excode".to_string()) || args.contains(&"-e".to_string()) {
            let mut excode = options::EXCODE.lock().unwrap();
            *excode = true;
        }

        if args.contains(&"--input".to_string()) || args.contains(&"-i".to_string()) {
            let mut input = options::INPUT.lock().unwrap();
            *input = true;
        }

        if args.contains(&"--precision".to_string()) || args.contains(&"-p".to_string()) {
            let index = args
                .iter()
                .position(|value| value == "--precision" || value == "-p")
                .unwrap();
            let precision_value = args.get(index + 1);

            if precision_value.is_none() {
                println!("{FG_RED}{BOLD}{}{RESET}: No precision provided", "ERROR");
                return;
            } else {
                let mut precision = options::PRECISION.lock().unwrap();
                let value = precision_value.unwrap().parse::<i32>();

                if value.is_err() {
                    println!("{FG_RED}{BOLD}{}{RESET}: Precision not an integer", "ERROR");
                    return;
                } else {
                    *precision = value.unwrap();
                }
            }
        }

        if args.contains(&"--nooutput".to_string()) || args.contains(&"-n".to_string()) {
            let mut debug = options::NOOUPUT.lock().unwrap();
            *debug = true;
        }

        if args.contains(&"--valueonly".to_string()) || args.contains(&"-o".to_string()) {
            let mut debug = options::VALUEONLY.lock().unwrap();
            *debug = true;
        }

        let parsed_args = parser::parser(args);
        runner::runner(parsed_args);
    }
}
