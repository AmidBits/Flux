﻿namespace Flux.Dsp
{
  public struct SampleQuadraphonic
    : IAudioChannelFl, IAudioChannelFr, IAudioChannelBl, IAudioChannelBr, System.IEquatable<SampleQuadraphonic>
  {
    public double FrontLeft { get; }
    public double FrontRight { get; }
    public double BackLeft { get; }
    public double BackRight { get; }

    public SampleQuadraphonic(in double frontLeft, in double frontRight, in double backLeft, in double backRight)
    {
      FrontLeft = frontLeft;
      FrontRight = frontRight;
      BackLeft = backLeft;
      BackRight = backRight;
    }

    // Operators
    public static bool operator ==(in SampleQuadraphonic a, in SampleQuadraphonic b)
      => a.Equals(b);
    public static bool operator !=(in SampleQuadraphonic a, in SampleQuadraphonic b)
      => !a.Equals(b);

    // IEquatable<T>
    public bool Equals(SampleQuadraphonic other)
      => FrontLeft.Equals(other.FrontLeft) && FrontRight.Equals(other.FrontRight) && BackLeft.Equals(other.BackLeft) && BackRight.Equals(other.BackRight);

    // Object overrides
    public override bool Equals(object? obj)
      => obj is SampleQuadraphonic sample && sample.Equals(this);
    public override int GetHashCode()
      => System.HashCode.Combine(FrontLeft, FrontRight, BackLeft, BackRight);
    public override string ToString()
      => $"<Fl:{FrontLeft}, Fr:{FrontRight}, Rl:{BackLeft}, Rr:{BackRight}>";
  }
}