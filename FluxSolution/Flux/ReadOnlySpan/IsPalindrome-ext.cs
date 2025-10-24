namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"^(?'letter'\p{L})+\p{L}?(?:\k'letter'(?'-letter'))+(?(letter)(?!))$")]
    public static partial System.Text.RegularExpressions.Regex RegexPalindrome();

    extension(System.ReadOnlySpan<char> source)
    {
      #region IsPalindrome

      /// <summary>Matches palindromes of any length.</summary>
      /// <see href="https://www.regular-expressions.info/balancing.html"/>
      public bool IsPalindrome()
        => RegexPalindrome().IsMatch(source);

      #endregion
    }

    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region IsPalindrome

      /// <summary>
      /// <para>Determines whether the <paramref name="source"/> is a palindrome. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public bool IsPalindrome(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        for (int indexL = 0, indexR = source.Length - 1; indexL < indexR; indexL++, indexR--)
          if (!equalityComparer.Equals(source[indexL], source[indexR]))
            return false;

        return true;
      }

      #endregion
    }
  }
}
