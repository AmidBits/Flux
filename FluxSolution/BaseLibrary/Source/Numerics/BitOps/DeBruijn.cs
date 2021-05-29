namespace Flux.Numerics
{
	// https://en.wikipedia.org/wiki/Binary_logarithm
	// http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious
	// http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer

	public static partial class BitOps
	{
		public static readonly byte[] DeBruijnTableLog = new byte[] { 0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30, 8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31 };

		[System.CLSCompliant(false)]
		public static int DeBruijnILog2(uint number)
		{
			BitOps.FoldRight(ref number);

			return DeBruijnTableLog[(number * 0x07C4ACDDU) >> 27];
		}

		public static readonly byte[] DeBruijnTableLs1bIndex = new byte[] { 0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9 };

		[System.CLSCompliant(false)]
		public static int DeBruijnLs1bIndex(uint number)
		{
			number = BitOps.LeastSignificant1Bit(number);

			return DeBruijnTableLs1bIndex[(number * 0x077CB531U) >> 27];
		}
		[System.CLSCompliant(false)]
		public static int DeBruijnLs1bIndex(ulong number)
			=> number > uint.MaxValue ? 32 + DeBruijnLs1bIndex((uint)(number >> 32)) : DeBruijnLs1bIndex((uint)number);

		public static readonly byte[] DeBruijnTableMs1bIndex = new byte[] { 0, 1, 16, 2, 29, 17, 3, 22, 30, 20, 18, 11, 13, 4, 7, 23, 31, 15, 28, 21, 19, 10, 12, 6, 14, 27, 9, 5, 26, 8, 25, 24 };

		[System.CLSCompliant(false)]
		public static int DeBruijnMs1bIndex(uint number)
		{
			number = BitOps.MostSignificant1Bit(number);

			return DeBruijnTableMs1bIndex[(number * 0x06EB14F9) >> 27];
		}
		[System.CLSCompliant(false)]
		public static int DeBruijnMs1bIndex(ulong number)
			=> number > uint.MaxValue ? 32 + DeBruijnMs1bIndex((uint)(number >> 32)) : DeBruijnMs1bIndex((uint)number);
	}
}
