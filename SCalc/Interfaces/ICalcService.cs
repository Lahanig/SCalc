using SCalc.Models;

namespace SCalc.Interfaces;

public interface ICalcService
{
   public void Calc(string userExpression);
   public int getCalcResult();
}
