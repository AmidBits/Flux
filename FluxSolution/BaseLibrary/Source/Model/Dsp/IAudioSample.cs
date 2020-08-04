namespace Flux.Dsp
{
  public interface IChannelBl
  {
    double BackLeft { get; }
  }
  public interface IChannelBr
  {
    double BackRight { get; }
  }
  public interface IChannelFc
  {
    double FrontCenter { get; }
  }
  public interface IChannelFl
  {
    double FrontLeft { get; }
  }
  public interface IChannelFr
  {
    double FrontRight { get; }
  }

  public interface ISample
  {
  }

  public interface ISampleMono
    : ISample, IChannelFc
  {
  }

  public interface ISampleStereo
    : ISample, IChannelFl, IChannelFr
  {
  }

  //public interface ISampleQuad
  //  : ISample, IChannelBl, IChannelBr, IChannelFl, IChannelFr
  //{
  //}

  public struct MonoSample
    : ISampleMono, System.IEquatable<MonoSample>
  {
    public double FrontCenter { get; }

    public MonoSample(in double frontCenter)
    {
      FrontCenter = frontCenter;
    }

    #region Static functions
    public static ISampleStereo ToStereoSample(in ISampleMono mono)
      => new StereoSample(mono.FrontCenter, mono.FrontCenter);
    //public static ISampleQuad ToQuadSample(in ISampleMono mono)
    //  => new QuadSample(mono.FrontCenter, mono.FrontCenter, mono.FrontCenter, mono.FrontCenter);
    #endregion Static functions

    // Operators
    public static bool operator ==(in MonoSample a, in MonoSample b)
      => a.Equals(b);
    public static bool operator !=(in MonoSample a, in MonoSample b)
      => !a.Equals(b);
    //public static implicit operator double(MonoSample sample)
    //  => sample.FrontCenter;
    //public static implicit operator MonoSample(in double sample)
    //  => new MonoSample(sample);
    //public static implicit operator StereoSample(in MonoSample mono)
    //  => new StereoSample(mono.FrontCenter, mono.FrontCenter);
    //public static implicit operator QuadSample(in MonoSample mono)
    //  => new QuadSample(mono.FrontCenter, mono.FrontCenter, mono.FrontCenter, mono.FrontCenter);
    // IEquatable<T>
    public bool Equals(MonoSample other)
      => FrontCenter.Equals(other.FrontCenter);
    // Object overrides
    public override bool Equals(object? obj)
      => obj is MonoSample sample && Equals(sample);
    public override int GetHashCode()
      => FrontCenter.GetHashCode();
    public override string ToString()
      => $"<Fc:{FrontCenter}>";
  }

  public struct StereoSample
    : ISampleStereo, System.IEquatable<StereoSample>
  {
    public double FrontLeft { get; }
    public double FrontRight { get; }

    public StereoSample(in double monoSample)
      : this(monoSample, monoSample)
    {
    }
    public StereoSample(in double frontLeft, in double frontRight)
    {
      FrontLeft = frontLeft;
      FrontRight = frontRight;
    }

    #region Static functions
    public static ISampleMono ToMonoSample(in ISampleStereo stereo)
      => new MonoSample((stereo.FrontLeft + stereo.FrontRight) / 2);
    //public static ISampleQuad ToQuadSample(in ISampleStereo stereo)
    //  => new QuadSample(stereo.FrontLeft, stereo.FrontRight, stereo.FrontLeft, stereo.FrontRight);
    #endregion Static functions

    // Operators
    public static bool operator ==(in StereoSample a, in StereoSample b)
      => a.Equals(b);
    public static bool operator !=(in StereoSample a, in StereoSample b)
      => !a.Equals(b);
    //public static implicit operator MonoSample(in StereoSample stereo) 
    //  => new MonoSample((stereo.FrontLeft + stereo.FrontRight) / 2);
    //public static implicit operator QuadSample(in StereoSample stereo)
    //  => new QuadSample(stereo.FrontLeft, stereo.FrontRight, stereo.FrontLeft, stereo.FrontRight);
    // IEquatable<T>
    public bool Equals(StereoSample other)
      => FrontLeft.Equals(other.FrontLeft) && FrontRight.Equals(other.FrontRight);
    // Object overrides
    public override bool Equals(object? obj)
      => obj is StereoSample sample && sample.Equals(this);
    public override int GetHashCode()
      => Flux.HashCode.Combine(FrontLeft, FrontRight);
    public override string ToString()
      => $"<Fl:{FrontLeft}, Fr:{FrontRight}>";
  }

  //public struct QuadSample
  //  : ISampleQuad, System.IEquatable<QuadSample>
  //{
  //  public double FrontLeft { get; }
  //  public double FrontRight { get; }
  //  public double BackLeft { get; }
  //  public double BackRight { get; }

  //  public QuadSample(in double frontLeft, in double frontRight, in double backLeft, in double backRight)
  //  {
  //    FrontLeft = frontLeft;
  //    FrontRight = frontRight;
  //    BackLeft = backLeft;
  //    BackRight = backRight;
  //  }

  //  #region Static functions
  //  public static ISampleMono ToMonoSample(in ISampleQuad quad)
  //    => new MonoSample((quad.FrontLeft + quad.BackLeft + quad.FrontRight + quad.BackRight) / 4);
  //  public static ISampleStereo ToStereoSample(in ISampleQuad quad)
  //    => new StereoSample((quad.FrontLeft + quad.BackLeft) / 2, (quad.FrontRight + quad.BackRight) / 2);
  //  #endregion Static functions

  //  // Operators
  //  public static bool operator ==(in QuadSample a, in QuadSample b)
  //    => a.Equals(b);
  //  public static bool operator !=(in QuadSample a, in QuadSample b)
  //    => !a.Equals(b);
  //  //public static implicit operator MonoSample(in QuadSample quad)
  //  //  => new MonoSample((quad.FrontLeft + quad.BackLeft + quad.FrontRight + quad.BackRight) / 4);
  //  //public static implicit operator StereoSample(in QuadSample quad)
  //  //  => new StereoSample((quad.FrontLeft + quad.BackLeft) / 2, (quad.FrontRight + quad.BackRight) / 2);
  //  // IEquatable<T>
  //  public bool Equals(QuadSample other)
  //    => FrontLeft.Equals(other.FrontLeft) && FrontRight.Equals(other.FrontRight) && BackLeft.Equals(other.BackLeft) && BackRight.Equals(other.BackRight);
  //  // Object overrides
  //  public override bool Equals(object? obj)
  //    => obj is QuadSample sample && sample.Equals(this);
  //  public override int GetHashCode()
  //    => Flux.HashCode.Combine(FrontLeft, FrontRight, BackLeft, BackRight);
  //  public override string ToString()
  //    => $"<Bl:{BackLeft}, Br:{BackRight}, Fl:{FrontLeft}, Fr:{FrontRight}>";
  //}
}
