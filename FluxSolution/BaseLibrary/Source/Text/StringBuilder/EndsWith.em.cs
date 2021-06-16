namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Indicates whether the source ends with value. Uses the specified comparer.</summary>
		public static bool EndsWith(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var sourceIndex = source.Length;
			var targetIndex = target.Length;

			if (sourceIndex < targetIndex)
				return false;

			while (--sourceIndex >= 0 && --targetIndex >= 0)
				if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
					return false;

			return true;
		}
		/// <summary>Indicates whether the source ends with value. Uses the default comparer.</summary>
		public static bool EndsWith(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
			=> EndsWith(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
