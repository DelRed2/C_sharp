using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AdvancedCalculator {

    public interface ICalculatorable<in T1, out T2> {
        T2 Plus(T1 other);
        T2 Minus(T1 other);
        T2 Divide(T1 other);
        T2 Multiply(T1 other);
    }

    public interface INegativable<out T> {
        T Negative();
    }

    public static class Calculator<T1, T2>
        where T1 : class, ICalculatorable<T1, T1>, INegativable<T1>
        where T2 : class, ICalculatorable<T2, T2>, ICalculatorable<T1, T2>, INegativable<T2> {

        private static readonly char[] Functions = { '(', '+', '-', '/', '*', ')' };
        private static readonly string[] ExtraFunctions = { "neg" };

        private static void ErrorAction(string error) {
            throw new Exception(error);
        }

        private static void CalculateExtraFunction(string func, Stack<Object> operands, Stack<string> functions, bool fromStack, ref Object prevtoken, Object token = null) {
            Object temp = null;
            if (fromStack) {
                temp = operands.Pop();
            }

            T1 a = !fromStack ? token as T1 : temp as T1;
            T2 b = !fromStack ? token as T2 : temp as T2;

            switch (func) {

                case "neg": {
                        operands.Push((object)a == null ? (object)b.Negative() : a.Negative());
                        break;
                    }

            }

            functions.Pop();
            prevtoken = functions.Peek();
        }


        private static int GetPriority(string funct) {
            switch (funct) {
                case "(":
                    return -1;

                case "*":
                case "/":
                    return 2;

                case "+":
                case "-":
                    return 3;

                default:
                    ErrorAction("Error: Unknown character \"" + funct + "\"");
                    return 0;
            }
        }

        private static void PopFunction(Stack<Object> operands, Stack<string> functions) {
            if (operands.Count < 2) {
                ErrorAction("Error: Count of functions and operands does not coincide");
            }

            Object b = operands.Pop();
            Object a = operands.Pop();

            T1 b1 = b as T1;
            T2 b2 = b as T2;

            T1 a1 = a as T1;
            T2 a2 = a as T2;

            string c = functions.Pop();
            switch (c) {
                case "+":
                    if (a.GetType() == b.GetType()) {
                        if (a1 == null) operands.Push(a2.Plus(b2));
                        else operands.Push(a1.Plus(b1));
                    } else {
                        operands.Push(a2 == null ? b2.Plus(a1) : a2.Plus(b1));
                    }
                    break;

                case "-":
                    if (a.GetType() == b.GetType()) {
                        if (a1 == null) operands.Push(a2.Minus(b2));
                        else operands.Push(a1.Minus(b1));
                    } else {
                        operands.Push(a2 == null ? b2.Minus(a1).Negative() : a2.Minus(b1));
                    }
                    break;

                case "*":
                    if (a.GetType() == b.GetType()) {
                        if (a1 == null) operands.Push(a2.Multiply(b2));
                        else operands.Push(a1.Multiply(b1));
                    } else {
                        operands.Push(a2 == null ? b2.Multiply(a1) : a2.Multiply(b1));
                    }
                    break;

                case "/":
                    if (a.GetType() == b.GetType()) {
                        if (a1 == null) operands.Push(a2.Divide(b2));
                        else operands.Push(a1.Divide(b1));
                    } else {
                        if (a2 != null) operands.Push(a2.Divide(b1));
                        else throw new Exception("Bad division");
                    }
                    break;

                default:
                    ErrorAction("Error: Unknown function \"" + c + "\"");
                    break;

            }
        }

        private static bool CanPop(string op, Stack<string> functions) {
            if (functions.Count == 0) {
                return false;
            }

            if (ExtraFunctions.Contains(functions.Peek()) || ExtraFunctions.Contains(op)) {
                return false;
            }

            int prior1 = GetPriority(op);
            int prior2 = GetPriority(functions.Peek());

            return prior1 >= 0 && prior2 >= 0 && prior1 >= prior2;
        }

        private static object GetToken(string s, ref int ind, Func<string, T1> rT1, Func<string, T2> rT2,
            Func<string, T1> sT1, Func<string, T2> sT2, IDictionary<string, object> variables) {
            while (ind < s.Length && Char.IsWhiteSpace(s[ind])) ind++;

            if (ind == s.Length) return null;

            if (!Functions.Contains(s[ind])) {
                StringBuilder sb = new StringBuilder();
                bool wasQuotes = false;

                while (ind < s.Length && (!Functions.Contains(s[ind]) || wasQuotes)) {
                    if (!Char.IsWhiteSpace(s[ind]) && !s[ind].Equals('\'')) sb.Append(s[ind]);
                    if (s[ind].Equals('\'')) wasQuotes = !wasQuotes;
                    ind++;
                }

                if (wasQuotes) {
                    ErrorAction("Wrong quotes number");
                }

                string vrbl = sb.ToString();
                if (!ExtraFunctions.Contains(vrbl)) {
                    if (vrbl[0].Equals('$')) {
                        if (vrbl.Length == 1) {
                            ErrorAction("Empty name for variable");
                        }
                        if (variables.ContainsKey(vrbl)) {
                            return variables[vrbl];
                        }

                        object val = null;
                        if (Char.IsLower(vrbl[1])) {
                            if (rT1 != null) {
                                val = rT1(vrbl);
                            }
                        } else if (rT2 != null) {
                            val = rT2(vrbl);
                        }
                        
                        if (val == null) {
                            ErrorAction("Bad tooken initializer '" + vrbl + "'");
                        }
                        variables.Add(vrbl, val);
                        return val;
                    } else {
                        if (sT1 != null) {
                            T1 t1 = sT1(vrbl);
                            if (t1 != null) return t1;
                        }
                        if (sT2 != null) {
                            T2 t2 = sT2(vrbl);
                            if (t2 != null) return t2;
                        }
                        ErrorAction("Unknown tooken '" + vrbl + "'");
                    }
                } else {
                    return vrbl;
                }

            }
            return s[ind++].ToString(CultureInfo.InvariantCulture);
        }

        public static Object Calculate(String[] args, Func<string, T1> rT1, Func<string, T2> rT2,
            Func<string, T1> sT1 = null, Func<string, T2> sT2 = null, IDictionary<string, object> dict = null) {
            StringBuilder sb = new StringBuilder("(");

            foreach (string t in args) {
                sb.Append(t);
            }
            sb.Append(")");

            return Calculate(sb.ToString(), rT1, rT2, sT1, sT2, dict);
        }

        public static Object Calculate(String str, Func<string, T1> rT1, Func<string, T2> rT2,
            Func<string, T1> sT1 = null, Func<string, T2> sT2 = null, IDictionary<string, object> dict = null) {
            int ind = 0;
            var functions = new Stack<string>();
            var operands = new Stack<object>();

            IDictionary<string, object> variables = dict ?? new SortedDictionary<string, object>();

            if (str == null || str.Equals("")) return null;

            str = "(" + str + ")";
            object prevtoken = new object();
            object token = GetToken(str, ref ind, rT1, rT2, sT1, sT2, variables);
            bool unaryMinus = false;

            while (token != null) {

                if (token is string && prevtoken is string
                    && prevtoken.Equals("(") && token.Equals("-")) {
                    unaryMinus = true;
                }

                if (token is T1 || token is T2) {
                    if (prevtoken is string && ExtraFunctions.Contains((string)prevtoken)) {
                        CalculateExtraFunction((string)prevtoken, operands, functions, false, ref prevtoken, token);
                    } else {
                        operands.Push(token);
                    }
                } else if (token is string) {

                    if (((string)token).Equals(")")) {
                        while (functions.Count != 0 && functions.Peek() != "(") {
                            PopFunction(operands, functions);
                        }

                        if (functions.Count == 0) {
                            ErrorAction("Error: brackets don't coincide");
                        }
                        functions.Pop();

                        if (functions.Count > 0 && ExtraFunctions.Contains(functions.Peek())) {
                            CalculateExtraFunction(functions.Peek(), operands, functions, true, ref prevtoken);
                        }
                    } else {
                        bool doPop = true;

                        bool isMinus = token.Equals("-");
                        bool wasPlus = prevtoken.Equals("+");

                        bool wasDiv = prevtoken.Equals("/");
                        bool wasMul = prevtoken.Equals("*");

                        if (wasPlus && isMinus) {
                            functions.Pop();
                            prevtoken = functions.Peek();
                            token = "-";
                            doPop = false;
                        }

                        if ((wasDiv || wasMul || unaryMinus) && isMinus) {
                            token = "neg";
                            doPop = false;
                        }

                        if (doPop) {
                            while (CanPop((string)token, functions)) PopFunction(operands, functions);
                        }
                        functions.Push((string)token);
                    }
                }

                prevtoken = token;
                token = GetToken(str, ref ind, rT1, rT2, sT1, sT2, variables);
                unaryMinus = false;
            }

            if (operands.Count != 1) {
                ErrorAction("Error: Count of functions and operands does not coincide");
            }
            if (functions.Count != 0) {
                ErrorAction("Error: brackets don't coincide");
            }

            return operands.Pop();
        }

    }

}
