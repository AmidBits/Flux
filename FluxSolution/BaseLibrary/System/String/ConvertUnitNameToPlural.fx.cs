//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <para>This is a support function specifically for units in this assembly. It is possible to use it outside of this assembly, but with caution.</para>
//    /// </summary>
//    /// <param name="unitName"></param>
//    /// <param name="preferPlural"></param>
//    /// <returns></returns>
//    public static string ToPluralUnitName(this string unitName, bool preferPlural)
//    {
//      if (!preferPlural) return unitName; // Do not want plural.

//      if (unitName.IndexOf("Per", StringComparison.InvariantCulture) is var index && index > 0)
//        return ConvertUnitNameToPlural(unitName.Substring(0, index)) + unitName.Substring(index); // Replace the (singular assumed) unit before the "Per" with a plural version.

//      return ConvertUnitNameToPlural(unitName);

//      static string ConvertUnitNameToPlural(string unitName)
//      {
//        if (
//          unitName.Equals("Celsius")
//          || unitName.Equals("Fahrenheit")
//          || unitName.Equals("Siemens")
//        )
//          return unitName; // Skip changes to the ones above.

//        if (
//          unitName.Equals("Foot")
//        )
//          return "Feet";

//        if (unitName.EndsWith("x")
//          || unitName.Equals("Inch")
//        )
//          return unitName + "es";

//        if (
//          unitName.EndsWith("y")
//        )
//          return unitName + "ies";

//        if (!unitName.EndsWith("s"))
//          return unitName + "s";

//        return unitName;
//      }
//    }
//  }
//}
