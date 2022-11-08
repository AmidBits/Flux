namespace Flux.Dsp
{
  public readonly record struct SampleMono
    : IAudioChannelFrontCenter
  {
    public readonly static SampleMono Silent;

    private readonly double m_frontCenter;

    public SampleMono(double frontCenter)
    {
      m_frontCenter = frontCenter;
    }

    public SampleStereo ToStereo()
      => new(FrontCenter, FrontCenter);

    public double FrontCenter { get => m_frontCenter; init => m_frontCenter = value; }

    #region Static methods
    /// <summary>Mix one or more mono signals. One mono signal will be returned as is.</summary>
    public static SampleMono Mix(System.Collections.Generic.IEnumerable<SampleMono> mono)
    {
      using var e = mono.GetEnumerator();

      if (e.MoveNext())
      {
        var count = 1;
        var sum = e.Current.FrontCenter;

        while (e.MoveNext())
        {
          count++;
          sum += e.Current.FrontCenter;
        }

        return count > 1 ? new(sum / System.Math.Sqrt(count)) : new(sum);
      }
      else throw new System.ArgumentException(@"The sequence is empty.");

    }
    public static SampleMono Mix(params SampleMono[] mono)
      => Mix(mono.AsEnumerable());
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(SampleMono value)
      => value.FrontCenter;
    public static implicit operator SampleMono(double value)
      => new(value);
    #endregion Overloaded operators
  }
}
