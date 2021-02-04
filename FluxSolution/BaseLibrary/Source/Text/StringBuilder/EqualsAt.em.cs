namespace Flux
{
	public static partial class SystemTextEm
	{
		/// <summary>Determines whether this instance has the same content as the specified value. The comparison starts at the specified character index and examines the specified number of characters. Uses the specified comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int startIndex, int count, System.Collections.Generic.IEqualityComparer<char> comparer, System.ReadOnlySpan<char> value)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			if (startIndex < 0 || startIndex >= source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
			if (count <= 0 || startIndex + count >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var sourceIndex = startIndex + count;
			var valueIndex = count;

			while (--sourceIndex >= 0 && --valueIndex >= 0)
				if (!comparer.Equals(source[sourceIndex], value[valueIndex]))
					break;

			if (sourceIndex < 0 && valueIndex < 0) return true;

			return false;
		}
		/// <summary>Determines whether this instance has the same content as specified value. The comparison starts at the specified character index and examines the specified number of characters. Uses the default comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int startAt, int count, System.ReadOnlySpan<char> value)
			=> EqualsAt(source, startAt, count, System.Collections.Generic.EqualityComparer<char>.Default, value);
		/// <summary>Determines whether this instance has the same content as specified value. The comparison starts at the specified character index and examines all characters. Uses the specified comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int startAt, System.Collections.Generic.IEqualityComparer<char> comparer, System.ReadOnlySpan<char> value)
			=> EqualsAt(source, startAt, value.Length, comparer, value);
		/// <summary>Determines whether this instance has the same content as specified value. The comparison starts at the specified character index and examines all characters. Uses the default comparer.</summary>
		public static bool EqualsAt(this System.Text.StringBuilder source, int startAt, System.ReadOnlySpan<char> value)
			=> EqualsAt(source, startAt, value.Length, System.Collections.Generic.EqualityComparer<char>.Default, value);
	}
}
