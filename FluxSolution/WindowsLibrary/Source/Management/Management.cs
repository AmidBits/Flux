using System.Linq;

namespace Flux
{
  [System.Runtime.Versioning.SupportedOSPlatform("windows")]
  public class Management
  {
    public static System.Collections.Generic.IEnumerable<string> GetWmiAll(string className, string propertyName)
    {
      using (var mc = new System.Management.ManagementClass(className))
      {
        using (var mci = mc.GetInstances())
        {
          return mci.Cast<System.Management.ManagementObject>().Select(mbo => System.Convert.ToString(mbo.Properties[propertyName].Value) ?? string.Empty);
        }
      }
    }

    public static string GetWmiOne(string className, string propertyName)
      => GetWmiAll(className, propertyName).FirstOrDefault(s => !string.IsNullOrWhiteSpace(s)) ?? string.Empty;
  }
}
