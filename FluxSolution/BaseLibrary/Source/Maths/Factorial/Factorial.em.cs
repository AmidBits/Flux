using System.Linq;

namespace Flux
{
	public static partial class Maths
	{
		/// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
		public static System.Numerics.BigInteger Factorial(System.Numerics.BigInteger value)
			=> ParallelSplitFactorial.Default.ComputeProduct(value);

		/// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
		public static int Factorial(int value)
			=> value >= 0 ? unchecked((int)Factorial((uint)value)) : throw new System.ArgumentOutOfRangeException(nameof(value));
		/// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
		public static long Factorial(long value)
			=> value >= 0 ? unchecked((long)Factorial((ulong)value)) : throw new System.ArgumentOutOfRangeException(nameof(value));

		/// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
		[System.CLSCompliant(false)]
		public static uint Factorial(uint value)
			=> value <= 1 ? 1 : Flux.LinqX.Range(2, value - 1, 1).AsParallel().Aggregate((a, b) => a * b);
		/// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
		[System.CLSCompliant(false)]
		public static ulong Factorial(ulong value)
			=> value <= 1 ? 1 : Flux.LinqX.Range(2, value - 1, 1).AsParallel().Aggregate((a, b) => a * b);
	}
}
