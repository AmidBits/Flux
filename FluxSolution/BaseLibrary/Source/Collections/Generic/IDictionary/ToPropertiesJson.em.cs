using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary></summary>
    public static System.Xml.Linq.XElement ToPropertiesJson(this System.Collections.Generic.IDictionary<string, string> properties, string propertiesElementName = @"Properties")
      => new System.Xml.Linq.XElement(propertiesElementName, (properties ?? throw new System.ArgumentNullException(nameof(properties))).Select(kvp => Flux.Text.XmlEx.ToPropertyXmlAsName(kvp.Key,kvp.Value)));
  }
}
