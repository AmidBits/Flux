namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perfect_number"/>
    public static bool IsPerfectNumber(System.Numerics.BigInteger value) => GetSumOfDivisors(value) - value == value;

    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perfect_number"/>
    public static bool IsPerfectNumber(int value) => GetSumOfDivisors(value) - value == value;
    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perfect_number"/>
    public static bool IsPerfectNumber(long value) => GetSumOfDivisors(value) - value == value;

    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perfect_number"/>
    [System.CLSCompliant(false)]
    public static bool IsPerfectNumber(uint value) => GetSumOfDivisors(value) - value == value;
    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perfect_number"/>
    [System.CLSCompliant(false)]
    public static bool IsPerfectNumber(ulong value) => GetSumOfDivisors(value) - value == value;
  }
}
