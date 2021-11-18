using System.Linq;

namespace Flux.Text.PhoneticAlgorithm
{
	/// <summary>Implements the Caverphone 2.0 algorithm</summary>
	// https://en.wikipedia.org/wiki/Caverphone
	public sealed class Caverphone2
		: IPhoneticAlgorithmEncoder
	{
		/// <summary>Encodes the given text using the Caverphone 2.0 algorithm.</summary>
		public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> word)
		{
			var code = string.Concat(word.ToArray().Select(c => char.ToLowerInvariant(c)).Where(c => c >= 'a' && c <= 'z')); // 2 & 3

			//code = System.Text.RegularExpressions.Regex.Replace(code, @"e$", string.Empty); // 4
			if (code.EndsWith('e')) code = code[0..^1];

			code = System.Text.RegularExpressions.Regex.Replace(code, @"^(cou|rou|tou|enou|trou)gh", @"${1}2f"); // 5.[1/2/3/4/5]
			code = System.Text.RegularExpressions.Regex.Replace(code, @"^gn", @"2n"); // 5.6

			code = System.Text.RegularExpressions.Regex.Replace(code, @"mb$", @"m2"); // 6

			code = code.Replace(@"cq", @"2q"); // 7.1
			code = System.Text.RegularExpressions.Regex.Replace(code, @"c(i|e|y)", @"s${1}"); // 7.[2/3/4]
			code = code.Replace(@"tch", @"2ch"); // 7.5
			code = System.Text.RegularExpressions.Regex.Replace(code, @"(c|q|x)", @"k"); // 7.[6/7/8]
			code = code.Replace('v', 'f'); // 7.9
			code = code.Replace(@"dg", @"2g"); // 7.10
			code = System.Text.RegularExpressions.Regex.Replace(code, @"ti(o|a)", @"si${1}"); // 7.[11,12]

			code = code.Replace('d', 't'); // 7.13
			code = code.Replace(@"ph", @"fh"); // 7.14
			code = code.Replace('b', 'p'); // 7.15
			code = code.Replace(@"sh", @"s2"); // 7.16
			code = code.Replace('z', 's'); // 7.17

			code = System.Text.RegularExpressions.Regex.Replace(code, @"^(a|e|i|o|u)", @"A"); // 7.18
			code = System.Text.RegularExpressions.Regex.Replace(code, @"(a|e|i|o|u)", @"3"); // 7.19

			code = code.Replace(@"j", @"y"); // 7.20
			code = System.Text.RegularExpressions.Regex.Replace(code, @"^y3", @"Y3"); // 7.21
			code = System.Text.RegularExpressions.Regex.Replace(code, @"^y", @"A"); // 7.22
			code = code.Replace(@"y", @"3"); // 7.23
			code = code.Replace(@"3gh3", @"3kh3"); // 7.24
			code = code.Replace(@"gh", @"22"); // 7.25
			code = code.Replace(@"g", @"k"); // 7.26

			code = System.Text.RegularExpressions.Regex.Replace(code, @"s+", @"S"); // 7.27
			code = System.Text.RegularExpressions.Regex.Replace(code, @"t+", @"T"); // 7.28
			code = System.Text.RegularExpressions.Regex.Replace(code, @"p+", @"P"); // 7.29
			code = System.Text.RegularExpressions.Regex.Replace(code, @"k+", @"K"); // 7.30
			code = System.Text.RegularExpressions.Regex.Replace(code, @"f+", @"F"); // 7.31
			code = System.Text.RegularExpressions.Regex.Replace(code, @"m+", @"M"); // 7.32
			code = System.Text.RegularExpressions.Regex.Replace(code, @"n+", @"N"); // 7.33

			code = code.Replace(@"w3", @"W3"); // 7.34
			code = code.Replace(@"wh3", @"Wh3"); // 7.35
			code = System.Text.RegularExpressions.Regex.Replace(code, @"w$", @"3"); // 7.36
			code = code.Replace(@"w", @"2"); // 7.37

			code = System.Text.RegularExpressions.Regex.Replace(code, @"^h", @"A"); // 7.38
			code = code.Replace(@"h", @"2"); // 7.39

			code = code.Replace(@"r3", @"R3"); // 7.40
			code = System.Text.RegularExpressions.Regex.Replace(code, @"r$", @"3"); // 7.41
			code = code.Replace(@"r", @"2"); // 7.42

			code = code.Replace(@"l3", @"L3"); // 7.43
			code = System.Text.RegularExpressions.Regex.Replace(code, @"l$", @"3"); // 7.44
			code = code.Replace(@"l", @"2"); // 7.45

			code = code.Replace(@"2", string.Empty); // 8
			code = System.Text.RegularExpressions.Regex.Replace(code, @"3$", @"A"); // 9
			code = code.Replace(@"3", string.Empty); // 10

			code += @"1111111111"; // 11

			return code[..10]; // 12
		}
	}
}
