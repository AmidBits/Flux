namespace Flux.Text
{
  public class CsvOptions
  {
    public bool AlwaysEnquote { get; set; }

    public System.Text.Encoding Encoding { get; set; } = System.Text.Encoding.UTF8;

    public char FieldSeparator { get; set; } = ',';
  }
}
