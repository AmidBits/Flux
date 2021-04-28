namespace Flux
{
	public interface ISequenceInfinite<T>
		: System.Collections.Generic.IEnumerable<T>
	{
		System.Collections.Generic.IEnumerable<T> GetSequence();
	}
}
