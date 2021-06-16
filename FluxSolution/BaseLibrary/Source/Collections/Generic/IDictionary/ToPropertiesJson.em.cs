using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Create a new <see cref="System.String"/> from the dictionary, in the form "{ Name=Value, ... }".</summary>
    public static string ToPropertiesJson<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> properties, System.Func<TKey, string> nameSelector, System.Func<TKey, TValue, string> valueSelector)
      => $"{{{string.Join(',', (properties ?? throw new System.ArgumentNullException(nameof(properties))).Select(kvp => $"{nameSelector(kvp.Key)}={valueSelector(kvp.Key, kvp.Value)}"))}}}";
  }
}
