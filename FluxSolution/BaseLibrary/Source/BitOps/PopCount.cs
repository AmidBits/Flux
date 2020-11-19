namespace Flux
{
	public static partial class BitOps
	{
		// http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetNaive
		// http://aggregate.org/MAGIC/#Population%20Count%20(Ones%20Count)
		// http://aggregate.org/MAGIC/
		// http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan

		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		public static int PopCount(System.Numerics.BigInteger value)
		{
			if (value > 255 && value.ToByteArray() is var byteArray)
			{
				var count = 0;
				for (var index = byteArray.Length - 1; index >= 0; index--)
					count += /*System.Numerics.BitOperations.*/PopCount(byteArray[index]);
				return count;
			}
			else if (value >= 0)
				return /*System.Numerics.BitOperations.*/PopCount((uint)value);

			return -1;
		}

		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int PopCount(int value)
			=> PopCount(unchecked((uint)value));

		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int PopCount(long value)
			=> PopCount(unchecked((ulong)value));

		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		[System.CLSCompliant(false)]
		public static int PopCount(uint value)
		{
			unchecked
			{
				value -= ((value >> 1) & 0x55555555);
				value = (value & 0x33333333) + ((value >> 2) & 0x33333333);
				return (int)((((value + (value >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);
			}
		}
		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		[System.CLSCompliant(false)]
		public static int PopCount(ulong value)
		{
			unchecked
			{
				value -= ((value >> 1) & 0x5555555555555555UL);
				value = (value & 0x3333333333333333UL) + ((value >> 2) & 0x3333333333333333UL);
				return (int)((((value + (value >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
			}
		}
	}
}
