namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
		/// <param name="minLength">The smaller length of the two spans.</param>
		public static int CountEqualAtEnd<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int minLength)
			=> ExtensionMethods.CountEqualAtEnd(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), equalityComparer, out minLength);
		/// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
		/// <param name="minLength">The smaller length of the two spans.</param>
		public static int CountEqualAtEnd<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target, out int minLength)
			=> ExtensionMethods.CountEqualAtEnd(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), System.Collections.Generic.EqualityComparer<T>.Default, out minLength);

		/// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
		/// <param name="minLength">The smaller length of the two spans.</param>
		public static int CountEqualAtStart<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int minLength)
			=> ExtensionMethods.CountEqualAtStart(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), equalityComparer, out minLength);
		/// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
		/// <param name="minLength">The smaller length of the two spans.</param>
		public static int CountEqualAtStart<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target, out int minLength)
			=> ExtensionMethods.CountEqualAtStart(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), System.Collections.Generic.EqualityComparer<T>.Default, out minLength);

		/// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
		public static int CountEqualOnBothSides<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer)
			=> ExtensionMethods.CountEqualOnBothSides(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), equalityComparer);
		/// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
		public static int CountEqualOnBothSides<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target)
			=> ExtensionMethods.CountEqualOnBothSides(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), System.Collections.Generic.EqualityComparer<T>.Default);
	}
}
