using System.Linq;

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Creates a sieve of divisors up to the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static int[] SieveOfDivisors(int number)
    {
      var array = new int[number];

      for (var i = 1; i < number; i++)
      {
        for (var j = i; j < number; j += i)
        {
          array[j] += 1;
        }
      }

      return array;
    }
  }
}
