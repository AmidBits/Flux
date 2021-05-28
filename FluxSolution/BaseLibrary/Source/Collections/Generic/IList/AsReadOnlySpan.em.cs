namespace Flux
{
	public static partial class SystemCollectionsGenericIListEm
	{
		/// <summary>Returns the IList cast as a ReadOnlySpan. No allocations are needed.</summary>
		public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this System.Collections.Generic.IList<T> source)
			=> (T[])(source ?? throw new System.ArgumentNullException(nameof(source)));
	}
}
