namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Enumerates all FieldInfo matching the specified binding flags. If the source is a System.Type, the fields are enumerated from the type, otherwise the instance is used.</summary>
    public static bool TryGetFieldValue(this System.Reflection.FieldInfo source, object? target, out object? result)
    {
      try
      {
        result = target is System.Type
        ? source.GetValue(null) // Static field containers.
        : source.GetValue(target); // Instance field containers.

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
