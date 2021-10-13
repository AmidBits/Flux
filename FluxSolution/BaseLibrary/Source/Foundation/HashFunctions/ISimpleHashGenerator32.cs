namespace Flux.Hashing
{
	public interface ISimpleHashGenerator32
	{
		int SimpleHash32 { get; }
		int GenerateSimpleHash32(byte[] bytes, int startAt, int count);
	}
}
