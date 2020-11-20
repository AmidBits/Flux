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
					count += PopCount((uint)byteArray[index]);
				return count;
			}
			else if (value >= 0)
				return PopCount((uint)value);

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
#if NETCOREAPP
			=> System.Numerics.BitOperations.PopCount(value);
#else
		{
			unchecked
			{
				value -= ((value >> 1) & 0x55555555U);
				value = (value & 0x33333333U) + ((value >> 2) & 0x33333333U);
				return (int)((((value + (value >> 4)) & 0x0F0F0F0FU) * 0x01010101U) >> 24);
			}
		}
#endif
		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		[System.CLSCompliant(false)]
		public static int PopCount(ulong value)
#if NETCOREAPP
			=> System.Numerics.BitOperations.PopCount(value);
#else
		{
			unchecked
			{
				value -= ((value >> 1) & 0x5555555555555555UL);
				value = (value & 0x3333333333333333UL) + ((value >> 2) & 0x3333333333333333UL);
				return (int)((((value + (value >> 4)) & 0x0F0F0F0F0F0F0F0FUL) * 0x0101010101010101UL) >> 56);
			}
		}
#endif
	}
}
