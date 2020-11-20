namespace Flux
{
	public static partial class BitOps
	{
		// https://en.wikipedia.org/wiki/Bit-length
		// http://aggregate.org/MAGIC/
		// http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan

		//private static System.Collections.Generic.IReadOnlyList<int>? m_byteBitLength;
		///// <summary></summary>
		//public static System.Collections.Generic.IReadOnlyList<int> ByteBitLength
		//  => m_byteBitLength ??= System.Linq.Enumerable.Range(0, 256).Select(n => BitLength(n)).ToList();

		/// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
		/// <remarks>BitLength(value) is equal to 1 + Log2(value).</remarks>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int BitLength(System.Numerics.BigInteger value)
		//		=> value > 0 ? Log2(value) + 1 : value == 0 ? 1 : 0;
		{
			if (value >= 0)
				return Log2(value) + 1;
			else
			{
				value.ToByteArrayEx(out var msbIndex, out var msbValue);

				return Log2(Log2(msbValue) + 8 * msbIndex);
			}
		}

		/// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int BitLength(int value)
			=> BitLength(unchecked((uint)value));
		/// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int BitLength(long value)
			=> BitLength(unchecked((ulong)value));

		/// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[System.CLSCompliant(false)]
		public static int BitLength(uint value)
		//=> value < 0 ? 32 : /*System.Numerics.BitOperations.*/Log2(value) + 1;
		{
			var count = 0;

			if (value > 0)
			{
				unchecked
				{
					if (value > 0x0000FFFF)
					{
						count += 16;
						value >>= 16;
					}

					if (value > 0x000000FF)
					{
						count += 8;
						value >>= 8;
					}

					if (value > 0x0000000F)
					{
						count += 4;
						value >>= 4;
					}

					if (value > 0x00000003)
					{
						count += 2;
						value >>= 2;
					}

					if (value > 0x00000001)
					{
						count++;
						value >>= 1;
					}

					if (value > 0x00000000)
						count++;
				}
			}

			return count;
		}
		/// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
		/// <remarks>The implementation is relatively fast.</remarks>
		/// <see cref="https://en.wikipedia.org/wiki/Bit-length"/>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[System.CLSCompliant(false)]
		public static int BitLength(ulong value)
		//=> value < 0 ? 64 : /*System.Numerics.BitOperations.*/Log2(value) + 1;
		{
			var count = 0;

			if (value > 0)
			{
				unchecked
				{
					if (value > 0x00000000FFFFFFFF)
					{
						count += 32;
						value >>= 32;
					}

					if (value > 0x0000FFFF)
					{
						count += 16;
						value >>= 16;
					}

					if (value > 0x000000FF)
					{
						count += 8;
						value >>= 8;
					}

					if (value > 0x0000000F)
					{
						count += 4;
						value >>= 4;
					}

					if (value > 0x00000003)
					{
						count += 2;
						value >>= 2;
					}

					if (value > 0x00000001)
					{
						count++;
						value >>= 1;
					}

					if (value > 0x00000000)
						count++;
				}
			}

			return count;
		}
	}
}
