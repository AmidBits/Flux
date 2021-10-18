#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    private readonly static System.Numerics.BigInteger[] m_ap = Flux.Numerics.PrimeNumber.GetAscendingPrimes(2).Take(100).ToArray(); // Primes.
    private readonly static int[] m_rn = System.Linq.Enumerable.Range(0, 100).ToArray(); // Rational.
    private readonly static int[] m_en = System.Linq.Enumerable.Range(1, 200).Where(i => (i & 1) == 0).ToArray(); // Even.
    private readonly static int[] m_on = System.Linq.Enumerable.Range(1, 200).Where(i => (i & 1) != 0).ToArray(); // Odd.

    public static void RunAmb()
    {
      for (var i = 0; i < 3; i++)
      {
        AmbTestingImpl();

        System.Console.WriteLine();
      }

      static void AmbTestingImpl()
      {
        var amb = new Flux.AmbOps.Amb();

        #region Flow & Measurements
        m_ap.Shuffle();
        m_rn.Shuffle();
        m_en.Shuffle();
        m_on.Shuffle();

        //var l = a.Length + b.Length + c.Length + d.Length;
        //System.Console.WriteLine($"Length: {l}");
        #endregion

        var x = amb.Choose(m_ap);
        var y = amb.Choose(m_rn);
        var z = amb.Choose(m_en);
        var w = amb.Choose(m_on);
        var answer = 29;

        amb.RequireFinal(() => x.Value + y.Value + z.Value + w.Value == answer);

        //System.Console.WriteLine($"{nameof(amb.Disambiguate)}: {amb.Disambiguate()}");

        System.Console.WriteLine($"{x} + {y} + {z} + {w} == {answer}");
      }
    }
  }
}
#endif

/*
  private static void AmbTest()
  {
    var amb = new Flux.AmbOps.Amb();

    var set1 = amb.Choose("the", "that", "a");
    var set2 = amb.Choose("frog", "tramp", "thing");
    amb.Require(() => set1.Value.Last() == set2.Value[0]);
    var set3 = amb.Choose("walked", "hauled", "treaded", "grows");
    amb.Require(() => set2.Value.Last() == set3.Value[0]);
    var set4 = amb.Choose("slowly", "quickly");
    amb.RequireFinal(() => set3.Value.Last() == set4.Value[0]);

    System.Console.WriteLine($"{set1} {set2} {set3} {set4}");
    System.Console.Read();

    // problem from http://www.randomhacks.net/articles/2005/10/11/amb-operator
    amb = new Flux.AmbOps.Amb();

    var x = amb.Choose(1, 2, 3);
    var y = amb.Choose(4, 5, 6);
    amb.RequireFinal(() => x.Value + y.Value == 8);

    System.Console.WriteLine($"{x} + {y} = 8");
    System.Console.Read();
    System.Console.Read();
  }
*/
