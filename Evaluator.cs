using System;
using System.Collections;
using System.Collections.Generic;
namespace ForthInterpreter
{
    class Evaluator
    {
        static Dictionary<string, string> functionMap = new Dictionary<string, string>();
        static Stack stack = new Stack();

        private static string getNextToken(string input, ref int pos)
        {
            string token = "";
            bool isInString = false;

            while (input.Length > pos && (input[pos] != ' ' || isInString))
            {
                if (input[pos] == '"') isInString = !isInString;
                token += input[pos];
                pos++;
            }
            pos++;

            return token;
        }

        public static void Eval(string input, out string output, out ArrayList errors)
        {
            errors = new ArrayList();
            output = "";
            int pos = 0;

            Stack oldStack = (Stack)stack.Clone();

            while (input.Length > pos)
            {

                int currentTokenPosition = pos;

                string currentToken = getNextToken(input, ref pos);

                int num;
                if (int.TryParse(currentToken, out num))
                {
                    stack.Push(num);
                }
                else if (currentToken == ".")
                {
                    try
                    {
                        output += stack.Peek().ToString() + " ";
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "CR")
                {
                    output += "\n";
                }
                else if (currentToken == "DUP")
                {
                    try
                    {
                        stack.Push(stack.Peek());
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "DROP")
                {
                    try
                    {
                        stack.Pop();
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "SWAP")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(a);
                        stack.Push(b);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "+")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b + a);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "-")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b - a);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "*")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b * a);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "/")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b / a);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "=")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b == a ? 1 : 0);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == ">")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b > a ? 1 : 0);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "<")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b < a ? 1 : 0);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == ">=")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b >= a ? 1 : 0);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "<=")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b <= a ? 1 : 0);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "<>")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(b != a ? 1 : 0);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "0>")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        stack.Push(0 > a ? 1 : 0);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "0<")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(0 < a ? 1 : 0);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "0=")
                {
                    try
                    {
                        int a = (int)stack.Pop();
                        int b = (int)stack.Pop();
                        stack.Push(0 == a ? 1 : 0);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "IF")
                {
                    bool b;
                    try
                    {
                        b = (int)stack.Pop() != 0;
                        if (!b)
                        {
                            string tempToken = getNextToken(input, ref pos);

                            while (tempToken != "ELSE" && tempToken != "THEN")
                            {
                                tempToken = getNextToken(input, ref pos);
                            }
                        }
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                    }
                }
                else if (currentToken == "ELSE")
                {
                    string tempToken = getNextToken(input, ref pos);
                    while (tempToken != "THEN")
                    {
                        tempToken = getNextToken(input, ref pos);
                    }
                }
                else if (currentToken == "THEN")
                {
                    // do nothing
                }
                else if (currentToken == ":") // Function definition
                {
                    string functionName = getNextToken(input, ref pos);

                    string functionBody = "";
                    try
                    {
                        string tempToken = getNextToken(input, ref pos);
                        while (tempToken != ";")
                        {
                            if (pos >= input.Length) throw new Exception("No end to function");
                            functionBody += tempToken + " ";
                            tempToken = getNextToken(input, ref pos);
                        }
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.SYNTAX_ERROR, currentTokenPosition));
                        continue;
                    }

                    functionMap.Add(functionName, functionBody);
                    Console.Write($"Function definition for '{functionName}' created. ");
                }
                else
                {
                    if (currentToken[0] == '.' && currentToken[1] == '"' && currentToken[currentToken.Length - 1] == '"')
                    {
                        for (int i = 2; i < currentToken.Length - 1; i++)
                        {
                            output += currentToken[i];
                        }
                        continue;
                    }

                    try
                    {
                        Eval(functionMap[currentToken], out output, out errors);
                    }
                    catch
                    {
                        errors.Add(new Error(ErrorType.UNKNOWN_SYMBOL, currentTokenPosition, currentToken));
                    }
                }
            }

            if (errors.Capacity != 0)
            {
                stack = oldStack;
            }
        }
    }
}
