namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed class MathTokenNumber
    : MathToken
  {
    public const string Regex = @"^(?=\d|\.\d)\d*(\.\d*)?([Ee]([+-]?\d+))?";

    public double NumericalValue { get; set; }

    public MathTokenNumber(string name, string text, int index)
      : base(name, text, index)
    {
      NumericalValue = double.Parse(Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture);
    }

    public override string ToString() => $"{base.ToString()},{nameof(NumericalValue)}=\"{NumericalValue}\"";
  }
}
