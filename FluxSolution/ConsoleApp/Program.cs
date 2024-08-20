using System;
using System.Collections;
using System.Collections.Immutable;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Reflection;
using System.Runtime.Intrinsics;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using Flux;
using Flux.DataStructures.UnionFind;
using Flux.Quantities;
using Flux.Text;

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

    private static int TestExcept(int[] a, int[] b) => a.AsReadOnlySpan().Except(b.AsReadOnlySpan()).Count;
    private static int TestIntersect(int[] a, int[] b) => a.AsReadOnlySpan().Intersect(b.AsReadOnlySpan()).Count;
    private static int TestSymmetricDifference(int[] a, int[] b) => a.AsReadOnlySpan().SymmetricExcept(b.AsReadOnlySpan()).Count;
    private static int TestUnion(int[] a, int[] b) => a.AsReadOnlySpan().Union(b.AsReadOnlySpan()).Count;
    private static int TestUnionAll(int[] a, int[] b) => a.AsReadOnlySpan().UnionAll(b.AsReadOnlySpan()).Count;

    static void RunHashSet(int[] a, int[] b, int count = 1000)
    {
      var c = a.ToHashSet();
      var d = b.ToHashSet();

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => c.Except(d).Count(), count));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => c.Intersect(d).Count(), count));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => c.SymmetricDifference(d).Count(), count));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => c.Union(d).Count(), count));
      //System.Console.WriteLine(Flux.Services.Performance.Measure(() => c.UnionAll(d).Count()));
    }

    static void RunReadOnlySpan(int[] a, int[] b, int count = 1000)
    {
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TestExcept(a, b), count));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TestIntersect(a, b), count));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TestSymmetricDifference(a, b), count));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TestUnion(a, b), count));
      //System.Console.WriteLine(Flux.Services.Performance.Measure(() => TestUnionAll(a, b), count));
    }

    private static void TimedMain(string[] _)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      //var ns = System.Enum.GetValues<Flux.Quantities.MetricPrefix>().Select(mp => System.Numerics.BigInteger.CopySign(System.Numerics.BigInteger.Pow(10, int.Abs((int)mp)), (int)mp)).ToArray();
      //var ns = System.Enum.GetValues<Flux.Quantities.MetricPrefix>().Select(mp => System.Numerics.BigInteger.CopySign(System.Numerics.BigInteger.Pow(10, int.Abs((int)mp)), (int)mp)).ToArray();
      var ns = System.Enum.GetValues<Flux.Quantities.BinaryPrefix>()/*.OrderBy(mp => System.Numerics.BigInteger.CopySign(System.Numerics.BigInteger.Pow(10, int.Abs((int)mp)), (int)mp))*/.ToArray();
      //System.Array.Sort(ns);

      //var dupes = new int[] { 1, 2, 3, 2, 3, 4, 3, 4, 5, 4, 5, 6, 5, 6, 7, 6, 7, 8, 7, 8, 9 }.ToList();
      //dupes.RemoveAll(i => i == 3);
      //var nodup = )

      var a = System.Linq.Enumerable.Range(1, 50000).ToArray();
      var b = System.Linq.Enumerable.Range(30001, 600000).ToArray();

      var count = 10000;

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => RunReadOnlySpan(a, b, count), 1));
      System.Console.WriteLine();
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => RunHashSet(a, b, count), 1));
      System.Console.WriteLine();

      return;

      var v = -(1000.0d);

      var mpe = ns.AsReadOnlySpan().GetExtremum(mp => System.Numerics.BigInteger.CopySign(System.Numerics.BigInteger.Pow(2, int.Abs((int)mp)), (int)mp));
      var mpis = ns.AsReadOnlySpan().GetInfimumAndSupremum(1024, mp => System.Numerics.BigInteger.CopySign(System.Numerics.BigInteger.Pow(2, int.Abs((int)mp)), (int)mp), true);

      var mp = Flux.Quantities.MetricPrefix.NoPrefix;
      var bp = Flux.Quantities.BinaryPrefix.NoPrefix;

      var mpvt = mp.GetInfimumAndSupremum(v, true);
      var bpvt = bp.GetInfimumAndSupremum(v, true);

      //var inf = p.GetInfimum(v);
      //System.Console.WriteLine(inf);

      //var sup = p.GetSupremum(v);
      //System.Console.WriteLine(sup);

      //var t = new Flux.Quantities.Time(v);

      //System.Console.WriteLine($"{t.GetSiPrefixSymbol(MetricPrefix.NoPrefix, false)}, {t.GetSiPrefixUnit(MetricPrefix.NoPrefix)}, {t.GetSiPrefixValue(MetricPrefix.NoPrefix)}, {t.ToSiPrefixValueSymbolString(MetricPrefix.NoPrefix)}");
      //System.Console.WriteLine($"{t.GetUnitName(TimeUnit.Second, false)}, {t.GetUnitSymbol(TimeUnit.Second, false)}, {t.GetUnitValue(TimeUnit.Second)}, {t.ToUnitValueSymbolString(TimeUnit.Second)}");

      //System.Console.WriteLine(t.ToSiPrefixValueSymbolString(MetricPrefix.Kilo));

      return;


      var fp = 0.ToBigInteger();
      var na = 0.ToBigInteger();

      for (var n = 0.ToBigInteger(); n < 102; n++)
      {
        var f = n.Factorial();
        var sf = n.SplitFactorial();

        System.Console.WriteLine($"{n} = {f} : {sf}");

        fp += f;
        na += n;
      }

      System.Console.WriteLine(string.Join(", ", Flux.NumberSequence.GetSphenicNumbers(166)));

      var number = 60;

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => string.Join(", ", number.PrimeFactors())));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => number.PrimeFactors().Count));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => number.PrimeFactors().Sum()));

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => string.Join(",", number.Factors(false)), 10));
      System.Console.WriteLine(Flux.Services.Performance.Measure(() => string.Join(",", number.Factors(true)), 10));
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

      Random rnd = new();

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
            tempResponseCollector = new System.Collections.Generic.List<string>();
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

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

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
