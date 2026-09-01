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
    private double calcResult;

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
                new Token { RawValue = tempNewTokenRawValue, Type = TokenType.Parenthesis }
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
                            RawValue = tempNewTokenRawValue,
                            NumberValue = Convert.ToDouble(tempNewTokenRawValue),
                            Type = TokenType.Number,
                        }
                    );
                    tempNewTokenRawValue = "";
                }

                if (currentSymbol == "(" || currentSymbol == ")")
                {
                    _tokenList.Add(
                        new Token { RawValue = currentSymbol, Type = TokenType.Parenthesis }
                    );
                }
                else
                {
                    _tokenList.Add(
                        new Token { RawValue = currentSymbol, Type = TokenType.Operator }
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
                            RawValue = tempNewTokenRawValue.ToString(),
                            NumberValue = tempNewTokenRawValue.ToDouble(),
                            Type = TokenType.Number,
                        }
                    );
                }
            }
        }
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
            if (token.Type == TokenType.Number)
            {
                numbers.Push(token.NumberValue);
            }
            else if (token.RawValue == "(")
            {
                operators.Push(token.RawValue);
            }
            else if (token.RawValue == ")")
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
                    && GetPriority(operators.Peek()) >= GetPriority(token.RawValue)
                )
                {
                    ExecuteTopOperator(numbers, operators);
                }
                operators.Push(token.RawValue);
            }
        }

        while (operators.Count > 0)
        {
            ExecuteTopOperator(numbers, operators);
        }

        calcResult = numbers.Count > 0 ? numbers.Pop() : 0;
    }

    public double? GetCalcResult()
    {
        return calcResult;
    }
}
