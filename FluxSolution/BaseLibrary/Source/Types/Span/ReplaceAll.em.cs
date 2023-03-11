using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsSpan
  {
    /// <summary>Replace all values in <paramref name="source"/> using the specified <paramref name="replacementSelector"/>.</summary>
    public static System.Span<T> ReplaceAll<T>(this System.Span<T> source, System.Func<T, int, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = replacementSelector(source[index], index);

      return source;
    }
    /// <summary>Replace all values in <paramref name="source"/> using the specified <paramref name="replacementSelector"/>.</summary>
    public static System.Span<T> ReplaceAll<T>(this System.Span<T> source, System.Func<T, T> replacementSelector) => ReplaceAll(source, (e, i) => replacementSelector(e));

    /// <summary>Replace all values in <paramref name="source"/> that satisfies the <paramref name="predicate"/> with the specified <paramref name="replacementValue"/>.</summary>
    public static System.Span<T> ReplaceAll<T>(this System.Span<T> source, T replacementValue, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index], index))
          source[index] = replacementValue;

      return source;
    }

    /// <summary>Replace all <paramref name="replaceValues"/> in <paramref name="source"/> with <paramref name="replacementValue"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static System.Span<T> ReplaceAll<T>(this System.Span<T> source, T replacementValue, System.Collections.Generic.IList<T> replaceValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return ReplaceAll(source, replacementValue, (e, i) => replaceValues.Contains(e, equalityComparer));
    }
  }
}
