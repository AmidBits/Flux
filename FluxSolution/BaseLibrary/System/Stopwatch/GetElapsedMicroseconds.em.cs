namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Compute how many microseconds has elapsed.</summary>
    public static double GetElapsedMicroseconds(this System.Diagnostics.Stopwatch source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000000.0;
    }
  }
}
