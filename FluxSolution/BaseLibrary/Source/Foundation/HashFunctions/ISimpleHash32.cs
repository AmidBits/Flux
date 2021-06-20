namespace Flux.Hashing
{
	public interface ISimpleHash32
	{
		//int Code { get; }
		int ComputeSimpleHash32(byte[] bytes, int startAt, int count);
	}
}
