namespace Flux.Model
{
  //public interface IPasswordGenerator
  //{
  //  public System.Collections.Generic.IEnumerable<char> GeneratePassword(int length);
  //}

  //public class PasswordGeneratorLiteral
  //  : IPasswordGenerator
  //{
  //  public string AllowedFirst { get; init; };
  //  public string AllowedMiddle { get; init; };
  //  public string AllowedLast { get; init; };

  //  public IEnumerable<char> GeneratePassword(int length) 
  //    => throw new System.NotImplementedException();
  //}

  public sealed class PasswordGenerator
  {
    public const string AlphaLower = "abcdefghijklmnopqrstuvwxyz";
    public const string AlphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string Numeric = "0123456789";
    public const char Space = ' ';
    public const string Symbols1 = "!\"#$%&'()*+,-./"; // Note the escaped double quote.
    public const string Symbols2 = ":;<=>?@";
    public const string Symbols3 = "[\\]^_`"; // Note the escaped slash.
    public const string Symbols4 = "{|}~";

    public bool AllowAlphaLower { get; set; } = true;
    public bool AllowAlphaUpper { get; set; } = true;
    public bool AllowNumeric { get; set; } = true;
    public bool AllowSpace { get; set; }
    public bool AllowSymbols1 { get; set; }
    public bool AllowSymbols2 { get; set; }
    public bool AllowSymbols3 { get; set; }
    public bool AllowSymbols4 { get; set; }

    public bool ForceStartWithAlpha { get; set; }

    public System.Random RandomNumberGenerator { get; init; } = System.Random.Shared;

    /// <summary>Creates a new password sequence of specified length and various options.</summary>
    public System.Collections.Generic.IEnumerable<char> GetPassword(int length)
    {
      if (length > 0 && ForceStartWithAlpha)
      {
        yield return GetRandomBiased(AlphaLower + AlphaUpper, RandomNumberGenerator).First();

        length--;
      }

      if (length > 0)
      {
        var characterPool = new System.Text.StringBuilder();

        if (AllowAlphaLower) characterPool.Append(AlphaLower);
        if (AllowAlphaUpper) characterPool.Append(AlphaUpper);

        if (AllowNumeric) characterPool.Append(Numeric);
        if (AllowSpace) characterPool.Append(Space);
        if (AllowSymbols1) characterPool.Append(Symbols1);
        if (AllowSymbols2) characterPool.Append(Symbols2);
        if (AllowSymbols3) characterPool.Append(Symbols3);
        if (AllowSymbols4) characterPool.Append(Symbols4);

        foreach (var character in GetRandomBiased(characterPool.ToString(), RandomNumberGenerator).Take(length))
        {
          yield return character;
        }
      }
    }
    /// <summary>Creates a new password sequence leading with the specified lenth of characters, the the specified length of inner characters, and finally the specified elngth of trailing characters.</summary>
    public static System.Collections.Generic.IEnumerable<char> GetPassword(string leadingPool, int leadingLength, string innerPool, int innerLength, string trailingPool, int trailingLength, System.Random rng)
    {
      if (leadingLength > 0)
      {
        foreach (var character in GetRandomBiased(leadingPool, rng).Take(leadingLength))
        {
          yield return character;
        }
      }

      if (innerLength > 0)
      {
        foreach (var character in GetRandomBiased(innerPool, rng).Take(innerLength))
        {
          yield return character;
        }
      }

      if (trailingLength > 0)
      {
        foreach (var character in GetRandomBiased(trailingPool, rng).Take(trailingLength))
        {
          yield return character;
        }
      }
    }

    /// <summary>Creates a new sequence where each char is randomized from the entire set of the specified characters. The more often a char appear in the specified characters the more likely it is to appear in the sequence (i.e. biased).</summary>
    public static System.Collections.Generic.IEnumerable<char> GetRandomBiased(string characterPool, System.Random rng)
    {
      for (var index = ushort.MaxValue; index > 0; index--)
      {
        yield return characterPool.GetRandomElements(1, rng).First();
      }
    }
    /// <summary>Creates a new sequence where all chars from the entire set of specified characters are randomly distributed over and over (unbiased).</summary>
    public static System.Collections.Generic.IEnumerable<char> GetRandomUnbiased(string characterPool, System.Random rng)
    {
      if (characterPool is null) throw new System.ArgumentNullException(nameof(characterPool));

      for (var index = ushort.MaxValue / characterPool.Length; index > 0; index--)
      {
        foreach (var character in characterPool.Distinct().GetRandomElements(1, rng))
        {
          yield return character;
        }
      }
    }
  }
}
