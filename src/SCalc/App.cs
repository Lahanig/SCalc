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
        if (!char.IsDigit(userPrompt[0]))
            return;

        _calcService.Calc(userPrompt);

        double calcResult = _calcService.GetCalcResult();

        Console.WriteLine($"Result: {calcResult}");
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
                if (input != null && input.Length > 0 && input.Trim() != "")
                {
                    ShowCalcResult(input.ToLower().Trim());
                    Console.WriteLine();
                }
                else
                    Console.WriteLine();
            }
        }
    }
}
