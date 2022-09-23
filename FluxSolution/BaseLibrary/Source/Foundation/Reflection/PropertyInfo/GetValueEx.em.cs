namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns the value from the instance (or type for static objects) using the specified PropertyInfo and indexer values.</summary>
    public static object? GetValueEx(this System.Reflection.PropertyInfo source, object instanceOrType, params object[] indexerValues)
      => instanceOrType is null
      ? throw new System.ArgumentNullException(nameof(source))
      : instanceOrType is System.Type
      ? source.GetValue(null, null)
      : source.GetValue(instanceOrType, indexerValues);
  }
}
