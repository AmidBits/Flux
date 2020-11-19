namespace Flux
{
	public static partial class BitOps
	{
		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		public static System.Numerics.BigInteger BinaryToGray(System.Numerics.BigInteger value)
			=> value >= 0 ? value ^ (value >> 1) : throw new System.ArgumentOutOfRangeException(nameof(value));

		/// <summary>Converts from an unsigned binary number to reflected binary Gray code.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		public static int BinaryToGray(int value)
			=> unchecked((int)BinaryToGray((uint)value));
		/// <summary>Converts from an unsigned binary number to reflected binary Gray code.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		public static long BinaryToGray(long value)
			=> unchecked((long)BinaryToGray((ulong)value));

		/// <summary>Converts from an unsigned binary number to reflected binary Gray code.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		[System.CLSCompliant(false)]
		public static uint BinaryToGray(uint value)
			=> value ^ (value >> 1);
		/// <summary>Converts from an unsigned binary number to reflected binary Gray code.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		[System.CLSCompliant(false)]
		public static ulong BinaryToGray(ulong value)
			=> value ^ (value >> 1);

		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		public static System.Numerics.BigInteger GrayToBinary(System.Numerics.BigInteger value)
		{
			if (value > 0)
			{
				var mask = value >> 1;

				while (mask != 0)
				{
					value ^= mask;
					mask >>= 1;
				}
			}

			return value;
		}

		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		public static int GrayToBinary(int value)
			=> unchecked((int)GrayToBinary((uint)value));
		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		public static long GrayToBinary(long value)
			=> unchecked((long)GrayToBinary((ulong)value));

		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		[System.CLSCompliant(false)]
		public static uint GrayToBinary(uint value)
		{
			value ^= (value >> 16);
			value ^= (value >> 8);
			value ^= (value >> 4);
			value ^= (value >> 2);
			value ^= (value >> 1);

			return value;
		}
		/// <summary>Converts from reflected binary gray code number to a binary number.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Gray_code"/>
		[System.CLSCompliant(false)]
		public static ulong GrayToBinary(ulong value)
		{
			value ^= (value >> 32);
			value ^= (value >> 16);
			value ^= (value >> 8);
			value ^= (value >> 4);
			value ^= (value >> 2);
			value ^= (value >> 1);

			return value;
		}

		[System.CLSCompliant(false)]
		public static void BinaryToGray(uint radix, uint value, uint[] gray)
		{
			if (gray is null) throw new System.ArgumentNullException(nameof(gray));

			var baseN = new uint[gray.Length]; // Stores the ordinary base-N number, one digit per entry

			for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
			{
				baseN[index] = value % radix;

				value /= radix;
			}

			var shift = 0U; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

			for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
			{
				gray[index] = (baseN[index] + shift) % radix;

				shift = shift + radix - gray[index]; // Subtract from base so shift is positive
			}
		}
	}
}
