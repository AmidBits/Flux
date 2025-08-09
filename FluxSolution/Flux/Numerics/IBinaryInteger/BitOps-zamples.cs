#if DEBUG
namespace Flux
{
  public static partial class Zamples
  {
    private static void BitOpsGeneral()
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

      var rtmo = value.MultipleOfNearest(multiple, true, Flux.UniversalRounding.WholeAwayFromZero, out var mTowardsZero, out var mAwayFromZero);

      var rtpTowardsZero = value.PowOfTowardZero(radix, true);
      var rtpAwayFromZero = value.PowOfAwayFromZero(radix, true);
      var rtp = value.RoundToNearest(Flux.UniversalRounding.WholeAwayFromZero, rtpTowardsZero, rtpAwayFromZero);
      //var rtp = Flux.Quantities.Radix.PowOf(value, radix, true, Flux.RoundingMode.AwayFromZero, out var rtpTowardsZero, out var rtpAwayFromZero);

      var (q, remainder) = value.AssertNonNegativeNumber().TruncRem(1);
      var quotient = int.CreateChecked(q);

      var p2TowardsZero = quotient.MostSignificant1Bit();
      var p2AwayFromZero = (p2TowardsZero < quotient || remainder > 0) ? (p2TowardsZero == 0 ? 1 : p2TowardsZero << 1) : p2TowardsZero;

      var p2TowardsZerop = p2TowardsZero == value ? p2TowardsZero >> 1 : p2TowardsZero;
      var p2AwayFromZerop = p2AwayFromZero == value ? p2AwayFromZero << 1 : p2AwayFromZero;

      var n = (int)(short.MaxValue / sbyte.MaxValue);
      //n = -3;
      System.Diagnostics.Debug.WriteLine($"          Number = {n}");

      var bibs = n.ToBinaryString();
      System.Diagnostics.Debug.WriteLine($"          Binary = {bibs}");
      var bios = n.ToOctalString();
      System.Diagnostics.Debug.WriteLine($"           Octal = {bios}");
      var bids = n.ToDecimalString();
      System.Diagnostics.Debug.WriteLine($"         Decimal = {bids}");
      var bihs = n.ToHexadecimalString();
      System.Diagnostics.Debug.WriteLine($"     Hexadecimal = {bihs}");
      var bir2s = n.ToRadixString(2);
      System.Diagnostics.Debug.WriteLine($"         Radix 2 = {bir2s}");
      var bir8s = n.ToRadixString(8);
      System.Diagnostics.Debug.WriteLine($"         Radix 8 = {bir8s}");
      var bir10s = n.ToRadixString(10);
      System.Diagnostics.Debug.WriteLine($"        Radix 10 = {bir10s}");
      var bir16s = n.ToRadixString(16);
      System.Diagnostics.Debug.WriteLine($"        Radix 16 = {bir16s}");

      //var rn = n.BinaryToGray();
      //var rrn = rn.GrayToBinary();

      //      n = 0;
      //      var nlpow2 = n.NextLargerPowerOf2();
      var np2TowardsZero = (int)n.RoundToNearest(Flux.UniversalRounding.WholeTowardZero, n.Pow2TowardZero(false), n.Pow2AwayFromZero(false));
      System.Diagnostics.Debug.WriteLine($" Pow2TowardsZero = {np2TowardsZero}");
      var np2AwayFromZero = (int)n.RoundToNearest(Flux.UniversalRounding.WholeAwayFromZero, n.Pow2TowardZero(false), n.Pow2AwayFromZero(false));
      System.Diagnostics.Debug.WriteLine($"Pow2AwayFromZero = {np2AwayFromZero}");

      var birbits = n.ReverseBits();
      System.Diagnostics.Debug.WriteLine($"    Reverse Bits = {birbits.ToBinaryString()}");
      var birbyts = n.ReverseBytes();
      System.Diagnostics.Debug.WriteLine($"   Reverse Bytes = {birbyts.ToBinaryString()}");

      var bfl = n.BitFoldMsb();
      System.Diagnostics.Debug.WriteLine($"   Bit-Fold Left = {bfl}");
      var bfls = bfl.ToBinaryString();
      System.Diagnostics.Debug.WriteLine($"       As Binary = {bfls}");
      var bfr = n.BitFoldLsb();
      System.Diagnostics.Debug.WriteLine($"  Bit-Fold Right = {bfr}");
      var bfrs = bfr.ToBinaryString();
      System.Diagnostics.Debug.WriteLine($"       As Binary = {bfrs}");
      //var bsbl = n.GetShortestBitLength();
      var bln = n.GetBitLength();
      //var l2 = bi.IntegerLog2();
      var ms1b = n.MostSignificant1Bit();
      var bmr = n.GetBitLength().CreateBitMaskLsbFromBitLength();
      var bmrs = bmr.ToBinaryString();
      var bmrsl = bmrs.Length;
      var bml = n.GetBitLength().CreateBitMaskMsbFromBitLength();
      var bmls = bml.ToBinaryString();
      var bmlsl = bmls.Length;
    }
  }
}
#endif
