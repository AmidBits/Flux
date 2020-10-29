using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maths
{
  [TestClass]
  public class Bitwise
  {
    //readonly System.Numerics.BigInteger[] nbi = System.Linq.Enumerable.Range(0, 255).Select(i => -(System.Numerics.BigInteger)i).ToArray();
    //readonly short[] ns = System.Linq.Enumerable.Range(0, 255).Select(i => (short)-i).ToArray();
    //readonly int[] ni = System.Linq.Enumerable.Range(0, 255).Select(i => (int)-i).ToArray();
    //readonly long[] nl = System.Linq.Enumerable.Range(0, 255).Select(i => -(long)i).ToArray();
    //readonly sbyte[] nsb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)-i).ToArray(); // Restricted to -127.

    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly byte[] pb = System.Linq.Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
    readonly short[] ps = System.Linq.Enumerable.Range(0, 255).Select(i => (short)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly sbyte[] psb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)i).ToArray(); // Restricted to 127.
    readonly ushort[] pus = System.Linq.Enumerable.Range(0, 255).Select(i => (ushort)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    //[TestMethod]
    //public void Abs()
    //{
    //  Assert.AreEqual(pi[1], Flux.Math.Abs(nsb[1]));
    //  Assert.AreEqual(pi[1], Flux.Math.Abs(ns[1]));
    //  Assert.AreEqual(pi[1], Flux.Math.Abs(ni[1]));
    //  Assert.AreEqual(pl[1], Flux.Math.Abs(nl[1]));
    //}

    //[TestMethod]
    //public void Abs_Speed()
    //{
    //  var value = System.Numerics.BigInteger.Parse("-670530");

    //  Flux.Diagnostics.Performance.Measure(() => Flux.Math.Abs(value)).Assert((System.Numerics.BigInteger)670530, 0.20);
    //  if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Abs((int)value)).Assert(670530, 0.175);
    //  if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Abs((long)value)).Assert(670530L, 0.175);

    //  //      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => null).Assert(value, 0.175);
    //}

    [TestMethod]
    public void Bit1CountBI()
      => Assert.AreEqual(2, Flux.Maths.Bit1Count(18.ToBigInteger()));
    [TestMethod]
    public void Bit1CountInt32()
      => Assert.AreEqual(2, Flux.Maths.Bit1Count(18));
    [TestMethod]
    public void Bit1CountInt64()
      => Assert.AreEqual(2, Flux.Maths.Bit1Count(18L));
    [TestMethod]
    public void Bit1CountUInt32()
      => Assert.AreEqual(2, Flux.Maths.Bit1Count(18U));
    [TestMethod]
    public void Bit1CountUInt64()
      => Assert.AreEqual(2, Flux.Maths.Bit1Count(18UL));

    [TestMethod]
    public void Bit1Count_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      var expected = Flux.Text.PositionalNotation.GetFromRadix(2).NumberToText(value).Count(c => c == '1');

      Flux.Diagnostics.Performance.Measure(() => (System.Numerics.BigInteger)Flux.Maths.Bit1Count(value), 500000).Assert((System.Numerics.BigInteger)expected, 0.7); // Large discrepancy between Debug and Release code. Not a problem.
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Bit1Count((int)value), 500000).Assert(expected, 0.20);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Bit1Count((long)value), 500000).Assert(expected, 0.20);
      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Bit1Count((uint)value), 500000).Assert(expected, 0.175);
      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Bit1Count((ulong)value), 500000).Assert(expected, 0.175);
    }

    [TestMethod]
    public void BitIndexBI()
      => Assert.AreEqual(4, Flux.Maths.BitIndex(16.ToBigInteger()));
    [TestMethod]
    public void BitIndexInt32()
      => Assert.AreEqual(4, Flux.Maths.BitIndex(16));
    [TestMethod]
    public void BitIndexInt64()
      => Assert.AreEqual(4, Flux.Maths.BitIndex(16L));
    [TestMethod]
    public void BitIndexUInt32()
      => Assert.AreEqual(4, Flux.Maths.BitIndex(16U));
    [TestMethod]
    public void BitIndexUInt64()
      => Assert.AreEqual(4, Flux.Maths.BitIndex(16UL));

    [TestMethod]
    public void BitLengthBI()
      => Assert.AreEqual(5, Flux.Maths.BitLength(18.ToBigInteger()));
    [TestMethod]
    public void BitLengthInt32()
      => Assert.AreEqual(5, Flux.Maths.BitLength(18));
    [TestMethod]
    public void BitLengthInt64()
      => Assert.AreEqual(5, Flux.Maths.BitLength(18L));
    [TestMethod]
    public void BitLengthUInt32()
      => Assert.AreEqual(5, Flux.Maths.BitLength(18U));
    [TestMethod]
    public void BitLengthUInt64()
      => Assert.AreEqual(5, Flux.Maths.BitLength(18UL));

    [TestMethod]
    public void BitLength_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      Flux.Diagnostics.Performance.Measure(() => Flux.Maths.BitLength(value), 500000).Assert(20, 0.75);
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.BitLength((int)value), 500000).Assert(20, 0.20);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.BitLength((long)value), 500000).Assert(20, 0.20);
      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.BitLength((uint)value), 500000).Assert(20, 0.20);
      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.BitLength((ulong)value), 500000).Assert(20, 0.20);
    }

    [TestMethod]
    public void CountLeadingZerosBI()
      => Assert.AreEqual(3, Flux.Maths.CountLeadingZeros(18.ToBigInteger()));
    [TestMethod]
    public void CountLeadingZerosInt32()
      => Assert.AreEqual(27, Flux.Maths.CountLeadingZeros(18));
    [TestMethod]
    public void CountLeadingZerosInt64()
      => Assert.AreEqual(59, Flux.Maths.CountLeadingZeros(18L));
    [TestMethod]
    public void CountLeadingZerosUInt32()
      => Assert.AreEqual(27, Flux.Maths.CountLeadingZeros(18U));
    [TestMethod]
    public void CountLeadingZerosUInt64()
      => Assert.AreEqual(59, Flux.Maths.CountLeadingZeros(18UL));

    [TestMethod]
    public void CountLeadingZeros()
    {
      //Assert.AreEqual(pbi[3], Flux.Math.CountLeadingZeros(pbi[18], 8));
      Assert.AreEqual(pi[27], Flux.Maths.CountLeadingZeros(pui[18]));
      Assert.AreEqual(pi[59], Flux.Maths.CountLeadingZeros(pul[18]));
    }

    [TestMethod]
    public void CountLeadingZeros_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      //Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountLeadingZeros(value, 128)).Assert(108, 0.75);
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.CountLeadingZeros((int)value)).Assert(12, 0.175);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.CountLeadingZeros((long)value)).Assert(44, 0.175);
      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.CountLeadingZeros((uint)value)).Assert(12, 0.175);
      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.CountLeadingZeros((ulong)value)).Assert(44, 0.175);
    }

    [TestMethod]
    public void CountTrailingZerosBI()
      => Assert.AreEqual(1, Flux.Maths.CountTrailingZeros(18.ToBigInteger()));
    [TestMethod]
    public void CountTrailingZerosInt32()
      => Assert.AreEqual(1, Flux.Maths.CountTrailingZeros(18));
    [TestMethod]
    public void CountTrailingZerosInt64()
      => Assert.AreEqual(1, Flux.Maths.CountTrailingZeros(18L));
    [TestMethod]
    public void CountTrailingZerosUInt32()
      => Assert.AreEqual(1, Flux.Maths.CountTrailingZeros(18U));
    [TestMethod]
    public void CountTrailingZerosUInt64()
      => Assert.AreEqual(1, Flux.Maths.CountTrailingZeros(18UL));

    [TestMethod]
    public void CountTrailingZeros_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      var expected = 1;

      Flux.Diagnostics.Performance.Measure(() => Flux.Maths.CountTrailingZeros(value), 500000).Assert(expected, 0.75);
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.CountTrailingZeros((int)value), 500000).Assert(expected, 0.175);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.CountTrailingZeros((long)value), 500000).Assert(expected, 0.175);
      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.CountTrailingZeros((uint)value), 500000).Assert(expected, 0.175);
      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.CountTrailingZeros((ulong)value), 500000).Assert(expected, 0.175);
    }

    [TestMethod]
    public void FoldBitsBI()
      => Assert.AreEqual(31, Flux.Maths.FoldBits(18.ToBigInteger()));
    [TestMethod]
    public void FoldBitsInt32()
      => Assert.AreEqual(31, Flux.Maths.FoldBits(18));
    [TestMethod]
    public void FoldBitsInt64()
      => Assert.AreEqual(31, Flux.Maths.FoldBits(18L));
    [TestMethod]
    public void FoldBitsUInt32()
      => Assert.AreEqual(31U, Flux.Maths.FoldBits(18U));
    [TestMethod]
    public void FoldBitsUInt64()
      => Assert.AreEqual(31UL, Flux.Maths.FoldBits(18UL));

    [TestMethod]
    public void FoldBits_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      var expected = 1048575;

      Flux.Diagnostics.Performance.Measure(() => Flux.Maths.FoldBits(value), 500000).Assert((System.Numerics.BigInteger)expected, 2);
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.FoldBits((int)value), 500000).Assert((int)expected, 0.175);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.FoldBits((long)value), 500000).Assert((long)expected, 0.175);
      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.FoldBits((uint)value), 500000).Assert((uint)expected, 0.175);
      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.FoldBits((ulong)value), 500000).Assert((ulong)expected, 0.175);
    }

    [TestMethod]
    public void LeastSignificant1Bit()
    {
      Assert.AreEqual(pbi[2], Flux.Maths.LeastSignificant1Bit(pbi[18]));
      Assert.AreEqual(pb[2], Flux.Maths.LeastSignificant1Bit(pb[18]));
      Assert.AreEqual(pi[2], Flux.Maths.LeastSignificant1Bit(pi[18]));
      Assert.AreEqual(pl[2], Flux.Maths.LeastSignificant1Bit(pl[18]));
      Assert.AreEqual(psb[2], Flux.Maths.LeastSignificant1Bit(psb[18]));
      Assert.AreEqual(ps[2], Flux.Maths.LeastSignificant1Bit(ps[18]));
      Assert.AreEqual(pui[2], Flux.Maths.LeastSignificant1Bit(pui[18]));
      Assert.AreEqual(pul[2], Flux.Maths.LeastSignificant1Bit(pul[18]));
      Assert.AreEqual(pus[2], Flux.Maths.LeastSignificant1Bit(pus[18]));
    }

    [TestMethod]
    public void LeastSignificant1Bit_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      var expected = 2;

      Flux.Diagnostics.Performance.Measure(() => Flux.Maths.LeastSignificant1Bit(value), 500000).Assert((System.Numerics.BigInteger)expected, 0.35);
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.LeastSignificant1Bit((int)value), 500000).Assert((int)expected, 0.15);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.LeastSignificant1Bit((long)value), 500000).Assert((long)expected, 0.15);
      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.LeastSignificant1Bit((uint)value), 500000).Assert((uint)expected, 0.15);
      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.LeastSignificant1Bit((ulong)value), 500000).Assert((ulong)expected, 0.15);
    }

    [TestMethod]
    public void Log2()
    {
      Assert.AreEqual(pi[4], Flux.Maths.Log2(pbi[18]));
      Assert.AreEqual(pi[4], Flux.Maths.Log2(pb[18]));
      Assert.AreEqual(pi[4], Flux.Maths.Log2(pi[18]));
      Assert.AreEqual(pi[4], Flux.Maths.Log2(pl[18]));
      Assert.AreEqual(pi[4], Flux.Maths.Log2(psb[18]));
      Assert.AreEqual(pi[4], Flux.Maths.Log2(ps[18]));
      Assert.AreEqual(pi[4], Flux.Maths.Log2(pui[18]));
      Assert.AreEqual(pi[4], Flux.Maths.Log2(pul[18]));
      Assert.AreEqual(pi[4], Flux.Maths.Log2(pus[18]));
    }

    [TestMethod]
    public void Log2_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      var expected = 19;

      Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Log2(value), 500000).Assert(expected, 0.75);
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Log2((int)value), 500000).Assert(expected, 0.175);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Log2((long)value), 500000).Assert(expected, 0.175);
      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Log2((uint)value), 500000).Assert(expected, 0.175);
      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.Log2((ulong)value), 500000).Assert(expected, 0.175);
    }

    [TestMethod]
    public void MostSignificant1Bit()
    {
      Assert.AreEqual(pbi[16], Flux.Maths.MostSignificant1Bit(pbi[18]));
      Assert.AreEqual(pb[16], Flux.Maths.MostSignificant1Bit(pb[18]));
      Assert.AreEqual(pi[16], Flux.Maths.MostSignificant1Bit(pi[18]));
      Assert.AreEqual(pl[16], Flux.Maths.MostSignificant1Bit(pl[18]));
      Assert.AreEqual(psb[16], Flux.Maths.MostSignificant1Bit(psb[18]));
      Assert.AreEqual(ps[16], Flux.Maths.MostSignificant1Bit(ps[18]));
      Assert.AreEqual(pui[16], Flux.Maths.MostSignificant1Bit(pui[18]));
      Assert.AreEqual(pul[16], Flux.Maths.MostSignificant1Bit(pul[18]));
      Assert.AreEqual(pus[16], Flux.Maths.MostSignificant1Bit(pus[18]));
    }

    [TestMethod]
    public void MostSignificant1Bit_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      Flux.Diagnostics.Performance.Measure(() => Flux.Maths.MostSignificant1Bit(value), 500000).Assert((System.Numerics.BigInteger)524288, 0.75);
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.MostSignificant1Bit((int)value), 500000).Assert(524288, 0.175);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.MostSignificant1Bit((long)value), 500000).Assert(524288L, 0.175);
      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.MostSignificant1Bit((uint)value), 500000).Assert(524288U, 0.175);
      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.MostSignificant1Bit((ulong)value), 500000).Assert(524288UL, 0.175);
    }

    [TestMethod]
    public void NextPowerOf2()
    {
      Assert.AreEqual(pbi[8], Flux.Maths.NextPowerOf2(pbi[5], false));
      Assert.AreEqual(pbi[32], Flux.Maths.NextPowerOf2(pbi[17], false));
      Assert.AreEqual(pbi[32], Flux.Maths.NextPowerOf2(pbi[32], false));

      Assert.AreEqual(pi[8], Flux.Maths.NextPowerOf2(pi[5], false));
      Assert.AreEqual(pi[32], Flux.Maths.NextPowerOf2(pi[17], false));
      Assert.AreEqual(pi[32], Flux.Maths.NextPowerOf2(pi[32], false));

      Assert.AreEqual(pi[8], Flux.Maths.NextPowerOf2(pl[5], false));
      Assert.AreEqual(pi[32], Flux.Maths.NextPowerOf2(pl[17], false));
      Assert.AreEqual(pi[32], Flux.Maths.NextPowerOf2(pl[32], false));

      Assert.AreEqual(pbi[8], Flux.Maths.NextPowerOf2(pbi[5], true));
      Assert.AreEqual(pbi[32], Flux.Maths.NextPowerOf2(pbi[17], true));
      Assert.AreEqual(pbi[64], Flux.Maths.NextPowerOf2(pbi[32], true));

      Assert.AreEqual(pi[8], Flux.Maths.NextPowerOf2(pi[5], true));
      Assert.AreEqual(pi[32], Flux.Maths.NextPowerOf2(pi[17], true));
      Assert.AreEqual(pi[64], Flux.Maths.NextPowerOf2(pi[32], true));

      Assert.AreEqual(pl[8], Flux.Maths.NextPowerOf2(pl[5], true));
      Assert.AreEqual(pl[32], Flux.Maths.NextPowerOf2(pl[17], true));
      Assert.AreEqual(pl[64], Flux.Maths.NextPowerOf2(pl[32], true));
    }

    [TestMethod]
    public void Pow2()
    {
      Assert.AreEqual(pi[16], Flux.Maths.Pow(pb[4], 2));
      Assert.AreEqual(pi[16], Flux.Maths.Pow(ps[4], 2));
      Assert.AreEqual(pi[16], Flux.Maths.Pow(pi[4], 2));
      Assert.AreEqual(pl[16], Flux.Maths.Pow(pl[4], 2));
      Assert.AreEqual(pi[16], Flux.Maths.Pow(psb[4], 2));
      Assert.AreEqual(pi[16], Flux.Maths.Pow(pus[4], 2));
      Assert.AreEqual(pui[16], Flux.Maths.Pow(pui[4], 2));
      Assert.AreEqual(pul[16], Flux.Maths.Pow(pul[4], 2));
    }

    [TestMethod]
    public void PowerOf()
    {
      Assert.AreEqual(64, Flux.Maths.PowerOf(101, 2));
      Assert.AreEqual(64, Flux.Maths.PowerOf(101, 8));
      Assert.AreEqual(100, Flux.Maths.PowerOf(101, 10));
      Assert.AreEqual(16, Flux.Maths.PowerOf(101, 16));
    }

    [TestMethod]
    public void PreviousPowerOf2()
    {
      Assert.AreEqual(pbi[8], Flux.Maths.PreviousPowerOf2(pbi[10], false));
      Assert.AreEqual(pbi[16], Flux.Maths.PreviousPowerOf2(pbi[19], false));
      Assert.AreEqual(pbi[32], Flux.Maths.PreviousPowerOf2(pbi[32], false));

      Assert.AreEqual(pi[8], Flux.Maths.PreviousPowerOf2(pi[10], false));
      Assert.AreEqual(pi[16], Flux.Maths.PreviousPowerOf2(pi[19], false));
      Assert.AreEqual(pi[32], Flux.Maths.PreviousPowerOf2(pi[32], false));

      Assert.AreEqual(pl[8], Flux.Maths.PreviousPowerOf2(pl[10], false));
      Assert.AreEqual(pl[16], Flux.Maths.PreviousPowerOf2(pl[19], false));
      Assert.AreEqual(pl[32], Flux.Maths.PreviousPowerOf2(pl[32], false));

      Assert.AreEqual(pbi[8], Flux.Maths.PreviousPowerOf2(pbi[10], true));
      Assert.AreEqual(pbi[16], Flux.Maths.PreviousPowerOf2(pbi[19], true));
      Assert.AreEqual(pbi[16], Flux.Maths.PreviousPowerOf2(pbi[32], true));

      Assert.AreEqual(pi[8], Flux.Maths.PreviousPowerOf2(pi[10], true));
      Assert.AreEqual(pi[16], Flux.Maths.PreviousPowerOf2(pi[19], true));
      Assert.AreEqual(pi[16], Flux.Maths.PreviousPowerOf2(pi[32], true));

      Assert.AreEqual(pl[8], Flux.Maths.PreviousPowerOf2(pl[10], true));
      Assert.AreEqual(pl[16], Flux.Maths.PreviousPowerOf2(pl[19], true));
      Assert.AreEqual(pl[16], Flux.Maths.PreviousPowerOf2(pl[32], true));
    }

    [TestMethod]
    public void ReverseBits()
    {
      Assert.AreEqual(pbi[30], Flux.Maths.ReverseBits(pbi[120]));
      Assert.AreEqual(pbi[1], Flux.Maths.ReverseBits(pbi[128]));

      Assert.AreEqual(pbi[48], Flux.Maths.ReverseBits(pbi[12]));
      Assert.AreEqual(pbi[222], Flux.Maths.ReverseBits(pbi[123]));
      Assert.AreEqual(805306368U, Flux.Maths.ReverseBits(pui[12]));
      Assert.AreEqual(301989888U, Flux.Maths.ReverseBits(pui[72]));
      Assert.AreEqual(3458764513820540928U, Flux.Maths.ReverseBits(pul[12]));
      Assert.AreEqual(1297036692682702848U, Flux.Maths.ReverseBits(pul[72]));
    }
    // 10100011101101000010
    // 01000010110111000101
    [TestMethod]
    public void ReverseBits_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      Flux.Diagnostics.Performance.Measure(() => Flux.Maths.ReverseBits(value), 500000).Assert((System.Numerics.BigInteger)4381776, 5);
      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.ReverseBits((int)value)).Assert(1121734656, 0.175);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.ReverseBits((long)value), 500000).Assert(4817813662309810176L, 0.175);
      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.ReverseBits((uint)value), 500000).Assert(1121734656U, 0.175);
      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Maths.ReverseBits((ulong)value), 500000).Assert(4817813662309810176UL, 0.175);
    }

  }
}
