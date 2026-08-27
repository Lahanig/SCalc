using SCalc.Interfaces;

namespace SCalc;

public class App
{
  private readonly ICalcService _calcService;
  private string StartupText = "Simple Calc by Kaibi";
  private string TipText = "Enter expression";
  private string InputSymbol = "$";

  public App(ICalcService calcService) 
  {
    _calcService = calcService;
  }

  private void ShowCalcResult(string userExpression)
  {
    _calcService.Calc(userExpression);
    Console.WriteLine($"Result: {_calcService.getCalcResult()}");
  }

  public void Run()
  {
    Console.Clear();
   
    Console.WriteLine($"{StartupText}"); 

    bool isRunning = true;
    while (isRunning)
    { 
      Console.WriteLine($"\n{TipText}\n");
      Console.Write($"{InputSymbol} ");
      
      string? input = Console.ReadLine();

      if (input?.ToLower() == "exit") 
      {
        isRunning = false;
        Console.WriteLine("Exit...");
      } else {
        ShowCalcResult(input?.ToLower());
      }
    } 
  }
}
