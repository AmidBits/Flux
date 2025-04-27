namespace Flux.Units
{
  [System.CLSCompliant(false)]
  [System.AttributeUsage(System.AttributeTargets.Enum)]
  public class UnitValueQuantifieableAttribute<TEnum>
    : System.Attribute
    where TEnum : struct, System.Enum
  {
    private readonly TEnum m_defaultUnit;
    private readonly TEnum m_baseUnit;

    // This constructor defines two required parameters: name and level.

    public UnitValueQuantifieableAttribute(TEnum defaultUnit, TEnum baseUnit)
    {
      m_defaultUnit = defaultUnit;
      m_baseUnit = baseUnit;
    }

    public virtual TEnum DefaultUnit => m_defaultUnit;
    public virtual TEnum BaseUnit => m_baseUnit;
  }

  [System.CLSCompliant(false)]
  [System.AttributeUsage(System.AttributeTargets.Field)]
  public class UnitAttribute
    : System.Attribute
  {
    private readonly double m_factor;
    private readonly string m_symbol;

    public UnitAttribute(double factor, string symbol)
    {
      m_factor = factor;
      m_symbol = symbol;
    }

    public double Factor => m_factor;
    public string Symbol => m_symbol;
  }
}
