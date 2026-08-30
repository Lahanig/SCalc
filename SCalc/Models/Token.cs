namespace SCalc.Models;

public class Token
{
    public string rawValue { get; set; } = string.Empty;
    public double numberValue { get; set; } = 0;
    public TokenType type { get; set; } // "NUMBER" or "OPERATION"
}
