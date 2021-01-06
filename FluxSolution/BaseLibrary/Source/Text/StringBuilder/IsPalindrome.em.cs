namespace Flux
{
  public static partial class SystemTextEm
  {
    /// <summary>Determines whether the string is a palindrome.</summary>
    public static bool IsPalindrome(this System.Text.StringBuilder source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (int indexL = 0, indexR = source.Length - 1; indexL < indexR; indexL++, indexR--)
        if (source[indexL] != source[indexR])
          return false;

      return true;
    }
  }
}
