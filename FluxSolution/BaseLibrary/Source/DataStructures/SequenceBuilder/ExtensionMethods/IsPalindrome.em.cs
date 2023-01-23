namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Determines whether the string is a palindrome.</summary>
    public static bool IsPalindrome<T>(this SequenceBuilder<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (int indexL = 0, indexR = source.Length - 1; indexL < indexR; indexL++, indexR--)
        if (!equalityComparer.Equals(source[indexL], source[indexR]))
          return false;

      return true;
    }
  }
}
