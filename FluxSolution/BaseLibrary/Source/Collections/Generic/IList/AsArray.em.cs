namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Returns the IList cast as an array. No allocations are needed.</summary>
		public static T[] AsArray<T>(this System.Collections.Generic.IList<T> source)
			=> (T[])(source ?? throw new System.ArgumentNullException(nameof(source)));
	}
}
