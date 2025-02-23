namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a string containing at most <paramref name="count"/> of characters from the right (end-of-string), if available, otherwise the entire string is returned.</summary>
    public static string RightMost(this System.Text.StringBuilder source, int count)
      => source.ToString(int.Max(0, source.Length - count));
  }
}
