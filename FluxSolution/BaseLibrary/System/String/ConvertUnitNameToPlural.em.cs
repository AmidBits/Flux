namespace Flux
{
  public static partial class Fx
  {
    public static string ConvertUnitNameToPlural(this string unitName, bool isPlural)
    {
      if (!isPlural) return unitName;

      if (unitName.IndexOf("Per") is var index && index > 0)
      {
        return ConvertUnitNameToPlural(unitName.Substring(0, index)) + unitName.Substring(index);
      }

      return ConvertUnitNameToPlural(unitName);

      static string ConvertUnitNameToPlural(string name)
      {
        if (name.Equals("Celsius", StringComparison.InvariantCultureIgnoreCase)
          || name.Equals("Fahrenheit", StringComparison.InvariantCultureIgnoreCase)
          || name.Equals("Siemens", StringComparison.InvariantCultureIgnoreCase)
        ) return name;

        if (name == "Foot") return "Feet";
        else if (name.EndsWith('x') || name == "Inch") return name + "es";
        else if (name.EndsWith('y')) return name + "ies";
        else return name + "s";
      }
    }
  }
}
