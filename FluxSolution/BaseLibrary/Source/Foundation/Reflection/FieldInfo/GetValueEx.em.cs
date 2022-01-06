namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns the value from the instance (or type for static objects) using the specified FieldInfo.</summary>
    public static object? GetValueEx(this System.Reflection.FieldInfo source, object instanceOrType)
      => instanceOrType is null
      ? throw new System.ArgumentNullException(nameof(source))
      : instanceOrType is System.Type
      ? source.GetValue(null)
      : source.GetValue(instanceOrType);
  }
}
