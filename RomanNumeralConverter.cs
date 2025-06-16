/*
 * Code Written by Jackson L. Davis
 *
 * This programs converts numbers to Roman numerals and vice versa.
 */

using System;
using System.Collections.Generic;

class RomanNumeralConverter {
    Dictionary<int, string> numberToRoman = new Dictionary<int, string>();
    Dictionary<string, int> romanToNumberOnes = new Dictionary<string, int>();
    Dictionary<string, int> romanToNumberTens = new Dictionary<string, int>();
    Dictionary<string, int> romanToNumberHundreds = new Dictionary<string, int>();

    public RomanNumeralConverter() {
        numberToRoman.Add(1, "I");
        numberToRoman.Add(2, "II");
        numberToRoman.Add(3, "III");
        numberToRoman.Add(4, "IV");
        numberToRoman.Add(5, "V");
        numberToRoman.Add(6, "VI");
        numberToRoman.Add(7, "VII");
        numberToRoman.Add(8, "VIII");
        numberToRoman.Add(9, "IX");
        numberToRoman.Add(10, "X");
        numberToRoman.Add(20, "XX");
        numberToRoman.Add(30, "XXX");
        numberToRoman.Add(40, "XL");
        numberToRoman.Add(50, "L");
        numberToRoman.Add(60, "LX");
        numberToRoman.Add(70, "LXX");
        numberToRoman.Add(80, "LXXX");
        numberToRoman.Add(90, "XC");
        numberToRoman.Add(100, "C");
        numberToRoman.Add(200, "CC");
        numberToRoman.Add(300, "CCC");
        numberToRoman.Add(400, "CD");
        numberToRoman.Add(500, "D");
        numberToRoman.Add(600, "DC");
        numberToRoman.Add(700, "DCC");
        numberToRoman.Add(800, "DCCC");
        numberToRoman.Add(900, "CM");
        numberToRoman.Add(1000, "M");

        romanToNumberOnes.Add("I", 1);
        romanToNumberOnes.Add("II", 2);
        romanToNumberOnes.Add("III", 3);
        romanToNumberOnes.Add("IV", 4);
        romanToNumberOnes.Add("V", 5);
        romanToNumberOnes.Add("VI", 6);
        romanToNumberOnes.Add("VII", 7);
        romanToNumberOnes.Add("VIII", 8);
        romanToNumberOnes.Add("IX", 9);

        romanToNumberTens.Add("X", 10);
        romanToNumberTens.Add("XX", 20);
        romanToNumberTens.Add("XXX", 30);
        romanToNumberTens.Add("XL", 40);
        romanToNumberTens.Add("L", 50);
        romanToNumberTens.Add("LX", 60);
        romanToNumberTens.Add("LXX", 70);
        romanToNumberTens.Add("LXXX", 80);
        romanToNumberTens.Add("XC", 90);

        romanToNumberHundreds.Add("C", 100);
        romanToNumberHundreds.Add("CC", 200);
        romanToNumberHundreds.Add("CCC", 300);
        romanToNumberHundreds.Add("CD", 400);
        romanToNumberHundreds.Add("D", 500);
        romanToNumberHundreds.Add("DC", 600);
        romanToNumberHundreds.Add("DCC", 700);
        romanToNumberHundreds.Add("DCCC", 800);
        romanToNumberHundreds.Add("CM", 900);
    }

    /**
     * @param num an integer
     * @precond num >= 0
     * @return a string representing num's Roman numeral
     */
    public string ConvertNumberToRomanNumeral(int num) {
        if (num < 0) {
            throw new Exception("num cannot be less than 0");
        }
        else if (num == 0) {
            return "";
        }
        else {
            int n = num;
            string romString = "";
            // thousands
            int thousands = n / 1000;
            for (int i = 0; i < thousands; i++) {
                romString += "M";
                n -= 1000;
            }
            // hundreds
            int hundreds = (n / 100) * 100;
            if (hundreds != 0) {
                romString += this.numberToRoman[hundreds];
                n -= hundreds;
            }
            // tens
            int tens = (n / 10) * 10;
            if (tens != 0) {
                romString += this.numberToRoman[tens];
                n -= tens;
            }
            // ones
            if (n != 0) {
                romString += this.numberToRoman[n];
            }
            return romString;
        }
    }

    /**
     * @param rom a string representing a Roman numeral
     * @precond rom must be a valid Roman numeral
     * @return an integer
     */
    public int ConvertRomanNumeralToNumber(string rom) {
        if (rom == ""){
            return 0;
        }
        else {
            string r = rom;
            int number = 0;
            // thousands
            while (r != "" && r[0] == 'M') {
                number += 1000;
                r = r.Substring(1);
            }
            // hundreds
            string rh = r;
            while (rh != "") {
                // check if the string is in the hundreds, and take off the last character every time it isn't
                if (this.romanToNumberHundreds.ContainsKey(rh)) {
                    number += this.romanToNumberHundreds[rh];
                    r = r.Substring(rh.Length);
                    break;
                }
                else {
                    rh = rh.Substring(0, rh.Length - 1);
                }
            }
            // tens
            string rt = r;
            while (rt != "") {
                if (this.romanToNumberTens.ContainsKey(rt)) {
                    number += this.romanToNumberTens[rt];
                    r = r.Substring(rt.Length);
                    break;
                }
                else {
                    rt = rt.Substring(0, rt.Length - 1);
                }
            }
            // ones
            string ro = r;
            while (ro != "") {
                if (this.romanToNumberOnes.ContainsKey(ro)) {
                    number += this.romanToNumberOnes[ro];
                    r = r.Substring(ro.Length);
                    break;
                }
                else {
                    ro = ro.Substring(0, ro.Length - 1);
                }
            }
            // if r is not the empty string by now, the Roman numeral is not valid
            if (r != "") {
                throw new Exception(rom + " is not a valid Roman numeral");
            }
            return number;
        }
    }

    static void Main(string[] args) {
        // test cases
        RomanNumeralConverter rnc = new RomanNumeralConverter();
        int wrongConversions = 0;
        string rn;
        int nm;
        for (int i = 1; i <= 1000; i++) {
            rn = rnc.ConvertNumberToRomanNumeral(i);
            Console.WriteLine(rn);
            try {
                nm = rnc.ConvertRomanNumeralToNumber(rn);
                Console.WriteLine(nm);
                if (nm != i) {
                    Console.WriteLine("Error: ConvertRomanNumeralToNumber() returned " + nm + " for Roman numeral " + rn);
                    wrongConversions += 1;
                }
            }
            catch (Exception e) {
                Console.WriteLine(rn + " is not a valid Roman numeral");
            }
        }

        int[] thousands = {1001, 1005, 1010, 1050, 1100, 1500, 1999, 2000, 3000, 4000, 5000, 10000};
        foreach (int t in thousands) {
            rn = rnc.ConvertNumberToRomanNumeral(t);
            Console.WriteLine(rn);
            try {
                nm = rnc.ConvertRomanNumeralToNumber(rn);
                Console.WriteLine(nm);
                if (nm != t) {
                    Console.WriteLine("Error: ConvertRomanNumeralToNumber() returned " + nm + " for Roman numeral " + rn);
                    wrongConversions += 1;
                }
            }
            catch (Exception e) {
                Console.WriteLine(rn + " is not a valid Roman numeral");
            }
        }

        Console.WriteLine("Test completed with " + wrongConversions + " wrong conversions");
    }
}
