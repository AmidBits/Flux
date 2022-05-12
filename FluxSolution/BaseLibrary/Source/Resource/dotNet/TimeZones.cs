namespace Flux.Resources.DotNet
{
  public sealed class TimeZones
    : ATabularDataAcquirer
  {
    /// <summary>.NET time zones.</summary>
    public override System.Collections.Generic.IEnumerable<object[]> AcquireTabularData()
    {
      var tz = TimeZoneInfo.GetSystemTimeZones();

      yield return Flux.Reflection.GetPropertyInfos(tz[0]).Select(pi => pi.Name).ToArray();

      for (var i = 0; i < tz.Count; i++)
      {
        var tzi = tz[i];

        yield return Flux.Reflection.GetPropertyInfos(tzi).Select(pi => pi.GetValue(tzi)!).ToArray();
      }
    }

    public override System.Data.IDataReader AcquireDataReader()
    {
      var tz = TimeZoneInfo.GetSystemTimeZones();

      var fieldNames = Flux.Reflection.GetPropertyInfos(tz[0]).Select(pi => pi.Name).ToArray();
      var fieldTypes = Flux.Reflection.GetPropertyInfos(tz[0]).Select(pi => pi.PropertyType).ToArray();

      return new Data.EnumerableTabularDataReader(GetTabularData(), fieldNames, fieldTypes);

      System.Collections.Generic.IEnumerable<object[]> GetTabularData()
      {
        for (var i = 0; i < tz.Count; i++)
        {
          var tzi = tz[i];

          yield return Flux.Reflection.GetPropertyInfos(tzi).Select(pi => pi.GetValue(tzi)!).ToArray();
        }
      }
    }
  }
}
