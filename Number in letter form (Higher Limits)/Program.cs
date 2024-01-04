using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomChallenge
{
    class Program
    {
        static void Main(string[] args) // A code that makes you input a number and it outputs it in letter for (now over 9 quadrillion!!!!!)
        {
            string[] ones = new string[9] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] smoltens = new string[9] { "eleven", "tweleve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eightteen", "nineteen" };
            string[] tens = new string[9] { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string[] majors = new string[] { "hundred", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "quattuordecillion", "quindecillion", "sexdecillion", "septendecillion", "octodecillion", "novemdecillion", "vigintillion", "bajillion" };
            string[] extratags = new string[] { "and", ",", "-" };
            string negative = "(minus) ";

            while (true)
            {
                string[] numgroups = new string[100];
            int digitval = 0;
            int numleft = 0;
            int zerocounter = 0;
            const int specialhundred = 3;
            string NumberInLetterForm = "";
            bool IsNegative = false;



                Console.WriteLine("Write a number and we will show you in letters");
                // 3 - 1 thou ,6 - 1 mil, 9 - 1 bil - 12 1 tril
                // 3,712,185

                bool numbervalid = false;
                while (numbervalid == false)
                {
                    string numinput = Console.ReadLine(); 
                    numinput = "0" + numinput; // This code seems to break when the input is empty so I have put a placeholder zero in front of the output.
                    if (numinput.Substring(1, 1) == extratags[2]) // Check if a minus number
                    {
                        NumberInLetterForm = NumberInLetterForm + negative;
                        numinput = numinput.Substring(2, numinput.Length - 2);
                        IsNegative = true;
                    }

                    int NotSigFigs = 0; // Remove any groups of 3 zeros at beginning of number
                    while (numinput.Substring(NotSigFigs, 1) == "0" && (numinput.Length - specialhundred) > NotSigFigs) // Zero remover
                    {
                        NotSigFigs = NotSigFigs + 1;
                    }
                    numinput = numinput.Substring(NotSigFigs, numinput.Length - NotSigFigs);

                    numleft = numinput.Length % specialhundred;
                    if (numleft != 0)
                    {
                        numgroups[0] = numinput.Substring(0, numleft);
                        for (int i = 0; i < specialhundred - numleft; i++) //Zero filler
                        {
                            numgroups[0] = "0" + numgroups[0];
                            numinput = "0" + numinput;
                        }
                        numleft = 0;
                    }


                    while (numleft < numinput.Length)
                    {
                        numbervalid = true;
                        try
                        {
                            string IndividualNumber = numinput.Substring(numleft, specialhundred);
                            int OriginalNum = Math.Abs(int.Parse(IndividualNumber)); // Check if the group of string can be written as letters and remove any accidental (or deliberate) mathematical symbols.
                            for (int i = 0; i < specialhundred - 1; i++) // Remove zeros from front
                            {
                                if (IndividualNumber.Substring(0, 1) == "0")
                                {
                                    IndividualNumber = IndividualNumber.Substring(1, IndividualNumber.Length - 1);
                                }
                            }
                            if (IndividualNumber != Convert.ToString(OriginalNum)) // In case a mathematical operator is found in the number/something that the "int" datatype didn't catch, create a purposeful error to reset back.
                            {
                                Convert.ToInt32("THISISNOTNUMBER");
                            }
                        }

                        catch
                        {
                            Console.WriteLine("Number invalid! Please try again...");
                            NumberInLetterForm = "";
                            numleft = 0;
                            digitval = 0;
                            numbervalid = false;
                            IsNegative = false;
                            break;
                        }

                        numgroups[digitval] = numinput.Substring(numleft, specialhundred);
                        digitval = digitval + 1;
                        numleft = numleft + specialhundred;


                    }
                }

                if (digitval > 0)
                {
                    int SeparatorPosition = 0;
                    for (int i = 0; i < digitval; i++) // Number builder
                    {
                        bool realtensfound = false;
                        if (numgroups[i].Substring(0, 3) != "000")
                        {
                            //NumberInLetterForm = NumberInLetterForm + " ";
                            if (numgroups[i].Substring(0, 1) != "0") //Hundreds digit
                            {
                                NumberInLetterForm = NumberInLetterForm + ones[Convert.ToInt32(numgroups[i].Substring(0, 1)) - 1] + " " + majors[0];

                                if (numgroups[i].Substring(1, 1) != "0" || numgroups[i].Substring(2, 1) != "0") // Include "and"
                                {
                                    NumberInLetterForm = NumberInLetterForm + " " + extratags[0] + " ";
                                }
                            }
                            else
                            {
                                if (i == digitval - 1 && digitval > 1 && (numgroups[i].Substring(1, 1) != "0" || numgroups[i].Substring(2, 1) != "0"))
                                {
                                    NumberInLetterForm = NumberInLetterForm + extratags[0] + " ";
                                }
                            }

                            if (numgroups[i].Substring(1, 1) != "0") // Tens digit
                            {
                                if (numgroups[i].Substring(1, 1) == "1" && numgroups[i].Substring(2, 1) != "0") // Numbers 11-19
                                {
                                    realtensfound = true;
                                    NumberInLetterForm = NumberInLetterForm + smoltens[Convert.ToInt32(numgroups[i].Substring(2, 1)) - 1];
                                }
                                else // Numbers 10 and 20-99
                                {
                                    NumberInLetterForm = NumberInLetterForm + tens[Convert.ToInt32(numgroups[i].Substring(1, 1)) - 1];
                                    if (numgroups[i].Substring(2, 1) != "0")
                                    {
                                        NumberInLetterForm = NumberInLetterForm + " ";
                                    }
                                }
                            }

                            if (numgroups[i].Substring(2, 1) != "0" && realtensfound == false) // Ones digit
                            {
                                NumberInLetterForm = NumberInLetterForm + ones[Convert.ToInt32(numgroups[i].Substring(2, 1)) - 1];
                            }

                            if (digitval - i > majors.Length) //Allow powers to be used if number exceeds the number suffix limit.
                            {
                                NumberInLetterForm = NumberInLetterForm + " " + majors[majors.Length - 1] + "(x10^" + Convert.ToString((digitval - i - majors.Length) * 3) + ")" + extratags[1];
                            }
                            else
                            {
                                if (i < digitval - 1)
                                {
                                    NumberInLetterForm = NumberInLetterForm + " " + majors[digitval - i - 1];
                                }
                                if (SeparatorPosition > 0)
                                {
                                    NumberInLetterForm = NumberInLetterForm.Substring(0, SeparatorPosition) + extratags[1] + NumberInLetterForm.Substring(SeparatorPosition, NumberInLetterForm.Length - SeparatorPosition);
                                }
                                SeparatorPosition = NumberInLetterForm.Length;
                            }
                            NumberInLetterForm = NumberInLetterForm + " ";

                        }
                        else
                        {
                            zerocounter = zerocounter + 1;
                        }
                    }
                    if (zerocounter == digitval)
                    {
                        Console.WriteLine("Zero");
                    }
                    else
                    {
                        string commaNums = "";
                        bool NumFound = false;
                        if (IsNegative == true)
                        {
                            commaNums = commaNums + extratags[2];
                        }
                        for (int i = 0; i < digitval; i++)
                        {
                            if (i > 0)
                            {
                                commaNums = commaNums + extratags[1];
                            }
                            if (NumFound == false)
                            {
                                commaNums = commaNums + Convert.ToString(Convert.ToInt32(numgroups[i]));
                                NumFound = true;
                            }
                            else
                            {
                                commaNums = commaNums + numgroups[i];
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine(commaNums);
                        Console.WriteLine();
                        Console.WriteLine(NumberInLetterForm);
                        for (int j = 0; j < 5; j++)
                        {
                            Console.WriteLine();
                        }
                        //// Scroller
                        //for (int i = 1; i <= NumberInLetterForm.Length; i++)
                        //{
                        //    for (int j = 0; j < 60; j++)
                        //    {
                        //        Console.WriteLine();
                        //    }
                        //    Console.WriteLine(NumberInLetterForm.Substring(0, i));
                        //    System.Threading.Thread.Sleep(50);
                        //}
                    }
                }

            }

        }
    }
}
