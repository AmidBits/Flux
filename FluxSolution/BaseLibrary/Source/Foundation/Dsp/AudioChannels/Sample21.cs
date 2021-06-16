namespace Flux.Dsp
{
	public struct Sample21
		: System.IEquatable<Sample21>
		, IAudioChannelFl, IAudioChannelFr, IAudioChannelLfe
	{
		public double FrontLeft { get; }
		public double FrontRight { get; }
		public double LowFrequency { get; }

		public Sample21(in double frontLeft, in double frontRight, in double lowFrequency)
		{
			FrontLeft = frontLeft;
			FrontRight = frontRight;
			LowFrequency = lowFrequency;
		}
		public Sample21(in SampleStereo stereo, in double lowFrequency)
			: this(stereo.FrontLeft, stereo.FrontRight, lowFrequency)
		{ }

		// Operators
		public static bool operator ==(in Sample21 a, in Sample21 b)
			=> a.Equals(b);
		public static bool operator !=(in Sample21 a, in Sample21 b)
			=> !a.Equals(b);

		// IEquatable<T>
		public bool Equals(Sample21 other)
			=> FrontLeft.Equals(other.FrontLeft) && FrontRight.Equals(other.FrontRight) && LowFrequency.Equals(other.LowFrequency);

		// Object overrides
		public override bool Equals(object? obj)
			=> obj is Sample51 sample && sample.Equals(this);
		public override int GetHashCode()
			=> System.HashCode.Combine(FrontLeft, FrontRight, LowFrequency);
		public override string ToString()
			=> $"<Fl:{FrontLeft}, Fr:{FrontRight}, Lfe:{LowFrequency}>";
	}
}
