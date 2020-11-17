namespace Flux
{
	public static partial class Maths
	{
		public static int LeastSignificantBitMaskInt32(int bitCount)
			=> unchecked((int)(uint.MaxValue >> (32 - bitCount)));
		public static long LeastSignificantBitMaskInt64(int bitCount)
			=> unchecked((long)(ulong.MaxValue >> (64 - bitCount)));

		[System.CLSCompliant(false)]
		public static uint LeastSignificantBitMaskUInt32(int bitCount)
			=> bitCount < 32 ? uint.MaxValue >> (32 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		[System.CLSCompliant(false)]
		public static ulong LeastSignificantBitMaskUInt64(int bitCount)
			=> bitCount < 64 ? ulong.MaxValue >> (64 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));

		public static int MostSignificantBitMaskInt32(int bitCount)
			=> unchecked((int)(uint.MaxValue << (32 - bitCount)));
		public static long MostSignificantBitMaskInt64(int bitCount)
			=> unchecked((long)(ulong.MaxValue << (64 - bitCount)));

		[System.CLSCompliant(false)]
		public static uint MostSignificantBitMaskUInt32(int bitCount)
			=> bitCount < 32 ? uint.MaxValue << (32 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
		[System.CLSCompliant(false)]
		public static ulong MostSignificantBitMaskUInt64(int bitCount)
			=> bitCount < 64 ? ulong.MaxValue << (64 - bitCount) : throw new System.ArgumentOutOfRangeException(nameof(bitCount));
	}
}
