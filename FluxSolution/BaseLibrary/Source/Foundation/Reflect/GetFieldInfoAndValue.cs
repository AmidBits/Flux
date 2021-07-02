namespace Flux
{
  public static partial class Reflect
  {
    /// <summary>Enumerates all FieldInfo matching the specified binding flags, and the respective field values. If a System.Type is specified as the object and the object is a static class, the values are enumerated from the static 'instance'.</summary>
    /// <param name="source">An object or a System.Type to enumerate.</param>
    /// <param name="bindingFlags"></param>
    public static System.Collections.Generic.IDictionary<System.Reflection.FieldInfo, object?> GetFieldInfoAndValue(object source, System.Reflection.BindingFlags bindingFlags, System.Func<System.Reflection.FieldInfo, object?, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var fieldInfos = new System.Collections.Generic.Dictionary<System.Reflection.FieldInfo, object?>();

      if (source is System.Type type) // If source is System.Type and not an instance object..
      {
        foreach (var fi in type.GetFields(bindingFlags | System.Reflection.BindingFlags.Static))
          if (fi.GetValue(null) is var value && predicate(fi, value)) // ..then use a null as an object.
            fieldInfos.Add(fi, value);
      }
      else
      {
        foreach (var fi in source.GetType().GetFields(bindingFlags | System.Reflection.BindingFlags.Instance))
          if (fi.GetValue(source) is var value && predicate(fi, value)) // ..otherwise just use the instance.
            fieldInfos.Add(fi, value);
      }

      return fieldInfos;
    }
    public static System.Collections.Generic.IDictionary<System.Reflection.FieldInfo, object?> GetFieldInfoAndValue(object source, System.Reflection.BindingFlags bindingFlags)
      => GetFieldInfoAndValue(source, bindingFlags, (fi, v) => true);
  }
}
