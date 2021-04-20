namespace Flux
{
	public static partial class BitOps
	{
		[System.CLSCompliant(false)]
		public static System.Numerics.BigInteger RotateRight(System.Numerics.BigInteger value, int count)
			=> (value << (BitLength(value) - count)) | (value >> count);

		[System.CLSCompliant(false)]
		public static void RotateRight1(ref uint value)
			=> value = (value << 31) | (value >> 1);
		[System.CLSCompliant(false)]
		public static uint RotateRight1(uint value)
			=> (value << 31) | (value >> 1);
		[System.CLSCompliant(false)]
		public static void RotateRight(ref uint value, int count)
			=> value = (value << (32 - count)) | (value >> count);
		[System.CLSCompliant(false)]
		public static uint RotateRight(uint value, int count)
			=> (value << (32 - count)) | (value >> count);

		[System.CLSCompliant(false)]
		public static void RotateRight1(ref ulong value)
			=> value = (value << 63) | (value >> 1);
		[System.CLSCompliant(false)]
		public static ulong RotateRight1(ulong value)
			=> (value << 63) | (value >> 1);
		[System.CLSCompliant(false)]
		public static void RotateRight(ref ulong value, int count)
			=> value = (value << (64 - count)) | (value >> count);
		[System.CLSCompliant(false)]
		public static ulong RotateRight(ulong value, int count)
			=> (value << (64 - count)) | (value >> count);
	}
}
