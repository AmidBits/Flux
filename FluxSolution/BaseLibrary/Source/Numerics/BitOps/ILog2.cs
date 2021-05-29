namespace Flux.Numerics
{
	// https://en.wikipedia.org/wiki/Binary_logarithm
	// http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious
	// http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer

	public static class ILog2
	{
		public static readonly byte[] ByteTable = new byte[] { 0, 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };

		public static int ByByteTable(int value)
			=> ByByteTable(unchecked((uint)value));
		public static int ByByteTable(long value)
			=> ByByteTable(unchecked((ulong)value));

		[System.CLSCompliant(false)]
		public static int ByByteTable(uint value)
			=> value switch
			{
				> 0x00FFFFFF => 0x18 + ByteTable[value >> 0x18],
				> 0x0000FFFF => 0x10 + ByteTable[value >> 0x10],
				> 0x000000FF => 0x08 + ByteTable[value >> 0x08],
				> 0x00000000 => 0x00 + ByteTable[value >> 0x00],
				_ => 0,
			};

		[System.CLSCompliant(false)]
		public static int ByByteTable(ulong value)
			=> value switch
			{
				> 0x00FFFFFFFFFFFFFFUL => 0x38 + ByteTable[value >> 0x38],
				> 0x0000FFFFFFFFFFFFUL => 0x30 + ByteTable[value >> 0x30],
				> 0x000000FFFFFFFFFFUL => 0x28 + ByteTable[value >> 0x28],
				> 0x00000000FFFFFFFFUL => 0x20 + ByteTable[value >> 0x20],
				_ => ByByteTable((uint)(value & 0xFFFFFFFFUL)),
			};

		/// <summary>Contains the bit positions.</summary>
		public static readonly byte[] DeBruijnTable = new byte[] { 0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30, 8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31 };

		public static int ByDeBruijnTable(int value)
		{
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return DeBruijnTable[(uint)(value * 0x07C4ACDDU) >> 27];
		}
	}
}
