namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a string containing the left most specified number of characters, if available, otherwise as many as there are.</summary>
    public static string LeftMost(this System.Text.StringBuilder source, int count)
      => source.ToString(source.Length - int.Min(source.Length, count));
  }
}
