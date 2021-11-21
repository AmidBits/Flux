namespace Flux.Dsp
{
  public struct Sample51
    : System.IEquatable<Sample51>
    , IAudioChannelBackLeft, IAudioChannelBackRight, IAudioChannelFrontCenter, IAudioChannelFrontLeft, IAudioChannelFrontRight, IAudioChannelLowFrequencyEffect
  {
    public double FrontLeft { get; }
    public double FrontRight { get; }
    public double FrontCenter { get; }
    public double LowFrequency { get; }
    public double BackLeft { get; }
    public double BackRight { get; }

    public Sample51(in double frontLeft, in double frontRight, in double frontCenter, in double lowFrequency, in double backLeft, in double backRight)
    {
      FrontLeft = frontLeft;
      FrontRight = frontRight;
      FrontCenter = frontCenter;
      LowFrequency = lowFrequency;
      BackLeft = backLeft;
      BackRight = backRight;
    }

    #region Overloaded operators
    public static bool operator ==(in Sample51 a, in Sample51 b)
      => a.Equals(b);
    public static bool operator !=(in Sample51 a, in Sample51 b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<T>
    public bool Equals(Sample51 other)
      => FrontLeft.Equals(other.FrontLeft) && FrontRight.Equals(other.FrontRight) && FrontCenter.Equals(other.FrontCenter) && BackLeft.Equals(other.BackLeft) && BackRight.Equals(other.BackRight) && LowFrequency.Equals(other.LowFrequency);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Sample51 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(FrontLeft, FrontRight, FrontCenter, LowFrequency, BackLeft, BackRight);
    public override string ToString()
      => $"{GetType().Name} {{ FL = {FrontLeft}, FR = {FrontRight}, FC = {FrontCenter}, LFE = {LowFrequency}, BL = {BackLeft}, BR = {BackRight} }}";
    #endregion Object overrides
  }
}
