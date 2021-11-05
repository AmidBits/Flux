namespace Flux.Dsp
{
  public struct SampleStereo
    : System.IEquatable<SampleStereo>
    , IAudioChannelFrontLeft, IAudioChannelFrontRight
  {
    public readonly static SampleStereo Zero;

    public double FrontLeft { get; }
    public double FrontRight { get; }

    public SampleStereo(in double frontLeft, in double frontRight)
    {
      FrontLeft = frontLeft;
      FrontRight = frontRight;
    }
    public SampleStereo(in double frontCenter)
      : this(frontCenter, frontCenter)
    { }

    public SampleMono ToMono()
      => new(ConvertStereoToMono(FrontLeft, FrontRight));

    #region Static methods
    public static double ConvertStereoToMono(double frontLeft, double frontRight)
      => (frontLeft + frontRight) / 2;
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(in SampleStereo a, in SampleStereo b)
      => a.Equals(b);
    public static bool operator !=(in SampleStereo a, in SampleStereo b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<T>
    public bool Equals(SampleStereo other)
      => FrontLeft == other.FrontLeft && FrontRight == other.FrontRight;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is SampleStereo o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(FrontLeft, FrontRight);
    public override string ToString()
      => $"<Fl:{FrontLeft}, Fr:{FrontRight}>";
    #endregion Object overrides
  }
}
