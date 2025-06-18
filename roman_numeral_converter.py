"""
Code Written by Jackson L. Davis

This program converts numbers to Roman numerals and vice versa.
"""


number_to_roman = {1: "I", 2: "II", 3: "III", 4: "IV", 5: "V", 6: "VI", 7: "VII", 8: "VIII", 9: "IX", 10: "X",
                   20: "XX", 30: "XXX", 40: "XL", 50: "L", 60: "LX", 70: "LXX", 80: "LXXX", 90: "XC", 100: "C",
                   200: "CC", 300: "CCC", 400: "CD", 500: "D", 600: "DC", 700: "DCC", 800: "DCCC", 900: "CM", 1000: "M"}

roman_to_number_ones = {"I": 1, "II": 2, "III": 3, "IV": 4, "V": 5, "VI": 6, "VII": 7, "VIII": 8, "IX": 9}
roman_to_number_tens = {"X": 10, "XX": 20, "XXX": 30, "XL": 40, "L": 50, "LX": 60, "LXX": 70, "LXXX": 80, "XC": 90}
roman_to_number_hundreds = {"C": 100, "CC": 200, "CCC": 300, "CD": 400, "D": 500, "DC": 600, "DCC": 700, "DCCC": 800, "CM": 900}


def convert_number_to_roman_numeral(num):
    """
    :param num: an integer
    :precond: num >= 0
    :return: a string representing num's Roman numeral
    """
    assert num >= 0
    if num == 0:
        return ""
    else:
        n = num
        rom_string = ""
        # thousands
        thousands = n // 1000
        for i in range(thousands):
            rom_string += "M"
            n -= 1000
        # hundreds
        hundreds = (n // 100) * 100
        if hundreds != 0:
            rom_string += number_to_roman[hundreds]
            n -= hundreds
        # tens
        tens = (n // 10) * 10
        if tens != 0:
            rom_string += number_to_roman[tens]
            n -= tens
        # ones
        if n != 0:
            rom_string += number_to_roman[n]
        return rom_string


def convert_roman_numeral_to_number(rom):
    """
    :param rom: a string representing a Roman numeral
    :precond: rom must be a valid Roman numeral
    :return: an integer
    """
    if rom == "":
        return 0
    else:
        r = rom
        number = 0
        # thousands
        while r != "" and r[0] == "M":
            number += 1000
            r = r[1:]
        # hundreds
        rh = r
        while rh != "":
            # check if the string is in the hundreds, and take off the last character every time it isn't
            if rh in roman_to_number_hundreds.keys():
                number += roman_to_number_hundreds[rh]
                r = r[len(rh):]
                break
            else:
                rh = rh[:-1]
        # tens
        rt = r
        while rt != "":
            if rt in roman_to_number_tens.keys():
                number += roman_to_number_tens[rt]
                r = r[len(rt):]
                break
            else:
                rt = rt[:-1]
        # ones
        ro = r
        while ro != "":
            if ro in roman_to_number_ones.keys():
                number += roman_to_number_ones[ro]
                r = r[len(ro):]
                break
            else:
                ro = ro[:-1]
        # if r is not the empty string by now, the Roman numeral is not valid
        if r != "":
            raise Exception(rom + " is not a valid Roman numeral")
        return number


# test cases
exceptions = 0
wrong_conversions = 0
for i in range(1000):
    rn = convert_number_to_roman_numeral(i + 1)
    print(rn)
    try:
        nm = convert_roman_numeral_to_number(rn)
        print(nm)
        if nm != i + 1:
            print("Error: convert_roman_numeral_to_number() returned " + str(nm) + " for Roman numeral " + rn)
            wrong_conversions += 1
    except:
        print(rn + " is not a valid Roman numeral")
        exceptions += 1

thousands = [1001, 1005, 1010, 1050, 1100, 1500, 1999, 2000, 3000, 4000, 5000, 10000]
for t in thousands:
    rn = convert_number_to_roman_numeral(t)
    print(rn)
    try:
        nm = convert_roman_numeral_to_number(rn)
        print(nm)
        if nm != t:
            print("Error: convert_roman_numeral_to_number() returned " + str(nm) + " for Roman numeral " + rn)
            wrong_conversions += 1
    except:
        print(rn + " is not a valid Roman numeral")
        exceptions += 1


print("Test completed with " + str(exceptions) + " exceptions")
print("Test completed with " + str(wrong_conversions) + " wrong conversions")
