namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Determines whether the sequence is a palindrome. Uses the specified comparer.</summary>
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
