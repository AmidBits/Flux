namespace Flux
{
	public static partial class BitOps
	{
		/// <summary>Create a bit mask with the specified number of LSBs (Least Significant Bits) set to 1.</summary>
		public static int CreateLsbMaskInt32(int bitCount)
			=> bitCount >= 0 ? (int)CreateLsbMaskUInt32(bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		/// <summary>Create a bit mask with the specified number of LSBs (Least Significant Bits) set to 1.</summary>
		public static long CreateLsbMaskInt64(int bitCount)
			=> bitCount >= 0 ? (long)CreateLsbMaskUInt64(bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));

		/// <summary>Create a bit mask with the specified number of LSBs (Least Significant Bits) set to 1.</summary>
		[System.CLSCompliant(false)]
		public static uint CreateLsbMaskUInt32(int bitCount)
			=> bitCount < 32 ? uint.MaxValue >> (32 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		/// <summary>Create a bit mask with the specified number of LSBs (Least Significant Bits) set to 1.</summary>
		[System.CLSCompliant(false)]
		public static ulong CreateLsbMaskUInt64(int bitCount)
			=> bitCount < 64 ? ulong.MaxValue >> (64 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));

		/// <summary>Create a bit mask with the specified number of MSBs (Most Significant Bits) set to 1.</summary>
		public static int CreateMsbMaskInt32(int bitCount)
			=> bitCount >= 0 ? (int)CreateMsbMaskUInt32(bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		/// <summary>Create a bit mask with the specified number of MSBs (Most Significant Bits) set to 1.</summary>
		public static long CreateMsbMaskInt64(int bitCount)
			=> bitCount >= 0 ? (long)CreateMsbMaskUInt64(bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));

		/// <summary>Create a bit mask with the specified number of MSBs (Most Significant Bits) set to 1.</summary>
		[System.CLSCompliant(false)]
		public static uint CreateMsbMaskUInt32(int bitCount)
			=> bitCount >= 0 && bitCount < 32 ? uint.MaxValue << (32 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		/// <summary>Create a bit mask with the specified number of MSBs (Most Significant Bits) set to 1.</summary>
		[System.CLSCompliant(false)]
		public static ulong CreateMsbMaskUInt64(int bitCount)
			=> bitCount >= 0 && bitCount < 64 ? ulong.MaxValue << (64 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
	}
}
