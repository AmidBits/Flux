namespace Flux
{
	public static partial class BitOps
	{
		/// <summary>Create a bit mask with the specified number of MSBs (Most Significant Bits) set to 1, and the specified number of LSBs (Least Significant Bits) set to 0.</summary>
		public static System.Numerics.BigInteger CreateMaskLeftBigInteger(int leadingOneCount, int trailingZeroCount)
			=> trailingZeroCount < 0
			? throw new System.ArgumentOutOfRangeException(nameof(trailingZeroCount))
			: CreateMaskRightBigInteger(leadingOneCount) << trailingZeroCount;

		/// <summary>Create a bit mask with the specified number of MSBs (Most Significant Bits) set to 1.</summary>
		public static int CreateMaskLeftInt32(int bit1Count)
			=> (int)CreateMaskLeftUInt32(bit1Count);
		/// <summary>Create a bit mask with the specified number of MSBs (Most Significant Bits) set to 1.</summary>
		public static long CreateMaskLeftInt64(int bit1Count)
			=> (long)CreateMaskLeftUInt64(bit1Count);

		/// <summary>Create a bit mask with the specified number of MSBs (Most Significant Bits) set to 1.</summary>
		[System.CLSCompliant(false)]
		public static uint CreateMaskLeftUInt32(int bit1Count)
			=> bit1Count < 0 || bit1Count >= 32
			? throw new System.ArgumentOutOfRangeException(nameof(bit1Count))
			: uint.MaxValue << (32 - bit1Count);
		/// <summary>Create a bit mask with the specified number of MSBs (Most Significant Bits) set to 1.</summary>
		[System.CLSCompliant(false)]
		public static ulong CreateMaskLeftUInt64(int bit1Count)
			=> bit1Count < 0 || bit1Count >= 64
			? throw new System.ArgumentOutOfRangeException(nameof(bit1Count))
			: ulong.MaxValue << (64 - bit1Count);
	}
}
