namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Enumerates all FieldInfo matching the specified binding flags. If the source is a System.Type, the fields are enumerated from the type, otherwise the instance is used.</summary>
    /// <param name="source">An instance object or a System.Type to enumerate fields on.</param>
    /// <param name="bindingFlags"></param>
    public static System.Reflection.FieldInfo[] GetFieldInfos(this object source, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
      => source is null
      ? throw new System.ArgumentNullException(nameof(source))
      : source is System.Type type
      ? type.GetFields(bindingFlags | System.Reflection.BindingFlags.Static)
      : source.GetType().GetFields(bindingFlags | System.Reflection.BindingFlags.Instance);

    /// <summary>Tries to enumerate all FieldInfo matching the specified binding flags. If the source is a System.Type, the fields are enumerated from the type, otherwise the instance is used.</summary>
    public static bool TryGetFieldInfos(this object source, out System.Reflection.FieldInfo[] result, System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
    {
      try
      {
        result = GetFieldInfos(source, bindingFlags);
        return true;
      }
      catch
      {
        result = System.Array.Empty<System.Reflection.FieldInfo>();
        return false;
      }
    }
  }
}
