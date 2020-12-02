using System.Linq;

namespace Flux
{
	public static partial class IEnumerableEm
	{
		/// <summary>Concatenates item data into a string from the sequence with the specified delimiter, by means of a <see cref="System.Text.StringBuilder"/> selector.</summary>
		private static string ToDelimitedString<T>(System.Collections.Generic.IEnumerable<T> source, string delimiter, System.Func<System.Text.StringBuilder, T, System.Text.StringBuilder> stringBuilder)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (stringBuilder is null) throw new System.ArgumentNullException(nameof(stringBuilder));

			var sb = new System.Text.StringBuilder();

			var index = 0;

			foreach (var value in source)
			{
				if (index++ > 0) sb.Append(delimiter);

				stringBuilder(sb, value);
			}

			return sb.ToString();
		}
		/// <summary>Concatenates item data into a string from the sequence with the specified delimiter.</summary>
		public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter)
			=> ToDelimitedString(source, delimiter, (sb, e) => sb.Append(e));
		public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter, System.Func<T, string> stringSelector)
			=> ToDelimitedString(source, delimiter, (sb, e) => sb.Append(stringSelector(e)));
	}
}
