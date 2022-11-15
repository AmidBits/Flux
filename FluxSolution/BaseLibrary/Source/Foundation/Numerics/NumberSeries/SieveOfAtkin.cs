//using System.Linq;

//namespace Flux.Numerics
//{
//  /// <see cref="https://en.wikipedia.org/wiki/Sieve_of_Atkin"/>
//  public sealed class SieveOfAtkin
//    : System.Collections.Generic.IEnumerable<int>
//  {
//    private System.Collections.BitArray m_sieve;
//    /// <summary>Holds the boolean values for each index. Each index represents a number (true means it is a prime number, and false that it is not).</summary>
//    public System.Collections.BitArray Sieve => m_sieve;

//    public System.Collections.Generic.IEnumerable<int> GetPrimes()
//    {
//      for (var index = 2; index < m_sieve.Length; index++)
//        if (m_sieve[index])
//          yield return index;
//    }

//    public bool IsPrime(int number)
//    {
//      if (number > m_sieve.Length)
//        m_sieve = CreateBitArray(number);

//      return m_sieve[number];
//    }

//    public SieveOfAtkin(int length)
//      => m_sieve = CreateBitArray(length);

//    [System.Diagnostics.Contracts.Pure]
//    public static System.Collections.BitArray CreateBitArray(int length)
//    {
//      var bits = new System.Collections.BitArray(length, false);

//      bits[2] = true;
//      bits[3] = true;

//      System.Threading.Tasks.Parallel.For(1, (int)System.Math.Sqrt(length), x =>
//      //for (int x = 1; x * x < length; x++)
//      {
//        var xx = x * x;

//        for (int y = 1; y * y < length; y++)
//        {
//          var yy = y * y;

//          int n = (4 * xx) + yy;
//          if (n <= length && (n % 12 == 1 || n % 12 == 5))
//            bits[(int)n] ^= true;

//          n = (3 * xx) + yy;
//          if (n <= length && n % 12 == 7)
//            bits[(int)n] ^= true;

//          n = (3 * xx) - yy;
//          if (x > y && n <= length && n % 12 == 11)
//            bits[(int)n] ^= true;
//        }
//      }
//      );

//      for (int r = 5, rr = r * r; rr < length; r++, rr = r * r)
//        if (bits[r])
//          for (int i = rr; i < length; i += rr)
//            bits[i] = false;

//      return bits;
//    }

//    // IEnumerable<int>
//    public System.Collections.Generic.IEnumerator<int> GetEnumerator()
//      => m_sieve.GetIndicesEqualToTrue().GetEnumerator();
//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//      => GetEnumerator();
//  }
//}
