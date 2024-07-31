use std::io::{self, Write};

fn main() {
    let mut input = String::new();

    print!("Please enter some text: ");
    
    io::stdout().flush().expect("Failed to flush stdout");
    io::stdin().read_line(&mut input).expect("Failed to read line");
    
    let input = input.trim();

    println!("You entered: {}", input);
}
