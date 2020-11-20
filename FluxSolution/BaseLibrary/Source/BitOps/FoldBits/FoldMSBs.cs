namespace Flux
{
	public static partial class BitOps
	{
		// <seealso cref="http://aggregate.org/MAGIC/"/>
		// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		/// <returns>Returns all ones from the LSB up.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static System.Numerics.BigInteger FoldMSB(System.Numerics.BigInteger value)
			=> (System.Numerics.BigInteger.One << BitLength(value)) - 1;

		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		/// <returns>Returns all ones from the LSB up.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int FoldMSB(int value)
			=> unchecked((int)FoldMSB((uint)value));
		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		/// <returns>Returns all ones from the LSB up.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long FoldMSB(long value)
			=> unchecked((long)FoldMSB((ulong)value));

		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
		/// <returns>Returns all ones from the LSB up.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[System.CLSCompliant(false)]
		public static uint FoldMSB(uint value)
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
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[System.CLSCompliant(false)]
		public static ulong FoldMSB(ulong value)
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
