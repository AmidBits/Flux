namespace Flux.Media.Dsp
{
	public struct SampleMono
		: System.IEquatable<SampleMono>
		, IAudioChannelFc
	{
		public static readonly SampleMono Empty;
		public bool IsEmpty => Equals(Empty);

		public double FrontCenter { get; }

		public SampleMono(in double frontCenter)
		{
			FrontCenter = frontCenter;
		}
		public SampleMono(in double frontLeft, in double frontRight)
			: this((frontLeft + frontRight) / 2)
		{ }

		public SampleStereo ToStereo()
			=> new SampleStereo(FrontCenter, FrontCenter);

		// Operators
		public static bool operator ==(in SampleMono a, in SampleMono b)
			=> a.Equals(b);
		public static bool operator !=(in SampleMono a, in SampleMono b)
			=> !a.Equals(b);

		// IEquatable<T>
		public bool Equals(SampleMono other)
			=> FrontCenter.Equals(other.FrontCenter);

		// Object overrides
		public override bool Equals(object? obj)
			=> obj is SampleMono sample && Equals(sample);
		public override int GetHashCode()
			=> FrontCenter.GetHashCode();
		public override string ToString()
			=> $"<Fc:{FrontCenter}>";
	}
}
