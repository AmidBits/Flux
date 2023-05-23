namespace Flux
{
  public static partial class ExtensionMethodsUri
  {
    public static bool TryConnectTo(this System.Uri uri)
    {
      try
      {
        using var wc = new System.Net.Http.HttpClient();
        using var s = wc.GetStreamAsync(uri);

        return true;
      }
      catch { }
      return false;
    }
  }
}
