namespace Flux.Text.Tokenization
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed partial class MathTokenNumber
    : MathToken
  {
    //public const string Regex = @"^(?=\d|\.\d)\d*(\.\d*)?([Ee]([+-]?\d+))?";

    [System.Text.RegularExpressions.GeneratedRegex(@"^(?=\d|\.\d)\d*(\.\d*)?([Ee]([+-]?\d+))?")]
    public static partial System.Text.RegularExpressions.Regex Regex();

    public double NumericalValue { get; set; }

    public MathTokenNumber(string name, string text, int index)
      : base(name, text, index)
      => NumericalValue = double.Parse(Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Creates a new <see cref="MathTokenNumber"/> with the value negated and with an optional change in <paramref name="index"/> (otherwise the current index - 1).</summary>
    /// <param name="index">The new index, or null to use the (current index - 1).</param>
    /// <returns>A new <see cref="MathTokenNumber"/>.</returns>
    public MathTokenNumber GetNegated(int? index = null) => new(Name, NumericalValue < 0 ? Value[1..] : '-' + Value, index ?? Index - 1);

    public override string ToString(string? format, System.IFormatProvider? formatProvider) => string.Format(formatProvider, $"{{0}}{(format is null ? string.Empty : $":{format}")}", NumericalValue);
  }
}
