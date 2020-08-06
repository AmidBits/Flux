namespace Flux.Text
{
  public class CsvOptions
  {
    public System.Text.Encoding Encoding { get; private set; } = System.Text.Encoding.UTF8;

    public System.Collections.Generic.List<char> EscapeCharacters { get; private set; } = new System.Collections.Generic.List<char>() { '"', ',', '\r', '\n' };

    public char FieldSeparator { get; private set; } = ',';
  }
}
