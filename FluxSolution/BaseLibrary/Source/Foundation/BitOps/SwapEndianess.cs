//namespace Flux
//{
//	public static partial class BitOps
//	{
//		public static short SwapEndianness(short value)
//			=> unchecked((short)SwapEndianness((ushort)value));
//		public static int SwapEndianness(int value)
//			=> unchecked((int)SwapEndianness((uint)value));
//		public static long SwapEndianness(long value)
//			=> unchecked((long)SwapEndianness((ulong)value));

//		[System.CLSCompliant(false)]
//		public static ushort SwapEndianness(ushort value)
//			=> (ushort)((value & 0xff) << 8
//			| (value >> 8) << 0);
//		[System.CLSCompliant(false)]
//		public static uint SwapEndianness(ref uint value)
//			=> ((value & 0xff) << 24)
//			| (((value >> 8) & 0xff) << 16)
//			| (((value >> 16) & 0xff) << 8)
//			| ((value >> 24) & 0xff);
//		[System.CLSCompliant(false)]
//		public static uint SwapEndianness(uint value)
//			=> SwapEndianness(ref value);
//		[System.CLSCompliant(false)]
//		public static ulong SwapEndianness(ref ulong value)
//			=> ((value & 0xff) << 56)
//			| (((value >> 8) & 0xff) << 48)
//			| (((value >> 16) & 0xff) << 40)
//			| (((value >> 24) & 0xff) << 32)
//			| (((value >> 32) & 0xff) << 24)
//			| (((value >> 40) & 0xff) << 16)
//			| (((value >> 48) & 0xff) << 8)
//			| (value >> 56);
//		[System.CLSCompliant(false)]
//		public static ulong SwapEndianness(ulong value)
//			=> SwapEndianness(ref value);
//	}
//}
