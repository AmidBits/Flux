using System.Linq;

namespace Flux.Formatters
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public class ArrayFormatter
    : AFormatter
  {
    public static readonly ArrayFormatter Separated = new ArrayFormatter();
    public static readonly ArrayFormatter SeparatedUniform = new ArrayFormatter() { UniformWidth = true };
    public static readonly ArrayFormatter NoSeparators = new ArrayFormatter() { HorizontalSeparator = null, VerticalSeparator = null };
    public static readonly ArrayFormatter NoSeparatorsUniform = new ArrayFormatter() { HorizontalSeparator = null, VerticalSeparator = null, UniformWidth = true };

    public string? HorizontalSeparator { get; set; } = "\u007C";
    public string? VerticalSeparator { get; set; } = "\u002D";
    public bool UniformWidth { get; set; }
    public int ForcedWidth { get; set; }

    //public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    //{
    //	if(arg is object[][])
    //	return HandleOtherFormats(format, arg);
    //}

    #region Jagged Arrays
    public System.Collections.Generic.IEnumerable<string> JaggedToConsoleStrings<T>(T[][] source, System.Func<T, int, string> formatSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      formatSelector ??= (e, i) => $"{e}";

      var maxColumns = System.Linq.Enumerable.Range(0, source.GetLength(0)).Max(i => source[i].Length);

      var columnMaxWidths = System.Linq.Enumerable.Range(0, maxColumns).Select(i => source.Max(a => i < a.Length ? $"{a[i]}".Length : 0)).ToArray();
      if (UniformWidth) columnMaxWidths = columnMaxWidths.Select(w => columnMaxWidths.Max()).ToArray(); // If used, replace all with total max width.
      if (ForcedWidth > 0) columnMaxWidths = columnMaxWidths.Select(w => ForcedWidth).ToArray();

      var format = string.Join(HorizontalSeparator, columnMaxWidths.Select((width, index) => $"{{{index},-{width}}}"));

      var verticalSeparatorRow = string.IsNullOrEmpty(VerticalSeparator) ? null : string.Join(HorizontalSeparator, columnMaxWidths.Select((width, index) => string.Concat(VerticalSeparator.Repeat(width / VerticalSeparator.Length + 1)).LeftMost(width)));

      for (int index0 = 0, length0 = source.Length; index0 < length0; index0++)
      {
        if (!(verticalSeparatorRow is null) && index0 > 0)
          yield return verticalSeparatorRow;

        yield return string.Format(System.Globalization.CultureInfo.CurrentCulture, format, source[index0].Select(o => (object?)o).ToArray());
      }
    }
    public string JaggedToConsoleString<T>(T[][] source, System.Func<T, int, string> formatSelector)
      => string.Join(System.Environment.NewLine, JaggedToConsoleStrings(source, formatSelector));
    public string JaggedToConsoleString<T>(T[][] source)
      => JaggedToConsoleString(source, (e, i) => $"{e}");
    #endregion Jagged Arrays

    #region Two-Dimensional Arrays
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    // Returns the array elements formatted for the console.
    public System.Collections.Generic.IEnumerable<string> TwoToConsoleStrings<T>(T[,] source, System.Func<T, int, string> formatSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      formatSelector ??= (e, i) => $"{e}";

      var columnMaxWidths = System.Linq.Enumerable.Range(0, source.GetLength(1)).Select(i => source.GetElements(1, i).Select((e, i) => formatSelector(e.item, i)).Max(s => s.Length)).ToArray();
      if (UniformWidth) columnMaxWidths = columnMaxWidths.Select(w => columnMaxWidths.Max()).ToArray(); // If used, replace all with total max width.
      if (ForcedWidth > 0) columnMaxWidths = columnMaxWidths.Select(w => ForcedWidth).ToArray();

      var format = string.Join(HorizontalSeparator, columnMaxWidths.Select((width, index) => $"{{{index},-{width}}}"));

      var verticalSeparatorRow = string.IsNullOrEmpty(VerticalSeparator) ? null : string.Join(HorizontalSeparator, columnMaxWidths.Select((width, index) => string.Concat(VerticalSeparator.Repeat(width / VerticalSeparator.Length + 1)).LeftMost(width)));

      for (int index0 = 0, length0 = source.GetLength(0); index0 < length0; index0++)
      {
        if (!(verticalSeparatorRow is null) && index0 > 0)
          yield return verticalSeparatorRow;

        yield return string.Format(System.Globalization.CultureInfo.CurrentCulture, format, source.GetElements(0, index0).Select((e, i) => formatSelector(e.item, i)).ToArray());
      }
    }
    public string TwoToConsoleString<T>(T[,] source, System.Func<T, int, string> formatSelector)
      => string.Join(System.Environment.NewLine, TwoToConsoleStrings(source, formatSelector));
    public string TwoToConsoleString<T>(T[,] source)
      => TwoToConsoleString(source, (e, i) => $"{e}");
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
    #endregion Two-Dimensional Arrays

    #region Single-Dimensional (i.e. 1D) Arrays
    #endregion Single-Dimensional (i.e. 1D) Arrays
  }
}
