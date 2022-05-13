namespace Flux.Resources.DotNet
{
  /// <summary>.NET time zones.</summary>
  public sealed class TimeZones
    : ATabularDataAcquirer
  {
    public override string[] FieldNames
      => typeof(TimeZoneInfo).GetProperties().Select(pi => pi.Name).ToArray();
    public override System.Type[] FieldTypes
      => typeof(TimeZoneInfo).GetProperties().Select(pi => pi.PropertyType).ToArray();

    public override System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
    {
      var tz = TimeZoneInfo.GetSystemTimeZones();

      for (var i = 0; i < tz.Count; i++)
      {
        var tzi = tz[i];

        yield return Flux.Reflection.GetPropertyInfos(tzi).Select(pi => pi.GetValue(tzi)!).ToArray();
      }
    }
  }
}
