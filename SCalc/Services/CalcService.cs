using SCalc.Interfaces;
using SCalc.Models;

namespace SCalc.Services;

public class CalcService : ICalcService
{
  private int calcResult = 0;

  public void Calc(string userExpression)
  {
    calcResult = 10;
  }

  public int getCalcResult()
  {
    return this.calcResult;
  }
}
