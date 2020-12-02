using System.Linq;

namespace Flux
{
  public static partial class IDictionaryEm
  {
    /// <summary></summary>
    public static System.Xml.Linq.XElement ToPropertiesXml(this System.Collections.Generic.IDictionary<string, object> properties, System.Func<object, string> valueSelector)
      => new System.Xml.Linq.XElement(@"Properties", (properties ?? throw new System.ArgumentNullException(nameof(properties))).Select(kvp => new System.Xml.Linq.XElement(@"Property", new System.Xml.Linq.XAttribute(@"Name", kvp.Key), new System.Xml.Linq.XAttribute(@"Value", valueSelector(kvp.Value)))));
  }
}
