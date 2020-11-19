using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitOps
{
	[TestClass]
	public class BitOps
	{
		//readonly System.Numerics.BigInteger[] nbi = System.Linq.Enumerable.Range(0, 255).Select(i => -(System.Numerics.BigInteger)i).ToArray();
		//readonly short[] ns = System.Linq.Enumerable.Range(0, 255).Select(i => (short)-i).ToArray();
		//readonly int[] ni = System.Linq.Enumerable.Range(0, 255).Select(i => (int)-i).ToArray();
		//readonly long[] nl = System.Linq.Enumerable.Range(0, 255).Select(i => -(long)i).ToArray();
		//readonly sbyte[] nsb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)-i).ToArray(); // Restricted to -127.

		readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
		//readonly byte[] pb = System.Linq.Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();
		//readonly short[] ps = System.Linq.Enumerable.Range(0, 255).Select(i => (short)i).ToArray();
		readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
		readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
		//readonly sbyte[] psb = System.Linq.Enumerable.Range(0, 127).Select(i => (sbyte)i).ToArray(); // Restricted to 127.
		//readonly ushort[] pus = System.Linq.Enumerable.Range(0, 255).Select(i => (ushort)i).ToArray();
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
		public void BitIndex_BigInteger()
			=> Assert.AreEqual(4, Flux.BitOps.BitIndex(16.ToBigInteger()));
		[TestMethod]
		public void BitIndex_Int32()
			=> Assert.AreEqual(4, Flux.BitOps.BitIndex(16));
		[TestMethod]
		public void BitIndex_Int64()
			=> Assert.AreEqual(4, Flux.BitOps.BitIndex(16L));
		[TestMethod]
		public void BitIndex_UInt32()
			=> Assert.AreEqual(4, Flux.BitOps.BitIndex(16U));
		[TestMethod]
		public void BitIndex_UInt64()
			=> Assert.AreEqual(4, Flux.BitOps.BitIndex(16UL));

		[TestMethod]
		public void BitLength_BigInteger()
			=> Assert.AreEqual(5, Flux.BitOps.BitLength(18.ToBigInteger()));
		[TestMethod]
		public void BitLength_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.BitLength(value), 500000).Assert(expected, 0.75);
		}
		[TestMethod]
		public void BitLength_Int32()
			=> Assert.AreEqual(5, Flux.BitOps.BitLength(18));
		[TestMethod]
		public void BitLength_Int32_Speed()
		{
			var value = 670530;
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.BitLength(value), 1000000).Assert(expected, 0.20);
		}
		[TestMethod]
		public void BitLength_Int64()
			=> Assert.AreEqual(5, Flux.BitOps.BitLength(18L));
		[TestMethod]
		public void BitLength_Int64_Speed()
		{
			var value = 670530L;
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.BitLength(value), 1000000).Assert(expected, 0.20);
		}
		[TestMethod]
		public void BitLength_UInt32()
			=> Assert.AreEqual(5, Flux.BitOps.BitLength(18U));
		[TestMethod]
		public void BitLength_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.BitLength(value), 1000000).Assert(expected, 0.10);
		}
		[TestMethod]
		public void BitLength_UInt64()
			=> Assert.AreEqual(5, Flux.BitOps.BitLength(18UL));
		[TestMethod]
		public void BitLength_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.BitLength(value), 1000000).Assert(expected, 0.10);
		}

		[TestMethod]
		public void FoldLeastSignificantBits_BigInteger()
			=> Assert.AreEqual(31, Flux.BitOps.FoldLeastSignificantBits(18.ToBigInteger()));
		[TestMethod]
		public void FoldLeastSignificantBits_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 1048575;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldLeastSignificantBits(value), 1000000).Assert((System.Numerics.BigInteger)expected, 0.5);
		}
		[TestMethod]
		public void FoldLeastSignificantBits_Int32()
			=> Assert.AreEqual(31, Flux.BitOps.FoldLeastSignificantBits(18));
		[TestMethod]
		public void FoldLeastSignificantBits_Int32_Speed()
		{
			var value = 670530;
			var expected = 1048575;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldLeastSignificantBits(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void FoldLeastSignificantBits_Int64()
			=> Assert.AreEqual(31, Flux.BitOps.FoldLeastSignificantBits(18L));
		[TestMethod]
		public void FoldLeastSignificantBits_Int64_Speed()
		{
			var value = 670530L;
			var expected = 1048575L;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldLeastSignificantBits(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void FoldLeastSignificantBits_UInt32()
			=> Assert.AreEqual(31U, Flux.BitOps.FoldLeastSignificantBits(18U));
		[TestMethod]
		public void FoldLeastSignificantBits_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 1048575U;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldLeastSignificantBits(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void FoldLeastSignificantBits_UInt64()
			=> Assert.AreEqual(31UL, Flux.BitOps.FoldLeastSignificantBits(18UL));
		[TestMethod]
		public void FoldLeastSignificantBits_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 1048575UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldLeastSignificantBits(value), 1000000).Assert(expected, 0.1);
		}

		[TestMethod]
		public void FoldMostSignificantBits_BigInteger()
			=> Assert.AreEqual(31, Flux.BitOps.FoldMostSignificantBits(18.ToBigInteger()));
		[TestMethod]
		public void FoldMostSignificantBits_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 1048575;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldMostSignificantBits(value), 1000000).Assert((System.Numerics.BigInteger)expected, 0.5);
		}
		[TestMethod]
		public void FoldMostSignificantBits_Int32()
			=> Assert.AreEqual(-2, Flux.BitOps.FoldMostSignificantBits(18));
		[TestMethod]
		public void FoldMostSignificantBits_Int32_Speed()
		{
			var value = 670530;
			var expected = -2;
			if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldMostSignificantBits(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void FoldMostSignificantBits_Int64()
			=> Assert.AreEqual(-2, Flux.BitOps.FoldMostSignificantBits(18L));
		[TestMethod]
		public void FoldMostSignificantBits_Int64_Speed()
		{
			var value = 670530L;
			var expected = -2L;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldMostSignificantBits(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void FoldMostSignificantBits_UInt32()
			=> Assert.AreEqual(4294967294U, Flux.BitOps.FoldMostSignificantBits(18U));
		[TestMethod]
		public void FoldMostSignificantBits_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 4294967294U;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldMostSignificantBits(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void FoldMostSignificantBits_UInt64()
			=> Assert.AreEqual(18446744073709551614UL, Flux.BitOps.FoldMostSignificantBits(18UL));
		[TestMethod]
		public void FoldMostSignificantBits_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 18446744073709551614UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.FoldMostSignificantBits(value), 1000000).Assert(expected, 0.1);
		}

		[TestMethod]
		public void LeadingZeroCount_BigInteger()
			=> Assert.AreEqual(3, Flux.BitOps.LeadingZeroCount(18.ToBigInteger()));
		[TestMethod]
		public void LeadingZeroCount_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 108;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeadingZeroCount(value, 128), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void LeadingZeroCount_Int32()
			=> Assert.AreEqual(27, Flux.BitOps.LeadingZeroCount(18));
		[TestMethod]
		public void LeadingZeroCount_Int32_Speed()
		{
			var value = 670530;
			var expected = 12;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeadingZeroCount(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void LeadingZeroCount_Int64()
			=> Assert.AreEqual(59, Flux.BitOps.LeadingZeroCount(18L));
		[TestMethod]
		public void LeadingZeroCount_Int64_Speed()
		{
			var value = 670530L;
			var expected = 44;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeadingZeroCount(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void LeadingZeroCount_UInt32()
			=> Assert.AreEqual(27, Flux.BitOps.LeadingZeroCount(18U));
		[TestMethod]
		public void LeadingZeroCount_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 12;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeadingZeroCount(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void LeadingZeroCount_UInt64()
			=> Assert.AreEqual(59, Flux.BitOps.LeadingZeroCount(18UL));
		[TestMethod]
		public void LeadingZeroCount_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 44;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeadingZeroCount(value), 1000000).Assert(expected, 0.1);
		}

		[TestMethod]
		public void LeastSignificant1Bit_BigInteger()
		{
			Assert.AreEqual(pbi[2], Flux.BitOps.LeastSignificant1Bit(pbi[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 2.ToBigInteger();
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 0.150);
		}
		[TestMethod]
		public void LeastSignificant1Bit_Int32()
		{
			Assert.AreEqual(pi[2], Flux.BitOps.LeastSignificant1Bit(pi[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_Int32_Speed()
		{
			var value = 670530;
			var expected = 2;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 0.100);
		}
		[TestMethod]
		public void LeastSignificant1Bit_Int64()
		{
			Assert.AreEqual(pl[2], Flux.BitOps.LeastSignificant1Bit(pl[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_Int64_Speed()
		{
			var value = 670530L;
			var expected = 2L;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 0.100);
		}
		[TestMethod]
		public void LeastSignificant1Bit_UInt32()
		{
			Assert.AreEqual(pui[2], Flux.BitOps.LeastSignificant1Bit(pui[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 2U;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 0.100);
		}
		[TestMethod]
		public void LeastSignificant1Bit_UInt64()
		{
			Assert.AreEqual(pul[2], Flux.BitOps.LeastSignificant1Bit(pul[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 2UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 0.100);
		}

		[TestMethod]
		public void Log2_BigInteger()
		{
			Assert.AreEqual(pi[4], Flux.BitOps.Log2(pbi[18]));
		}
		[TestMethod]
		public void Log2_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 19; // Log2() returns an int.
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.Log2(value), 1000000).Assert(expected, 0.2);
		}
		[TestMethod]
		public void Log2_UInt32()
		{
			Assert.AreEqual(pi[4], Flux.BitOps.Log2(pbi[18]));
		}
		[TestMethod]
		public void Log2_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 19; // Log2() returns an int.
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.Log2(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void Log2_UInt64()
		{
			Assert.AreEqual(pi[4], Flux.BitOps.Log2(pbi[18]));
		}
		[TestMethod]
		public void Log2_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 19; // Log2() returns an int.
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.Log2(value), 1000000).Assert(expected, 0.1);
		}

		[TestMethod]
		public void MostSignificant1Bit_BigInteger()
		{
			Assert.AreEqual(pbi[16], Flux.BitOps.MostSignificant1Bit(pbi[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 524288.ToBigInteger();
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.5);
		}
		[TestMethod]
		public void MostSignificant1Bit_Int32()
		{
			Assert.AreEqual(pi[16], Flux.BitOps.MostSignificant1Bit(pi[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_Int32_Speed()
		{
			var value = 670530;
			var expected = 524288;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void MostSignificant1Bit_Int64()
		{
			Assert.AreEqual(pl[16], Flux.BitOps.MostSignificant1Bit(pl[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_Int64_Speed()
		{
			var value = 670530L;
			var expected = 524288L;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void MostSignificant1Bit_UInt32()
		{
			Assert.AreEqual(pui[16], Flux.BitOps.MostSignificant1Bit(pui[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 524288U;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void MostSignificant1Bit_UInt64()
		{
			Assert.AreEqual(pul[16], Flux.BitOps.MostSignificant1Bit(pul[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 524288UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.1);
		}

		[TestMethod]
		public void NextPowerOf2_BigInteger()
		{
			Assert.AreEqual(pbi[8], Flux.BitOps.NextPowerOf2(pbi[5]));
			Assert.AreEqual(pbi[32], Flux.BitOps.NextPowerOf2(pbi[17]));
			Assert.AreEqual(pbi[64], Flux.BitOps.NextPowerOf2(pbi[32]));
		}
		[TestMethod]
		public void NextPowerOf2_Int32()
		{
			Assert.AreEqual(pi[8], Flux.BitOps.NextPowerOf2(pi[5]));
			Assert.AreEqual(pi[32], Flux.BitOps.NextPowerOf2(pi[17]));
			Assert.AreEqual(pi[64], Flux.BitOps.NextPowerOf2(pi[32]));
		}
		[TestMethod]
		public void NextPowerOf2_Int64()
		{
			Assert.AreEqual(pi[8], Flux.BitOps.NextPowerOf2(pl[5]));
			Assert.AreEqual(pi[32], Flux.BitOps.NextPowerOf2(pl[17]));
			Assert.AreEqual(pi[64], Flux.BitOps.NextPowerOf2(pl[32]));
		}

		[TestMethod]
		public void PopCount_BigInteger()
			=> Assert.AreEqual(2, Flux.BitOps.PopCount(18.ToBigInteger()));
		[TestMethod]
		public void PopCount_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.PopCount(value), 500000).Assert(expected, 0.1); // Large discrepancy between Debug and Release code. Not a problem.
		}
		[TestMethod]
		public void PopCount_Int32()
			=> Assert.AreEqual(2, Flux.BitOps.PopCount(18));
		[TestMethod]
		public void PopCount_Int32_Speed()
		{
			var value = 670530;
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.PopCount(value), 1000000).Assert(expected, 0.1); // Large discrepancy between Debug and Release code. Not a problem.
		}
		[TestMethod]
		public void PopCount_Int64()
			=> Assert.AreEqual(2, Flux.BitOps.PopCount(18));
		[TestMethod]
		public void PopCount_Int64_Speed()
		{
			var value = 670530L;
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.PopCount(value), 1000000).Assert(expected, 0.1); // Large discrepancy between Debug and Release code. Not a problem.
		}
		[TestMethod]
		public void PopCount_UInt32()
			=> Assert.AreEqual(2, Flux.BitOps.PopCount(18));
		[TestMethod]
		public void PopCount_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.PopCount(value), 1000000).Assert(expected, 0.1); // Large discrepancy between Debug and Release code. Not a problem.
		}
		[TestMethod]
		public void PopCount_UInt64()
			=> Assert.AreEqual(2, Flux.BitOps.PopCount(18));
		[TestMethod]
		public void PopCount_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.PopCount(value), 1000000).Assert(expected, 0.1); // Large discrepancy between Debug and Release code. Not a problem.
		}

		[TestMethod]
		public void Pow2_BigInteger()
		{
			Assert.AreEqual(pi[16], Flux.Maths.Pow(pi[4], 2));
		}
		[TestMethod]
		public void Pow2_Int32()
		{
			Assert.AreEqual(pi[16], Flux.Maths.Pow(pi[4], 2));
		}
		[TestMethod]
		public void Pow2_Int64()
		{
			Assert.AreEqual(pl[16], Flux.Maths.Pow(pl[4], 2));
		}
		[TestMethod]
		public void Pow2_UInt32()
		{
			Assert.AreEqual(pui[16], Flux.Maths.Pow(pui[4], 2));
		}
		[TestMethod]
		public void Pow2_UInt64()
		{
			Assert.AreEqual(pul[16], Flux.Maths.Pow(pul[4], 2));
		}

		[TestMethod]
		public void PowerOf2_BigInteger()
		{
			Assert.AreEqual(64.ToBigInteger(), Flux.BitOps.PowerOf2(101.ToBigInteger()));
		}
		[TestMethod]
		public void PowerOf2_Int32()
		{
			Assert.AreEqual(64, Flux.BitOps.PowerOf2(101));
		}
		[TestMethod]
		public void PowerOf2_Int64()
		{
			Assert.AreEqual(64L, Flux.BitOps.PowerOf2(101L));
		}

		[TestMethod]
		public void PreviousPowerOf2_BigInteger()
		{
			Assert.AreEqual(pbi[4], Flux.BitOps.PreviousPowerOf2(pbi[10]));
			Assert.AreEqual(pbi[8], Flux.BitOps.PreviousPowerOf2(pbi[19]));
			Assert.AreEqual(pbi[16], Flux.BitOps.PreviousPowerOf2(pbi[32]));
		}
		[TestMethod]
		public void PreviousPowerOf2_Int32()
		{
			Assert.AreEqual(pi[4], Flux.BitOps.PreviousPowerOf2(pi[10]));
			Assert.AreEqual(pi[8], Flux.BitOps.PreviousPowerOf2(pi[19]));
			Assert.AreEqual(pi[16], Flux.BitOps.PreviousPowerOf2(pi[32]));
		}
		[TestMethod]
		public void PreviousPowerOf2_Int64()
		{
			Assert.AreEqual(pl[4], Flux.BitOps.PreviousPowerOf2(pl[10]));
			Assert.AreEqual(pl[8], Flux.BitOps.PreviousPowerOf2(pl[19]));
			Assert.AreEqual(pl[16], Flux.BitOps.PreviousPowerOf2(pl[32]));
		}

		[TestMethod]
		public void ReverseBits_BigInteger()
		{
			Assert.AreEqual(pbi[30], Flux.BitOps.ReverseBits(pbi[120]));
			Assert.AreEqual(pbi[1], Flux.BitOps.ReverseBits(pbi[128]));

			Assert.AreEqual(pbi[48], Flux.BitOps.ReverseBits(pbi[12]));
			Assert.AreEqual(pbi[222], Flux.BitOps.ReverseBits(pbi[123]));
		}
		[TestMethod]
		public void ReverseBits_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 4381776.ToBigInteger();
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.4);
		}
		[TestMethod]
		public void ReverseBits_Int32()
		{
			Assert.AreEqual(805306368, Flux.BitOps.ReverseBits(pi[12]));
			Assert.AreEqual(301989888, Flux.BitOps.ReverseBits(pi[72]));
		}
		[TestMethod]
		public void ReverseBits_Int32_Speed()
		{
			var value = 670530;
			var expected = 1121734656;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void ReverseBits_Int64()
		{
			Assert.AreEqual(3458764513820540928, Flux.BitOps.ReverseBits(pl[12]));
			Assert.AreEqual(1297036692682702848, Flux.BitOps.ReverseBits(pl[72]));
		}
		[TestMethod]
		public void ReverseBits_Int64_Speed()
		{
			var value = 670530L;
			var expected = 4817813662309810176L;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void ReverseBits_UInt32()
		{
			Assert.AreEqual(805306368U, Flux.BitOps.ReverseBits(pui[12]));
			Assert.AreEqual(301989888U, Flux.BitOps.ReverseBits(pui[72]));
		}
		[TestMethod]
		public void ReverseBits_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 1121734656U;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void ReverseBits_UInt64()
		{
			Assert.AreEqual(3458764513820540928U, Flux.BitOps.ReverseBits(pul[12]));
			Assert.AreEqual(1297036692682702848U, Flux.BitOps.ReverseBits(pul[72]));
		}
		[TestMethod]
		public void ReverseBits_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 4817813662309810176UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.1);
		}

		[TestMethod]
		public void TrailingZeroCount_BigInteger()
			=> Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18.ToBigInteger()));
		[TestMethod]
		public void TrailingZeroCount_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.2);
		}
		[TestMethod]
		public void TrailingZeroCount_Int32()
			=> Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18));
		[TestMethod]
		public void TrailingZeroCount_Int32_Speed()
		{
			var value = 670530;
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void TrailingZeroCount_Int64()
			=> Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18L));
		[TestMethod]
		public void TrailingZeroCount_Int64_Speed()
		{
			var value = 670530L;
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void TrailingZeroCount_UInt32()
			=> Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18U));
		[TestMethod]
		public void TrailingZeroCount_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.1);
		}
		[TestMethod]
		public void TrailingZeroCount_UInt64()
			=> Assert.AreEqual(1, Flux.BitOps.TrailingZeroCount(18UL));
		[TestMethod]
		public void TrailingZeroCount_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.1);
		}
	}
}
