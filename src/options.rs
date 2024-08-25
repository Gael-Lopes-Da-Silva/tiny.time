use std::sync::Mutex;

pub static VERSION: &str = "v0.2";
pub static EXCODE: Mutex<bool> = Mutex::new(false);
pub static INPUT: Mutex<bool> = Mutex::new(false);
pub static NOOUPUT: Mutex<bool> = Mutex::new(false);
pub static VALUEONLY: Mutex<bool> = Mutex::new(false);
pub static PRECISION: Mutex<i32> = Mutex::new(5);
