namespace Flux.Numerics
{
	public interface INumberSequence<T>
		: System.Collections.Generic.IEnumerable<T>
	{
		System.Collections.Generic.IEnumerable<T> GetSequence();
	}
}
