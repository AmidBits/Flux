namespace Flux.Units
{
  [System.ComponentModel.DefaultValue(Kilogram)]
  public enum MassUnit
  {
    /// <summary>This is the default unit for <see cref="Mass"/>.</summary>
    Kilogram,
    /// <summary>Even though gram is an unprefixed unit, it is not the base unit for mass.</summary>
    Gram,
    Ounce,
    Pound,
    /// <summary>The metric ton.</summary>
    Tonne,
  }
}
