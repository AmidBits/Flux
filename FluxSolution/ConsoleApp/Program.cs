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

  public enum Colour { Red, Green, White, Yellow, Blue }
  public enum Nationality { Englishman, Swede, Dane, Norwegian, German }
  public enum Pet { Dog, Birds, Cats, Horse, Zebra }
  public enum Drink { Coffee, Tea, Milk, Beer, Water }
  public enum Smoke { PallMall, Dunhill, Blend, BlueMaster, Prince }

  public static class ZebraPuzzle
  {
    private static (Colour[] colours, Drink[] drinks, Smoke[] smokes, Pet[] pets, Nationality[] nations) _solved;

    static ZebraPuzzle()
    {
      var solve = from colours in Permute<Colour>()  //r1 5 range
                  where (colours, Colour.White).IsRightOf(colours, Colour.Green) // r5
                  from nations in Permute<Nationality>()
                  where nations[0] == Nationality.Norwegian // r10
                  where (nations, Nationality.Englishman).IsSameIndex(colours, Colour.Red) //r2
                  where (nations, Nationality.Norwegian).IsNextTo(colours, Colour.Blue) // r15
                  from drinks in Permute<Drink>()
                  where drinks[2] == Drink.Milk //r9
                  where (drinks, Drink.Coffee).IsSameIndex(colours, Colour.Green) // r6
                  where (drinks, Drink.Tea).IsSameIndex(nations, Nationality.Dane) //r4
                  from pets in Permute<Pet>()
                  where (pets, Pet.Dog).IsSameIndex(nations, Nationality.Swede) // r3
                  from smokes in Permute<Smoke>()
                  where (smokes, Smoke.PallMall).IsSameIndex(pets, Pet.Birds) // r7
                  where (smokes, Smoke.Dunhill).IsSameIndex(colours, Colour.Yellow) // r8
                  where (smokes, Smoke.Blend).IsNextTo(pets, Pet.Cats) // r11
                  where (smokes, Smoke.Dunhill).IsNextTo(pets, Pet.Horse) //r12
                  where (smokes, Smoke.BlueMaster).IsSameIndex(drinks, Drink.Beer) //r13
                  where (smokes, Smoke.Prince).IsSameIndex(nations, Nationality.German) // r14
                  where (drinks, Drink.Water).IsNextTo(smokes, Smoke.Blend) // r16
                  select (colours, drinks, smokes, pets, nations);

      _solved = solve.First();
    }

    private static int IndexOf<T>(this T[] arr, T obj) => Array.IndexOf(arr, obj);

    private static bool IsRightOf<T, U>(this (T[] a, T v) right, U[] a, U v) => right.a.IndexOf(right.v) == a.IndexOf(v) + 1;

    private static bool IsSameIndex<T, U>(this (T[] a, T v) x, U[] a, U v) => x.a.IndexOf(x.v) == a.IndexOf(v);

    private static bool IsNextTo<T, U>(this (T[] a, T v) x, U[] a, U v) => (x.a, x.v).IsRightOf(a, v) || (a, v).IsRightOf(x.a, x.v);

    // made more generic from https://codereview.stackexchange.com/questions/91808/permutations-in-c
    public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> values)
    {
      if (values.Count() == 1)
        return values.ToSingleton();

      return values.SelectMany(v => Permutations(values.Except(v.ToSingleton())), (v, p) => p.Prepend(v));
    }

    public static IEnumerable<T[]> Permute<T>() => ToEnumerable<T>().Permutations().Select(p => p.ToArray());

    private static IEnumerable<T> ToSingleton<T>(this T item) { yield return item; }

    private static IEnumerable<T> ToEnumerable<T>() => Enum.GetValues(typeof(T)).Cast<T>();

    public static new String ToString()
    {
      var sb = new StringBuilder();
      sb.AppendLine("House Colour Drink    Nationality Smokes     Pet");
      sb.AppendLine("───── ────── ──────── ─────────── ────────── ─────");
      var (colours, drinks, smokes, pets, nations) = _solved;
      for (var i = 0; i < 5; i++)
        sb.AppendLine($"{i + 1,5} {colours[i],-6} {drinks[i],-8} {nations[i],-11} {smokes[i],-10} {pets[i],-10}");
      return sb.ToString();
    }
  }
  public class Program
  {
    private static void TimedMain(string[] args)
    {
      //System.Console.WriteLine($"\u2103 \u2109 \u212A ");
      return;

      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Flux.Zamplez.IsSupported) { Flux.Zamplez.Run(); return; }

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
