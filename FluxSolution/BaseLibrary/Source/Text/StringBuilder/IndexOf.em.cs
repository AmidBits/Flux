namespace Flux
{
	public static partial class SystemTextStringBuilderEm
	{
		/// <summary>Reports the first index of the specified character within the string builder, or -1 if not found. Uses the specified comparer.</summary>
		public static int IndexOf(this System.Text.StringBuilder source, char value, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var index = 0; index < source.Length; index++)
				if (comparer.Equals(source[index], value))
					return index;

			return -1;
		}
		/// <summary>Reports the first index of the specified character within the string builder, or -1 if not found. Uses the default comparer.</summary>
		public static int IndexOf(this System.Text.StringBuilder source, char value)
			=> IndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Returns the first index of the specified string within the string builder, or -1 if not found. Uses the specified comparer.</summary>
		public static int IndexOf(this System.Text.StringBuilder source, string value, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var index = 0; index < source.Length; index++)
				if (source.Equals(index, value, 0, value?.Length ?? throw new System.ArgumentNullException(nameof(value)), comparer)) return index;
				else if (source.Length - index < value.Length) break;

			return -1;
		}
		/// <summary>Reports the first index of the specified string within the string builder, or -1 if not found. Uses the default comparer.</summary>
		public static int IndexOf(this System.Text.StringBuilder source, string value)
			=> IndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
