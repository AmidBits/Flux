using System;
using System.Linq;

using Flux;

namespace ConsoleApp
{
  public static partial class Zamplez
  {
    public static bool IsSupported
#if ZAMPLEZ
      => true;
#else
      => false;
#endif

    /// <summary>Run all zamplez available.</summary>
    public static void Run()
    {
#if ZAMPLEZ
      RunAmbOperator();
      RunArrayRank2();
      RunColors();
      RunCoordinateSystems();
      RunDataStructuresGraphs();
      RunImmutableDataStructures();
      RunLocale();
      ParallelVsSerial.Run();
      RunPhoneticAlgorithms();
      RunReflection();
      RunResource();
      RunRulesEngine();
      RunISetOps();
      RunStatistics();
      RunTemporal();
#else
      throw new System.NotImplementedException(@"/define:ZAMPLEZ");
#endif
    }

#if ZAMPLEZ
    private static class ParallelVsSerial
    {
      public static void Run()
      {
        System.Console.WriteLine();
        System.Console.WriteLine($"{nameof(ParallelVsSerial)} comparison:");
        System.Console.WriteLine();
        System.Console.WriteLine(Flux.Services.Performance.Measure(() => RegularForLoop(10, 0.05), 1));
        System.Console.WriteLine(Flux.Services.Performance.Measure(() => ParallelForLoop(10, 0.05), 1));
        System.Console.WriteLine();
      }

      static void RegularForLoop(int taskCount = 10, double taskLoad = 1)
      {
        //var startDateTime = DateTime.Now;
        //System.Console.WriteLine($"{nameof(RegularForLoop)} started at {startDateTime}.");
        for (int i = 0; i < taskCount; i++)
        {
          ExpensiveTask(taskLoad);
          //var total = ExpensiveTask(taskLoad);
          //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
        }
        //var endDateTime = DateTime.Now;
        //System.Console.WriteLine($"{nameof(RegularForLoop)} ended at {endDateTime}.");
        //var span = endDateTime - startDateTime;
        //System.Console.WriteLine($"{nameof(RegularForLoop)} executed in {span.TotalSeconds} seconds.");
        //System.Console.WriteLine();
      }
      static void ParallelForLoop(int taskCount = 10, double taskLoad = 1)
      {
        //var startDateTime = DateTime.Now;
        System.Threading.Tasks.Parallel.For(0, taskCount, i =>
        {
          ExpensiveTask(taskLoad);
          //var total = ExpensiveTask(taskLoad);
          //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
        });
        //var endDateTime = DateTime.Now;
        //System.Console.WriteLine($"{nameof(ParallelForLoop)} ended at {endDateTime}.");
        //var span = endDateTime - startDateTime;
        //System.Console.WriteLine($"{nameof(ParallelForLoop)} executed in {span.TotalSeconds} seconds");
        //System.Console.WriteLine();
      }
      static long ExpensiveTask(double taskLoad = 1)
      {
        var total = 0L;
        for (var i = 1; i < int.MaxValue * taskLoad; i++)
          total += i;
        return total;
      }
    }
#endif

    #region RunAmbOperator

    private readonly static System.Numerics.BigInteger[] m_ap = Flux.NumberSequences.PrimeNumber.GetAscendingPrimes(System.Numerics.BigInteger.CreateChecked(2)).Take(100).ToArray(); // Primes.
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

    #endregion

    #region ArrayRank2

    /// <summary>Run the two-dimensional (rank equals 2) array zample.</summary>
    public static void RunArrayRank2()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunArrayRank2));
      System.Console.WriteLine();

      var matrix = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      var padding = 2;
      padding++;

      var (minLeft, minTop, maxLeft, maxTop) = new string[] { "Array-2D" }.Concat(matrix.Rank2ToConsoleStrings(new ConsoleStringOptions() { UniformWidth = true })).WriteToConsole();

      var mrotatec = matrix.RotateToCopyCw();
      (minLeft, minTop, maxLeft, maxTop) = new string[] { "Rotated-CW" }.Concat(mrotatec.Rank2ToConsoleStrings(new ConsoleStringOptions() { UniformWidth = true })).WriteToConsole(maxLeft + padding, minTop);

      var mrotatecc = matrix.RotateToCopyCcw();
      (minLeft, minTop, maxLeft, maxTop) = new string[] { "Rotated-CCW" }.Concat(mrotatecc.Rank2ToConsoleStrings(new ConsoleStringOptions() { UniformWidth = true })).WriteToConsole(maxLeft + padding, minTop);

      var mtranspose = matrix.TransposeToCopy();
      (minLeft, minTop, maxLeft, maxTop) = new string[] { "Transposed" }.Concat(mtranspose.Rank2ToConsoleStrings(new ConsoleStringOptions() { UniformWidth = true })).WriteToConsole(maxLeft + padding, minTop);

      var minsert = matrix.InsertToCopy(1, 1, 4, 0);
      (minLeft, minTop, maxLeft, maxTop) = new string[] { "Inserted" }.Concat(minsert.Rank2ToConsoleStrings(new ConsoleStringOptions() { UniformWidth = true })).WriteToConsole(maxLeft + padding, minTop);
      minsert.Fill(0, 1, 3, 4, 9);
      (minLeft, minTop, maxLeft, maxTop) = new string[] { "Fill (Inserted)" }.Concat(minsert.Rank2ToConsoleStrings(new ConsoleStringOptions() { UniformWidth = true })).WriteToConsole(maxLeft + padding, minTop);

      var mflip0 = matrix.FlipToCopy(0);
      (minLeft, minTop, maxLeft, maxTop) = new string[] { "Flip_0" }.Concat(mflip0.Rank2ToConsoleStrings(new ConsoleStringOptions() { UniformWidth = true })).WriteToConsole(maxLeft + padding, minTop);

      var mflip1 = matrix.FlipToCopy(1);
      (minLeft, minTop, maxLeft, maxTop) = new string[] { "Flip_1" }.Concat(mflip1.Rank2ToConsoleStrings(new ConsoleStringOptions() { UniformWidth = true })).WriteToConsole(maxLeft + padding, minTop);

      var mremove = matrix.RemoveToCopy(1, 1);
      (minLeft, minTop, maxLeft, maxTop) = new string[] { "Remove" }.Concat(mremove.Rank2ToConsoleStrings(new ConsoleStringOptions() { UniformWidth = true })).WriteToConsole(maxLeft + padding, minTop);

      System.Console.WriteLine();

      //System.Console.WriteLine(nameof(matrix));
      //System.Console.WriteLine(matrix.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mrotatec = matrix.RotateClockwise();
      //System.Console.WriteLine(nameof(ExtensionMethods.RotateClockwise));
      //System.Console.WriteLine(mrotatec.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mrotatecc = matrix.RotateCounterClockwise();
      //System.Console.WriteLine(nameof(ExtensionMethods.RotateCounterClockwise));
      //System.Console.WriteLine(mrotatecc.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mtranspose = matrix.TransposeToCopy();
      //System.Console.WriteLine(nameof(ExtensionMethods.Transpose));
      //System.Console.WriteLine(mtranspose.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var minsert = matrix.Insert(1, 1, 4, 0);
      //System.Console.WriteLine(nameof(ArrayRank2.Insert));
      //System.Console.WriteLine(minsert.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();
      //minsert.Fill(0, 1, 3, 4, 9);
      //System.Console.WriteLine(nameof(ArrayRank2.Fill));
      //System.Console.WriteLine(minsert.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mflip0 = matrix.FlipToCopy(0);
      //System.Console.WriteLine(nameof(ArrayRank2.FlipToCopy));
      //System.Console.WriteLine(mflip0.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mflip1 = matrix.FlipToCopy(1);
      //System.Console.WriteLine(nameof(ArrayRank2.FlipToCopy));
      //System.Console.WriteLine(mflip1.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mremove = matrix.Remove(1, 1);
      //System.Console.WriteLine(nameof(ArrayRank2.Remove));
      //System.Console.WriteLine(mremove.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();
    }

    #endregion

    #region RunColors

    /// <summary>Run the colors zample.</summary>
    public static void RunColors()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunColors));
      System.Console.WriteLine();

      var argb = new Flux.Colors.Argb(new System.Random().GetRandomBytes(4));

      //rgb = new Flux.Colors.Rgb(0xF0, 0xC8, 0x0E);
      //rgb = new Flux.Colors.Rgb(0xB4, 0x30, 0xE5);
      //rgb = new Flux.Media.Colors.Rgb(0x0, 0x0, 0x0);
      //rgb = new Flux.Media.Colors.Rgb(0x1, 0x1, 0x1);
      //rgb = new Flux.Media.Colors.Rgb(0xFF, 0xFF, 0xFF);
      //rgb = new Flux.Media.Colors.Rgb(0xFE, 0xFE, 0xFE);

      System.Console.WriteLine($"{argb}");
      var hue = argb.RGB.GetHue(out var _, out var _, out var r, out var g, out var b, out var chroma);
      argb.RGB.GetSecondaryChromaAndHue(out var chroma2, out var hue2);
      var cmyk = argb.ToAcmyk();
      System.Console.WriteLine($"{cmyk} ({cmyk.ToArgb()})");
      var ahsi = argb.ToAhsi();
      System.Console.WriteLine($"{ahsi} ({ahsi.ToArgb()})");
      var ahsl = argb.ToAhsl();
      System.Console.WriteLine($"{ahsl} ({ahsl.ToArgb()}) ({ahsl.ToAhsv()})");
      var ahsv = argb.ToAhsv();
      System.Console.WriteLine($"{ahsv} ({ahsv.ToArgb()}) ({ahsv.ToAhsl()})");// ({hsv.ToAhwb()})");
      var ahwb = argb.ToAhwb();
      System.Console.WriteLine($"{ahwb} ({ahwb.ToArgb()}) ({ahwb.ToAhsv()})");

      System.Console.WriteLine($"{argb.ToHtmlHexString()} | {(r * 100):N1}%, {(g * 100):N1}%, {(b * 100):N1}% | {hue:N1}, {hue2} | {(chroma * 100):N1}, {(chroma2 * 100):N1} | {(ahsv.HSV.Value * 100):N1}%, {(ahsl.HSL.Lightness * 100):N1}%, {(ahsi.HSI.Intensity * 100):N1}% | Y={argb.RGB.GetLuma601()} | {(ahsv.HSV.Saturation * 100):N1}%, {(ahsl.HSL.Saturation * 100):N1}%, {(ahsi.HSI.Saturation * 100):N1}%");
      System.Console.WriteLine();
    }

    #endregion

    #region RunCoordinateSystems

    /// <summary>This is a reference coordinate for Madrid, Spain, which is antipodal to Takapau, New Zeeland.</summary>
    public static Flux.Geometry.GeographicCoordinate MadridSpain => new(40.416944, -3.703333, 650);

    /// <summary>This is a reference coordinate for Takapau, New Zeeland, which is antipodal to Madrid, Spain.</summary>
    public static Flux.Geometry.GeographicCoordinate TakapauNewZealand => new(-40.033333, 176.35, 235);

    /// <summary>This is a reference point for Phoenix, Arizona, USA, from where the C# version of this library originated.</summary>
    public static Flux.Geometry.GeographicCoordinate PhoenixAzUsa => new(33.448333, -112.073889, 331);
    /// <summary>This is a reference point for Tucson, Arizona, USA, from where the C# version of this library originated.</summary>
    public static Flux.Geometry.GeographicCoordinate TucsonAzUsa => new(32.221667, -110.926389, 728);

    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunCoordinateSystems()
    {
      //System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunCoordinateSystems));
      System.Console.WriteLine();

      Draw(MadridSpain, nameof(MadridSpain));
      Draw(PhoenixAzUsa, nameof(PhoenixAzUsa));
      Draw(TakapauNewZealand, nameof(TakapauNewZealand));
      Draw(TucsonAzUsa, nameof(TucsonAzUsa));

      static void Draw(Flux.Geometry.GeographicCoordinate gc, System.ReadOnlySpan<char> label)
      {
        Flux.Console.WriteInformationLine($"{label.ToString()}:");

        System.Console.WriteLine(gc);

        var sca = gc.ToSphericalCoordinate(); System.Console.WriteLine(sca);
        var cca = sca.ToCylindricalCoordinate(); System.Console.WriteLine(cca);
        var cc3a = cca.ToVector3(); System.Console.WriteLine(cc3a);

        // Show 2D coordinate systems also.
        {
          Flux.Console.WriteWarningLine($" Sub 2D coordinate show-case from the 3D components X and Y.");

          var cc2a = cc3a.ToVector2XY(); System.Console.Write(' '); System.Console.WriteLine(cc2a);
          var pca = cc2a.ToPolarCoordinate(); System.Console.Write(' '); System.Console.WriteLine(pca);
          var cc2b = pca.ToVector2(); System.Console.Write(' '); System.Console.WriteLine(cc2b);
        }

        var ccb = cc3a.ToCylindricalCoordinate(); System.Console.WriteLine(ccb);
        var scb = ccb.ToSphericalCoordinate(); System.Console.WriteLine(scb);
        var gcb = scb.ToGeographicCoordinate(); System.Console.WriteLine(gcb);

        System.Console.WriteLine();
      }
    }

    #endregion

    #region RunDataStructuresGraphs

    public static void RunDataStructuresGraphs()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunDataStructuresGraphs));
      System.Console.WriteLine();

      System.Console.WriteLine();
      AdjacencyList();
      System.Console.WriteLine();

      System.Console.WriteLine();
      AdjacencyMatrix1();
      System.Console.WriteLine();
      AdjacencyMatrix2();
      System.Console.WriteLine();

      static void AdjacencyList()
      {
        var gal = new Flux.DataStructures.Graphs.AdjacencyList<int, (int unused, int distance)>();

        gal.AddVertex(0);
        gal.AddVertex(1);
        gal.AddVertex(2);
        gal.AddVertex(3);
        gal.AddVertex(4);
        gal.AddVertex(5);
        gal.AddVertex(6);
        gal.AddVertex(7);
        gal.AddVertex(8);

        gal.AddEdge(0, 1, (0, 4));
        gal.AddEdge(0, 7, (0, 8));
        gal.AddEdge(1, 0, (0, 4));
        gal.AddEdge(1, 2, (0, 8));
        gal.AddEdge(1, 7, (0, 11));
        gal.AddEdge(2, 1, (0, 8));
        gal.AddEdge(2, 3, (0, 7));
        gal.AddEdge(2, 5, (0, 4));
        gal.AddEdge(2, 8, (0, 2));
        gal.AddEdge(3, 2, (0, 7));
        gal.AddEdge(3, 4, (0, 9));
        gal.AddEdge(3, 5, (0, 14));
        gal.AddEdge(4, 3, (0, 9));
        gal.AddEdge(4, 5, (0, 10));
        gal.AddEdge(5, 2, (0, 4));
        gal.AddEdge(5, 3, (0, 14));
        gal.AddEdge(5, 4, (0, 10));
        gal.AddEdge(5, 6, (0, 2));
        gal.AddEdge(6, 5, (0, 2));
        gal.AddEdge(6, 7, (0, 1));
        gal.AddEdge(6, 8, (0, 6));
        gal.AddEdge(7, 0, (0, 8));
        gal.AddEdge(7, 1, (0, 11));
        gal.AddEdge(7, 6, (0, 1));
        gal.AddEdge(7, 8, (0, 7));
        gal.AddEdge(8, 2, (0, 2));
        gal.AddEdge(8, 6, (0, 6));
        gal.AddEdge(8, 7, (0, 7));

        // 6, 8
        //gal.AddEdge(0, 1, (3, 1));
        //gal.AddEdge(0, 2, (1, 0));
        //gal.AddEdge(0, 4, (3, 2));
        //gal.AddEdge(1, 2, (2, 0));
        //gal.AddEdge(1, 3, (0, 3));
        //gal.AddEdge(2, 3, (1, 0));
        //gal.AddEdge(2, 4, (6, 0));
        //gal.AddEdge(3, 4, (2, 1));

        // 10, 1
        //gal.AddEdge(0, 1, (3, 1));
        //gal.AddEdge(0, 2, (4, 0));
        //gal.AddEdge(0, 3, (5, 0));
        //gal.AddEdge(1, 2, (2, 0));
        //gal.AddEdge(2, 3, (4, 0));
        //gal.AddEdge(2, 4, (1, 0));
        //gal.AddEdge(3, 4, (10, 0));

        System.Console.WriteLine(gal.ToConsoleString());

        var spt = gal.GetDijkstraShortestPathTree(0, o => o.distance).ToList();
        System.Console.WriteLine();
        System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
        foreach (var (destination, distance) in spt)
          System.Console.WriteLine($"{destination}={distance}");
        System.Console.WriteLine();
      }

      static void AdjacencyMatrix1()
      {
        var gam = new Flux.DataStructures.Graphs.AdjacencyMatrix<int, (int capacity, int cost)>();

        gam.AddVertex(0);
        gam.AddVertex(1);
        gam.AddVertex(2);
        gam.AddVertex(3);
        gam.AddVertex(4);

        // BellmanFord: 6, 8
        gam.AddEdge(0, 1, (3, 1));
        gam.AddEdge(0, 2, (1, 0));
        gam.AddEdge(0, 4, (3, 2));
        gam.AddEdge(1, 2, (2, 0));
        gam.AddEdge(1, 3, (0, 3));
        gam.AddEdge(2, 3, (1, 0));
        gam.AddEdge(2, 4, (6, 0));
        gam.AddEdge(3, 4, (2, 1));

        // BellmanFord: 10, 1
        //gam.AddEdge(0, 1, (3, 1));
        //gam.AddEdge(0, 2, (4, 0));
        //gam.AddEdge(0, 3, (5, 0));
        //gam.AddEdge(1, 2, (2, 0));
        //gam.AddEdge(2, 3, (4, 0));
        //gam.AddEdge(2, 4, (1, 0));
        //gam.AddEdge(3, 4, (10, 0));

        System.Console.WriteLine(gam.ToConsoleString());

        var mcmf = gam.GetBellmanFordMaxFlowMinCost(0, 4, o => o.capacity, o => o.cost);
        System.Console.WriteLine($"BellmanFord Min-Cost-Max-Flow: {mcmf}");
        System.Console.WriteLine();

        var spt = gam.GetDijkstraShortestPathTree(0, o => o.capacity).ToList();
        System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination = distance):");
        foreach (var (destination, distance) in spt)
          System.Console.WriteLine($"{destination} = {distance}");
        System.Console.WriteLine(System.Environment.NewLine);
      }

      static void AdjacencyMatrix2()
      {
        var gam = new Flux.DataStructures.Graphs.AdjacencyMatrix<char, int>();

        gam.AddVertex(0, 'a');
        gam.AddVertex(1, 'b');
        gam.AddVertex(2, 'c');
        gam.AddVertex(3, 'd');
        gam.AddVertex(4, 'e');
        gam.AddVertex(5, 'f');
        gam.AddVertex(6, 'g');
        gam.AddVertex(7, 'h');
        gam.AddVertex(8, 'i');

        gam.AddEdge(0, 1, 4);
        gam.AddEdge(1, 0, 4);

        gam.AddEdge(0, 7, 8);

        gam.AddEdge(1, 2, 8);

        gam.AddEdge(2, 1, 8);
        gam.AddEdge(2, 3, 7);
        gam.AddEdge(2, 5, 4);
        gam.AddEdge(2, 8, 2);

        gam.AddEdge(3, 2, 7);
        gam.AddEdge(3, 5, 14);
        gam.AddEdge(3, 3, 13);
        gam.AddEdge(3, 4, 9);

        gam.AddEdge(4, 3, 9);
        gam.AddEdge(4, 5, 10);

        gam.AddEdge(5, 2, 4);
        gam.AddEdge(5, 3, 14);
        gam.AddEdge(5, 4, 10);
        gam.AddEdge(5, 6, 2);

        gam.AddEdge(6, 5, 2);
        gam.AddEdge(6, 7, 1);
        gam.AddEdge(6, 8, 6);

        gam.AddEdge(7, 0, 8);
        gam.AddEdge(7, 1, 11);
        gam.AddEdge(7, 6, 1);
        gam.AddEdge(7, 8, 7);

        gam.AddEdge(8, 2, 2);
        gam.AddEdge(8, 6, 6);
        gam.AddEdge(8, 7, 7);

        System.Console.WriteLine(gam.ToConsoleString());

        var mcmf = gam.GetBellmanFordMaxFlowMinCost(0, 4, o => o, o => o);
        System.Console.WriteLine($"BellmanFord Min-Cost-Max-Flow: {mcmf}");
        System.Console.WriteLine();

        var spt = gam.GetDijkstraShortestPathTree(0, o => (int)o).ToList();
        System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
        foreach (var (destination, distance) in spt)
          System.Console.WriteLine($"{destination}={distance}");
        System.Console.WriteLine();
      }
    }

    #endregion

    #region RunImmutableDataStructures

    public static void RunImmutableDataStructures()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunImmutableDataStructures));
      System.Console.WriteLine();

      RunAvlTree();
    }

    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunAvlTree()
    {
      var bst = Flux.DataStructures.ImmutableAvlTree<int, string>.Empty;

      for (var index = 0; bst.GetNodeCount() < 16; index++)
      {
        var r = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 31);

        if (!bst.Contains(r))
          bst = bst.Add(r, r.ToBigInteger().ToCardinalNumeralCompoundString());
      }

      System.Console.WriteLine(bst.ToConsoleBlock());

      var counter = 0;
      foreach (var item in bst.GetNodesInOrder())
        System.Console.WriteLine($"{counter++:D2} : {item.Value}");
    }

    #endregion

    #region RunLocale

    /// <summary>Run the locale zample.</summary>
    public static void RunLocale()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunLocale));
      System.Console.WriteLine();

      System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainName)} = \"{Flux.Locale.AppDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainPath)} = \"{Flux.Locale.AppDomainPath}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ClrVersion)} = \"{Flux.Locale.ClrVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ComputerDnsPrimaryHostName)} = \"{Flux.Locale.ComputerDnsPrimaryHostName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.EnvironmentOsTitle)} = \"{Flux.Locale.EnvironmentOsTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.EnvironmentOsVersion)} = \"{Flux.Locale.EnvironmentOsVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkTitle)} = \"{Flux.Locale.FrameworkTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkVersion)} = \"{Flux.Locale.FrameworkVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.MachineName)} = \"{Flux.Locale.MachineName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.NetworkDomainName)} = \"{Flux.Locale.NetworkDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.NetworkHostName)} = \"{Flux.Locale.NetworkHostName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeOsArchitecture)} = \"{Flux.Locale.RuntimeOsArchitecture}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeOsTitle)} = \"{Flux.Locale.RuntimeOsTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeOsVersion)} = \"{Flux.Locale.RuntimeOsVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.SystemOsTitle)} = \"{Flux.Locale.SystemOsTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.SystemOsVersion)} = \"{Flux.Locale.SystemOsVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickCounter)} = \"{Flux.Locale.TimerTickCounter}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickResolution)} = \"{Flux.Locale.TimerTickResolution}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.UserDomainName)} = \"{Flux.Locale.UserDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.UserName)} = \"{Flux.Locale.UserName}\"");
      System.Console.WriteLine();
    }

    #endregion

    #region RunPhoneticAlgorithms

    /// <summary>Run the phonetic algorithm zample.</summary>
    public static void RunPhoneticAlgorithms()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunPhoneticAlgorithms));
      System.Console.WriteLine();

      var ipaes = typeof(Flux.Text.IPhoneticAlgorithmEncoder).GetDerivedTypes().Select(t => (Flux.Text.IPhoneticAlgorithmEncoder)System.Activator.CreateInstance(t));
      var names = new string[] { "Dougal", "Glinde", "Plumridge", "Simak", "Webberley" };

      foreach (var ipae in ipaes)
        foreach (var name in names)
          System.Console.WriteLine($"{ipae?.GetType().Name ?? @"[untyped]"}.\"{name}\", \"{ipae?.EncodePhoneticAlgorithm(name) ?? @"[unnamed]"}\"");

      System.Console.WriteLine();
    }

    #endregion

    #region RunReflection

    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunReflection()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunReflection));
      System.Console.WriteLine();

      // Write(typeof(System.IConvertible));

      Write(typeof(IUnitQuantifiable<,>));
      Write(typeof(IQuantifiable<>), typeof(IUnitQuantifiable<,>));

      static void Write(System.Type type, params System.Type[] excludingTypes)
      {
        var types = typeof(Flux.Locale).Assembly.DefinedTypes;
        var exclusions = excludingTypes.SelectMany(type => type.GetDerivedTypes(types));
        var implementations = type.GetDerivedTypes(types).OrderBy(t => t.Name).Where(t => !exclusions.Contains(t)).ToList();
        System.Console.WriteLine($"{type.Name} ({implementations.Count}) : {string.Join(", ", implementations.Select(i => i.Name))}");
        System.Console.WriteLine();
      }

      System.Console.WriteLine(string.Join(System.Environment.NewLine, typeof(IQuantifiable<>).GetDerivedTypes().Append(typeof(Flux.Units.Rate<Flux.Units.Length, Flux.Units.Time>)).OrderBy(t => t.Name).Where(t => !t.IsInterface && !t.Name.Contains("Fraction")).Select(q => q.Name + " = " + q.GetDefaultValue()?.ToString() ?? "Null")));
    }

    #endregion

    #region RunResource

    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunResource()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunResource));
      System.Console.WriteLine();

      var tdas = new ITabularDataAcquirable[]
      {
        new Flux.Resources.Census.CountiesAllData(),
        new Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows(),
        new Flux.Resources.ProjectGutenberg.TableOfContents(),
        new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings(),
        new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary(),
        new Flux.Resources.Scowl.TwoOfTwelveFull(),
        new Flux.Resources.Scrape.ZipCodes(),
        new Flux.Resources.Ucd.UnicodeData(),
        new Flux.Resources.Ucd.Blocks(),
        new Flux.Resources.W3c.NamedCharacterReferences(),
        new Flux.Resources.DotNet.FxSequence(System.TimeZoneInfo.GetSystemTimeZones()),
      };

      foreach (var tda in tdas)
      {
        var fn = tda.FieldNames;
        var ft = tda.FieldTypes;
        var fv = tda.GetFieldValues();
        var dr = new Flux.Data.EnumerableTabularDataReader(fv, fn);
        var dt = dr.ToDataTable(tda.GetType().Name);

        System.Console.WriteLine($"'{dt.TableName}' with {dt.Columns.Count} columns ({(ft.Distinct().Count() is var types ? types : types)} {(types > 1 ? "types" : "type")}) and {dt.Rows.Count} rows.");
      }
    }

    #endregion

    #region RunRulesEngine

    /// <summary>Run the rules engine zample.</summary>
    public static void RunRulesEngine()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunRulesEngine));
      System.Console.WriteLine();

      User.ShowCase();

      System.Console.WriteLine();
    }

    private sealed class User
    {
      public int Age { get; set; }
      public string Name { get; set; }
      public string BirthCountry { get; set; }

      public User(int age, string name, string birthCountry)
      {
        Age = age;
        Name = name;
        BirthCountry = birthCountry;
      }

      public override string ToString()
        => $"{GetType().Name} {{ {Name}, {Age} ({BirthCountry}) }}";

      public static void ShowCase()
      {
        var rules = new Flux.RulesEngine.RulesDictionary
        {
          { "AgeLimit", new Flux.RulesEngine.Rule(nameof(Age), nameof(System.Linq.Expressions.BinaryExpression.GreaterThan), 20) },
          { "NameRequirement", new Flux.RulesEngine.Rule(nameof(Name), nameof(System.Linq.Expressions.BinaryExpression.Equal), "John") },
          { "CountryOfBirth", new Flux.RulesEngine.Rule(nameof(BirthCountry), nameof(System.Linq.Expressions.BinaryExpression.Equal), "Canada") }
        };

        foreach (var rule in rules)
          System.Console.WriteLine(rule);

        var user1 = new User(43, "Royi", "Australia");
        var user2 = new User(33, "John", "England");
        var user3 = new User(23, "John", "Canada");

        var rulesCompiled = rules.CompileRules<User>();
        System.Console.WriteLine($"{user1}, {rulesCompiled.EvaluateRules(user1).ToConsoleString(new ConsoleStringOptions() { VerticalSeparator = ',' })}");
        System.Console.WriteLine($"{user2}, {rulesCompiled.EvaluateRules(user2).ToConsoleString(new ConsoleStringOptions() { VerticalSeparator = ',' })}");
        System.Console.WriteLine($"{user3}, {rulesCompiled.EvaluateRules(user3).ToConsoleString(new ConsoleStringOptions() { VerticalSeparator = ',' })}");
      }
    }

    #endregion

    #region RunISetOps

    /// <summary>Run the set ops zample.</summary>
    public static void RunISetOps()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunISetOps));
      System.Console.WriteLine();

      var os1 = new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
      var os2 = new System.Collections.Generic.HashSet<int>() { 4, 5, 6, 7, 8, 9 };

      var padding = 2;
      padding++;

      System.Console.WriteLine($"Set:1");
      System.Console.WriteLine($"{string.Join(", ", os1)}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Set:2");
      System.Console.WriteLine($"{string.Join(", ", os2)}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Source-Diff.");
      System.Console.WriteLine($"{string.Join(", ", os1.SourceDifference(os2))}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Sym-Diff.");
      System.Console.WriteLine($"{string.Join(", ", os1.SymmetricDifference(os2))}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Target-Diff.");
      System.Console.WriteLine($"{string.Join(", ", os1.TargetDifference(os2))}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Cartesian-Product");
      System.Console.WriteLine($"{string.Join("; ", os1.CartesianProduct(new System.Collections.Generic.HashSet<int>[] { os2 }).Select(v => string.Join(',', v)))}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Power-Set");
      System.Console.WriteLine($"{string.Join("; ", os1.PowerSet().Where(list => list.Count > 0).Select(list => string.Join(',', list)))}");
      System.Console.WriteLine();

      //var (minLeft, minTop, maxLeft, maxTop) = os1.Select(v => v.ToString()).Prepend("Set:1").WriteToConsole();
      //(minLeft, minTop, maxLeft, maxTop) = os2.Select(v => v.ToString()).Prepend("Set:2").WriteToConsole(maxLeft + padding, minTop);

      //(minLeft, minTop, maxLeft, maxTop) = os1.SourceDifference(os2).Select(v => v.ToString()).Prepend("Source-Diff.").WriteToConsole(maxLeft + padding, minTop);
      //(minLeft, minTop, maxLeft, maxTop) = os1.SymmetricDifference(os2).Select(v => v.ToString()).Prepend("Sym-Diff.").WriteToConsole(maxLeft + padding, minTop);
      //(minLeft, minTop, maxLeft, maxTop) = os1.TargetDifference(os2).Select(v => v.ToString()).Prepend("Target-Diff.").WriteToConsole(maxLeft + padding, minTop);
      // (minLeft, minTop, maxLeft, maxTop) = os1.CartesianProduct(new System.Collections.Generic.HashSet<int>[] { os2 }).Select(v => string.Join(",", v)).Prepend($"Cartesian-Product ").WriteToConsole(maxLeft + padding, minTop);
      //(minLeft, minTop, maxLeft, maxTop) = os1.PowerSet().Select(v => string.Join(",", v)).Prepend($"Power-Set ").WriteToConsole(maxLeft + padding, minTop);

      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.SetEquals)} = {os1.SetEquals(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.Overlaps)} = {os1.Overlaps(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.IsSubsetOf)} \u2286 = {os1.IsSubsetOf(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.IsProperSubsetOf)} \u2282 = {os1.IsProperSubsetOf(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.IsSupersetOf)} \u2287 = {os1.IsSupersetOf(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.IsProperSupersetOf)} \u2283 = {os1.IsProperSupersetOf(os2)}");

      System.Console.WriteLine();
    }

    #endregion

    #region RunStatistics

    /// <summary>Run the statistics zample.</summary>
    public static void RunStatistics()
    {
      System.Console.WriteLine(nameof(RunStatistics));
      System.Console.WriteLine();

      var a = new System.Collections.Generic.List<double>() { 6, 7, 15, 36, 39, 40, 41, 42, 43, 47, 49 };
      System.Console.WriteLine($"For {{{string.Join(", ", a)}}}:");
      System.Console.WriteLine();

      RunQuantiles(a);
      System.Console.WriteLine();
      RunQuartiles(a);
      System.Console.WriteLine();

      var b = new System.Collections.Generic.List<double>() { 7, 15, 36, 39, 40, 41 };
      System.Console.WriteLine($"For {{{string.Join(", ", b)}}}:");
      System.Console.WriteLine();

      RunQuantiles(b);
      System.Console.WriteLine();
      RunQuartiles(b);
      System.Console.WriteLine();
    }

    static void RunQuartiles(System.Collections.Generic.List<double> x)
    {
      System.Console.WriteLine($"The computed quartiles:");
      System.Console.WriteLine($"Method 1: {new Flux.Statistics.QuartileMethod1().ComputeQuartiles(x)}");
      System.Console.WriteLine($"Method 2: {new Flux.Statistics.QuartileMethod2().ComputeQuartiles(x)}");
      System.Console.WriteLine($"Method 3: {new Flux.Statistics.QuartileMethod3().ComputeQuartiles(x)}");
      System.Console.WriteLine($"Method 4: {new Flux.Statistics.QuartileMethod4().ComputeQuartiles(x)}");
    }

    static void RunQuantiles(System.Collections.Generic.List<double> x)
    {
      for (var q = 0.25; q < 1; q += 0.25)
        RunQuantiles(x, q);

      static void RunQuantiles(System.Collections.Generic.List<double> x, double p)
      {
        var values = new double[]
        {
          Flux.Statistics.QuantileR1.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.QuantileR2.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.QuantileR3.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.QuantileR4.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.QuantileR5.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.QuantileR6.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.QuantileR7.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.QuantileR8.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.QuantileR9.Default.EstimateQuantileValue(x, p),
        };

        System.Console.WriteLine($"The estimated quantiles of {p:N2}:");
        System.Console.WriteLine($"{string.Join("\t", System.Linq.Enumerable.Range(0, values.Length).Select((e, i) => $"R{(i + 1)} = {values[i]:N2}"))}");
        System.Console.WriteLine($"(Average: {values.Average()})");
      }
    }

    static void RunStats()
    {
      var a = new int[] { 13, 12, 11, 8, 4, 3, 2, 1, 1, 1 };
      a = a.Reverse().ToArray();
      //a = new int[] { 1, 2, 3, 6, 6, 6, 7, 8, 9 };
      //a = new int[] { 1, 2, 3, 4, 4, 5, 6, 7, 8 };
      //a = new int[] { 2, 4, 5, 6, 6, 7, 9, 12, 14, 15, 18, 19, 22, 24, 26, 28 };
      //a = new int[] { 3, 6, 7, 8, 8, 10, 13, 15, 16, 20 };
      //  a = new int[] { 15, 20, 35, 40, 50 };
      //a = new Flux.Randomization.Xoshiro256SS().GetRandomInt32s(0, 20).Take(200).OrderBy(k => k).ToArray();

      //var hg = new Histogram<int>();
      //foreach (var k in a)
      //  hg[k] = hg.TryGetValue(k, out var v) ? v + 1 : 1;

      var h = a.ToHistogram(e => e, e => 1);
      System.Console.WriteLine("H:");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, h));
      System.Console.WriteLine();

      var pmf = h.ToProbabilityMassFunction(1d);
      System.Console.WriteLine("PMF:");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, pmf));
      System.Console.WriteLine();

      var cdf = h.ToCumulativeDistributionFunction(1d);
      System.Console.WriteLine("CDF:");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, cdf));
      System.Console.WriteLine();

      //var pr = h.PercentRank(sof);
      //System.Console.WriteLine("PR:");
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, pr));
      //System.Console.WriteLine();

      //var pl = h.CumulativeMassFunction(kvp => kvp.Key, kvp => kvp.Value, 100.0);
      //System.Console.WriteLine("PL:");
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, pl));
      //System.Console.WriteLine();
    }

    #endregion

    #region RunTemporal

    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunTemporal()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunTemporal));
      System.Console.WriteLine();

      var dt = new Flux.Units.MomentUtc(1100, 04, 28, 13, 30, 31);
      System.Console.WriteLine($"{dt}");

      var jdgc = dt.ToJulianDate(Flux.Units.TemporalCalendar.GregorianCalendar);
      System.Console.WriteLine($"{jdgc.ToTimeString()}");
      var jdngc = jdgc.ToJulianDayNumber();
      System.Console.WriteLine($"{jdngc.ToDateString(Flux.Units.TemporalCalendar.GregorianCalendar)}");
      var mugc = jdgc.ToMomentUtc(Flux.Units.TemporalCalendar.GregorianCalendar);
      //System.Console.WriteLine($"{mugc}, {mugc.ToDateOnly()}, {mugc.ToDateTime()}, {mugc.ToTimeOnly()}, {mugc.ToTimeSpan()}");
      System.Console.WriteLine($"{mugc}, {mugc.ToDateTime()}, {mugc.ToTimeSpan()}");

      //var mugc1 = mugc with { Year = Year + 1 };

      var jdjc = dt.ToJulianDate(Flux.Units.TemporalCalendar.JulianCalendar);
      System.Console.WriteLine($"{jdjc.ToTimeString()}");
      var jdnjc = jdjc.ToJulianDayNumber();
      System.Console.WriteLine($"{jdnjc.ToDateString(Flux.Units.TemporalCalendar.JulianCalendar)}");
      var mujc = jdjc.ToMomentUtc(Flux.Units.TemporalCalendar.JulianCalendar);
      //System.Console.WriteLine($"{mujc}, {mujc.ToDateOnly()}, {mujc.ToDateTime()}, {mujc.ToTimeOnly()}, {mujc.ToTimeSpan()}");
      System.Console.WriteLine($"{mujc}, {mujc.ToDateTime()}, {mujc.ToTimeSpan()}");

      //return;

      //var jdn = dt.ToJulianDayNumber(ConversionCalendar.GregorianCalendar);
      //System.Console.WriteLine($"{jdn}");
      //var jd1 = jdn.ToJulianDate();
      //System.Console.WriteLine($"{jd1}");
      //var dt1 = jdn.ToMomentUtc(ConversionCalendar.JulianCalendar);
      //System.Console.WriteLine($"{dt1} = {dt1.ToDateTime()}");

      //var jd = dt.ToJulianDate(ConversionCalendar.JulianCalendar);
      //System.Console.WriteLine($"{jd}");
      //var dt2 = jd.ToMomentUtc(ConversionCalendar.JulianCalendar);
      //System.Console.WriteLine($"{dt2} = {dt2.ToDateTime()}");
    }

    #endregion

    #region Console Helpers

    public static (int minLeft, int minTop, int maxLeft, int maxTop) WriteToConsole(this System.Collections.Generic.IEnumerable<string> source, int left, int top)
    {
      var index = 0;

      var maxLeft = 0;
      var maxTop = 0;

      foreach (var item in source)
      {
        maxLeft = System.Math.Max(maxLeft, left + item.Length);
        maxTop = System.Math.Max(maxTop, top + index);

        System.Console.SetCursorPosition(left, top + index++);
        System.Console.Write(item);
      }

      System.Console.WriteLine();

      return (left, top, maxLeft - 1, maxTop); // Compensate for maxLeft because it represents the length, not index, like maxTop is.
    }
    public static (int minLeft, int minTop, int maxLeft, int maxTop) WriteToConsole(this System.Collections.Generic.IEnumerable<string> source)
    {
      var (left, top) = System.Console.GetCursorPosition();

      return WriteToConsole(source, left, top);
    }

    #endregion
  }
}
