# 🎯 Mega Sena Lottery Checker (C# Console App)
A **C# Console Application** designed to validate lottery bets against the official *Mega Sena* draw results. 

## 🚀 Key Features

- Reads official lottery draw numbers from console input
- Loads bets from a CSV file (C:\apostas.csv)
- Supports bets with 6 or more numbers
- Automatically validates:
  - Number range (1–60)
  - Duplicated numbers
  - Minimum bet size
- Identifies:
  - 🏆 Sena (6 hits)
  - 🥈 Quina (5 hits)
  - 🥉 Quadra (4 hits)
- Displays:
  - Matched numbers per bet
  - Prize category per bet
- Uses color-coded console output:
  - Green → Winning results
  - Red → No prize
- Exports a full report to the user’s Downloads folder
- Fully compatible with Windows standalone execution

## 🧠 Technical Highlights

- **.NET Console Application**
- Safe parsing and validation logic
- Uses LINQ for clean and expressive data comparison
- Defensive handling of invalid or malformed bets
- Ready for extension (unit tests, logging, UI, API, etc.)

## 📄 Input Format
**Lottery Draw** (console input)

`01 02 03 04 05 06`

**Bets file** (`C:\apostas.csv`)

Each line represents one bet.
Bets may contain 6 or more numbers:

`01 02 03 04 05 06`  
`10 11 12 13 14 15 16`  
`05 10 15 20 25 30 35 40`  

## 📊 Output Example

- Console output highlights:
  - Bet number
  - Bet numbers
  - Matched numbers
  - Prize category
  - Color-coded result

A full report is also generated as:

`Downloads/resultado_mega.txt`
