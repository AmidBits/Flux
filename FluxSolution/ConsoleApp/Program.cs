using System.Linq;
using Flux;

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

    private static void EvaluateNumericStuff()
    {

      var value = 128;
      var multiple = -4;
      var radix = 2;

      ////      var mTowardsZero = value.MultipleOfTowardZero(multiple);
      //var mTowardsZeropf = value.MultipleOfTowardZero(multiple, false);
      //var mTowardsZeropt = value.MultipleOfTowardZero(multiple, true);

      ////      var mAwayFromZero = value.MultipleOfAwayFromZero(multiple);
      //var mAwayFromZeropf = value.MultipleOfAwayFromZero(multiple, false);
      //var mAwayFromZeropt = value.MultipleOfAwayFromZero(multiple, true);

      var rtmo = value.RoundToMultipleOf(multiple, true, Flux.RoundingMode.AwayFromZero, out var mTowardsZero, out var mAwayFromZero);

      var rtp = Flux.Units.Radix.RoundToPowOf(value, radix, true, Flux.RoundingMode.AwayFromZero, out var rtpTowardsZero, out var rtpAwayFromZero);

      var quotient = int.CreateChecked(value.AssertNonNegative().TruncMod(1, out var remainder));

      var p2TowardsZero = quotient.MostSignificant1Bit();
      var p2AwayFromZero = (p2TowardsZero < quotient || remainder > 0) ? (p2TowardsZero == 0 ? 1 : p2TowardsZero << 1) : p2TowardsZero;

      var p2TowardsZerop = p2TowardsZero == value ? p2TowardsZero >> 1 : p2TowardsZero;
      var p2AwayFromZerop = p2AwayFromZero == value ? p2AwayFromZero << 1 : p2AwayFromZero;

      var n = (int)(short.MaxValue / sbyte.MaxValue);
      n = -3;
      System.Console.WriteLine($"        Number = {n}");

      var bibs = new SpanBuilder<char>(n.ToBinaryString()).InsertEvery(' ', 3).AsReadOnlySpan();
      System.Console.WriteLine($"        Binary = {bibs}");
      var bios = n.ToOctalString();
      System.Console.WriteLine($"         Octal = {bios}");
      var bids = n.ToDecimalString();
      System.Console.WriteLine($"       Decimal = {bids}");
      var bihs = n.ToHexadecimalString();
      System.Console.WriteLine($"   Hexadecimal = {bihs}");
      var bir2s = n.ToRadixString(2);
      System.Console.WriteLine($"       Radix 2 = {bir2s}");
      var bir8s = n.ToRadixString(8);
      System.Console.WriteLine($"       Radix 8 = {bir8s}");
      var bir10s = n.ToRadixString(10);
      System.Console.WriteLine($"      Radix 10 = {bir10s}");
      var bir16s = n.ToRadixString(16);
      System.Console.WriteLine($"      Radix 16 = {bir16s}");

      //var rn = n.BinaryToGray();
      //var rrn = rn.GrayToBinary();

      //      n = 0;
      //      var nlpow2 = n.NextLargerPowerOf2();
      var np2TowardsZero = (int)n.RoundToPowOf2(false, Flux.RoundingMode.TowardZero, out var p2TowardsZerolo, out var p2TowardsZerohi);
      System.Console.WriteLine($" RoundToPow2TowardsZero = {np2TowardsZero}");
      var np2AwayFromZero = (int)n.RoundToPowOf2(false, Flux.RoundingMode.AwayFromZero, out var p2AwayFromZerolo, out var p2AwayFromZerohi);
      System.Console.WriteLine($"RountToPow2AwayFromZero = {np2AwayFromZero}");

      var birbits = n.ReverseBits();
      System.Console.WriteLine($"  Reverse Bits = {birbits.ToBinaryString()}");
      var birbyts = n.ReverseBytes();
      System.Console.WriteLine($" Reverse Bytes = {birbyts.ToBinaryString()}");

      var bfl = n.BitFoldLeft();
      System.Console.WriteLine($" Bit-Fold Left = {bfl}");
      var bfls = bfl.ToBinaryString();
      System.Console.WriteLine($"     As Binary = {bfls}");
      var bfr = n.BitFoldRight();
      System.Console.WriteLine($"Bit-Fold Right = {bfr}");
      var bfrs = bfr.ToBinaryString();
      System.Console.WriteLine($"     As Binary = {bfrs}");
      var bl = n.GetShortestBitLength();
      var bln = n.GetBitLength();
      //var l2 = bi.IntegerLog2();
      var ms1b = n.MostSignificant1Bit();
      var bmr = n.BitMaskRight();
      var bmrs = bmr.ToBinaryString();
      var bmrsl = bmrs.Length;
      var bml = n.BitMaskLeft();
      var bmls = bml.ToBinaryString();
      var bmlsl = bmls.Length;
    }

    #endregion // Presets

    private static void TimedMain(string[] _)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      var vr = new Flux.ValueRange<int>(4, 9);
      var vrt = vr.IterateAlternating(1, SortOrder.Descending, IntervalNotation.Closed).Take(30).ToArray();
      //var vra = vr.IterateRange().ToArray();
      //var vrb = vr.IterateRange(IterativeOrder.Ascending, IntervalConstraint.HalfOpenLeft, 10).Take(20).ToArray();
      //var vrc = vr.IterateRange(IterativeOrder.Descending, IntervalConstraint.HalfOpenRight, 10).Take(20).ToArray();

      var ia = Flux.Iteration.IntervalDescending(5, 9).Take(12).ToArray();

      var n = 1234;

      var alphabet = "0123456789";

      Flux.Units.Radix.TryConvertNumberToPositionalNotationIndices(n, 2, out var npni);

      var symbols = npni.Select(i => alphabet[i]).ToArray();

      var spni = symbols.Select(s => alphabet.IndexOf(s)).ToArray();

      Flux.Units.Radix.TryConvertPositionalNotationIndicesToNumber(spni, 2, out long pnin);

      var gray = Flux.Units.Radix.BinaryToGray(1899U, 10U);

      var db = Flux.NumberSequence.GetDeBruijnSequenceExpanded(10, 4, new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' }).ToArray();

      //var v = 215;

      //var a = Flux.NumberSequences.PerfectNthRoot.GetPerfectNthRootSequence<int>(2).Take(20).ToArray();

      //var c = Flux.NumberSequences.PerfectNthRoot.GetPerfectNthRootSequence<int>(2).PartitionTuple2(false, (l, h, i) => (lo: l, hi: h)).First(p => p.lo <= v && p.hi >= v);

      //var v = 550.ToBigInteger();
      ////var vpow10 = v.PowOf(10);
      //var (log10TowardsZero, log10AwayFromZero) = v.IntegerLog(10);
      //var (log2TowardsZero, log2AwayFromZero) = v.IntegerLog2();
      //var powOfClosest = v.RoundToPowOf(10, true, RoundingMode.HalfTowardZero, out var powOfTowardsZero, out var powOfAwayFromZero);
      //var powOf2Closest = v.RoundToPowOf2(true, RoundingMode.HalfTowardZero, out var pow2TowardsZero, out var pow2AwayFromZero);
      //var pvs = v.GetPlaceValues(10);

      //var vTowardsZero = v.MostSignificant1Bit();
      //var vAwayFromZero = vTowardsZero << 1;

      //byte a = 215;
      //short b = 215;
      //int c = 215;
      //ulong d = 215;
      //System.Int128 e = 215;
      //System.Numerics.BigInteger f = 215;
      ////f += 1;

      //var mdcish = 0b0000000111111111;
      //var mdcish2 = unchecked((short)0b1111111000000000);
      //var mdc = Flux.Bits.GetMaxDigitCount(10, 10, false);

      //var abc = a.GetBitCount();
      //var abl = a.IsISignedNumber();
      //var bbc = b.GetBitCount();
      //var bbl = b.IsISignedNumber();
      //var cbc = c.GetBitCount();
      //var cbl = c.IsISignedNumber();
      //var dbc = d.GetBitCount();
      //var dbl = d.IsISignedNumber();
      //var ebc = e.GetBitCount();
      //var ebl = e.IsISignedNumber();
      //var fbc = f.GetBitCount();
      //var fbl = f.IsISignedNumber();

      //var x = d.IsISignedNumber();
      //var y = d.IsIUnsignedNumber();
    }

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
