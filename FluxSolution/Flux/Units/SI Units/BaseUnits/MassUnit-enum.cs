namespace Flux.Units
{
  //[System.ComponentModel.DefaultValue(Kilogram)]
  //[UnitValueQuantifieable<MassUnit>(Kilogram, Gram)]
  public enum MassUnit
  {
    /// <summary>This is the default unit for <see cref="Mass"/>.</summary>
    //[Unit(1, "kg")]
    Kilogram,
    /// <summary>Even though gram is an unprefixed unit, it is not the base unit for mass.</summary>
    //[Unit(0.001, "g")]
    Gram,
    //[Unit(0.028349523125, "oz")]
    Ounce,
    //[Unit(0.45359237, "lb")]
    Pound,
    /// <summary>The metric ton.</summary>
    //[Unit(1000, "t")]
    Tonne,
  }
}
