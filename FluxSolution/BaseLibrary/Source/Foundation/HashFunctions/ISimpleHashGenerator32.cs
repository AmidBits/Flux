namespace Flux.Hashing
{
	public interface ISimpleHashGenerator32
	{
		/// <summary>The current hash value.</summary>
		int SimpleHash32 { get; }
		/// <summary>Continue generating a simple hash value.</summary>
		int GenerateSimpleHash32(byte[] bytes, int startAt, int count);
	}
}
