namespace Flux.Dsp
{
  public struct SampleMono
    : System.IEquatable<SampleMono>
    , IAudioChannelFc
  {
    public static SampleMono Silent
      => new SampleMono();

    public double FrontCenter { get; }

    public SampleMono(double frontCenter)
    {
      FrontCenter = frontCenter;
    }

    public SampleStereo ToStereo()
      => new SampleStereo(FrontCenter, FrontCenter);

    #region Static methods
    public static double ConvertStereoToMono(double frontLeft, double frontRight)
      => (frontLeft + frontRight) / 2;
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(in SampleMono a, in SampleMono b)
      => a.Equals(b);
    public static bool operator !=(in SampleMono a, in SampleMono b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(SampleMono other)
      => FrontCenter.Equals(other.FrontCenter);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is SampleMono o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(FrontCenter);
    public override string ToString()
      => $"<Fc:{FrontCenter}>";
    #endregion Object overrides
  }
}
