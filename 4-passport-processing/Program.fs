open System
open System.IO
open System.Linq
open System.Text.RegularExpressions

let validByr (fields: string[]): bool = 
    let field = fields.FirstOrDefault(fun (p) -> p.Contains("byr"))
    if Object.ReferenceEquals(field, null) then
        false
    else
        let byr: int = new string(field.SkipWhile(fun (p) -> p <> ':').Skip(1).ToArray()) |> int
        1920 <= byr && byr <= 2002

let validIyr (fields: string[]): bool =
    let field =  fields.FirstOrDefault(fun (p) -> p.Contains("iyr"))
    if Object.ReferenceEquals(field, null) then
        false
    else
        let iyr: int = new string(field.SkipWhile(fun (p) -> p <> ':').Skip(1).ToArray()) |> int
        2010 <= iyr && iyr <= 2020

let validEyr (fields: string[]): bool =
    let field = fields.FirstOrDefault(fun (p) -> p.Contains("eyr"))
    if Object.ReferenceEquals(field, null) then
        false
    else
        let eyr: int = new string(field.SkipWhile(fun (p) -> p <> ':').Skip(1).ToArray()) |> int
        2020 <= eyr && eyr <= 2030

let validHgt (fields: string[]): bool =
    let field = fields.FirstOrDefault(fun (p) -> p.Contains("hgt"))
    if Object.ReferenceEquals(field, null) then
        false
    else
        let hgt: string = new string(field.SkipWhile(fun (p) -> p <> ':').Skip(1).ToArray())
        let hgtNum: int = hgt.Substring(0, hgt.Length - 2) |> int
        let typ: string = new string(hgt.TakeLast(2).ToArray())
        if typ = "cm" then
            150 <= hgtNum && hgtNum <= 193
        else if typ = "in" then
            59 <= hgtNum && hgtNum <= 76
        else
            false

let validHcl (fields: string[]): bool =
    let field = fields.FirstOrDefault(fun (p) -> p.Contains("hcl"))
    if Object.ReferenceEquals(field, null) then
        false
    else
        let hcl: string = new string(field.SkipWhile(fun (p) -> p <> ':').Skip(1).ToArray())
        Regex.Match(hcl, "#[0-9abcdefABCDEF]{6}").Success

let validEcl (fields: string[]): bool =
    let field = fields.FirstOrDefault(fun (p) -> p.Contains("ecl"))
    if Object.ReferenceEquals(field, null) then
        false
    else
        let ecl: string = new string(field.SkipWhile(fun (p) -> p <> ':').Skip(1).ToArray())
        ecl = "amb" ||  ecl = "blu" ||  ecl = "brn" ||  ecl = "gry" ||  ecl = "grn" ||  ecl = "hzl" ||  ecl = "oth" 

let validPid (fields: string[]): bool =
    let field = fields.FirstOrDefault(fun (p) -> p.Contains("pid"))
    if Object.ReferenceEquals(field, null) then
        false
    else
        let pid: string = new string(field.SkipWhile(fun (p) -> p <> ':').Skip(1).ToArray())
        pid.Length = 9 && Regex.Match(pid, "[0-9]{9}").Success

let validate1 (passport: string): bool =
    passport.Contains("byr") &&
    passport.Contains("iyr") &&
    passport.Contains("eyr") &&
    passport.Contains("hgt") &&
    passport.Contains("hcl") &&
    passport.Contains("ecl") &&
    passport.Contains("pid")

let validate2 (passport: string): bool =
    let fields = passport.Split()
    validByr fields && validIyr fields && validEyr fields && validHgt fields && validHcl fields && validEcl fields && validPid fields

[<EntryPoint>]
let main _ : int =
    use sr = new StreamReader("./input.txt")
    let raw = sr.ReadToEnd()
    let passports = raw.Split("\n\n")
    printfn "Part 1: %d" (passports.Where(validate1).Count())
    printfn "Part 2: %d" (passports.Where(validate2).Count())
    0
