using System;
using System.Linq;

namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension, i.e. so called row-major.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings<T>(this T[,] source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false, bool centerContent = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var length0 = source.GetLength(0);
      var length1 = source.GetLength(1);


      var maxWidths = new int[length1];
      var maxWidth = 0;

      var target = new string[length0, length1];

      for (var i0 = length0 - 1; i0 >= 0; i0--)
      {
        for (var i1 = length1 - 1; i1 >= 0; i1--)
        {
          var v01 = $"{source[i0, i1]}";

          target[i0, i1] = v01;

          if (v01.Length > maxWidths[i1])
          {
            maxWidths[i1] = v01.Length;

            if (v01.Length > maxWidth)
              maxWidth = v01.Length;
          }
        }
      }

      var horizontalFormat = string.Join(horizontalSeparator.ToString(), maxWidths.Select((e, i) => $"{{{i},-{(uniformMaxWidth ? maxWidth : e)}}}"));

      var verticalSeparatorLine = verticalSeparator == '\0' ? null : string.Join(horizontalSeparator.ToString(), maxWidths.Select((e, i) => new string(verticalSeparator, uniformMaxWidth ? maxWidth : e)));

      for (var rowIndex = 0; rowIndex < length0; rowIndex++) // Consider row as dimension 0.
      {
        if (!(verticalSeparatorLine is null) && rowIndex > 0)
          yield return verticalSeparatorLine;

        yield return string.Format(System.Globalization.CultureInfo.InvariantCulture, horizontalFormat, target.GetElements(0, rowIndex).Select((e, i) => centerContent ? e.item.ToStringBuilder().PadEven(maxWidth, ' ', ' ').ToString() : e.item).ToArray());
      }
    }
    /// <summary>Returns the two-dimensional array as a ready-to-print grid-like formatted string, that can be printed in the console.</summary>
    public static string ToConsoleString<T>(this T[,] source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u2015', bool uniformWidth = false, bool centerContent = false)
      => string.Join(System.Environment.NewLine, ToConsoleStrings(source, horizontalSeparator, verticalSeparator, uniformWidth, centerContent));
  }
}
