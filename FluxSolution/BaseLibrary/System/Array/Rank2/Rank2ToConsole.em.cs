namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static System.Collections.Generic.IEnumerable<string> Rank2ToConsoleStrings<T>(this T[,] source, ConsoleStringOptions? options = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      options ??= new ConsoleStringOptions();

      var maxWidths = new int[source.GetLength(1)];

      for (var i0 = source.GetLength(0) - 1; i0 >= 0; i0--)
        for (var i1 = source.GetLength(1) - 1; i1 >= 0; i1--)
          maxWidths[i1] = System.Math.Max(maxWidths[i1], (source[i0, i1]?.ToString() ?? string.Empty).Length);

      var maxWidth = maxWidths.Max();

      if (options.UniformWidth)
        for (var c = maxWidths.Length - 1; c >= 0; c--)
          maxWidths[c] = maxWidth;

      var verticalLine = options.VerticalSeparator == '\0' ? null : string.Join(options.HorizontalSeparator, System.Linq.Enumerable.Select(maxWidths, width => new string(options.VerticalSeparator, width)));

      var horizontalLineFormat = string.Join(options.HorizontalSeparator == '\0' ? null : options.HorizontalSeparator.ToString(), System.Linq.Enumerable.Select(maxWidths, (width, index) => $"{{{index},-{width}}}"));

      for (var r = 0; r < source.GetLength(0); r++) // Consider row as dimension 0.
      {
        if (!string.IsNullOrEmpty(verticalLine) && r > 0)
          yield return verticalLine;

        var values = System.Linq.Enumerable.Range(0, source.GetLength(1)).Select((c, i) => $"{source[r, c]}" is var s && options.CenterContent ? new System.Text.StringBuilder(s).PadEven(maxWidths[i], ' ', ' ').ToString() : s).ToArray();

        yield return string.Format(null, horizontalLineFormat, values);
      }
    }

    public static string Rank2ToConsoleString<T>(this T[,] source, ConsoleStringOptions? options = null)
      => string.Join(System.Environment.NewLine, source.Rank2ToConsoleStrings(options));
  }
}
