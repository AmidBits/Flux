namespace Flux
{
  public static partial class ArrayJaggedExtensions
  {
    extension<T>(T[][] source)
    {
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
