using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AdvancedCalculator;
using CalculatorTypes;

namespace Program {

    public partial class Program {

        private static readonly NumberFormatInfo format = new NumberFormatInfo {
            NumberDecimalSeparator = ".",
            NumberGroupSeparator = "."
        };

        public static T[] readSome<T>(int count, Func<string, T> cnv) {
            int ready = 0;
            T[] res = new T[count];
            while (ready < count) {
                string str = Console.ReadLine();
                string[] strSplit = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int to = Math.Min(strSplit.Length, count);
                for (int i = 0; i < to; ++i) { res[ready + i] = cnv(strSplit[i]); }
                ready += strSplit.Length;
            }
            return res;
        }

        public static T stringTo<T>(string str) where T : class {
            if (typeof(T) == typeof(Complex)) {
                return stringToComplex(str) as T;
            }
            if (typeof(T) == typeof(Real)) {
                return strToReal(str) as T;
            }
            return null;
        }

        public static Complex stringToComplex(string s) {
            return (Complex)Calculator<Complex, Complex>.Calculate(s, null, null, strToComplex);
        }

        //String to simple-types
        public static Real strToReal(string str) {
            try {
                double tmp = Convert.ToDouble(str, format);
                return new Real(tmp);
            } catch (Exception) {
                return null;
            }
        }

        public static Complex strToComplex(string str) {
            try {
                if (str.Equals("i")) {
                    return new Complex(0, 1);
                }
                double tmp = Convert.ToDouble(str, format);
                return new Complex(tmp);
            } catch (Exception) {
                return null;
            }
        }

        //Read simple-types
        public static Real readReal(string name) {
            Console.Write("Input Real " + name + ": ");
            return new Real(Convert.ToDouble(Console.ReadLine(), format));
        }

        public static Complex readComplex(string name) {
            Console.Write("Input Complex " + name + ": ");
            string s = Console.ReadLine();
            return stringToComplex(s);
        }

        //Read Matrix
        public static T[,] readMatrix<T>(string name, Func<string, T> convert) {
            Console.Write("Input Matrix '" + name + "' size: ");
            int[] prm = readSome(2, Convert.ToInt32);
            int a = prm[0];
            int b = prm[1];
            Console.WriteLine("Input Matrix:");
            T[] matrix = readSome(a * b, convert);
            int current = 0;
            T[,] mtr = new T[a, b];
            for (int i = 0; i < a; ++i) {
                for (int j = 0; j < b; ++j) {
                    mtr[i, j] = matrix[current++];
                }
            }
            return mtr;
        }

        public static Matrix<Complex> readComplexMatrix(string name) {
            return new Matrix<Complex>(readMatrix(name, stringToComplex));
        }

        public static Matrix<Real> readRealMatrix(string name) {
            return new Matrix<Real>(readMatrix(name, strToReal));
        }

        //Read polinom
        public static Polinom<Real> readRealPolinom(string name) {
            Console.Write("Input Polinom '" + name + "' size: ");
            Console.Write("Input coefficients: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Real[] coeff = readSome(size, strToReal);
            return new Polinom<Real>(coeff);
        }

        public static Polinom<Complex> readComplexPolinom(string name) {
            Console.Write("Input Polinom '" + name + "' size: ");
            Console.Write("Input coefficients: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Complex[] coeff = readSome(size, stringToComplex);
            return new Polinom<Complex>(coeff);
        }

        //string to polinom
        public static Polinom<Real> strToRealPolinom(string str) {
            try {
                if (str.Equals("x")) {
                    return new Polinom<Real>(new[] { new Real(0), new Real(1) });
                }
                int tmp = Convert.ToInt32(str.Substring(2));
                Real[] arr = new Real[tmp + 1];
                for (int i = 0; i < tmp; ++i) { arr[i] = new Real(0); }
                arr[tmp] = new Real(1);
                return new Polinom<Real>(arr);
            } catch (Exception) {
                return null;
            }
        }

        public static Polinom<Complex> strToComplexPolinom(string str) {
            try {
                if (str.Equals("x")) {
                    return new Polinom<Complex>(new[] { new Complex(0), new Complex(1) });
                }
                int tmp = Convert.ToInt32(str.Substring(2));
                Complex[] arr = new Complex[tmp + 1];
                for (int i = 0; i < tmp; ++i) { arr[i] = new Complex(0); }
                arr[tmp] = new Complex(1);
                return new Polinom<Complex>(arr);
            } catch (Exception) {
                return null;
            }
        }

        //string to matrix
        public static T[,] strToMatrix<T>(string str) where T : class {
            const string pattern = @"\{\s*(?:\{\s*([^{}]+)\s*\},\s*)*\{\s*([^{}]+)\s*\}\s*\}";
            T[,] arr = null;
            int width = 0;
            int line = -2;

            Regex regex = new Regex(pattern);
            Match match = regex.Match(str);

            int height = match.Groups[1].Captures.Count + 1;

            while (match.Success) {
                foreach (Group g in match.Groups) {
                    foreach (Capture c in g.Captures) {
                        if (++line == -1) continue;

                        string[] split = c.Value.Split(',');
                        if (line == 0) {
                            width = split.Count();
                            arr = new T[height, width];
                        } else if (split.Count() != width) {
                            throw new Exception("Wrong line in matrix");
                        }

                        for (int i = 0; i < width; ++i) {
                            arr[line, i] = stringTo<T>(split[i]);
                        }
                    }
                }
                match = match.NextMatch();
            }
            return arr;
        }

        public static Matrix<Complex> strToComplexMatrix(string str) {
            Complex[,] arr = strToMatrix<Complex>(str);
            return arr == null ? null : new Matrix<Complex>(arr);
        }

        public static Matrix<Real> strToRealMatrix(string str) {
            Real[,] arr = strToMatrix<Real>(str);
            return arr == null ? null : new Matrix<Real>(arr);
        }
    }
}
