namespace Flux
{
	public static partial class BitOps
	{
		[System.CLSCompliant(false)]
		public static System.Numerics.BigInteger RotateLeft(System.Numerics.BigInteger value, int count)
			=> (value << 1) | (value >> (BitLength(value) - count));

		[System.CLSCompliant(false)]
		public static void RotateLeft1(ref uint value)
			=> value = (value << 1) | (value >> 31);
		[System.CLSCompliant(false)]
		public static uint RotateLeft1(uint value)
			=> (value << 1) | (value >> 31);
		[System.CLSCompliant(false)]
		public static void RotateLeft(ref uint value, int count)
			=> value = (value << count) | (value >> (32 - count));
		[System.CLSCompliant(false)]
		public static uint RotateLeft(uint value, int count)
			=> (value << count) | (value >> (32 - count));

		[System.CLSCompliant(false)]
		public static void RotateLeft1(ref ulong value)
			=> value = (value << 1) | (value >> 63);
		[System.CLSCompliant(false)]
		public static ulong RotateLeft1(ulong value)
			=> (value << 1) | (value >> 63);
		[System.CLSCompliant(false)]
		public static void RotateLeft(ref ulong value, int count)
			=> value = (value << count) | (value >> (64 - count));
		[System.CLSCompliant(false)]
		public static ulong RotateLeft(ulong value, int count)
			=> (value << count) | (value >> (64 - count));
	}
}
