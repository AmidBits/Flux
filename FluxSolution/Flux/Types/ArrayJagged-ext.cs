namespace Flux
{
  public static partial class XtensionArrayJagged
  {
    extension<T>(T[][] source)
    {
      /// <summary>
      /// <para>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public string JaggedToConsoleString(ConsoleFormatOptions? options = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        options ??= ConsoleFormatOptions.Default;

        var length0 = source.GetLength(0);
        var length1 = source.Max(a => a.Length); // Here, length1 = the MAX number of elements found in all sub-arrays of the jagged array.

        #region MaxWidths

        var maxWidths = new int[length1]; // Create an array to hold the max widths of all elements in the largest sub-array.

        for (var r = length0 - 1; r >= 0; r--)
          for (var c = source[r].Length - 1; c >= 0; c--)
            maxWidths[c] = int.Max(maxWidths[c], (source[r][c]?.ToString() ?? string.Empty).Length); // Find the max width for each column from all sub-arrays.

        #endregion // MaxWidths

        var sb = new System.Text.StringBuilder();

        var verticalString = options.CreateVerticalString(maxWidths);

        for (var r = 0; r < length0; r++)
        {
          if (r > 0)
          {
            sb.AppendLine();

            if (verticalString is not null)
              sb.AppendLine(verticalString);
          }

          var horizontalString = options.CreateHorizontalString(source[r], maxWidths);

          sb.Append(horizontalString);
        }

        return sb.ToString();
      }

      /// <summary>
      /// <para>Creates a new two-dimensional array from the jagged array (i.e. an array of arrays). The outer array becomes dimension-0 (rows) and the inner arrays make up each dimension-1 (columns).</para>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public T[,] JaggedToRank2Array()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var target = new T[source.Length, source.Max(t => t.Length)];

        for (var i = source.Length - 1; i >= 0; i--)
          for (var j = source[i].Length - 1; j >= 0; j--)
            target[i, j] = source[i][j];

        return target;
      }

      /// <summary>
      /// <para>Writes a jagged array as URGF (Unicode tabular data) to the <paramref name="target"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="target"></param>
      public void WriteJaggedToUrgf(System.IO.TextWriter target)
      {
        for (var r = 0; r < source.Length; r++)
        {
          if (r > 0) target.Write((char)UnicodeInformationSeparator.RecordSeparator);

          target.Write(source[r].ToUrgfString());
        }
      }

      /// <summary>
      /// <para>Writes a jagged array as URGF (Unicode tabular data) to the <paramref name="target"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="target"></param>
      public void WriteJaggedToCsv(System.IO.TextWriter target, string newLine = "\r\n")
      {
        for (var r = 0; r < source.Length; r++)
        {
          if (r > 0) target.Write(newLine);

          target.Write(source[r].ToCsvString());
        }
      }
    }
  }
}

#region Create sample file
/*

using var sw = System.IO.File.CreateText(fileName);

var data = new string[][] { new string[] { "A", "B" }, new string[] { "1", "2" } };

Flux.Fx.WriteUrgf(sw, data);

sw.Write((char)UnicodeInformationSeparator.FileSeparator);

data = new string[][] { new string[] { "C", "D" }, new string[] { "3", "4" } };

Flux.Fx.WriteUrgf(sw, data);

sw.Flush();
sw.Close();

*/
#endregion // Create sample file
