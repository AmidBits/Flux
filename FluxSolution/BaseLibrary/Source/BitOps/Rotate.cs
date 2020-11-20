namespace Flux
{
	public static partial class BitOps
	{
		[System.CLSCompliant(false)]
		public static System.Numerics.BigInteger RotateLeft(System.Numerics.BigInteger value, int bitCount)
			=> (value << 1) | (value >> (BitLength(value) - bitCount));

		[System.CLSCompliant(false)]
		public static uint RotateLeft(uint value, int bitCount)
#if NETCOREAPP
			=> System.Numerics.BitOperations.RotateLeft(value, bitCount);
#else
			=> (value << bits) | (value >> (32 - bits));
#endif

		[System.CLSCompliant(false)]
		public static ulong RotateLeft(ulong value, int bitCount)
#if NETCOREAPP
			=> System.Numerics.BitOperations.RotateLeft(value, bitCount);
#else
			=> (value << bits) | (value >> (64 - bits));
#endif

		[System.CLSCompliant(false)]
		public static System.Numerics.BigInteger RotateRight(System.Numerics.BigInteger value, int bitCount)
			=> (value << (BitLength(value) - bitCount)) | (value >> bitCount);

		[System.CLSCompliant(false)]
		public static uint RotateRight(uint value, int bitCount)
#if NETCOREAPP
			=> System.Numerics.BitOperations.RotateRight(value, bitCount);
#else
			=> (value << (32 - bits)) | (value >> bits);
#endif

		[System.CLSCompliant(false)]
		public static ulong RotateRight(ulong value, int bitCount)
#if NETCOREAPP
			=> System.Numerics.BitOperations.RotateRight(value, bitCount);
#else
			=> (value << (64 - bits)) | (value >> bits);
#endif
	}
}
