﻿using System;
using System.Collections;
using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using Flux;
using Flux.DataStructures.UnionFind;
using Flux.Probabilities;
using Flux.Text;
using Flux.Units;
using Microsoft.VisualBasic;

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

    private static void TimedMain(string[] _)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      var spread = 5.Spread(3, 6, HalfRounding.TowardZero);

      var dividend = 1234d;
      var divisor = 123d;

      var quotient = dividend / divisor;
      var remainder = dividend % divisor;

      var (Q, R) = dividend.TruncRem(divisor);

      var (TruncatedQuotient, Remainder) = dividend.TruncRem(divisor);

      var ens = (1234).ToBinaryString(16);

      var lfb = Flux.IntervalNotation.Closed.LoopBackAndForth(-1, 8).ToArray();
      var lb = Flux.IntervalNotation.Closed.LoopBetween(-1, 8).ToArray();
      var lr = Flux.IntervalNotation.Closed.LoopRange(-1, -10).ToArray();

      return;

      System.Console.WriteLine(Flux.Probabilities.Normal.Distribution(1.0, 1.5).Histogram(-4, 4));

      var xy = "ABCD".Select((e, i) => System.Collections.Generic.KeyValuePair.Create(e, i)).FlipPairs().ToArray();

      var sequence1 = new System.Collections.Generic.List<int> { 1, 2, 3 }.ToArray();
      var sequence2 = new System.Collections.Generic.List<string> { "A", "B", "C" }.ToArray();

      var s1s2 = sequence1.CartesianProductPairWith(sequence2).ToArray();

      //var test0 = .SelectManyMany(z => new int[] { z.First }, z => new string[] { z.Second }).ToArray();
      //var test1 = test0.FlipPairs().ToArray();
      //var test2 = test1.FlipPairs().ToArray();


      var d = new System.Collections.Generic.Dictionary<char, int>() { { 'A', 1 }, { 'B', 2 }, { 'C', 1 } };

      var ds = d.ToSwitchable();

      var sds1 = ds.Switch();
      var sds2 = sds1.Switch();

      var sds2u = sds1.ToUnswitchable();

      //Flux.Globalization.En.NumeralComposition.ShortScaleDictionary.TryGet

      var ssn = Flux.MetricPrefix.Mega.ConvertToShortScale();


      var html = "<div>This is some text by <b>Paka</b></div>";
      //var actual = html.RemoveAllMarkupTags();

      var sm = html.ToSpanMaker();
      var smactual = sm.RemoveRegex(@"(<[^>]+>)+");

      var n = 0x10;
      var nlp2 = n.MostSignificant1Bit() << 1;
      var nsp2 = n.MostSignificant1Bit() >> 1;
      var np2tzf = n.Pow2TowardZero(false);
      var np2afzf = n.Pow2AwayFromZero(false);
      var np2tzt = n.Pow2TowardZero(true);
      var np2afzt = n.Pow2AwayFromZero(true);
      var nflsb = (n.BitFoldLsb() >> 2) + 1;

      var text = "M";
      var bytes = System.Text.Encoding.UTF8.GetBytes(text);
      //bytes.Recode(8, 5, out byte[] recoded);

      bytes.EncodeToIndices(6, out var indices);

      var iwi = Flux.IntervalNotation.Closed.IsWithinInterval(5, 3, 7);


      var di = new System.IO.DirectoryInfo(@"E:\Media\Audio\Tracks");
      var ef = di.EnumerateFiles("*.mp3", System.IO.SearchOption.AllDirectories).Select(fi => fi.FullName).Distinct().Order().ToArray();
      var efid = ef.Select(fn => (fn.GetHashCode(), fn)).Distinct().ToArray();

      var di2 = new System.IO.DirectoryInfo(@"F:\Media\Audio\Sounds");
      var ef2 = di2.EnumerateFiles("*.*", System.IO.SearchOption.AllDirectories).Select(fi => fi.FullName).Distinct().ToArray();
      var efhc2 = ef2.Select(fn => fn.GetHashCode()).Distinct().ToArray();


      var s = "-41 ° 26 '46″ N79 ° 58 ′ 56 ″W";
      s = "a 123b45c";
      var x = s.ToSpanMaker().ReplaceRegex(@"\d+", s => int.TryParse(s, System.Globalization.NumberStyles.Integer, null, out var r) ? r.ToOrdinalIndicatorString() : "UGH!");

      s = "Z̤͔ͧ̑̓ä͖̭̈̇lͮ̒ͫǧ̗͚̚o̙̔ͮ̇͐̇";
      s = "Powerلُلُصّبُلُلصّبُررً ॣ ॣh ॣ ॣ冗\r\n🏳0🌈️\r\nజ్ఞ‌ా";
      var ss = s.AsSpan();
      var tel = s.AsSpan().ToListOfTextElement().Select(l => l.ToString()).ToList().AsSpan();
      var sl = s.Length;
      var t = s.GetTextElements();
      var tl = t.Count;

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
