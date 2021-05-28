using System.Linq;

namespace Flux
{
  public static partial class SystemCollectionsGenericIDictionaryEm
  {
    /// <summary>Create a new <see cref="System.String"/> from the dictionary, in the form "{ Name=Value, ... }".</summary>
    public static string ToPropertiesJson(this System.Collections.Generic.IDictionary<string, object?> properties, System.Func<object?, string> valueSelector)
      => $"{{{string.Join(',', (properties ?? throw new System.ArgumentNullException(nameof(properties))).Select(kvp => $"{kvp.Key}={valueSelector(kvp.Value)}"))}}}";
  }
}
