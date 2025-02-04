namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Determines whether the string is a palindrome.</summary>
    public static bool IsPalindrome(this System.Text.StringBuilder source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      for (int indexL = 0, indexR = source.Length - 1; indexL < indexR; indexL++, indexR--)
        if (source[indexL] != source[indexR])
          return false;

      return true;
    }
  }
}
