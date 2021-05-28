namespace Flux
{
	public static partial class SystemCollectionsGenericListEm
	{
		/// <summary>Returns a random element from the list in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
		public static bool RandomElement<T>(this System.Collections.Generic.List<T> source, out T result, System.Random rng)
			=> SystemSpanEm.RandomElement(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), out result, rng);
		/// <summary>Returns a random element from the list in the output variable. Uses the .NET cryptographic random number generator.</summary>
		public static bool RandomElement<T>(this System.Collections.Generic.List<T> source, out T result)
			=> SystemSpanEm.RandomElement(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), out result, Flux.Random.NumberGenerator.Crypto);
	}
}
