namespace Flux
{
  public record class ConsoleFormatOptions
  {
    public static ConsoleFormatOptions Default { get; } = new();

    public AlignmentHorizontal HorizontalAlignment { get; init; } = AlignmentHorizontal.Left;

    /// <summary>The horizontal separator character for the console string output, if applicable.</summary>
    public string? HorizontalSeparator { get; init; } = "|";

    /// <summary>The vertical separator character for the console string output, if applicable.</summary>
    public string? VerticalSeparator { get; init; } = "-";

    /// <summary>Make column content to uniform width in the console string output, if applicable.</summary>
    public bool UniformWidth { get; init; } = false;

    ///// <summary>Center column content in the console string output, if applicable.</summary>
    //public bool CenterContent { get; init; } = false;

    /// <summary>Includes the column names in the console string output, if applicable.</summary>
    public bool IncludeColumnNames { get; init; } = true;

    /// <summary>
    /// <para>Creates a new format string for horizontal output. The <paramref name="maxWidths"/> for each column determines the generated character count for each column, and the <paramref name="maxUnits"/> determines how many column formats to generate.</para>
    /// </summary>
    /// <param name="maxWidths"></param>
    /// <param name="maxUnits"></param>
    /// <returns></returns>
    public string CreateHorizontalFormat(System.ReadOnlySpan<int> maxWidths, int? maxUnits = null)
    {
      var usedMaxWidths = maxUnits ?? maxWidths.Length;

      var sb = new System.Text.StringBuilder();

      var maxWidth = maxWidths[usedMaxWidths..].Extremum(i => i).MaxValue;

      for (var i = 0; i < usedMaxWidths; i++)
      {
        if (i > 0)
          sb.Append(HorizontalSeparator?.ToString() ?? string.Empty);

        var width = maxWidths[i];

        sb.Append('{');
        sb.Append(i);
        sb.Append(',');
        sb.Append('-');
        sb.Append(UniformWidth ? maxWidth : width);
        sb.Append('}');
      }

      return sb.ToString();
    }

    /// <summary>
    /// <para>Creates a new string for horizontal output, using the specified <paramref name="values"/> and <paramref name="maxWidths"/>.</para>
    /// <para>The number of values dictates how many string units to generate. If the number of max-widths is greater than the number of values, the excess of max-widths are simply not used.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="maxWidths"></param>
    /// <returns></returns>
    public string CreateHorizontalString<T>(System.ReadOnlySpan<T> values, int[] maxWidths)
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
    public string CreateHorizontalString<T>(System.ReadOnlySpan<T> values, int[] maxWidths, string horizontalFormat)
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
    public System.Collections.Generic.List<string> CreateHorizontalValues<T>(System.ReadOnlySpan<T> values, int[] maxWidths)
    {
      var list = new System.Collections.Generic.List<string>();

      var sb = new System.Text.StringBuilder();

      var maxWidth = maxWidths.Max();

      for (var i = 0; i < values.Length; i++)
      {
        sb.Clear();

        sb.Append(values[i]);

        switch (HorizontalAlignment)
        {
          case AlignmentHorizontal.Left:
            sb.PadRight(UniformWidth ? maxWidth : maxWidths[i], ' ');
            break;
          case AlignmentHorizontal.Center:
            sb.PadEven(UniformWidth ? maxWidth : maxWidths[i], ' ', ' ');
            break;
          case AlignmentHorizontal.Right:
            sb.PadLeft(UniformWidth ? maxWidth : maxWidths[i], ' ');
            break;
          default:
            throw new System.NotImplementedException();
        }

        list.Add(sb.ToString());
      }

      return list;
    }

    /// <summary>
    /// <para>Creates a new string for vertical output. Vertical in this sense, means the string between each horizontal string.</para>
    /// </summary>
    /// <param name="maxWidths"></param>
    /// <param name="maxWidth">Optional maximum width for the vertical string. If not provided, the maximum width will be calculated from the <paramref name="maxWidths"/> array.</param>
    /// <returns></returns>
    public string? CreateVerticalString(System.ReadOnlySpan<int> maxWidths, int? maxWidth = null)
    {
      if (VerticalSeparator is null)
        return null;

      var sb = new System.Text.StringBuilder();

      if (!maxWidth.HasValue)
        maxWidth = maxWidths.Extremum(i => i).MaxValue;

      for (var i = 0; i < maxWidths.Length; i++)
      {
        if (i > 0)
          sb.Append(HorizontalSeparator);

        sb.Append(VerticalSeparator.ToStringBuilder().PadRight(UniformWidth ? maxWidth.Value : maxWidths[i], VerticalSeparator));
      }

      return sb.ToString();
    }
  }
}
