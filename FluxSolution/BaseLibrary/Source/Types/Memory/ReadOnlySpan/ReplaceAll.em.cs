namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Replace all elements using the specified replacement selector function.</summary>
    public static T[] ReplaceAll<T>(this System.ReadOnlySpan<T> source, System.Func<T, T> replacementSelector)
    {
      var target = source.ToArray();
      ReplaceAll(target, replacementSelector);
      return target;
    }

    /// <summary>Replace all elements satisfying the predicate with the specified character.</summary>
    public static T[] ReplaceAll<T>(this System.ReadOnlySpan<T> source, T replacement, System.Func<T, bool> predicate)
    {
      var target = source.ToArray();
      ReplaceAll(target, replacement, predicate);
      return target;
    }
    /// <summary>Replace all specified elements with the specified element. Uses the specified comparer.</summary>
    public static T[] ReplaceAll<T>(this System.ReadOnlySpan<T> source, T replacement, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, params T[] replace)
      => ReplaceAll(source, replacement, t => System.Array.Exists(replace, e => comparer.Equals(e, t)));
    /// <summary>Replace all specified elements with the specified element. Uses the default comparer.</summary>
    public static T[] ReplaceAll<T>(this System.ReadOnlySpan<T> source, T replacement, params T[] replace)
      => ReplaceAll(source, replacement, System.Collections.Generic.EqualityComparer<T>.Default, replace);
  }
}
