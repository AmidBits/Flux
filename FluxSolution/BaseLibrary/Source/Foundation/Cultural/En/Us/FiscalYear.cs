namespace Flux.Cultural.EnUs
{
  public static partial class FiscalYear
  {
    /// <summary>Gets the fiscal year of the specified scope for the datetime.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "Easier to read switch statement")]
    public static int Get(System.DateTime source, FiscalYearScope scope)
    {
      switch (scope)
      {
        case FiscalYearScope.OtherStates:
        case FiscalYearScope.TerritoryOfPuertoRico:
          return source.Month >= 7 ? source.Year + 1 : source.Year;
        case FiscalYearScope.FederalGovernment:
        case FiscalYearScope.OtherTerritories:
        case FiscalYearScope.StateOfAlabama:
        case FiscalYearScope.StateOfMichigan:
          return source.Month >= 10 ? source.Year + 1 : source.Year;
        case FiscalYearScope.StateOfTexas:
          return source.Month >= 9 ? source.Year + 1 : source.Year;
        case FiscalYearScope.StateOfNewYork:
          return source.Month >= 3 ? source.Year + 1 : source.Year;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(scope));
      }
    }
  }
}
