namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Replace all values in <paramref name="source"/> using the specified <paramref name="replacementSelector"/>.</summary>
    public static System.Span<T> ReplaceAll<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, System.Func<T, int, T> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

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
