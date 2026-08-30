namespace SCalc.Models;

public class Token
{
    public string RawValue { get; set; } = string.Empty;
    public double NumberValue { get; set; } = 0;
    public TokenType Type { get; set; }
}
