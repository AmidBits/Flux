namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines whether the <paramref name="source"/> is a supertype of <paramref name="subtype"/>.</summary>
    public static bool IsSupertypeOf(this System.Type source, System.Type subtype)
      => IsSubtypeOf(subtype, source);
  }
}
