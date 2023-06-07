using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Flux;
using Flux.Formatting;
using Flux.Maths;
using Flux.Text;
using Flux.Unicode;
using Microsoft.VisualBasic.FileIO;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\AmidBits\Flux\FluxSolution\BaseLibrary\bin\Debug\net6.0\BaseLibrary.dll"

namespace ConsoleApp
{
  public class Program
  {
    private static void Eliza()
    {
      // At some point? https://github.com/jeffshrager/elizagen.org/blob/master/Other_Elizas/20120310ShragerNorthEliza.c64basic

      var keywords = new string[] { "CAN YOU", "CAN I", "YOU ARE", "YOURE", "I DONT", "I FEEL", "WHY DONT YOU", "WHY CANT I", "ARE YOU", "I CANT", "I AM", " IM ", "YOU", "I WANT", "WHAT", "HOW", "WHO", "WHERE", "WHEN", "WHY", "NAME", "CAUSE", "SORRY", "DREAM", "HELLO", "HI", "MAYBE", "NO", "YOUR", "ALWAYS", "THINK", "ALIKE", "YES", "FRIEND", "COMPUTER", "NOKEYFOUND" };
      var conjugations = new string[] { " ARE ", " AM ", "WERE ", "WAS ", " YOU ", " I ", "YOUR ", "MY ", " IVE ", " YOUVE ", " IM ", " YOURE " };
      var replies = new string[] { "DON'T YOU BELIEVE THAT I CAN.", "PERHAPS YOU WOULD LIKE TO BE ABLE TO.", "YOU WANT ME TO BE ABLE TO*", "PERHAPS YOU DON'T WANT TO*", "DO YOU WANT TO BE ABLE TO*", "WHAT MAKES YOU THINK I AM*", "DOES IT PLEASE YOU TO BELIEVE I AM*", "PERHAPS YOU WOULD LIKE TO BE*", "DO YOU SOMETIMES WISH YOU WERE*", "DON'T YOU REALLY*", "WHY DON'T YOU*", "DO YOU WISH TO BE ABLE TO*", "DOES THAT TROUBLE YOU?", "TELL ME MORE ABOUT SUCH FEELINGS*", "DO YOU OFTEN FEEL*", "DO YOU ENJOY FEELING*", "DO YOU REALLY BELIEVE I DON'T*", "PERHAPS IN G00D TIME I WILL*", "DO YOU WANT ME TO*", "DO YOU THINK YOU SHOULD BE ABLE TO*", "WHY CAN'T YOU*", "WHY ARE YOU INTERESTED IN WHETHER OR NOT I AM*", "WOULD YOU PREFER IF I WERE NOT*", "PERHAPS IN YOUR FANTASIES I AM*", "HOW DO YOU KNOW YOU CAN'T*", "HAVE YOU TRIED?", "PERHAPS YOU CAN NOW*", "DID YOU COME TO ME BECAUSE YOU ARE*", "HOW LONG HAVE YOU BEEN*", "DO YOU BELIEVE IT IS NORMAL TO BE*", "DO YOU ENJOY BEING*", "WE WERE DISCUSSING YOU-- NOT ME.", "OH, I*", "YOU'RE NOT REALLY TALKING ABOUT ME. ARE YOU?", "WHAT WOULD IT MEAN TO YOU IF YOU GOT*", "WHY DO YOU WANT*", "SUPPOSE YOU SOON GOT*", "WHAT IF YOU NEVER GOT*", "I SOMETIMES ALSO WANT*", "WHY DO YOU ASK?", "DOES THAT QUESTION INTEREST YOU?", "WHAT ANSWER WOULD PLEASE YOU THE MOST?", "WHAT DO YOU THINK?", "ARE SUCH QUESTIONS ON YOUR MIND OFTEN?", "WHAT IS IT THAT YOU REALLY WANT TO KNOW?", "HAVE YOU ASKED ANYONE ELSE?", "HAVE YOU ASKED SUCH QUESTIONS BEFORE?", "WHAT ELSE COMES TO MIND WHEN YOU ASK THAT?", "NAMES DON'T INTEREST ME.", "I DON'T CARE ABOUT NAMES-- PLEASE GO ON.", "IS THAT THE REAL REASON?", "DON'T ANY OTHER REASONS COME TO MIND?", "DOES THAT REASON EXPLAIN ANYTHING ELSE?", "WHAT OTHER REASONS MIGHT THERE BE?", "PLEASE DON'T APOLOGIZE.", "APOLOGIES ARE NOT NECESSARY.", "WHAT FEELINGS DO YOU HAVE WHEN YOU APOLOGIZE.", "DON'T BE SO DEFENSIVE!", "WHAT DOES THAT DREAM SUGGEST TO YOU?", "DO YOU DREAM OFTEN?", "WHAT PERSONS APPEAR IN YOUR DREAMS?", "ARE YOU DISTURBED BY YOUR DREAMS?", "HOW DO YOU DO .,. PLEASE STATE YOUR PROBLEM.", "YOU DON'T SEEM QUITE CERTAIN.", "WHY THE UNCERTAIN TONE?", "CAN'T YOU BE MORE POSITIVE?", "YOU AREN'T SURE?", "DON'T YOU KNOW?", "ARE YOU SAYING NO JUST TO BE NEGATIVE?", "YOU ARE BEING A BIT NEGATIVE.", "WHY NOT?", "ARE YOU SURE?", "WHY NO?", "WHY ARE YOU CONCERNED ABOUT MY*", "WHAT ABOUT YOUR OWN*", "CAN'T YOU THINK OF A SPECIFIC EXAMPLE?", "WHEN?", "WHAT ARE YOU THINKING OF?", "REALLY. ALWAYS?", "DO YOU REALLY THINK SO?", "BUT YOU ARE NOT SURE YOU.", "DO YOU DOUBT YOU.", "IN WHAT WAY?", "WHAT RESEMBLANCE DO YOU SEE?", "WHAT DOES THE SIMILARITY SUGGEST TO YOU?", "WHAT OTHER CONNECTIONS DO YOU SEE?", "COULD THERE REALLY BE SOME CONNECTION?", "HOW?", "YOU SEEM QUITE POSITIVE.", "ARE YOU SURE?", "I SEE.", "I UNDERSTAND.", "WHY DO YOU BRING UP THE TOPIC OF FRIENDS?", "DO YOUR FRIENDS WORRY YOU?", "DO YOUR FRIENDS PICK ON YOU?", "ARE YOU SURE YOU HAVE ANY FRIENDS?", "DO YOU IMPOSE ON YOUR FRIENDS?", "PERHAPS YOUR LOVE FOR FRIENDS WORRIES YOU.", "DO COMPUTERS WORRY YOU?", "ARE YOU TALKING ABOUT ME IN PARTICULAR?", "ARE YOU FRIGHTENED BY MACHINES?", "WHY DO YOU MENTION COMPUTERS?", "WHAT DO YOU THINK MACHINES HAVE TO DO WITH YOUR PROBLEM?", "DON'T YOU THINK COMPUTERS CAN HELP PEOPLE?", "WHAT IS IT ABOUT MACHINES THAT WORRIES YOU?", "SAY, DO YOU HAVE ANY PSYCHOLOGICAL PROBLEMS?", "WHAT DOES THAT SUGGEST TO YOU?", "I SEE.", "I'M NOT SURE I UNDERSTAND YOU FULLY.", "COME COME ELUCIDATE YOUR THOUGHTS.", "CAN YOU ELABORATE ON THAT?", "THAT IS QUITE INTERESTING." };
      var finding = new int[] { 1, 3, 4, 2, 6, 4, 6, 4, 10, 4, 14, 3, 17, 3, 20, 2, 22, 3, 25, 3, 28, 4, 28, 4, 32, 3, 35, 5, 40, 9, 40, 9, 40, 9, 40, 9, 40, 9, 40, 9, 49, 2, 51, 4, 55, 4, 59, 4, 63, 1, 63, 1, 64, 5, 69, 5, 74, 2, 76, 4, 80, 3, 83, 7, 90, 3, 93, 6, 99, 7, 106, 6 };
    }
    public struct X
    {
      public required int x;
    }
    private static void TimedMain(string[] _)
    {
      //if (args.Length is var argsLength && argsLength > 0) System.Console.WriteLine($"Args ({argsLength}):{System.Environment.NewLine}{string.Join(System.Environment.NewLine, System.Linq.Enumerable.Select(args, s => $"\"{s}\""))}");
      //if (Zamplez.IsSupported) { Zamplez.Run(); return; }

      var ntp = new Flux.Net.NetworkTimeProtocol();

      ntp.Request();

      var slope = new Flux.Units.BigRational(5, 8) + 2;
      var slopeThird = slope / 3;
      var slopes = slope.ToImproperString();

      var carportPostHeight = new Flux.Units.Length(12, Flux.Units.LengthUnit.Foot);

      var carportPostHeightIn = carportPostHeight.ToUnitString(Flux.Units.LengthUnit.Inch);
      var carportPostHeightFt = carportPostHeight.ToUnitString(Flux.Units.LengthUnit.Foot);

      // Frame:
      var carportPostWidth = new Flux.Units.Length(291, Flux.Units.LengthUnit.Inch);
      var carportPostDepth = new Flux.Units.Length(288, Flux.Units.LengthUnit.Inch);

      // Posts:
      //var carportPostWidth = new Flux.Units.Length(279, Flux.Units.LengthUnit.Inch);
      //var carportPostDepth = new Flux.Units.Length(180, Flux.Units.LengthUnit.Inch);

      System.Int32 six = 6;
      var alsosix = six;

      var carportCenterPostDiagonalVector = new Flux.Geometry.CartesianCoordinate2<double>(carportPostWidth.Value, carportPostDepth.Value);
      var carportCenterPostDiagonal = new Flux.Units.Length(carportCenterPostDiagonalVector.EuclideanLength());

      var carportDiagonalIn = carportCenterPostDiagonal.ToUnitString(Flux.Units.LengthUnit.Inch);
      var carportDiagonalFt = carportCenterPostDiagonal.ToUnitString(Flux.Units.LengthUnit.Foot);

      var carportDiagonalFtBr = carportCenterPostDiagonal.ToUnitValue(Flux.Units.LengthUnit.Foot).ToBigRational(2);

      var num = -2039;
      var str = num.ToBinaryString();
      var numtxt = Flux.Text.PositionalNotation.Base10.NumberToText(num);
      var txtnum = Flux.Text.PositionalNotation.Base10.TextToNumber(numtxt);

      for (var index = 1; index < 101; index++)
      {
        System.Console.WriteLine($"{index}");
        System.Console.WriteLine($"Bin: {index.ToBinaryString()}");
        System.Console.WriteLine($"Dec: {index.ToDecimalString()}");
        System.Console.WriteLine($"Hex: {index.ToHexadecimalString()}");
        System.Console.WriteLine($"Oct: {index.ToOctalString()}");
        System.Console.WriteLine();
      }

      var s = System.Numerics.BigInteger.Parse("93528675864").ToString("X");
      var sl = 93528675864L.ToString("X");
      var slb = 93528675864L.ToHexadecimalString();

      var ui16 = unchecked((ushort)0xC0C0);
      var ui32 = unchecked((uint)0xC0C0C0C0U);
      var ui64 = unchecked((ulong)0xC0C0C0C0C0C0C0C0UL);

      ui16.ReverseBits();
      ui32.ReverseBits();
      ui64.ReverseBits();

      var n = (short.MaxValue - sbyte.MaxValue);
      var nlpo2 = n.NextLargerPowerOf2();
      var bi = n;//.ToBigInteger();
      var birbts = bi.ReverseBits();
      var bis = bi.ToBinaryString();
      var bfl = bi.BitFoldLeft();
      var bfr = bi.BitFoldRight();
      var bl = bi.ShortestBitLength();
      var bln = bi.BitLengthN();
      var l2 = bi.IntegerLog2();
      var ms1b = bi.MostSignificant1Bit();
      var bmr = bi.BitMaskRight();
      var bmrs = bmr.ToBinaryString();
      var bmrsl = bmrs.Length;
      var bml = bi.BitMaskLeft();
      var bmls = bml.ToBinaryString();
      var bmlsl = bmls.Length;
      return;
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
