namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
		public static int CountEqualOnBothSides<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
			=> source.CountEqualAtStart(target, equalityComparer, out _) + source.CountEqualAtEnd(target, equalityComparer, out _);
		/// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
		public static int CountEqualOnBothSides<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
			=> CountEqualOnBothSides(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
	}
}
