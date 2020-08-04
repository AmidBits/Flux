namespace Flux
{
  public static partial class XtensionsException
  {
    public static System.Xml.Linq.XDocument ToXDocument(this System.Exception source, string? additionalText = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var xd = new System.Xml.Linq.XDocument();
      xd.Add(new System.Xml.Linq.XElement(@"Error"));
      if (additionalText != null) xd.Root.Add(new System.Xml.Linq.XAttribute(@"AdditionalText", additionalText));
      xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(System.Guid), System.Guid.NewGuid().ToString()));
      xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(source.HResult), source.HResult.ToString(@"X2", System.Globalization.CultureInfo.CurrentCulture)));
      xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(source.Message), source.Message));
      xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(source.Source), source.Source));
      xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(source.StackTrace), source.StackTrace));
      return xd;
    }

    public static System.Xml.XmlDocument ToXmlDocument(this System.Exception source, string? additionalText = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var xd = new System.Xml.XmlDocument();
      xd.AppendChild(xd.CreateElement(@"Error"));
      if (additionalText != null) xd.DocumentElement.SetAttribute(@"AdditionalText", additionalText);
      xd.DocumentElement.SetAttribute(nameof(System.Guid), System.Guid.NewGuid().ToString());
      xd.DocumentElement.SetAttribute(nameof(source.HResult), source.HResult.ToString(@"X2", System.Globalization.CultureInfo.CurrentCulture));
      xd.DocumentElement.SetAttribute(nameof(source.Message), source.Message);
      xd.DocumentElement.SetAttribute(nameof(source.Source), source.Source);
      xd.DocumentElement.SetAttribute(nameof(source.StackTrace), source.StackTrace);
      return xd;
    }
  }
}
