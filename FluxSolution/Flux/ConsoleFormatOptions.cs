namespace Flux
{
  public record class ConsoleFormatOptions
  {
    private readonly string? m_horizontalSeparator = "|";
    private readonly string? m_verticalSeparator = "-";
    private readonly bool m_uniformWidth = false;
    private readonly bool m_centerContent = false;
    private readonly bool m_includeColumnNames = true;

    public static ConsoleFormatOptions Default { get; } = new();

    /// <summary>The horizontal separator character for the console string output, if applicable.</summary>
    public string? HorizontalSeparator { get => m_horizontalSeparator; init => m_horizontalSeparator = value; }

    /// <summary>The vertical separator character for the console string output, if applicable.</summary>
    public string? VerticalSeparator { get => m_verticalSeparator; init => m_verticalSeparator = value; }

    /// <summary>Make column content to uniform width in the console string output, if applicable.</summary>
    public bool UniformWidth { get => m_uniformWidth; init => m_uniformWidth = value; }

    /// <summary>Center column content in the console string output, if applicable.</summary>
    public bool CenterContent { get => m_centerContent; init => m_centerContent = value; }

    /// <summary>Includes the column names in the console string output, if applicable.</summary>
    public bool IncludeColumnNames { get => m_includeColumnNames; init => m_includeColumnNames = value; }

    /// <summary>
    /// <para>Creates a new format string for horizontal output. The <paramref name="maxWidths"/> for each column determines the generated character count for each column, and the <paramref name="maxUnits"/> determines how many column formats to generate.</para>
    /// </summary>
    /// <param name="maxWidths"></param>
    /// <param name="maxUnits"></param>
    /// <returns></returns>
    public string CreateHorizontalFormat(int[] maxWidths, int? maxUnits = null)
    {
      var usedMaxWidths = maxUnits.HasValue ? maxWidths.Take(maxUnits.Value) : maxWidths;

      var maxWidth = maxWidths.Max(); // We do max on ALL max-widths (not just the maxUnits/maxCounts), so that UniformWidth is universal across all lines.

      return string.Join(HorizontalSeparator?.ToString() ?? string.Empty, usedMaxWidths.Select((width, index) => '{' + index.ToString() + ',' + '-' + (UniformWidth ? maxWidth : width).ToString() + '}'));
    }

    /// <summary>
    /// <para>Creates a new string for horizontal output, using the specified <paramref name="values"/> and <paramref name="maxWidths"/>.</para>
    /// <para>The number of values dictates how many string units to generate. If the number of max-widths is greater than the number of values, the excess of max-widths are simply not used.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="maxWidths"></param>
    /// <returns></returns>
    public string CreateHorizontalString<T>(T[] values, int[] maxWidths)
    {
      var horizontalFormat = CreateHorizontalFormat(maxWidths, values.Length);

      return CreateHorizontalString(values, maxWidths, horizontalFormat);
    }

    /// <summary>
    /// <para>Creates a new string for horizonal output, using the specified <paramref name="values"/>, <paramref name="maxWidths"/> and <paramref name="horizontalFormat"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="maxWidths"></param>
    /// <param name="horizontalFormat"></param>
    /// <returns></returns>
    public string CreateHorizontalString<T>(T[] values, int[] maxWidths, string horizontalFormat)
    {
      var horizontalValues = CreateHorizontalValues(values, maxWidths);

      return string.Format(null, horizontalFormat, horizontalValues.AsReadOnlySpan());
    }

    /// <summary>
    /// <para>Creates a new list of strings for horizontal output, using the specified <paramref name="values"/> and <paramref name="maxWidths"/>.</para>
    /// <para>The number of values determines how many string values to generate. If the number of max-widths is greater than the number of values, the excess of max-widths are simply not used.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="maxWidths"></param>
    /// <returns></returns>
    public System.Collections.Generic.List<string> CreateHorizontalValues<T>(T[] values, int[] maxWidths)
    {
      var list = new System.Collections.Generic.List<string>();

      var sb = new System.Text.StringBuilder();

      var maxWidth = maxWidths.Max();

      for (var i = 0; i < values.Length; i++)
      {
        sb.Clear();

        sb.Append(values[i]);

        if (CenterContent)
          sb.PadEven(UniformWidth ? maxWidth : maxWidths[i], ' ', ' ');

        list.Add(sb.ToString());
      }

      return list;
    }

    /// <summary>
    /// <para>Creates a new string for vertical output. Vertical in this sense, means the string between each horizontal string.</para>
    /// </summary>
    /// <param name="maxWidths"></param>
    /// <returns></returns>
    public string? CreateVerticalString(int[] maxWidths)
    {
      if (VerticalSeparator is null)
        return null;

      var maxWidth = maxWidths.Max();

      return string.Join(HorizontalSeparator, maxWidths.Select(width => VerticalSeparator.ToStringBuilder().PadRight(UniformWidth ? maxWidth : width, VerticalSeparator).ToString()));
    }
  }
}
