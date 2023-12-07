namespace Flux
{
  public static partial class LinqX
  {
    public static T[] ImplicitJoin<T>(params T[][] source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sourceLength = source.Length;

      var ij = System.Array.Empty<T>();
      for (var index = 0; index < sourceLength; index++)
        ij = index == 0 ? source[index] : ij.Join(source[index], outer => outer, inner => inner, (outer, inner) => inner).ToArray();
      return ij;
    }
  }
}
