namespace Flux
{
  public static partial class XtensionException
  {
    extension(System.Exception source)
    {
      public System.Xml.Linq.XDocument ToXDocument(string? additionalText = null)
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

      public System.Xml.XmlDocument ToXmlDocument(string? additionalText = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var xd = new System.Xml.XmlDocument();

        var xe = xd.CreateElement(@"Error");
        if (additionalText is not null)
          xe.SetAttribute(@"AdditionalText", additionalText);
        xe.SetAttribute(nameof(System.Guid), System.Guid.NewGuid().ToString());
        xe.SetAttribute(nameof(source.HResult), source.HResult.ToString(@"X2", System.Globalization.CultureInfo.CurrentCulture));
        xe.SetAttribute(nameof(source.Message), source.Message);
        if (source.Source is not null)
          xe.SetAttribute(nameof(source.Source), source.Source);
        if (source.StackTrace is not null)
          xe.SetAttribute(nameof(source.StackTrace), source.StackTrace);

        xd.AppendChild(xe);

        return xd;
      }
    }
  }
}
