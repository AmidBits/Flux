namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Determines whether the type is a static class, based on it being abstract and sealed.</summary>
    public static bool IsStaticClass(this System.Type source)
      => source is null
      ? throw new System.ArgumentNullException(nameof(source))
      : source.IsClass && source.IsAbstract && source.IsSealed;
  }
}
