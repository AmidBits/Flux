namespace Flux.Text.Csv
{
  public class CsvOptions
  {
    public bool AlwaysEnquote { get; set; }

    public System.Text.Encoding Encoding { get; set; } = System.Text.Encoding.UTF8;

    public char FieldSeparator { get; set; } = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator[0];
  }
}
