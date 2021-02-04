using System.Linq;

namespace Flux
{
	public static partial class SystemTextEm
	{
		/// <summary>Determines whether this instance has the same value as any in the specified sequence when compared using the specified comparison option. The comparison starts at a specified character position and examines a specified number of characters.</summary>
		public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startIndex, int count, System.Collections.Generic.IEqualityComparer<char> comparer, System.Collections.Generic.IEnumerable<string> values)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (values is null) throw new System.ArgumentNullException(nameof(values));

			if (startIndex < 0 || startIndex >= source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
			if (count <= 0 || startIndex + count >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			foreach (var value in values)
			{
				if (value.Length < count)
					continue; // This value cannot possibly be the same, since it does not have enought characters.

				var sourceIndex = startIndex + count;
				var valueIndex = count;

				while (--sourceIndex >= 0 && --valueIndex >= 0)
					if (!comparer.Equals(source[sourceIndex], value[valueIndex]))
						break;

				if (sourceIndex < 0 && valueIndex < 0) return true;
			}

			return false;
		}
		public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startIndex, int count, System.Collections.Generic.IEnumerable<string> values)
			=> EqualsAnyAt(source, startIndex, count, System.Collections.Generic.EqualityComparer<char>.Default, values);
	}
}
