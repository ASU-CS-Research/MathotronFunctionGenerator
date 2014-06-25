using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace TextOutput
{
    /// <summary>
    /// Program that helps with the autogeneration of .function file text for
    /// use in Mathotron.
    /// 
    /// Author:  Austin Keener (DV8FromTheWorld)
    /// </summary>
    class FunctionGenerator
    {
        private static Random rand;
        private static readonly string MENU = 
            "=================================\n"
                + "Which grade level would you like?\n\n"
                + " 0:  PreK (Counting)\n"
                + " 1:  1st Grade (Integer Addition)\n"
                + " 2:  2nd Grade (Integer Multiplication)\n"
                + " 3:  3rd Grade (Integer Division)\n"
                + " 4:  4th Grade (Integer Division w/ Remainders)\n"
                + "-1:  Exit Program\n"
                + "=================================";

        /// <summary>
        /// Intital beginning point of the program.
        /// Allows the user to select an option from a menu.
        /// </summary>
        /// <param name="args">Command Line Arguments (Unused)</param>
        public static void Main(string[] args)
        {
            rand = new Random();
            bool exit = false;
            int grade;
            Console.WriteLine("This program outputs generated text for Mathotron to the console."); 
            while (!exit)
            {
                grade = readInt(MENU);
                try
                {
                    switch (grade)
                    {
                        case 0:
                            GenPreK();
                            break;
                        case 1:
                            GenFirstGrade();
                            break;
                        case 2:
                            GenSecondGrade();
                            break;
                        case 3:
                            GenThirdGrade();
                            break;
                        case 4:
                            GenFourthGrade();
                            break;
                        case -1:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option selected.");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Method that generates text output that works with the PreK selection in Mathotron.
        /// 
        /// output format:  #,#-3 #-2 #-1 _ #+1
        /// Example:        5,2 3 4 _ 6
        /// 
        /// It does this 3 times for each number, but never moving the blank to the very end of the number line.
        /// </summary>
        private static void GenPreK()
        {
            using (StreamWriter outputFile = new StreamWriter("PreK.functions"))
            {
                int startValue = readInt("Starting value?");
                int stopValue = readInt("Stopping value? (Must be greater than or equal to start value");
                int numberLineSize = 5;
                for (int i = startValue; i <= stopValue; i++)
                {
                    for (int j = 0; j < numberLineSize - 2; j++)
                    {
                        String s = String.Format("{0} {1} {2} {3} {4}",
                            (i - 3) + j,
                            (i - 2) + j,
                            (i - 1) + j,
                            i + j,
                            (i + 1) + j);
                        Console.WriteLine(i + "," + s.Replace("" + i, "_"));
                        outputFile.WriteLine(i + "," + s.Replace("" + i, "_"));
                    }
                }
                Console.WriteLine("Output to: " + ((FileStream)outputFile.BaseStream).Name);
            }             
        }

        /// <summary>
        /// Method that generates text output that works with the First Grade selection in Mathotron.
        /// Can output Addition or Subtraction.
        /// 
        /// Addition
        /// output format:  (i+j), i + j = _
        /// Example:        7,2 + 5 = _
        /// 
        /// Subtraction
        /// output format:  (i-j), i - j = _
        /// Example:        5,8 - 5 = _
        /// </summary>
        private static void GenFirstGrade()
        {
            using (StreamWriter outputFile = new StreamWriter("FirstGrade.functions"))
            {
                string input = readLine("Addition or Subtraction? (a/s)").ToLower();
                while (!input.Equals("a") && !input.Equals("s"))
                {
                    input = readLine("Please enter either a or s to select an option.").ToLower();
                }
                if (input.Equals("a"))
                {
                    int startValue = readInt("Starting value?");
                    int stopValue = readInt("Stopping value? (Must be greater than or equal to start value");
                    int maxAdden = readInt("Max adden? (EX: If  5+10, max would be 10");
                    for (int i = startValue; i <= stopValue; i++)
                    {
                        for (int j = 0; j <= maxAdden; j++)
                        {
                            Console.WriteLine(String.Format("{0},{1} + {2} = _", i + j, i, j));
                            outputFile.WriteLine(String.Format("{0},{1} + {2} = _", i + j, i, j));
                        }
                    }
                }
                else
                {
                    int startValue = readInt("Starting value?");
                    int stopValue = readInt("Stopping value? (Must be greater than or equal to start value");
                    int maxAdden = readInt("Max subtrahend? (EX: If  15-10, max would be 10");
                    bool negativeNumbers = readBool("Include forumlas with negative answers? (true/false)");
                    for (int i = startValue; i <= stopValue; i++)
                    {
                        for (int j = 0; j <= maxAdden; j++)
                        {
                            if (i - j < 0 && !negativeNumbers)
                            {
                                continue;
                            }
                            Console.WriteLine(String.Format("{0},{1} - {2} = _", i - j, i, j));
                            outputFile.WriteLine(String.Format("{0},{1} - {2} = _", i - j, i, j));
                        }
                    }
                }
                Console.WriteLine("Output to: " + ((FileStream)outputFile.BaseStream).Name);
            }
        }

        /// <summary>
        /// Method that generates text output that works with the Second Grade selection in Mathotron.
        /// 
        /// output format:  (i*j),i * j = _
        /// Example:        45,5 * 9 = _
        /// </summary>
        private static void GenSecondGrade()
        {
            using (StreamWriter outputFile = new StreamWriter("SecondGrade.functions"))
            {
                int startValue = readInt("Starting value?");
                int stopValue = readInt("Stopping value? (Must be greater than or equal to start value");
                int highestFactor = readInt("How large would you like the largest factor to be? (EX: 2 x 10 would be 10");
                for (int i = startValue; i <= stopValue; i++)
                {
                    for (int j = 0; j <= highestFactor; j++)
                    {
                        Console.WriteLine(String.Format("{0},{1} * {2} = _", i * j, i, j));
                        outputFile.WriteLine(String.Format("{0},{1} * {2} = _", i * j, i, j));
                    }
                }
                Console.WriteLine("Output to: " + ((FileStream)outputFile.BaseStream).Name);
            }
        }

        /// <summary>
        ///Method that generates text output that works with the Third Grade selection in Mathotron.
        ///
        /// output format:  j,(i*j) / i = _
        /// Example:        5,45 / 9 = _
        /// </summary>
        private static void GenThirdGrade()
        {
            using (StreamWriter outputFile = new StreamWriter("ThirdGrade.functions"))
            {
                int startValue = readInt("Starting value?");
                int stopValue = readInt("Stopping value? (Must be greater than or equal to start value");
                int highestFactor = readInt("How large would you like the largest factor to be? (EX: 10 / 2 would be 5 (2 * 5)");
                for (int i = startValue; i <= stopValue; i++)
                {
                    for (int j = 0; j <= highestFactor; j++)
                    {
                        Console.WriteLine(String.Format("{0},{1} / {2} = _", j, i * j, i));
                        outputFile.WriteLine(String.Format("{0},{1} / {2} = _", j, i * j, i));
                    }
                }
                Console.WriteLine("Output to: " + ((FileStream)outputFile.BaseStream).Name);
            }
        }

        /// <summary>
        /// Method that generates text output that works with the Fourth Grade selection in mathotron.
        ///
        /// output format:  dividend/divisor r dividend%divisor,dividend / divisor = _
        /// Example:        5r2,47 / 9 = _
        /// </summary>
        private static void GenFourthGrade()
        {
            using (StreamWriter outputFile = new StreamWriter("FourthGrade.functions"))
            {
                int dividendMaxAmount = readInt("Max amount for dividend?");
                int dividendMinAmount = readInt("Min amount for dividend?");
                int divisorMaxAmount = readNonZeroInt("Max amount for divisor?");
                int divisorMinAmount = readNonZeroInt("Min amount for divisor?");
                int formulaAmount = readInt("How many formulas should be created?");

                for (int i = 0; i < formulaAmount; i++)
                {
                    int dividend;
                    int divisor;
                    do
                    {
                        dividend = rand.Next(dividendMinAmount, dividendMaxAmount);
                        divisor = rand.Next(divisorMinAmount, divisorMaxAmount);
                    } while (dividend % divisor == 0);
                    String s = String.Format(
                        "{0}r{1},{2} / {3} = _",
                        dividend / divisor,
                        dividend % divisor,
                        dividend,
                        divisor);
                    Console.WriteLine(s);
                    outputFile.WriteLine(s);
                }
                Console.WriteLine("Output to: " + ((FileStream)outputFile.BaseStream).Name);
            }
        }

        /// <summary>
        /// Helper method that prints a message to the screen and returns a string from the user.
        /// </summary>
        /// <param name="message">Message or prompt to be printed to the screen.</param>
        /// <returns>User input string value.</returns>
        private static string readLine(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        /// <summary>
        /// Helper method that prints a message to the screen and then returns
        /// a valid integer.  Will continue to ask for a proper int until one is entered.
        /// </summary>
        /// <param name="message">Message or prompt to be printed to the screen.</param>
        /// <returns>User input integer value.</returns>
        private static int readInt(string message)
        {
            int i;
            bool parsed = Int32.TryParse(readLine(message), out i);
            while (!parsed)
            {
                parsed = Int32.TryParse(readLine("Please enter a valid integer"), out i);
            }
            return i;
        }

        /// <summary>
        /// Helper method that ensures that the returned value is not a zero.
        /// </summary>
        /// <param name="message">Message or prompt to be printed to the screen.</param>
        /// <returns>User input integer value, non-zero.</returns>
        private static int readNonZeroInt(string message)
        {
            int nonZero = readInt(message);
            while (nonZero == 0)
            {
                Console.WriteLine("This value cannot equal zero, please enter a new value.");
                nonZero = readInt(message);
            }
            return nonZero;
        }

        /// <summary>
        /// Helper method that prints a message to the screen and then returns
        /// a valid double.  Will continue to ask for a proper double until one is entered.
        /// </summary>
        /// <param name="message">Message or prompt to be printed to the screen.</param>
        /// <returns>User input double value.</returns>
        private static double readDouble(string message)
        {
            double d;
            bool parsed = Double.TryParse(readLine(message), out d);
            while (!parsed)
            {
                parsed = Double.TryParse(readLine("Please enter a valid decimal"), out d);
            }
            return d;
        }
        /// <summary>
        /// Helper method that prints a message to the screen then returns
        /// a valid boolean.  Will continue to ask for a proper boolean until one is entered.
        /// </summary>
        /// <param name="message">Message or prompt to be printed to the screen.</param>
        /// <returns>User input true or false.</returns>
        private static bool readBool(string message)
        {
            bool b;
            bool parsed = Boolean.TryParse(readLine(message), out b);
            while (!parsed)
            {
                parsed = Boolean.TryParse(readLine("Please enter either true or false"), out b);
            }
            return b;
        }
    }
}
