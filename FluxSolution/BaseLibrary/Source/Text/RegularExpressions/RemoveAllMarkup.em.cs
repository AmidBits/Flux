namespace Flux
{
	public static partial class Regex
	{
		public const string AllMarkupTags = @"(" + OneMarkupTag + @")+";
		public const string OneMarkupTag = @"<[^>]+>";

		/// <summary>Remove all markup tags (i.e. like in HTML, XML, etc.) from the string. Uses regular expressions.</summary>
		public static string RemoveAllMarkupTags(this string source)
			=> ReplaceAllMarkupTags(source, string.Empty);

		/// <summary>Replace all markup tags (i.e. like in HTML, XML, etc.) with a chosen string. Uses regular expressions.</summary>	
		public static string ReplaceAllMarkupTags(this string source, string replacement)
			=> System.Text.RegularExpressions.Regex.Replace(source, AllMarkupTags, replacement);
	}
}
