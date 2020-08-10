using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary></summary>
    public static System.Xml.Linq.XElement ToPropertiesXml<T>(this System.Collections.Generic.IDictionary<string, T> properties)
      => new System.Xml.Linq.XElement(@"Properties", properties.Select(kvp => kvp.ToPropertyXml()));
    public static System.Xml.Linq.XElement ToPropertyXml<TKey, TValue>(this System.Collections.Generic.KeyValuePair<TKey, TValue> property)
      => new System.Xml.Linq.XElement(@"Property", new System.Xml.Linq.XAttribute(@"Name", property.Key.ToPropertyXmlValue()), new System.Xml.Linq.XAttribute(@"Value", property.Value.ToPropertyXmlValue()));
    public static string ToPropertyXmlValue<T>(this T value)
      => value switch
      {
        System.DateTime dt => dt.ToString($"o"),
        _ => (value?.ToString() ?? string.Empty)
      };
  }
}
