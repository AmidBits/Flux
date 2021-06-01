namespace Flux
{
	namespace Text.PhoneticAlgorithm
	{
		/// <summary>The match rating approach (MRA) is a phonetic algorithm developed by Western Airlines in 1977 for the indexation and comparison of homophonous names.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Match_rating_approach"/>
		public class MatchRatingApproach
			: IPhoneticAlgorithmEncoder
		{
			public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> name)
			{
				var code = new System.Text.StringBuilder();

				for (var index = 0; index < name.Length; index++)
				{
					var character = name[index];

					if (index > 0)
					{
						if (GlobalizationEnUs.IsEnglishVowel(character, false))
							continue;

						if (GlobalizationEnUs.IsEnglishVowel(character, true) && character == name[index - 1])
							continue;
					}

					code.Append(char.ToUpper(character, System.Globalization.CultureInfo.InvariantCulture));
				}

				if (code.Length > 6)
					code.Remove(3, code.Length - 6);

				return code.ToString();
			}

			/// <summary>Compare two MRA encoded names.</summary>
			/// <param name="code1">The first encoded name.</param>
			/// <param name="code2">The second encoded name.</param>
			/// <param name="minimumRating">Output variable returning the minimum rating.</param>
			/// <param name="similarityRating">Output variable returning the similarity rating.</param>
			/// <returns>Whether the name comparison is considered successful.</returns>
			public static bool CompareEncoded(System.ReadOnlySpan<char> code1, System.ReadOnlySpan<char> code2, out int minimumRating, out int similarityRating)
			{
				minimumRating = ComputeMinimumRating(code1.Length, code2.Length);
				similarityRating = ComputeSimilarityRating(code1, code2);

				return similarityRating >= minimumRating;
			}

			/// <summary>Returns the minimum rating which is based on the code lengths from two encoded names.</summary>
			/// <param name="codeLength1">The first encoded length.</param>
			/// <param name="codeLength2">The second encoded length.</param>
			public static int ComputeMinimumRating(int codeLength1, int codeLength2)
			{
				if (System.Math.Abs(codeLength1 - codeLength2) >= 3)
					return 0;

				return (codeLength1 + codeLength2) switch
				{
					<= 4 => 5,
					<= 7 => 4,
					<= 11 => 3,
					12 => 2,
					_ => 0
				};
			}
			/// <summary>Returns the similarity rating for two encoded names.</summary>
			/// <param name="code1">The first encoded name.</param>
			/// <param name="code2">The second encoded name.</param>
			/// <returns></returns>
			public static int ComputeSimilarityRating(System.ReadOnlySpan<char> code1, System.ReadOnlySpan<char> code2)
			{
				var large = code1.ToStringBuilder();
				var small = code2.ToStringBuilder();

				if (large.Length < small.Length)
				{
					var tmp = large;
					large = small;
					small = tmp;
				}

				for (int i = 0; i < small.Length;)
				{
					bool found = false;

					for (int j = 0; j < large.Length; j++)
						if (small[i] == large[j])
						{
							small.Remove(i, 1);
							large.Remove(j, 1);
							found = true;
						}

					if (!found)
						i++;
				}

				for (var i = small.Length - 1; i >= 0;)
				{
					var found = false;

					for (var j = large.Length - 1; j >= 0; j--)
					{
						if (small[i] == large[j])
						{
							small.Remove(i, 1);
							large.Remove(j, 1);
							found = true;
						}
					}

					if (!found)
						i--;
				}

				return 6 - large.Length;
			}
		}
	}
}
