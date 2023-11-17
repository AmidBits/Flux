using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsSpan
  {
    /// <summary>Replace all values in <paramref name="source"/> using the specified <paramref name="replacementSelector"/>.</summary>
    public static System.Span<T> ReplaceAll<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, System.Func<T, int, T> replacementSelector)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = 0; index < source.Length; index++)
      {
        var item = source[index];

        if (predicate(item, index))
          source[index] = replacementSelector(item, index);
      }

      return source;
    }
  }
}
