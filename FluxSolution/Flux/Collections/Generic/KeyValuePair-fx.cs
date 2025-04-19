namespace Flux
{
  public static partial class KeyValuePairs
  {


    /// <summary>Converts a sequence of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> into a single composite string.</summary>
    public static string ToConsoleString<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, System.Func<TKey, string> keySelector, System.Func<TValue, string> valueSelector, ConsoleFormatOptions? options = null)
      where TKey : notnull
      => source.ToJaggedArray().JaggedToConsoleString(options ?? ConsoleFormatOptions.Default with { HorizontalSeparator = "=" });

    public static object?[][] ToJaggedArray<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, ConsoleFormatOptions? options = null)
      where TKey : notnull
      => source.Select(kvp => new object?[] { kvp.Key, kvp.Value }).ToArray();

    ///// <summary>Create a new <see cref="System.String"/> from the dictionary, in the form "{ Name=Value, ... }".</summary>
    //public static string ToPropertiesJson<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> properties, System.Func<TKey, string> nameSelector, System.Func<TKey, TValue, string> valueSelector)
    //  => $"{{{string.Join(',', (properties ?? throw new System.ArgumentNullException(nameof(properties))).Select(kvp => $"{nameSelector(kvp.Key)}={valueSelector(kvp.Key, kvp.Value)}"))}}}";

    ///// <summary>Create a new <see cref="System.Xml.Linq.XElement"/> from the dictionary, in the form "<Properties><Property Name="" Value="" />...</Properties>".</summary>
    //public static System.Xml.Linq.XElement ToPropertiesXml<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> properties, System.Func<TKey, string> nameSelector, System.Func<TKey, TValue, string> valueSelector)
    //  => new(@"Properties", properties.Select(kvp => new System.Xml.Linq.XElement(@"Property", new System.Xml.Linq.XAttribute(@"Name", nameSelector(kvp.Key)), new System.Xml.Linq.XAttribute(@"Value", valueSelector(kvp.Key, kvp.Value)))));
  }
}
