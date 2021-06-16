namespace Flux.Dsp
{
	public struct SampleStereo
		: System.IEquatable<SampleStereo>
		, IAudioChannelFl, IAudioChannelFr
	{
		public static readonly SampleStereo Empty;
		public bool IsEmpty => Equals(Empty);

		public double FrontLeft { get; }
		public double FrontRight { get; }

		public SampleStereo(in double frontLeft, in double frontRight)
		{
			FrontLeft = frontLeft;
			FrontRight = frontRight;
		}
		public SampleStereo(in double monoSample)
			: this(monoSample, monoSample)
		{ }

		public double ToMono()
			=> (FrontLeft + FrontRight) / 2;

		// Operators
		public static bool operator ==(in SampleStereo a, in SampleStereo b)
			=> a.Equals(b);
		public static bool operator !=(in SampleStereo a, in SampleStereo b)
			=> !a.Equals(b);

		// IEquatable<T>
		public bool Equals(SampleStereo other)
			=> FrontLeft == other.FrontLeft && FrontRight == other.FrontRight;

		// Object overrides
		public override bool Equals(object? obj)
			=> obj is SampleStereo sample && Equals(sample);
		public override int GetHashCode()
			=> System.HashCode.Combine(FrontLeft, FrontRight);
		public override string ToString()
			=> $"<Fl:{FrontLeft}, Fr:{FrontRight}>";
	}
}
