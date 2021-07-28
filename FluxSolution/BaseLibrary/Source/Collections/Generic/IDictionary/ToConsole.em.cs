using System.Linq;

namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary></summary>
		public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> source, char horizontalSeparator = '\u003D')
			where TKey : notnull
			=> source.Select(kvp => $"{kvp.Key}{horizontalSeparator}{kvp.Value}");

		public static string ToConsoleString<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> source, char horizontalSeparator = '\u003D')
			where TKey : notnull
			=> string.Join(System.Environment.NewLine, ToConsoleStrings(source, horizontalSeparator));
	}
}
