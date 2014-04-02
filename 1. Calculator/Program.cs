using System;
using System.Linq;

namespace StackCalculator {

    public class Program {

        private static string InputVariable(string variable) {
            Console.Write("Input expression for variable '{0}': ", variable);
            return Console.ReadLine();
        }

        public static void Main(String[] args) {
            if (args.Any()) {
                Console.WriteLine("Result =" + Calculator.Calculate(args, InputVariable));
            } else {
                while (true) {
                    try {
                        Console.Write("Input: ");
                        string str = Console.ReadLine();
                        if (str == "exit") {
                            break;
                        }
                        Console.WriteLine("Result = " + Calculator.Calculate(str, InputVariable));
                    } catch (Exception expt) {
                        Console.WriteLine(expt.Message);
                    }
                }
            }
        }

    }


}
