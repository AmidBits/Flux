namespace Flux.Numerics
{
	public abstract class ANumberSequenceable<T>
		: INumberSequenceable<T>
  {
		public abstract System.Collections.Generic.IEnumerable<T> GetNumberSequence();

		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
			=> GetNumberSequence().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}
}
