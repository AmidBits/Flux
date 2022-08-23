namespace Flux.Numerics
{
	public interface INumberSequenceable<T>
		: System.Collections.Generic.IEnumerable<T>
  {
		System.Collections.Generic.IEnumerable<T> GetNumberSequence();
	}
}
