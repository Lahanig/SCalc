using SCalc.Models;

namespace SCalc.Interfaces;

public interface ICalcService
{
   public void Calc(string userPrompt);
   public double GetCalcResult();
}
