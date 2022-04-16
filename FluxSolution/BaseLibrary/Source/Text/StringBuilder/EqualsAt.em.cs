namespace Flux
{
	public static partial class StringBuilderEm
	{
		/// <summary>Returns whether the specified part of the target is found at the specified index in the string, using the specified comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int sourceIndex, System.ReadOnlySpan<char> target, int targetIndex, int length, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			if (sourceIndex < 0 || targetIndex < 0 || length <= 0 || sourceIndex + length > source.Length || targetIndex + length > target.Length)
				return false;

			while (length-- > 0)
				if (!equalityComparer.Equals(source[sourceIndex++], target[targetIndex++]))
					return false;

			return true;
		}
		/// <summary>Returns whether the specified part of the target is found at the specified index in the string, using the default comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int sourceIndex, System.ReadOnlySpan<char> target, int targetIndex, int length)
			=> EqualsAt(source, sourceIndex, target, targetIndex, length, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Returns whether the specified target is found at the specified index in the string, using the specified comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int sourceIndex, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char> comparer)
			=> EqualsAt(source, sourceIndex, target, 0, target.Length, comparer);
		/// <summary>Returns whether the specified target is found at the specified index in the string, using the default comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int sourceIndex, System.ReadOnlySpan<char> target)
			=> EqualsAt(source, sourceIndex, target, 0, target.Length, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
