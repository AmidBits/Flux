using System.Linq;

namespace Flux
{
	public static partial class Convert
	{
		/// <summary>Returns a string with the numeric superscript.</summary>
		public static string ToSuperscriptString(System.Numerics.BigInteger number, int radix)
			=> radix >= 2 && radix <= 10 ? string.Concat(Maths.GetDigitsReversed(number, radix).Select(d => Text.Sequences.Superscript0Through9[(int)d][0]).Reverse()) : throw new System.ArgumentOutOfRangeException(nameof(number));

		/// <summary>Returns a string with the numeric superscript.</summary>
		public static string ToSuperscriptString(int number, int radix)
			=> radix >= 2 && radix <= 10 ? string.Concat(Maths.GetDigitsReversed(number, radix).Select(d => Text.Sequences.Superscript0Through9[d][0]).Reverse()) : throw new System.ArgumentOutOfRangeException(nameof(number));
		/// <summary>Returns a string with the numeric superscript.</summary>
		public static string ToSuperscriptString(long number, int radix)
			=> radix >= 2 && radix <= 10 ? string.Concat(Maths.GetDigitsReversed(number, radix).Select(d => Text.Sequences.Superscript0Through9[(int)d][0]).Reverse()) : throw new System.ArgumentOutOfRangeException(nameof(number));
	}
}
