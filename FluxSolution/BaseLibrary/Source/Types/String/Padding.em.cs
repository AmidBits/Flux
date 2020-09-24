namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a string padded evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
    public static string PadEven(this string source, int totalWidth, char paddingLeft, char paddingRight)
      => totalWidth > (source ?? throw new System.ArgumentNullException(nameof(source))).Length && totalWidth - source.Length is int overflow && overflow / 2 is var overflowDiv2 ? new string(paddingLeft, overflowDiv2) + source + new string(paddingRight, overflow - overflowDiv2) : source;

    /// <summary>Returns a new string that right-aligns this string by padding them on the left with the specified padding string.</summary>
    public static string PadLeft(this string source, int totalWidth, string paddingString)
    {
      var sb = new System.Text.StringBuilder(source);

      while (sb.Length < totalWidth)
      {
        sb.Insert(0, paddingString);
      }

      return sb.ToString(sb.Length - totalWidth, totalWidth);
    }

    /// <summary>Returns a new string that left-aligns this string by padding them on the right with the specified padding string.</summary>
    public static string PadRight(this string source, int totalWidth, string paddingString)
    {
      var sb = new System.Text.StringBuilder(source);

      while (sb.Length < totalWidth)
      {
        sb.Append(paddingString);
      }

      return sb.ToString(0, totalWidth);
    }
  }
}
