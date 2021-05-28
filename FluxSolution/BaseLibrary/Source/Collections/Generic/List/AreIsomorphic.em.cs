namespace Flux
{
	public static partial class SystemCollectionsGenericListEm
	{
		/// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
		/// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
		public static bool AreIsomorphic<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
			where T : notnull
			=> SystemSpanEm.AreIsomorphic(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), System.Collections.Generic.EqualityComparer<T>.Default);
		/// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
		/// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
		public static bool AreIsomorphic<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target)
			where T : notnull
			=> SystemSpanEm.AreIsomorphic(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), System.Collections.Generic.EqualityComparer<T>.Default);
	}
}
