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

      var length0 = source.GetLength(0);
      var length1 = source.GetLength(1);

      #region MaxWidths

      var maxWidths = new int[length1];

      for (var i0 = length0 - 1; i0 >= 0; i0--)
        for (var i1 = length1 - 1; i1 >= 0; i1--)
          maxWidths[i1] = System.Math.Max(maxWidths[i1], (source[i0, i1]?.ToString() ?? string.Empty).Length);

      var maxWidth = maxWidths.Max();

      if (options.UniformWidth)
        for (var c = length1 - 1; c >= 0; c--)
          maxWidths[c] = maxWidth;

      #endregion // MaxWidths

      var verticalString = options.VerticalSeparator == '\0' ? null : string.Join(options.HorizontalSeparator, maxWidths.Select(width => new string(options.VerticalSeparator, width)));

      var horizontalFormat = string.Join(options.HorizontalSeparator == '\0' ? null : options.HorizontalSeparator.ToString(), maxWidths.Select((width, index) => $"{{{index},-{width}}}")); // Build format in advance since all rows have the same number of columns.

      for (var r = 0; r < length0; r++) // Consider row as dimension 0.
      {
        if (r > 0)
        {
          sb.AppendLine();

          if (!string.IsNullOrEmpty(verticalString) && r > 0)
            sb.AppendLine(verticalString);
        }

        var horizontalValues = System.Linq.Enumerable.Range(0, length1).Select(ci => (new System.Text.StringBuilder($"{source[r, ci]}") is var sb && options.CenterContent ? sb.PadEven(maxWidths[ci], ' ', ' ') : sb).ToString()).ToArray();

        sb.Append(string.Format(null, horizontalFormat, horizontalValues));
      }

      return sb;
    }
  }
}