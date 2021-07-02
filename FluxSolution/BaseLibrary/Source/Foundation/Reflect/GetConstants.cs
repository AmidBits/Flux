namespace Flux
{
  public static partial class Reflect
  {
    /// <summary>Returns all the constants in a dictionary. To get constants of a static object, specify typeof([StaticObjectName]) as the source.</summary>
    public static System.Collections.Generic.IDictionary<string, object?> GetConstants(object source, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var constants = new System.Collections.Generic.Dictionary<string, object?>();

      foreach (var kvp in GetFieldInfoAndValue(source, bindingFlags, (fi, v) => fi.IsConstant()))
        constants.Add(kvp.Key.Name, kvp.Value);

      return constants;
    }
  }
}
