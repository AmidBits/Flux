namespace Flux
{
  public static partial class ExtensionMethodsDiagnostics
  {
    /// <summary>Compute how many nanoseconds has elapsed.</summary>
    public static double GetElapsedNanoseconds(this System.Diagnostics.Stopwatch source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000000000.0;
    }
  }
}
