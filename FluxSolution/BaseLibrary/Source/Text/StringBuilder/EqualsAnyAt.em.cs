using System.Linq;

namespace Flux
{
	public static partial class SystemTextEm
	{
		/// <summary>Determines whether <paramref name="count"/> characters from any of the specified <paramref name="targets"/> is found in the <paramref name="source"/> at the specified <paramref name="startIndex"/>. Uses the specified comparer.</summary>
		public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startIndex, int count, System.Collections.Generic.IEnumerable<string> targets, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			if (startIndex < 0 || startIndex >= source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
			if (count <= 0 || (startIndex + count) >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

			foreach (var target in targets)
			{
				if (target.Length < count)
					continue; // Target has less characters than requested, which means not equal.

				var sourceIndex = startIndex + count;
				var targetIndex = count;

				while (--sourceIndex >= 0 && --targetIndex >= 0)
					if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
						break;

				if (sourceIndex < 0 && targetIndex < 0) return true;
			}

			return false;
		}
		public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startIndex, int count, System.Collections.Generic.IEnumerable<string> targets)
			=> EqualsAnyAt(source, startIndex, count, targets, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
