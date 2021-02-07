namespace Flux
{
	public static partial class SystemTextStringBuilderEm
	{
		/// <summary>Indicates whether the source starts with value. Uses the specified comparer.</summary>
		public static bool StartsWith(this System.Text.StringBuilder source, string target, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (target is null) throw new System.ArgumentNullException(nameof(target));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var sourceLength = source.Length;
			var targetLength = target.Length;

			if (sourceLength < targetLength)
				return false;

			for (var index = 0; index < targetLength; index++)
				if (!comparer.Equals(source[index], target[index]))
					return false;

			return true;
		}
		/// <summary>Indicates whether the source starts with value. Uses the default comparer.</summary>
		public static bool StartsWith(this System.Text.StringBuilder source, string target)
			 => StartsWith(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
