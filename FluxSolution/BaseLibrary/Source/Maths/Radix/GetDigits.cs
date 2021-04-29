using System.Linq;

namespace Flux
{
	public static partial class Maths
	{
		/// <summary>Returns the digits of a value.</summary>
		public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDigits(System.Numerics.BigInteger value, int radix)
			=> new System.Collections.Generic.Stack<System.Numerics.BigInteger>(GetDigitsReversed(value, radix));

		/// <summary>Returns the digits of a value.</summary>
		public static System.Collections.Generic.IEnumerable<int> GetDigits(int value, int radix)
			=> new System.Collections.Generic.Stack<int>(GetDigitsReversed(value, radix));
		/// <summary>Returns the digits of a value.</summary>
		public static System.Collections.Generic.IEnumerable<long> GetDigits(long value, int radix)
			=> new System.Collections.Generic.Stack<long>(GetDigitsReversed(value, radix));

		/// <summary>Returns the digits of a value.</summary>
		[System.CLSCompliant(false)]
		public static System.Collections.Generic.IEnumerable<uint> GetDigits(uint value, int radix)
			=> new System.Collections.Generic.Stack<uint>(GetDigitsReversed(value, radix));
		/// <summary>Returns the digits of a value.</summary>
		[System.CLSCompliant(false)]
		public static System.Collections.Generic.IEnumerable<ulong> GetDigits(ulong value, int radix)
			=> new System.Collections.Generic.Stack<ulong>(GetDigitsReversed(value, radix));
	}
}
