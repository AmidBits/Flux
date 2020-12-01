namespace Flux.Text
{
	/// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
	/// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
	public ref struct PositionalNotation
	{
		public static PositionalNotation Base2 => new PositionalNotation(Sequences.Base62.Slice(0, 2));
		public static PositionalNotation Base8 => new PositionalNotation(Sequences.Base62.Slice(0, 8));
		public static PositionalNotation Base10 => new PositionalNotation(Sequences.Base62.Slice(0, 10));
		public static PositionalNotation Base16 => new PositionalNotation(Sequences.Base62.Slice(0, 16));

		public System.ReadOnlySpan<string> Symbols { get; }

		public PositionalNotation(System.ReadOnlySpan<string> symbols)
			=> Symbols = symbols;

		/// <summary>Convert a number into a positional notation text string.</summary>
		/// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
		/// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
		/// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
		/// System.Collections.Generic.IList<string>
		public string NumberToText(System.Numerics.BigInteger number)
		{
			var sb = new System.Text.StringBuilder(128);

			if (number.IsZero)
			{
				sb.Append('0');
			}
			else while (number != 0)
				{
					number = System.Numerics.BigInteger.DivRem(number, Symbols.Length, out var remainder);

					sb.Insert(0, Symbols[(int)System.Numerics.BigInteger.Abs(remainder)]);
				}

			return sb.ToString();
		}
		public bool TryNumberToText(System.Numerics.BigInteger number, out string? result)
		{
			try
			{
				result = NumberToText(number);
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch { }
#pragma warning restore CA1031 // Do not catch general exception types

			result = default;
			return false;
		}

		/// <summary>Convert a positional notation text string into a number.</summary>
		/// <param name="number">Must consist of only TextElements (i.e. graphemes).</param>
		/// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
		/// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
		/// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
		public System.Numerics.BigInteger TextToNumber(string number)
		{
			var bi = System.Numerics.BigInteger.Zero;

			foreach (var textElement in number.GetTextElements())
			{
				bi *= Symbols.Length;

				var position = Symbols.IndexOf(textElement);

				bi += position > -1 ? position : throw new System.InvalidOperationException();
			}

			return bi;
		}
		/// <summary>Convert a positional notation text string into a number.</summary>
		public bool TryTextToNumber(string number, out System.Numerics.BigInteger result)
		{
			try
			{
				result = TextToNumber(number);
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch { }
#pragma warning restore CA1031 // Do not catch general exception types

			result = default;
			return false;
		}

		/// <summary>Custom instance based on Base62 which results in traditional radix conversions.</summary>
		public static PositionalNotation ForRadix(int radix)
			=> radix switch
			{
				2 => Base2,
				8 => Base8,
				10 => Base10,
				16 => Base16,
				var r when r >= 2 && r <= 62 => new PositionalNotation(Sequences.Base62.Slice(0, r)),
				_ => throw new System.ArgumentOutOfRangeException(nameof(radix))
			};

		public static System.Collections.Generic.Dictionary<int, string> ToStringRadices(System.Numerics.BigInteger number)
		{
			var dictionary = new System.Collections.Generic.Dictionary<int, string>();
			for (var radix = 2; radix <= 62; radix++)
				dictionary.Add(radix, ForRadix(radix).NumberToText(number));
			return dictionary;
		}
	}
}
