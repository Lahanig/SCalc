using System.Globalization;
using SCalc.Interfaces;
using SCalc.Models;

namespace SCalc.Services;

public static class StringExtensions
{
    public static double ToDouble(this string value)
    {
        string cleanValue = value.Replace(',', '.');

        if (double.TryParse(cleanValue, CultureInfo.InvariantCulture, out double result))
        {
            return result;
        }

        return 0;
    }
}

public class CalcService : ICalcService
{
    private double calcResult = 0;

    private List<Token> _tokenList = new List<Token>();

    private bool isOperation(string operation)
    {
        bool result = operation switch
        {
            "+" => true,
            "-" => true,
            "*" => true,
            "/" => true,
            _ => false,
        };

        return result;
    }

    private int GetPriority(string op)
    {
        int result = op switch
        {
            "+" => 1,
            "-" => 1,
            "*" => 2,
            "/" => 2,
            _ => 0,
        };

        return result;
    }

    private void CreateCombinedTokenList(string userPrompt)
    {
        _tokenList = new List<Token>();

        string trimedUserPrompt = userPrompt.Replace(" ", "");
        string tempNewTokenRawValue = trimedUserPrompt[0].ToString();

        if (tempNewTokenRawValue == "(")
        {
            _tokenList.Add(
                new Token { rawValue = tempNewTokenRawValue, type = TokenType.Parenthesis }
            );
            tempNewTokenRawValue = "";
        }

        for (int i = 1; i < trimedUserPrompt.Length; i++)
        {
            string currentSymbol = trimedUserPrompt[i].ToString();

            bool isUnaryMinus =
                currentSymbol == "-"
                && (
                    i == 0
                    || isOperation(trimedUserPrompt[i - 1].ToString())
                    || trimedUserPrompt[i - 1] == '('
                );

            if (isUnaryMinus)
            {
                tempNewTokenRawValue += currentSymbol;
                continue;
            }

            if (
                isOperation(currentSymbol)
                // && !isOperation(trimedUserPrompt[i - 1].ToString())
                || currentSymbol == "("
                || currentSymbol == ")"
            )
            {
                if (!string.IsNullOrEmpty(tempNewTokenRawValue) && tempNewTokenRawValue != "-")
                {
                    _tokenList.Add(
                        new Token
                        {
                            rawValue = tempNewTokenRawValue,
                            numberValue = Convert.ToDouble(tempNewTokenRawValue),
                            type = TokenType.Number,
                        }
                    );
                    tempNewTokenRawValue = "";
                }

                if (currentSymbol == "(" || currentSymbol == ")")
                {
                    _tokenList.Add(
                        new Token { rawValue = currentSymbol, type = TokenType.Parenthesis }
                    );
                }
                else
                {
                    _tokenList.Add(
                        new Token { rawValue = currentSymbol, type = TokenType.Operator }
                    );
                }
            }
            else
            {
                tempNewTokenRawValue += currentSymbol;
            }

            if (i == trimedUserPrompt.Length - 1)
            {
                if (tempNewTokenRawValue != "")
                {
                    _tokenList.Add(
                        new Token
                        {
                            rawValue = tempNewTokenRawValue.ToString(),
                            numberValue = tempNewTokenRawValue.ToDouble(),
                            type = TokenType.Number,
                        }
                    );
                }
            }
        }

        // Debug
        // foreach (Token token in _numberTokenList)
        // {
        //   Console.WriteLine($"n: {token.numberValue}");
        // }
        //
        // foreach (Token token in _operationTokenList)
        // {
        //   Console.WriteLine($"o: {token.rawValue}");
        // }
    }

    private void ExecuteTopOperator(Stack<double> numbers, Stack<string> operators)
    {
        if (operators.Count == 0)
            return;

        string op = operators.Pop();

        if (numbers.Count < 2)
            return;

        double r = numbers.Pop();
        double l = numbers.Pop();

        switch (op)
        {
            case "+":
                numbers.Push(l + r);
                break;
            case "-":
                numbers.Push(l - r);
                break;
            case "*":
                numbers.Push(l * r);
                break;
            case "/":

                if (r == 0)
                    numbers.Push(0);
                else
                    numbers.Push(l / r);
                break;
        }
    }

    public void Calc(string userPrompt)
    {
        CreateCombinedTokenList(userPrompt);

        List<Token> tokens = _tokenList;

        Stack<double> numbers = new Stack<double>();
        Stack<string> operators = new Stack<string>();

        // Debug
        // foreach (Token token in tokens)
        // {
        //     Console.WriteLine($"token: {token.rawValue}");
        // }

        foreach (Token token in tokens)
        {
            if (token.type == TokenType.Number)
            {
                numbers.Push(token.numberValue);
            }
            else if (token.rawValue == "(")
            {
                operators.Push(token.rawValue);
            }
            else if (token.rawValue == ")")
            {
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    ExecuteTopOperator(numbers, operators);
                }
                if (operators.Count > 0)
                {
                    operators.Pop();
                }
            }
            else
            {
                while (
                    operators.Count > 0
                    && GetPriority(operators.Peek()) >= GetPriority(token.rawValue)
                )
                {
                    Console.WriteLine(operators.Peek(), token.rawValue);
                    ExecuteTopOperator(numbers, operators);
                }
                operators.Push(token.rawValue);
            }
        }

        while (operators.Count > 0)
        {
            ExecuteTopOperator(numbers, operators);
        }

        calcResult = numbers.Count > 0 ? numbers.Pop() : 0;
    }

    public double GetCalcResult()
    {
        return calcResult;
    }
}
