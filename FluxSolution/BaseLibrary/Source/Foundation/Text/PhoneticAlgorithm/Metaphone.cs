using System.Linq;

namespace Flux.Text.PhoneticAlgorithm
{
	/// <summary>Implements the Metaphone algorithm</summary>
	public sealed class Metaphone
		: IPhoneticAlgorithmEncoder
	{
		public int MaxCodeLength { get; set; } = 6;

		private System.Text.StringBuilder _output = new();
		public string Output { get { return _output.ToString(); } }

		private const char NullChar = (char)0;
		private const string Vowels = "AEIOU";

		private string __text = string.Empty;
		private int __pos;

		/// <summary>Encodes the given text using the Metaphone algorithm.</summary>
		public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> expression)
		{
			_output = new System.Text.StringBuilder();

			__text = string.Concat(expression.ToString().RemoveDiacriticalMarks(c => (char)((System.Text.Rune)c).ReplaceDiacriticalLatinStroke().Value).Where(c => char.IsLetter(c)).Select(c => char.ToUpper(c, System.Globalization.CultureInfo.CurrentCulture)));
			__pos = 0;

			// Special handling of some string prefixes: PN, KN, GN, AE, WR, WH and X
			switch (Peek())
			{
				case 'P':
				case 'K':
				case 'G':
					if (Peek(1) == 'N')
						MoveAhead();
					break;

				case 'A':
					if (Peek(1) == 'E')
						MoveAhead();
					break;

				case 'W':
					if (Peek(1) == 'R')
						MoveAhead();
					else if (Peek(1) == 'H')
					{
						_output.Append('W');
						MoveAhead(2);
					}
					break;

				case 'X':
					_output.Append('S');
					MoveAhead();
					break;
			}

			while (__pos < __text.Length && _output.Length < MaxCodeLength)
			{
				var c = Peek();

				// Ignore duplicates except CC
				if (c == Peek(-1) && c != 'C')
				{
					MoveAhead();
					continue;
				}

				// Don't change F, J, L, M, N, R or first-letter vowel
				if (IsOneOf(c, "FJLMNR") || (_output.Length == 0 && IsOneOf(c, Vowels)))
				{
					_output.Append(c);
					MoveAhead();
				}
				else
				{
					int charsConsumed = 1;

					switch (c)
					{
						case 'B':
							// B = 'B' if not -MB
							if (Peek(-1) != 'M' || Peek(1) != NullChar)
								_output.Append('B');
							break;

						case 'C':
							// C = 'X' if -CIA- or -CH-
							// Else 'S' if -CE-, -CI- or -CY-
							// Else 'K' if not -SCE-, -SCI- or -SCY-
							if (Peek(-1) != 'S' || !IsOneOf(Peek(1), "EIY"))
							{
								if (Peek(1) == 'I' && Peek(2) == 'A')
									_output.Append('X');
								else if (IsOneOf(Peek(1), "EIY"))
									_output.Append('S');
								else if (Peek(1) == 'H')
								{
									if ((__pos == 0 && !IsOneOf(Peek(2), Vowels)) || Peek(-1) == 'S')
										_output.Append('K');
									else
										_output.Append('X');
									charsConsumed++;    // Eat 'CH'
								}
								else _output.Append('K');
							}
							break;

						case 'D':
							// D = 'J' if DGE, DGI or DGY
							// Else 'T'
							if (Peek(1) == 'G' && IsOneOf(Peek(2), "EIY"))
								_output.Append('J');
							else
								_output.Append('T');
							break;

						case 'G':
							// G = 'F' if -GH and not B--GH, D--GH, -H--GH, -H---GH
							// Else dropped if -GNED, -GN, -DGE-, -DGI-, -DGY-
							// Else 'J' if -GE-, -GI-, -GY- and not GG
							// Else K
							if ((Peek(1) != 'H' || IsOneOf(Peek(2), Vowels)) && (Peek(1) != 'N' || (Peek(1) != NullChar && (Peek(2) != 'E' || Peek(3) != 'D'))) && (Peek(-1) != 'D' || !IsOneOf(Peek(1), "EIY")))
							{
								if (IsOneOf(Peek(1), "EIY") && Peek(2) != 'G')
									_output.Append('J');
								else
									_output.Append('K');
							}
							// Eat GH
							if (Peek(1) == 'H')
								charsConsumed++;
							break;

						case 'H':
							// H = 'H' if before or not after vowel
							if (!IsOneOf(Peek(-1), Vowels) || IsOneOf(Peek(1), Vowels))
								_output.Append('H');
							break;

						case 'K':
							// K = 'C' if not CK
							if (Peek(-1) != 'C')
								_output.Append('K');
							break;

						case 'P':
							// P = 'F' if PH
							// Else 'P'
							if (Peek(1) == 'H')
							{
								_output.Append('F');
								charsConsumed++;    // Eat 'PH'
							}
							else
								_output.Append('P');
							break;

						case 'Q':
							// Q = 'K'
							_output.Append('K');
							break;

						case 'S':
							// S = 'X' if SH, SIO or SIA
							// Else 'S'
							if (Peek(1) == 'H')
							{
								_output.Append('X');
								charsConsumed++;    // Eat 'SH'
							}
							else if (Peek(1) == 'I' && IsOneOf(Peek(2), "AO"))
								_output.Append('X');
							else
								_output.Append('S');
							break;

						case 'T':
							// T = 'X' if TIO or TIA
							// Else '0' if TH
							// Else 'T' if not TCH
							if (Peek(1) == 'I' && IsOneOf(Peek(2), "AO"))
								_output.Append('X');
							else if (Peek(1) == 'H')
							{
								_output.Append('0');
								charsConsumed++;    // Eat 'TH'
							}
							else if (Peek(1) != 'C' || Peek(2) != 'H')
								_output.Append('T');
							break;

						case 'V':
							// V = 'F'
							_output.Append('F');
							break;

						case 'W':
						case 'Y':
							// W,Y = Keep if not followed by vowel
							if (IsOneOf(Peek(1), Vowels))
								_output.Append(c);
							break;

						case 'X':
							// X = 'S' if first character (already done)
							// Else 'KS'
							_output.Append("KS");
							break;

						case 'Z':
							// Z = 'S'
							_output.Append('S');
							break;
					}

					MoveAhead(charsConsumed);
				}
			}

			return Output.ToString();
		}

		/// <summary>Moves the current position ahead the specified number of characters.</summary>
		void MoveAhead(int count = 1)
		{
			__pos = System.Math.Min(__pos + count, __text.Length);
		}

		/// <summary>Returns the character at the current position.</summary>
		private char Peek()
		{
			return Peek(0);
		}

		/// <summary>Returns the character at the specified position.</summary>
		private char Peek(int ahead)
		{
			int pos = (__pos + ahead);
			if (pos < 0 || pos >= __text.Length)
				return NullChar;
			return __text[pos];
		}

		/// <summary>Indicates if the specified character occurs within the specified string.</summary>
		private static bool IsOneOf(char c, string characters)
			=> (characters ?? throw new System.ArgumentNullException(nameof(characters))).IndexOf(c, System.StringComparison.Ordinal) != -1;

		//private static readonly System.Text.RegularExpressions.Regex m_notLetters = new System.Text.RegularExpressions.Regex(@"[^A-Z]+");
	}
}
