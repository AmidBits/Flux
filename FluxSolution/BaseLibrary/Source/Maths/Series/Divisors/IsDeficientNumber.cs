namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Deficient_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static bool IsDeficientNumber(System.Numerics.BigInteger value) => GetSumOfDivisors(value) - value < value;

    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Deficient_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static bool IsDeficientNumber(int value) => GetSumOfDivisors(value) - value < value;
    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Deficient_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static bool IsDeficientNumber(long value) => GetSumOfDivisors(value) - value < value;

    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Deficient_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    [System.CLSCompliant(false)]
    public static bool IsDeficientNumber(uint value) => GetSumOfDivisors(value) - value < value;
    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Deficient_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    [System.CLSCompliant(false)]
    public static bool IsDeficientNumber(ulong value) => GetSumOfDivisors(value) - value < value;
  }
}
