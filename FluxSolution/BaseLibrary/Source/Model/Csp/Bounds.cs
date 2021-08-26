namespace Flux.Csp
{
	public struct Bounds<T>
	{
		public T LowerBound;
		public T UpperBound;

		public Bounds(T lowerBound, T upperBound)
		{
			this.LowerBound = lowerBound;
			this.UpperBound = upperBound;
		}
	}
}
