namespace Flux
{
	public static partial class SystemTextEm
	{
		/// <summary>Yields the number of characters that the source and the target have in common at the end.</summary>
		public static int CountEqualAtEnd(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var sourceIndex = source.Length;
			var targetIndex = target.Length;

			var minLength = System.Math.Min(sourceIndex, targetIndex);

			for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
				if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
					return atEnd;

			return minLength;
		}
		/// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
		public static int CountEqualAtEnd(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
			=> CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
