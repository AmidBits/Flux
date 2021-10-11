using System.Linq;

namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Returns the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> equalityComparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			var sourceLength = source.Length;
			var targetsCount = targets.Count;

			for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
				if (source[sourceIndex] is var sourceChar)
					for (var targetsIndex = 0; targetsIndex < targetsCount; targetsIndex++) // Favor targets in list order.
						if (equalityComparer.Equals(sourceChar, targets[targetsIndex]))
							return sourceIndex;

			return -1;
		}
		/// <summary>Reports the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the default comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets)
			=> IndexOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Reports the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<string> targets, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> equalityComparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			var sourceLength = source.Length;
			var targetsCount = targets.Count;

			for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
				for (var targetsIndex = 0; targetsIndex < targetsCount; targetsIndex++) // Favor targets in list order.
					if (EqualsAt(source, sourceIndex, targets[targetsIndex], equalityComparer))
						return sourceIndex;

			return -1;
		}
		/// <summary>Reports the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the default comparer.</summary>
		public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<string> targets)
			=> IndexOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
