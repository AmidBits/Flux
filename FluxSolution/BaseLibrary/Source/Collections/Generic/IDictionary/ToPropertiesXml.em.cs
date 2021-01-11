using System.Linq;

namespace Flux
{
  public static partial class SystemCollectionsGenericEm
  {
    /// <summary>Create a new <see cref="System.Xml.Linq.XElement"/> from the dictionary, in the form "<Properties><Property Name="" Value="" />...</Properties>".</summary>
    public static System.Xml.Linq.XElement ToPropertiesXml(this System.Collections.Generic.IDictionary<string, object?> properties, System.Func<object?, string> valueSelector)
      => new System.Xml.Linq.XElement(@"Properties", (properties ?? throw new System.ArgumentNullException(nameof(properties))).Select(kvp => new System.Xml.Linq.XElement(@"Property", new System.Xml.Linq.XAttribute(@"Name", kvp.Key), new System.Xml.Linq.XAttribute(@"Value", valueSelector(kvp.Value)))));
  }
}
