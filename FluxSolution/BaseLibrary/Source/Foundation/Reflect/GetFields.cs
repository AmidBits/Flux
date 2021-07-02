namespace Flux
{
  public static partial class Reflect
  {
    /// <summary>Returns all the fields in a dictionary. To get fields of a static object, specify typeof([StaticObjectName]) as the source.</summary>
    public static System.Collections.Generic.IDictionary<string, object?> GetFields(object source, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var fields = new System.Collections.Generic.Dictionary<string, object?>();

      foreach (var kvp in GetFieldInfoAndValue(source, bindingFlags))
        fields.Add(kvp.Key.Name, kvp.Value);

      return fields;
    }
  }
}
