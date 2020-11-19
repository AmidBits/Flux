namespace Flux
{
	public static partial class BitOps
	{
		public static int LeastSignificantBitMaskInt32(int bitCount)
			=> bitCount >= 0 ? (int)LeastSignificantBitMaskUInt32(bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		public static long LeastSignificantBitMaskInt64(int bitCount)
			=> bitCount >= 0 ? (long)LeastSignificantBitMaskUInt64(bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));

		[System.CLSCompliant(false)]
		public static uint LeastSignificantBitMaskUInt32(int bitCount)
			=> bitCount < 32 ? uint.MaxValue >> (32 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		[System.CLSCompliant(false)]
		public static ulong LeastSignificantBitMaskUInt64(int bitCount)
			=> bitCount < 64 ? ulong.MaxValue >> (64 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));

		public static int MostSignificantBitMaskInt32(int bitCount)
			=> bitCount >= 0 ? (int)MostSignificantBitMaskUInt32(bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		public static long MostSignificantBitMaskInt64(int bitCount)
			=> bitCount >= 0 ? (long)MostSignificantBitMaskUInt64(bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));

		[System.CLSCompliant(false)]
		public static uint MostSignificantBitMaskUInt32(int bitCount)
			=> bitCount >= 0 && bitCount < 32 ? uint.MaxValue << (32 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		[System.CLSCompliant(false)]
		public static ulong MostSignificantBitMaskUInt64(int bitCount)
			=> bitCount >= 0 && bitCount < 64 ? ulong.MaxValue << (64 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
	}
}
