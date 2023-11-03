//using System.Linq;

//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    /// <summary>Create a new <see cref="System.Xml.Linq.XElement"/> from the dictionary, in the form "<Properties><Property Name="" Value="" />...</Properties>".</summary>
//    public static System.Xml.Linq.XElement ToPropertiesXml<TKey, TValue>(this System.Collections.Generic.KeyValuePair<TKey, TValue> properties, System.Func<TKey, string> nameSelector, System.Func<TKey, TValue, string> valueSelector)
//      where TKey : notnull
//      => new(@"Properties", (properties ?? throw new System.ArgumentNullException(nameof(properties))).Select(kvp => new System.Xml.Linq.XElement(@"Property", new System.Xml.Linq.XAttribute(@"Name", nameSelector(kvp.Key)), new System.Xml.Linq.XAttribute(@"Value", valueSelector(kvp.Key, kvp.Value)))));
//  }
//}
