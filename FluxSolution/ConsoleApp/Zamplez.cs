using System;
using System.Linq;

using Flux;
using Flux.Units;

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
      RunBits();
      RunColors();
      RunCoordinateSystems();
      RunDataStructuresGraph();
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

#if INCLUDE_SWAR

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.SwarFoldLeft()));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.BitFoldLeft()));

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.SwarFoldRight()));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.BitFoldRight()));

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.SwarIntegerLog2()));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.IntegerLog2TowardZero()));

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.SwarLeadingZeroCount()));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.GetLeadingZeroCount()));

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.SwarMostSignificant1Bit()));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.MostSignificant1Bit()));

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.SwarNextLargestPow2()));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => xx.NextLargestPow2()));

#endif

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
        System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => RegularForLoop(10, 0.05), 1));
        System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => ParallelForLoop(10, 0.05), 1));
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

        var m_ap = Flux.Numerics.NumberSequence.GetAscendingPrimes(2).Take(100).ToArray(); // Primes.
        var m_rn = System.Linq.Enumerable.Range(0, 100).ToArray(); // Rational.
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

    #endregion

    #region RunBits

    private static void RunBits()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunBits));
      System.Console.WriteLine();

      var value = 128;
      var multiple = -4;
      var radix = 2;

      ////      var mTowardsZero = value.MultipleOfTowardZero(multiple);
      //var mTowardsZeropf = value.MultipleOfTowardZero(multiple, false);
      //var mTowardsZeropt = value.MultipleOfTowardZero(multiple, true);

      ////      var mAwayFromZero = value.MultipleOfAwayFromZero(multiple);
      //var mAwayFromZeropf = value.MultipleOfAwayFromZero(multiple, false);
      //var mAwayFromZeropt = value.MultipleOfAwayFromZero(multiple, true);

      var rtmo = value.MultipleOfNearest(multiple, true, Flux.UniversalRounding.WholeAwayFromZero, out var mTowardsZero, out var mAwayFromZero);

      var rtpTowardsZero = value.PowOfTowardZero(radix, true);
      var rtpAwayFromZero = value.PowOfAwayFromZero(radix, true);
      var rtp = value.RoundToNearest(Flux.UniversalRounding.WholeAwayFromZero, rtpTowardsZero, rtpAwayFromZero);
      //var rtp = Flux.Quantities.Radix.PowOf(value, radix, true, Flux.RoundingMode.AwayFromZero, out var rtpTowardsZero, out var rtpAwayFromZero);

      System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      var (q, remainder) = value.TruncRem(1);
      var quotient = int.CreateChecked(q);

      var p2TowardsZero = quotient.MostSignificant1Bit();
      var p2AwayFromZero = (p2TowardsZero < quotient || remainder > 0) ? (p2TowardsZero == 0 ? 1 : p2TowardsZero << 1) : p2TowardsZero;

      var p2TowardsZerop = p2TowardsZero == value ? p2TowardsZero >> 1 : p2TowardsZero;
      var p2AwayFromZerop = p2AwayFromZero == value ? p2AwayFromZero << 1 : p2AwayFromZero;

      var n = (int)(short.MaxValue / sbyte.MaxValue);
      //n = -3;
      System.Console.WriteLine($"          Number = {n}");

      var bibs = n.ToBinaryString();
      System.Console.WriteLine($"          Binary = {bibs}");
      var bios = n.ToOctalString();
      System.Console.WriteLine($"           Octal = {bios}");
      var bids = n.ToDecimalString();
      System.Console.WriteLine($"         Decimal = {bids}");
      var bihs = n.ToHexadecimalString();
      System.Console.WriteLine($"     Hexadecimal = {bihs}");
      var bir2s = n.ToRadixString(2);
      System.Console.WriteLine($"         Radix 2 = {bir2s}");
      var bir8s = n.ToRadixString(8);
      System.Console.WriteLine($"         Radix 8 = {bir8s}");
      var bir10s = n.ToRadixString(10);
      System.Console.WriteLine($"        Radix 10 = {bir10s}");
      var bir16s = n.ToRadixString(16);
      System.Console.WriteLine($"        Radix 16 = {bir16s}");

      //var rn = n.BinaryToGray();
      //var rrn = rn.GrayToBinary();

      //      n = 0;
      //      var nlpow2 = n.NextLargerPowerOf2();
      var np2TowardsZero = (int)n.RoundToNearest(Flux.UniversalRounding.WholeTowardZero, n.Pow2TowardZero(false), n.Pow2AwayFromZero(false));
      System.Console.WriteLine($" Pow2TowardsZero = {np2TowardsZero}");
      var np2AwayFromZero = (int)n.RoundToNearest(Flux.UniversalRounding.WholeAwayFromZero, n.Pow2TowardZero(false), n.Pow2AwayFromZero(false));
      System.Console.WriteLine($"Pow2AwayFromZero = {np2AwayFromZero}");

      var birbits = n.ReverseBits();
      System.Console.WriteLine($"    Reverse Bits = {birbits.ToBinaryString()}");
      var birbyts = n.ReverseBytes();
      System.Console.WriteLine($"   Reverse Bytes = {birbyts.ToBinaryString()}");

      var bfl = n.BitFoldLeft();
      System.Console.WriteLine($"   Bit-Fold Left = {bfl}");
      var bfls = bfl.ToBinaryString();
      System.Console.WriteLine($"       As Binary = {bfls}");
      var bfr = n.BitFoldLeft();
      System.Console.WriteLine($"  Bit-Fold Right = {bfr}");
      var bfrs = bfr.ToBinaryString();
      System.Console.WriteLine($"       As Binary = {bfrs}");
      //var bsbl = n.GetShortestBitLength();
      var bln = n.GetBitLength();
      //var l2 = bi.IntegerLog2();
      var ms1b = n.MostSignificant1Bit();
      var bmr = n.GetBitLength().CreateBitMaskRight();
      var bmrs = bmr.ToBinaryString();
      var bmrsl = bmrs.Length;
      var bml = n.GetBitLength().CreateBitMaskLeft();
      var bmls = bml.ToBinaryString();
      var bmlsl = bmls.Length;
    }

    #endregion // RunBits

    #region RunColors

    /// <summary>Run the colors zample.</summary>
    public static void RunColors()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunColors));
      System.Console.WriteLine();

      var argb = Flux.Colors.FromRandom();

      //rgb = new Flux.Colors.Rgb(0xF0, 0xC8, 0x0E);
      //rgb = new Flux.Colors.Rgb(0xB4, 0x30, 0xE5);
      //rgb = new Flux.Media.Colors.Rgb(0x0, 0x0, 0x0);
      //rgb = new Flux.Media.Colors.Rgb(0x1, 0x1, 0x1);
      //rgb = new Flux.Media.Colors.Rgb(0xFF, 0xFF, 0xFF);
      //rgb = new Flux.Media.Colors.Rgb(0xFE, 0xFE, 0xFE);

      System.Console.WriteLine($"{argb}");
      var hue = argb.ComputeHue(out var a, out var r, out var g, out var b, out var _, out var _, out var chroma);
      var (chroma2, hue2) = argb.ComputeSecondaryChromaAndHue();
      var acmyk = argb.ToAcmyk();
      System.Console.WriteLine($"{acmyk} ({Flux.Colors.FromAcmyk(acmyk.Alpha, acmyk.Cyan, acmyk.Magenta, acmyk.Yellow, acmyk.Key)})");
      var ahsi = argb.ToAhsi();
      System.Console.WriteLine($"{ahsi} ({Flux.Colors.FromAhsi(ahsi.Alpha, ahsi.Hue, ahsi.Saturation, ahsi.Intensity)})");
      var ahsl = argb.ToAhsl();
      System.Console.WriteLine($"{ahsl} ({Flux.Colors.FromAhsi(ahsl.Alpha, ahsl.Hue, ahsl.Saturation, ahsl.Lightness)}) ({Flux.Colors.ConvertHslToHsv(ahsl.Hue, ahsl.Saturation, ahsl.Lightness)})");
      var ahsv = argb.ToAhsv();
      System.Console.WriteLine($"{ahsv} ({Flux.Colors.FromAhsi(ahsv.Alpha, ahsv.Hue, ahsv.Saturation, ahsv.Value)}) (HSL: {Flux.Colors.ConvertHsvToHsl(ahsv.Hue, ahsv.Saturation, ahsv.Value)}) (HWB: {Flux.Colors.ConvertHsvToHwb(ahsv.Hue, ahsv.Saturation, ahsv.Value)})");
      var ahwb = argb.ToAhwb();
      System.Console.WriteLine($"{ahwb} ({Flux.Colors.FromAhwb(ahwb.Alpha, ahwb.Hue, ahwb.White, ahwb.Black)}) ({Flux.Colors.ConvertHwbToHsv(ahwb.Hue, ahwb.White, ahwb.Black)})");

      System.Console.WriteLine($"{argb.ToHtmlHexString()} | {(r * 100):N1}%, {(g * 100):N1}%, {(b * 100):N1}% | {hue:N1}, {hue2} | {(chroma * 100):N1}, {(chroma2 * 100):N1} | {(ahsv.Value * 100):N1}%, {(ahsl.Lightness * 100):N1}%, {(ahsi.Intensity * 100):N1}% | Y={argb.ComputeLuma601()} | {(ahsv.Saturation * 100):N1}%, {(ahsl.Saturation * 100):N1}%, {(ahsi.Saturation * 100):N1}%");
      System.Console.WriteLine();
    }

    #endregion

    #region RunCoordinateSystems

    /// <summary>This is a reference coordinate for Madrid, Spain, which is antipodal to Takapau, New Zeeland.</summary>
    public static Flux.CoordinateSystems.GeographicCoordinate MadridSpain => new(40.416944, Flux.Units.AngleUnit.Degree, -3.703333, Flux.Units.AngleUnit.Degree, 650);

    /// <summary>This is a reference coordinate for Takapau, New Zeeland, which is antipodal to Madrid, Spain.</summary>
    public static Flux.CoordinateSystems.GeographicCoordinate TakapauNewZealand => new(-40.033333, Flux.Units.AngleUnit.Degree, 176.35, Flux.Units.AngleUnit.Degree, 235);

    /// <summary>This is a reference point for Phoenix, Arizona, USA, from where the C# version of this library originated.</summary>
    public static Flux.CoordinateSystems.GeographicCoordinate PhoenixAzUsa => new(33.448333, Flux.Units.AngleUnit.Degree, -112.073889, Flux.Units.AngleUnit.Degree, 331);
    /// <summary>This is a reference point for Tucson, Arizona, USA, from where the C# version of this library originated.</summary>
    public static Flux.CoordinateSystems.GeographicCoordinate TucsonAzUsa => new(32.221667, Flux.Units.AngleUnit.Degree, -110.926389, Flux.Units.AngleUnit.Degree, 728);

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

      static void Draw(Flux.CoordinateSystems.GeographicCoordinate gc, System.ReadOnlySpan<char> label)
      {
        Flux.Console.WriteInformationLine($"{label.ToString()}:");

        System.Console.WriteLine($"Geographical: {gc}");

        var sca = gc.ToSphericalCoordinate(); System.Console.WriteLine($"Spherical: {sca}");
        var cca = sca.ToCylindricalCoordinate(); System.Console.WriteLine($"Cylindrical: {cca}");
        var cc3a = cca.ToVector3(); System.Console.WriteLine($"Vector: {cc3a}");
        var cc4a = cca.ToCartesianCoordinate(); System.Console.WriteLine($"(to Cartesian): {cc4a}");
        var cc5a = cc4a.ToCylindricalCoordinate(); System.Console.WriteLine($"(back to Cylindrical): {cc5a}");
        var cc6a = cc4a.ToSphericalCoordinate(); System.Console.WriteLine($"(and to Spherical): {cc6a}");

        Flux.Console.WriteWarningLine($" Sub 2D coordinate show-case from the 3D components X and Y."); // Show 2D coordinate systems also.
        var pca = cca.ToPolarCoordinate(); System.Console.Write(' '); System.Console.WriteLine($"Polar: {pca}");

        var ccb = pca.ToCylindricalCoordinate(cca.Height); System.Console.WriteLine($"Cylindrical: {ccb}");
        var scb = ccb.ToSphericalCoordinate(); System.Console.WriteLine($"Spherical: {scb}");
        var gcb = scb.ToGeographicCoordinate(); System.Console.WriteLine($"Geographical: {gcb}");

        System.Console.WriteLine();
      }
    }

    #endregion

    #region DataStructuresGraph

    public static void RunDataStructuresGraph()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunDataStructuresGraph));
      System.Console.WriteLine();

      RunDataStructuresGraphAdjacencyList();
      RunDataStructuresGraphAdjacencyMatrix();
    }

    public static void RunDataStructuresGraphAdjacencyList()
    {
      var al = new Flux.DataStructures.Graphs.AdjacencyList<int, int>();

      al.AddVertex(0, 9);
      al.AddVertex(1, 8);
      al.AddVertex(2, 7);
      al.AddVertex(3, 6);

      //am.AddEdge(0, 1, 1);
      //am.AddEdge(0, 2, 1);
      //am.AddEdge(1, 0, 1);
      //am.AddEdge(1, 2, 1);
      //am.AddEdge(2, 0, 1);
      //am.AddEdge(2, 1, 1);
      //am.AddEdge(2, 3, 1);
      //am.AddEdge(3, 2, 1);

      al.AddEdge(0, 1, 2);
      al.AddEdge(0, 2, 1);
      al.AddEdge(1, 2, 4);
      al.AddEdge(2, 3, 1);

      System.Console.WriteLine(al.ToConsoleString());

      var amt = (Flux.DataStructures.Graphs.AdjacencyList<int, int>)al.TransposeToCopy();

      System.Console.WriteLine(amt.ToConsoleString());
    }

    public static void RunDataStructuresGraphAdjacencyMatrix()
    {
      var am = new Flux.DataStructures.Graphs.AdjacencyMatrix<int, int>();

      am.AddVertex(0, 9);
      am.AddVertex(1, 8);
      am.AddVertex(2, 7);
      am.AddVertex(3, 6);

      //am.AddEdge(0, 1, 1);
      //am.AddEdge(0, 2, 1);
      //am.AddEdge(1, 0, 1);
      //am.AddEdge(1, 2, 1);
      //am.AddEdge(2, 0, 1);
      //am.AddEdge(2, 1, 1);
      //am.AddEdge(2, 3, 1);
      //am.AddEdge(3, 2, 1);

      am.AddEdge(0, 1, 2);
      am.AddEdge(0, 2, 1);
      am.AddEdge(1, 2, 4);
      am.AddEdge(2, 3, 1);

      System.Console.WriteLine(am.ToConsoleString());

      var amt = (Flux.DataStructures.Graphs.AdjacencyMatrix<int, int>)am.TransposeToCopy();

      System.Console.WriteLine(amt.ToConsoleString());
    }

    #endregion DataStructuresGraph

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
      var bst = Flux.DataStructures.Immutable.ImmutableAvlTree<int, string>.Empty;

      for (var index = 0; bst.GetNodeCount() < 16; index++)
      {
        var r = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 31);

        if (!bst.Contains(r))
          bst = bst.Add(r, r.ToEnglishCardinalNumeralCompoundString());
      }

      System.Console.WriteLine(bst.ToConsoleBlock().ToString());

      var counter = 0;
      foreach (var item in bst.TraverseDfsInOrder())
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

      System.Console.WriteLine($"AppDomainDirectory = \"{new System.IO.DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory)}\"");
      System.Console.WriteLine($"AppDomainName = \"{System.AppDomain.CurrentDomain.FriendlyName}\"");

      System.Console.WriteLine($"ClrVersion = \"{System.Environment.Version}\"");

      System.Console.WriteLine($"CurrentDirectory = \"{new System.IO.DirectoryInfo(System.Environment.CurrentDirectory)}\"");

      System.Console.WriteLine($"DnsHostAddresses = \"{string.Join(", ", System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Select(a => a.ToString()))}\"");
      System.Console.WriteLine($"DnsHostName = \"{System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).HostName}\"");

      System.Console.WriteLine($"EnvironmentOsPlatform = \"{System.Environment.OSVersion.Platform}\"");
      System.Console.WriteLine($"EnvironmentOsVersion = \"{System.Environment.OSVersion.Version}\"");

      System.Console.WriteLine($"MachineName = \"{System.Environment.MachineName}\"");

      System.Console.WriteLine($"NicDomainName = \"{System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName}\"");
      System.Console.WriteLine($"NicHostName = \"{System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().HostName}\"");

      System.Console.WriteLine($"OneDriveDirectory = \"{new System.IO.DirectoryInfo(System.Environment.GetEnvironmentVariable("OneDrive")!)}\"");
      System.Console.WriteLine($"OneDriveDirectoryCommercial = \"{new System.IO.DirectoryInfo(System.Environment.GetEnvironmentVariable("OneDriveCommercial")!)}\"");
      System.Console.WriteLine($"OneDriveDirectoryConsumer = \"{new System.IO.DirectoryInfo(System.Environment.GetEnvironmentVariable("OneDriveConsumer")!)}\"");

      System.Console.WriteLine($"OSArchitecture = \"{System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString()}\"");

      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeFrameworkTitle)} = \"{Flux.Locale.RuntimeFrameworkTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeFrameworkVersion)} = \"{Flux.Locale.RuntimeFrameworkVersion}\"");

      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeOsTitle)} = \"{Flux.Locale.RuntimeOsTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeOsVersion)} = \"{Flux.Locale.RuntimeOsVersion}\"");

      System.Console.WriteLine($"{nameof(Flux.Locale.SystemOsPlatform)} = \"{Flux.Locale.SystemOsPlatform}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.SystemOsVersion)} = \"{Flux.Locale.SystemOsVersion}\"");

      System.Console.WriteLine($"StopwatchCounter = \"{System.Diagnostics.Stopwatch.GetTimestamp()}\"");
      System.Console.WriteLine($"StopwatchFrequency = \"{System.Diagnostics.Stopwatch.Frequency}\"");

      System.Console.WriteLine($"TemporaryDirectory = \"{new System.IO.DirectoryInfo(System.IO.Path.GetTempPath())}\"");

      System.Console.WriteLine($"UserDomainName = \"{System.Environment.UserDomainName}\"");
      System.Console.WriteLine($"UserName = \"{System.Environment.UserName}\"");

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

      var ipaes = typeof(Flux.PhoneticAlgorithm.IPhoneticAlgorithmEncodable).GetDerivedTypes().Select(t => (Flux.PhoneticAlgorithm.IPhoneticAlgorithmEncodable)System.Activator.CreateInstance(t));
      var names = new string[] { "Dougal", "Glinde", "Plumridge", "Simak", "Webberley", "Ashcraft", "Ashcroft", "Asicroft", "Schmidt", "Schneider", "Lloyd", "Pfister" };

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

      Write(typeof(System.IConvertible), t => true);

      Write(typeof(IValueQuantifiable<>), t => true);
      Write(typeof(IUnitValueQuantifiable<,>), t => true);
      Write(typeof(ISiUnitValueQuantifiable<,>), t => !t.Name.EndsWith('V')); // SI Units NOT using vectors.
      Write(typeof(ISiUnitValueQuantifiable<,>), t => t.Name.EndsWith('V')); // SI Units using vectors.

      static void Write(System.Type type, System.Predicate<System.Type> predicate)
      {
        var ofTypes = typeof(Flux.Locale).Assembly.DefinedTypes.ToList();
        var derivedTypes = type.GetDerivedTypes(ofTypes).Where(t => predicate(t)).OrderBy(t => t.IsInterface).ThenBy(t => t.Name).ToList();
        System.Console.WriteLine($"{type.Name} ({derivedTypes.Count}) : {string.Join(", ", derivedTypes.Select(i => i.Name))}");
        System.Console.WriteLine();
      }

      System.Console.WriteLine(string.Join(System.Environment.NewLine, typeof(IValueQuantifiable<>)
        .GetDerivedTypes()
        .Append(typeof(Flux.Units.Rate<Flux.Units.Length, Flux.Units.Time>))
        .OrderBy(t => t.Name)
        .Where(t => !t.IsInterface && !t.Name.Contains("Fraction"))
        .Select(q => q.Name + " = " + (q.CreateDefaultValue()?.ToString() ?? "Null"))));
    }

    #endregion

    #region RunResource

    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunResource()
    {
      System.Console.WriteLine("Under RE-construction");
      System.Console.WriteLine(nameof(RunResource));
      System.Console.WriteLine();

      //var tdas = new ITabularDataAcquirable[]
      //{
      //  new Flux.Resources.Census.CountiesAllData(),
      //  new Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows(),
      //  new Flux.Resources.ProjectGutenberg.TableOfContents(),
      //  new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings(),
      //  new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary(),
      //  new Flux.Resources.Scowl.TwoOfTwelveFull(),
      //  new Flux.Resources.Scrape.ZipCodes(),
      //  new Flux.Resources.Ucd.UnicodeData(),
      //  new Flux.Resources.Ucd.Blocks(),
      //  new Flux.Resources.W3c.NamedCharacterReferences(),
      //  new Flux.Resources.DotNet.FxSequence(System.TimeZoneInfo.GetSystemTimeZones()),
      //};

      //foreach (var tda in tdas)
      //{
      //  var fn = tda.FieldNames;
      //  var ft = tda.FieldTypes;
      //  var fv = tda.GetFieldValues();
      //  var dr = new Flux.Data.EnumerableTabularDataReader(fv, fn);
      //  var dt = dr.ToDataTable(tda.GetType().Name);

      //  System.Console.WriteLine($"'{dt.TableName}' with {dt.Columns.Count} columns ({(ft.Distinct().Count() is var types ? types : types)} {(types > 1 ? "types" : "type")}) and {dt.Rows.Count} rows.");
      //}
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
      private int m_age;
      private string m_name;
      private string m_birthCountry;

      public User(int age, string name, string birthCountry)
      {
        m_age = age;
        m_name = name;
        m_birthCountry = birthCountry;
      }

      public int Age => m_age;
      public string Name => m_name;
      public string BirthCountry => m_birthCountry;

      public override string ToString()
        => $"{GetType().Name} {{ {m_name}, {m_age} ({m_birthCountry}) }}";

      public static void ShowCase()
      {
        var rules = new Flux.Model.RulesEngine.RulesDictionary
        {
          { "AgeLimit", new Flux.Model.RulesEngine.Rule(nameof(Age), nameof(System.Linq.Expressions.BinaryExpression.GreaterThan), 20) },
          { "NameRequirement", new Flux.Model.RulesEngine.Rule(nameof(Name), nameof(System.Linq.Expressions.BinaryExpression.Equal), "John") },
          { "CountryOfBirth", new Flux.Model.RulesEngine.Rule(nameof(BirthCountry), nameof(System.Linq.Expressions.BinaryExpression.Equal), "Canada") }
        };

        foreach (var rule in rules)
          System.Console.WriteLine(rule);

        var user1 = new User(43, "Royi", "Australia");
        var user2 = new User(33, "John", "England");
        var user3 = new User(23, "John", "Canada");

        var rulesCompiled = rules.CompileRules<User>();

        System.Console.WriteLine($"{user1}{System.Environment.NewLine}{rulesCompiled.EvaluateRules(user1).ToConsoleString(k => k.ToString(), v => v.ToString())}");
        System.Console.WriteLine($"{user2}{System.Environment.NewLine}{rulesCompiled.EvaluateRules(user2).ToConsoleString(k => k.ToString(), v => v.ToString())}");
        System.Console.WriteLine($"{user3}{System.Environment.NewLine}{rulesCompiled.EvaluateRules(user3).ToConsoleString(k => k.ToString(), v => v.ToString())}");
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

      System.Console.WriteLine($"Sym-Diff.");
      System.Console.WriteLine($"{string.Join(", ", os1.SymmetricDifference(os2))}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Cartesian-Product");
      System.Console.WriteLine($"{string.Join("; ", os1.CartesianProduct(new System.Collections.Generic.HashSet<int>[] { os2 }).Select(v => string.Join(',', v)))}");
      System.Console.WriteLine();

      //System.Console.WriteLine($"Power-Set");
      //System.Console.WriteLine($"{string.Join("; ", os1.PowerSet().Where(list => list.Count > 0).Select(list => string.Join(',', list)))}");
      //System.Console.WriteLine();

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
      System.Console.WriteLine($"Method 1: {new Flux.Statistics.Quartile.Method1().ComputeQuartiles(x)}");
      System.Console.WriteLine($"Method 2: {new Flux.Statistics.Quartile.Method2().ComputeQuartiles(x)}");
      System.Console.WriteLine($"Method 3: {new Flux.Statistics.Quartile.Method3().ComputeQuartiles(x)}");
      System.Console.WriteLine($"Method 4: {new Flux.Statistics.Quartile.Method4().ComputeQuartiles(x)}");
    }

    static void RunQuantiles(System.Collections.Generic.List<double> x)
    {
      for (var q = 0.25; q < 1; q += 0.25)
        RunQuantiles(x, q);

      static void RunQuantiles(System.Collections.Generic.List<double> x, double p)
      {
        var values = new double[]
        {
          Flux.Statistics.Quantile.R1.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.Quantile.R2.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.Quantile.R3.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.Quantile.R4.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.Quantile.R5.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.Quantile.R6.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.Quantile.R7.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.Quantile.R8.Default.EstimateQuantileValue(x, p),
          Flux.Statistics.Quantile.R9.Default.EstimateQuantileValue(x, p),
        };

        System.Console.WriteLine($"The estimated quantiles of {p:N2}:");
        System.Console.WriteLine($"{string.Join("\t", System.Linq.Enumerable.Range(0, values.Length).Select((e, i) => $"R{(i + 1)} = {values[i]:N2}"))}");
        System.Console.WriteLine($"(Average: {values.Average()})");
      }
    }

    static void RunStats()
    {
      var a = new int[] { 13, 12, 11, 8, 4, 3, 2, 1, 1, 1 };
      a.Reverse();
      a = a.ToArray();
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

      var dt = new Flux.Temporal.Moment(1100, 04, 28, 13, 30, 31);
      System.Console.WriteLine($"{dt}");

      var jdgc = dt.ToJulianDate(Flux.Temporal.TemporalCalendar.GregorianCalendar);
      System.Console.WriteLine($"{jdgc.ToTimeString()}");
      var jdngc = jdgc.ToJulianDayNumber();
      System.Console.WriteLine($"{jdngc.ToDateString(Flux.Temporal.TemporalCalendar.GregorianCalendar)}");
      var mugc = jdgc.ToMoment(Flux.Temporal.TemporalCalendar.GregorianCalendar);
      //System.Console.WriteLine($"{mugc}, {mugc.ToDateOnly()}, {mugc.ToDateTime()}, {mugc.ToTimeOnly()}, {mugc.ToTimeSpan()}");
      System.Console.WriteLine($"{mugc}, {mugc.ToDateTime()}, {mugc.ToTimeSpan()}");

      //var mugc1 = mugc with { Year = Year + 1 };

      var jdjc = dt.ToJulianDate(Flux.Temporal.TemporalCalendar.JulianCalendar);
      System.Console.WriteLine($"{jdjc.ToTimeString()}");
      var jdnjc = jdjc.ToJulianDayNumber();
      System.Console.WriteLine($"{jdnjc.ToDateString(Flux.Temporal.TemporalCalendar.JulianCalendar)}");
      var mujc = jdjc.ToMoment(Flux.Temporal.TemporalCalendar.JulianCalendar);
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

    public static (int minLeft, int minTop, int maxLeft, int maxTop) WriteToConsole(this string source, int left, int top)
      => WriteToConsole(new string[] { source }, left, top);

    public static (int minLeft, int minTop, int maxLeft, int maxTop) WriteToConsole(this string source)
      => WriteToConsole(new string[] { source });

    #endregion
  }
}
