namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a string containing at most <paramref name="count"/> of characters from the left (start-of-string), if available, otherwise the entire string is returned.</summary>
    public static string LeftMost(this System.Text.StringBuilder source, int count)
      => source.ToString(source.Length - int.Min(source.Length, count));
  }
}
