namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Gets the fiscal year of the specified scope for the datetime.</summary>
    ///[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "Easier to read switch statement")]
    public static int GetFiscalYear(this Globalization.En.Us.FiscalYearScope source, System.DateTime timestamp)
      => source switch
      {
        Globalization.En.Us.FiscalYearScope.OtherStates or Globalization.En.Us.FiscalYearScope.TerritoryOfPuertoRico => timestamp.Month >= 7 ? timestamp.Year + 1 : timestamp.Year,
        Globalization.En.Us.FiscalYearScope.FederalGovernment or Globalization.En.Us.FiscalYearScope.OtherTerritories or Globalization.En.Us.FiscalYearScope.StateOfAlabama or Globalization.En.Us.FiscalYearScope.StateOfMichigan => timestamp.Month >= 10 ? timestamp.Year + 1 : timestamp.Year,
        Globalization.En.Us.FiscalYearScope.StateOfTexas => timestamp.Month >= 9 ? timestamp.Year + 1 : timestamp.Year,
        Globalization.En.Us.FiscalYearScope.StateOfNewYork => timestamp.Month >= 3 ? timestamp.Year + 1 : timestamp.Year,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      };

  }

  namespace Globalization.En.Us
  {
    public enum FiscalYearScope
    {
      FederalGovernment,
      OtherStates,
      OtherTerritories,
      StateOfAlabama,
      StateOfMichigan,
      StateOfNewYork,
      StateOfTexas,
      TerritoryOfPuertoRico,
    }
  }
}
