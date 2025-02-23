namespace Flux
{
  public static partial class Fx
  {
    public static System.Xml.Linq.XDocument ToXDocument(this System.Exception source, string? additionalText = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var xe = new System.Xml.Linq.XElement(@"Error");
      if (additionalText is not null)
        xe.SetAttributeValue(@"AdditionalText", additionalText);
      xe.SetAttributeValue(nameof(System.Guid), System.Guid.NewGuid().ToString());
      xe.SetAttributeValue(nameof(source.HResult), source.HResult.ToString(@"X2", System.Globalization.CultureInfo.CurrentCulture));
      xe.SetAttributeValue(nameof(source.Message), source.Message);
      if (source.Source is not null)
        xe.SetAttributeValue(nameof(source.Source), source.Source);
      if (source.StackTrace is not null)
        xe.SetAttributeValue(nameof(source.StackTrace), source.StackTrace);

      return new System.Xml.Linq.XDocument(xe);
    }
  }
}
