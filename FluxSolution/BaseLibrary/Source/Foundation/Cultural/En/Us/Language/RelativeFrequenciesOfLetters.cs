namespace Flux.Cultural.EnUs
{
	public static partial class Language
	{
		///// <summary></summary>
		//// https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language
		//public static System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<System.Text.Rune, double>> RelativeFrequencyOfFirstLettersInDictionaries
		//	=> new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Text.Rune, double>>()
		//	{
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'s', 11),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'c', 9.4),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'p', 7.7),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'b', 6),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'r', 6),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'d', 6.1),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'a', 5.7),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'m', 5.6),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'t', 5),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'f', 4.1),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'e', 3.9),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'i', 3.9),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'h', 3.7),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'g', 3.3),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'l', 3.1),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'u', 2.9),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'w', 2.7),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'o', 2.5),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'n', 2.2),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'v', 1.5),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'j', 1.1),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'k', 1),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'q', 0.49),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'y', 0.36),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'z', 0.24),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'x', 0.05),
		//	};
		///// <summary></summary>
		//// https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language
		//public static System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<System.Text.Rune, double>> RelativeFrequencyOfFirstLettersInPlainTexts
		//	=> new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Text.Rune, double>>()
		//	{
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'t', 16),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'o', 7.6),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'i', 7.3),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'s', 6.7),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'w', 5.5),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'c', 5.2),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'b', 4.4),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'p', 4.3),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'h', 4.2),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'f', 4),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'m', 3.8),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'d', 3.2),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'e', 2.8),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'r', 2.8),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'l', 2.4),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'n', 2.3),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'a', 1.7),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'g', 1.6),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'u', 1.2),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'k', 0.86),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'v', 0.82),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'y', 0.76),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'j', 0.51),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'q', 0.22),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'x', 0.045),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'z', 0.045),
		//	};

		///// <summary></summary>
		//// https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_the_first_letters_of_a_word_in_the_English_language
		//public static System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<System.Text.Rune, double>> RelativeFrequencyOfLettersInDictionaries
		//	=> new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Text.Rune, double>>()
		//	{
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'e', 11),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'s', 8.7),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'i', 8.6),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'a', 7.8),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'r', 7.3),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'n', 7.2),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'t', 6.7),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'o', 6.1),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'l', 5.3),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'c', 4),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'d', 3.8),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'u', 3.3),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'g', 3),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'p', 2.8),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'m', 2.7),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'h', 2.3),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'b', 2),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'y', 1.6),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'f', 1.4),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'v', 1),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'k', 0.97),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'w', 0.91),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'z', 0.44),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'x', 0.27),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'j', 0.21),
		//		new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'q', 0.19),
		//	};
		/// <summary></summary>
		// https://en.wikipedia.org/wiki/Letter_frequency
		public static System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<System.Text.Rune, double>> RelativeFrequencyOfLetters
			=> new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Text.Rune, double>>()
			{
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'e', 0.12702),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'t', 0.09056),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'a', 0.08167),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'o', 0.07507),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'i', 0.06966),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'n', 0.06749),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'s', 0.06327),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'h', 0.06094),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'r', 0.05987),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'d', 0.04253),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'l', 0.04025),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'c', 0.02782),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'u', 0.02758),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'m', 0.02406),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'w', 0.02360),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'f', 0.02228),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'g', 0.02015),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'y', 0.01974),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'p', 0.01929),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'b', 0.01492),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'v', 0.00978),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'k', 0.00772),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'j', 0.00153),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'x', 0.00150),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'q', 0.00095),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'z', 0.00074),
			};
	}
}
/*
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'e', 12.7),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'t', 9.06),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'a', 8.17),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'i', 6.97),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'o', 7.51),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'n', 6.7),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'s', 6.33),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'h', 6.09),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'r', 5.99),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'b', 1.5),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'c', 2.8),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'d', 4.25),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'l', 4.03),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'u', 2.76),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'m', 2.41),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'w', 2.36),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'f', 2.23),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'g', 2.02),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'y', 1.97),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'p', 1.93),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'v', 0.98),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'k', 0.77),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'j', 0.15),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'x', 0.15),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'q', 0.1),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'z', 0.07),

 */
