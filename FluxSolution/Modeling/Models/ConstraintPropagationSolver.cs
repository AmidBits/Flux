namespace Flux
{
  public static class ConstraintPropagationSolver
  {
    public enum EnumColor
    {
      Unkown,
      Blue,
      Green,
      Red,
      White,
      Yellow,
    }

    public enum EnumNationality
    {
      Unkown,
      Brit,
      Dane,
      German,
      Norwegian,
      Swede,
    }

    public enum EnumDrink
    {
      Unkown,
      Beer,
      Coffee,
      Milk,
      Tea,
      Water,
    }

    public enum EnumSmoke
    {
      Unkown,
      Blends,
      BlueMaster,
      Dunhill,
      PallMall,
      Prince,
    }

    public enum EnumAnimal
    {
      Unkown,
      Bird,
      Cat,
      Dog,
      Fish,
      Horse,
    }

    // This is a class that holds some fact (by one or more houses).
    public sealed class Fact
    {
      public System.Collections.Generic.List<int> House = new() { 1, 2, 3, 4, 5 };
      public EnumColor Color;
      public EnumNationality Nationality;
      public EnumDrink Drink;
      public EnumSmoke Smoke;
      public EnumAnimal Animal;
    }

    // These are known facts.
    public readonly static System.Collections.Generic.IList<Fact> KnownFacts = new System.Collections.Generic.List<Fact>()
    {
      new Fact() { Color = EnumColor.Red, Nationality = EnumNationality.Brit },
      new Fact() { Nationality = EnumNationality.Swede, Animal = EnumAnimal.Dog },
      new Fact() { Nationality = EnumNationality.Dane, Drink = EnumDrink.Tea },
      new Fact() { House = new System.Collections.Generic.List<int>() { 3, 4 }, Color = EnumColor.Green, Drink = EnumDrink.Coffee },
      new Fact() { Smoke = EnumSmoke.PallMall, Animal = EnumAnimal.Bird },
      new Fact() { Color = EnumColor.Yellow, Smoke = EnumSmoke.Dunhill },
      new Fact() { House = new System.Collections.Generic.List<int>() { 3 }, Drink = EnumDrink.Milk },
      new Fact() { House = new System.Collections.Generic.List<int>() { 1 }, Nationality = EnumNationality.Norwegian },
      new Fact() { Drink = EnumDrink.Beer, Smoke = EnumSmoke.BlueMaster },
      new Fact() { Nationality = EnumNationality.German, Smoke = EnumSmoke.Prince },
      new Fact() { House = new System.Collections.Generic.List<int>() { 2 }, Color = EnumColor.Blue, Animal = EnumAnimal.Horse },
      new Fact() { House = new System.Collections.Generic.List<int>() { 4, 5 }, Color = EnumColor.White },
      new Fact() { Smoke = EnumSmoke.Blends },
      new Fact() { Drink = EnumDrink.Water },
      new Fact() { Animal = EnumAnimal.Cat },
      new Fact() { House = new System.Collections.Generic.List<int>() { 4 }, Animal = EnumAnimal.Fish },
    };

    // This only renders the data as a "table" on the screen.
    public static void RenderKnownFacts()
    {
      var matrix = new object[6, KnownFacts.Count];

      for (var index = 0; index < KnownFacts.Count; index++)
      {
        var kf = KnownFacts[index];

        matrix[0, index] = string.Join(',', kf.House);
        matrix[1, index] = kf.Color != EnumColor.Unkown ? kf.Color.ToString() : string.Empty;
        matrix[2, index] = kf.Nationality != EnumNationality.Unkown ? kf.Nationality.ToString() : string.Empty;
        matrix[3, index] = kf.Drink != EnumDrink.Unkown ? kf.Drink.ToString() : string.Empty;
        matrix[4, index] = kf.Smoke != EnumSmoke.Unkown ? kf.Smoke.ToString() : string.Empty;
        matrix[5, index] = kf.Animal != EnumAnimal.Unkown ? kf.Animal.ToString() : string.Empty;
      }

      System.Console.WriteLine(string.Join(System.Environment.NewLine, matrix.Rank2ToConsoleString(uniformWidth: true, centerContent: true)));
    }

    public static void Example()
    {
      System.Console.Clear();
      RenderKnownFacts(); // Display all that has been deduced so far, either by fact or constraint propagation.

      while (KnownFacts.Any(kf => kf.House.Count > 1))
      {
        System.Console.ReadKey();

        // This part performs what is known as constraint propagation.
        foreach (var kf in KnownFacts)
        {
          if (kf.House.Count == 1)
          {
            var houseNumber = kf.House.First();

            if (kf.Color != EnumColor.Unkown)
              foreach (var kfp in KnownFacts.Where(kfs => kfs.House.Count > 1 && kfs.Color != EnumColor.Unkown))
                kfp.House.Remove(houseNumber);

            if (kf.Nationality != EnumNationality.Unkown)
              foreach (var kfp in KnownFacts.Where(kfs => kfs.House.Count > 1 && kfs.Nationality != EnumNationality.Unkown))
                kfp.House.Remove(houseNumber);

            if (kf.Drink != EnumDrink.Unkown)
              foreach (var kfp in KnownFacts.Where(kfs => kfs.House.Count > 1 && kfs.Drink != EnumDrink.Unkown))
                kfp.House.Remove(houseNumber);

            if (kf.Smoke != EnumSmoke.Unkown)
              foreach (var kfp in KnownFacts.Where(kfs => kfs.House.Count > 1 && kfs.Smoke != EnumSmoke.Unkown))
                kfp.House.Remove(houseNumber);

            if (kf.Animal != EnumAnimal.Unkown)
              foreach (var kfp in KnownFacts.Where(kfs => kfs.House.Count > 1 && kfs.Animal != EnumAnimal.Unkown))
                kfp.House.Remove(houseNumber);
          }
        }

        // So this part just simply combines definite facts, by house number.
        for (var hn = 1; hn <= 5; hn++) // Combine by house number.
        {
          if (KnownFacts.Any(kf => kf.House.Count == 1 && kf.House.First() == hn))
          {
            var h = new Fact() { House = new System.Collections.Generic.List<int>() { hn } };

            foreach (var kf in KnownFacts.Where(kf => kf.House.Count == 1 && kf.House.First() == hn))
            {
              if (kf.Color != EnumColor.Unkown)
                h.Color = kf.Color;
              if (kf.Nationality != EnumNationality.Unkown)
                h.Nationality = kf.Nationality;
              if (kf.Drink != EnumDrink.Unkown)
                h.Drink = kf.Drink;
              if (kf.Smoke != EnumSmoke.Unkown)
                h.Smoke = kf.Smoke;
              if (kf.Animal != EnumAnimal.Unkown)
                h.Animal = kf.Animal;
            }


            var indices = KnownFacts.GetElementsAndIndices((kf, i) => kf.House.Count == 1 && kf.House.First() == hn).Select(e => e.index).OrderByDescending(k => k).ToArray();

            foreach (var index in indices)
              KnownFacts.RemoveAt((int)index);

            KnownFacts.Add(h);
          }
        }

        System.Console.Clear();
        RenderKnownFacts(); // Display all that has been deduced so far, either by fact or constraint propagation.
      }
    }
  }
}
