namespace Flux
{
	public static partial class BitOps
	{
		// http://aggregate.org/MAGIC/
		// http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan
		// https://en.wikipedia.org/wiki/Bit-length

		// https://en.wikipedia.org/wiki/Find_first_set#CTZ

		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		public static int TrailingZeroCount(System.Numerics.BigInteger value)
			=> value > 0
			? PopCount((value & -value) - 1)
			: -1;

		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		public static int TrailingZeroCount(int value)
			=> PopCount((value & -value) - 1);
		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		public static int TrailingZeroCount(long value)
			=> PopCount((value & -value) - 1);

		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.CLSCompliant(false)]
		public static int TrailingZeroCount(uint value)
		{
#if NETCOREAPP
			if (System.Runtime.Intrinsics.X86.Bmi1.IsSupported)
				System.Runtime.Intrinsics.X86.Bmi1.TrailingZeroCount(value);
#endif

			if (value == 0)
				return 32;

			return PopCount((value & ((~value) + 1)) - 1);
		}
		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.CLSCompliant(false)]
		public static int TrailingZeroCount(ulong value)
		{
#if NETCOREAPP
			if (System.Runtime.Intrinsics.X86.Bmi1.X64.IsSupported)
				System.Runtime.Intrinsics.X86.Bmi1.X64.TrailingZeroCount(value);
#endif

			if ((uint)value == 0) // All lower 32 bits are obviously zero.
				return TrailingZeroCount((uint)(value >> 32)) + 32; // Add any upper LSbits.

			return TrailingZeroCount((uint)value); // Evaluate lower 32 bits.
		}
	}
}
