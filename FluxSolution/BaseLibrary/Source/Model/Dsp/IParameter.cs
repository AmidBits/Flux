namespace Flux.Dsp
{
  public interface IModulatedParameter
  {
    double Modulation { get; set; }

    IOscillator Modulator { get; set; }

    double Value { get; set; }
  }

  public class ModulatedValue : IModulatedParameter
  {
    private double _modulation;
    /// <summary>The amount of modulation to apply when computing the value.</summary>
    public double Modulation { get => _modulation; set => _modulation = Maths.Clamp(value, 0.0, 1.0); }

    /// <summary>The modulator to use when automating the computation of values. Values can still be manipulated.</summary>
    public IOscillator Modulator { get; set; }

    private double _value;
    /// <summary>The value (manually set or automatically calculated on call to the MoveNext method) of the parameter.</summary>
    public double Value { get => _value; set => _value = Maths.Clamp(value, -1.0, 1.0); }

    public ModulatedValue(double modulation, IOscillator modulator, double value)
    {
      _modulation = modulation;
      Modulator = modulator;
      _value = value;
    }
    public ModulatedValue(double value) : this(default, IOscillator.Empty, value) { }
    public ModulatedValue() : this(default) { }

    public double MoveNext()
    {
      if (Modulator != null && _modulation > Maths.EpsilonCpp32)
      {
        _value = Modulator.NextSample().FrontCenter * _modulation;
      }
      else
      {
        _value = 0.0;
      }

      return _value;
    }
  }
}
