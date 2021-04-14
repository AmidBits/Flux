using System.Linq;

namespace Flux
{
	public static partial class UnicodeEm
	{
		/// <summary>Returns a string representing the <see cref="System.Text.Rune"/> as a Unicode notation.</summary>
		public static string ToUnicodeNotation(this System.Text.Rune source)
			=> Text.UnicodeNotation.ToString(source);
	}

	namespace Text
	{
		/// <summary>The functionality of this class relates to U+xxxxx style formatting.</summary>
		public static class UnicodeNotation
		{
			public static readonly System.Text.RegularExpressions.Regex ParseRegex = new System.Text.RegularExpressions.Regex(@"(?<=U\+)[0-9A-F]{4,6}", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);

			public static System.Collections.Generic.IEnumerable<System.Text.Rune> Parse(string expression)
				=> ParseRegex.Matches(expression).Where(m => m.Success).Select(m => new System.Text.Rune(int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null)));
			public static bool TryParse(string text, out System.Collections.Generic.List<System.Text.Rune> result)
			{
				try
				{
					result = Parse(text).ToList();
					return true;
				}
        catch { }

        result = default!;
				return false;
			}

			/// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
			public static string ToString(System.Text.Rune rune)
				=> $"U+{rune.Value:X4}";
		}
	}
}