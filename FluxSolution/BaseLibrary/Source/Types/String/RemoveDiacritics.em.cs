namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Remove diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
		public static System.Collections.Generic.IEnumerable<char> RemoveDiacriticalLatinStrokes(this string source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			for (var index = 0; index < source.Length; index++)
			{
				yield return ExtensionMethods.RemoveDiacriticalLatinStroke(source[index]);
			}
		}

		/// <summary>Remove diacritical marks and any optional replacements desired.</summary>
		public static System.Collections.Generic.IEnumerable<char> RemoveDiacriticalMarks(this string source, System.Func<char, char> additionalCharacterReplacements)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (additionalCharacterReplacements is null) throw new System.ArgumentNullException(nameof(additionalCharacterReplacements));

			foreach (var character in source.ToString().Normalize(System.Text.NormalizationForm.FormKD))
			{
				switch (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(character))
				{
					case System.Globalization.UnicodeCategory.NonSpacingMark:
					case System.Globalization.UnicodeCategory.SpacingCombiningMark:
					case System.Globalization.UnicodeCategory.EnclosingMark:
						break;
					default:
						yield return additionalCharacterReplacements(character);
						break;
				}
			}
		}

		/// <summary>Remove diacritical marks and latin strokes (the latter are unaffected by normalization forms in NET).</summary>
		public static System.Collections.Generic.IEnumerable<char> RemoveDiacriticalMarksAndStrokes(this string source)
			=> RemoveDiacriticalMarks(source, ExtensionMethods.RemoveDiacriticalLatinStroke);
	}
}
