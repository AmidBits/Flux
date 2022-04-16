namespace Flux
{
	public static partial class StringBuilderEm
	{
		/// <summary>Indicates whether the source starts with value. Uses the specified comparer.</summary>
		public static bool StartsWith(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			var sourceLength = source.Length;
			var targetLength = target.Length;

			if (sourceLength < targetLength)
				return false;

			for (var index = targetLength - 1; index >= 0; index--)
				if (!equalityComparer.Equals(source[index], target[index]))
					return false;

			return true;
		}
		/// <summary>Indicates whether the source starts with value. Uses the default comparer.</summary>
		public static bool StartsWith(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
			 => StartsWith(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
