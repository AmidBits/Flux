namespace Flux
{
	public static partial class SystemTextStringBuilderEm
	{
		/// <summary>Returns the last index of any of the specified characters. Or -1 if none were found.</summary>
		public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var targetsCount = targets.Count;

			for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
				if (source[sourceIndex] is var sourceChar)
					for (var targetsIndex = 0; targetsIndex < targetsCount; targetsIndex++) // Favor targets in list order.
					if (comparer.Equals(sourceChar, targets[targetsIndex]))
						return sourceIndex;

			return -1;
		}
		/// <summary>Returns the last index of any of the specified characters. Or -1 if none were found.</summary>
		public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets)
			=> LastIndexOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Returns the last index of any of the specified values. or -1 if none is found.</summary>
		public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<string> targets, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var targetsCount = targets.Count;

			for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
				for (var targetsIndex = 0; targetsIndex < targetsCount; targetsIndex++) // Favor targets in list order.
					if (EqualsAt(source, sourceIndex, targets[targetsIndex], comparer))
						return sourceIndex;

			return -1;
		}
		/// <summary>Returns the last index of any of the specified values. or -1 if none is found.</summary>
		public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<string> targets)
			=> LastIndexOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}