namespace Flux.Text
{
	public ref struct Sequences
	{
		/// <summary>ASCII</summary>
		/// <see cref="https://en.wikipedia.org/wiki/ASCII"/>
		public static System.ReadOnlySpan<char> Ascii
			=> new char[] { '\u0000', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\u0007', '\u0008', '\u0009', '\u000A', '\u000B', '\u000C', '\u000D', '\u000E', '\u000F', '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001A', '\u001B', '\u001C', '\u001D', '\u001E', '\u001F', '\u0020', '\u0021', '\u0022', '\u0023', '\u0024', '\u0025', '\u0026', '\u0027', '\u0028', '\u0029', '\u002A', '\u002B', '\u002C', '\u002D', '\u002E', '\u002F', '\u0030', '\u0031', '\u0032', '\u0033', '\u0034', '\u0035', '\u0036', '\u0037', '\u0038', '\u0039', '\u003A', '\u003B', '\u003C', '\u003D', '\u003E', '\u003F', '\u0040', '\u0041', '\u0042', '\u0043', '\u0044', '\u0045', '\u0046', '\u0047', '\u0048', '\u0049', '\u004A', '\u004B', '\u004C', '\u004D', '\u004E', '\u004F', '\u0050', '\u0051', '\u0052', '\u0053', '\u0054', '\u0055', '\u0056', '\u0057', '\u0058', '\u0059', '\u005A', '\u005B', '\u005C', '\u005D', '\u005E', '\u005F', '\u0060', '\u0061', '\u0062', '\u0063', '\u0064', '\u0065', '\u0066', '\u0067', '\u0068', '\u0069', '\u006A', '\u006B', '\u006C', '\u006D', '\u006E', '\u006F', '\u0070', '\u0071', '\u0072', '\u0073', '\u0074', '\u0075', '\u0076', '\u0077', '\u0078', '\u0079', '\u007A', '\u007B', '\u007C', '\u007D', '\u007E', '\u007F' };

		/// <summary>The base62 encoding scheme uses 62 characters. The characters consist of the capital letters A-Z, the lower case letters a-z and the numbers 0�9.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Base62"/>
		public static System.ReadOnlySpan<string> Base62
			=> new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

		/// <summary>https://en.wikipedia.org/wiki/Base64</summary>
		public static System.ReadOnlySpan<string> Base64
			=> new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+", "/" };

		/// <summary>The Mayan numeral system was the system to represent numbers and calendar dates in the Maya civilization. It was a vigesimal (base-20) positional numeral system.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Maya_numerals"/>
		/// <seealso cref="https://en.wikipedia.org/wiki/Vigesimal"/>
		public static System.ReadOnlySpan<string> MayanNumerals
			=> new string[] { "\U0001D2E0", "\U0001D2E1", "\U0001D2E2", "\U0001D2E3", "\U0001D2E4", "\U0001D2E5", "\U0001D2E6", "\U0001D2E7", "\U0001D2E8", "\U0001D2E9", "\U0001D2EA", "\U0001D2EB", "\U0001D2EC", "\U0001D2ED", "\U0001D2EE", "\U0001D2EF", "\U0001D2F0", "\U0001D2F1", "\U0001D2F2", "\U0001D2F3" };

		/// <summary>Base 95 (https://en.wikipedia.org/wiki/ASCII)</summary>
		public static System.ReadOnlySpan<char> PrintableAscii
			=> Ascii.Slice(32, 95);

		/// <summary>Decimal unicode subscript</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
		/// <seealso cref="https://en.wikipedia.org/wiki/Decimal"/>
		public static System.ReadOnlySpan<string> Subscript0Through9
			=> new string[] { "\u2080", "\u2081", "\u2082", "\u2083", "\u2084", "\u2085", "\u2086", "\u2087", "\u2088", "\u2089" };
		/// <summary>Decimal unicode superscript</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
		/// <seealso cref="https://en.wikipedia.org/wiki/Decimal"/>
		public static System.ReadOnlySpan<string> Superscript0Through9
			=> new string[] { "\u2070", "\u00B9", "\u00B2", "\u00B3", "\u2074", "\u2075", "\u2076", "\u2077", "\u2078", "\u2079" };
	}
}