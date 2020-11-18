using System.Linq;

// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
	public static partial class Maths
	{
		private static System.Collections.Generic.IReadOnlyList<byte>? m_byteFoldMostSignificantBits;
		/// <summary></summary>
		public static System.Collections.Generic.IReadOnlyList<byte> ByteFoldMostSignificantBits
			=> m_byteFoldMostSignificantBits ??= System.Linq.Enumerable.Range(0, 256).Select(n => (byte)FoldMostSignificantBits(n)).ToList();

		/// <summary>"Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		public static System.Numerics.BigInteger FoldMostSignificantBits(System.Numerics.BigInteger value)
			=> (System.Numerics.BigInteger.One << BitLength(value)) - 1;

		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		public static int FoldMostSignificantBits(int value)
			=> unchecked((int)FoldMostSignificantBits((uint)value));
		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		public static long FoldMostSignificantBits(long value)
			=> unchecked((long)FoldMostSignificantBits((ulong)value));

		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		[System.CLSCompliant(false)]
		public static byte FoldMostSignificantBits(byte value)
		{
			value |= (byte)(value << 1);
			value |= (byte)(value << 2);
			value |= (byte)(value << 4);

			return value;
		}
		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		[System.CLSCompliant(false)]
		public static ushort FoldMostSignificantBits(ushort value)
		{
			value |= (ushort)(value << 1);
			value |= (ushort)(value << 2);
			value |= (ushort)(value << 4);
			value |= (ushort)(value << 8);

			return value;
		}
		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		[System.CLSCompliant(false)]
		public static uint FoldMostSignificantBits(uint value)
		{
			value |= (value << 1);
			value |= (value << 2);
			value |= (value << 4);
			value |= (value << 8);
			value |= (value << 16);

			return value;
		}
		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
		/// <returns>Returns all ones from the MSB down.</returns>
		[System.CLSCompliant(false)]
		public static ulong FoldMostSignificantBits(ulong value)
		{
			value |= (value << 1);
			value |= (value << 2);
			value |= (value << 4);
			value |= (value << 8);
			value |= (value << 16);
			value |= (value << 32);

			return value;
		}
	}
}
