namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static System.Text.StringBuilder Rank2ToConsole<T>(this T[,] source, ConsoleFormatOptions? options = null)
    {
      source.AssertRank(2);

      options ??= ConsoleFormatOptions.Default;

      var sb = new System.Text.StringBuilder();

      #region MaxWidths

      var maxWidths = new int[source.GetLength(1)];

      for (var i0 = source.GetLength(0) - 1; i0 >= 0; i0--)
        for (var i1 = source.GetLength(1) - 1; i1 >= 0; i1--)
          maxWidths[i1] = System.Math.Max(maxWidths[i1], (source[i0, i1]?.ToString() ?? string.Empty).Length);

      var maxWidth = maxWidths.Max();

      if (options.UniformWidth)
        for (var c = maxWidths.Length - 1; c >= 0; c--)
          maxWidths[c] = maxWidth;

      #endregion // MaxWidths

      var verticalLine = options.VerticalSeparator == '\0' ? null : string.Join(options.HorizontalSeparator, System.Linq.Enumerable.Select(maxWidths, width => new string(options.VerticalSeparator, width)));

      var horizontalLineFormat = string.Join(options.HorizontalSeparator == '\0' ? null : options.HorizontalSeparator.ToString(), System.Linq.Enumerable.Select(maxWidths, (width, index) => $"{{{index},-{width}}}"));

      for (var r = 0; r < source.GetLength(0); r++) // Consider row as dimension 0.
      {
        if (!string.IsNullOrEmpty(verticalLine) && r > 0)
          sb.AppendLine(verticalLine);

        var values = System.Linq.Enumerable.Range(0, source.GetLength(1)).Select((c, i) => $"{source[r, c]}" is var s && options.CenterContent ? new System.Text.StringBuilder(s).PadEven(maxWidths[i], ' ', ' ').ToString() : s).ToArray();

        sb.AppendLine(string.Format(null, horizontalLineFormat, values));
      }

      if (sb.IsCommonSuffix(0, System.Environment.NewLine)) // Remove CR + LF at the end of the string builder.
        sb.Remove(sb.Length - System.Environment.NewLine.Length, System.Environment.NewLine.Length);

      return sb;
    }
  }
}
