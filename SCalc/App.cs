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

    private void ShowCalcResult(string userPrompt)
    {
        _calcService.Calc(userPrompt);
        Console.WriteLine($"Result: {_calcService.GetCalcResult()}\n");
    }

    public void Run()
    {
        Console.Clear();

        Console.WriteLine($"{StartupText}");
        Console.WriteLine($"\n{TipText}\n");

        bool isRunning = true;
        while (isRunning)
        {
            // Console.WriteLine($"\n{TipText}\n");
            Console.Write($"{InputSymbol} ");

            string? input = Console.ReadLine();

            if (input?.ToLower() == "exit")
            {
                isRunning = false;
                Console.WriteLine("Exit...");
            }
            else
            {
                if (input != null && input != "")
                {
                    ShowCalcResult(input.ToLower());
                }
            }
        }
    }
}
