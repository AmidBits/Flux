namespace Flux
{
  public static partial class Reflection
  {
    public static bool TryConnectTo(this System.Uri source)
    {
      try
      {
        System.ArgumentNullException.ThrowIfNull(source);

        using var wc = new System.Net.Http.HttpClient();
        using var s = wc.GetStreamAsync(source);

        return true;
      }
      catch { }
      return false;
    }
  }
}
