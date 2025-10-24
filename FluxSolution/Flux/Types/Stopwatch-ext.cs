namespace Flux
{
  public static partial class XtensionStopwatch
  {
    extension(System.Diagnostics.Stopwatch source)
    {
      /// <summary>Compute how many microseconds has elapsed.</summary>
      public double GetElapsedMicroseconds()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000000.0;
      }

      /// <summary>Compute how many milliseconds has elapsed.</summary>
      public double GetElapsedMilliseconds()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000.0;
      }

      /// <summary>Compute how many nanoseconds has elapsed.</summary>
      public double GetElapsedNanoseconds()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000000000.0;
      }
    }
  }
}
