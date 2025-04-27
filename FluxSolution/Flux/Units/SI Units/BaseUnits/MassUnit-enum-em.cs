//namespace Flux.Units
//{
//  public static partial class Em
//  {
//    public static double XGetUnitFactor(this MassUnit unit)
//      =>
//      unit switch
//      {
//        MassUnit.Kilogram => 1,

//        MassUnit.Gram => 0.001,
//        MassUnit.Ounce => 0.028349523125,
//        MassUnit.Pound => 0.45359237,
//        MassUnit.Tonne => 1000,

//        _ => throw new System.NotImplementedException()
//      };

//    public static string XGetUnitSymbol(this MassUnit unit, bool preferUnicode)
//      => unit switch
//      {
//        MassUnit.Gram => "g",
//        MassUnit.Ounce => "oz",
//        MassUnit.Pound => "lb",
//        MassUnit.Kilogram => preferUnicode ? "\u338F" : "kg",
//        MassUnit.Tonne => "t",
//        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
//      };
//  }
//}
