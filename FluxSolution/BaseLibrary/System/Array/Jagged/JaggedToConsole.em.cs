namespace Flux
{
  public record class ConsoleStringOptions
  {
    private readonly bool m_centerContent = false;
    private readonly string m_columnSeparator = "\u007c";
    private readonly bool m_includeNames = true;
    private readonly char m_rowSeparator = '\u002d';
    private readonly bool m_uniformWidth = false;

    public bool CenterContent { get => m_centerContent; init => m_centerContent = value; }
    public string ColumnSeparator { get => m_columnSeparator; init => m_columnSeparator = value; }
    public bool IncludeNames { get => m_includeNames; init => m_includeNames = value; }
    public char RowSeparator { get => m_rowSeparator; init => m_rowSeparator = value; }
    public bool UniformWidth { get => m_uniformWidth; init => m_uniformWidth = value; }

    public string? ColumnSeparatorString => UseColumnSeparator ? m_columnSeparator : null;
    public string? RowSeparatorString => UseRowSeparator ? m_rowSeparator.ToString() : null;

    public bool UseColumnSeparator => m_columnSeparator.Length >= 1 && m_columnSeparator[0] != '\0';
    public bool UseRowSeparator => m_rowSeparator != '\0';

    public string GetContentCentered(string text, int maxWidth) => m_centerContent ? new System.Text.StringBuilder(text).PadEven(maxWidth, ' ', ' ').ToString() : text;
  }

  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension, i.e. so called row-major.</summary>
  public static partial class ArrayJagged
  {
    /// <summary>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static System.Collections.Generic.IEnumerable<string> JaggedToConsoleStrings<T>(this T[][] source, char horizontalSeparator = '\u007c', char verticalSeparator = '\u002d', bool uniformWidth = false, bool centerContent = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var maxWidths = new int[source.Max(a => a.Length)]; // Create an array to hold the max widths of all elements in the largest sub-array.

      for (var r = source.Length - 1; r >= 0; r--)
        for (var c = source[r].Length - 1; c >= 0; c--)
          maxWidths[c] = System.Math.Max(maxWidths[c], $"{source[r][c]}".Length); // Find the max width for each column from all sub-arrays.

      var maxWidth = maxWidths.Max(); // Find the total max width.

      if (uniformWidth)
        for (var c = maxWidths.Length - 1; c >= 0; c--)
          maxWidths[c] = maxWidth;

      var verticalSeparatorString = verticalSeparator == '\0' ? null : string.Join(horizontalSeparator, maxWidths.Select(width => new string(verticalSeparator, width)));

      for (var r = 0; r < source.Length; r++) // Consider row as dimension 0.
      {
        if (verticalSeparatorString is not null && r > 0)
          yield return verticalSeparatorString;

        var format = string.Join(horizontalSeparator == '\0' ? null : horizontalSeparator.ToString(), maxWidths.Take(source[r].Length).Select((width, index) => $"{{{index},-{width}}}"));
        var values = source[r].Select((e, i) => $"{e}" is var s && centerContent ? new System.Text.StringBuilder(s).PadEven(maxWidths[i], ' ', ' ').ToString() : s).ToArray();

        yield return string.Format(null, format, values);
      }
    }

    public static string JaggedToConsoleString<T>(this T[][] source, char horizontalSeparator = '\u007c', char verticalSeparator = '\u002d', bool uniformWidth = false, bool centerContent = false)
      => string.Join(System.Environment.NewLine, JaggedToConsoleStrings(source, horizontalSeparator, verticalSeparator, uniformWidth, centerContent));
  }
}
