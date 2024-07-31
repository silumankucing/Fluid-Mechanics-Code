use std::io::{self, Write};

fn main() {
    let mut name = String::new();
    let mut email = String::new();

    print!("Enter Name: ");
    print!("Enter Email: ");

    io::stdout().flush().expect("Failed to flush stdout");
    io::stdin().read_line(&mut input).expect("Failed to read line");
    
    let input = input.trim();

    println!("You entered: {}", name);
    println!("You entered: {}", email);

}
