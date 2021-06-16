namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Reports the first index of the specified character within the string builder, or -1 if not found. Uses the specified comparer.</summary>
		public static int IndexOf(this System.Text.StringBuilder source, char value, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var sourceLength = source.Length;

			for (var index = 0; index < sourceLength; index++)
				if (comparer.Equals(source[index], value))
					return index;

			return -1;
		}
		/// <summary>Reports the first index of the specified character within the string builder, or -1 if not found. Uses the default comparer.</summary>
		public static int IndexOf(this System.Text.StringBuilder source, char target)
			=> IndexOf(source, target, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Returns the first index of the specified string within the string builder, or -1 if not found. Uses the specified comparer.</summary>
		public static int IndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var targetLength = target.Length;

			for (int index = 0, lastIndex = source.Length - targetLength; index <= lastIndex; index++)
				if (EqualsAt(source, index, target, 0, targetLength, comparer))
					return index;

			return -1;
		}
		/// <summary>Reports the first index of the specified string within the string builder, or -1 if not found. Uses the default comparer.</summary>
		public static int IndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
			=> IndexOf(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
