
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
			if (value == 0)
				return 32;

			var count = 0;

			if (value > 0)
			{
				if ((value & 0xFFFF0000) == 0)
				{
					count += 16;
					value <<= 16;
				}

				if ((value & 0xFF000000) == 0)
				{
					count += 8;
					value <<= 8;
				}

				if ((value & 0xF0000000) == 0)
				{
					count += 4;
					value <<= 4;
				}

				if ((value & 0xC0000000) == 0)
				{
					count += 2;
					value <<= 2;
				}

				if ((value & 0x80000000) == 0)
					count += 1;
			}

			return count;
		}
		/// <summary>Often called 'Count Leading Zeros' (clz), counts the number of zero bits preceding the most significant one bit.</summary>
		[System.CLSCompliant(false)]
		public static int LeadingZeroCount(ulong value)
		{
			if (value == 0)
				return 64;

			var count = 0;

			if (value > 0)
			{
				if ((value & 0xFFFFFFFF00000000) == 0)
				{
					count += 32;
					value <<= 32;
				}

				if ((value & 0xFFFF000000000000) == 0)
				{
					count += 16;
					value <<= 16;
				}

				if ((value & 0xFF00000000000000) == 0)
				{
					count += 8;
					value <<= 8;
				}

				if ((value & 0xF000000000000000) == 0)
				{
					count += 4;
					value <<= 4;
				}

				if ((value & 0xC000000000000000) == 0)
				{
					count += 2;
					value <<= 2;
				}

				if ((value & 0x8000000000000000) == 0)
					count += 1;
			}

			return count;
		}

		// https://en.wikipedia.org/wiki/Find_first_set#CTZ

		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(System.Numerics.BigInteger value)
			=> value > 0 ? PopCount((value & -value) - 1) : -1;

		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		public static int TrailingZeroCount(int value)
			=> PopCount((value & -value) - 1);
		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		public static int TrailingZeroCount(long value)
			=> PopCount((value & -value) - 1);

		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.CLSCompliant(false)]
		public static int TrailingZeroCount(uint value)
			=> PopCount((value & ((~value) + 1)) - 1);
		/// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
		[System.CLSCompliant(false)]
		public static int TrailingZeroCount(ulong value)
			=> PopCount((value & ((~value) + 1)) - 1);
	}
}
