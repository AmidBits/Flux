namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static System.Text.StringBuilder Rank1ToConsoleString<T>(this T[] source, bool pivot, ConsoleStringOptions? options = null)
      => (pivot ? new T[][] { source } : source.Select(e => new T[] { e }).ToArray())
      .JaggedToConsoleString(options);
  }
}
