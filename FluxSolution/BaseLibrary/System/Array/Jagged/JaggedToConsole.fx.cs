namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static System.Text.StringBuilder JaggedToConsole<T>(this T[][] source, ConsoleFormatOptions? options = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      options ??= ConsoleFormatOptions.Default;

      var sb = new System.Text.StringBuilder();

      var length0 = source.GetLength(0);

      #region MaxWidths

      var length1Max = source.Max(a => a.Length);

      var maxWidths = new int[length1Max]; // Create an array to hold the max widths of all elements in the largest sub-array.

      for (var r = length0 - 1; r >= 0; r--)
        for (var c = source[r].Length - 1; c >= 0; c--)
          maxWidths[c] = System.Math.Max(maxWidths[c], $"{source[r][c]}".Length); // Find the max width for each column from all sub-arrays.

      var maxWidth = maxWidths.Max(); // Find the total max width.

      if (options.UniformWidth)
        for (var c = length1Max - 1; c >= 0; c--)
          maxWidths[c] = maxWidth;

      #endregion // MaxWidths

      var verticalString = options.VerticalSeparator == '\0' ? null : string.Join(options.HorizontalSeparator, maxWidths.Select(width => new string(options.VerticalSeparator, width)));

      for (var r = 0; r < length0; r++) // Consider row as dimension 0.
      {
        if (r > 0)
        {
          sb.AppendLine();

          if (verticalString is not null)
            sb.AppendLine(verticalString);
        }

        var horizontalFormat = string.Join(options.HorizontalSeparator == '\0' ? null : options.HorizontalSeparator.ToString(), maxWidths.Take(source[r].Length).Select((width, index) => $"{{{index},-{width}}}")); // Build format for each horizontal since each can be different.
        var horizontalValues = source[r].Select((o, oi) => (new System.Text.StringBuilder($"{o}") is var sb && options.CenterContent ? sb.PadEven(maxWidths[oi], ' ', ' ') : sb).ToString()).ToArray();

        sb.Append(string.Format(null, horizontalFormat, horizontalValues));
      }

      return sb;
    }
  }
}