namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Enumerates all FieldInfo matching the specified binding flags and predicate. If the source is a System.Type, the values are enumerated from the type, otherwise the instance is used.</summary>
    /// <param name="source">An object or a System.Type to enumerate fields on.</param>
    /// <param name="bindingFlags"></param>
    /// <param name="predicate"></param>
    public static System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo> GetFieldInfos(object source, System.Func<System.Reflection.FieldInfo, bool> predicate, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var fieldInfos = source is System.Type type ? type.GetFields(bindingFlags | System.Reflection.BindingFlags.Static) : source.GetType().GetFields(bindingFlags | System.Reflection.BindingFlags.Instance);

      foreach (var fieldInfo in fieldInfos)
        if (predicate(fieldInfo))
          yield return fieldInfo;
    }
    /// <summary>Enumerates all FieldInfo matching the specified binding flags. If the source is a System.Type, the values are enumerated from the type, otherwise the instance is used.</summary>
    /// <param name="source">An object or a System.Type to enumerate fields on.</param>
    /// <param name="bindingFlags"></param>
    public static System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo> GetFieldInfos(object source, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
      => GetFieldInfos(source, fi => true, bindingFlags);
  }
}
