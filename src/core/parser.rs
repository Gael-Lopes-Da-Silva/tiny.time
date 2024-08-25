pub fn parser(args: Vec<String>) -> Vec<String> {
    let black_list: Vec<String> = vec![
        "--excode".to_string(),
        "-e".to_string(),
        "--input".to_string(),
        "-i".to_string(),
        "--precision".to_string(),
        "-p".to_string(),
        "--nooutput".to_string(),
        "-n".to_string(),
        "--valueonly".to_string(),
        "-o".to_string(),
    ];

    let mut parsed_args: Vec<String> = vec![];
    let mut cache: Vec<String> = vec![];

    let mut precision_args = 0;

    for arg in args[1..].iter() {
        if precision_args == 1 {
            precision_args = 0;
            continue;
        }

        if !black_list.contains(arg) || cache.contains(arg) {
            parsed_args.push(arg.clone());
        } else {
            if arg == "--precision" || arg == "-p" {
                precision_args += 1;
            }

            cache.push(arg.clone());
        }
    }

    return parsed_args;
}
