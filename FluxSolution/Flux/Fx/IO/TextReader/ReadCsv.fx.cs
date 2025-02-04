namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a sequence of fields derived using <see cref="Microsoft.VisualBasic.FileIO.TextFieldParser"/>.</para>
    /// </summary>
    /// <param name="reader">The <see cref="System.IO.TextReader"/> to read from.</param>
    /// <param name="delimiter">The delimiter to use for the parser.</param>
    /// <param name="enclosedInQuotes">Whether fields are enclosed in double quotes.</param>
    /// <param name="trimWhiteSpace">Indicates whether leading and trailing white space should be trimmed from field values.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static System.Collections.Generic.IEnumerable<string[]> ReadCsv(this System.IO.TextReader reader, string delimiter = ",", bool enclosedInQuotes = true, bool trimWhiteSpace = false)
    {
      using var tfp = new Microsoft.VisualBasic.FileIO.TextFieldParser(reader);

      tfp.SetDelimiters(delimiter);
      tfp.HasFieldsEnclosedInQuotes = enclosedInQuotes;
      tfp.TrimWhiteSpace = trimWhiteSpace;

      while (!tfp.EndOfData)
        yield return tfp.ReadFields() ?? throw new System.InvalidOperationException();
    }
  }
}
