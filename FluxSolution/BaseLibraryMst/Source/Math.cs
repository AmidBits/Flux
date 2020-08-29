//using System;
//using System.Linq;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Static
//{
//  [TestClass]
//  public class Math
//  {
//    readonly System.Numerics.BigInteger[] nbi = System.Linq.Enumerable.Range(0, 255).Select(i => -(System.Numerics.BigInteger)i).ToArray();
//    readonly short[] ns = System.Linq.Enumerable.Range(0, 127).Select(i => (short)-i).ToArray();
//    readonly int[] ni = System.Linq.Enumerable.Range(0, 127).Select(i => (int)-i).ToArray();
//    readonly long[] nl = System.Linq.Enumerable.Range(0, 127).Select(i => -(long)i).ToArray();
//    readonly sbyte[] nsb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)-i).ToArray();

//    readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
//    readonly byte[] pb = System.Linq.Enumerable.Range(0, 127).Select(i => (byte)i).ToArray();
//    readonly short[] ps = System.Linq.Enumerable.Range(0, 127).Select(i => (short)i).ToArray();
//    readonly int[] pi = System.Linq.Enumerable.Range(0, 127).Select(i => (int)i).ToArray();
//    readonly long[] pl = System.Linq.Enumerable.Range(0, 127).Select(i => (long)i).ToArray();
//    readonly sbyte[] psb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)i).ToArray();
//    readonly ushort[] pus = System.Linq.Enumerable.Range(0, 127).Select(i => (ushort)i).ToArray();
//    readonly uint[] pui = System.Linq.Enumerable.Range(0, 127).Select(i => (uint)i).ToArray();
//    readonly ulong[] pul = System.Linq.Enumerable.Range(0, 127).Select(i => (ulong)i).ToArray();

//    //[TestMethod]
//    //public void Abs()
//    //{
//    //  Assert.AreEqual(pi[1], Flux.Math.Abs(nsb[1]));
//    //  Assert.AreEqual(pi[1], Flux.Math.Abs(ns[1]));
//    //  Assert.AreEqual(pi[1], Flux.Math.Abs(ni[1]));
//    //  Assert.AreEqual(pl[1], Flux.Math.Abs(nl[1]));
//    //}

//    //[TestMethod]
//    //public void Abs_Speed()
//    //{
//    //  var value = System.Numerics.BigInteger.Parse("-670530");

//    //  Flux.Diagnostics.Performance.Measure(() => Flux.Math.Abs(value)).Assert((System.Numerics.BigInteger)670530, 0.5);
//    //  if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Abs((int)value)).Assert(670530, 0.5);
//    //  if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Abs((long)value)).Assert(670530L, 0.5);

//    //  //      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => null).Assert(value, 0.175);
//    //}
//    [TestMethod]
//    public void Bit1Count()
//    {
//      Assert.AreEqual(pi[2], Flux.Math.Bit1Count(pbi[18]));
//      Assert.AreEqual(pi[2], Flux.Math.Bit1Count(pb[18]));
//      Assert.AreEqual(pi[2], Flux.Math.Bit1Count(ps[18]));
//      Assert.AreEqual(pi[2], Flux.Math.Bit1Count(pi[18]));
//      Assert.AreEqual(pi[2], Flux.Math.Bit1Count(pl[18]));
//      Assert.AreEqual(pi[2], Flux.Math.Bit1Count(psb[18]));
//      Assert.AreEqual(pi[2], Flux.Math.Bit1Count(pus[18]));
//      Assert.AreEqual(pi[2], Flux.Math.Bit1Count(pui[18]));
//      Assert.AreEqual(pi[2], Flux.Math.Bit1Count(pul[18]));
//    }

//    [TestMethod]
//    public void Bit1Count_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      var expected = Flux.PositionalNotation.NumberToText(value, Flux.PositionalNotation.Base62.Take(2).ToArray()).Count(c => c == '1');

//      Flux.Diagnostics.Performance.Measure(() => Flux.Math.Bit1Count(value)).Assert(expected, 9);
//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Bit1Count((int)value)).Assert(expected, 0.40);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Bit1Count((long)value)).Assert(expected, 0.40);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Bit1Count((uint)value)).Assert(expected, 0.40);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Bit1Count((ulong)value)).Assert(expected, 0.4);
//    }

//    [TestMethod]
//    public void BitIndex()
//    {
//      Assert.AreEqual(ni[1], Flux.Math.BitIndex(pbi[18]));
//      Assert.AreEqual(ni[1], Flux.Math.BitIndex(pi[18]));
//      Assert.AreEqual(nl[1], Flux.Math.BitIndex(pl[18]));
//      Assert.AreEqual(pi[5], Flux.Math.BitIndex(pbi[32]));
//      Assert.AreEqual(pi[5], Flux.Math.BitIndex(pi[32]));
//      Assert.AreEqual(pl[5], Flux.Math.BitIndex(pl[32]));
//    }

//    [TestMethod]
//    public void BitLength()
//    {
//      Assert.AreEqual(pi[5], Flux.Math.BitLength(pbi[18]));
//      Assert.AreEqual(pi[5], Flux.Math.BitLength(pb[18]));
//      Assert.AreEqual(pi[5], Flux.Math.BitLength(ps[18]));
//      Assert.AreEqual(pi[5], Flux.Math.BitLength(pi[18]));
//      Assert.AreEqual(pl[5], Flux.Math.BitLength(pl[18]));
//      Assert.AreEqual(pi[5], Flux.Math.BitLength(psb[18]));
//      Assert.AreEqual(pi[5], Flux.Math.BitLength(pus[18]));
//      Assert.AreEqual(pi[5], Flux.Math.BitLength(pui[18]));
//      Assert.AreEqual(pi[5], Flux.Math.BitLength(pul[18]));
//    }

//    [TestMethod]
//    public void BitLength_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      Flux.Diagnostics.Performance.Measure(() => Flux.Math.BitLength(value)).Assert(20, 4);
//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.BitLength((int)value)).Assert(20, 0.5);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.BitLength((long)value)).Assert(20, 0.5);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.BitLength((uint)value)).Assert(20, 0.5);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.BitLength((ulong)value)).Assert(20, 0.5);
//    }

//    [TestMethod]
//    public void Clamp()
//    {
//      Assert.AreEqual(pbi[9], Flux.Math.Clamp(pbi[19], pbi[3], pbi[9]));
//      Assert.AreEqual(pb[9], Flux.Math.Clamp(pb[19], pb[3], pb[9]));
//      Assert.AreEqual(pi[9], Flux.Math.Clamp(pi[19], pi[3], pi[9]));
//      Assert.AreEqual(pl[9], Flux.Math.Clamp(pl[19], pl[3], pl[9]));
//      Assert.AreEqual(psb[9], Flux.Math.Clamp(psb[19], psb[3], psb[9]));
//      Assert.AreEqual(ps[9], Flux.Math.Clamp(ps[19], ps[3], ps[9]));
//      Assert.AreEqual(pui[9], Flux.Math.Clamp(pui[19], pui[3], pui[9]));
//      Assert.AreEqual(pul[9], Flux.Math.Clamp(pul[19], pul[3], pul[9]));
//      Assert.AreEqual(pus[9], Flux.Math.Clamp(pus[19], pus[3], pus[9]));
//    }

//    [TestMethod]
//    public void CountLeadingZeros()
//    {
//      //Assert.AreEqual(pbi[3], Flux.Math.CountLeadingZeros(pbi[18], 8));
//      Assert.AreEqual(pi[27], Flux.Math.CountLeadingZeros(pui[18]));
//      Assert.AreEqual(pi[59], Flux.Math.CountLeadingZeros(pul[18]));
//    }

//    [TestMethod]
//    public void CountLeadingZeros_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      //Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountLeadingZeros(value, 128)).Assert(108, 0.75);
//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountLeadingZeros((int)value)).Assert(12, 0.75);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountLeadingZeros((long)value)).Assert(44, 0.75);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountLeadingZeros((uint)value)).Assert(12, 0.75);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountLeadingZeros((ulong)value)).Assert(44, 0.75);
//    }

//    [TestMethod]
//    public void CountTrailingZeros()
//    {
//      Assert.AreEqual(pi[1], Flux.Math.CountTrailingZeros(pbi[18]));
//      Assert.AreEqual(pi[1], Flux.Math.CountTrailingZeros(pb[18]));
//      Assert.AreEqual(pi[1], Flux.Math.CountTrailingZeros(pus[18]));
//      Assert.AreEqual(pi[1], Flux.Math.CountTrailingZeros(pui[18]));
//      Assert.AreEqual(pi[1], Flux.Math.CountTrailingZeros(pul[18]));
//    }

//    [TestMethod]
//    public void CountTrailingZeros_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      var expected = 1;

//      Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountTrailingZeros(value)).Assert(expected, 5);
//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountTrailingZeros((int)value)).Assert(expected, 0.75);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountTrailingZeros((long)value)).Assert(expected, 0.75);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountTrailingZeros((uint)value)).Assert(expected, 0.75);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.CountTrailingZeros((ulong)value)).Assert(expected, 0.75);
//    }

//    [TestMethod]
//    public void DigitCount()
//    {
//      Assert.AreEqual(2, Flux.Math.DigitCount(pbi[53], pi[10]));
//      Assert.AreEqual(1, Flux.Math.DigitCount(pbi[5], pi[10]));
//    }

//    [TestMethod]
//    public void DigitSum()
//    {
//      Assert.AreEqual(8, Flux.Math.DigitSum(pbi[53], pi[10]));
//      Assert.AreEqual(13, Flux.Math.DigitSum(pbi[67], pi[10]));
//    }

//    [TestMethod]
//    public void DropLeadingDigit()
//    {
//      Assert.AreEqual(3, Flux.Math.DropLeadingDigit(pbi[53], pi[10]));
//      Assert.AreEqual(7, Flux.Math.DropLeadingDigit(pbi[67], pi[10]));
//    }

//    [TestMethod]
//    public void DropTrailingDigit()
//    {
//      Assert.AreEqual(5, Flux.Math.DropTrailingDigit(pbi[53], pi[10]));
//      Assert.AreEqual(6, Flux.Math.DropTrailingDigit(pbi[67], pi[10]));
//    }

//    [TestMethod]
//    public void Factorial()
//    {
//      Assert.AreEqual(120, Flux.Math.Factorial(pbi[5]));
//      Assert.AreEqual(362880, Flux.Math.Factorial(pbi[9]));
//    }

//    [TestMethod]
//    public void Fold()
//    {
//      Assert.AreEqual(13, Flux.Math.Fold(pbi[53], pbi[10], pbi[20]));
//      Assert.AreEqual(31, Flux.Math.Fold(pbi[5], pbi[30], pbi[36]));

//      Assert.AreEqual(13, Flux.Math.Fold(pi[53], pi[10], pi[20]));
//      Assert.AreEqual(31, Flux.Math.Fold(pi[5], pi[30], pi[36]));

//      Assert.AreEqual(pl[13], Flux.Math.Fold(pl[53], pl[10], pl[20]));
//      Assert.AreEqual(pl[31], Flux.Math.Fold(pl[5], pl[30], pl[36]));

//      Assert.AreEqual(1.3, Flux.Math.Fold(5.3, 1.0, 2.0), Flux.Math.EpsilonCpp32);
//      Assert.AreEqual(3.1, Flux.Math.Fold(0.5, 3.0, 3.6), Flux.Math.EpsilonCpp32);
//    }

//    [TestMethod]
//    public void FoldBits()
//    {
//      Assert.AreEqual(pbi[31], Flux.Math.FoldBits(pbi[18]));
//      Assert.AreEqual(pb[31], Flux.Math.FoldBits(pb[18]));
//      Assert.AreEqual(ps[31], Flux.Math.FoldBits(ps[18]));
//      Assert.AreEqual(pi[31], Flux.Math.FoldBits(pi[18]));
//      Assert.AreEqual(pl[31], Flux.Math.FoldBits(pl[18]));
//      Assert.AreEqual(psb[31], Flux.Math.FoldBits(psb[18]));
//      Assert.AreEqual(pus[31], Flux.Math.FoldBits(pus[18]));
//      Assert.AreEqual(pui[31], Flux.Math.FoldBits(pui[18]));
//      Assert.AreEqual(pul[31], Flux.Math.FoldBits(pul[18]));
//    }

//    [TestMethod]
//    public void FoldBits_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      var expected = 1048575;

//      Flux.Diagnostics.Performance.Measure(() => Flux.Math.FoldBits(value)).Assert((System.Numerics.BigInteger)expected, 4.25);
//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.FoldBits((int)value)).Assert((int)expected, 0.5);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.FoldBits((long)value)).Assert((long)expected, 0.5);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.FoldBits((uint)value)).Assert((uint)expected, 0.5);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.FoldBits((ulong)value)).Assert((ulong)expected, 0.5);
//    }

//    [TestMethod]
//    public void GetAscendingPrimes()
//    {
//      var expected = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997, 1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069, 1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163, 1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373, 1381, 1399, 1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511, 1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597, 1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657, 1663, 1667, 1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759, 1777, 1783, 1787, 1789, 1801, 1811, 1823, 1831, 1847, 1861, 1867, 1871, 1873, 1877, 1879, 1889, 1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987, 1993, 1997, 1999, 2003, 2011, 2017, 2027, 2029, 2039, 2053, 2063, 2069, 2081, 2083, 2087, 2089, 2099, 2111, 2113, 2129, 2131, 2137, 2141, 2143, 2153, 2161, 2179, 2203, 2207, 2213, 2221, 2237, 2239, 2243, 2251, 2267, 2269, 2273, 2281, 2287, 2293, 2297, 2309, 2311, 2333, 2339, 2341, 2347, 2351, 2357, 2371, 2377, 2381, 2383, 2389, 2393, 2399, 2411, 2417, 2423, 2437, 2441, 2447, 2459, 2467, 2473, 2477, 2503, 2521, 2531, 2539, 2543, 2549, 2551, 2557, 2579, 2591, 2593, 2609, 2617, 2621, 2633, 2647, 2657, 2659, 2663, 2671, 2677, 2683, 2687, 2689, 2693, 2699, 2707, 2711, 2713, 2719, 2729, 2731, 2741, 2749, 2753, 2767, 2777, 2789, 2791, 2797, 2801, 2803, 2819, 2833, 2837, 2843, 2851, 2857, 2861, 2879, 2887, 2897, 2903, 2909, 2917, 2927, 2939, 2953, 2957, 2963, 2969, 2971, 2999, 3001, 3011, 3019, 3023, 3037, 3041, 3049, 3061, 3067, 3079, 3083, 3089, 3109, 3119, 3121, 3137, 3163, 3167, 3169, 3181, 3187, 3191, 3203, 3209, 3217, 3221, 3229, 3251, 3253, 3257, 3259, 3271, 3299, 3301, 3307, 3313, 3319, 3323, 3329, 3331, 3343, 3347, 3359, 3361, 3371, 3373, 3389, 3391, 3407, 3413, 3433, 3449, 3457, 3461, 3463, 3467, 3469, 3491, 3499, 3511, 3517, 3527, 3529, 3533, 3539, 3541, 3547, 3557, 3559, 3571, 3581, 3583, 3593, 3607, 3613, 3617, 3623, 3631, 3637, 3643, 3659, 3671, 3673, 3677, 3691, 3697, 3701, 3709, 3719, 3727, 3733, 3739, 3761, 3767, 3769, 3779, 3793, 3797, 3803, 3821, 3823, 3833, 3847, 3851, 3853, 3863, 3877, 3881, 3889, 3907, 3911, 3917, 3919, 3923, 3929, 3931, 3943, 3947, 3967, 3989, 4001, 4003, 4007, 4013, 4019, 4021, 4027, 4049, 4051, 4057, 4073, 4079, 4091, 4093, 4099, 4111, 4127, 4129, 4133, 4139, 4153, 4157, 4159, 4177, 4201, 4211, 4217, 4219, 4229, 4231, 4241, 4243, 4253, 4259, 4261, 4271, 4273, 4283, 4289, 4297, 4327, 4337, 4339, 4349, 4357, 4363, 4373, 4391, 4397, 4409, 4421, 4423, 4441, 4447, 4451, 4457, 4463, 4481, 4483, 4493, 4507, 4513, 4517, 4519, 4523, 4547, 4549, 4561, 4567, 4583, 4591, 4597, 4603, 4621, 4637, 4639, 4643, 4649, 4651, 4657, 4663, 4673, 4679, 4691, 4703, 4721, 4723, 4729, 4733, 4751, 4759, 4783, 4787, 4789, 4793, 4799, 4801, 4813, 4817, 4831, 4861, 4871, 4877, 4889, 4903, 4909, 4919, 4931, 4933, 4937, 4943, 4951, 4957, 4967, 4969, 4973, 4987, 4993, 4999, 5003, 5009, 5011, 5021, 5023, 5039, 5051, 5059, 5077, 5081, 5087, 5099, 5101, 5107, 5113, 5119, 5147, 5153, 5167, 5171, 5179, 5189, 5197, 5209, 5227, 5231, 5233, 5237, 5261, 5273, 5279, 5281, 5297, 5303, 5309, 5323, 5333, 5347, 5351, 5381, 5387, 5393, 5399, 5407, 5413, 5417, 5419, 5431, 5437, 5441, 5443, 5449, 5471, 5477, 5479, 5483, 5501, 5503, 5507, 5519, 5521, 5527, 5531, 5557, 5563, 5569, 5573, 5581, 5591, 5623, 5639, 5641, 5647, 5651, 5653, 5657, 5659, 5669, 5683, 5689, 5693, 5701, 5711, 5717, 5737, 5741, 5743, 5749, 5779, 5783, 5791, 5801, 5807, 5813, 5821, 5827, 5839, 5843, 5849, 5851, 5857, 5861, 5867, 5869, 5879, 5881, 5897, 5903, 5923, 5927, 5939, 5953, 5981, 5987, 6007, 6011, 6029, 6037, 6043, 6047, 6053, 6067, 6073, 6079, 6089, 6091, 6101, 6113, 6121, 6131, 6133, 6143, 6151, 6163, 6173, 6197, 6199, 6203, 6211, 6217, 6221, 6229, 6247, 6257, 6263, 6269, 6271, 6277, 6287, 6299, 6301, 6311, 6317, 6323, 6329, 6337, 6343, 6353, 6359, 6361, 6367, 6373, 6379, 6389, 6397, 6421, 6427, 6449, 6451, 6469, 6473, 6481, 6491, 6521, 6529, 6547, 6551, 6553, 6563, 6569, 6571, 6577, 6581, 6599, 6607, 6619, 6637, 6653, 6659, 6661, 6673, 6679, 6689, 6691, 6701, 6703, 6709, 6719, 6733, 6737, 6761, 6763, 6779, 6781, 6791, 6793, 6803, 6823, 6827, 6829, 6833, 6841, 6857, 6863, 6869, 6871, 6883, 6899, 6907, 6911, 6917, 6947, 6949, 6959, 6961, 6967, 6971, 6977, 6983, 6991, 6997, 7001, 7013, 7019, 7027, 7039, 7043, 7057, 7069, 7079, 7103, 7109, 7121, 7127, 7129, 7151, 7159, 7177, 7187, 7193, 7207, 7211, 7213, 7219, 7229, 7237, 7243, 7247, 7253, 7283, 7297, 7307, 7309, 7321, 7331, 7333, 7349, 7351, 7369, 7393, 7411, 7417, 7433, 7451, 7457, 7459, 7477, 7481, 7487, 7489, 7499, 7507, 7517, 7523, 7529, 7537, 7541, 7547, 7549, 7559, 7561, 7573, 7577, 7583, 7589, 7591, 7603, 7607, 7621, 7639, 7643, 7649, 7669, 7673, 7681, 7687, 7691, 7699, 7703, 7717, 7723, 7727, 7741, 7753, 7757, 7759, 7789, 7793, 7817, 7823, 7829, 7841, 7853, 7867, 7873, 7877, 7879, 7883, 7901, 7907, 7919 };
//      var actual = Flux.Math.GetAscendingPrimes(2).Take(expected.Length).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetBellTriangle()
//    {
//      var expected = new System.Numerics.BigInteger[][] { new System.Numerics.BigInteger[] { 1 }, new System.Numerics.BigInteger[] { 1, 2 }, new System.Numerics.BigInteger[] { 2, 3, 5 }, new System.Numerics.BigInteger[] { 5, 7, 10, 15 }, new System.Numerics.BigInteger[] { 15, 20, 27, 37, 52 }, new System.Numerics.BigInteger[] { 52, 67, 87, 114, 151, 203 }, new System.Numerics.BigInteger[] { 203, 255, 322, 409, 523, 674, 877 } };
//      var actual = Flux.Math.GetBellTriangle().Take(expected.Length).ToArray();

//      for (var index = 0; index < expected.Length; index++)
//        Assert.IsTrue(actual[index].SequenceEqual(expected[index]));
//    }

//    [TestMethod]
//    public void GetCatalanSequence()
//    {
//      var expected = new long[] { 1, 1, 2, 5, 14, 42, 132, 429, 1430, 4862, 16796, 58786, 208012, 742900, 2674440, 9694845, 35357670, 129644790, 477638700, 1767263190, 6564120420, 24466267020, 91482563640, 343059613650, 1289904147324, 4861946401452, 18367353072152, 69533550916004, 263747951750360, 1002242216651368, 3814986502092304 };
//      var actual = Flux.Math.GetCatalanSequence().Take(expected.Length).Select(bi => (long)bi).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetFactors()
//    {
//      var expected = new int[] { 1, 60, 2, 30, 3, 20, 4, 15, 5, 12, 6, 10 };
//      var actual = Flux.Math.GetFactors(60).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetFibonacciSequence()
//    {
//      var expected = new long[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711, 28657, 46368, 75025, 121393, 196418, 317811, 514229, 832040, 1346269, 2178309, 3524578, 5702887, 9227465, 14930352, 24157817, 39088169, 63245986, 102334155, 165580141, 267914296, 433494437, 701408733, 1134903170, 1836311903, 2971215073, 4807526976, 7778742049, 12586269025, 20365011074, 32951280099, 53316291173, 86267571272, 139583862445, 225851433717, 365435296162, 591286729879, 956722026041, 1548008755920, 2504730781961, 4052739537881, 6557470319842, 10610209857723, 17167680177565, 27777890035288, 44945570212853, 72723460248141, 117669030460994, 190392490709135, 308061521170129, 498454011879264, 806515533049393, 1304969544928657, 2111485077978050, 3416454622906707, 5527939700884757, 8944394323791464, 14472334024676221, 23416728348467685, 37889062373143906, 61305790721611591, 99194853094755497, 160500643816367088, 259695496911122585, 420196140727489673, 679891637638612258, 1100087778366101931, 1779979416004714189, 2880067194370816120, 4660046610375530309, 7540113804746346429 };
//      var actual = Flux.Math.GetFibonacciSequence().Take(expected.Length).Select(i => (long)i).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetHighlyAbundantNumbers()
//    {
//      var expected = new System.Numerics.BigInteger[] { 1, 2, 3, 4, 6, 8, 10, 12, 16, 18, 20, 24, 30, 36, 42, 48, 60, 72, 84, 90, 96, 108, 120, 144, 168, 180, 210, 216, 240, 288, 300, 336, 360, 420, 480, 504, 540, 600, 630, 660, 720, 840, 960, 1008, 1080, 1200, 1260, 1440, 1560, 1620, 1680, 1800, 1920, 1980, 2100 };
//      var actual = Flux.Math.GetHighlyAbundantNumbers().Take(expected.Length).Select(kvp => kvp.Key).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetHighlyCompositeNumbers()
//    {
//      var expected = new System.Numerics.BigInteger[] { 1, 2, 4, 6, 12, 24, 36, 48, 60, 120, 180, 240, 360, 720, 840, 1260, 1680, 2520, 5040, 7560, 10080, 15120, 20160, 25200, 27720, 45360, 50400, 55440, 83160 };
//      var actual = Flux.Math.GetHighlyCompositeNumbers().Take(expected.Length).Select(kvp => kvp.Key).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetMersennePrimes()
//    {
//      var expected = new int[] { 3, 7, 31, 127, 8191, 131071, 524287, 2147483647 };
//      var actual = Flux.Math.GetMersennePrimes().Take(expected.Length).Select(bi => (int)bi).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetPadovanSequence()
//    {
//      var expected = new int[] { 1, 1, 1, 2, 2, 3, 4, 5, 7, 9, 12, 16, 21, 28, 37, 49, 65, 86, 114, 151, 200, 265, 351, 465, 616, 816, 1081, 1432, 1897, 2513, 3329, 4410, 5842, 7739, 10252, 13581, 17991, 23833, 31572, 41824, 55405, 73396, 97229, 128801, 170625 };
//      var actual = Flux.Math.GetPadovanSequence().Take(expected.Length).Select(bi => (int)bi).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetPerrinSequence()
//    {
//      var expected = new int[] { 3, 0, 2, 3, 2, 5, 5, 7, 10, 12, 17, 22, 29, 39, 51, 68, 90, 119, 158, 209, 277, 367, 486, 644, 853, 1130, 1497, 1983, 2627, 3480, 4610, 6107, 8090, 10717, 14197, 18807, 24914, 33004, 43721, 57918, 76725, 101639, 134643, 178364, 236282, 313007 };
//      var actual = Flux.Math.GetPerrinSequence().Take(expected.Length).Select(bi => (int)bi).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetPrimeFactors()
//    {
//      var expected = new int[] { 2, 2, 3, 5 };
//      var actual = Flux.Math.GetPrimeFactors(60).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void GetSuperAbundantNumbers()
//    {
//      var expected = new System.Numerics.BigInteger[] { 1, 2, 4, 6, 12, 24, 36, 48, 60, 120, 180, 240, 360, 720, 840, 1260, 1680, 2520, 5040, 10080, 15120, 25200, 27720, 55440 };
//      var actual = Flux.Math.GetSuperAbundantNumbers().Take(expected.Length).Select(kvp => kvp.Key).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void IsJumbled()
//    {
//      Assert.AreEqual(true, Flux.Math.IsJumbled(6765, 10));
//      Assert.AreEqual(true, Flux.Math.IsJumbled(1223, 10));
//      Assert.AreEqual(false, Flux.Math.IsJumbled(1235, 10));
//    }

//    [TestMethod]
//    public void LeastSignificant1Bit()
//    {
//      Assert.AreEqual(pbi[2], Flux.Math.LeastSignificant1Bit(pbi[18]));
//      Assert.AreEqual(pb[2], Flux.Math.LeastSignificant1Bit(pb[18]));
//      Assert.AreEqual(pi[2], Flux.Math.LeastSignificant1Bit(pi[18]));
//      Assert.AreEqual(pl[2], Flux.Math.LeastSignificant1Bit(pl[18]));
//      Assert.AreEqual(psb[2], Flux.Math.LeastSignificant1Bit(psb[18]));
//      Assert.AreEqual(ps[2], Flux.Math.LeastSignificant1Bit(ps[18]));
//      Assert.AreEqual(pui[2], Flux.Math.LeastSignificant1Bit(pui[18]));
//      Assert.AreEqual(pul[2], Flux.Math.LeastSignificant1Bit(pul[18]));
//      Assert.AreEqual(pus[2], Flux.Math.LeastSignificant1Bit(pus[18]));
//    }

//    [TestMethod]
//    public void LeastSignificant1Bit_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      var expected = 2;

//      Flux.Diagnostics.Performance.Measure(() => Flux.Math.LeastSignificant1Bit(value)).Assert((System.Numerics.BigInteger)expected, 1.75);
//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.LeastSignificant1Bit((int)value)).Assert((int)expected, 0.4);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.LeastSignificant1Bit((long)value)).Assert((long)expected, 0.4);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.LeastSignificant1Bit((uint)value)).Assert((uint)expected, 0.4);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.LeastSignificant1Bit((ulong)value)).Assert((ulong)expected, 0.4);
//    }

//    [TestMethod]
//    public void Log2()
//    {
//      Assert.AreEqual(pi[4], Flux.Math.Log2(pbi[18]));
//      Assert.AreEqual(pi[4], Flux.Math.Log2(pb[18]));
//      Assert.AreEqual(pi[4], Flux.Math.Log2(pi[18]));
//      Assert.AreEqual(pi[4], Flux.Math.Log2(pl[18]));
//      Assert.AreEqual(pi[4], Flux.Math.Log2(psb[18]));
//      Assert.AreEqual(pi[4], Flux.Math.Log2(ps[18]));
//      Assert.AreEqual(pi[4], Flux.Math.Log2(pui[18]));
//      Assert.AreEqual(pi[4], Flux.Math.Log2(pul[18]));
//      Assert.AreEqual(pi[4], Flux.Math.Log2(pus[18]));
//    }

//    [TestMethod]
//    public void Log2_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      var expected = 19;

//      Flux.Diagnostics.Performance.Measure(() => Flux.Math.Log2(value)).Assert(expected, 4);
//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Log2((int)value)).Assert(expected, 0.5);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Log2((long)value)).Assert(expected, 0.5);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Log2((uint)value)).Assert(expected, 0.5);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.Log2((ulong)value)).Assert(expected, 0.5);
//    }

//    [TestMethod]
//    public void Max()
//    {
//      Assert.AreEqual(pbi[32], Flux.Math.Max(pbi[23], pbi[32]));
//      Assert.AreEqual(pb[32], Flux.Math.Max(pb[23], pb[32]));
//      Assert.AreEqual(ps[32], Flux.Math.Max(ps[23], ps[32]));
//      Assert.AreEqual(pi[32], Flux.Math.Max(pi[23], ps[32]));
//      Assert.AreEqual(pl[32], Flux.Math.Max(pl[23], ps[32]));
//      Assert.AreEqual(psb[32], Flux.Math.Max(psb[23], psb[32]));
//      Assert.AreEqual(pus[32], Flux.Math.Max(pus[23], pus[32]));
//      Assert.AreEqual(pui[32], Flux.Math.Max(pui[23], pui[32]));
//      Assert.AreEqual(pul[32], Flux.Math.Max(pul[23], pul[32]));
//    }

//    [TestMethod]
//    public void Min()
//    {
//      Assert.AreEqual(pbi[23], Flux.Math.Min(pbi[23], pbi[32]));
//      Assert.AreEqual(pb[23], Flux.Math.Min(pb[23], pb[32]));
//      Assert.AreEqual(ps[23], Flux.Math.Min(ps[23], ps[32]));
//      Assert.AreEqual(pi[23], Flux.Math.Min(pi[23], ps[32]));
//      Assert.AreEqual(pl[23], Flux.Math.Min(pl[23], ps[32]));
//      Assert.AreEqual(psb[23], Flux.Math.Min(psb[23], psb[32]));
//      Assert.AreEqual(pus[23], Flux.Math.Min(pus[23], pus[32]));
//      Assert.AreEqual(pui[23], Flux.Math.Min(pui[23], pui[32]));
//      Assert.AreEqual(pul[23], Flux.Math.Min(pul[23], pul[32]));
//    }

//    [TestMethod]
//    public void MostSignificant1Bit()
//    {
//      Assert.AreEqual(pbi[16], Flux.Math.MostSignificant1Bit(pbi[18]));
//      Assert.AreEqual(pb[16], Flux.Math.MostSignificant1Bit(pb[18]));
//      Assert.AreEqual(pi[16], Flux.Math.MostSignificant1Bit(pi[18]));
//      Assert.AreEqual(pl[16], Flux.Math.MostSignificant1Bit(pl[18]));
//      Assert.AreEqual(psb[16], Flux.Math.MostSignificant1Bit(psb[18]));
//      Assert.AreEqual(ps[16], Flux.Math.MostSignificant1Bit(ps[18]));
//      Assert.AreEqual(pui[16], Flux.Math.MostSignificant1Bit(pui[18]));
//      Assert.AreEqual(pul[16], Flux.Math.MostSignificant1Bit(pul[18]));
//      Assert.AreEqual(pus[16], Flux.Math.MostSignificant1Bit(pus[18]));
//    }

//    [TestMethod]
//    public void MostSignificant1Bit_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      Flux.Diagnostics.Performance.Measure(() => Flux.Math.MostSignificant1Bit(value)).Assert((System.Numerics.BigInteger)524288, 4.5);
//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.MostSignificant1Bit((int)value)).Assert(524288, 0.5);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.MostSignificant1Bit((long)value)).Assert(524288L, 0.5);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.MostSignificant1Bit((uint)value)).Assert(524288U, 0.5);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.MostSignificant1Bit((ulong)value)).Assert(524288UL, 0.5);
//    }

//    [TestMethod]
//    public void NextPowerOf2()
//    {
//      Assert.AreEqual(pbi[8], Flux.Math.NextPowerOf2(pbi[5], false));
//      Assert.AreEqual(pbi[32], Flux.Math.NextPowerOf2(pbi[17], false));
//      Assert.AreEqual(pbi[32], Flux.Math.NextPowerOf2(pbi[32], false));

//      Assert.AreEqual(pi[8], Flux.Math.NextPowerOf2(pi[5], false));
//      Assert.AreEqual(pi[32], Flux.Math.NextPowerOf2(pi[17], false));
//      Assert.AreEqual(pi[32], Flux.Math.NextPowerOf2(pi[32], false));

//      Assert.AreEqual(pi[8], Flux.Math.NextPowerOf2(pl[5], false));
//      Assert.AreEqual(pi[32], Flux.Math.NextPowerOf2(pl[17], false));
//      Assert.AreEqual(pi[32], Flux.Math.NextPowerOf2(pl[32], false));

//      Assert.AreEqual(pbi[8], Flux.Math.NextPowerOf2(pbi[5], true));
//      Assert.AreEqual(pbi[32], Flux.Math.NextPowerOf2(pbi[17], true));
//      Assert.AreEqual(pbi[64], Flux.Math.NextPowerOf2(pbi[32], true));

//      Assert.AreEqual(pi[8], Flux.Math.NextPowerOf2(pi[5], true));
//      Assert.AreEqual(pi[32], Flux.Math.NextPowerOf2(pi[17], true));
//      Assert.AreEqual(pi[64], Flux.Math.NextPowerOf2(pi[32], true));

//      Assert.AreEqual(pl[8], Flux.Math.NextPowerOf2(pl[5], true));
//      Assert.AreEqual(pl[32], Flux.Math.NextPowerOf2(pl[17], true));
//      Assert.AreEqual(pl[64], Flux.Math.NextPowerOf2(pl[32], true));
//    }

//    [TestMethod]
//    public void Pow2()
//    {
//      Assert.AreEqual(pi[16], Flux.Math.Pow(pb[4], 2));
//      Assert.AreEqual(pi[16], Flux.Math.Pow(ps[4], 2));
//      Assert.AreEqual(pi[16], Flux.Math.Pow(pi[4], 2));
//      Assert.AreEqual(pl[16], Flux.Math.Pow(pl[4], 2));
//      Assert.AreEqual(pi[16], Flux.Math.Pow(psb[4], 2));
//      Assert.AreEqual(pi[16], Flux.Math.Pow(pus[4], 2));
//      Assert.AreEqual(pui[16], Flux.Math.Pow(pui[4], 2));
//      Assert.AreEqual(pul[16], Flux.Math.Pow(pul[4], 2));
//    }

//    [TestMethod]
//    public void PowerOf()
//    {
//      Assert.AreEqual(64, Flux.Math.PowerOf(101, 2));
//      Assert.AreEqual(64, Flux.Math.PowerOf(101, 8));
//      Assert.AreEqual(100, Flux.Math.PowerOf(101, 10));
//      Assert.AreEqual(16, Flux.Math.PowerOf(101, 16));
//    }

//    [TestMethod]
//    public void PreviousPowerOf2()
//    {
//      Assert.AreEqual(pbi[8], Flux.Math.PreviousPowerOf2(pbi[10], false));
//      Assert.AreEqual(pbi[16], Flux.Math.PreviousPowerOf2(pbi[19], false));
//      Assert.AreEqual(pbi[32], Flux.Math.PreviousPowerOf2(pbi[32], false));

//      Assert.AreEqual(pi[8], Flux.Math.PreviousPowerOf2(pi[10], false));
//      Assert.AreEqual(pi[16], Flux.Math.PreviousPowerOf2(pi[19], false));
//      Assert.AreEqual(pi[32], Flux.Math.PreviousPowerOf2(pi[32], false));

//      Assert.AreEqual(pl[8], Flux.Math.PreviousPowerOf2(pl[10], false));
//      Assert.AreEqual(pl[16], Flux.Math.PreviousPowerOf2(pl[19], false));
//      Assert.AreEqual(pl[32], Flux.Math.PreviousPowerOf2(pl[32], false));

//      Assert.AreEqual(pbi[8], Flux.Math.PreviousPowerOf2(pbi[10], true));
//      Assert.AreEqual(pbi[16], Flux.Math.PreviousPowerOf2(pbi[19], true));
//      Assert.AreEqual(pbi[16], Flux.Math.PreviousPowerOf2(pbi[32], true));

//      Assert.AreEqual(pi[8], Flux.Math.PreviousPowerOf2(pi[10], true));
//      Assert.AreEqual(pi[16], Flux.Math.PreviousPowerOf2(pi[19], true));
//      Assert.AreEqual(pi[16], Flux.Math.PreviousPowerOf2(pi[32], true));

//      Assert.AreEqual(pl[8], Flux.Math.PreviousPowerOf2(pl[10], true));
//      Assert.AreEqual(pl[16], Flux.Math.PreviousPowerOf2(pl[19], true));
//      Assert.AreEqual(pl[16], Flux.Math.PreviousPowerOf2(pl[32], true));
//    }

//    [TestMethod]
//    public void Radix_DigitCount()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      Assert.AreEqual(20, Flux.Math.DigitCount(value, 2));
//      Assert.AreEqual(7, Flux.Math.DigitCount(value, 8));
//      Assert.AreEqual(6, Flux.Math.DigitCount(value, 10));
//      Assert.AreEqual(5, Flux.Math.DigitCount(value, 16));
//    }

//    [TestMethod]
//    public void Radix_DigitSum()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      Assert.AreEqual(9, Flux.Math.DigitSum(value, 2));
//      Assert.AreEqual(21, Flux.Math.DigitSum(value, 8));
//      Assert.AreEqual(21, Flux.Math.DigitSum(value, 10));
//      Assert.AreEqual(30, Flux.Math.DigitSum(value, 16));
//    }

//    [TestMethod]
//    public void Radix_DropLeadingDigit()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      Assert.AreEqual(146242, Flux.Math.DropLeadingDigit(value, 2)); // 10100011101101000010 = 0100011101101000010
//      Assert.AreEqual(146242, Flux.Math.DropLeadingDigit(value, 8)); // 2435502 = 435502
//      Assert.AreEqual(70530, Flux.Math.DropLeadingDigit(value, 10)); // 670530 = 70530
//      Assert.AreEqual(15170, Flux.Math.DropLeadingDigit(value, 16)); // A3B42 = 3B42
//    }

//    [TestMethod]
//    public void Radix_DropTrailingDigit()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      Assert.AreEqual(335265, Flux.Math.DropTrailingDigit(value, 2)); // bin 10100011101101000010 = bin 1010001110110100001 = dec 335265
//      Assert.AreEqual(83816, Flux.Math.DropTrailingDigit(value, 8)); // oct 2435502 = oct 243550 = dec 83816
//      Assert.AreEqual(67053, Flux.Math.DropTrailingDigit(value, 10)); // dec 670530 = dec 67053
//      Assert.AreEqual(41908, Flux.Math.DropTrailingDigit(value, 16)); // hex A3B42 = hex A3B4 = dec 41908
//    }

//    [TestMethod]
//    public void Radix_IsPowerOf()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      Assert.AreEqual(false, Flux.Math.IsPowerOf(value, 2));
//      Assert.AreEqual(false, Flux.Math.IsPowerOf(value, 8));
//      Assert.AreEqual(false, Flux.Math.IsPowerOf(value, 10));
//      Assert.AreEqual(false, Flux.Math.IsPowerOf(value, 16));

//      Assert.AreEqual(true, Flux.Math.IsPowerOf(4, 2));
//      Assert.AreEqual(true, Flux.Math.IsPowerOf(64, 8));
//      Assert.AreEqual(true, Flux.Math.IsPowerOf(100, 10));
//      Assert.AreEqual(true, Flux.Math.IsPowerOf(256, 16));
//    }

//    [TestMethod]
//    public void Radix_PowerOf()
//    {
//      Assert.AreEqual(16, Flux.Math.PowerOf(30, 2));
//      Assert.AreEqual(8, Flux.Math.PowerOf(30, 8));
//      Assert.AreEqual(10, Flux.Math.PowerOf(30, 10));
//      Assert.AreEqual(16, Flux.Math.PowerOf(30, 16));
//    }

//    [TestMethod]
//    public void ReverseBits()
//    {
//      Assert.AreEqual(pbi[30], Flux.Math.ReverseBits(pbi[120]));
//      Assert.AreEqual(pbi[1], Flux.Math.ReverseBits(pbi[128]));

//      Assert.AreEqual(pbi[48], Flux.Math.ReverseBits(pbi[12]));
//      Assert.AreEqual(pbi[222], Flux.Math.ReverseBits(pbi[123]));
//      Assert.AreEqual(805306368U, Flux.Math.ReverseBits(pui[12]));
//      Assert.AreEqual(301989888U, Flux.Math.ReverseBits(pui[72]));
//      Assert.AreEqual(3458764513820540928U, Flux.Math.ReverseBits(pul[12]));
//      Assert.AreEqual(1297036692682702848U, Flux.Math.ReverseBits(pul[72]));
//    }
//    // 10100011101101000010
//    // 01000010110111000101
//    [TestMethod]
//    public void ReverseBits_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseBits(value)).Assert((System.Numerics.BigInteger)4381776, 10);
//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseBits((int)value)).Assert(1121734656, 0.5);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseBits((long)value)).Assert(4817813662309810176L, 0.5);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseBits((uint)value)).Assert(1121734656U, 0.5);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseBits((ulong)value)).Assert(4817813662309810176UL, 0.5);
//    }

//    [TestMethod]
//    public void ReverseDigits()
//    {
//      Assert.AreEqual(pbi[21], Flux.Math.ReverseDigits(pbi[12], 10));
//      Assert.AreEqual(pbi[27], Flux.Math.ReverseDigits(pbi[72], 10));
//      Assert.AreEqual(pb[21], Flux.Math.ReverseDigits(pb[12], 10));
//      Assert.AreEqual(pb[27], Flux.Math.ReverseDigits(pb[72], 10));
//      Assert.AreEqual(pus[21], Flux.Math.ReverseDigits(pus[12], 10));
//      Assert.AreEqual(pus[27], Flux.Math.ReverseDigits(pus[72], 10));
//      Assert.AreEqual(pui[21], Flux.Math.ReverseDigits(pui[12], pi[10]));
//      Assert.AreEqual(pui[27], Flux.Math.ReverseDigits(pui[72], pi[10]));
//      Assert.AreEqual(pul[21], Flux.Math.ReverseDigits(pul[12], pi[10]));
//      Assert.AreEqual(pul[27], Flux.Math.ReverseDigits(pul[72], pi[10]));
//    }

//    [TestMethod]
//    public void ReverseDigits_Speed()
//    {
//      var value = System.Numerics.BigInteger.Parse("670530");

//      if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseDigits((int)value, 10)).Assert(35076, 1.5);
//      if (value >= long.MinValue && value <= long.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseDigits((long)value, 10)).Assert(35076L, 1.5);
//      Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseDigits(value, 10)).Assert((System.Numerics.BigInteger)35076, 6.0);
//      if (value >= uint.MinValue && value <= uint.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseDigits((uint)value, 10)).Assert(35076U, 1.5);
//      if (value >= ulong.MinValue && value <= ulong.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Math.ReverseDigits((ulong)value, 10)).Assert(35076UL, 1.5);
//    }

//    [TestMethod]
//    public void SieveOfEratosthenes()
//    {
//      var expected = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997, 1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069, 1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163, 1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373, 1381, 1399, 1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511, 1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597, 1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657, 1663, 1667, 1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759, 1777, 1783, 1787, 1789, 1801, 1811, 1823, 1831, 1847, 1861, 1867, 1871, 1873, 1877, 1879, 1889, 1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987, 1993, 1997, 1999, 2003, 2011, 2017, 2027, 2029, 2039, 2053, 2063, 2069, 2081, 2083, 2087, 2089, 2099, 2111, 2113, 2129, 2131, 2137, 2141, 2143, 2153, 2161, 2179, 2203, 2207, 2213, 2221, 2237, 2239, 2243, 2251, 2267, 2269, 2273, 2281, 2287, 2293, 2297, 2309, 2311, 2333, 2339, 2341, 2347, 2351, 2357, 2371, 2377, 2381, 2383, 2389, 2393, 2399, 2411, 2417, 2423, 2437, 2441, 2447, 2459, 2467, 2473, 2477, 2503, 2521, 2531, 2539, 2543, 2549, 2551, 2557, 2579, 2591, 2593, 2609, 2617, 2621, 2633, 2647, 2657, 2659, 2663, 2671, 2677, 2683, 2687, 2689, 2693, 2699, 2707, 2711, 2713, 2719, 2729, 2731, 2741, 2749, 2753, 2767, 2777, 2789, 2791, 2797, 2801, 2803, 2819, 2833, 2837, 2843, 2851, 2857, 2861, 2879, 2887, 2897, 2903, 2909, 2917, 2927, 2939, 2953, 2957, 2963, 2969, 2971, 2999, 3001, 3011, 3019, 3023, 3037, 3041, 3049, 3061, 3067, 3079, 3083, 3089, 3109, 3119, 3121, 3137, 3163, 3167, 3169, 3181, 3187, 3191, 3203, 3209, 3217, 3221, 3229, 3251, 3253, 3257, 3259, 3271, 3299, 3301, 3307, 3313, 3319, 3323, 3329, 3331, 3343, 3347, 3359, 3361, 3371, 3373, 3389, 3391, 3407, 3413, 3433, 3449, 3457, 3461, 3463, 3467, 3469, 3491, 3499, 3511, 3517, 3527, 3529, 3533, 3539, 3541, 3547, 3557, 3559, 3571, 3581, 3583, 3593, 3607, 3613, 3617, 3623, 3631, 3637, 3643, 3659, 3671, 3673, 3677, 3691, 3697, 3701, 3709, 3719, 3727, 3733, 3739, 3761, 3767, 3769, 3779, 3793, 3797, 3803, 3821, 3823, 3833, 3847, 3851, 3853, 3863, 3877, 3881, 3889, 3907, 3911, 3917, 3919, 3923, 3929, 3931, 3943, 3947, 3967, 3989, 4001, 4003, 4007, 4013, 4019, 4021, 4027, 4049, 4051, 4057, 4073, 4079, 4091, 4093, 4099, 4111, 4127, 4129, 4133, 4139, 4153, 4157, 4159, 4177, 4201, 4211, 4217, 4219, 4229, 4231, 4241, 4243, 4253, 4259, 4261, 4271, 4273, 4283, 4289, 4297, 4327, 4337, 4339, 4349, 4357, 4363, 4373, 4391, 4397, 4409, 4421, 4423, 4441, 4447, 4451, 4457, 4463, 4481, 4483, 4493, 4507, 4513, 4517, 4519, 4523, 4547, 4549, 4561, 4567, 4583, 4591, 4597, 4603, 4621, 4637, 4639, 4643, 4649, 4651, 4657, 4663, 4673, 4679, 4691, 4703, 4721, 4723, 4729, 4733, 4751, 4759, 4783, 4787, 4789, 4793, 4799, 4801, 4813, 4817, 4831, 4861, 4871, 4877, 4889, 4903, 4909, 4919, 4931, 4933, 4937, 4943, 4951, 4957, 4967, 4969, 4973, 4987, 4993, 4999, 5003, 5009, 5011, 5021, 5023, 5039, 5051, 5059, 5077, 5081, 5087, 5099, 5101, 5107, 5113, 5119, 5147, 5153, 5167, 5171, 5179, 5189, 5197, 5209, 5227, 5231, 5233, 5237, 5261, 5273, 5279, 5281, 5297, 5303, 5309, 5323, 5333, 5347, 5351, 5381, 5387, 5393, 5399, 5407, 5413, 5417, 5419, 5431, 5437, 5441, 5443, 5449, 5471, 5477, 5479, 5483, 5501, 5503, 5507, 5519, 5521, 5527, 5531, 5557, 5563, 5569, 5573, 5581, 5591, 5623, 5639, 5641, 5647, 5651, 5653, 5657, 5659, 5669, 5683, 5689, 5693, 5701, 5711, 5717, 5737, 5741, 5743, 5749, 5779, 5783, 5791, 5801, 5807, 5813, 5821, 5827, 5839, 5843, 5849, 5851, 5857, 5861, 5867, 5869, 5879, 5881, 5897, 5903, 5923, 5927, 5939, 5953, 5981, 5987, 6007, 6011, 6029, 6037, 6043, 6047, 6053, 6067, 6073, 6079, 6089, 6091, 6101, 6113, 6121, 6131, 6133, 6143, 6151, 6163, 6173, 6197, 6199, 6203, 6211, 6217, 6221, 6229, 6247, 6257, 6263, 6269, 6271, 6277, 6287, 6299, 6301, 6311, 6317, 6323, 6329, 6337, 6343, 6353, 6359, 6361, 6367, 6373, 6379, 6389, 6397, 6421, 6427, 6449, 6451, 6469, 6473, 6481, 6491, 6521, 6529, 6547, 6551, 6553, 6563, 6569, 6571, 6577, 6581, 6599, 6607, 6619, 6637, 6653, 6659, 6661, 6673, 6679, 6689, 6691, 6701, 6703, 6709, 6719, 6733, 6737, 6761, 6763, 6779, 6781, 6791, 6793, 6803, 6823, 6827, 6829, 6833, 6841, 6857, 6863, 6869, 6871, 6883, 6899, 6907, 6911, 6917, 6947, 6949, 6959, 6961, 6967, 6971, 6977, 6983, 6991, 6997, 7001, 7013, 7019, 7027, 7039, 7043, 7057, 7069, 7079, 7103, 7109, 7121, 7127, 7129, 7151, 7159, 7177, 7187, 7193, 7207, 7211, 7213, 7219, 7229, 7237, 7243, 7247, 7253, 7283, 7297, 7307, 7309, 7321, 7331, 7333, 7349, 7351, 7369, 7393, 7411, 7417, 7433, 7451, 7457, 7459, 7477, 7481, 7487, 7489, 7499, 7507, 7517, 7523, 7529, 7537, 7541, 7547, 7549, 7559, 7561, 7573, 7577, 7583, 7589, 7591, 7603, 7607, 7621, 7639, 7643, 7649, 7669, 7673, 7681, 7687, 7691, 7699, 7703, 7717, 7723, 7727, 7741, 7753, 7757, 7759, 7789, 7793, 7817, 7823, 7829, 7841, 7853, 7867, 7873, 7877, 7879, 7883, 7901, 7907, 7919 };
//      var actual = new Flux.Math.SieveOfEratosthenes(expected.Last()).ToArray();
//      Assert.IsTrue(actual.SequenceEqual(expected));
//    }

//    [TestMethod]
//    public void SincN()
//    {
//      Assert.AreEqual(0.636619772367581, Flux.Math.Sincn(0.5), Flux.Math.EpsilonCpp32);
//    }

//    [TestMethod]
//    public void SincU()
//    {
//      Assert.AreEqual(0.958851077208406, Flux.Math.Sincu(0.5), Flux.Math.EpsilonCpp32);
//    }

//    [TestMethod]
//    public void UnitImpulse()
//    {
//      Assert.AreEqual(1.0, Flux.Math.UnitImpulse(0.0));
//      Assert.AreEqual(0.0, Flux.Math.UnitImpulse(1.0));
//    }

//    [TestMethod]
//    public void Heaviside()
//    {
//      Assert.AreEqual(0.0, Flux.Math.Heaviside(-1.0));
//      Assert.AreEqual(0.5, Flux.Math.Heaviside(0.0));
//      Assert.AreEqual(1.0, Flux.Math.Heaviside(1.0));

//      Assert.AreEqual(0.0, Flux.Math.Heaviside(-1));
//      Assert.AreEqual(0.5, Flux.Math.Heaviside(0));
//      Assert.AreEqual(1.0, Flux.Math.Heaviside(1));
//    }

//    [TestMethod]
//    public void Wrap()
//    {
//      Assert.AreEqual(pbi[7], Flux.Math.Wrap(pbi[19], pbi[3], pbi[9]));
//      Assert.AreEqual(pb[7], Flux.Math.Wrap(pb[19], pb[3], pb[9]));
//      Assert.AreEqual(pi[7], Flux.Math.Wrap(pi[19], pi[3], pi[9]));
//      Assert.AreEqual(pl[7], Flux.Math.Wrap(pl[19], pl[3], pl[9]));
//      Assert.AreEqual(psb[7], Flux.Math.Wrap(psb[19], psb[3], psb[9]));
//      Assert.AreEqual(ps[7], Flux.Math.Wrap(ps[19], ps[3], ps[9]));
//      Assert.AreEqual(pui[7], Flux.Math.Wrap(pui[19], pui[3], pui[9]));
//      Assert.AreEqual(pul[7], Flux.Math.Wrap(pul[19], pul[3], pul[9]));
//      Assert.AreEqual(pus[7], Flux.Math.Wrap(pus[19], pus[3], pus[9]));
//    }
//  }
//}
