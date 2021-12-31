namespace Flux.Dsp
{
  public struct SampleMono
    : System.IEquatable<SampleMono>
    , IAudioChannelFrontCenter
  {
    public readonly static SampleMono Silent;

    public double FrontCenter { get; }

    public SampleMono(double frontCenter)
    {
      FrontCenter = frontCenter;
    }

    public SampleStereo ToStereo()
      => new(FrontCenter, FrontCenter);

    #region Overloaded operators
    public static implicit operator double(SampleMono value)
      => value.FrontCenter;
    public static implicit operator SampleMono(double value)
      => new(value);

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
      => $"{GetType().Name} {{ FC = {FrontCenter} }}";
    #endregion Object overrides
  }
}
