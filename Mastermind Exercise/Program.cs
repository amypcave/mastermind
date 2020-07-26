using System;

namespace Mastermind_Exercise
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(Figgle.FiggleFonts.Speed.Render("Mastermind"));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to Mastermind! Do you have the smarts to break the code??");
            Console.WriteLine();
            Console.WriteLine("GAME PLAY: You have 10 attempts to guess a secret 4 digit code where each digit is between 1-6.");
            Console.WriteLine("           If you have the correct digit in the correct place a plus sign (+) will be returned.");
            Console.WriteLine("           If you have the correct digit in an incorrect place a minus sign (-) will be returned.");
            Console.WriteLine("           If you do not have a matching digit nothing will be returned.");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();

            //create 4 digit random number that the player is to guess:
            Random rnd = new Random();
            int a = rnd.Next(1, 7);
            int b = rnd.Next(1, 7);
            int c = rnd.Next(1, 7);
            int d = rnd.Next(1, 7);

            int randomNumber = (a * 1000) + (b * 100) + (c * 10) + d;
           
            Console.WriteLine("Please guess a 4 digit number with each digit between 1-6");
            try
            {
                //create a loop to accommodate 10 guesses
                for (int i = 1; i <= 10; i++)
                {

                    if (i == 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Final Guess!");
                        Console.ResetColor();
                    }

                    //Get the guess from the user
                    Console.Write($"Guess #{i}: ");
                    int guessedNumber = int.Parse(Console.ReadLine());

                    //ensure the guess is valid
                    if (guessedNumber / 1000 >= 10 || guessedNumber < 0)
                    {
                        Console.WriteLine("Please enter a positive four-digit number. Try again.");
                        i--;
                        continue;
                    }
                    int[] arr = SeparateNumberIntoDigits(guessedNumber);
                    if (arr[0] > 6 || arr[1] > 6 || arr[2] > 6 || arr[3] > 6
                       || arr[0] < 1 || arr[1] < 1 || arr[2] < 1 || arr[3] < 1)
                    {
                        Console.WriteLine("Please enter only digits from 1-6. Try again.");
                        i--;
                        continue;
                    }

                    //return appropriate amount of + and -
                    string response = FinalResponse(guessedNumber, randomNumber);
                    if (response == "++++")
                    {
                        Console.WriteLine($"Congratulations! You guessed the secret number, {randomNumber} !");
                        break;
                    }
                    else
                    {
                        Console.WriteLine(response);
                        if (i < 10)
                        {
                            Console.WriteLine("Not quite. Try another number");
                            Console.WriteLine();
                        }
                        else if (i == 10)
                        {
                            Console.WriteLine($"Close, but no cigar!! The secret number was {randomNumber}");
                            break;
                        }

                    }
                }
                
            }
            
            catch (FormatException)
            {
                Console.WriteLine("An error occured parsing the input.");
            }

            //separate out each digit of a 4 digit number
            int[] SeparateNumberIntoDigits(int num)
            {
                int[] arr = new int[4];
                arr[0] = num / 1000;
                arr[1] = (num / 100) % 10;
                arr[2] = (num / 10) % 10;
                arr[3] = num % 10;

                return arr;
            }

            //Create method for returning + when the correct digit is in the right spot
            string CorrectDigitCorrectSpot(int guessedNumber, int realNumber)
            {
                int[] a = SeparateNumberIntoDigits(guessedNumber);
                int[] b = SeparateNumberIntoDigits(realNumber);
                string plusses = "";

                if (a[0] == b[0])
                {
                    plusses += "+";
                }
                if (a[1] == b[1])
                {
                    plusses += "+";
                }
                if (a[2] == b[2])
                {
                    plusses += "+";
                }
                if (a[3] == b[3])
                {
                    plusses += "+";
                }
                return plusses;
            }

            //create a method for returning - if the correct digit is in the incorrect spot
            string CorrectDigitWrongSpot(int guessedNumber, int realNumber)
            {
                int[] a = SeparateNumberIntoDigits(guessedNumber);
                int[] b = SeparateNumberIntoDigits(realNumber);
                string minuses = "";

                if (a[0] != b[0])
                {
                    if (a[1] == b[0] || a[2] == b[0] || a[3] == b[0])
                    {
                        minuses += "-";
                    }
                }
                if (a[1] != b[1])
                {
                    if (a[0] == b[1] || a[2] == b[1] || a[3] == b[1])
                    {
                        minuses += "-";
                    }
                }
                if (a[2] != b[2])
                {
                    if (a[0] == b[2] || a[1] == b[2] || a[3] == b[2])
                    {
                        minuses += "-";
                    }
                }
                if (a[3] != b[3])
                {
                    if (a[0] == b[3] || a[1] == b[3] || a[2] == b[3])
                    {
                        minuses += "-";
                    }
                }
                return minuses;
            }

            //paste the + and - responses together
            string FinalResponse(int guessedNumber, int realNumber)
            {
                string final = CorrectDigitCorrectSpot(guessedNumber, realNumber) + CorrectDigitWrongSpot(guessedNumber, realNumber);
                return final;
            }
        }
    }
}
