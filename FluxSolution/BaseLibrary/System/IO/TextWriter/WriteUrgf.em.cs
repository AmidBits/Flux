namespace Flux
{
  public static partial class Fx
  {
    public static void WriteUrgfUnitSeparator(this System.IO.TextWriter writer) => writer.Write('\u001F');
    public static void WriteUrgfRecordSeparator(this System.IO.TextWriter writer) => writer.Write('\u001E');
    public static void WriteUrgfGroupSeparator(this System.IO.TextWriter writer) => writer.Write('\u001D');
    public static void WriteUrgfFileSeparator(this System.IO.TextWriter writer) => writer.Write('\u001C');

    public static void WriteUrgfUnit(this System.IO.TextWriter writer, params string[] data)
    {
      for (var u = 0; u < data.Length; u++)
      {
        if (u > 0) writer.WriteUrgfUnitSeparator();

        writer.Write(data[u]);
      }
    }

    public static void WriteUrgfRecord(this System.IO.TextWriter writer, params string[][] data)
    {
      for (var r = 0; r < data.Length; r++)
      {
        if (r > 0) writer.WriteUrgfRecordSeparator();

        WriteUrgfUnit(writer, data[r]);
      }
    }

    public static void WriteUrgfGroup(this System.IO.TextWriter writer, params string[][][] data)
    {
      for (var g = 0; g < data.Length; g++)
      {
        if (g > 0) writer.WriteUrgfGroupSeparator();

        WriteUrgfRecord(writer, data[g]);
      }
    }

    public static void WriteUrgfFile(this System.IO.TextWriter writer, params string[][][][] data)
    {
      for (var g = 0; g < data.Length; g++)
      {
        if (g > 0) writer.WriteUrgfFileSeparator();

        WriteUrgfGroup(writer, data[g]);
      }
    }
  }
}
