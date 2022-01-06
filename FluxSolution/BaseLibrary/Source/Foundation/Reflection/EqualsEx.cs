namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Same as equals but handles null.</summary>
    public static bool EqualsEx<TClass>(this TClass source, TClass target)
      => System.Object.ReferenceEquals(source, target) || (source is null || target is null ? source is null && target is null : source.Equals(target));
  }
}
