using System;
using System.Collections;

namespace ForthInterpreter
{
    class Forth
    {
        static void Main(string[] args)
        {
            Console.WriteLine("JFORTH INTERPRETER VERSION 1.0");

            while (true)
            {
                ArrayList errors;
                string output;

                Console.Write(">>> ");
                string userInput = Console.ReadLine();
                if(userInput == "EXIT") break;

                Evaluator.Eval(userInput, out output, out errors);

                if(errors.Capacity != 0)
                {
                    foreach(Error e in errors)
                    {
                        Console.WriteLine(e.prettyPrint());
                    }

                    Console.WriteLine(":(");
                    continue;
                }

                Console.Write(output);
                Console.WriteLine(":)");
            }
        }
    }
}
