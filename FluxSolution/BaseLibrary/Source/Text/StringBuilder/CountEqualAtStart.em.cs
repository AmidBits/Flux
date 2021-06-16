namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Yields the number of characters that the source and the target have in common from the start.</summary>
		public static int CountEqualAtStart(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var minLength = System.Math.Min(source.Length, target.Length);

			var index = 0;
			while (index < minLength && comparer.Equals(source[index], target[index]))
				index++;
			return index;
		}
		/// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
		public static int CountEqualAtStart(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
			=> CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
