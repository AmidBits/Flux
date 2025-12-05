#if DEBUG
namespace Flux
{
  public static partial class Zamples
  {
    public static void AmbOpGeneralTesting()
    {
      for (var i = 0; i < 3; i++)
      {
        AmbTestingImpl();

        System.Console.WriteLine();
      }

      static void AmbTestingImpl()
      {
        var rng = new System.Random();

        var m_ap = 2.GetAscendingPrimes().Take(100).ToArray(); // Primes.
        var m_rn = System.Linq.Enumerable.Range(0, 100).ToArray(); // Natural.
        var m_en = System.Linq.Enumerable.Range(1, 200).Where(i => (i & 1) == 0).ToArray(); // Even.
        var m_on = System.Linq.Enumerable.Range(1, 200).Where(i => (i & 1) != 0).ToArray(); // Odd.

        var amb = new Flux.AmbOperator.Amb();

        #region Flow & Measurements
        rng.Shuffle(m_ap);
        rng.Shuffle(m_rn);
        rng.Shuffle(m_en);
        rng.Shuffle(m_on);

        //var l = a.Length + b.Length + c.Length + d.Length;
        //System.Console.WriteLine($"Length: {l}");
        #endregion

        var x = amb.Choose(m_ap);
        var y = amb.Choose(m_rn);
        var z = amb.Choose(m_en);
        var w = amb.Choose(m_on);
        var answer = 291;

        var isRequireFinal = amb.RequireFinal(() => x.Value + y.Value * z.Value - w.Value == answer);
        System.Console.WriteLine($"{nameof(amb.RequireFinal)} == {isRequireFinal} : {x} + ({y} * {z}) - {w} == {answer}");

        var isDisambiguate = amb.Disambiguate();
        System.Console.WriteLine($"{nameof(amb.Disambiguate)} == {isDisambiguate}");
      }
    }

    public static void AmbOpZebraPuzzle()
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

      System.Diagnostics.Debug.WriteLine(hints.ToConsoleString<string>().ToString());
      System.Console.WriteLine();

      var amb = new Flux.AmbOperator.Amb();

      var domain = new[] { 1, 2, 3, 4, 5 };
      var terms = new System.Collections.Generic.Dictionary<Flux.AmbOperator.IValue<int>, string>();

      Flux.AmbOperator.IValue<int> Term(string name)
      {
        var x = amb.Choose(domain);
        terms.Add(x, name);
        return x;
      }
      ;

      void AreUnique(params Flux.AmbOperator.IValue<int>[] values) => amb.Require(() => values.Select(v => v.Value).Distinct().Count() == 5);
      void IsSame(Flux.AmbOperator.IValue<int> left, Flux.AmbOperator.IValue<int> right) => amb.Require(() => left.Value == right.Value);
      void IsLeftOf(Flux.AmbOperator.IValue<int> left, Flux.AmbOperator.IValue<int> right) => amb.Require(() => right.Value - left.Value == 1);
      void IsIn(Flux.AmbOperator.IValue<int> attrib, int house) => amb.Require(() => attrib.Value == house);
      void IsNextTo(Flux.AmbOperator.IValue<int> left, Flux.AmbOperator.IValue<int> right) => amb.Require(() => System.Math.Abs(left.Value - right.Value) == 1);

      Flux.AmbOperator.IValue<int> english = Term(nameof(english)), swede = Term(nameof(swede)), dane = Term(nameof(dane)), norwegian = Term(nameof(norwegian)), german = Term(nameof(german));
      AreUnique(english, swede, german, dane, norwegian); // Unique values.
      IsIn(norwegian, 1); // 10

      Flux.AmbOperator.IValue<int> red = Term(nameof(red)), green = Term(nameof(green)), white = Term(nameof(white)), blue = Term(nameof(blue)), yellow = Term(nameof(yellow));
      AreUnique(red, green, white, blue, yellow); // Unique values.
      IsNextTo(norwegian, blue); // 15
      IsLeftOf(green, white); // 5
      IsSame(english, red); // 2

      Flux.AmbOperator.IValue<int> tea = Term(nameof(tea)), coffee = Term(nameof(coffee)), milk = Term(nameof(milk)), beer = Term(nameof(beer)), water = Term(nameof(water));
      AreUnique(tea, coffee, milk, beer, water); // Unique values.
      IsIn(milk, 3); // 9
      IsSame(dane, tea); // 4
      IsSame(green, coffee); // 6

      Flux.AmbOperator.IValue<int> dog = Term(nameof(dog)), cat = Term(nameof(cat)), birds = Term(nameof(birds)), horse = Term(nameof(horse)), zebra = Term(nameof(zebra));
      AreUnique(dog, cat, birds, horse, zebra); // Unique values.
      IsSame(swede, dog); // 3

      Flux.AmbOperator.IValue<int> pallmall = Term(nameof(pallmall)), dunhill = Term(nameof(dunhill)), blend = Term(nameof(blend)), bluemaster = Term(nameof(bluemaster)), prince = Term(nameof(prince));
      AreUnique(pallmall, dunhill, bluemaster, prince, blend); // Unique values.
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
        a[i] = [];

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
