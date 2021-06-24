namespace Flux
{
  public static partial class ExtensionMethods
	{
		/// <summary>Remove (in-place) diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
		public static void RemoveDiacriticalLatinStrokes(this System.Span<char> source)
		{
			for (var index = source.Length - 1; index >= 0; index--)
			{
				var sc = source[index];

				if ((char)ExtensionMethods.RemoveDiacriticalLatinStroke((System.Text.Rune)sc).Value is var tc && tc != sc)
					source[index] = tc;
			}
		}
	}
}
