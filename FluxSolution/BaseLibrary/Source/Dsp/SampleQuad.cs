namespace Flux.Dsp
{
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
