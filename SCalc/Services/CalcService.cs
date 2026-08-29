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
  // private List<string> trimedUserPrompt = new List<string>();
  private List<Token> _numberTokenList = new List<Token>();
  private List<Token> _operationTokenList = new List<Token>();

  private bool isOperation(string operation)
  {
    bool result = operation switch
    {
      "+" => true,
      "-" => true,
      "*" => true,
      "/" => true,
      _ => false
    };

    return result;
  }
  
  private void CreateTokenLists(string userPrompt)
  {
    _numberTokenList = new List<Token>();
    _operationTokenList = new List<Token>();

    string trimedUserPrompt = userPrompt.Replace(" ", ""); 
    string tempNewTokenRawValue = trimedUserPrompt[0].ToString();

    for (int i = 1; i < trimedUserPrompt.Length; i++)
    {
      if (isOperation(trimedUserPrompt[i].ToString()) && !isOperation(trimedUserPrompt[i-1].ToString()))
      { 
        _numberTokenList.Add(new Token {
          rawValue = tempNewTokenRawValue.ToString(), 
          numberValue = tempNewTokenRawValue.ToDouble(),
          type = "NUMBER"
        });

        tempNewTokenRawValue = "";

        _operationTokenList.Add(new Token {
          rawValue = trimedUserPrompt[i].ToString(),
          type = "OPERATION"
        });
      } else {
        tempNewTokenRawValue += trimedUserPrompt[i]; 
      }
 
      if (i == trimedUserPrompt.Length - 1)
      {
        _numberTokenList.Add(new Token {
          rawValue = tempNewTokenRawValue.ToString(), 
          numberValue = tempNewTokenRawValue.ToDouble(),
          type = "NUMBER"
        });
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

  public void Calc(string userPrompt)
  {
    CreateTokenLists(userPrompt); 

    calcResult = _numberTokenList[0].numberValue;

    for (int i = 0; i < _operationTokenList.Count; i++)
    {
      switch (_operationTokenList[i].rawValue)
      {
        case "+":
          calcResult += _numberTokenList[i+1].numberValue; 
        break;
        case "-":
          calcResult -= _numberTokenList[i+1].numberValue; 
        break;
        case "*":
          calcResult *= _numberTokenList[i+1].numberValue; 
        break;
        case "/":
          calcResult /= _numberTokenList[i+1].numberValue; 
        break;
      } 
    }
  }

  public double GetCalcResult()
  {
    return calcResult;
  }
}
