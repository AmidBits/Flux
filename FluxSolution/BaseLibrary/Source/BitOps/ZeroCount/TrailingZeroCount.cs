
namespace Flux
{
	public static partial class BitOps
	{
		// http://aggregate.org/MAGIC/
		// http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan
		// https://en.wikipedia.org/wiki/Bit-length

		// https://en.wikipedia.org/wiki/Find_first_set#CTZ

		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(System.Numerics.BigInteger value)
			=> value > 0 ? PopCount((value & -value) - 1) : -1;

		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(int value)
			=> PopCount((value & -value) - 1);
		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(long value)
			=> PopCount((value & -value) - 1);

		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[System.CLSCompliant(false)]
		public static int TrailingZeroCount(uint value)
			=> PopCount((value & ((~value) + 1)) - 1);
		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[System.CLSCompliant(false)]
		public static int TrailingZeroCount(ulong value)
			=> PopCount((value & ((~value) + 1)) - 1);
	}
}
