namespace Flux.Numerics
{
	public interface ISequencedNumbers<T>
		: System.Collections.Generic.IEnumerable<T>
	{
		System.Collections.Generic.IEnumerable<T> GetNumberSequence();
	}

	public abstract class ASequencedNumbers<T>
		: ISequencedNumbers<T>
  {
		public abstract System.Collections.Generic.IEnumerable<T> GetNumberSequence();

		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
			=> GetNumberSequence().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}
}
