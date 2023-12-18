namespace Flux
{
  public static class ConstraintPropagationSolver2
  {
    public enum EnumColor
    {
      Unknown,
      Yellow,
      Blue,
      Red,
      Ivory,
      Green,
    }

    public enum EnumNationality
    {
      Unknown,
      Norwegian,
      Ukranian,
      Englishman,
      Spaniard,
      Japanese,
    }

    public enum EnumDrink
    {
      Unknown,
      Water,
      Tea,
      Milk,
      OrangeJuice,
      Coffee,
    }

    public enum EnumSmoke
    {
      Unknown,
      Kools,
      Chesterfield,
      OldGold,
      LuckyStrike,
      Parliament,
    }

    public enum EnumPet
    {
      Unknown,
      Fox,
      Horse,
      Snails,
      Dog,
      Zebra,
    }

    // This is a class that holds some fact (by one or more houses).
    public sealed class NegativeFact
    {
      public System.Collections.Generic.List<int> House = new() { 1, 2, 3, 4, 5 };
      public EnumColor Color;
      public EnumNationality Nationality;
      public EnumDrink Drink;
      public EnumSmoke Smoke;
      public EnumPet Pet;
    }

    // This is a class that holds some fact (by one or more houses).
    public sealed class PositiveFact
    {
      public System.Collections.Generic.List<int> House = new() { 1, 2, 3, 4, 5 };
      public EnumColor Color;
      public EnumNationality Nationality;
      public EnumDrink Drink;
      public EnumSmoke Smoke;
      public EnumPet Pet;

      public int UnknownCount()
        => (Color == EnumColor.Unknown ? 1 : 0) + (Nationality == EnumNationality.Unknown ? 1 : 0) + (Drink == EnumDrink.Unknown ? 1 : 0) + (Smoke == EnumSmoke.Unknown ? 1 : 0) + (Pet == EnumPet.Unknown ? 1 : 0);
    }

    public readonly static System.Collections.Generic.List<NegativeFact> NegativeFacts = new()
    {
      new NegativeFact() { Smoke = EnumSmoke.Chesterfield, Pet = EnumPet.Fox },
      new NegativeFact() { Smoke = EnumSmoke.Kools, Pet = EnumPet.Horse },
    };

    // These are known facts.
    public readonly static System.Collections.Generic.IList<PositiveFact> PositiveFacts = new System.Collections.Generic.List<PositiveFact>()
    {
      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 3 }, Color = EnumColor.Red, Nationality = EnumNationality.Englishman },
      new PositiveFact() { Nationality = EnumNationality.Spaniard, Pet = EnumPet.Dog },
      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 4, 5 }, Color = EnumColor.Green, Drink = EnumDrink.Coffee },
      new PositiveFact() { Nationality = EnumNationality.Ukranian, Drink = EnumDrink.Tea },
      new PositiveFact() { Smoke = EnumSmoke.OldGold, Pet = EnumPet.Snails },
      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 1 }, Color = EnumColor.Yellow, Smoke = EnumSmoke.Kools },
      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 3 }, Drink = EnumDrink.Milk },
      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 1 }, Nationality = EnumNationality.Norwegian },
      new PositiveFact() { Drink = EnumDrink.OrangeJuice, Smoke = EnumSmoke.LuckyStrike },
      new PositiveFact() { Nationality = EnumNationality.Japanese, Smoke = EnumSmoke.Parliament },
      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 2 }, Color = EnumColor.Blue },

      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 3, 4 }, Color = EnumColor.Ivory },
      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 2 }, Smoke = EnumSmoke.Chesterfield },
      new PositiveFact() { Drink = EnumDrink.Water },
      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 1 }, Pet = EnumPet.Fox },
      new PositiveFact() { House = new System.Collections.Generic.List<int>() { 2 }, Pet = EnumPet.Horse },
      new PositiveFact() { Pet = EnumPet.Zebra },

      //new Fact() { House = new System.Collections.Generic.List<int>() { 4, 5 }, Color = EnumColor.White },
      //new Fact() { Smoke = EnumSmoke.Blends },
      //new Fact() { Drink = EnumDrink.Water },
      //new Fact() { Animal = EnumAnimal.Cat },
      //new Fact() { House = new System.Collections.Generic.List<int>() { 4 }, Animal = EnumAnimal.Fish },
    };

    // This only renders the data as a "table" on the screen.
    public static void RenderKnownFacts()
    {
      var matrix = new object[6, PositiveFacts.Count];

      for (var index = 0; index < PositiveFacts.Count; index++)
      {
        var kf = PositiveFacts[index];

        matrix[0, index] = string.Join(',', kf.House);
        matrix[1, index] = kf.Color != EnumColor.Unknown ? kf.Color.ToString() : string.Empty;
        matrix[2, index] = kf.Nationality != EnumNationality.Unknown ? kf.Nationality.ToString() : string.Empty;
        matrix[3, index] = kf.Drink != EnumDrink.Unknown ? kf.Drink.ToString() : string.Empty;
        matrix[4, index] = kf.Smoke != EnumSmoke.Unknown ? kf.Smoke.ToString() : string.Empty;
        matrix[5, index] = kf.Pet != EnumPet.Unknown ? kf.Pet.ToString() : string.Empty;
      }

      System.Console.WriteLine(string.Join(System.Environment.NewLine, matrix.Rank2ToConsoleString(new ConsoleStringOptions() { UniformWidth = true, CenterContent = true })));
    }

    public static void Example()
    {
      System.Console.Clear();
      RenderKnownFacts(); // Display all that has been deduced so far, either by fact or constraint propagation.

      while (PositiveFacts.Any(kf => kf.House.Count > 1))
      {
        System.Console.ReadKey();

        // This part performs what is known as constraint propagation.
        foreach (var pf in PositiveFacts)
        {
          if (pf.House.Count == 1)
          {
            var houseNumber = pf.House.First();

            if (pf.Color != EnumColor.Unknown)
            {
              foreach (var kfp in PositiveFacts.Where(kfs => kfs.House.Count > 1 && kfs.Color != EnumColor.Unknown))
                kfp.House.Remove(houseNumber);
            }

            if (pf.Nationality != EnumNationality.Unknown)
            {
              foreach (var kfp in PositiveFacts.Where(kfs => kfs.House.Count > 1 && kfs.Nationality != EnumNationality.Unknown))
                kfp.House.Remove(houseNumber);
            }

            if (pf.Drink != EnumDrink.Unknown)
            {
              foreach (var kfp in PositiveFacts.Where(kfs => kfs.House.Count > 1 && kfs.Drink != EnumDrink.Unknown))
                kfp.House.Remove(houseNumber);
            }

            if (pf.Smoke != EnumSmoke.Unknown)
            {
              foreach (var kfp in PositiveFacts.Where(kfs => kfs.House.Count > 1 && kfs.Smoke != EnumSmoke.Unknown))
                kfp.House.Remove(houseNumber);
            }

            if (pf.Pet != EnumPet.Unknown)
            {
              foreach (var kfp in PositiveFacts.Where(kfs => kfs.House.Count > 1 && kfs.Pet != EnumPet.Unknown))
                kfp.House.Remove(houseNumber);
            }
          }
        }

        foreach (var nf in NegativeFacts)
        {
        }

        // So this part just simply combines definite facts, by house number.
        for (var hn = 1; hn <= 5; hn++) // Combine by house number.
        {
          if (PositiveFacts.Any(kf => kf.House.Count == 1 && kf.House.First() == hn))
          {
            var h = new PositiveFact() { House = new System.Collections.Generic.List<int>() { hn } };

            foreach (var kf in PositiveFacts.Where(kf => kf.House.Count == 1 && kf.House.First() == hn))
            {
              if (kf.Color != EnumColor.Unknown)
                h.Color = kf.Color;
              if (kf.Nationality != EnumNationality.Unknown)
                h.Nationality = kf.Nationality;
              if (kf.Drink != EnumDrink.Unknown)
                h.Drink = kf.Drink;
              if (kf.Smoke != EnumSmoke.Unknown)
                h.Smoke = kf.Smoke;
              if (kf.Pet != EnumPet.Unknown)
                h.Pet = kf.Pet;
            }

            var indices = PositiveFacts.SelectWhere((kf, i) => kf.House.Count == 1 && kf.House.First() == hn, (e, i) => i).OrderByDescending(k => k).ToArray();

            foreach (var index in indices)
              PositiveFacts.RemoveAt((int)index);

            PositiveFacts.Add(h);
          }
        }

        System.Console.Clear();
        RenderKnownFacts(); // Display all that has been deduced so far, either by fact or constraint propagation.

        foreach (var pf in PositiveFacts)
        {
          if (pf.UnknownCount() == 1)
          {
            if (pf.Color == EnumColor.Unknown)
            {
              var list = PositiveFacts.Where(pf => pf.Color != EnumColor.Unknown).ToList();
              if (list.Count == 1)
                pf.Color = list[0].Color;
            }
            else if (pf.Drink == EnumDrink.Unknown)
            {
              var list = PositiveFacts.Where(pf => pf.UnknownCount() == 4 && pf.Drink != EnumDrink.Unknown).ToList();
              if (list.Count == 1)
                list[0].House.RemoveAll(i => i != pf.House[0]);
            }
            else if (pf.Nationality == EnumNationality.Unknown)
            {
              var list = PositiveFacts.Where(pf => pf.Nationality != EnumNationality.Unknown).ToList();
              if (list.Count == 1)
                pf.Nationality = list[0].Nationality;
            }
            else if (pf.Pet == EnumPet.Unknown)
            {
              var list = PositiveFacts.Where(pf => pf.Pet != EnumPet.Unknown).ToList();
              if (list.Count == 1)
                pf.Pet = list[0].Pet;
            }
            else if (pf.Smoke == EnumSmoke.Unknown)
            {
              var list = PositiveFacts.Where(pf => pf.Smoke != EnumSmoke.Unknown).ToList();
              if (list.Count == 1)
                pf.Smoke = list[0].Smoke;
            }
          }
        }
      }
    }
  }
}
