namespace Flux
{
  public static partial class Reflect
  {
    /// <summary>Same as equals but handles null.</summary>
    public static bool EqualsEx<TClass>(this TClass source, TClass target)
      => System.Object.ReferenceEquals(source, target) ? true : source is null || target is null ? source is null && target is null : source.Equals(target);
  }
}
