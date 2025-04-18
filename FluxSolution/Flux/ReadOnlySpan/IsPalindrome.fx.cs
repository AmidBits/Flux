namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="source"/> is a palindrome. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsPalindrome<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (int indexL = 0, indexR = source.Length - 1; indexL < indexR; indexL++, indexR--)
        if (!equalityComparer.Equals(source[indexL], source[indexR]))
          return false;

      return true;
    }
  }
}
