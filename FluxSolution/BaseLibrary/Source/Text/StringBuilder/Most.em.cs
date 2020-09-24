namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static string LeftMost(this System.Text.StringBuilder source, int maxCount)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).ToString(0, System.Math.Min(source.Length, maxCount));

    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static string RightMost(this System.Text.StringBuilder source, int maxCount)
      => System.Math.Max(0, (source ?? throw new System.ArgumentNullException(nameof(source))).Length - maxCount) is var start ? source.ToString(start, source.Length - start) : string.Empty;
  }
}
