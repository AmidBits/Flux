namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Uses the built-in <see cref="System.Activator.CreateInstance(Type)"/> to return the default value of <see cref="System.Type"/>.</para>
    /// </summary>
    public static object? CreateInstance(this System.Type source)
      => (source?.IsValueType ?? false)
      ? System.Activator.CreateInstance(source)
      : null;
  }
}
