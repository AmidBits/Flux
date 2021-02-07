using System.Linq;

namespace Flux
{
	public static partial class SystemTextStringBuilderEm
	{
		/// <summary>Returns the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var index = 0; index < source.Length; index++)
				if (source[index] is var character && targets.Any(c => comparer.Equals(c, character)))
					return index;

			return -1;
		}
		/// <summary>Reports the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the default comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets)
			=> IndexOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Reports the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<string> targets, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var valueIndex = 0; valueIndex < targets.Count; valueIndex++)
				if (IndexOf(source, targets[valueIndex], comparer) is var index && index > -1)
					return index;

			return -1;
		}
		/// <summary>Reports the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the default comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<string> targets)
			=> IndexOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
