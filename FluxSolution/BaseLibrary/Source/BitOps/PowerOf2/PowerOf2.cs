namespace Flux
{
	public static partial class BitOps
	{
		// <seealso cref="http://aggregate.org/MAGIC/"/>
		// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

		/// <summary>Returns the power of 2 for the specified number.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static System.Numerics.BigInteger PowerOf2(System.Numerics.BigInteger number)
			=> number > 2 ? System.Numerics.BigInteger.One << Log2(number) : number >= 0 ? number : 0;

		/// <summary>Returns the power of 2 for the specified number.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int PowerOf2(int number)
			=> number > 2 ? 1 << /*System.Numerics.BitOperations.*/Log2(unchecked((uint)number)) : number >= 0 ? number : int.MinValue;

		/// <summary>Returns the power of 2 for the specified number.</summary>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long PowerOf2(long number)
			=> number > 2 ? 1L << /*System.Numerics.BitOperations.*/Log2(unchecked((ulong)number)) : number >= 0 ? number : long.MinValue;
	}
}
