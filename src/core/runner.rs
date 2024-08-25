use crate::core::colors::*;
use crate::options;
use std::process::Command;
use std::time::Instant;

pub fn runner(commands: Vec<String>) {
    let now = Instant::now();
    let output = if cfg!(windows) {
        Command::new("cmd")
            .args(["/C", &commands.join(" ")])
            .output()
            .unwrap()
    } else {
        Command::new("sh")
            .args(["-c", &commands.join(" ")])
            .output()
            .unwrap()
    };
    let elapsed = now.elapsed().as_secs_f64();

    let nooutput = options::NOOUPUT.lock().unwrap().clone();

    if !nooutput {
        if !output.stdout.is_empty() {
            println!("{}", String::from_utf8(output.stdout).unwrap());
        } else if !output.stderr.is_empty() {
            println!("{}", String::from_utf8(output.stderr).unwrap());
        }

        println!("---------------------------");
    }

    if options::INPUT.lock().unwrap().clone() {
        println!("{FG_BRIGHT_BLACK}Input{RESET}: {}", &commands.join(" "));
    }

    let value_only = options::VALUEONLY.lock().unwrap().clone();
    let precision = options::PRECISION.lock().unwrap().clone();

    if value_only {
        println!("{:.1$}s", elapsed, precision as usize);
    } else {
        println!("{FG_BRIGHT_BLACK}Total{RESET}: {:.1$}s", elapsed, precision as usize);
    }

    if options::EXCODE.lock().unwrap().clone() {
        println!(
            "{FG_BRIGHT_BLACK}Exit code{RESET}: {}",
            output.status.code().unwrap()
        );
    }
}
