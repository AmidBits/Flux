namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static T[] NormalizeAdjacent<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      var target = source.ToArray();
      NormalizeAdjacent(target, comparer, values);
      return target;
    }
    public static T[] NormalizeAdjacent<T>(this System.ReadOnlySpan<T> source, params T[] normalize)
      => NormalizeAdjacent(source, System.Collections.Generic.EqualityComparer<T>.Default, normalize);
  }
}
