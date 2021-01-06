namespace Flux
{
	public static partial class SystemTextEm
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

		/// <summary>Returns the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer, params char[] values)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var index = 0; index < source.Length; index++)
				if (source[index] is var character && System.Array.Exists(values, c => comparer.Equals(c, character)))
					return index;

			return -1;
		}
		/// <summary>Reports the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the default comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, params char[] values)
			=> IndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);

		/// <summary>Reports the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
				if (IndexOf(source, values[valueIndex], comparer) is var index && index > -1)
					return index;

			return -1;
		}
		/// <summary>Reports the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the default comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, params string[] values)
			=> IndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);

		/// <summary>Reports all first indices of the specified characters within the string builder (-1 if not found). Uses the specified comparer.</summary>
		public static int[] IndicesOf(this System.Text.StringBuilder source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer, params char[] values)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var indices = new int[values.Length];

			System.Array.Fill(indices, -1);

			for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
			{
				var sourceChar = source[sourceIndex];

				for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
				{
					if (indices[valueIndex] == -1 && comparer.Equals(sourceChar, values[valueIndex]))
					{
						indices[valueIndex] = sourceIndex;

						if (!System.Array.Exists(indices, i => i == -1))
							return indices;
					}
				}
			}

			return indices;
		}
		/// <summary>Reports all first indices of the specified characters within the string builder (-1 if not found). Uses the default comparer.</summary>
		public static int[] IndicesOf(this System.Text.StringBuilder source, params char[] values)
			=> IndicesOf(source, System.Collections.Generic.EqualityComparer<char>.Default, values);
	}
}
