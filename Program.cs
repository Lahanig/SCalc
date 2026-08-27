// See https://aka.ms/new-console-template for more information
using SCalc;
using SCalc.Interfaces;
using SCalc.Services;

internal class Program
{
  static void Main() {
    ICalcService calcService = new CalcService();
    App app = new App(calcService);
    
    app.Run();
  }
}

