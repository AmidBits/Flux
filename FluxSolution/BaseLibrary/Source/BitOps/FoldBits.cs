//namespace Flux
//{
//	public static partial class BitOps
//	{
//		// <seealso cref="http://aggregate.org/MAGIC/"/>
//		// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

//		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
//		/// <returns>Returns all ones from the LSB up.</returns>
//		public static System.Numerics.BigInteger FoldHigh(System.Numerics.BigInteger value)
//		{
//			var level = BitLength(value);
//			for (var shift = 1; shift < level; shift <<= 1)
//				value |= value << shift;
//			return value;
//		}

//		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
//		/// <returns>Returns all ones from the LSB up.</returns>
//		public static int FoldHigh(int value)
//			=> unchecked((int)FoldHigh((uint)value));
//		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
//		/// <returns>Returns all ones from the LSB up.</returns>
//		public static long FoldHigh(long value)
//			=> unchecked((long)FoldHigh((ulong)value));

//		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
//		/// <returns>Returns all ones from the LSB up.</returns>
//		[System.CLSCompliant(false)]
//		public static uint FoldHigh(uint value)
//		{
//			value |= (value << 1);
//			value |= (value << 2);
//			value |= (value << 4);
//			value |= (value << 8);
//			value |= (value << 16);
//			return value;
//		}
//		/// <summary>Recurively "folds" the lower bits into the upper bits. The process yields a bit vector with the same least significant 1 as the value, but all 1's above it.</summary>
//		/// <returns>Returns all ones from the LSB up.</returns>
//		[System.CLSCompliant(false)]
//		public static ulong FoldHigh(ulong value)
//		{
//			value |= (value << 1);
//			value |= (value << 2);
//			value |= (value << 4);
//			value |= (value << 8);
//			value |= (value << 16);
//			value |= (value << 32);
//			return value;
//		}

//		// <seealso cref="http://aggregate.org/MAGIC/"/>
//		// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

//		/// <summary>"Folds" the upper bits into the lower bits, by taking the most significant 1 bit (MS1B) and OR it with (MS1B - 1). The process yields a bit vector with the same most significant 1 as the value, but all 1's below it.</summary>
//		/// <returns>Returns all ones from the MSB down.</returns>
//		public static System.Numerics.BigInteger FoldLow(System.Numerics.BigInteger value)
//			=> (System.Numerics.BigInteger.One << BitLength(value)) - 1;

//		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
//		/// <returns>Returns all ones from the MSB down.</returns>
//		public static int FoldLow(int value)
//			=> unchecked((int)FoldLow((uint)value));
//		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
//		/// <returns>Returns all ones from the MSB down.</returns>
//		public static long FoldLow(long value)
//			=> unchecked((long)FoldLow((ulong)value));

//		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
//		/// <returns>Returns all ones from the MSB down.</returns>
//		[System.CLSCompliant(false)]
//		public static uint FoldLow(uint value)
//		{
//			value |= (value >> 1);
//			value |= (value >> 2);
//			value |= (value >> 4);
//			value |= (value >> 8);
//			value |= (value >> 16);
//			return value;
//		}
//		/// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it.</summary>
//		/// <returns>Returns all ones from the MSB down.</returns>
//		[System.CLSCompliant(false)]
//		public static ulong FoldLow(ulong value)
//		{
//			value |= (value >> 1);
//			value |= (value >> 2);
//			value |= (value >> 4);
//			value |= (value >> 8);
//			value |= (value >> 16);
//			value |= (value >> 32);
//			return value;
//		}
//	}
//}
