using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Flux;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  public class Program
  {
    private static void TimedMain(string[] args)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

      var x = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
      var z = new double[,] { { 0, 0.25, 0 }, { 1, 0, 0.5 }, { 1, 0, 0 } };
      System.Console.WriteLine(x.ToConsoleBlock());
      System.Console.WriteLine();
      Flux.ArrayRank2.TransposeInPlace(x);
      var y = x;
      System.Console.WriteLine(y.ToConsoleBlock());
      System.Console.WriteLine();

      System.Console.WriteLine(z.ToConsoleBlock());
      System.Console.WriteLine();

      foreach (var vertex in z.GraphGetVertices())
        System.Console.WriteLine(vertex);
      System.Console.WriteLine();

      foreach (var edge in z.GraphGetEdges())
        System.Console.WriteLine(edge);
      System.Console.WriteLine();

      foreach (var cost in z.GraphDijkstraShortestPathTree(0, v => System.Convert.ToDouble(v)))
        System.Console.WriteLine(cost);
      System.Console.WriteLine();

      return;

      var a = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

      var b = a.Insert(10, 11, 12, 12);

      //Flux.ArrayRank1.Remove(ref a, 6, 3);
      //var b = a.Remove(7, 3);

      var span = "robertHugo".EnumerateRunes().ToList().ToReadOnlySpan();

      var rb = new Flux.DataStructures.RuneBuilder();
      //rb.Append('*');
      //rb.Prepend(Angle.PrimeSymbol);
      rb.Append("'Snobben' ");
      rb.Prepend((System.Text.Rune)' ');
      rb.Append((System.Text.Rune)'H');
      rb.Append((System.Text.Rune)'u');
      rb.Append((System.Text.Rune)'g');
      rb.Append((System.Text.Rune)'o');
      rb.Append((System.Text.Rune)0x1D2E1);
      rb.Prepend((System.Text.Rune)'t');
      rb.Prepend((System.Text.Rune)'r');
      rb.Prepend((System.Text.Rune)'e');
      rb.Prepend((System.Text.Rune)'b');
      rb.Prepend((System.Text.Rune)'o');
      rb.Prepend((System.Text.Rune)'R');
      rb.Prepend((System.Text.Rune)' ');
      rb.Prepend((System.Text.Rune)'o');
      rb.Prepend((System.Text.Rune)'T');
      rb.Prepend("Congratulations ");
      rb.Append(", to retirement.");

      System.Console.WriteLine($"\"{rb.ToString(0, rb.Count)}\", {rb.Count}/{rb.GetUtf16SequenceLength()}/{rb.GetUtf8SequenceLength()}");
      return;

      var s1 = "lowerCase2UpperSLOWCase";
      System.Console.WriteLine(s1);
      var s2 = s1.ToStringBuilder().SplitCamelCase().ToLowerFirstCharacters().ToString();
      System.Console.WriteLine(s2);
      var s3 = s2.ToStringBuilder().MakeCamelCase(char.IsWhiteSpace).ToString();
      System.Console.WriteLine(s3);

      return;

      //Flux.ConstraintPropagationSolver.Example();

      //var amb = new Flux.AmbOps.Amb();

      //var domain = new[] { 1, 2, 3, 4, 5 };
      //var terms = new System.Collections.Generic.Dictionary<Flux.AmbOps.IValue<int>, string>();
      //Flux.AmbOps.IValue<int> Term(string name)
      //{
      //  var x = amb.Choose(domain);
      //  terms.Add(x, name);
      //  return x;
      //};

      //void IsUnequal(params Flux.AmbOps.IValue<int>[] values) => amb.Require(() => values.Select(v => v.Value).Distinct().Count() == 5);
      //void IsMatch(Flux.AmbOps.IValue<int> left, Flux.AmbOps.IValue<int> right) => amb.Require(() => left.Value == right.Value);
      //void IsNextOrdered(Flux.AmbOps.IValue<int> left, Flux.AmbOps.IValue<int> right) => amb.Require(() => right.Value - left.Value == 1);
      //void IsInHouse(Flux.AmbOps.IValue<int> attrib, int house) => amb.Require(() => attrib.Value == house);
      //void IsNextTo(Flux.AmbOps.IValue<int> left, Flux.AmbOps.IValue<int> right) => amb.Require(() => Math.Abs(left.Value - right.Value) == 1);


      //Flux.AmbOps.IValue<int> english = Term("Englishman"), swede = Term("Swede"), dane = Term("Dane"), norwegian = Term("Norwegian"), german = Term("German");
      //IsIn(norwegian, 1);
      //IsUnequal(english, swede, german, dane, norwegian);

      //Flux.AmbOps.IValue<int> red = Term("red"), green = Term("green"), white = Term("white"), blue = Term("blue"), yellow = Term("yellow");
      //IsUnequal(red, green, white, blue, yellow);
      //IsNextTo(norwegian, blue);
      //IsLeftOf(green, white);
      //IsSame(english, red);

      //Flux.AmbOps.IValue<int> tea = Term("tea"), coffee = Term("coffee"), milk = Term("milk"), beer = Term("beer"), water = Term("water");
      //IsIn(milk, 3);
      //IsUnequal(tea, coffee, milk, beer, water);
      //IsSame(dane, tea);
      //IsSame(green, coffee);

      //Flux.AmbOps.IValue<int> dog = Term("dog"), birds = Term("birds"), cats = Term("cats"), horse = Term("horse"), zebra = Term("zebra");
      //IsUnequal(dog, cats, birds, horse, zebra);
      //IsSame(swede, dog);

      //Flux.AmbOps.IValue<int> pallmall = Term("pallmall"), dunhill = Term("dunhill"), blend = Term("blend"), bluemaster = Term("bluemaster"), prince = Term("prince");
      //IsUnequal(pallmall, dunhill, bluemaster, prince, blend);
      //IsSame(pallmall, birds);
      //IsSame(dunhill, yellow);
      //IsNextTo(blend, cats);
      //IsNextTo(horse, dunhill);
      //IsSame(bluemaster, beer);
      //IsSame(german, prince);
      //IsNextTo(water, blend);


      //Flux.AmbOps.IValue<int> englishman = Term("englishman"), spaniard = Term("spaniard"), ukranian = Term("ukranian"), norwegian = Term("norwegian"), japanese = Term("japanese");
      //IsUnequal(englishman, spaniard, ukranian, norwegian, japanese);

      //Flux.AmbOps.IValue<int> red = Term("red"), green = Term("green"), ivory = Term("ivory"), yellow = Term("yellow"), blue = Term("blue");
      //IsUnequal(red, green, ivory, blue, yellow);

      //Flux.AmbOps.IValue<int> coffee = Term("coffee"), tea = Term("tea"), milk = Term("milk"), orangejuice = Term("orangejuice"), water = Term("water");
      //IsUnequal(tea, coffee, milk, orangejuice, water);

      //Flux.AmbOps.IValue<int> dog = Term("dog"), snails = Term("snails"), fox = Term("fox"), horse = Term("horse"), zebra = Term("zebra");
      //IsUnequal(dog, snails, fox, horse, zebra);

      //Flux.AmbOps.IValue<int> oldgold = Term(nameof(oldgold)), kools = Term(nameof(kools)), chesterfield = Term(nameof(chesterfield)), luckystrike = Term(nameof(luckystrike)), parliament = Term(nameof(parliament));
      //IsUnequal(oldgold, kools, luckystrike, parliament, chesterfield);


      //IsMatch(englishman, red);
      //IsMatch(spaniard, dog);
      //IsMatch(coffee, green);
      //IsMatch(ukranian, tea);

      //IsNextOrdered(ivory, green);

      //IsMatch(oldgold, snails);
      //IsMatch(kools, yellow);

      //IsInHouse(milk, 3);
      //IsInHouse(norwegian, 1);

      //IsNextTo(chesterfield, fox);
      //IsNextTo(kools, horse);

      //IsMatch(luckystrike, orangejuice);
      //IsMatch(japanese, parliament);

      //IsNextTo(norwegian, blue);

      //if (!amb.Disambiguate())
      //{
      //  System.Console.WriteLine("No solution found.");
      //  System.Console.ReadKey();
      //  return;
      //}


      //var h = new System.Collections.Generic.List<string>[5];
      //for (int i = 0; i < 5; i++)
      //  h[i] = new System.Collections.Generic.List<string>();

      //foreach (var (key, value) in terms.Select(kvp => (kvp.Key, kvp.Value)))
      //{
      //  h[key.Value - 1].Add(value);
      //}

      //var owner = String.Concat(h.Where(l => l.Contains("zebra")).Select(l => l[0]));
      //System.Console.WriteLine($"The {owner} owns the zebra");

      //foreach (var house in h)
      //{
      //  System.Console.Write("|");
      //  foreach (var attrib in house)
      //    System.Console.Write($"{attrib,-10}|");
      //  System.Console.Write("\n");
      //}
      //System.Console.ReadKey();

      //var owner = _solved.nations[_solved.pets.IndexOf(Pet.Zebra)];
      //WriteLine($"The zebra owner is {owner}");
      //Write(ToString());
      //Read();


      // http://rosettacode.org/wiki/Zebra_puzzle#C.23
    }

    private static void Main(string[] args)
    {
      var originalOutputEncoding = System.Console.OutputEncoding;

      try { System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false); }
      catch { System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8; }

      System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} {System.Console.OutputEncoding.HeaderName.ToUpper()} (code page {System.Console.OutputEncoding.CodePage})");
      System.Console.WriteLine();

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      System.Console.OutputEncoding = originalOutputEncoding;

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
