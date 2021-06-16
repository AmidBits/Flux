namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of XML attributes for each column in the current row.</summary>
    public static System.Collections.Generic.IEnumerable<System.Xml.Linq.XAttribute?> GetXmlAttributes(this System.Data.IDataRecord source, System.Func<string, string> nameSelector, System.Func<object, string> valueSelector)
      => GetFields(source, (idr, i) => idr.IsDBNull(i) ? null : new System.Xml.Linq.XAttribute(nameSelector(idr.GetName(i)), valueSelector(idr.GetValue(i))));

    /// <summary>Creates a new sequence of XML elements for each column in the current row.</summary>
    public static System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement?> GetXmlElements(this System.Data.IDataRecord source, System.Func<string, string> nameSelector, System.Func<object, string> valueSelector)
      => GetFields(source, (idr, i) => idr.IsDBNull(i) ? null : new System.Xml.Linq.XElement(nameSelector(idr.GetName(i)), valueSelector(idr.GetValue(i))));

    /// <summary>Creates a new sequence of XML elements for each column in the current row.</summary>
    public static System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement?> GetXmlNameValueElements(this System.Data.IDataRecord source, System.Func<string, string> nameSelector, System.Func<object, string> valueSelector, string tagName = @"Property")
      => GetFields(source, (idr, i) => idr.IsDBNull(i) ? null : new System.Xml.Linq.XElement(tagName, new System.Xml.Linq.XAttribute(@"Name", nameSelector(idr.GetName(i))), new System.Xml.Linq.XAttribute(@"Value", valueSelector(idr.GetValue(i)))));
  }
}
