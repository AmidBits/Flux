namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer.</summary>
		public static bool EndsWith<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
			=> ExtensionMethods.EndsWith(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), equalityComparer);
		/// <summary>Indicates whether the sequence ends with the other sequence. Uses the default comparer.</summary>
		public static bool EndsWith<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target)
			=> ExtensionMethods.EndsWith(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), System.Collections.Generic.EqualityComparer<T>.Default);

		/// <summary>Indicates whether the sequence starts with the other sequence. Uses the specified comparer.</summary>
		public static bool StartsWith<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
			=> ExtensionMethods.StartsWith(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), equalityComparer);
		/// <summary>Indicates whether the sequence starts with the other sequence. Uses the default comparer.</summary>
		public static bool StartsWith<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target)
			=> ExtensionMethods.StartsWith(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target), System.Collections.Generic.EqualityComparer<T>.Default);
	}
}
