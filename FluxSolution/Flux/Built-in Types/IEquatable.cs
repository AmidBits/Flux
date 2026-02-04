namespace Flux
{
  public static class IEquatableExtensions
  {
    public static bool EqualsDefault<IEquatable>(this IEquatable source)
      where IEquatable : System.IEquatable<IEquatable>
      => source.Equals(default);
  }
}
