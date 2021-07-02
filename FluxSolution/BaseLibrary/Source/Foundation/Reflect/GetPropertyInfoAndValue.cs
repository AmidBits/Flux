namespace Flux
{
  public static partial class Reflect
  {
    /// <summary>Enumerates all PropertyInfo matching the specified binding flags, and the respective property values. If a System.Type is specified as the object and the object is a static class, the values are enumerated from the static 'instance'.</summary>
    /// <param name="source">An object or a System.Type to enumerate.</param>
    /// <param name="bindingFlags"></param>
    public static System.Collections.Generic.IDictionary<System.Reflection.PropertyInfo, object?> GetPropertyInfoAndValue(object source, System.Reflection.BindingFlags bindingFlags, System.Func<System.Reflection.PropertyInfo, object?, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var dictionary = new System.Collections.Generic.Dictionary<System.Reflection.PropertyInfo, object?>();

      if (source is System.Type type) // If source is System.Type and not an instance object..
      {
        foreach (var pi in type.GetProperties(bindingFlags | System.Reflection.BindingFlags.Static))
          if (pi.GetValue(null, null) is var value && predicate(pi, value)) // ..then use a null as an object.
            dictionary.Add(pi, value);
      }
      else
      {
        foreach (var pi in source.GetType().GetProperties(bindingFlags | System.Reflection.BindingFlags.Instance))
          if (pi.GetValue(source, System.Array.Empty<object>()) is var value && predicate(pi, value)) // ..otherwise just use the instance.
            dictionary.Add(pi, value); // ..otherwise just use the specified instance.
      }

      return dictionary;
    }
    public static System.Collections.Generic.IDictionary<System.Reflection.PropertyInfo, object?> GetPropertyInfoAndValue(object source, System.Reflection.BindingFlags bindingFlags)
      => GetPropertyInfoAndValue(source, bindingFlags, (pi, v) => true);
  }
}
