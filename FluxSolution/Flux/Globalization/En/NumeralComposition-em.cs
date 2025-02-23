namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Create a string of words from the <paramref name="number"/>. The <paramref name="decimalPointWord"/> is added as a decimal separator word if it's not a whole number, and </para>
    /// <para><see href="https://en.wikipedia.org/wiki/Cardinal_numeral"/></para>
    /// <example>(-123.456).ToCardinalNumeralCompoundString() // = "Negative One Hundred Twenty-Three Point Four Hundred Fifty-Six".</example>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string ToEnglishCardinalNumeralCompoundString<TSelf>(this TSelf number, bool includeAnd = false, string decimalPointWord = "Point")
      where TSelf : System.Numerics.INumber<TSelf>
      => Globalization.En.NumeralComposition.GetCardinalNumeralString(number, decimalPointWord, includeAnd);
  }
}
