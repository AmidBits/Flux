using System.Linq;

namespace Flux
{
	public static partial class TextPhoneticAlgorithmEm
	{
		/// <summary>The match rating approach (MRA) is a phonetic algorithm developed by Western Airlines in 1977 for the indexation and comparison of homophonous names.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Match_rating_approach"/>
		public static string EncodeMatchRatingApproach(this System.ReadOnlySpan<char> source)
			=> new Text.PhoneticAlgorithm.MatchRatingApproach().EncodePhoneticAlgorithm(source);
	}

	namespace Text.PhoneticAlgorithm
	{
		/// <summary>The match rating approach (MRA) is a phonetic algorithm developed by Western Airlines in 1977 for the indexation and comparison of homophonous names.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Match_rating_approach"/>
		public class MatchRatingApproach
			: IPhoneticAlgorithmEncoder
		{
			public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> name)
			{
				var soundex = new System.Text.StringBuilder();

				for (var index = 0; index < name.Length; index++)
				{
					var character = name[index];

					if (index > 0)
					{
						if (Flux.Globalization.EnUs.Language.IsEnglishVowel(character, false))
							continue;

						if (Flux.Globalization.EnUs.Language.IsEnglishVowel(character, true) && character == name[index - 1])
							continue;
					}

					soundex.Append(char.ToUpper(character, System.Globalization.CultureInfo.InvariantCulture));
				}

				if (soundex.Length > 6)
					return soundex.ToString(0, 3) + soundex.ToString(soundex.Length - 3, 3);

				return soundex.ToString();
			}

			public int CommonChars(string left, string right)
			{
				return left.GroupBy(c => c)
						.Join(
								right.GroupBy(c => c),
								g => g.Key,
								g => g.Key,
								(lg, rg) => lg.Zip(rg, (l, r) => l).Count())
						.Sum();
			}
			public void Chars(System.ReadOnlySpan<char> left, System.ReadOnlySpan<char> right, out System.Text.StringBuilder leftOnly, out System.Text.StringBuilder leftAlso, out System.Text.StringBuilder rightOnly, out System.Text.StringBuilder rightAlso)
			{
				leftOnly = new System.Text.StringBuilder();
				leftAlso = new System.Text.StringBuilder();

				foreach (var c in left)
				{
					if (right.IndexOf(c) == -1)
						leftOnly.Append(c);
					else
						leftAlso.Append(c);
				}

				rightOnly = new System.Text.StringBuilder();
				rightAlso = new System.Text.StringBuilder();

				foreach (var c in right)
				{
					if (left.IndexOf(c) == -1)
						rightOnly.Append(c);
					else
						rightAlso.Append(c);
				}
			}

			public bool CompareEncodings(System.ReadOnlySpan<char> name1, System.ReadOnlySpan<char> name2, out int minimumRating, out int similarityRating)
			{
				minimumRating = 0;
				similarityRating = 0;

				var code1 = EncodePhoneticAlgorithm(name1).AsReadOnlySpan();
				var code2 = EncodePhoneticAlgorithm(name2).AsReadOnlySpan();

				if (System.Math.Abs(code1.Length - code2.Length) >= 3)
					return false;

				minimumRating = (code1.Length + code2.Length) switch
				{
					<= 4 => 5,
					<= 7 => 4,
					<= 11 => 3,
					12 => 2,
					_ => 0
				};

				Chars(name1, name2, out var nlo, out var nla, out var nro, out var nra);
				Chars(code1, code2, out var clo, out var cla, out var cro, out var cra);

				//var c1 = code1.ToStringBuilder().ReplaceAll(' ', code2.ToArray());
				//var n1 = name1.ToStringBuilder().ReplaceAll(' ', name2.ToArray());

				//if (code1.CountEqualAtStart(code2, out var startMinLength) is var equalAtStart && equalAtStart > 0)
				//{
				//	code1 = code1.Slice(equalAtStart);
				//	code2 = code2.Slice(equalAtStart);
				//}

				//if (code1.CountEqualAtEnd(code2, out var endMinLength) is var equalAtEnd && equalAtEnd > 0)
				//{
				//	code1 = code1.Slice(0, code1.Length - equalAtEnd);
				//	code2 = code2.Slice(0, code2.Length - equalAtEnd);
				//}

				similarityRating = 6 - (code1.Length > code2.Length ? code1.Length : code2.Length);

				return similarityRating >= minimumRating;
			}
		}
	}
}
