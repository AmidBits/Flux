using System.Linq;

namespace Flux
{
	public static partial class StringBuilderEm
	{
		/// <summary>Determines whether <paramref name="count"/> characters from any of the specified <paramref name="targets"/> is found in the <paramref name="source"/> at the specified <paramref name="startIndex"/>. Uses the specified comparer.</summary>
		public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startIndex, int count, System.Collections.Generic.IList<string> targets, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			if (startIndex < 0 || startIndex >= source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
			if (count <= 0 || (startIndex + count) >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

			foreach (var target in targets)
			{
				if (target.Length < count)
					continue; // Target has less characters than requested, which means not equal.

				var sourceIndex = startIndex + count;
				var targetIndex = count;

				while (--sourceIndex >= 0 && --targetIndex >= 0)
					if (!equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
						break;

				if (targetIndex < 0)
					return true; // If target index reached negative, there was a match.
			}

			return false;
		}
		public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startIndex, int count, System.Collections.Generic.IList<string> targets)
			=> EqualsAnyAt(source, startIndex, count, targets, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}