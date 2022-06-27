using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maths
{
  [TestClass]
  public class Radix
  {
    readonly System.Numerics.BigInteger[] nbi = System.Linq.Enumerable.Range(0, 255).Select(i => -(System.Numerics.BigInteger)i).ToArray();
    readonly short[] ns = System.Linq.Enumerable.Range(0, 255).Select(i => (short)-i).ToArray();
    readonly int[] ni = System.Linq.Enumerable.Range(0, 255).Select(i => (int)-i).ToArray();
    readonly long[] nl = System.Linq.Enumerable.Range(0, 255).Select(i => -(long)i).ToArray();
    readonly sbyte[] nsb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)-i).ToArray(); // Restricted to -127.

    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
    readonly byte[] pb = System.Linq.Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
    readonly short[] ps = System.Linq.Enumerable.Range(0, 255).Select(i => (short)i).ToArray();
    readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
    readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
    readonly sbyte[] psb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)i).ToArray(); // Restricted to 127.
    readonly ushort[] pus = System.Linq.Enumerable.Range(0, 255).Select(i => (ushort)i).ToArray();
    readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

    [TestMethod]
    public void DigitCountBI()
      => Assert.AreEqual(2, Flux.Maths.DigitCount(53.ToBigInteger(), 10));
    [TestMethod]
    public void DigitCountInt32()
      => Assert.AreEqual(2, Flux.Maths.DigitCount(53, 10));
    [TestMethod]
    public void DigitCountInt64()
      => Assert.AreEqual(2, Flux.Maths.DigitCount(53L, 10));
    [TestMethod]
    public void DigitCountUInt32()
      => Assert.AreEqual(2, Flux.Maths.DigitCount(53U, 10));
    [TestMethod]
    public void DigitCountUInt64()
      => Assert.AreEqual(2, Flux.Maths.DigitCount(53UL, 10));

    [TestMethod]
    public void DigitSumBI()
      => Assert.AreEqual(8, Flux.Maths.DigitSum(53.ToBigInteger(), 10));
    [TestMethod]
    public void DigitSumInt32()
      => Assert.AreEqual(8, Flux.Maths.DigitSum(53, 10));
    [TestMethod]
    public void DigitSumInt64()
      => Assert.AreEqual(8, Flux.Maths.DigitSum(53L, 10));
    [TestMethod]
    public void DigitSumUInt32()
      => Assert.AreEqual(8, Flux.Maths.DigitSum(53U, 10));
    [TestMethod]
    public void DigitSumUInt64()
      => Assert.AreEqual(8, Flux.Maths.DigitSum(53UL, 10));

    [TestMethod]
    public void DigitCount_BigInteger()
    {
      for (var i = 1.ToBigInteger(); i < ulong.MaxValue; i += Flux.Randomization.NumberGenerator.Crypto.NextBigInteger(i) + 1)
      {
        var expectedString = i.ToString();
        var actualCount = Flux.Maths.DigitCount(i, 10);
        Assert.AreEqual(expectedString.Length, actualCount, $"{i} = {expectedString} ({expectedString.Length}) = {actualCount}");
      }
    }
    [TestMethod]
    public void DigitCount_Int32()
    {
      for (var i = 2U; i < short.MaxValue; i += (uint)Flux.Randomization.NumberGenerator.Crypto.Next((int)i) + 1)
      {
        var expectedString = i.ToString();
        var actualCount = Flux.Maths.DigitCount((int)i, 10);
        Assert.AreEqual(expectedString.Length, actualCount, $"{i} = {expectedString} ({expectedString.Length}) = {actualCount}");
      }
    }
    [TestMethod]
    public void DigitCount_Int64()
    {
      for (var i = 2UL; i < long.MaxValue; i += (ulong)Flux.Randomization.NumberGenerator.Crypto.NextInt64((long)i) + 1)
      {
        var expectedString = i.ToString();
        var actualCount = Flux.Maths.DigitCount((long)i, 10);
        Assert.AreEqual(expectedString.Length, actualCount, $"{i} = {expectedString} ({expectedString.Length}) = {actualCount}");
      }
    }

    [TestMethod]
    public void DigitSum_BigInteger()
    {
      for (var i = 1.ToBigInteger(); i < ulong.MaxValue; i += Flux.Randomization.NumberGenerator.Crypto.NextBigInteger(i) + 1)
      {
        var span = Flux.Maths.GetDigitsReversed(i, 10);
        var expectedSum = System.Numerics.BigInteger.Zero;
        for (var index = span.Length - 1; index >= 0; index--)
          expectedSum += span[index];
        var actualSum = Flux.Maths.DigitSum(i, 10);
        Assert.AreEqual(expectedSum, actualSum, $"{i} = {expectedSum} ({expectedSum}) = {actualSum}");
      }
    }
    [TestMethod]
    public void DigitSum_Int32()
    {
      for (var i = 2; i > 0 && i < int.MaxValue; i += (int)Flux.Randomization.NumberGenerator.Crypto.Next((int)i) + 1)
      {
        var span = Flux.Maths.GetDigitsReversed(i, 10);
        var expectedSum = 0;
        for (var index = span.Length - 1; index >= 0; index--)
          expectedSum += span[index];
        var actualSum = Flux.Maths.DigitSum(i, 10);
        Assert.AreEqual(expectedSum, actualSum, $"{i} = {expectedSum} ({expectedSum}) = {actualSum}");
      }
    }
    [TestMethod]
    public void DigitSum_Int64()
    {
      for (var i = 2L; i > 0 && i < long.MaxValue; i += (long)Flux.Randomization.NumberGenerator.Crypto.NextInt64((long)i) + 1)
      {
        var span = Flux.Maths.GetDigitsReversed(i, 10);
        var expectedSum = 0L;
        for (var index = span.Length - 1; index >= 0; index--)
          expectedSum += span[index];
        var actualSum = (long)Flux.Maths.DigitSum(i, 10);
        Assert.AreEqual(expectedSum, actualSum, $"{i} = {expectedSum} ({expectedSum}) = {actualSum}");
      }
    }

    [TestMethod]
    public void DropLeadingDigit()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      Assert.AreEqual(146242, Flux.Maths.DropLeadingDigit(value, 2)); // 10100011101101000010 = 0100011101101000010
      Assert.AreEqual(146242, Flux.Maths.DropLeadingDigit(value, 8)); // 2435502 = 435502
      Assert.AreEqual(70530, Flux.Maths.DropLeadingDigit(value, 10)); // 670530 = 70530
      Assert.AreEqual(15170, Flux.Maths.DropLeadingDigit(value, 16)); // A3B42 = 3B42
    }

    [TestMethod]
    public void DropTrailingDigit()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      Assert.AreEqual(335265, Flux.Maths.DropTrailingDigit(value, 2)); // bin 10100011101101000010 = bin 1010001110110100001 = dec 335265
      Assert.AreEqual(83816, Flux.Maths.DropTrailingDigit(value, 8)); // oct 2435502 = oct 243550 = dec 83816
      Assert.AreEqual(67053, Flux.Maths.DropTrailingDigit(value, 10)); // dec 670530 = dec 67053
      Assert.AreEqual(41908, Flux.Maths.DropTrailingDigit(value, 16)); // hex A3B42 = hex A3B4 = dec 41908
    }

    [TestMethod]
    public void GetComponents()
    {
      var x = Flux.Maths.GetPlaceValues(670530, 2).ToArray();
      CollectionAssert.AreEqual(new int[] { 0, 2, 0, 0, 0, 0, 64, 0, 256, 512, 0, 2048, 4096, 8192, 0, 0, 0, 131072, 0, 524288 }, Flux.Maths.GetPlaceValues(670530, 2).ToArray(), nameof(Flux.Maths.GetPlaceValues) + ".Radix=2");
      CollectionAssert.AreEqual(new int[] { 0, 30, 500, 0, 70000, 600000 }, Flux.Maths.GetPlaceValues(670530, 10).ToArray(), nameof(Flux.Maths.GetPlaceValues) + ".Radix=10");
      CollectionAssert.AreEqual(new int[] { 2, 64, 2816, 12288, 655360 }, Flux.Maths.GetPlaceValues(670530, 16).ToArray(), nameof(Flux.Maths.GetPlaceValues) + ".Radix=16");
    }

    [TestMethod]
    public void GetDigits()
    {
      CollectionAssert.AreEqual(new int[] { 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0 }, Flux.Maths.GetDigits(670530, 2).ToArray(), nameof(Flux.Maths.GetDigits) + ".Radix=2");
      CollectionAssert.AreEqual(new int[] { 6, 7, 0, 5, 3, 0 }, Flux.Maths.GetDigits(670530, 10).ToArray(), nameof(Flux.Maths.GetDigits) + ".Radix=10");
      CollectionAssert.AreEqual(new int[] { 10, 3, 11, 4, 2 }, Flux.Maths.GetDigits(670530, 16).ToArray(), nameof(Flux.Maths.GetDigits) + ".Radix=16");
    }

    [TestMethod]
    public void GetDigitsReversed()
    {
      CollectionAssert.AreEqual(new int[] { 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 }, Flux.Maths.GetDigitsReversed(670530, 2).ToArray(), nameof(Flux.Maths.GetDigitsReversed) + ".Radix=2");
      CollectionAssert.AreEqual(new int[] { 0, 3, 5, 0, 7, 6 }, Flux.Maths.GetDigitsReversed(670530, 10).ToArray(), nameof(Flux.Maths.GetDigitsReversed) + ".Radix=10");
      CollectionAssert.AreEqual(new int[] { 2, 4, 11, 3, 10 }, Flux.Maths.GetDigitsReversed(670530, 16).ToArray(), nameof(Flux.Maths.GetDigitsReversed) + ".Radix=16");
    }

    [TestMethod]
    public void IsJumbled()
    {
      Assert.AreEqual(true, Flux.Maths.IsJumbled(0b01001101, 2), nameof(Flux.Maths.IsJumbled) + ".Radix=2");
      Assert.AreEqual(true, Flux.Maths.IsJumbled(1223, 10), nameof(Flux.Maths.IsJumbled) + ".Radix=10");
      Assert.AreEqual(false, Flux.Maths.IsJumbled(0x123F, 16), nameof(Flux.Maths.IsJumbled) + ".Radix=16");
    }

    [TestMethod]
    public void IsPowerOf()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      Assert.AreEqual(false, Flux.Maths.IsPowerOf(value, 2));
      Assert.AreEqual(false, Flux.Maths.IsPowerOf(value, 8));
      Assert.AreEqual(false, Flux.Maths.IsPowerOf(value, 10));
      Assert.AreEqual(false, Flux.Maths.IsPowerOf(value, 16));

      Assert.AreEqual(true, Flux.Maths.IsPowerOf(4, 2));
      Assert.AreEqual(true, Flux.Maths.IsPowerOf(64, 8));
      Assert.AreEqual(true, Flux.Maths.IsPowerOf(100, 10));
      Assert.AreEqual(true, Flux.Maths.IsPowerOf(256, 16));
    }

    [TestMethod]
    public void Radix_PowerOf()
    {
      Assert.AreEqual(16, Flux.Maths.PowerOf(30, 2));
      Assert.AreEqual(8, Flux.Maths.PowerOf(30, 8));
      Assert.AreEqual(10, Flux.Maths.PowerOf(30, 10));
      Assert.AreEqual(16, Flux.Maths.PowerOf(30, 16));
    }

    [TestMethod]
    public void Radix_ReverseDigits()
    {
      Assert.AreEqual(pbi[21], Flux.Maths.ReverseDigits(pbi[12], 10));
      Assert.AreEqual(pbi[27], Flux.Maths.ReverseDigits(pbi[72], 10));
      Assert.AreEqual(pb[21], Flux.Maths.ReverseDigits(pb[12], 10));
      Assert.AreEqual(pb[27], Flux.Maths.ReverseDigits(pb[72], 10));
      Assert.AreEqual(pus[21], Flux.Maths.ReverseDigits(pus[12], 10));
      Assert.AreEqual(pus[27], Flux.Maths.ReverseDigits(pus[72], 10));
      Assert.AreEqual(pui[21], Flux.Maths.ReverseDigits(pui[12], pi[10]));
      Assert.AreEqual(pui[27], Flux.Maths.ReverseDigits(pui[72], pi[10]));
      Assert.AreEqual(pul[21], Flux.Maths.ReverseDigits(pul[12], pi[10]));
      Assert.AreEqual(pul[27], Flux.Maths.ReverseDigits(pul[72], pi[10]));
    }

    [TestMethod]
    public void Radix_ReverseDigits_Speed()
    {
      var value = System.Numerics.BigInteger.Parse("670530");

      if (value >= int.MinValue && value <= int.MaxValue) Flux.Services.Performance.Measure(() => Flux.Maths.ReverseDigits((int)value, 10), 200000).Assert(35076, 0.25);
      if (value >= long.MinValue && value <= long.MaxValue) Flux.Services.Performance.Measure(() => Flux.Maths.ReverseDigits((long)value, 10), 200000).Assert(35076L, 0.25);
      Flux.Services.Performance.Measure(() => Flux.Maths.ReverseDigits(value, 10), 200000).Assert((System.Numerics.BigInteger)35076, 1.2);
    }

    [TestMethod]
    public void SelfNumber_BigInteger()
    {
      var expected = new System.Numerics.BigInteger[] { 1, 3, 5, 7, 9, 20, 31, 42, 53, 64, 75, 86, 97, 108, 110, 121, 132, 143, 154, 165, 176, 187, 198, 209, 211, 222, 233, 244, 255, 266, 277, 288, 299, 310, 312, 323, 334, 345, 356, 367, 378, 389, 400, 411, 413, 424, 435, 446, 457, 468, 479, 490 };
      var actual = System.Linq.Enumerable.Range(1, 500).Where(i => Flux.Maths.IsSelfNumber(i, 10)).Select(i => i.ToBigInteger()).ToArray();
      CollectionAssert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void SelfNumber_Int32()
    {
      var expected = new int[] { 1, 3, 5, 7, 9, 20, 31, 42, 53, 64, 75, 86, 97, 108, 110, 121, 132, 143, 154, 165, 176, 187, 198, 209, 211, 222, 233, 244, 255, 266, 277, 288, 299, 310, 312, 323, 334, 345, 356, 367, 378, 389, 400, 411, 413, 424, 435, 446, 457, 468, 479, 490 };
      var actual = System.Linq.Enumerable.Range(1, 500).Where(i => Flux.Maths.IsSelfNumber(i, 10)).ToArray();
      CollectionAssert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void SelfNumber_Int64()
    {
      var expected = new long[] { 1, 3, 5, 7, 9, 20, 31, 42, 53, 64, 75, 86, 97, 108, 110, 121, 132, 143, 154, 165, 176, 187, 198, 209, 211, 222, 233, 244, 255, 266, 277, 288, 299, 310, 312, 323, 334, 345, 356, 367, 378, 389, 400, 411, 413, 424, 435, 446, 457, 468, 479, 490 };
      var actual = System.Linq.Enumerable.Range(1, 500).Where(i => Flux.Maths.IsSelfNumber(i, 10)).Select(i => (long)i).ToArray();
      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
