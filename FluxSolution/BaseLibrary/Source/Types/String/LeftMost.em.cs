namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static string LeftMost(this string source, int maxCount)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Substring(0, System.Math.Min(source.Length, maxCount));
  }
}
