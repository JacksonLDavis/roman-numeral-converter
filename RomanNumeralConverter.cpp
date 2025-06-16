/*
 * Code Written by Jackson L. Davis
 *
 * This programs converts numbers to Roman numerals and vice versa.
 *
 * Reference Numbers for Exceptions:
 * 501: convertNumberToRomanNumeral(): num must be greater than or equal to 0
 * 502: convertRomanNumeralToNumber(): rom must be a valid Roman numeral
 */

#include <iostream>
#include <map>
#include <string>
using namespace std;

class RomanNumeralConverter {
    public:
        map<int, string> numberToRoman;
        map<string, int> romanToNumberOnes;
        map<string, int> romanToNumberTens;
        map<string, int> romanToNumberHundreds;

        RomanNumeralConverter() {
            numberToRoman[1] = "I";
            numberToRoman[2] = "II";
            numberToRoman[3] = "III";
            numberToRoman[4] = "IV";
            numberToRoman[5] = "V";
            numberToRoman[6] = "VI";
            numberToRoman[7] = "VII";
            numberToRoman[8] = "VIII";
            numberToRoman[9] = "IX";
            numberToRoman[10] = "X";
            numberToRoman[20] = "XX";
            numberToRoman[30] = "XXX";
            numberToRoman[40] = "XL";
            numberToRoman[50] = "L";
            numberToRoman[60] = "LX";
            numberToRoman[70] = "LXX";
            numberToRoman[80] = "LXXX";
            numberToRoman[90] = "XC";
            numberToRoman[100] = "C";
            numberToRoman[200] = "CC";
            numberToRoman[300] = "CCC";
            numberToRoman[400] = "CD";
            numberToRoman[500] = "D";
            numberToRoman[600] = "DC";
            numberToRoman[700] = "DCC";
            numberToRoman[800] = "DCCC";
            numberToRoman[900] = "CM";
            numberToRoman[1000] = "M";

            romanToNumberOnes["I"] = 1;
            romanToNumberOnes["II"] = 2;
            romanToNumberOnes["III"] = 3;
            romanToNumberOnes["IV"] = 4;
            romanToNumberOnes["V"] = 5;
            romanToNumberOnes["VI"] = 6;
            romanToNumberOnes["VII"] = 7;
            romanToNumberOnes["VIII"] = 8;
            romanToNumberOnes["IX"] = 9;

            romanToNumberTens["X"] = 10;
            romanToNumberTens["XX"] = 20;
            romanToNumberTens["XXX"] = 30;
            romanToNumberTens["XL"] = 40;
            romanToNumberTens["L"] = 50;
            romanToNumberTens["LX"] = 60;
            romanToNumberTens["LXX"] = 70;
            romanToNumberTens["LXXX"] = 80;
            romanToNumberTens["XC"] = 90;

            romanToNumberHundreds["C"] = 100;
            romanToNumberHundreds["CC"] = 200;
            romanToNumberHundreds["CCC"] = 300;
            romanToNumberHundreds["CD"] = 400;
            romanToNumberHundreds["D"] = 500;
            romanToNumberHundreds["DC"] = 600;
            romanToNumberHundreds["DCC"] = 700;
            romanToNumberHundreds["DCCC"] = 800;
            romanToNumberHundreds["CM"] = 900;
        }

        /**
         * @param num an integer
         * @precond num >= 0
         * @return a string representing num's Roman numeral
         */
        string convertNumberToRomanNumeral(int num) {
            if (num < 0) {
                throw 501;
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
                    romString = romString + "M";
                    n -= 1000;
                }
                // hundreds
                int hundreds = (n / 100) * 100;
                if (hundreds != 0) {
                    romString = romString + numberToRoman[hundreds];
                    n -= hundreds;
                }
                // tens
                int tens = (n / 10) * 10;
                if (tens != 0) {
                    romString = romString + numberToRoman[tens];
                    n -= tens;
                }
                // ones
                if (n != 0) {
                    romString = romString + numberToRoman[n];
                }
                return romString;
            }
        }
    
        /**
         * @param rom a string representing a Roman numeral
         * @precond rom must be a valid Roman numeral
         * @return an integer
         */
        int convertRomanNumeralToNumber(string rom) {
            if (rom == ""){
                return 0;
            }
            else {
                string r = rom;
                int number = 0;
                // thousands
                while (r != "" && r.at(0) == 'M') {
                    number += 1000;
                    r = r.substr(1);
                }
                // hundreds
                string rh = r;
                while (rh != "") {
                    // check if the string is in the hundreds, and take off the last character every time it isn't
                    if (romanToNumberHundreds.count(rh) > 0) {
                        number += romanToNumberHundreds[rh];
                        r = r.substr(rh.length());
                        break;
                    }
                    else {
                        rh = rh.substr(0, rh.length() - 1);
                    }
                }
                // tens
                string rt = r;
                while (rt != "") {
                    if (romanToNumberTens.count(rt) > 0) {
                        number += romanToNumberTens[rt];
                        r = r.substr(rt.length());
                        break;
                    }
                    else {
                        rt = rt.substr(0, rt.length() - 1);
                    }
                }
                // ones
                string ro = r;
                while (ro != "") {
                    if (romanToNumberOnes.count(ro) > 0) {
                        number += romanToNumberOnes[ro];
                        r = r.substr(ro.length());
                        break;
                    }
                    else {
                        ro = ro.substr(0, ro.length() - 1);
                    }
                }
                // if r is not the empty string by now, the Roman numeral is not valid
                if (r != "") {
                    throw 502;
                }
                return number;
            }
        }
};

int main() {
    // test cases
    RomanNumeralConverter rnc;
    int wrongConversions = 0;
    string rn;
    int nm;
    for (int i = 1; i <= 1000; i++) {
        rn = rnc.convertNumberToRomanNumeral(i);
        cout << rn << "\n";
        try {
            nm = rnc.convertRomanNumeralToNumber(rn);
            cout << nm << "\n";
            if (nm != i) {
                cout << "Error: convertRomanNumeralToNumber() returned " << nm << " for Roman numeral " << rn << "\n";
                wrongConversions += 1;
            }
        }
        catch (int errNum) {
            cout << errNum << ": " << rn << " is not a valid Roman numeral";
        }
    }

    int thousands[12] = {1001, 1005, 1010, 1050, 1100, 1500, 1999, 2000, 3000, 4000, 5000, 10000};
    for (int t : thousands) {
        rn = rnc.convertNumberToRomanNumeral(t);
        cout << rn << "\n";
        try {
            nm = rnc.convertRomanNumeralToNumber(rn);
            cout << nm << "\n";
            if (nm != t) {
                cout << "Error: convertRomanNumeralToNumber() returned " << nm << " for Roman numeral " << rn << "\n";
                wrongConversions += 1;
            }
        }
        catch (int errNum) {
            cout << errNum << ": " << rn << " is not a valid Roman numeral" << "\n";
        }
    }

    cout << "Test completed with " << wrongConversions << " wrong conversions" << "\n";
    
    return 0;
}
