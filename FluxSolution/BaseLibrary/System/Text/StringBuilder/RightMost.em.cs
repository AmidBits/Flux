namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    public static string RightMost(this System.Text.StringBuilder source, int count)
      => source.ToString(int.Max(0, source.Length - count));
  }
}
