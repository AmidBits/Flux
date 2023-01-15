#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    private readonly static System.Numerics.BigInteger[] m_ap = Flux.NumberSequences.PrimeNumber.GetAscendingPrimes(2).Take(100).ToArray(); // Primes.
    private readonly static int[] m_rn = System.Linq.Enumerable.Range(0, 100).ToArray(); // Rational.
    private readonly static int[] m_en = System.Linq.Enumerable.Range(1, 200).Where(i => (i & 1) == 0).ToArray(); // Even.
    private readonly static int[] m_on = System.Linq.Enumerable.Range(1, 200).Where(i => (i & 1) != 0).ToArray(); // Odd.

    /// <summary>Run the amb operator zample.</summary>
    public static void RunAmbOperator()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunAmbOperator));
      System.Console.WriteLine();

      RunAmbOpTesting();
      RunAmbOpZebraPuzzle();
    }

    public static void RunAmbOpTesting()
    {
      for (var i = 0; i < 3; i++)
      {
        AmbTestingImpl();

        System.Console.WriteLine();
      }

      static void AmbTestingImpl()
      {
        var rng = new System.Random();

        var amb = new Flux.AmbOps.Amb();

        #region Flow & Measurements
        m_ap.AsSpan().Shuffle(rng);
        m_rn.AsSpan().Shuffle(rng);
        m_en.AsSpan().Shuffle(rng);
        m_on.AsSpan().Shuffle(rng);

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

    public static void RunAmbOpZebraPuzzle()
    {
      var hints = new string[] {
        "One version of the zebra puzzle:",
        "",
        " 1. There are five houses",
        " 2. The English man lives in the red house",
        " 3. The Swede has a dog",
        " 4. The Dane drinks tea",
        " 5. The green house is immediately to the left of the white house",
        " 6. They drink coffee in the green house",
        " 7. The man who smokes Pall Mall has birds",
        " 8. In the yellow house they smoke Dunhill",
        " 9. In the middle house they drink milk",
        "10. The Norwegian lives in the first house",
        "11. The man who smokes Blend lives in the house next to the house with cats",
        "12. In a house next to the house where they have a horse, they smoke Dunhill",
        "13. The man who smokes Blue Master drinks beer",
        "14. The German smokes Prince",
        "15. The Norwegian lives next to the blue house",
        "16. They drink water in a house next to the house where they smoke Blend"
      };

      hints.WriteToConsole();
      System.Console.WriteLine();

      var amb = new Flux.AmbOps.Amb();

      var domain = new[] { 1, 2, 3, 4, 5 };
      var terms = new System.Collections.Generic.Dictionary<Flux.AmbOps.IValue<int>, string>();

      Flux.AmbOps.IValue<int> Term(string name)
      {
        var x = amb.Choose(domain);
        terms.Add(x, name);
        return x;
      };

      void IsUnequal(params Flux.AmbOps.IValue<int>[] values) => amb.Require(() => values.Select(v => v.Value).Distinct().Count() == 5);
      void IsSame(Flux.AmbOps.IValue<int> left, Flux.AmbOps.IValue<int> right) => amb.Require(() => left.Value == right.Value);
      void IsLeftOf(Flux.AmbOps.IValue<int> left, Flux.AmbOps.IValue<int> right) => amb.Require(() => right.Value - left.Value == 1);
      void IsIn(Flux.AmbOps.IValue<int> attrib, int house) => amb.Require(() => attrib.Value == house);
      void IsNextTo(Flux.AmbOps.IValue<int> left, Flux.AmbOps.IValue<int> right) => amb.Require(() => System.Math.Abs(left.Value - right.Value) == 1);

      Flux.AmbOps.IValue<int> english = Term(nameof(english)), swede = Term(nameof(swede)), dane = Term(nameof(dane)), norwegian = Term(nameof(norwegian)), german = Term(nameof(german));
      IsIn(norwegian, 1); // 10
      IsUnequal(english, swede, german, dane, norwegian);

      Flux.AmbOps.IValue<int> red = Term(nameof(red)), green = Term(nameof(green)), white = Term(nameof(white)), blue = Term(nameof(blue)), yellow = Term(nameof(yellow));
      IsUnequal(red, green, white, blue, yellow);
      IsNextTo(norwegian, blue); // 15
      IsLeftOf(green, white); // 5
      IsSame(english, red); // 2

      Flux.AmbOps.IValue<int> tea = Term(nameof(tea)), coffee = Term(nameof(coffee)), milk = Term(nameof(milk)), beer = Term(nameof(beer)), water = Term(nameof(water));
      IsIn(milk, 3); // 9
      IsUnequal(tea, coffee, milk, beer, water);
      IsSame(dane, tea); // 4
      IsSame(green, coffee); // 6

      Flux.AmbOps.IValue<int> dog = Term(nameof(dog)), cat = Term(nameof(cat)), birds = Term(nameof(birds)), horse = Term(nameof(horse)), zebra = Term(nameof(zebra));
      IsUnequal(dog, cat, birds, horse, zebra);
      IsSame(swede, dog); // 3

      Flux.AmbOps.IValue<int> pallmall = Term(nameof(pallmall)), dunhill = Term(nameof(dunhill)), blend = Term(nameof(blend)), bluemaster = Term(nameof(bluemaster)), prince = Term(nameof(prince));
      IsUnequal(pallmall, dunhill, bluemaster, prince, blend);
      IsSame(pallmall, birds); // 7
      IsSame(dunhill, yellow); // 8
      IsNextTo(blend, cat); // 11
      IsNextTo(horse, dunhill); // 12
      IsSame(bluemaster, beer); // 13
      IsSame(german, prince); // 14
      IsNextTo(water, blend); // 16

      if (!amb.Disambiguate())
      {
        System.Console.WriteLine("No solution found.");
        return;
      }

      var a = new System.Collections.Generic.List<string>[5];
      for (int i = 0; i < 5; i++)
        a[i] = new System.Collections.Generic.List<string>();

      foreach (var (key, value) in terms.Select(kvp => (kvp.Key, kvp.Value)))
        a[key.Value - 1].Add(value);

      var animal = string.Concat(a.Where(l => l.Contains(nameof(zebra))).Select(l => l[0]));
      var drink = string.Concat(a.Where(l => l.Contains(nameof(water))).Select(l => l[0]));

      foreach (var house in a)
      {
        System.Console.Write('|');
        foreach (var attrib in house)
          System.Console.Write($"{attrib,-11}|");
        System.Console.WriteLine();
      }
      System.Console.WriteLine();

      System.Console.WriteLine($"The {animal} owns the zebra.");
      System.Console.WriteLine($"The {drink} drinks water.");
      System.Console.WriteLine();
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
