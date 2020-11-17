using System.Linq;

namespace Flux
{
	public static partial class Maths
	{
		private static System.Collections.Generic.IReadOnlyList<int>? m_byteBit1Count;
		/// <summary></summary>
		public static System.Collections.Generic.IReadOnlyList<int> ByteBit1Count
			=> m_byteBit1Count ??= System.Linq.Enumerable.Range(0, 256).Select(n => Bit1Count(n)).ToList();

		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		/// <remarks>The implementation is relatively slow for huge BigInteger values.</remarks>
		/// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetNaive"/>
		/// <seealso cref="http://aggregate.org/MAGIC/#Population%20Count%20(Ones%20Count)"/>
		public static int Bit1Count(System.Numerics.BigInteger value)
		{
			if (value >= 0 && value <= 255)
				return ByteBit1Count[(int)value];

			var byteArray = value.ToByteArrayEx(out var msbIndex, out var msbValue);

			var count = ByteBit1Count[msbValue];

			for (var index = msbIndex - 1; index >= 0; index--)
				count += ByteBit1Count[byteArray[index]];

			return count;
		}

		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		/// <remarks>The implementation is relatively fast.</remarks>
		/// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetNaive"/>
		/// <seealso cref="http://aggregate.org/MAGIC/#Population%20Count%20(Ones%20Count)"/>
		public static int Bit1Count(int value)
			=> System.Numerics.BitOperations.PopCount(unchecked((uint)value));
		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		/// <remarks>The implementation is relatively fast.</remarks>
		/// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetNaive"/>
		/// <seealso cref="http://aggregate.org/MAGIC/#Population%20Count%20(Ones%20Count)"/>
		public static int Bit1Count(long value)
			=> System.Numerics.BitOperations.PopCount(unchecked((ulong)value));

		#region Code retained for posterity.
		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		/// <remarks>The implementation is relatively fast.</remarks>
		/// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetNaive"/>
		/// <seealso cref="http://aggregate.org/MAGIC/#Population%20Count%20(Ones%20Count)"/>
		//[System.CLSCompliant(false)]
		//public static int Bit1Count(uint value)
		//{
		//  unchecked
		//  {
		//    value -= ((value >> 1) & 0x55555555);
		//    value = (value & 0x33333333) + ((value >> 2) & 0x33333333);
		//    return (int)((((value + (value >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);
		//  }
		//}
		/// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
		/// <remarks>The implementation is relatively fast.</remarks>
		/// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetNaive"/>
		/// <seealso cref="http://aggregate.org/MAGIC/#Population%20Count%20(Ones%20Count)"/>
		//[System.CLSCompliant(false)]
		//public static int Bit1Count(ulong value)
		//{
		//  unchecked
		//  {
		//    value -= ((value >> 1) & 0x5555555555555555UL);
		//    value = (value & 0x3333333333333333UL) + ((value >> 2) & 0x3333333333333333UL);
		//    return (int)((((value + (value >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
		//  }
		//}
		#endregion Code retained for posterity.
	}
}
