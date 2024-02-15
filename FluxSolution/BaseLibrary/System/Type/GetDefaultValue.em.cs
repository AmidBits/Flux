namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the default value, like the built-in keyword <see cref="default"/> does, but from a <see cref="System.Type"/> (<paramref name="source"/>).</summary>
    public static object? GetDefaultValue(this System.Type source)
      => (source?.IsValueType ?? false)
      ? System.Activator.CreateInstance(source)
      : null;
  }
}
