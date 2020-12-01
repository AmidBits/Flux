namespace Flux
{
	public static partial class Xtensions
	{
		/// <summary>Returns the IList cast as a ReadOnlySpan. No allocations are needed.</summary>
		public static T[] AsArray<T>(this System.Collections.Generic.IList<T> source)
			=> (T[])(source ?? throw new System.ArgumentNullException(nameof(source)));
		/// <summary>Returns the IList cast as a ReadOnlySpan. No allocations are needed.</summary>
		public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this System.Collections.Generic.IList<T> source)
			=> (T[])(source ?? throw new System.ArgumentNullException(nameof(source)));
		/// <summary>Returns the IList cast as a Span. No allocations are needed.</summary>
		public static System.Span<T> AsSpan<T>(this System.Collections.Generic.IList<T> source)
			=> (T[])(source ?? throw new System.ArgumentNullException(nameof(source)));
	}
}
