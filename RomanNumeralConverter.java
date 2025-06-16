/*
 * Code Written by Jackson L. Davis
 *
 * This programs converts numbers to Roman numerals and vice versa.
 */

import java.util.HashMap;
import java.util.Objects;

public class RomanNumeralConverter {
    HashMap<Integer, String> numberToRoman = new HashMap<>();
    HashMap<String, Integer> romanToNumberOnes = new HashMap<>();
    HashMap<String, Integer> romanToNumberTens = new HashMap<>();
    HashMap<String, Integer> romanToNumberHundreds = new HashMap<>();

    public RomanNumeralConverter() {
        numberToRoman.put(1, "I");
        numberToRoman.put(2, "II");
        numberToRoman.put(3, "III");
        numberToRoman.put(4, "IV");
        numberToRoman.put(5, "V");
        numberToRoman.put(6, "VI");
        numberToRoman.put(7, "VII");
        numberToRoman.put(8, "VIII");
        numberToRoman.put(9, "IX");
        numberToRoman.put(10, "X");
        numberToRoman.put(20, "XX");
        numberToRoman.put(30, "XXX");
        numberToRoman.put(40, "XL");
        numberToRoman.put(50, "L");
        numberToRoman.put(60, "LX");
        numberToRoman.put(70, "LXX");
        numberToRoman.put(80, "LXXX");
        numberToRoman.put(90, "XC");
        numberToRoman.put(100, "C");
        numberToRoman.put(200, "CC");
        numberToRoman.put(300, "CCC");
        numberToRoman.put(400, "CD");
        numberToRoman.put(500, "D");
        numberToRoman.put(600, "DC");
        numberToRoman.put(700, "DCC");
        numberToRoman.put(800, "DCCC");
        numberToRoman.put(900, "CM");
        numberToRoman.put(1000, "M");

        romanToNumberOnes.put("I", 1);
        romanToNumberOnes.put("II", 2);
        romanToNumberOnes.put("III", 3);
        romanToNumberOnes.put("IV", 4);
        romanToNumberOnes.put("V", 5);
        romanToNumberOnes.put("VI", 6);
        romanToNumberOnes.put("VII", 7);
        romanToNumberOnes.put("VIII", 8);
        romanToNumberOnes.put("IX", 9);

        romanToNumberTens.put("X", 10);
        romanToNumberTens.put("XX", 20);
        romanToNumberTens.put("XXX", 30);
        romanToNumberTens.put("XL", 40);
        romanToNumberTens.put("L", 50);
        romanToNumberTens.put("LX", 60);
        romanToNumberTens.put("LXX", 70);
        romanToNumberTens.put("LXXX", 80);
        romanToNumberTens.put("XC", 90);

        romanToNumberHundreds.put("C", 100);
        romanToNumberHundreds.put("CC", 200);
        romanToNumberHundreds.put("CCC", 300);
        romanToNumberHundreds.put("CD", 400);
        romanToNumberHundreds.put("D", 500);
        romanToNumberHundreds.put("DC", 600);
        romanToNumberHundreds.put("DCC", 700);
        romanToNumberHundreds.put("DCCC", 800);
        romanToNumberHundreds.put("CM", 900);
    }

    /**
     * @param num an integer
     * @precond num >= 0
     * @return a string representing num's Roman numeral
     */
    public String convertNumberToRomanNumeral(int num) {
        if (num < 0) {
            throw new RuntimeException("num cannot be less than 0");
        }
        else if (num == 0) {
            return "";
        }
        else {
            int n = num;
            StringBuilder romString = new StringBuilder();
            // thousands
            int thousands = n / 1000;
            for (int i = 0; i < thousands; i++) {
                romString.append("M");
                n -= 1000;
            }
            // hundreds
            int hundreds = (n / 100) * 100;
            if (hundreds != 0) {
                romString.append(this.numberToRoman.get(hundreds));
                n -= hundreds;
            }
            // tens
            int tens = (n / 10) * 10;
            if (tens != 0) {
                romString.append(this.numberToRoman.get(tens));
                n -= tens;
            }
            // ones
            if (n != 0) {
                romString.append(this.numberToRoman.get(n));
            }
            return romString.toString();
        }
    }

    /**
     * @param rom a string representing a Roman numeral
     * @precond rom must be a valid Roman numeral
     * @return an integer
     */
    public int convertRomanNumeralToNumber(String rom) {
        if (Objects.equals(rom, "")){
            return 0;
        }
        else {
            String r = rom;
            int number = 0;
            // thousands
            while (!Objects.equals(r, "") && Objects.equals(r.charAt(0),'M')) {
                number += 1000;
                r = r.substring(1);
            }
            // hundreds
            String rh = r;
            while (!rh.equals("")) {
                // check if the string is in the hundreds, and take off the last character every time it isn't
                if (this.romanToNumberHundreds.containsKey(rh)) {
                    number += this.romanToNumberHundreds.get(rh);
                    r = r.substring(rh.length());
                    break;
                }
                else {
                    rh = rh.substring(0, rh.length() - 1);
                }
            }
            // tens
            String rt = r;
            while (!rt.equals("")) {
                if (this.romanToNumberTens.containsKey(rt)) {
                    number += this.romanToNumberTens.get(rt);
                    r = r.substring(rt.length());
                    break;
                }
                else {
                    rt = rt.substring(0, rt.length() - 1);
                }
            }
            // ones
            String ro = r;
            while (!ro.equals("")) {
                if (this.romanToNumberOnes.containsKey(ro)) {
                    number += this.romanToNumberOnes.get(ro);
                    r = r.substring(ro.length());
                    break;
                }
                else {
                    ro = ro.substring(0, ro.length() - 1);
                }
            }
            // if r is not the empty string by now, the Roman numeral is not valid
            if (!r.equals("")) {
                throw new RuntimeException(rom + " is not a valid Roman numeral");
            }
            return number;
        }
    }

    public static void main(String[] args) {
        // test cases
        RomanNumeralConverter rnc = new RomanNumeralConverter();
        int wrongConversions = 0;
        String rn;
        Integer nm;
        for (int i = 1; i <= 1000; i++) {
            rn = rnc.convertNumberToRomanNumeral(i);
            System.out.println(rn);
            try {
                nm = rnc.convertRomanNumeralToNumber(rn);
                System.out.println(nm);
                if (!Objects.equals(nm, i)) {
                    System.out.println("Error: convertRomanNumeralToNumber() returned " + nm + " for Roman numeral " + rn);
                    wrongConversions += 1;
                }
            }
            catch (RuntimeException e) {
                System.out.println(rn + " is not a valid Roman numeral");
            }
        }

        int[] thousands = {1001, 1005, 1010, 1050, 1100, 1500, 1999, 2000, 3000, 4000, 5000, 10000};
        for (int t : thousands) {
            rn = rnc.convertNumberToRomanNumeral(t);
            System.out.println(rn);
            try {
                nm = rnc.convertRomanNumeralToNumber(rn);
                System.out.println(nm);
                if (!Objects.equals(nm, t)) {
                    System.out.println("Error: convertRomanNumeralToNumber() returned " + nm + " for Roman numeral " + rn);
                    wrongConversions += 1;
                }
            }
            catch (RuntimeException e) {
                System.out.println(rn + " is not a valid Roman numeral");
            }
        }

        System.out.println("Test completed with " + wrongConversions + " wrong conversions");
    }
}
