namespace Flux
{
	public static partial class Maths
	{
		/// <summary>Returns the integer square root of the specified number, using Heron's method.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Integer_square_root"/>
		/// <seealso cref="https://en.wikipedia.org/wiki/Methods_of_computing_square_roots#Babylonian_method"/>
		public static System.Numerics.BigInteger ISqrt(System.Numerics.BigInteger number)
		{
			if (number == 0)
				return 0;

			if (number > 0)
			{
				var bitLength = Numerics.BitOps.BitLength(number);

				var root = System.Numerics.BigInteger.One << (bitLength >> 1);

				while (!IsSqrt(number, root))
				{
					root += number / root;
					root >>= 1;
				}

				return root;
			}

			throw new System.ArithmeticException();

			static bool IsSqrt(System.Numerics.BigInteger number, System.Numerics.BigInteger root)
				=> (root * root is var lowerBound) && (lowerBound + root + root + 1 is var upperBound) ? (number >= lowerBound && number < upperBound) : throw new System.Exception();
		}

		/// <summary>Returns the integer square root of the specified number. Provided for a consistent call site, and internally calls System.Math.Floor(System.Math.Sqrt()).</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Integer_square_root"/>
		public static int ISqrt(int value)
			=> System.Convert.ToInt32(System.Math.Floor(System.Math.Sqrt(value)));

		/// <summary>Returns the integer square root of the specified number. Provided for a consistent call site, and internally calls System.Math.Floor(System.Math.Sqrt()).</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Integer_square_root"/>
		public static long ISqrt(long value)
			=> System.Convert.ToInt64(System.Math.Floor(System.Math.Sqrt(value)));
	}
}
