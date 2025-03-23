namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Enumerates all PropertyInfo matching the specified binding flags. If the source is a System.Type, the properties are enumerated from the type, otherwise the instance is used.</summary>
    /// <param name="source">An instance object or a System.Type to enumerate properties on.</param>
    /// <param name="bindingFlags"></param>
    public static System.Reflection.PropertyInfo[] GetPropertyInfos<T>(this T source, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public)
      where T : notnull
      => source is null
      ? throw new System.ArgumentNullException(nameof(source))
      : source is System.Type type
      ? type.GetProperties(bindingFlags | System.Reflection.BindingFlags.Static) // Static property containers.
      : source.GetType().GetProperties(bindingFlags | System.Reflection.BindingFlags.Instance); // Instance property containers.
  }
}
