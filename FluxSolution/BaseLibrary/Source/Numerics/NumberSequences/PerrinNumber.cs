#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Yields a Perrin number of the specified value number.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Perrin_number"/>
    public static TSelf GetPerrinNumber<TSelf>(TSelf index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Linq.Enumerable.First(System.Linq.Enumerable.Where(GetPerrinNumbers<TSelf>(), (e, i) => TSelf.CreateChecked(i) == index));

    /// <summary>Creates an indefinite sequence of Perrin numbers.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Perrin_number"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPerrinNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      TSelf a = TSelf.CreateChecked(3), b = TSelf.Zero, c = TSelf.CreateChecked(2);

      yield return a;
      yield return b;
      yield return c;

      while (true)
      {
        var p = a + b;

        a = b;
        b = c;
        c = p;

        yield return p;
      }
    }
  }
}
#endif

//#if NET7_0_OR_GREATER
//namespace Flux.NumberSequences
//{
//  /// <summary>Creates an indefinite sequence of Perrin numbers.</summary>
//  /// <see href="https://en.wikipedia.org/wiki/Perrin_number"/>
//  public sealed class PerrinNumber
//    : INumericSequence<System.Numerics.BigInteger>
//  {
//    #region Static methods

//    /// <summary>Yields a Perrin number of the specified value number.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Perrin_number"/>
//    public static TSelf GetPerrinNumber<TSelf>(TSelf index)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => System.Linq.Enumerable.First(System.Linq.Enumerable.Where(GetPerrinNumbers<TSelf>(), (e, i) => TSelf.CreateChecked(i) == index));

//    /// <summary>Creates an indefinite sequence of Perrin numbers.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Perrin_number"/>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetPerrinNumbers<TSelf>()
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      TSelf a = TSelf.CreateChecked(3), b = TSelf.Zero, c = TSelf.CreateChecked(2);

//      yield return a;
//      yield return b;
//      yield return c;

//      while (true)
//      {
//        var p = a + b;

//        a = b;
//        b = c;
//        c = p;

//        yield return p;
//      }
//    }

//    #endregion Static methods

//    #region Implemented interfaces
//    // INumberSequence
//    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
//      => GetPerrinNumbers<System.Numerics.BigInteger>();


//    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
//      => GetSequence().GetEnumerator();

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//      => GetEnumerator();
//    #endregion Implemented interfaces
//  }
//}
//#endif
