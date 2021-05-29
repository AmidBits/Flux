using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numerics
{
	[TestClass]
	public class BitOps
	{
		readonly System.Numerics.BigInteger[] pbi = System.Linq.Enumerable.Range(0, 255).Select(i => (System.Numerics.BigInteger)i).ToArray();
		readonly int[] pi = System.Linq.Enumerable.Range(0, 255).Select(i => (int)i).ToArray();
		readonly long[] pl = System.Linq.Enumerable.Range(0, 255).Select(i => (long)i).ToArray();
		readonly uint[] pui = System.Linq.Enumerable.Range(0, 255).Select(i => (uint)i).ToArray();
		readonly ulong[] pul = System.Linq.Enumerable.Range(0, 255).Select(i => (ulong)i).ToArray();

		[TestMethod]
		public void BitLength_BigInteger()
			=> Assert.AreEqual(5, Flux.Numerics.BitOps.BitLength(18.ToBigInteger()));
		[TestMethod]
		public void BitLength_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.BitLength(value), 500000).Assert(expected, 1);
		}
		[TestMethod]
		public void BitLength_Int32()
			=> Assert.AreEqual(5, Flux.Numerics.BitOps.BitLength(18));
		[TestMethod]
		public void BitLength_Int32_Speed()
		{
			var value = 670530;
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.BitLength(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void BitLength_Int64()
			=> Assert.AreEqual(5, Flux.Numerics.BitOps.BitLength(18L));
		[TestMethod]
		public void BitLength_Int64_Speed()
		{
			var value = 670530L;
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.BitLength(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void BitLength_UInt32()
			=> Assert.AreEqual(5, Flux.Numerics.BitOps.BitLength(18U));
		[TestMethod]
		public void BitLength_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.BitLength(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void BitLength_UInt64()
			=> Assert.AreEqual(5, Flux.Numerics.BitOps.BitLength(18UL));
		[TestMethod]
		public void BitLength_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 20;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.BitLength(value), 1000000).Assert(expected, 0.3);
		}

		[TestMethod]
		public void FoldHigh_BigInteger()
			=> Assert.AreEqual(30.ToBigInteger(), Flux.Numerics.BitOps.FoldLeft(18.ToBigInteger()));
		[TestMethod]
		public void FoldHigh_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530"); // 0x000a3b42
			var expected = System.Numerics.BigInteger.Parse("1048574"); // 0x000ffffe
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldLeft(value), 1000000).Assert(expected, 4);
		}
		[TestMethod]
		public void FoldHigh_Int32()
			=> Assert.AreEqual(-2, Flux.Numerics.BitOps.FoldLeft(18));
		[TestMethod]
		public void FoldHigh_Int32_Speed()
		{
			var value = 670530;
			var expected = -2;
			if (value >= int.MinValue && value <= int.MaxValue) Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldLeft(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void FoldHigh_Int64()
			=> Assert.AreEqual(-2, Flux.Numerics.BitOps.FoldLeft(18L));
		[TestMethod]
		public void FoldHigh_Int64_Speed()
		{
			var value = 670530L;
			var expected = -2L;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldLeft(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void FoldHigh_UInt32()
			=> Assert.AreEqual(4294967294U, Flux.Numerics.BitOps.FoldLeft(18U));
		[TestMethod]
		public void FoldHigh_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 4294967294U;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldLeft(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void FoldHigh_UInt64()
			=> Assert.AreEqual(18446744073709551614UL, Flux.Numerics.BitOps.FoldLeft(18UL));
		[TestMethod]
		public void FoldHigh_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 18446744073709551614UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldLeft(value), 1000000).Assert(expected, 0.3);
		}

		[TestMethod]
		public void FoldLow_BigInteger()
			=> Assert.AreEqual(31, Flux.Numerics.BitOps.FoldRight(18.ToBigInteger()));
		[TestMethod]
		public void FoldLow_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 1048575;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldRight(value), 1000000).Assert((System.Numerics.BigInteger)expected, 1);
		}
		[TestMethod]
		public void FoldLow_Int32()
			=> Assert.AreEqual(31, Flux.Numerics.BitOps.FoldRight(18));
		[TestMethod]
		public void FoldLow_Int32_Speed()
		{
			var value = 670530;
			var expected = 1048575;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldRight(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void FoldLow_Int64()
			=> Assert.AreEqual(31, Flux.Numerics.BitOps.FoldRight(18L));
		[TestMethod]
		public void FoldLow_Int64_Speed()
		{
			var value = 670530L;
			var expected = 1048575L;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldRight(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void FoldLow_UInt32()
			=> Assert.AreEqual(31U, Flux.Numerics.BitOps.FoldRight(18U));
		[TestMethod]
		public void FoldLow_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 1048575U;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldRight(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void FoldLow_UInt64()
			=> Assert.AreEqual(31UL, Flux.Numerics.BitOps.FoldRight(18UL));
		[TestMethod]
		public void FoldLow_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 1048575UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.FoldRight(value), 1000000).Assert(expected, 0.3);
		}

		[TestMethod]
		public void LeadingZeroCount_BigInteger()
			=> Assert.AreEqual(3, Flux.Numerics.BitOps.LeadingZeroCount(18.ToBigInteger()));
		[TestMethod]
		public void LeadingZeroCount_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 108;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeadingZeroCount(value, 128), 1000000).Assert(expected, 1);
		}
		[TestMethod]
		public void LeadingZeroCount_Int32()
			=> Assert.AreEqual(27, Flux.Numerics.BitOps.LeadingZeroCount(18));
		[TestMethod]
		public void LeadingZeroCount_Int32_Speed()
		{
			var value = 670530;
			var expected = 12;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeadingZeroCount(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void LeadingZeroCount_Int64()
			=> Assert.AreEqual(59, Flux.Numerics.BitOps.LeadingZeroCount(18L));
		[TestMethod]
		public void LeadingZeroCount_Int64_Speed()
		{
			var value = 670530L;
			var expected = 44;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeadingZeroCount(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void LeadingZeroCount_UInt32()
			=> Assert.AreEqual(27, Flux.Numerics.BitOps.LeadingZeroCount(18U));
		[TestMethod]
		public void LeadingZeroCount_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 12;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeadingZeroCount(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void LeadingZeroCount_UInt64()
			=> Assert.AreEqual(59, Flux.Numerics.BitOps.LeadingZeroCount(18UL));
		[TestMethod]
		public void LeadingZeroCount_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 44;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeadingZeroCount(value), 1000000).Assert(expected, 0.3);
		}

		[TestMethod]
		public void LeastSignificant1Bit_BigInteger()
		{
			Assert.AreEqual(pbi[2], Flux.Numerics.BitOps.LeastSignificant1Bit(pbi[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 2.ToBigInteger();
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 1);
		}
		[TestMethod]
		public void LeastSignificant1Bit_Int32()
		{
			Assert.AreEqual(pi[2], Flux.Numerics.BitOps.LeastSignificant1Bit(pi[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_Int32_Speed()
		{
			var value = 670530;
			var expected = 2;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void LeastSignificant1Bit_Int64()
		{
			Assert.AreEqual(pl[2], Flux.Numerics.BitOps.LeastSignificant1Bit(pl[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_Int64_Speed()
		{
			var value = 670530L;
			var expected = 2L;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void LeastSignificant1Bit_UInt32()
		{
			Assert.AreEqual(pui[2], Flux.Numerics.BitOps.LeastSignificant1Bit(pui[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 2U;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void LeastSignificant1Bit_UInt64()
		{
			Assert.AreEqual(pul[2], Flux.Numerics.BitOps.LeastSignificant1Bit(pul[18]));
		}
		[TestMethod]
		public void LeastSignificant1Bit_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 2UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.LeastSignificant1Bit(value), 1000000).Assert(expected, 0.3);
		}

		[TestMethod]
		public void Log2_BigInteger()
		{
			Assert.AreEqual(pi[4], Flux.Numerics.BitOps.Log2(pbi[18]));
		}
		[TestMethod]
		public void Log2_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 19; // Log2() returns an int.
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.Log2(value), 1000000).Assert(expected, 1);
		}
		[TestMethod]
		public void Log2_UInt32()
		{
			Assert.AreEqual(pi[4], Flux.Numerics.BitOps.Log2(pbi[18]));
		}
		[TestMethod]
		public void Log2_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 19; // Log2() returns an int.
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.Log2(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void Log2_UInt64()
		{
			Assert.AreEqual(pi[4], Flux.Numerics.BitOps.Log2(pbi[18]));
		}
		[TestMethod]
		public void Log2_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 19; // Log2() returns an int.
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.Log2(value), 1000000).Assert(expected, 0.3);
		}

		[TestMethod]
		public void MostSignificant1Bit_BigInteger()
		{
			Assert.AreEqual(pbi[16], Flux.Numerics.BitOps.MostSignificant1Bit(pbi[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 524288.ToBigInteger();
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 1);
		}
		[TestMethod]
		public void MostSignificant1Bit_Int32()
		{
			Assert.AreEqual(pi[16], Flux.Numerics.BitOps.MostSignificant1Bit(pi[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_Int32_Speed()
		{
			var value = 670530;
			var expected = 524288;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void MostSignificant1Bit_Int64()
		{
			Assert.AreEqual(pl[16], Flux.Numerics.BitOps.MostSignificant1Bit(pl[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_Int64_Speed()
		{
			var value = 670530L;
			var expected = 524288L;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void MostSignificant1Bit_UInt32()
		{
			Assert.AreEqual(pui[16], Flux.Numerics.BitOps.MostSignificant1Bit(pui[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 524288U;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void MostSignificant1Bit_UInt64()
		{
			Assert.AreEqual(pul[16], Flux.Numerics.BitOps.MostSignificant1Bit(pul[18]));
		}
		[TestMethod]
		public void MostSignificant1Bit_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 524288UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.MostSignificant1Bit(value), 1000000).Assert(expected, 0.3);
		}

		[TestMethod]
		public void NextPowerOf2_UInt32()
		{
			Assert.AreEqual(pui[8], Flux.Numerics.BitOps.NextHighestPowerOf2(pui[5]));
			Assert.AreEqual(pui[32], Flux.Numerics.BitOps.NextHighestPowerOf2(pui[17]));
			Assert.AreEqual(pui[64], Flux.Numerics.BitOps.NextHighestPowerOf2(pui[32]));
		}
		[TestMethod]
		public void NextPowerOf2_UInt64()
		{
			Assert.AreEqual(pul[8], Flux.Numerics.BitOps.NextHighestPowerOf2(pul[5]));
			Assert.AreEqual(pul[32], Flux.Numerics.BitOps.NextHighestPowerOf2(pul[17]));
			Assert.AreEqual(pul[64], Flux.Numerics.BitOps.NextHighestPowerOf2(pul[32]));
		}

		[TestMethod]
		public void PopCount_BigInteger()
			=> Assert.AreEqual(2, Flux.Numerics.BitOps.PopCount(18.ToBigInteger()));
		[TestMethod]
		public void PopCount_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.PopCount(value), 500000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
		}
		[TestMethod]
		public void PopCount_Int32()
			=> Assert.AreEqual(2, Flux.Numerics.BitOps.PopCount(18));
		[TestMethod]
		public void PopCount_Int32_Speed()
		{
			var value = 670530;
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.PopCount(value), 1000000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
		}
		[TestMethod]
		public void PopCount_Int64()
			=> Assert.AreEqual(2, Flux.Numerics.BitOps.PopCount(18));
		[TestMethod]
		public void PopCount_Int64_Speed()
		{
			var value = 670530L;
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.PopCount(value), 1000000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
		}
		[TestMethod]
		public void PopCount_UInt32()
			=> Assert.AreEqual(2, Flux.Numerics.BitOps.PopCount(18));
		[TestMethod]
		public void PopCount_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.PopCount(value), 1000000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
		}
		[TestMethod]
		public void PopCount_UInt64()
			=> Assert.AreEqual(2, Flux.Numerics.BitOps.PopCount(18));
		[TestMethod]
		public void PopCount_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 9;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.PopCount(value), 1000000).Assert(expected, 0.3); // Large discrepancy between Debug and Release code. Not a problem.
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
		public void ReverseBits_BigInteger()
		{
			Assert.AreEqual(pbi[30], Flux.Numerics.BitOps.ReverseBits(pbi[120]));
			Assert.AreEqual(pbi[1], Flux.Numerics.BitOps.ReverseBits(pbi[128]));

			Assert.AreEqual(pbi[48], Flux.Numerics.BitOps.ReverseBits(pbi[12]));
			Assert.AreEqual(pbi[222], Flux.Numerics.BitOps.ReverseBits(pbi[123]));
		}
		[TestMethod]
		public void ReverseBits_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 4381776.ToBigInteger();
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.ReverseBits(value), 1000000).Assert(expected, 1);
		}
		[TestMethod]
		public void ReverseBits_Int32()
		{
			Assert.AreEqual(805306368, Flux.Numerics.BitOps.ReverseBits(pi[12]));
			Assert.AreEqual(301989888, Flux.Numerics.BitOps.ReverseBits(pi[72]));
		}
		[TestMethod]
		public void ReverseBits_Int32_Speed()
		{
			var value = 670530;
			var expected = 1121734656;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void ReverseBits_Int64()
		{
			Assert.AreEqual(3458764513820540928, Flux.Numerics.BitOps.ReverseBits(pl[12]));
			Assert.AreEqual(1297036692682702848, Flux.Numerics.BitOps.ReverseBits(pl[72]));
		}
		[TestMethod]
		public void ReverseBits_Int64_Speed()
		{
			var value = 670530L;
			var expected = 4817813662309810176L;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void ReverseBits_UInt32()
		{
			Assert.AreEqual(805306368U, Flux.Numerics.BitOps.ReverseBits(pui[12]));
			Assert.AreEqual(301989888U, Flux.Numerics.BitOps.ReverseBits(pui[72]));
		}
		[TestMethod]
		public void ReverseBits_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 1121734656U;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void ReverseBits_UInt64()
		{
			Assert.AreEqual(3458764513820540928U, Flux.Numerics.BitOps.ReverseBits(pul[12]));
			Assert.AreEqual(1297036692682702848U, Flux.Numerics.BitOps.ReverseBits(pul[72]));
		}
		[TestMethod]
		public void ReverseBits_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 4817813662309810176UL;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.ReverseBits(value), 1000000).Assert(expected, 0.3);
		}

		[TestMethod]
		public void RotateLeft_BigInteger()
		{
			Assert.AreEqual(254.ToBigInteger(), Flux.Numerics.BitOps.RotateLeft(pbi[120], 5));
			Assert.AreEqual(272.ToBigInteger(), Flux.Numerics.BitOps.RotateLeft(pbi[128], 5));
			Assert.AreEqual(24.ToBigInteger(), Flux.Numerics.BitOps.RotateLeft(pbi[12], 5));
			Assert.AreEqual(254.ToBigInteger(), Flux.Numerics.BitOps.RotateLeft(pbi[123], 5));
		}
		[TestMethod]
		public void RotateLeft_UInt32()
		{
			Assert.AreEqual(3840U, Flux.Numerics.BitOps.RotateLeft(pui[120], 5));
			Assert.AreEqual(4096U, Flux.Numerics.BitOps.RotateLeft(pui[128], 5));
			Assert.AreEqual(786432U, Flux.Numerics.BitOps.RotateLeft(pui[12], 16));
			Assert.AreEqual(3936U, Flux.Numerics.BitOps.RotateLeft(pui[123], 5));
		}
		[TestMethod]
		public void RotateLeft_UInt64()
		{
			Assert.AreEqual(3840UL, Flux.Numerics.BitOps.RotateLeft(pul[120], 5));
			Assert.AreEqual(4096UL, Flux.Numerics.BitOps.RotateLeft(pul[128], 5));
			Assert.AreEqual(786432UL, Flux.Numerics.BitOps.RotateLeft(pul[12], 16));
			Assert.AreEqual(3936UL, Flux.Numerics.BitOps.RotateLeft(pul[123], 5));
		}

		[TestMethod]
		public void RotateRight_BigInteger()
		{
			Assert.AreEqual(483.ToBigInteger(), Flux.Numerics.BitOps.RotateRight(pbi[120], 5));
			Assert.AreEqual(1028.ToBigInteger(), Flux.Numerics.BitOps.RotateRight(pbi[128], 5));
			Assert.AreEqual(6.ToBigInteger(), Flux.Numerics.BitOps.RotateRight(pbi[12], 5));
			Assert.AreEqual(495.ToBigInteger(), Flux.Numerics.BitOps.RotateRight(pbi[123], 5));
		}
		[TestMethod]
		public void RotateRight_UInt32()
		{
			Assert.AreEqual(3221225475U, Flux.Numerics.BitOps.RotateRight(pui[120], 5));
			Assert.AreEqual(4U, Flux.Numerics.BitOps.RotateRight(pui[128], 5));
			Assert.AreEqual(786432U, Flux.Numerics.BitOps.RotateRight(pui[12], 16));
			Assert.AreEqual(3623878659U, Flux.Numerics.BitOps.RotateRight(pui[123], 5));
		}
		[TestMethod]
		public void RotateRight_UInt64()
		{
			Assert.AreEqual(13835058055282163715UL, Flux.Numerics.BitOps.RotateRight(pul[120], 5));
			Assert.AreEqual(4UL, Flux.Numerics.BitOps.RotateRight(pul[128], 5));
			Assert.AreEqual(3377699720527872UL, Flux.Numerics.BitOps.RotateRight(pul[12], 16));
			Assert.AreEqual(15564440312192434179UL, Flux.Numerics.BitOps.RotateRight(pul[123], 5));
		}

		[TestMethod]
		public void TrailingZeroCount_BigInteger()
			=> Assert.AreEqual(1, Flux.Numerics.BitOps.TrailingZeroCount(18.ToBigInteger()));
		[TestMethod]
		public void TrailingZeroCount_BigInteger_Speed()
		{
			var value = System.Numerics.BigInteger.Parse("670530");
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 1);
		}
		[TestMethod]
		public void TrailingZeroCount_Int32()
			=> Assert.AreEqual(1, Flux.Numerics.BitOps.TrailingZeroCount(18));
		[TestMethod]
		public void TrailingZeroCount_Int32_Speed()
		{
			var value = 670530;
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void TrailingZeroCount_Int64()
			=> Assert.AreEqual(1, Flux.Numerics.BitOps.TrailingZeroCount(18L));
		[TestMethod]
		public void TrailingZeroCount_Int64_Speed()
		{
			var value = 670530L;
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void TrailingZeroCount_UInt32()
			=> Assert.AreEqual(1, Flux.Numerics.BitOps.TrailingZeroCount(18U));
		[TestMethod]
		public void TrailingZeroCount_UInt32_Speed()
		{
			var value = 670530U;
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.3);
		}
		[TestMethod]
		public void TrailingZeroCount_UInt64()
			=> Assert.AreEqual(1, Flux.Numerics.BitOps.TrailingZeroCount(18UL));
		[TestMethod]
		public void TrailingZeroCount_UInt64_Speed()
		{
			var value = 670530UL;
			var expected = 1;
			Flux.Diagnostics.Performance.Measure(() => Flux.Numerics.BitOps.TrailingZeroCount(value), 1000000).Assert(expected, 0.3);
		}
	}
}
