namespace Flux
{
	public static partial class BitOps
	{
		// <seealso cref="http://aggregate.org/MAGIC/"/>
		// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		/// <returns>Returns all ones from the LSB up.</returns>
		public static System.Numerics.BigInteger FoldLeft(System.Numerics.BigInteger value)
		{
			var level = BitLength(value);
			for (var shift = 1; shift < level; shift <<= 1)
				value |= value << shift;
			return value;
		}

		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		/// <returns>Returns all ones from the LSB up.</returns>
		public static int FoldLeft(int value)
			=> unchecked((int)FoldLeft((uint)value));
		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		/// <returns>Returns all ones from the LSB up.</returns>
		public static long FoldLeft(long value)
			=> unchecked((long)FoldLeft((ulong)value));

		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		[System.CLSCompliant(false)]
		public static void FoldLeft(ref uint value)
		{
			value |= (value << 1);
			value |= (value << 2);
			value |= (value << 4);
			value |= (value << 8);
			value |= (value << 16);
		}
		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		/// <returns>Returns all ones from the LSB up.</returns>
		[System.CLSCompliant(false)]
		public static uint FoldLeft(uint value)
		{
			value |= (value << 1);
			value |= (value << 2);
			value |= (value << 4);
			value |= (value << 8);
			value |= (value << 16);
			return value;
		}
		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		/// <returns>Returns all ones from the LSB up.</returns>
		[System.CLSCompliant(false)]
		public static ulong FoldLeft(ulong value)
		{
			value |= (value << 1);
			value |= (value << 2);
			value |= (value << 4);
			value |= (value << 8);
			value |= (value << 16);
			value |= (value << 32);
			return value;
		}
	}
}
