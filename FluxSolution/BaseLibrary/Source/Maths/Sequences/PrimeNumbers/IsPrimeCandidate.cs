namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Indicates whether a specified number is a prime candidate.</summary>
    public static bool IsPrimeCandidate(System.Numerics.BigInteger source)
      => source % 6 is var remainder && (remainder == 5 || remainder == 1);
    /// <summary>Indicates whether a specified number is a prime candidate, and also returns the properties of "6n-1"/"6n+1".</summary>
    public static bool IsPrimeCandidate(System.Numerics.BigInteger source, out System.Numerics.BigInteger multiplier, out System.Numerics.BigInteger offset)
    {
      multiplier = System.Numerics.BigInteger.DivRem(source, 6, out offset);

      if (offset == 5)
      {
        multiplier++;
        offset = -1;

        return true;
      }
      else return offset == 1;
    }

    /// <summary>Indicates whether a specified number is a prime candidate.</summary>
    public static bool IsPrimeCandidate(int source)
      => source % 6 is var remainder && (remainder == 5 || remainder == 1);
    /// <summary>Indicates whether a specified number is a prime candidate, and also returns the properties of "6n-1"/"6n+1".</summary>
    public static bool IsPrimeCandidate(int source, out int multiplier, out int offset)
    {
      multiplier = System.Math.DivRem(source, 6, out offset);

      if (offset == 5)
      {
        multiplier++;
        offset = -1;

        return true;
      }
      else return offset == 1;
    }

    /// <summary>Indicates whether a specified number is a prime candidate.</summary>
    public static bool IsPrimeCandidate(long source)
      => source % 6 is var remainder && (remainder == 5 || remainder == 1);
    /// <summary>Indicates whether a specified number is a prime candidate, and also returns the properties of "6n-1"/"6n+1".</summary>
    public static bool IsPrimeCandidate(long source, out long multiplier, out long offset)
    {
      multiplier = System.Math.DivRem(source, 6, out offset);

      if (offset == 5)
      {
        multiplier++;
        offset = -1;

        return true;
      }
      else return offset == 1;
    }
  }
}
