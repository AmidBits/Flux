namespace Flux
{
  public static partial class XtendString
  {
    /// <summary>Returns a new sequence of Unicode code points from the string.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetCodePoints(this string characters)
    {
      for (var index = 0; index < characters.Length; index += char.IsSurrogatePair(characters, index) ? 2 : 1)
      {
        yield return char.ConvertToUtf32(characters, index);
      }
    }
  }
}
