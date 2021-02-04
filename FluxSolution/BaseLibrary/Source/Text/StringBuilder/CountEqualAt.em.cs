namespace Flux
{
	public static partial class SystemTextEm
	{
		/// <summary>Yields the number of characters that the source and the target have in common from the specified respective indices. Uses the specified comparer.</summary>
		public static int CountEqualAt(this System.Text.StringBuilder source, int sourceStartIndex, System.ReadOnlySpan<char> target, int targetStartIndex, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (sourceStartIndex < 0 || sourceStartIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceStartIndex));
			if (targetStartIndex < 0 || targetStartIndex >= target.Length) throw new System.ArgumentOutOfRangeException(nameof(targetStartIndex));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var minLength = System.Math.Min(source.Length - sourceStartIndex, target.Length - targetStartIndex);

			var count = 0;
			while (count < minLength && comparer.Equals(source[sourceStartIndex++], target[targetStartIndex++]))
				count++;
			return count;
		}
		/// <summary>Yields the number of characters that the source and the target have in common from the specified respective indices. Uses the default comparer.</summary>
		public static int CountEqualAt(this System.Text.StringBuilder source, int sourceStartIndex, System.ReadOnlySpan<char> target, int targetStartIndex)
			=> CountEqualAt(source, sourceStartIndex, target, targetStartIndex, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
