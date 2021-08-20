namespace Flux
{
  [System.Runtime.Versioning.SupportedOSPlatform("windows")]
  public static class Registry
  {
    private static bool TryGetRegistryKey(string path, string key, out object? value)
    {
      value = null;

      try
      {
        using (var rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(path))
        {
          if (rk == null) return false;
          value = rk.GetValue(key);
          return value != null;
        }
      }
      catch
      {
        return false;
      }
    }
  }
}
