namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed class MathTokenOperator
    : MathToken
  {
    public const string AssociativityLeft = @"Left";
    public const string AssociativityRight = @"Right";

    public const string Regex = @"^[\u002B\u00F7\u00D7\u2212]";

    public const string SymbolAdd = "\u002B";
    public const string SymbolDivide = "\u00F7";
    public const string SymbolMultiply = "\u00D7";
    public const string SymbolSubtract = "\u2212";

    public string Associativity { get; set; }
    public int Precedence { get; set; }

    public MathTokenOperator(string name, string text, int index)
      : base(name, text, index)
    {
      switch (Value)
      {
        case SymbolDivide:
        case SymbolMultiply:
          Associativity = MathTokenOperator.AssociativityLeft;
          Precedence = 3;
          break;
        case SymbolAdd:
        case SymbolSubtract:
          Associativity = MathTokenOperator.AssociativityLeft;
          Precedence = 2;
          break;
        default:
          Associativity = MathTokenOperator.AssociativityLeft;
          Precedence = 0;
          break;
      }
    }

    public MathToken Evaluate(MathToken left, MathToken right)
    {
      if (left is null) throw new System.ArgumentNullException(nameof(left));
      if (right is null) throw new System.ArgumentNullException(nameof(right));

      return Value switch
      {
        SymbolAdd => new MathTokenNumber(nameof(MathTokenNumber), (double.Parse(left.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) + double.Parse(right.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture)).ToString(System.Globalization.CultureInfo.CurrentCulture), left.Index),
        SymbolDivide => new MathTokenNumber(nameof(MathTokenNumber), (double.Parse(left.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) / double.Parse(right.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture)).ToString(System.Globalization.CultureInfo.CurrentCulture), left.Index),
        SymbolMultiply => new MathTokenNumber(nameof(MathTokenNumber), (double.Parse(left.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) * double.Parse(right.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture)).ToString(System.Globalization.CultureInfo.CurrentCulture), left.Index),
        SymbolSubtract => new MathTokenNumber(nameof(MathTokenNumber), (double.Parse(left.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture) - double.Parse(right.Value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture)).ToString(System.Globalization.CultureInfo.CurrentCulture), left.Index),
        _ => throw new System.ArithmeticException(),
      };
    }

    public override string ToString()
      => $"{base.ToString()},{nameof(Associativity)}=\"{Associativity}\",{nameof(Precedence)}=\"{Precedence}\"";

    public static string Unify(string expression)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      expression = expression.Replace(@"+", MathTokenOperator.SymbolAdd, System.StringComparison.Ordinal);
      expression = expression.Replace(@"/", MathTokenOperator.SymbolDivide, System.StringComparison.Ordinal);
      expression = expression.Replace(@"*", MathTokenOperator.SymbolMultiply, System.StringComparison.Ordinal);
      expression = expression.Replace(@"-", MathTokenOperator.SymbolSubtract, System.StringComparison.Ordinal);

      return expression;
    }
  }
}
