using System.Linq;

namespace Flux.Text.PhoneticAlgorithm
{
	/// <summary>Implements the Caverphone 1.0 algorithm</summary>
	// https://en.wikipedia.org/wiki/Caverphone
	public sealed class Caverphone1
		: IPhoneticAlgorithmEncoder
	{
		/// <summary>Encodes the given text using the Caverphone 2.0 algorithm.</summary>
		public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> word)
		{
			var code = string.Concat(word.ToArray().Select(c => char.ToLowerInvariant(c)).Where(c => c >= 'a' && c <= 'z')); // 1 & 2

			code = System.Text.RegularExpressions.Regex.Replace(code, @"^(cou|rou|tou|enou)gh", @"${1}2f"); // 3.[1/2/3/4]
			code = System.Text.RegularExpressions.Regex.Replace(code, @"^gn", @"2n"); // 3.5

			code = System.Text.RegularExpressions.Regex.Replace(code, @"mb$", @"m2"); // 4

			code = code.Replace(@"cq", @"2q"); // 5.1
			code = System.Text.RegularExpressions.Regex.Replace(code, @"c(i|e|y)", @"s${1}"); // 5.[2/3/4]
			code = code.Replace(@"tch", @"2ch"); // 5.5
			code = System.Text.RegularExpressions.Regex.Replace(code, @"(c|q|x)", @"k"); // 5.[6/7/8]
			code = code.Replace('v', 'f'); // 5.9
			code = code.Replace(@"dg", @"2g"); // 5.10
			code = System.Text.RegularExpressions.Regex.Replace(code, @"ti(o|a)", @"si${1}"); // 5.[11,12]

			code = code.Replace('d', 't'); // 5.13
			code = code.Replace(@"ph", @"fh"); // 5.14
			code = code.Replace('b', 'p'); // 5.15
			code = code.Replace(@"sh", @"s2"); // 5.16
			code = code.Replace('z', 's'); // 5.17

			code = System.Text.RegularExpressions.Regex.Replace(code, @"^(a|e|i|o|u)", @"A"); // 5.18
			code = System.Text.RegularExpressions.Regex.Replace(code, @"(a|e|i|o|u)", @"3"); // 5.19

			code = code.Replace(@"3gh3", @"3kh3"); // 5.20
			code = code.Replace(@"gh", @"22"); // 5.21
			code = code.Replace(@"g", @"k"); // 5.22

			code = System.Text.RegularExpressions.Regex.Replace(code, @"s+", @"S"); // 5.23
			code = System.Text.RegularExpressions.Regex.Replace(code, @"t+", @"T"); // 5.24
			code = System.Text.RegularExpressions.Regex.Replace(code, @"p+", @"P"); // 5.25
			code = System.Text.RegularExpressions.Regex.Replace(code, @"k+", @"K"); // 5.26
			code = System.Text.RegularExpressions.Regex.Replace(code, @"f+", @"F"); // 5.27
			code = System.Text.RegularExpressions.Regex.Replace(code, @"m+", @"M"); // 5.28
			code = System.Text.RegularExpressions.Regex.Replace(code, @"n+", @"N"); // 5.29

			code = code.Replace(@"w3", @"W3"); // 5.30
			code = code.Replace(@"wy", @"Wy"); // 5.31
			code = code.Replace(@"wh3", @"Wh3"); // 5.32
			code = code.Replace(@"why", @"Why"); // 5.33
			code = code.Replace(@"w", @"2"); // 5.34

			code = System.Text.RegularExpressions.Regex.Replace(code, @"^h", @"A"); // 5.35
			code = code.Replace(@"h", @"2"); // 5.36

			code = code.Replace(@"r3", @"R3"); // 5.37
			code = code.Replace(@"ry", @"Ry"); // 5.38
			code = code.Replace(@"r", @"2"); // 5.39

			code = code.Replace(@"l3", @"L3"); // 5.40
			code = code.Replace(@"ly", @"Ly"); // 5.41
			code = code.Replace(@"l", @"2"); // 5.42

			code = code.Replace(@"j", @"y"); // 5.43
			code = code.Replace(@"y3", @"Y3"); // 5.44
			code = code.Replace(@"y", @"2"); // 5.45

			code = code.Replace(@"2", string.Empty); // 6.1
			code = code.Replace(@"3", string.Empty); // 6.2

			code += @"111111"; // 7

			return code.Substring(0, 6); // 8
		}
	}
}
