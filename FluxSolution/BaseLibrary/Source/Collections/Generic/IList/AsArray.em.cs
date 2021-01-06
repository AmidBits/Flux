namespace Flux
{
	public static partial class SystemCollectionsGenericEm
	{
		/// <summary>Returns the IList cast as a ReadOnlySpan. No allocations are needed.</summary>
		public static T[] AsArray<T>(this System.Collections.Generic.IList<T> source)
			=> (T[])(source ?? throw new System.ArgumentNullException(nameof(source)));
		/// <summary>Returns the IList cast as a ReadOnlySpan. No allocations are needed.</summary>
	}
}
