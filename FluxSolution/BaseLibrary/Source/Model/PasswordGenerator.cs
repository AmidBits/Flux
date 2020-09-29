using System.Linq;

namespace Flux.Model
{
  public static class PasswordGenerator
  {
    public const string AlphaLower = "abcdefghijklmnopqrstuvwxyz";
    public const string AlphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string Numeric = "0123456789";
    public const string Space = @" ";
    public const string Symbols1 = "!\"#$%&'()*+,-./"; // Note the escaped double quote.
    public const string Symbols2 = ":;<=>?@";
    public const string Symbols3 = "[\\]^_`"; // Note the escaped slash.
    public const string Symbols4 = "{|}~";

    /// <summary>Creates a new password sequence of specified length and various options.</summary>
    public static System.Collections.Generic.IEnumerable<char> GetPassword(bool startWithAlpha, bool allowAlphaLower, bool allowAlphaUpper, bool allowNumeric, bool allowSpace, bool allowSymbols1, bool allowSymbols2, bool allowSymbols3, bool allowSymbols4, int length)
    {
      if (length > 0 && startWithAlpha)
      {
        yield return GetRandomBiased(AlphaLower + AlphaUpper).First();

        length--;
      }

      if (length > 0)
      {
        var characterPool = new System.Text.StringBuilder();

        if (allowAlphaLower) characterPool.Append(AlphaLower);
        if (allowAlphaUpper) characterPool.Append(AlphaUpper);

        if (allowNumeric) characterPool.Append(Numeric);

#pragma warning disable CA1834 // Consider using 'StringBuilder.Append(char)' when applicable
        if (allowSpace) characterPool.Append(Space);
#pragma warning restore CA1834 // Consider using 'StringBuilder.Append(char)' when applicable

        if (allowSymbols1) characterPool.Append(Symbols1);
        if (allowSymbols2) characterPool.Append(Symbols2);
        if (allowSymbols3) characterPool.Append(Symbols3);
        if (allowSymbols4) characterPool.Append(Symbols4);

        foreach (var character in GetRandomBiased(characterPool.ToString()).Take(length))
        {
          yield return character;
        }
      }
    }
    /// <summary>Creates a new password sequence leading with the specified lenth of characters, the the specified length of inner characters, and finally the specified elngth of trailing characters.</summary>
    public static System.Collections.Generic.IEnumerable<char> GetPassword(string leadingPool, int leadingLength, string innerPool, int innerLength, string trailingPool, int trailingLength)
    {
      if (leadingLength > 0)
      {
        foreach (var character in GetRandomBiased(leadingPool).Take(leadingLength))
        {
          yield return character;
        }
      }

      if (innerLength > 0)
      {
        foreach (var character in GetRandomBiased(innerPool).Take(innerLength))
        {
          yield return character;
        }
      }

      if (trailingLength > 0)
      {
        foreach (var character in GetRandomBiased(trailingPool).Take(trailingLength))
        {
          yield return character;
        }
      }
    }

    /// <summary>Creates a new sequence where each char is randomized from the entire set of the specified characters. The more often a char appear in the specified characters the more likely it is to appear in the sequence (i.e. biased).</summary>
    public static System.Collections.Generic.IEnumerable<char> GetRandomBiased(string characterPool)
    {
      for (var index = ushort.MaxValue; index > 0; index--)
      {
        yield return characterPool.RandomElements(1).First();
      }
    }
    /// <summary>Creates a new sequence where all chars from the entire set of specified characters are randomly distributed over and over (unbiased).</summary>
    public static System.Collections.Generic.IEnumerable<char> GetRandomUnbiased(string characterPool)
    {
      for (var index = ushort.MaxValue / characterPool?.Length ?? throw new System.ArgumentNullException(nameof(characterPool)); index > 0; index--)
      {
        foreach (var character in characterPool.Distinct().RandomElements(1))
        {
          yield return character;
        }
      }
    }
  }
}
