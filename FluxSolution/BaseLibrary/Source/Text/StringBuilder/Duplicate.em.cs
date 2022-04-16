namespace Flux
{
	public static partial class StringBuilderEm
	{
		/// <summary>Duplicates this string builder a specified number of times.</summary>
		public static System.Text.StringBuilder Duplicate(this System.Text.StringBuilder source, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			else if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

			var original = source.ToString();

			while (count-- > 0)
				source.Append(original);

			return source;
		}

		/// <summary>Returns the string builder with the specified characters duplicated by the specified count throughout. If no characters are specified, all characters are replicated. If the string builder is empty, nothing is replicated. Uses the specified comparer.</summary>
		public static System.Text.StringBuilder Duplicate(this System.Text.StringBuilder source, System.ReadOnlySpan<char> characters, int count, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			for (var index = 0; index < source.Length; index++)
			{
				var sourceChar = source[index];

				if (characters.Length == 0 || characters.IndexOf(sourceChar, equalityComparer) > -1)
				{
					source.Insert(index, sourceChar.ToString(), count);

					index += count;
				}
			}

			return source;
		}
		/// <summary>Returns the string builder with the specified characters duplicated by the specified count throughout. If no characters are specified, all characters are replicated. If the string builder is empty, nothing is replicated. Uses the default comparer.</summary>
		public static System.Text.StringBuilder Duplicate(this System.Text.StringBuilder source, System.ReadOnlySpan<char> characters, int count)
			=> Duplicate(source, characters, count, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
