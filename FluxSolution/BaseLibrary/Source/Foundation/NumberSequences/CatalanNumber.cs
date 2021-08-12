namespace Flux.Numerics
{
  public class CatalanNumber
  : ASequencedNumbers<System.Numerics.BigInteger>
  {
    // INumberSequence
    /// <summary>Creates a new sequence with Catalan numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
    {
      for (var number = System.Numerics.BigInteger.Zero; ; number++)
        yield return GetCatalanNumber(number);
    }

    #region Statics

    /// <summary>Returns the Catalan number for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>
    public static System.Numerics.BigInteger GetCatalanNumber(System.Numerics.BigInteger number)
      => Maths.Factorial(number * 2) / (Maths.Factorial(number + 1) * Maths.Factorial(number));

    #endregion Statics
  }

  //public static partial class Maths
  // {
  //   /// <summary>Returns the Catalan number for the specified number.</summary>
  //   /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>
  //   public static System.Numerics.BigInteger GetCatalanNumber(System.Numerics.BigInteger number)
  //     => Factorial(number * 2) / (Factorial(number + 1) * Factorial(number));

  //   /// <summary>Creates a new sequence with Catalan numbers.</summary>
  //   /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>
  //   /// <remarks>This function runs indefinitely, if allowed.</remarks>
  //   public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetCatalanSequence()
  //   {
  //     for (var number = System.Numerics.BigInteger.Zero; ; number++)
  //       yield return GetCatalanNumber(number);
  //   }
  // }
}
