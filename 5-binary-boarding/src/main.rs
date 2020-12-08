use std::fs;

const FILENAME: &'static str = "input.txt";

fn main() {
    let seats = fs::read_to_string(FILENAME)
        .expect("Could not read file")
        .lines()
        .filter(|seat| seat.len() == 10)
        .map(|seat| get_position(seat))
        .map(|seat| get_seat_id(seat))
        .collect();

    println!("Part 1: {}", part1(&seats));
    println!("Part 2: {}", part2(&seats));
}

fn part1(seats: &Vec<u32>) -> u32 {
    return *seats
        .iter()
        .max()
        .expect("Failed to extract max");
}

fn part2(seats: &Vec<u32>) -> u32 {
    let mut prev_prev_exists: bool = false;
    let mut prev_exists: bool = false;

    for i in 0..1024 {
        if seats.iter().any(|seat| *seat == i as u32) {
            if !prev_exists && prev_prev_exists {
                return i - 1 as u32;
            }
            prev_prev_exists = prev_exists;
            prev_exists = true;
        } else {
            prev_prev_exists = prev_exists;
            prev_exists = false;
        }
    }
    
    return 0;
}

fn get_position(binay: &str) -> (u32, u32) {
    let base: u32 = 2;
    let mut chars = binay.chars();
    let mut row = 0;
    let mut col = 0;

    for i in 0..7 {
        if chars.next().unwrap() == 'B' {
            let exponent = 6 - i as u32;
            row += base.pow(exponent);
        }
    }

    for i in 7..10 {
        if chars.next().unwrap() == 'R' {
            let exponent = 9 - i as u32;
            col += base.pow(exponent);
        }
    }

    return (row, col);
}

fn get_seat_id(position: (u32, u32)) -> u32 {
    let (row, col) = position;
    return row * 8 + col;
}
