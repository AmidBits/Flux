namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IDictionary<string, object> ToDictionary(this System.Data.IDataRecord source)
    {
      var od = new Flux.DataStructure.OrderedDictionary<string, object>();

      for (var index = 0; index < source.FieldCount; index++)
        od.Add(source.GetNameEx(index), source.GetValue(index));

      return od;
    }
  }
}
