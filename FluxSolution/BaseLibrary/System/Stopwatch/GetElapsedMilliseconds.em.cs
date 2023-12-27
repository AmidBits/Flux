namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Compute how many milliseconds has elapsed.</summary>
    public static double GetElapsedMilliseconds(this System.Diagnostics.Stopwatch source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000.0;
    }
  }
}
