namespace Flux
{
	public static partial class BitOps
	{
		// http://aggregate.org/MAGIC/
		// http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan
		// https://en.wikipedia.org/wiki/Bit-length

		// https://en.wikipedia.org/wiki/Find_first_set#CLZ

		/// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
		/// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary in size there is a required bit width to measure against.</remarks>
		/// <param name="bitWidth">The number of bits in the set. E.g. 32, 64 or 128 for built-in integer data type sizes.</param>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(System.Numerics.BigInteger value, int bitWidth)
			=> bitWidth - BitLength(value);
		/// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
		/// <remarks>Returns a number representing the number of leading zeros of the binary representation of the value. Since BigInteger is arbitrary this version finds and subtracts from the nearest power-of-two bit-length that the value fits in.</remarks>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(System.Numerics.BigInteger value)
		{
			if (value > 255) return BitLength(value) is var bitLength ? ((1 << BitLength(bitLength)) - bitLength) : throw new System.ArithmeticException();
			else if (value > 0) return 8 - BitLength(value);
			else return -1;
		}

		/// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(int value)
			=> LeadingZeroCount((uint)value);
		/// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(long value)
			=> LeadingZeroCount((ulong)value);

		/// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
		[System.CLSCompliant(false)]
		public static int LeadingZeroCount(uint value)
		{
#if NETCOREAPP
			if (System.Runtime.Intrinsics.X86.Lzcnt.IsSupported)
				return (int)System.Runtime.Intrinsics.X86.Lzcnt.LeadingZeroCount(value);
#endif

			if (value == 0)
				return 32;

			return 31 - Log2(value);
		}
		/// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
		[System.CLSCompliant(false)]
		public static int LeadingZeroCount(ulong value)
		{
#if NETCOREAPP
			if (System.Runtime.Intrinsics.X86.Lzcnt.X64.IsSupported)
				return (int)System.Runtime.Intrinsics.X86.Lzcnt.X64.LeadingZeroCount(value);
#endif

			if (value <= uint.MaxValue) // All upper 32 bits are obviously zero.
				return 32 + LeadingZeroCount((uint)value); // Add any lower MSbits.

			return LeadingZeroCount((uint)(value >> 32)); // Evaluate upper 32 bits.
		}
	}
}
