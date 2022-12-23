namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Enumerates all FieldInfo matching the specified binding flags. If the source is a System.Type, the fields are enumerated from the type, otherwise the instance is used.</summary>
    public static bool TryGetPropertyValue(this System.Reflection.PropertyInfo source, object? target, out object? result)
    {
      try
      {
        result = target is System.Type
        ? source.GetValue(null) // Static property containers.
        : source.GetValue(target); // Instance property containers.

        return true;
      }
      catch
      {
        result = default;

        return false;
      }
    }
  }
}
