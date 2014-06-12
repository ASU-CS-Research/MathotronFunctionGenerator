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
        private static readonly string MENU = 
            "=================================\n"
                + "Which grade level would you like?\n\n"
                + " 0:  PreK (Counting)\n"
                + " 1:  1st Grade (Integer Addition)\n"
                + " 2:  2nd Grade (Integer Multiplication)\n"
                + " 3:  3rd Grade (Integer Division)\n"
                + "-1:  Exit Program\n"
                + "=================================";

        /// <summary>
        /// Intital beginning point of the program.
        /// Allows the user to select an option from a menu.
        /// </summary>
        /// <param name="args">Command Line Arguments (Unused)</param>
        public static void Main(string[] args)
        {
            bool exit = false;
            int grade;
            Console.WriteLine("This program outputs generated text for Mathotron to the console."); 
            while (!exit)
            {
                grade = readInt(MENU);
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
                    case -1:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option selected.");
                        break;
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
            }             
        }

        /// <summary>
        /// Method that generates text output that works with the First Grade selection in Mathotron.
        /// 
        /// output format:  (i+j), i + j = _
        /// Example:        7,2 + 5 = _
        /// </summary>
        private static void GenFirstGrade()
        {
            using (StreamWriter outputFile = new StreamWriter("FirstGrade.functions"))
            {
                int startValue = readInt("Starting value?");
                int stopValue = readInt("Stopping value? (Must be greater than or equal to start value");
                int maxAdden = readInt("Max adden / subtrahend? (EX: If  5+10, max would be 10");
                for (int i = startValue; i <= stopValue; i++)
                {
                    for (int j = 0; j <= maxAdden; j++)
                    {
                        Console.WriteLine(String.Format("{0},{1} + {2} = _", i + j, i, j));
                        outputFile.WriteLine(String.Format("{0},{1} + {2} = _", i + j, i, j));
                    }
                }
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
            }
             
        }

        /// <summary>
        /// Helper method that prints a message to the screen and then returns
        /// a valid integer.  Will continue to ask for a proper int until was is entered.
        /// </summary>
        /// <param name="message">Message or prompt to be printed to the screen.</param>
        /// <returns></returns>
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
        /// Helper method that prints a message to the screen and returns a string from the user.
        /// </summary>
        /// <param name="message">Message or prompt to be printed to the screen.</param>
        /// <returns></returns>
        private static string readLine(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
