using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

using Flux;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  class Program
  {
    private static void AmbTest()
    {
      var amb = new Flux.AmbOps.Amb();

      var x = amb.Choose(7, 9, 1, 2, 3);
      var y = amb.Choose(7, 8, 4, 5, 6);
      var r = amb.Choose("Rob", "Maxi", "Karen");
      var s = amb.Choose("abc", "xyz", "ax");
      amb.RequireFinal(() => x.Value * y.Value + r.Value.Length == 12 && r.Value.Contains(s.Value));

      System.Console.WriteLine($"{x} * {y} + {r} = 12");
      System.Console.Read();
      System.Console.Read();
    }

    // These are just enums so that I don't have to deal with comparing strings... each enum is essentially a number.

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
    public class Fact
    {
      public System.Collections.Generic.List<int> House = new System.Collections.Generic.List<int>() { 1, 2, 3, 4, 5 };
      public EnumColor Color;
      public EnumNationality Nationality;
      public EnumDrink Drink;
      public EnumSmoke Smoke;
      public EnumAnimal Animal;
    }

    // These are known facts.
    public static System.Collections.Generic.List<Fact> KnownFacts = new System.Collections.Generic.List<Fact>()
    {
      new Fact() { Color = EnumColor.Red, Nationality = EnumNationality.Brit },
      new Fact() { Nationality = EnumNationality.Swede, Animal = EnumAnimal.Dog },
      new Fact() { Nationality = EnumNationality.Dane, Drink = EnumDrink.Tea },
      new Fact() { House = new List<int>() { 3, 4 }, Color = EnumColor.Green, Drink = EnumDrink.Coffee },
      new Fact() { Smoke = EnumSmoke.PallMall, Animal = EnumAnimal.Bird },
      new Fact() { Color = EnumColor.Yellow, Smoke = EnumSmoke.Dunhill },
      new Fact() { House = new List<int>() { 3 }, Drink = EnumDrink.Milk },
      new Fact() { House = new List<int>() { 1 }, Nationality = EnumNationality.Norwegian },
      new Fact() { Drink = EnumDrink.Beer, Smoke = EnumSmoke.BlueMaster },
      new Fact() { Nationality = EnumNationality.German, Smoke = EnumSmoke.Prince },
      new Fact() { House = new List<int>() { 2 }, Color = EnumColor.Blue, Animal = EnumAnimal.Horse },
      new Fact() { House = new List<int>() { 4, 5 }, Color = EnumColor.White },
      new Fact() { Smoke = EnumSmoke.Blends },
      new Fact() { Drink = EnumDrink.Water },
      new Fact() { Animal = EnumAnimal.Cat },
      new Fact() { House = new List<int>() { 4 }, Animal = EnumAnimal.Fish },
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

      System.Console.WriteLine(matrix.ToConsoleBlock(uniformWidth: true, centerContent: true));
    }

    private static void TimedMain(string[] args)
    {
      AmbTest();
      return;

      while (KnownFacts.Any(kf => kf.House.Count > 1))
      {
        //System.Console.Clear();
        //RenderKnownFacts(); // Display all that has been deduced so far, either by fact or constraint propagation.

        //System.Console.ReadKey();

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
            var h = new Fact() { House = new List<int>() { hn } };

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

            var indices = KnownFacts.IndicesOf(kf => kf.House.Count == 1 && kf.House.First() == hn).OrderByDescending(k => k).ToArray();

            foreach (var index in indices)
              KnownFacts.RemoveAt(index);

            KnownFacts.Add(h);
          }
        }
      }

      return; // And we're done.


      AmbTest();
      return;

      int[,] board = {
        { 1, 0, 2 },
        { 1, 0, 0 },
        { 0, 0, 2 }
      };

      int[,] board4 = {
        { 0, 2, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 1, 0, 0, 1, 2 },
        //{ 0, 0, 0, 0, 0 },
        //{ 0, 0, 0, 0 },
        //{ 0, 0, 0, 0, 0, 0 },
      };

      System.Console.WriteLine(board4.ToConsoleBlock());

      System.Console.WriteLine(Flux.Model.TicTacToe2.Game.GetCounts(board4, out var playerUp).ToConsoleString());
      System.Console.WriteLine(playerUp);
      System.Console.WriteLine();

      var moves = Flux.Model.TicTacToe2.Game.GetMoves4(board4, out var maxMove, out var minMove);

      System.Console.WriteLine($"Best for max: {maxMove} and best for min: {minMove}");
      System.Console.WriteLine();

      foreach (var move in moves)
        System.Console.WriteLine(move);
      System.Console.WriteLine();

      //      var scores = new int[] { 3, 5, 6, 9, 1, 2, 0, -1 };

      //      var mm = new Flux.Model.MinMax();

      //System.Console.WriteLine(      mm.Minimax(true, scores));

      //      var liss = new Flux.Metrical.LongestIncreasingSubsequence<int>();
      //      var seq = new int[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };

      //      var glis = liss.GetLongestIncreasingSubsequence(seq);

      //var game = new Flux.Model.GameOfLife.Game(32, 32, true, 0.5);
      //var cv = new Flux.Model.GameOfLife.Console(game);
      //cv.Run(100);
      //return;

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => RegularForLoop(10, 0.1), 1));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => ParallelForLoop(10, 0.1), 1));
    }

    #region Serial vs. Parallel loops
    //static void RegularForLoop(int taskCount = 10, double taskLoad = 1)
    //{
    //  //var startDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} started at {startDateTime}.");
    //  for (int i = 0; i < taskCount; i++)
    //  {
    //    ExpensiveTask(taskLoad);
    //    //var total = ExpensiveTask(taskLoad);
    //    //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
    //  }
    //  //var endDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} ended at {endDateTime}.");
    //  //var span = endDateTime - startDateTime;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} executed in {span.TotalSeconds} seconds.");
    //  //System.Console.WriteLine();
    //}
    //static void ParallelForLoop(int taskCount = 10, double taskLoad = 1)
    //{
    //  //var startDateTime = DateTime.Now;
    //  System.Threading.Tasks.Parallel.For(0, taskCount, i =>
    //  {
    //    ExpensiveTask(taskLoad);
    //    //var total = ExpensiveTask(taskLoad);
    //    //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
    //  });
    //  //var endDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(ParallelForLoop)} ended at {endDateTime}.");
    //  //var span = endDateTime - startDateTime;
    //  //System.Console.WriteLine($"{nameof(ParallelForLoop)} executed in {span.TotalSeconds} seconds");
    //  //System.Console.WriteLine();
    //}
    //static long ExpensiveTask(double taskLoad = 1)
    //{
    //  var total = 0L;
    //  for (var i = 1; i < int.MaxValue * taskLoad; i++)
    //    total += i;
    //  return total;
    //}
    #endregion Serial vs. Parallel loops

    #region Main method
    static void Main(string[] args)
    {
      var originalOutputEncoding = System.Console.OutputEncoding;

      try
      {
        System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false);
        System.Console.WriteLine("Output encoding set to UTF-16");
      }
      catch
      {
        System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8;
        System.Console.WriteLine("Output encoding set to UTF-8");
      }

      System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} (code page {System.Console.OutputEncoding.CodePage})");
      System.Console.WriteLine();

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 100000));

      Console.OutputEncoding = originalOutputEncoding;

      RenderKnownFacts(); // Display all that has been deduced so far, either by fact or constraint propagation.
      System.Console.WriteLine();

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
    #endregion Main method
  }
}
