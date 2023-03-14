namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension, i.e. so called row-major.</summary>
  public static partial class ArrayRank1
  {
    /// <summary>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static string ToRank1ConsoleString<T>(this T[] source, char horizontalSeparator = '\u007c', bool uniformWidth = false, bool centerContent = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var values = System.Linq.Enumerable.Range(0, source.Length).Select(i => source[i]?.ToString() ?? string.Empty).ToArray();

      var maxWidths = values.Select(s => s.Length).ToArray();

      var maxWidth = maxWidths.Max();

      var format = string.Join(horizontalSeparator == '\0' ? null : horizontalSeparator.ToString(), System.Linq.Enumerable.Select(maxWidths, (e, i) => $"{{{i},-{(uniformWidth ? maxWidth : e)}}}"));

      var array = values.Select(s => centerContent ? s.ToStringBuilder().PadEven(maxWidth, ' ', ' ').ToString() : s).ToArray();

      return string.Format(null, format, array);
    }
  }
}
