using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Flux;
using Flux.Formatting;
using Flux.Text;
using Flux.Unicode;
using Flux.Units;
using Microsoft.VisualBasic.FileIO;

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

      ////      var mtz = value.MultipleOfTowardZero(multiple);
      //var mtzpf = value.MultipleOfTowardZero(multiple, false);
      //var mtzpt = value.MultipleOfTowardZero(multiple, true);

      ////      var mafz = value.MultipleOfAwayFromZero(multiple);
      //var mafzpf = value.MultipleOfAwayFromZero(multiple, false);
      //var mafzpt = value.MultipleOfAwayFromZero(multiple, true);

      var rtmo = value.RoundToMultipleOf(multiple, true, RoundingMode.AwayFromZero, out var mtz, out var mafz);

      var rtp = value.RoundToPowOf(radix, true, RoundingMode.AwayFromZero, out var rtptz, out var rtpafz);

      var quotient = int.CreateChecked(value.AssertNonNegative().TruncMod(1, out var remainder));

      var p2tz = quotient.GetMostSignificant1Bit();
      var p2afz = (p2tz < quotient || remainder > 0) ? (p2tz == 0 ? 1 : p2tz << 1) : p2tz;

      var p2tzp = p2tz == value ? p2tz >> 1 : p2tz;
      var p2afzp = p2afz == value ? p2afz << 1 : p2afz;

      var n = (short.MaxValue - sbyte.MaxValue);

      var rn = n.BinaryToGray();
      var rrn = rn.GrayToBinary();

      //      n = 0;
      var ns = n.ToBinaryString();
      //      var nlpow2 = n.NextLargerPowerOf2();
      var np2tz = (int)n.RoundToPow2(false, RoundingMode.TowardZero, out var p2tzlo, out var p2tzhi);
      var np2afz = (int)n.RoundToPow2(false, RoundingMode.AwayFromZero, out var p2afzlo, out var p2afzhi);

      var bi = n;///.ToBigInteger();
      //var birbts = bi.ReverseBits();
      var bibs = bi.ToBinaryString();
      var bios = bi.ToOctalString();
      var bids = bi.ToDecimalString();
      var bihs = bi.ToHexadecimalString();
      var bir2s = bi.ToRadixString(2);
      var bir8s = bi.ToRadixString(8);
      var bir10s = bi.ToRadixString(10);
      var bir16s = bi.ToRadixString(16);
      var bfl = bi.BitFoldLeft();
      var bfls = bfl.ToBinaryString();
      var bfr = bi.BitFoldRight();
      var bfrs = bfr.ToBinaryString();
      var bl = bi.GetShortestBitLength();
      var bln = bi.GetBitLength();
      var l2 = bi.IntegerLog2();
      var ms1b = bi.GetMostSignificant1Bit();
      var bmr = bi.BitMaskRight();
      var bmrs = bmr.ToBinaryString();
      var bmrsl = bmrs.Length;
      var bml = bi.BitMaskLeft();
      var bmls = bml.ToBinaryString();
      var bmlsl = bmls.Length;
    }

    #endregion Presets

    static int MyFunction(int x, int y)
    {
      var v = x;

      for (var i = y - 1; i > 0; i--)
        v *= x;

      return v;
    }

    public static System.Collections.Generic.IEnumerable<TSelf> GetAscendingPotentialPrimes<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var six = TSelf.CreateChecked(6);

      var (quotient, remainder) = TSelf.DivRem(startAt, six);

      var multiple = six * (quotient + (remainder > TSelf.One ? TSelf.One : TSelf.Zero));

      if (quotient == TSelf.Zero) // If startAt is less than 6.
      {
        var two = TSelf.CreateChecked(2);
        var three = TSelf.CreateChecked(3);

        if (remainder <= two) yield return two;
        if (remainder <= three) yield return three;

        multiple = six;
      }
      else if (remainder <= TSelf.One) // Or, either between two potential primes or on right of a % 6 number. E.g. 12 or 13.
      {
        yield return multiple + TSelf.One;
        multiple += six;
      }

      while (true)
      {
        yield return multiple - TSelf.One;
        yield return multiple + TSelf.One;

        multiple += six;
      }
    }

    //public static (TSelf potentialPrimeTowardZero, TSelf potentialPrimeAwayFromZero) RoundToPotentialPrime<TSelf>(TSelf value)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //{
    //  if (TSelf.CreateChecked(3) is var three && value <= three)
    //    return (TSelf.CreateChecked(2), three);
    //  else if (TSelf.CreateChecked(5) is var five && value < five)
    //    return (three, five);

    //  var (potentialPrimeMultipleTowardsZero, potentialPrimeMultipleAwayFromZero) = Flux.Maths.RoundToMultipleOf(value, TSelf.CreateChecked(6), false);

    //  if (potentialPrimeMultipleTowardsZero - TSelf.One is var tzTz && potentialPrimeMultipleAwayFromZero + TSelf.One is var afzAfz && potentialPrimeMultipleTowardsZero == potentialPrimeMultipleAwayFromZero)
    //    return (tzTz, afzAfz);
    //  else if (potentialPrimeMultipleTowardsZero + TSelf.One is var tzAfz && value <= tzAfz)
    //    return (tzTz, tzAfz);
    //  else if (potentialPrimeMultipleAwayFromZero - TSelf.One is var afzTz && value >= afzTz)
    //    return (afzTz, afzAfz);
    //  else
    //    return (tzAfz, afzTz);
    //}

    //public static TSelf RoundToPotentialPrime<TSelf>(TSelf value, RoundingMode mode, out TSelf potentialPrimeTowardZero, out TSelf potentialPrimeAwayFromZero)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //{
    //  (potentialPrimeTowardZero, potentialPrimeAwayFromZero) = RoundToPotentialPrime(value);

    //  return value.RoundToBoundaries(mode, potentialPrimeTowardZero, potentialPrimeAwayFromZero);
    //}

    private static void TimedMain(string[] _)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      //var app = GetAscendingPotentialPrimes(0).Take(30);
      //foreach (var pp in app)
      //  System.Console.WriteLine(pp);

      //for (var i = 0; i < 30; i++)
      //{
      //  var n = RoundToPotentialPrime(i, RoundingMode.HalfTowardZero, out var tz, out var afz);

      //  var dpp = Flux.NumberSequences.PrimeNumberReverse.GetDescendingPotentialPrimes(i).First();
      //  var app = Flux.NumberSequences.PrimeNumber.GetAscendingPotentialPrimes(i).First();

      //  System.Console.WriteLine($"{i:D2} : {n} : ({tz}, {afz}) : ({dpp}, {app})");
      //}

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
