//using System.Linq;

//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Returns a sequence of prime quadruplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8}.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Prime_quadruplet"/>
//    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, int Index)> GetPrimeQuadruplets()
//    {
//      var index = 0;

//      var list = new System.Collections.Generic.List<System.Numerics.BigInteger>();

//      foreach (var primeNumber in GetAscendingPrimes(SmallestPrime))
//      {
//        list.Add(primeNumber);

//        if (list.Count == 4)
//        {
//          if (list[1] - list[0] == 2 && list[2] - list[0] == 6 && list[3] - list[0] == 8)
//          {
//            yield return (list[0], list[1], list[2], list[3], index++);
//          }

//          list.RemoveAt(0);
//        }
//      }
//    }
//  }
//}
