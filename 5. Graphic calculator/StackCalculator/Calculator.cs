using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackCalculator {

    public static class Calculator {

        private static readonly char[] Functions = { '(', '^', '+', '-', '/', '*', ')' };
        private static readonly string[] ExtraFunctions = { "neg", "sin", "cos", "tg", "ctg", "ln", "abs" };

        public delegate string GetVariable(string variable);

        private static void ErrorAction(string error) {
            throw new Exception(error);
        }

        private static void CalculateExtraFunction(string func, Stack<double> operands, Stack<string> functions, bool fromStack, ref Object prevtoken, double token = 0) {
            double temp = 0;
            if (fromStack) {
                temp = operands.Pop();
            }

            switch (func) {

                case "neg": {
                    operands.Push(!fromStack ? (-1) * token : (-1) * temp);
                    break;
                }

                case "sin": {
                    operands.Push(!fromStack ? Math.Sin(token) : Math.Sin(temp));
                    break;
                }

                case "cos": {
                    operands.Push(!fromStack ? Math.Cos(token) : Math.Cos(temp));
                    break;
                }

                case "tg": {
                    operands.Push(!fromStack ? Math.Tan(token) : Math.Tan(temp));
                    break;
                }

                case "ctg": {
                    operands.Push(!fromStack ? 1/Math.Tan(token) : 1/Math.Tan(temp));
                    break;
                }

                case "ln": {
                    operands.Push(!fromStack ? Math.Log(token) : Math.Log(temp));
                    break;
                }

                case "abs": {
                    operands.Push(!fromStack ? Math.Abs(token) : Math.Abs(temp));
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

                case "^":
                    return 1;

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

        private static void PopFunction(Stack<double> operands, Stack<string> functions) {
            if (operands.Count < 2) {
                ErrorAction("Error: Count of functions and operands does not coincide");
            }

            double b = operands.Pop();
            double a = operands.Pop();

            string c = functions.Pop();
            switch (c) {
                case "+":
                    operands.Push(checked(a + b));
                    break;

                case "-":
                    operands.Push(checked(a - b));
                    break;

                case "*":
                    operands.Push(checked(a * b));
                    break;

                case "/":
                    operands.Push(checked(a / b));
                    break;

                case "^":
                    operands.Push(checked(Math.Pow(a, b)));
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
        
        private static double ReadDouble(string s, ref int ind) {
            int begin = ind;
            int length = 0;

            while (ind < s.Length && (char.IsDigit(s[ind]) || s[ind].Equals(',') || s[ind].Equals('.')))
            {
                ind++;
                length++;
            }
            string strToDouble = s.Substring(begin, length).Replace('.', ',');
            try {
                return Double.Parse(strToDouble);
            } catch {
                ErrorAction("Error: bad value '" + strToDouble + "'");
            }
            return 0;
        }

        private static Object GetToken(string s, ref int ind, IDictionary<string, double> variables, GetVariable getVar) {
            while (ind < s.Length && Char.IsWhiteSpace(s[ind])) ind++;
            
            if (ind == s.Length) return null;

            if (Char.IsDigit(s[ind])) {
                return ReadDouble(s, ref ind);
            }

            if (!Functions.Contains(s[ind])) {
                StringBuilder sb = new StringBuilder();
                sb.Append(s[ind++]);

                while (!Functions.Contains(s[ind]) && (Char.IsLetter(s[ind]) || Char.IsDigit(s[ind])) || Char.IsWhiteSpace(s[ind])) {
                    if (!Char.IsWhiteSpace(s[ind])) sb.Append(s[ind++]);
                    else ind++;
                }

                if (!Char.IsLetter(s[ind]) && !Char.IsDigit(s[ind]) && !Functions.Contains(s[ind])) {
                    ErrorAction("Unknown character '" + s[ind] + "'");
                }

                string vrbl = sb.ToString();
                if (!ExtraFunctions.Contains(vrbl)) {
                    if (Char.IsLetter(vrbl[0])) {
                        if (variables.ContainsKey(vrbl)) {
                            return variables[vrbl];
                        }

                        if (vrbl == "pi") {
                            return Math.PI;
                        }
                        if (vrbl == "e") {
                            return Math.E;
                        }

                        if (getVar != null) {
                            double val = Calculate("(" + getVar(vrbl) + ")", variables, getVar);
                            if (!variables.ContainsKey(vrbl)) {
                                variables.Add(vrbl, val);
                            } else {
                                ErrorAction("Error: incorrect initialisation of variable '" + vrbl + "'");
                            }
                            return val;
                        }
                        ErrorAction("Error: uninitialised variable");
                    } else {
                        ErrorAction("Unknown tooken '" + vrbl + "'");
                    }
                } else {
                    return vrbl;
                }

            }
            return s[ind++].ToString();
        }

        public static double Calculate(String[] args, GetVariable getVar) {
            return Calculate(args, null, getVar);
        }

        public static double Calculate(String str, GetVariable getVar) {
            return Calculate(str, null, getVar);
        }

        public static double Calculate(String[] args, IDictionary<string, double> dict = null, GetVariable getVar = null) {
            StringBuilder sb = new StringBuilder("(");

            foreach (string t in args) {
                sb.Append(t);
            }
            sb.Append(")");

            return Calculate(sb.ToString(), dict, getVar);
        }

        public static double Calculate(String str, IDictionary<string, double> dict = null, GetVariable getVar = null) {
            int ind = 0;
            var functions = new Stack<string>();
            var operands = new Stack<double>();

            IDictionary<string, double> variables = dict ?? new SortedDictionary<string, double>();

            if (str == null || str.Equals("")) return 0;

            str = "(" + str + ")";
            Object prevtoken = new Object();
            Object token = GetToken(str, ref ind, variables, getVar);
            bool unaryMinus = false;

            while (token != null) {

                if (token is string && prevtoken is string
                    && prevtoken.Equals("(") && (token.Equals("+") || token.Equals("-"))) {
                    unaryMinus = true;
                }

                if (token is double) {
                    if (prevtoken is string && ExtraFunctions.Contains((string) prevtoken)) {
                        CalculateExtraFunction((string) prevtoken, operands, functions, false, ref prevtoken, (double) token);
                    } else {
                        operands.Push((double)token);
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
                        bool wasExp = prevtoken.Equals("^");

                        if (wasPlus && isMinus) {
                            functions.Pop();
                            prevtoken = functions.Peek();
                            token = "-";
                            doPop = false;
                        }

                        if ((wasDiv || wasMul || wasExp || unaryMinus) && isMinus) {
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
                token = GetToken(str, ref ind, variables, getVar);
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
