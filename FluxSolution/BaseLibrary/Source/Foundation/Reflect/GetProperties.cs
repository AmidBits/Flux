namespace Flux
{
  public static partial class Reflect
  {
    /// <summary>Returns all the properties in a dictionary. To get properties of a static object, specify typeof([StaticObjectName]) as the source.</summary>
    public static System.Collections.Generic.IDictionary<string, object?> GetProperties(object source, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var properties = new System.Collections.Generic.Dictionary<string, object?>();

      foreach (var kvp in GetPropertyInfoAndValue(source, bindingFlags))
        properties.Add(kvp.Key.Name, kvp.Value);

      return properties;
    }
  }
}
