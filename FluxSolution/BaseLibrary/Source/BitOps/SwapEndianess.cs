namespace Flux
{
	public static partial class BitOps
	{
		public static short SwapEndianness(short value)
			=> unchecked((short)SwapEndianness((ushort)value));
		public static int SwapEndianness(int value)
			=> unchecked((int)SwapEndianness((uint)value));
		public static long SwapEndianness(long value)
			=> unchecked((long)SwapEndianness((ulong) value));

		[System.CLSCompliant(false)]
		public static ushort SwapEndianness(ushort value)
		{
			var b1 = (value >> 0) & 0xff;
			var b2 = (value >> 8) & 0xff;
			return (ushort)(b1 << 8 | b2 << 0);
		}
		[System.CLSCompliant(false)]
		public static uint SwapEndianness(uint value)
		{
			var b1 = (value >> 0) & 0xff;
			var b2 = (value >> 8) & 0xff;
			var b3 = (value >> 16) & 0xff;
			var b4 = (value >> 24) & 0xff;
			return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
		}
		[System.CLSCompliant(false)]
		public static ulong SwapEndianness(ulong value)
		{
			var b1 = (value >> 0) & 0xff;
			var b2 = (value >> 8) & 0xff;
			var b3 = (value >> 16) & 0xff;
			var b4 = (value >> 24) & 0xff;
			var b5 = (value >> 32) & 0xff;
			var b6 = (value >> 40) & 0xff;
			var b7 = (value >> 48) & 0xff;
			var b8 = (value >> 56) & 0xff;
			return b1 << 56 | b2 << 48 | b3 << 40 | b4 << 32 | b5 << 24 | b6 << 16 | b7 << 8 | b8 << 0;
		}
	}
}
