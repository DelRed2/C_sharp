using System;
using AdvancedCalculator;
using CalculatorTypes;

namespace Program {

    public partial class Program {

        public static void Main(String[] args) {
            bool workWithMatrix = false;
            Console.WriteLine("To work with Calculator<Complex, Polinom<Complex>> input 'polinom'");
            Console.WriteLine("To work with Calculator<Complex, Matrix<Complex>> input 'matrix'");
            Console.WriteLine("To exit input 'exit'");
            Console.WriteLine();

            while (true) {
                try {
                    Console.Write("Input " + (workWithMatrix?"matrix: ":"polinom: "));
                    string str = Console.ReadLine();

                    if (str.Equals("exit")) { break; }

                    if (str.Equals("matrix")) {
                        workWithMatrix = true;
                        Console.Write("Input matrix: ");
                        str = Console.ReadLine();
                    } else if (str.Equals("polinom")) {
                        workWithMatrix = false;
                        Console.Write("Input polinom: ");
                        str = Console.ReadLine();
                    }

                    if (!workWithMatrix) {
                        var res = Calculator<Complex, Polinom<Complex>>.Calculate(str, readComplex, readComplexPolinom, strToComplex, strToComplexPolinom);
                        Console.WriteLine("Result: ");
                        Console.WriteLine(res);
                    } else {
                        var res2 = Calculator<Complex, Matrix<Complex>>.Calculate(str, readComplex, readComplexMatrix, strToComplex, strToComplexMatrix);
                        Console.WriteLine("Result: ");
                        Console.WriteLine(res2);
                        var mtr = res2 as Matrix<Complex>;
                        if (mtr != null && mtr.height == mtr.width) {
                            Console.WriteLine("det = "+ mtr.Determinant());
                            Console.WriteLine("trace = " + mtr.Trace());
                        }
                    }
                } catch (Exception expt) {
                    Console.WriteLine(expt.Message);
                }
                Console.WriteLine();
            }
        }

    }

}