namespace Flux
{
	public static partial class SystemTextStringBuilderEm
	{
		/// <summary>Determines whether the specified <paramref name="count"/> of characters from the <paramref name="target"/> can be found in the <paramref name="source"/> at the specified <paramref name="startIndex"/>. Uses the specified comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int startIndex, System.ReadOnlySpan<char> target, int count, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			if (startIndex < 0 || startIndex >= source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
			if (count <= 0 || (startIndex + count) >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

			var sourceIndex = startIndex + count;
			var targetIndex = count;

			if (sourceIndex > source.Length || targetIndex > target.Length)
				return false; // If beyond either ends, there can be no equality.

			while (--sourceIndex >= 0 && --targetIndex >= 0)
				if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
					break;

			if (targetIndex < 0)
				return true; // If target index reached negative, there was a match.

			return false;
		}
		/// <summary>Determines whether the specified <paramref name="count"/> of characters from the <paramref name="target"/> can be found in the <paramref name="source"/> at the specified <paramref name="startIndex"/>. Uses the default comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int startIndex, System.ReadOnlySpan<char> target, int count)
			=> EqualsAt(source, startIndex, target, count, System.Collections.Generic.EqualityComparer<char>.Default);
		/// <summary>Determines whether the <paramref name="target"/> can be found in the <paramref name="source"/> at the specified <paramref name="startIndex"/>. Uses the specified comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int startIndex, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char> comparer)
			=> EqualsAt(source, startIndex, target, target.Length, comparer);
		/// <summary>Determines whether the <paramref name="target"/> can be found in the <paramref name="source"/> at the specified <paramref name="startIndex"/>. Uses the default comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int startIndex, System.ReadOnlySpan<char> target)
			=> EqualsAt(source, startIndex, target, target.Length, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
