namespace Flux.Dsp
{
  //public struct MonoSample
  //  : IChannelFc, System.IEquatable<MonoSample>
  //{
  //  public double FrontCenter { get; }

  //  public MonoSample(in double frontCenter)
  //  {
  //    FrontCenter = frontCenter;
  //  }

  //  public StereoSample ToStereo()
  //    => new StereoSample(FrontCenter, FrontCenter);

  //  #region Static functions
  //  public static StereoSample ToStereoSample(MonoSample mono)
  //    => new StereoSample(mono.FrontCenter, mono.FrontCenter);
  //  //public static ISampleQuad ToQuadSample(in ISampleMono mono)
  //  //  => new QuadSample(mono.FrontCenter, mono.FrontCenter, mono.FrontCenter, mono.FrontCenter);
  //  #endregion Static functions

  //  // Operators
  //  public static bool operator ==(in MonoSample a, in MonoSample b)
  //    => a.Equals(b);
  //  public static bool operator !=(in MonoSample a, in MonoSample b)
  //    => !a.Equals(b);
  //  //public static implicit operator double(MonoSample sample)
  //  //  => sample.FrontCenter;
  //  //public static implicit operator MonoSample(in double sample)
  //  //  => new MonoSample(sample);
  //  //public static implicit operator StereoSample(in MonoSample mono)
  //  //  => new StereoSample(mono.FrontCenter, mono.FrontCenter);
  //  //public static implicit operator QuadSample(in MonoSample mono)
  //  //  => new QuadSample(mono.FrontCenter, mono.FrontCenter, mono.FrontCenter, mono.FrontCenter);
  //  // IEquatable<T>
  //  public bool Equals(MonoSample other)
  //    => FrontCenter.Equals(other.FrontCenter);
  //  // Object overrides
  //  public override bool Equals(object? obj)
  //    => obj is MonoSample sample && Equals(sample);
  //  public override int GetHashCode()
  //    => FrontCenter.GetHashCode();
  //  public override string ToString()
  //    => $"<Fc:{FrontCenter}>";
  //}
}
