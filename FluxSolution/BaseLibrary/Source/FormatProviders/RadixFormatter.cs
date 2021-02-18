using System.Linq;

namespace Flux.FormatProviders
{
	/// <summary>Enables formatting of any number base (radix) with any characters desired.</summary>
	public class RadixFormatter
		: FormatProvider
	{
		public const string FormatIdentifier = @"RADIX";

		public static readonly string[] RadixNumerals = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Select(c => c.ToString()).ToArray();

		public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
		{
			if (!string.IsNullOrEmpty(format) && format.StartsWith(FormatIdentifier, System.StringComparison.OrdinalIgnoreCase) && System.Numerics.BigInteger.TryParse(format.Substring(5), out var radix))
			{
				if (System.Numerics.BigInteger.TryParse(arg?.ToString() ?? string.Empty, out var raw))
				{
					if (TryFormat(raw, RadixNumerals.Take((int)radix).ToArray(), out var newBase))
					{
						return newBase;
					}
				}
			}

			return HandleOtherFormats(format, arg);
		}

		public static bool TryFormat(System.Numerics.BigInteger number, string[] radixNumerals, out string result)
		{
			try
			{
				var pn = new Flux.Text.PositionalNotation(radixNumerals);
				result = pn.NumberToText(number);
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types.
			catch
#pragma warning restore CA1031 // Do not catch general exception types.
			{ }

			result = string.Empty;
			return false;
		}
	}
}
