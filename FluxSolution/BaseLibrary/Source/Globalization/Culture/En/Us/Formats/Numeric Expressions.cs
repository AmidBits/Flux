namespace Flux.Globalization
{
  public static partial class Number
  {
    /// <summary>This regular expression matches an optional sign, that is either followed by zero or more digits followed by a dot and one or more digits (a floating point number with optional integer part), or that is followed by one or more digits (an integer).</summary>
    public const string RegexDecimalNumber = @"[-+]?[0-9]*\.?[0-9]+";
    /// <summary>The entire exponent part can be made optional by adding grouping (parenthesis) and the a question mark at the end of the parenthesis.</summary>
    public const string RegexNumberExponent = @"([eE][-+]?[0-9]+)";
  }
}
