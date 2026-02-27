namespace Flux
{
  public static class IPEndPointExtensions
  {
    extension(System.Net.IPEndPoint)
    {
      /// <summary>
      /// <para>Attempt to get a local <see cref="System.Net.IPAddress"/> from a remote <see cref="System.Net.IPEndPoint"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="result"></param>
      /// <returns></returns>
      public static bool TryGetLocalIPAddressByUdp(System.Net.IPEndPoint source, out System.Net.IPAddress result)
      {
        try
        {
          source ??= new(System.Net.IPAddress.Parse(@"4.3.2.1"), 4321);

          using var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0);

          socket.Connect(source);

          result = (socket.LocalEndPoint as System.Net.IPEndPoint)?.Address ?? throw new System.NullReferenceException();
          return true;
        }
        catch { }

        result = default!;
        return false;
      }
    }
  }
}
