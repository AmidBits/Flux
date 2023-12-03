namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension, i.e. so called row-major.</summary>
  public static partial class Fx
  {
    /// <summary>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static string Rank1ToConsoleString<T>(this T[] source, bool pivot, char horizontalSeparator = '\u007c', char verticalSeparator = '\u002d', bool uniformWidth = false, bool centerContent = false)
      => (pivot ? new T[][] { source } : source.Select(e => new T[] { e }).ToArray())
      .JaggedToConsoleString(horizontalSeparator, verticalSeparator, uniformWidth, centerContent);
  }
}
