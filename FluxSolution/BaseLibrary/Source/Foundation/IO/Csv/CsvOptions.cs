namespace Flux
{
  public sealed class CsvOptions
  {
    public bool AlwaysEnquote { get; set; }

    public System.Text.Encoding Encoding { get; set; } = System.Text.Encoding.UTF8;

    public char FieldSeparator { get; set; } = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator[0];

    public override string ToString()
      => $"{GetType().Name} {{ AlwaysEnquote = {AlwaysEnquote}, Encoding = {Encoding}, FieldSeparator = {FieldSeparator} }}";
  }
}
