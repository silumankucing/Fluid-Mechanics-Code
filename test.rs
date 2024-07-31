use std::io::{self, Write};

fn main() {
    let mut name = String::new();
    let mut email = String::new();

    print!("Enter Name: ");

    io::stdout().flush().expect("Failed to flush stdout");
    io::stdin().read_line(&mut name).expect("Failed to read line");

    print!("Enter Email: ");

    io::stdout().flush().expect("Failed to flush stdout");
    io::stdin().read_line(&mut email).expect("Failed to read line");
    
    let name = name.trim();
    let email = email.trim();

    println!("You entered: {}", name);
    println!("You entered: {}", email);

}
