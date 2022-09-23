namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Enumerates all PropertyInfo matching the specified binding flags and predicate. If the source is a System.Type, the values are enumerated from the type, otherwise the instance is used.</summary>
    /// <param name="source">An object or a System.Type to enumerate properties on.</param>
    /// <param name="bindingFlags"></param>
    /// <param name="predicate"></param>
    public static System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> GetPropertyInfos(object source, System.Func<System.Reflection.PropertyInfo, bool> predicate, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var propertyInfos = source is System.Type type ? type.GetProperties(bindingFlags | System.Reflection.BindingFlags.Static) : source.GetType().GetProperties(bindingFlags | System.Reflection.BindingFlags.Instance);

      foreach (var propertyInfo in propertyInfos)
        if (predicate(propertyInfo))
          yield return propertyInfo;
    }
  }
}
