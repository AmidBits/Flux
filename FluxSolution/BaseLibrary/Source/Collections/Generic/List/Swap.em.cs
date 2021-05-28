namespace Flux
{
	public static partial class SystemCollectionsGenericListEm
	{
		/// <summary>Swap two elements by the specified indices.</summary>
		public static void Swap<T>(this System.Collections.Generic.List<T> source, int indexA, int indexB)
			=> SystemSpanEm.Swap(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), indexA, indexB);
		public static void SwapFirstWith<T>(this System.Collections.Generic.List<T> source, int index)
			=> SystemSpanEm.Swap(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), 0, index);
		public static void SwapLastWith<T>(this System.Collections.Generic.List<T> source, int index)
			=> SystemSpanEm.Swap(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source), index, (source ?? throw new System.ArgumentNullException(nameof(source))).Count - 1);
	}
}
