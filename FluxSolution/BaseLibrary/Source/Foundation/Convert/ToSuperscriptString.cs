using System.Linq;

namespace Flux
{
	public static partial class Convert
	{
		/// <summary>Returns a string with the numeric superscript.</summary>
		public static string ToSuperscriptString(System.Numerics.BigInteger number, int radix)
		{
			if (radix < 2 || radix > 10) throw new System.ArgumentOutOfRangeException(nameof(radix));

			var span = Maths.GetDigits(number, radix);
			var chars = new char[span.Length];
			for (var index = span.Length - 1; index >= 0; index--)
				chars[index] = RuneSequences.Superscript0Through9[index].ToString()[0];
			return new string(chars);
		}

		/// <summary>Returns a string with the numeric superscript.</summary>
		public static string ToSuperscriptString(int number, int radix)
		{
			if (radix < 2 || radix > 10) throw new System.ArgumentOutOfRangeException(nameof(radix));

			var span = Maths.GetDigits(number, radix);
			var chars = new char[span.Length];
			for (var index = span.Length - 1; index >= 0; index--)
				chars[index] = RuneSequences.Superscript0Through9[index].ToString()[0];
			return new string(chars);
		}
		/// <summary>Returns a string with the numeric superscript.</summary>
		public static string ToSuperscriptString(long number, int radix)
		{
			if (radix < 2 || radix > 10) throw new System.ArgumentOutOfRangeException(nameof(radix));

			var span = Maths.GetDigits(number, radix);
			var chars = new char[span.Length];
			for (var index = span.Length - 1; index >= 0; index--)
				chars[index] = RuneSequences.Superscript0Through9[index].ToString()[0];
			return new string(chars);
		}
	}
}
