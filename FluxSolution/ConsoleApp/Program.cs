using System;
using System.Buffers;
using System.Collections;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.XPath;
using Flux;
using Flux.Globalization.En;
// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\AmidBits\Flux\FluxSolution\BaseLibrary\bin\Debug\net6.0\BaseLibrary.dll"

namespace ConsoleApp
{
  public class Program
  {
    #region Presets

    private static void Eliza()
    {
      // At some point? https://github.com/jeffshrager/elizagen.org/blob/master/Other_Elizas/20120310ShragerNorthEliza.c64basic

      var keywords = new string[] { "CAN YOU", "CAN I", "YOU ARE", "YOURE", "I DONT", "I FEEL", "WHY DONT YOU", "WHY CANT I", "ARE YOU", "I CANT", "I AM", " IM ", "YOU", "I WANT", "WHAT", "HOW", "WHO", "WHERE", "WHEN", "WHY", "NAME", "CAUSE", "SORRY", "DREAM", "HELLO", "HI", "MAYBE", "NO", "YOUR", "ALWAYS", "THINK", "ALIKE", "YES", "FRIEND", "COMPUTER", "NOKEYFOUND" };
      var conjugations = new string[] { " ARE ", " AM ", "WERE ", "WAS ", " YOU ", " I ", "YOUR ", "MY ", " IVE ", " YOUVE ", " IM ", " YOURE " };
      var replies = new string[] { "DON'T YOU BELIEVE THAT I CAN.", "PERHAPS YOU WOULD LIKE TO BE ABLE TO.", "YOU WANT ME TO BE ABLE TO*", "PERHAPS YOU DON'T WANT TO*", "DO YOU WANT TO BE ABLE TO*", "WHAT MAKES YOU THINK I AM*", "DOES IT PLEASE YOU TO BELIEVE I AM*", "PERHAPS YOU WOULD LIKE TO BE*", "DO YOU SOMETIMES WISH YOU WERE*", "DON'T YOU REALLY*", "WHY DON'T YOU*", "DO YOU WISH TO BE ABLE TO*", "DOES THAT TROUBLE YOU?", "TELL ME MORE ABOUT SUCH FEELINGS*", "DO YOU OFTEN FEEL*", "DO YOU ENJOY FEELING*", "DO YOU REALLY BELIEVE I DON'T*", "PERHAPS IN G00D TIME I WILL*", "DO YOU WANT ME TO*", "DO YOU THINK YOU SHOULD BE ABLE TO*", "WHY CAN'T YOU*", "WHY ARE YOU INTERESTED IN WHETHER OR NOT I AM*", "WOULD YOU PREFER IF I WERE NOT*", "PERHAPS IN YOUR FANTASIES I AM*", "HOW DO YOU KNOW YOU CAN'T*", "HAVE YOU TRIED?", "PERHAPS YOU CAN NOW*", "DID YOU COME TO ME BECAUSE YOU ARE*", "HOW LONG HAVE YOU BEEN*", "DO YOU BELIEVE IT IS NORMAL TO BE*", "DO YOU ENJOY BEING*", "WE WERE DISCUSSING YOU-- NOT ME.", "OH, I*", "YOU'RE NOT REALLY TALKING ABOUT ME. ARE YOU?", "WHAT WOULD IT MEAN TO YOU IF YOU GOT*", "WHY DO YOU WANT*", "SUPPOSE YOU SOON GOT*", "WHAT IF YOU NEVER GOT*", "I SOMETIMES ALSO WANT*", "WHY DO YOU ASK?", "DOES THAT QUESTION INTEREST YOU?", "WHAT ANSWER WOULD PLEASE YOU THE MOST?", "WHAT DO YOU THINK?", "ARE SUCH QUESTIONS ON YOUR MIND OFTEN?", "WHAT IS IT THAT YOU REALLY WANT TO KNOW?", "HAVE YOU ASKED ANYONE ELSE?", "HAVE YOU ASKED SUCH QUESTIONS BEFORE?", "WHAT ELSE COMES TO MIND WHEN YOU ASK THAT?", "NAMES DON'T INTEREST ME.", "I DON'T CARE ABOUT NAMES-- PLEASE GO ON.", "IS THAT THE REAL REASON?", "DON'T ANY OTHER REASONS COME TO MIND?", "DOES THAT REASON EXPLAIN ANYTHING ELSE?", "WHAT OTHER REASONS MIGHT THERE BE?", "PLEASE DON'T APOLOGIZE.", "APOLOGIES ARE NOT NECESSARY.", "WHAT FEELINGS DO YOU HAVE WHEN YOU APOLOGIZE.", "DON'T BE SO DEFENSIVE!", "WHAT DOES THAT DREAM SUGGEST TO YOU?", "DO YOU DREAM OFTEN?", "WHAT PERSONS APPEAR IN YOUR DREAMS?", "ARE YOU DISTURBED BY YOUR DREAMS?", "HOW DO YOU DO .,. PLEASE STATE YOUR PROBLEM.", "YOU DON'T SEEM QUITE CERTAIN.", "WHY THE UNCERTAIN TONE?", "CAN'T YOU BE MORE POSITIVE?", "YOU AREN'T SURE?", "DON'T YOU KNOW?", "ARE YOU SAYING NO JUST TO BE NEGATIVE?", "YOU ARE BEING A BIT NEGATIVE.", "WHY NOT?", "ARE YOU SURE?", "WHY NO?", "WHY ARE YOU CONCERNED ABOUT MY*", "WHAT ABOUT YOUR OWN*", "CAN'T YOU THINK OF A SPECIFIC EXAMPLE?", "WHEN?", "WHAT ARE YOU THINKING OF?", "REALLY. ALWAYS?", "DO YOU REALLY THINK SO?", "BUT YOU ARE NOT SURE YOU.", "DO YOU DOUBT YOU.", "IN WHAT WAY?", "WHAT RESEMBLANCE DO YOU SEE?", "WHAT DOES THE SIMILARITY SUGGEST TO YOU?", "WHAT OTHER CONNECTIONS DO YOU SEE?", "COULD THERE REALLY BE SOME CONNECTION?", "HOW?", "YOU SEEM QUITE POSITIVE.", "ARE YOU SURE?", "I SEE.", "I UNDERSTAND.", "WHY DO YOU BRING UP THE TOPIC OF FRIENDS?", "DO YOUR FRIENDS WORRY YOU?", "DO YOUR FRIENDS PICK ON YOU?", "ARE YOU SURE YOU HAVE ANY FRIENDS?", "DO YOU IMPOSE ON YOUR FRIENDS?", "PERHAPS YOUR LOVE FOR FRIENDS WORRIES YOU.", "DO COMPUTERS WORRY YOU?", "ARE YOU TALKING ABOUT ME IN PARTICULAR?", "ARE YOU FRIGHTENED BY MACHINES?", "WHY DO YOU MENTION COMPUTERS?", "WHAT DO YOU THINK MACHINES HAVE TO DO WITH YOUR PROBLEM?", "DON'T YOU THINK COMPUTERS CAN HELP PEOPLE?", "WHAT IS IT ABOUT MACHINES THAT WORRIES YOU?", "SAY, DO YOU HAVE ANY PSYCHOLOGICAL PROBLEMS?", "WHAT DOES THAT SUGGEST TO YOU?", "I SEE.", "I'M NOT SURE I UNDERSTAND YOU FULLY.", "COME COME ELUCIDATE YOUR THOUGHTS.", "CAN YOU ELABORATE ON THAT?", "THAT IS QUITE INTERESTING." };
      var finding = new int[] { 1, 3, 4, 2, 6, 4, 6, 4, 10, 4, 14, 3, 17, 3, 20, 2, 22, 3, 25, 3, 28, 4, 28, 4, 32, 3, 35, 5, 40, 9, 40, 9, 40, 9, 40, 9, 40, 9, 40, 9, 49, 2, 51, 4, 55, 4, 59, 4, 63, 1, 63, 1, 64, 5, 69, 5, 74, 2, 76, 4, 80, 3, 83, 7, 90, 3, 93, 6, 99, 7, 106, 6 };
    }

    #endregion // Presets

    #region Stuff

    private static void TripletGenerator()
    {
      var reWhitespace = new System.Text.RegularExpressions.Regex(@"\s+");

      var ttwts = Flux.Resources.GetGutenbergTenThousandWonderfulThings().ToArray();

      var dict3 = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>();

      foreach (var ttwt in ttwts.Skip(1))
      {
        var title = ttwt[0];
        var text = ttwt[1];

        foreach (var triplet in reWhitespace.Split(text).PartitionTuple3(0, (a, b, c, i) => (a, b, c)))
        {
          var firstandsecond = $"{triplet.a} {triplet.b}";
          var third = $"{triplet.c}";

          if (dict3.TryGetValue(firstandsecond, out var threes))
            threes.Add(third);
          else
          {
            threes = [third];
            dict3[firstandsecond] = threes;
          }

        }
      }

      var textlist = new System.Collections.Generic.List<string>();


      for (var i = 0; i < 300; i++)
      {

        if (textlist.Count == 0 || ($"{textlist[^2]} {textlist[^1]}" is var last && last.EndsWith('.')))
        {
          textlist.AddRange(reWhitespace.Split(dict3.Keys.Where(two => char.IsUpper(two[0])).Random()));

          last = $"{textlist[^2]} {textlist[^1]}";
        }

        try
        {
          var next = dict3[last].Random();

          textlist.Add(next);
        }
        catch
        {
          break;
        }
      }

      System.Console.WriteLine(string.Join(" ", textlist));

    }

    private static void Testerino(System.UInt128 maxN)
    {
      for (var n = System.UInt128.Zero; n <= maxN; n++)
      {
        for (var k = System.UInt128.Zero; k <= n; k++)
          System.Console.Write($"{n.BinomialCoefficient(k)} ");

        System.Console.WriteLine();
      }
    }

    private static void TestDspWithPlot()
    {
      var wg = new Flux.Dsp.WaveGenerators.TriangleWave();
      var wp = new Flux.Dsp.WaveProcessors.MonoQuadratic(Flux.Dsp.WaveProcessors.MonoQuadraticMode.Symmetric, .75);

      var listx = new System.Collections.Generic.List<double>();
      var lists = new System.Collections.Generic.List<double>();
      var listsi = new System.Collections.Generic.List<double>();
      var listy = new System.Collections.Generic.List<double>();
      var listyi = new System.Collections.Generic.List<double>();


      for (var phase = 0.0; phase <= double.Tau; phase += double.Tau / 100)
      {
        var signal = wg.GenerateMonoWavePi2(phase);
        var signali = (Flux.Dsp.Waves.WaveMono<double>)(-signal.Wave);
        listx.Add(phase);
        lists.Add(signal.Wave);
        listsi.Add(signali.Wave);
        listy.Add(wp.ProcessMonoWave(signal).Wave);
        listyi.Add(wp.ProcessMonoWave(signali).Wave);
      }

      ScottPlot.Plot plt = new();
      plt.Add.Scatter(listx, lists, ScottPlot.Color.Gray(55));
      plt.Add.Scatter(listx, listsi, ScottPlot.Color.Gray(200));
      plt.Add.Scatter(listx, listy, ScottPlot.Color.FromColor(System.Drawing.Color.DarkGreen));
      plt.Add.Scatter(listx, listyi, ScottPlot.Color.FromColor(System.Drawing.Color.LightGreen));
      plt.SavePng("C:\\Users\\Rob\\source\\repos\\AmidBits\\Flux\\FluxSolution\\ConsoleApp\\quickstart.png", 400, 300);
    }

    private static double LoadBearingCapacityAluExt(double sectionDiameter, double wallThickness, double length, double tensileStrength)
      => 0.44 * (sectionDiameter + wallThickness) * length * tensileStrength;

    //public static System.Collections.Generic.List<Flux.Numerics.BigRational> ToCfes(double n, int maxIterations = 100)
    //{
    //  var a = new System.Collections.Generic.List<Flux.Numerics.BigRational>();

    //  var iteration = 0;

    //  int w;
    //  do
    //  {
    //    w = (int)double.Truncate(n);
    //    var f = n - w;

    //    a.Add(new Flux.Numerics.BigRational(w));

    //    n = 1 / f;
    //  }
    //  while (w != 0 && iteration++ < maxIterations);

    //  return a;
    //}

    //public static Flux.Numerics.BigRational FromCfes(System.Collections.Generic.List<Flux.Numerics.BigRational> cfe)
    //{
    //  var m = Flux.Numerics.BigRational.Zero;

    //  for (var i = cfe.Count - 1; i >= 0; i--)
    //  {
    //    var c = cfe[i];

    //    m = Flux.Numerics.BigRational.IsZero(m) ? c : c + Flux.Numerics.BigRational.One / m;
    //  }

    //  return m;
    //}

    //public static System.Collections.Generic.List<int> Fractions(int p, int q, int maxIterations = 100)
    //{
    //  var a = new System.Collections.Generic.List<int>();

    //  var iteration = 0;

    //  while (q != 0 && iteration < maxIterations)
    //  {
    //    a.Add(p / q);

    //    (p, q) = (q, p % q);

    //    iteration++;
    //  }

    //  return a;
    //}

    #endregion // Stuff

    #region Mock DataTables

    private static void MakeParentTable(System.Data.DataSet dataSet)
    {
      // Create a new DataTable.
      System.Data.DataTable table = new("ParentTable");
      // Declare variables for DataColumn and DataRow objects.
      DataColumn column;
      DataRow row;

      // Create new DataColumn, set DataType,
      // ColumnName and add to DataTable.
      column = new();
      column.DataType = System.Type.GetType("System.Int32");
      column.ColumnName = "id";
      column.ReadOnly = true;
      column.Unique = true;
      // Add the Column to the DataColumnCollection.
      table.Columns.Add(column);

      // Create second column.
      column = new();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "ParentItem";
      column.AutoIncrement = false;
      column.Caption = "ParentItem";
      column.ReadOnly = false;
      column.Unique = false;
      // Add the column to the table.
      table.Columns.Add(column);

      // Make the ID column the primary key column.
      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = table.Columns["id"];
      table.PrimaryKey = PrimaryKeyColumns;

      // Instantiate the DataSet variable.
      //dataSet = new DataSet();
      // Add the new DataTable to the DataSet.
      dataSet.Tables.Add(table);

      // Create three new DataRow objects and add
      // them to the DataTable
      for (int i = 0; i <= 2; i++)
      {
        row = table.NewRow();
        row["id"] = i;
        row["ParentItem"] = "ParentItem " + i;
        table.Rows.Add(row);
      }
    }

    private static void MakeChildTable(System.Data.DataSet dataSet)
    {
      // Create a new DataTable.
      DataTable table = new("childTable");
      DataColumn column;
      DataRow row;

      // Create first column and add to the DataTable.
      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Int32");
      column.ColumnName = "ChildID";
      column.AutoIncrement = true;
      column.Caption = "ID";
      column.ReadOnly = true;
      column.Unique = true;

      // Add the column to the DataColumnCollection.
      table.Columns.Add(column);

      // Create second column.
      column = new();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "ChildItem";
      column.AutoIncrement = false;
      column.Caption = "ChildItem";
      column.ReadOnly = false;
      column.Unique = false;
      table.Columns.Add(column);

      // Create third column.
      column = new();
      column.DataType = System.Type.GetType("System.Int32");
      column.ColumnName = "ParentID";
      column.AutoIncrement = false;
      column.Caption = "ParentID";
      column.ReadOnly = false;
      column.Unique = false;
      table.Columns.Add(column);

      dataSet.Tables.Add(table);

      // Create three sets of DataRow objects,
      // five rows each, and add to DataTable.
      for (int i = 0; i <= 4; i++)
      {
        row = table.NewRow();
        row["childID"] = i;
        row["ChildItem"] = "Item " + i;
        row["ParentID"] = 0;
        table.Rows.Add(row);
      }
      for (int i = 0; i <= 4; i++)
      {
        row = table.NewRow();
        row["childID"] = i + 5;
        row["ChildItem"] = "Item " + i;
        row["ParentID"] = 1;
        table.Rows.Add(row);
      }
      for (int i = 0; i <= 4; i++)
      {
        row = table.NewRow();
        row["childID"] = i + 10;
        row["ChildItem"] = "Item " + i;
        row["ParentID"] = 2;
        table.Rows.Add(row);
      }
    }

    #endregion // Mock DataTables

    public static void Tewt()
    {
      var n = new BigInteger(5);
      var k = new BigInteger(3);

      var cwor = n.CountCombinationsWithoutRepetition(k);
      //var cwor_f = n.BinomialCoefficient(k);

      var cwr = n.CountCombinationsWithRepetition(k);
      //var cwr_f = (k + n - 1).BinomialCoefficient(k);

      var pwop = n.CountPermutationsWithoutRepetition(k);
      //var pwop_f = n.Factorial() / (n - k).Factorial();

      var pwr = n.CountPermutationsWithRepetition(k);
      //var pwr_f = (int)System.Numerics.BigInteger.Pow(n, k);
    }

    public static (int q, int r) EnvelopDivRem(int d, int n)
    {
      var q = (d / n);
      var r = (d % n);

      q = int.CreateChecked(((double)d / (double)n).Envelop());
      r = int.Abs(d) - int.Abs(q) * int.Abs(n);

      return (q, r);
    }

    public static (int q, int r) FloorDivRem(int d, int n)
    {
      var q = int.CreateChecked(double.Floor((double)d / (double)n));
      var r = int.Abs(d) - int.Abs(q) * int.Abs(n);

      return (q, r);
    }

    public static (int q, int r) CeilingDivRem(int d, int n)
    {
      var q = int.CreateChecked(double.Ceiling((double)d / (double)n));
      var r = int.Abs(d) - int.Abs(q) * int.Abs(n);

      return (q, r);
    }

    public static (int q, int r) TruncateDivRem(int d, int n)
    {
      var q = int.CreateChecked(double.Ceiling((double)d / (double)n));
      var r = int.Abs(d) - int.Abs(q) * int.Abs(n);

      return (q, r);
    }

    public static void DivRemStuff()
    {
      var d = 9;
      var n = -5;

      var iq = d / n;
      var ir = d % n;

      var rd = (double)d / (double)n;

      var id = int.DivRem(d, n);

      var ed = EnvelopDivRem(d, n);
      var fd = FloorDivRem(d, n);
      var cd = CeilingDivRem(d, n);
      var td = TruncateDivRem(d, n);

    }

    private static void TimedMain(string[] _)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      //var mod = 4;

      //for (var idx = 0; idx < 15; idx++)
      //  System.Console.WriteLine($"{idx:D2} : {idx % mod} : {idx.RemainderNoZero(mod, out var _)} : {idx.ReverseRemainder(mod, out var _)} : {idx.ReverseRemainderNoZero(mod, out var _)} ... {idx.Remainders(mod)}");

      for (var i = System.Numerics.BigInteger.One; i < long.MaxValue; i *= 9)
      {
        var ni = i;// (i + Flux.RandomNumberGenerators.SscRng.Shared.Next(100000000));
        var nin = ni.GetCompoundNumbers(System.Numerics.BigInteger.Parse("1e123", System.Globalization.NumberStyles.AllowExponent, null)).ToList();
        var anis = ni.GetCompoundNumerals(NamingScale.AnyScale);
        var lnis = ni.GetCompoundNumerals(NamingScale.LongScale);
        var snis = ni.GetCompoundNumerals(NamingScale.ShortScale);

        System.Console.WriteLine($"{i:D2}{i.GetOrdinalIndicatorSuffix()} = ({ni}) : {string.Join(", ", nin)}");
        System.Console.WriteLine($"{NumeralComposition.InterpretNumbersAndNumerals(anis)}");
        System.Console.WriteLine($"{NumeralComposition.InterpretNumbersAndNumerals(lnis)}");
        System.Console.WriteLine($"{NumeralComposition.InterpretNumbersAndNumerals(snis)}");
      }

      var dv = 0.15625d;
      var sv = 0.15625f;

      var (Binary64Sign, Binary64ExponentUnbiased, Binary64Significand53) = dv.GetBinary64Components(out var binary64SignBit, out var binary64ExponentBiased, out var binary64Significand52);
      var (Binary32Sign, Binary32ExponentUnbiased, Binary32Significand24) = sv.GetBinary32Components(out var binary32SignBit, out var binary32ExponentBiased, out var binary32Significand23);

      Tewt();

      var irafz = (25.23456).Round(4, HalfRounding.ToRandom);

      DivRemStuff();
      return;

      var n = 20L;

      var f = (n).Factorial();
      var df9 = (n).DoubleFactorial();
      var df8 = (n - 1).DoubleFactorial();

      var df = df9 * df8;

      var dfdiv = (double)df9 / df8;
      var e = double.E;
      var divisor = 5;
      for (var i = 1; i < 19; i++)
      {
        System.Console.WriteLine($"{i:D2} / {divisor} = {i / divisor} ({((double)i / (double)divisor):N3}) : Env = {i.EnvelopedDivRem(divisor)} : Euc = {i.EuclideanDivRem(divisor)} : Flr = {i.FloorDivRem(divisor)} : Trc = {NumberFunctions.TruncRem(i, divisor)} ({NumberFunctions.TruncRem((double)i, (double)divisor)})");
      }

      var lf = double.Log(4.Factorial());

      Zamplez.RunLocale(); return;

      //Flux.Locale.GetProperties().ToJaggedArray()

      var sb = new Flux.SpanBuilder<char>();

      //sb.Append("Hello");
      //sb.Append(' ');
      //sb.Append("World");
      //sb.Append(", ");
      //sb.Append("Again");

      //sb.Prepend("Again");
      //sb.Prepend(", ");
      //sb.Prepend("World");
      //sb.Prepend(' ');
      //sb.Prepend("Hello");

      sb.Append("World").Prepend("Hello").Insert(5, " ").Insert(5, "---").Insert(9, "---");

      sb.RemoveAt(3);

      return;

      //ContinuedFractions();

      //Tewt();

      //var lcm = Flux.MultiplicativeFunctions.LeastCommonMultiple(new BigInteger(1), 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20);

      //var count = 100;
      //var sumofsqrs = Flux.Numerics.NumberSequence.GetNthPowerNumbers(new BigInteger(2)).Take(count).Sum();
      //var sqrofsum = System.Linq.Enumerable.Range(1, count).Sum();
      //sqrofsum = sqrofsum * sqrofsum;
      //var sumsqrdiff = sqrofsum - sumofsqrs;

      //var apc = 14.GetAscendingPrimeCandidates().Take(10).ToArray();

      //for (var i = 0; i <= 101; i++)
      //  System.Console.WriteLine($"{i:D3} : {i.NearestPrimeCandidate(HalfRounding.AwayFromZero, out var tz0, out var afz0):D3} ({tz0:D3}, {afz0:D3})");
      //return;

      //{
      //  var n = 6L;
      //  var k = 2L;

      //  //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => Flux.FactorialPowers.RisingFactorial(n, k), 10000));
      //  //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => Flux.FactorialPowers.FallingFactorial(n, k), 10000));

      //  System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => Flux.StirlingNumbers.StirlingNumber1st(n, k), 1000000));
      //  System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => Flux.StirlingNumbers.StirlingNumber2nd(n, k), 1000000));

      //  return;
      //}

      //var dividend = 23;
      //var divisor = -5;

      //var drenv = dividend.DivRemEnveloped(divisor);
      //var dreuc = dividend.DivRemEuclidean(divisor);
      //var drf = dividend.DivRemFloor(divisor);
      //var tr = dividend.TruncRem(divisor);


      //var ph = (3).FallingFactorial(6);
      //var mul = 3 * 4 * 5 * 6 * 7 * 8;
      //var div = 8.Factorial() / 2.Factorial();
      //var bc = 8.BinomialCoefficient(3);
      //var ccwr = 8.CountCombinationsWithoutRepetition(3);
      //var cpwr = 8.CountPermutationsWithoutRepetition(3);
      //var v = 8.Factorial();
      //var y = 8.FallingFactorial(3);
      //var z = 8.RisingFactorial(3);

      //var src = 9;

      //System.Console.WriteLine($"Number: {src}");

      //var countDivisors = src.CountDivisors();
      //System.Console.WriteLine($"CountDivisors: {countDivisors}");

      //var sumDivisors = src.SumDivisors();
      //System.Console.WriteLine($"SumDivisors: (Sum: {sumDivisors.Sum}, AliqoutSum: {sumDivisors.AliquotSum})");

      //var divisors = src.GetDivisors();
      //System.Console.WriteLine($"Divisors: {string.Join(',', divisors)}");

      //var primeFactors = src.GetPrimeFactors();
      //System.Console.WriteLine($"Prime factors: {string.Join(',', primeFactors)} ({primeFactors.ToPrimeFactorString()})");

      //var countPrimeFactors = src.CountPrimeFactors();
      //System.Console.WriteLine($"CountPrimeFactors: (TotalCount: {countPrimeFactors.TotalCount}, DistinctCount: {countPrimeFactors.DistinctCount})");

      //var eulerTotient = src.EulerTotient();
      //System.Console.WriteLine($"EulerTotient: {string.Join(',', eulerTotient)}");

      //return;

      //var e1 = (22.45).Envelop(-1);
      //var rup = (22.45).Round(-1, HalfRounding.AwayFromZero);

      //var t1 = (-987654321.12345).Round(4, HalfRounding.Alternating);
      //var t2 = (-987654321.12345).Round(4, HalfRounding.Alternating);
      //var t3 = (-987654321.12345).Round(4, HalfRounding.Alternating);
      //var t4 = (-987654321.12345).Round(4, HalfRounding.Alternating);

      ////      4.465, 2

      //var ms1 = Flux.ModularArithmetic.ModSub(33, 11, 12);
      //var ms2 = Flux.ModularArithmetic.ModAdd(11, 11, 12);

      ////Flux.ModularArithmetic.ModAdd(11, 11, 12);
      ////Flux.ModularArithmetic.ModSub(11, 11, 12);

      ////var t = Flux.IModularArithmetic.ModAdd(7, 8, 12);

      ////var x = Flux.IModularArithmetic.ModMul(5, 3, 11);
      ////var y = Flux.IModularArithmetic.ModMul(12, 15, 7);


      ////var ipaks = AksPrimalityTest.IsPrimeAks(37);
      ////var ip = AksPrimalityTest.IsPrime(37);

      //for (var n = 0; n < 30; n++)
      //{
      //  //var pc = n.GetPrimeCandidates(UniversalRounding.HalfAwayFromZero, out var tz, out var afz);
      //  var pc = n.NearestPrimeCandidate(HalfRounding.AwayFromZero, out var tz, out var afz);

      //  System.Console.WriteLine($"{n:D2} (/ 6 = {n / 6}) : (% 6 = {n % 6}) : {tz:D2} | {afz:D2} ~ {pc:D2}");
      //}

      //return;

      //var sieveSize = 1000;
      //var testRuns = 1;

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => Flux.PrimeNumbers.SieveOfEratosthenes(sieveSize), testRuns));

      //var soe = Flux.PrimeNumbers.SieveOfEratosthenes(100);


      //var ips = "The quick brown fox jumps over the lazy dog".AsSpan().IsPangram("abcdefghijklmnopqrstuvwxyz");

      //var gcd = 091575.Gcd(999999);
      //var isc = 100010.IsCoprime(10);

      //var total = new BigInteger(52);
      //var taken = 5;

      //var pwr = total.CountPermutationsWithRepetition(taken);
      //var pwor = total.CountPermutationsWithoutRepetition(taken);
      //var cwr = total.CountCombinationsWithRepetition(taken);
      //var cwor = total.CountCombinationsWithoutRepetition(taken);

      //var atoms = new char[] { 'A', 'B', 'C', 'D' };
      //var radix = new int[3];

      //var count = Flux.LehmerCode.GetRadixes<char>(atoms, radix);

      //var index = new int[radix.Length];

      //var permutation = new char[radix.Length];

      //System.Console.WriteLine($"{atoms.Length} choose {radix.Length} = {count}");

      //for (var i = 0; i < count; i++)
      //{
      //  System.Console.Write($"{(i + 1):D4} : ");

      //  Flux.LehmerCode.Decode(i, radix, index);

      //  Flux.LehmerCode.Encode(index, radix);

      //  System.Console.Write(string.Join(',', index));
      //  Flux.LehmerCode.Reindex(index.AsSpan());

      //  System.Console.Write(" : ");
      //  System.Console.Write(string.Join(',', index));

      //  Flux.LehmerCode.GetPermutationByIndices<char>(atoms, index.AsSpan(), permutation);

      //  System.Console.Write(" : ");
      //  System.Console.WriteLine(string.Join(',', permutation));
      //}

    }

    #region Eliza example

    static void ElizaExample()
    {
      var reflections = new System.Collections.Generic.Dictionary<string, string>
      {
        ["am"] = "are",
        ["was"] = "were",
        ["i"] = "you",
        ["i'd"] = "you would",
        ["i've"] = "you have",
        ["i'll"] = "you will",
        ["my"] = "your",
        ["are"] = "am",
        ["you've"] = "I have",
        ["you'll"] = "I will",
        ["your"] = "my",
        ["yours"] = "mine",
        ["you"] = "me",
        ["me"] = "you"
      };

      var responses = setupResponses();

      System.Console.WriteLine("Therapist\n---------");
      System.Console.WriteLine("Talk to the program by typing in plain English, using normal upper-");
      System.Console.WriteLine("and lower-case letters and punctuation.  Enter 'quit' when done.");
      System.Console.WriteLine(new String('=', 72));
      System.Console.WriteLine("Hello.  How are you feeling today?");

      bool exit = false;

      System.Random rnd = new();

      while (!exit)
      {
        string chosenResponse = "";
        string userInput = System.Console.ReadLine().ToLower();
        // remove punctuation at end of input
        userInput = System.Text.RegularExpressions.Regex.Replace(userInput, @"[^\w\s]{1,}$", "");

        // test all regexes in turn
        foreach (System.Collections.Generic.List<String> possibleResponses in responses)
        {
          // right to left as we only need the last match
          var pattern = new System.Text.RegularExpressions.Regex(possibleResponses[0]);
          // got a bite?
          if (pattern.IsMatch(userInput))
          {
            // pick a random response
            int randomIdx = rnd.Next(1, possibleResponses.Count);
            chosenResponse = possibleResponses[randomIdx];

            // if there's a %1, we need to include part of what the user said
            if (chosenResponse.Contains("%1"))
            {
              // get the bit of the text we're going to include
              var matchedTextGroups = pattern.Match(userInput).Groups;
              string reflectedInput = "";
              foreach (System.Text.RegularExpressions.Group g in matchedTextGroups.Cast<Group>())
              {
                reflectedInput = g.ToString();
              }
              // check all words, flip using reflections (i.e. I am -> you are)
              String[] reflectedInputArray = reflectedInput.Split();
              for (int i = 0; i < reflectedInputArray.Length; i++)
              {
                // element and potential dictionary key are different
                string word = reflectedInputArray[i];
                // strip punctuation so "am," still becomes "are,"
                string possibleKey = System.Text.RegularExpressions.Regex.Replace(word, @"\p{P}", "");
                if (reflections.TryGetValue(possibleKey, out var possibleValue))
                {
                  word = word.Replace(possibleKey, possibleValue);
                  reflectedInputArray[i] = word;
                }
              }
              // join it up again and stick the last matched group into the placeholder
              reflectedInput = String.Join(" ", reflectedInputArray);
              chosenResponse = chosenResponse.Replace("%1", reflectedInput);
              // strip duplicate punctuation
              chosenResponse = System.Text.RegularExpressions.Regex.Replace(chosenResponse, @"([^\w\s])(\1){1,}", @"$2");
              // capitalize first letter
              chosenResponse = Char.ToUpper(chosenResponse[0]).ToString() + chosenResponse[1..];
            }
            break;
          }
        }

        System.Console.WriteLine(chosenResponse);
        if (userInput == "quit")
        {
          exit = true;
        }
      }

      static System.Collections.Generic.List<System.Collections.Generic.List<string>> setupResponses()
      {
        // list of lists - first option in each sub-list is the regex to match,
        // the rest are possible responses
        var allResponses = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
        var uri = new System.Uri(@"file://\Resources\responses.txt");
        using var stream = uri.GetStream();
        using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

        string txtLine = "";
        var tempResponseCollector = new System.Collections.Generic.List<string>();
        while ((txtLine = reader.ReadLine()) != null)
        {
          if (txtLine == "-")
          {
            allResponses.Add(tempResponseCollector);
            // .Clear() gives some odd results here
            tempResponseCollector = [];
          }
          else
          {
            tempResponseCollector.Add(txtLine.ToString());
          }
        }

        return allResponses;
      }
    }

    #endregion // Eliza example

    #region Puzzle

    //public static Grid<int> CompletedPuzzle()
    //{
    //  var grid = new Grid<int>(4, 4);

    //  for (var index = 0; index < grid.Rows * grid.Columns; index++) grid[index] = index + 1;

    //  grid[15] = 0;

    //  return grid;
    //}

    //public static void AssertValidPuzzle(Grid<int> puzzle) { if (!IsValidPuzzle(puzzle)) throw new System.Exception("The board is invalid."); }

    ////public static int[] CreatePuzzle() { var puzzle = CompletedPuzzle; puzzle.AsSpan().Shuffle(); AssertValidPuzzle(puzzle); return puzzle; }

    //public static bool IsValidPuzzle(Grid<int> puzzle)
    //{
    //  var list = puzzle.GetIndexValuePairs();

    //  return list.Any(pair => pair.Value < 0 || pair.Value > 15) && list.Select(kvp => kvp.Value).Distinct().Count() == 16;

    //  //var index = 0;

    //  //foreach (var kvp in puzzle.GetIndexValuePairs())
    //  //  if (kvp.Key != index++)
    //  //    return false;

    //  //if (index != 16)
    //  //  return false;

    //  //return true;
    //}

    //public static bool TryMoveBlock(Grid<int> puzzle, int number, Flux.Units.CardinalDirection direction)
    //{
    //  var index = System.Array.IndexOf(puzzle, number);

    //  if (index < 0) return false;

    //  switch (direction)
    //  {
    //    case Flux.Units.CardinalDirection.N:
    //      if (index - 4 is var up && puzzle[up] == 0)
    //      {
    //        puzzle.Swap(index, up);
    //        return true;
    //      }
    //      break;
    //    case Flux.Units.CardinalDirection.E:
    //      if (index + 1 is var right && puzzle[right] == 0)
    //      {
    //        puzzle.Swap(index, right);
    //        return true;
    //      }
    //      break;
    //    case Flux.Units.CardinalDirection.S:
    //      if (index + 4 is var down && puzzle[down] == 0)
    //      {
    //        puzzle.Swap(index, down);
    //        return true;
    //      }
    //      break;
    //    case Flux.Units.CardinalDirection.W:
    //      if (index - 1 is var left && puzzle[left] == 0)
    //      {
    //        puzzle.Swap(index, left);
    //        return true;
    //      }
    //      break;
    //    default:
    //      throw new System.ArgumentOutOfRangeException(nameof(direction));
    //  }

    //  return false;
    //}

    //public static System.Collections.Generic.IEnumerable<int> MovableBlocks(Grid<int> puzzle)
    //{
    //  for (var index = 0; index < puzzle.Length; index++)
    //  {
    //    if (index - 4 is var indexN && indexN >= 0 && puzzle[indexN] is var blockN && blockN == 0)
    //      yield return puzzle[index];

    //    if (index + 1 is var indexE && indexE <= 15 && puzzle[indexE] is var blockE && blockE == 0)
    //      yield return puzzle[index];

    //    if (index + 4 is var indexS && indexS <= 15 && puzzle[indexS] is var blockS && blockS == 0)
    //      yield return puzzle[index];

    //    if (index - 1 is var indexW && indexW >= 0 && puzzle[indexW] is var blockW && blockW == 0)
    //      yield return puzzle[index];
    //  }
    //}

    //public static void PrintPuzzle(Grid<int> puzzle)
    //{
    //  AssertValidPuzzle(puzzle);

    //  var twod = puzzle.Select(n => n).ToTwoDimensionalArray(4, 4).Rank2ToConsoleString(new ConsoleStringOptions() { CenterContent = true });

    //  System.Console.WriteLine(twod);
    //}

    #endregion // Puzzle

    private static void Main(string[] args)
    {
      var originalOutputEncoding = SetEncoding();

      SetSize(0.75);

      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => TimedMain(args), 1));

      ResetEncoding(originalOutputEncoding);

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();

      #region Support functions

      static void ResetEncoding(System.Text.Encoding originalOutputEncoding)
      {
        System.Console.OutputEncoding = originalOutputEncoding;
      }

      static System.Text.Encoding SetEncoding()
      {
        var originalOutputEncoding = System.Console.OutputEncoding;

        try { System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false); }
        catch { System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8; }

        System.Console.ForegroundColor = System.ConsoleColor.Blue;
        System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} {System.Console.OutputEncoding.HeaderName.ToUpper()} (code page {System.Console.OutputEncoding.CodePage})");
        System.Console.ResetColor();

        return originalOutputEncoding;
      }

      static void SetSize(double percentOfLargestWindowSize)
      {
        if (System.OperatingSystem.IsWindows())
        {
          if (percentOfLargestWindowSize < 0.1 || percentOfLargestWindowSize >= 1.0) throw new System.ArgumentOutOfRangeException(nameof(percentOfLargestWindowSize));

          var width = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowWidth * percentOfLargestWindowSize), System.Console.LargestWindowWidth), short.MaxValue);
          var height = System.Math.Min(System.Math.Min(System.Convert.ToInt32(System.Console.LargestWindowHeight * percentOfLargestWindowSize), System.Console.LargestWindowHeight), short.MaxValue);

          System.Console.SetWindowSize(width, height);
        }
      }

      #endregion Support functions
    }
  }
}
