namespace Flux
{
	public static partial class SystemTextStringBuilderEm
	{
		/// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the specfied comparer.</summary>
		public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, int maxAdjacentLength, System.Collections.Generic.IEqualityComparer<char> comparer, System.ReadOnlySpan<char> characters)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var index = 0;
			var previous = '\0';
			var adjacentLength = 1;

			for (var indexOfSource = 0; indexOfSource < source.Length; indexOfSource++)
			{
				var current = source[indexOfSource];

				var isEqual = characters.Length > 0 // Use list or just characters?
					? (characters.IndexOf(current, comparer) > -1 && characters.IndexOf(previous, comparer) > -1) // Is both current and previous in characters?
					: comparer.Equals(current, previous); // Is current and previous character equal?

				if (!isEqual || adjacentLength < maxAdjacentLength)
				{
					source[index++] = current;

					previous = current;
				}

				adjacentLength = !isEqual ? 1 : adjacentLength + 1;
			}

			return source.Remove(index, source.Length - index);
		}
		/// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the default comparer.</summary>
		public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, int maxAdjacentLength, params char[] characters)
			=> NormalizeAdjacent(source, maxAdjacentLength, System.Collections.Generic.EqualityComparer<char>.Default, characters);
	}
}
