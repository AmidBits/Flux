namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static System.Text.StringBuilder JaggedToConsole<T>(this T[][] source, ConsoleStringOptions? options = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      options ??= new ConsoleStringOptions();

      var sb = new System.Text.StringBuilder();

      #region MaxWidths

      var maxWidths = new int[source.Max(a => a.Length)]; // Create an array to hold the max widths of all elements in the largest sub-array.

      for (var r = source.Length - 1; r >= 0; r--)
        for (var c = source[r].Length - 1; c >= 0; c--)
          maxWidths[c] = System.Math.Max(maxWidths[c], $"{source[r][c]}".Length); // Find the max width for each column from all sub-arrays.

      var maxWidth = maxWidths.Max(); // Find the total max width.

      if (options.UniformWidth)
        for (var c = maxWidths.Length - 1; c >= 0; c--)
          maxWidths[c] = maxWidth;

      #endregion // MaxWidths

      var verticalSeparatorString = options.VerticalSeparator == '\0' ? null : string.Join(options.HorizontalSeparator, maxWidths.Select(width => new string(options.VerticalSeparator, width)));

      for (var r = 0; r < source.Length; r++) // Consider row as dimension 0.
      {
        if (verticalSeparatorString is not null && r > 0)
          sb.AppendLine(verticalSeparatorString);

        var format = string.Join(options.HorizontalSeparator == '\0' ? null : options.HorizontalSeparator.ToString(), maxWidths.Take(source[r].Length).Select((width, index) => $"{{{index},-{width}}}"));
        var values = source[r].Select((e, i) => $"{e}" is var s && options.CenterContent ? new System.Text.StringBuilder(s).PadEven(maxWidths[i], ' ', ' ').ToString() : s).ToArray();

        sb.AppendLine(string.Format(null, format, values));
      }

      return sb;
    }
  }
}
