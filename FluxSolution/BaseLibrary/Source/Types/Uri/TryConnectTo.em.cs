namespace Flux
{
  public static partial class SystemUriEm
  {
    public static bool TryConnectTo(this System.Uri uri)
    {
      try
      {
        using var wc = new System.Net.WebClient();
        using var s = wc.OpenRead(uri);

        return true;
      }
      catch { }
      return false;
    }
  }
}
