namespace Flux
{
  public static partial class XtendString
  {
    /// <summary>Returns whether the expression is a valid Social Security Number (SSN).</summary>
    public static bool IsSocialSecurityNumber(this string text)
      => System.Text.RegularExpressions.Regex.IsMatch(text, string.Format(@"(?<!\d)(?<AAA>{1}){0}(?<GG>{2}){0}(?<SSSS>{3})(?!\d)", @".?", @"(?!(000|666|9\d\d))\d{3}", @"(?!00)\d{2}", @"(?!0000)\d{4}"));
  }
}
