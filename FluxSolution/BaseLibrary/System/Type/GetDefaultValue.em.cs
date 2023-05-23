namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns the default value, like the default(T) does but from the <paramref name="source"/>.</summary>
    public static object? GetDefaultValue(this System.Type source)
      => (source?.IsValueType ?? false)
      ? System.Activator.CreateInstance(source)
      : null;
  }
}
