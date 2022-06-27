//namespace Flux
//{
//	namespace PhoneticAlgorithm
//	{
//		/// <summary>SQL Soundex is a phonetic algorithm for indexing names by sound, as pronounced in English, and as adopted by some relational database systems.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Soundex"/>
//		public class SqlSoundex
//			: IPhoneticAlgorithmEncoder
//		{
//			//ABCDEFGHIJKLMNOPQRSTUVWXYZ
//			public const string LetterCodeMap = @"01230120022455012623010202";

//			public int MaxCodeLength { get; set; } = 4;

//			public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> name)
//			{
//				var soundex = new System.Text.StringBuilder();

//				for (var index = 0; index < name.Length; index++)
//				{
//					if (char.ToUpper(name[index], System.Globalization.CultureInfo.CurrentCulture) is var letter && letter >= 'A' && letter <= 'Z')
//					{
//						if (index == 0)
//							soundex.Append(letter);

//						soundex.Append(LetterCodeMap[letter - 'A']);
//					}
//				}

//				soundex.NormalizeAdjacent('1', '2', '3', '4', '5', '6', '7', '8', '9');

//				soundex.RemoveAll('0');

//				if (soundex[1] == LetterCodeMap[soundex[0] - 'A'])
//					soundex.Remove(1, 1);

//				if (soundex.Length < MaxCodeLength)
//					soundex.Append('0', MaxCodeLength - soundex.Length);
//				if (soundex.Length > MaxCodeLength)
//					soundex.KeepLeft(MaxCodeLength);

//				return soundex.ToString();
//			}

//			/// <summary>Returns a score in the range [0, 4] symbolizing a difference of the two specified soundex codes. The larger the difference score the larger the difference.</summary>
//			/// <see cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/>
//			public static int Difference(System.ReadOnlySpan<char> soundex1, System.ReadOnlySpan<char> soundex2)
//			{
//				if (soundex1 == soundex2)
//					return 4;

//				if (soundex1.Slice(1, 3) == soundex2.Slice(1, 3))
//					return 3;

//				var result = soundex1[0] == soundex2[0] ? 1 : 0;

//				if (soundex2.IndexOf(soundex1.Slice(2, 2)) > -1)
//					return 2 + result;
//				else if (soundex2.IndexOf(soundex1.Slice(1, 2)) > -1)
//					return 2 + result;

//				if (soundex2[1] == soundex1[1])
//					result++;
//				if (soundex2[2] == soundex1[2])
//					result++;
//				if (soundex2[3] == soundex1[3])
//					result++;

//				return result == 0 ? 1 : result;
//			}
//		}
//	}
//}
