namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static string Rank1ToConsoleString<T>(this T[] source, bool pivot, ConsoleStringOptions? options = null)
      => (pivot ? new T[][] { source } : source.Select(e => new T[] { e }).ToArray())
      .JaggedToConsoleString(options);
  }
}
