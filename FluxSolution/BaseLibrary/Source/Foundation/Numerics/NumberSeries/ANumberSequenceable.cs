namespace Flux.Numerics
{
	public abstract class ANumberSequenceable<T>
		: INumberSequenceable<T>
  {
		[System.Diagnostics.Contracts.Pure]
		public abstract System.Collections.Generic.IEnumerable<T> GetNumberSequence();

		[System.Diagnostics.Contracts.Pure]
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
			=> GetNumberSequence().GetEnumerator();
		[System.Diagnostics.Contracts.Pure]
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}
}
