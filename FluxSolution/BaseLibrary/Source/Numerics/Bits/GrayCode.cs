//namespace Flux
//{
//	public static partial class BitOps
//	{
//		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		public static System.Numerics.BigInteger BinaryToGray(System.Numerics.BigInteger value)
//			=> value >= 0 ? value ^ (value >> 1) : throw new System.ArgumentOutOfRangeException(nameof(value));

//		/// <summary>Converts from an unsigned binary number to reflected binary Gray code.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		public static int BinaryToGray(int value)
//			=> unchecked((int)BinaryToGray((uint)value));
//		/// <summary>Converts from an unsigned binary number to reflected binary Gray code.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		public static long BinaryToGray(long value)
//			=> unchecked((long)BinaryToGray((ulong)value));

//		/// <summary>Converts from an unsigned binary number to reflected binary Gray code.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		[System.CLSCompliant(false)]
//		public static uint BinaryToGray(uint value)
//			=> value ^ (value >> 1);
//		/// <summary>Converts from an unsigned binary number to reflected binary Gray code.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		[System.CLSCompliant(false)]
//		public static ulong BinaryToGray(ulong value)
//			=> value ^ (value >> 1);

//		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		public static System.Numerics.BigInteger GrayToBinary(System.Numerics.BigInteger value)
//		{
//			if (value > 0)
//			{
//				var mask = value >> 1;

//				while (mask != 0)
//				{
//					value ^= mask;
//					mask >>= 1;
//				}
//			}

//			return value;
//		}

//		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		public static int GrayToBinary(int value)
//			=> unchecked((int)GrayToBinary((uint)value));
//		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		public static long GrayToBinary(long value)
//			=> unchecked((long)GrayToBinary((ulong)value));

//		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		[System.CLSCompliant(false)]
//		public static uint GrayToBinary(uint value)
//		{
//			value ^= (value >> 16);
//			value ^= (value >> 8);
//			value ^= (value >> 4);
//			value ^= (value >> 2);
//			value ^= (value >> 1);

//			return value;
//		}
//		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
//		[System.CLSCompliant(false)]
//		public static ulong GrayToBinary(ulong value)
//		{
//			value ^= (value >> 32);
//			value ^= (value >> 16);
//			value ^= (value >> 8);
//			value ^= (value >> 4);
//			value ^= (value >> 2);
//			value ^= (value >> 1);

//			return value;
//		}
//	}
//}