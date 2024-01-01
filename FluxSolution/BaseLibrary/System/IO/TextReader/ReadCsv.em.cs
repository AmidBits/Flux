namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a sequence of fields derived using <see cref="Microsoft.VisualBasic.FileIO.TextFieldParser"/>.</para>
    /// </summary>
    /// <param name="reader">The <see cref="System.IO.TextReader"/> to read from.</param>
    /// <param name="delimiters">The delimiters to use for the parser.</param>
    /// <param name="trimWhiteSpace">Indicates whether leading and trailing white space should be trimmed from field values.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static System.Collections.Generic.IEnumerable<string[]> ReadCsv(this System.IO.TextReader reader, string delimiters, bool trimWhiteSpace)
    {
      using var tfp = new Microsoft.VisualBasic.FileIO.TextFieldParser(reader);

      tfp.SetDelimiters(delimiters);
      tfp.TrimWhiteSpace = trimWhiteSpace;

      while (!tfp.EndOfData)
        yield return tfp.ReadFields() ?? throw new System.InvalidOperationException();
    }
  }
}
