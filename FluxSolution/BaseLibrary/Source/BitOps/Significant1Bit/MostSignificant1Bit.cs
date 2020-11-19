namespace Flux
{
	public static partial class BitOps
	{
		// https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit
		// http://aggregate.org/MAGIC/
		// http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan

		/// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set) by clearing the least significant 1 bit in each iteration of a loop.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static System.Numerics.BigInteger MostSignificant1Bit(System.Numerics.BigInteger value)
			=> System.Numerics.BigInteger.One << Log2(value);

		/// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int MostSignificant1Bit(int value)
			=> unchecked((int)MostSignificant1Bit((uint)value));
		/// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long MostSignificant1Bit(long value)
			=> unchecked((long)MostSignificant1Bit((ulong)value));

		/// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
		[System.CLSCompliant(false)]
		public static uint MostSignificant1Bit(uint value)
		{
			value |= (value >> 1);
			value |= (value >> 2);
			value |= (value >> 4);
			value |= (value >> 8);
			value |= (value >> 16);
			return value & ~(value >> 1);
		}
		/// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
		[System.CLSCompliant(false)]
		public static ulong MostSignificant1Bit(ulong value)
		{
			value |= (value >> 1);
			value |= (value >> 2);
			value |= (value >> 4);
			value |= (value >> 8);
			value |= (value >> 16);
			value |= (value >> 32);
			return value & ~(value >> 1);
		}
	}
}
