using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static T[] NormalizeAll<T>(this System.ReadOnlySpan<T> source, T normalizeWith, System.Func<T, bool> predicate)
    {
      var target = source.ToArray();
      NormalizeAll(target, normalizeWith, predicate);
      return target;
    }
    /// <summary>Normalize all sequences of the specified characters throughout the read only span. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public static T[] NormalizeAll<T>(this System.ReadOnlySpan<T> source, T normalizeWith, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] normalize)
      => NormalizeAll(source, normalizeWith, t => normalize.Contains(t, comparer));
    /// <summary>Normalize all sequences of the specified characters throughout the read only span. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the default equality comparer.</summary>
    public static T[] NormalizeAll<T>(this System.ReadOnlySpan<T> source, T normalizeWith, params T[] normalize)
      => NormalizeAll(source, normalizeWith, normalize.Contains);
  }
}
