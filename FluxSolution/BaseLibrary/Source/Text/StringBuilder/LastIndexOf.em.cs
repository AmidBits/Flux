namespace Flux
{
	public static partial class SystemTextStringBuilderEm
	{
		/// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
		public static int LastIndexOf(this System.Text.StringBuilder source, char target, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var index = source.Length - 1; index >= 0; index--)
				if (comparer.Equals(source[index], target))
					return index;

			return -1;
		}
		/// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
		public static int LastIndexOf(this System.Text.StringBuilder source, char target)
			=> LastIndexOf(source, target, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
		public static int LastIndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (int index = source.Length - target.Length; index >= 0; index--)
				if (Equals(source, index, target, comparer))
					return index;

			return -1;
		}
		/// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
		public static int LastIndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
			=> LastIndexOf(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
