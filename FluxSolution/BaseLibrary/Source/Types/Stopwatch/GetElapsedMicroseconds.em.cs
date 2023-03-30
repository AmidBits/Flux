namespace Flux
{
  public static partial class ExtensionMethodsDiagnostics
  {
    /// <summary>Compute how many microseconds has elapsed.</summary>
    public static double GetElapsedMicroseconds(this System.Diagnostics.Stopwatch source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000000.0;
  }
}
