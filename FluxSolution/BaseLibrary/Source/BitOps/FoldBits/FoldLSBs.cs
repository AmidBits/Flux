namespace Flux
{
	public static partial class BitOps
	{
		// <seealso cref="http://aggregate.org/MAGIC/"/>
		// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

		/// <summary>"Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static System.Numerics.BigInteger FoldLSB(System.Numerics.BigInteger value)
			=> (System.Numerics.BigInteger.One << BitLength(value)) - 1;

		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int FoldLSB(int value)
			=> unchecked((int)FoldLSB((uint)value));
		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long FoldLSB(long value)
			=> unchecked((long)FoldLSB((ulong)value));

		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[System.CLSCompliant(false)]
		public static uint FoldLSB(uint value)
		{
			value |= (value >> 1);
			value |= (value >> 2);
			value |= (value >> 4);
			value |= (value >> 8);
			value |= (value >> 16);
			return value;
		}
		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[System.CLSCompliant(false)]
		public static ulong FoldLSB(ulong value)
		{
			value |= (value >> 1);
			value |= (value >> 2);
			value |= (value >> 4);
			value |= (value >> 8);
			value |= (value >> 16);
			value |= (value >> 32);
			return value;
		}
	}
}
