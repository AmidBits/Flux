using System.Linq;

namespace Flux
{
  //public static class MulticastAddress
  //{
  //  public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetRange(System.Net.IPAddress first, System.Net.IPAddress last)
  //  {
  //    var bytes = first.GetAddressBytes();
  //    var lastBytes = last.GetAddressBytes();

  //    while (!bytes.SequenceEqual(lastBytes))
  //    {
  //      if (bytes[3] == 255)
  //      {
  //        if (bytes[2] == 255)
  //        {
  //          if (bytes[1] == 255)
  //          {
  //            if (bytes[0] == 255) yield break;
  //            else bytes[0]++;
  //          }
  //          else bytes[1]++;
  //        }
  //        else bytes[2]++;
  //      }
  //      else bytes[3]++;

  //      yield return new System.Net.IPAddress(bytes);
  //    }
  //  }

  //  public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetLocalSubnetwork()
  //  {
  //    for (byte b3 = 0; b3 <= 255; b3++)
  //      yield return new System.Net.IPAddress(new byte[4] { 224, 0, 0, b3 });
  //  }
  //  public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetInternetworkControl()
  //  {
  //    for (var b3 = 0; b3 <= 255; b3++)
  //      yield return new System.Net.IPAddress(new byte[4] { 224, 0, 1, (byte)b3 });
  //  }
  //  public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetAdHocBlock1()
  //  {
  //    for (var b2 = 2; b2 <= 255; b2++)
  //      for (var b3 = 0; b3 <= 255; b3++)
  //        yield return new System.Net.IPAddress(new byte[] { 224, 0, (byte)b2, (byte)b3 });
  //  }
  //  public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetAdHocBlock2()
  //  {
  //    for (var b1 = 3; b1 <= 4; b1++)
  //      for (var b2 = 0; b2 <= 255; b2++)
  //        for (var b3 = 0; b3 <= 255; b3++)
  //          yield return new System.Net.IPAddress(new byte[] { 224, (byte)b1, (byte)b2, (byte)b3 });
  //  }
  //  public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetSourceSpecificMulticast()
  //  {
  //    for (var b1 = 0; b1 <= 255; b1++)
  //      for (var b2 = 0; b2 <= 255; b2++)
  //        for (var b3 = 0; b3 <= 255; b3++)
  //          yield return new System.Net.IPAddress(new byte[] { 232, (byte)b1, (byte)b2, (byte)b3 });
  //  }
  //  public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetGlopAddressing()
  //  {
  //    for (var b1 = 0; b1 <= 251; b1++)
  //      for (var b2 = 0; b2 <= 255; b2++)
  //        for (var b3 = 0; b3 <= 255; b3++)
  //          yield return new System.Net.IPAddress(new byte[] { 233, (byte)b1, (byte)b2, (byte)b3 });
  //  }
  //  public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetAdHocBlock3()
  //  {
  //    for (var b1 = 252; b1 <= 255; b1++)
  //      for (var b2 = 0; b2 <= 255; b2++)
  //        for (var b3 = 0; b3 <= 255; b3++)
  //          yield return new System.Net.IPAddress(new byte[] { 233, (byte)b1, (byte)b2, (byte)b3 });
  //  }
  //}

  /// <summary>Event arguments for when data is received.</summary>
  public class UdpCastDataReceivedEventArgs
    : System.EventArgs
  {
    public System.Collections.Generic.IList<byte> Bytes { get; private set; }
    public System.Net.EndPoint Remote { get; private set; }

    public UdpCastDataReceivedEventArgs(byte[] bytes, System.Net.EndPoint remote)
    {
      Bytes = bytes;
      Remote = remote;
    }
  }

  public class UdpCast
    : Disposable
  {
    private static readonly System.Text.RegularExpressions.Regex m_regexMulticast = new System.Text.RegularExpressions.Regex(@"2(?:2[4-9]|3\d)(?:\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]\d?|0)){3}");
    private static readonly System.Text.RegularExpressions.Regex m_regexBroadcast = new System.Text.RegularExpressions.Regex(@"255.255.255.255");

    public static readonly System.Net.IPEndPoint MulticastTest = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(@"224.5.6.7"), 4567);

    public static UdpCast CreateTestMulticast() => new UdpCast(MulticastTest);
    public static UdpCast CreateTestBroadcast() => new UdpCast(new System.Net.IPEndPoint(System.Net.IPAddress.Broadcast, 0));

    #region "Event Handling"
    private System.Threading.Thread Thread;

    public event System.EventHandler<UdpCastDataReceivedEventArgs>? DataReceived;
    #endregion

    public System.Net.IPEndPoint RemoteAddress { get; private set; }

    public System.Net.Sockets.Socket Socket { get; private set; }

    public UdpCast(System.Net.IPEndPoint remoteAddress)
    {
      RemoteAddress = remoteAddress ?? throw new System.ArgumentNullException(nameof(remoteAddress));

      Socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);
      Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.ExclusiveAddressUse, false);
      Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.ReuseAddress, true);
      Socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, RemoteAddress.Port));
      Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.IP, System.Net.Sockets.SocketOptionName.DontFragment, true);
      Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.IP, System.Net.Sockets.SocketOptionName.IpTimeToLive, 0);
      if (m_regexMulticast.IsMatch(RemoteAddress.Address.ToString()))
      {
        Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.IP, System.Net.Sockets.SocketOptionName.AddMembership, new System.Net.Sockets.MulticastOption(RemoteAddress.Address));
      }
      if (m_regexBroadcast.IsMatch(RemoteAddress.Address.ToString()))
      {
        Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.Broadcast, true);
      }

      Thread = new System.Threading.Thread(Thread_SocketReceiver);
      Thread.Start();
    }

    private void Thread_SocketReceiver()
    {
      System.Net.EndPoint remoteEP = (System.Net.EndPoint)new System.Net.IPEndPoint(System.Net.IPAddress.None, RemoteAddress.Port);

      while (true)
      {
        if (Socket.Available > 0)
        {
          byte[] buffer = new byte[Socket.Available];

          Socket.ReceiveFrom(buffer, ref remoteEP);

          DataReceived?.Invoke(this, new UdpCastDataReceivedEventArgs(buffer, remoteEP));
        }
      }
    }

    public void SendData(byte[] bytes)
    {
      Socket.SendTo(bytes, RemoteAddress);
    }

    protected override void DisposeManaged()
    {
      if (Thread != null)
      {
        Thread.Abort();
        Thread = null!;
      }

      if (Socket != null)
      {
        Socket.Dispose();
        Socket = null!;
      }
    }
  }
}

/*
  class Program
  {
    static void Main(string[] args)
    {
      System.Console.OutputEncoding = System.Text.Encoding.Unicode;

      System.Console.WriteLine("Enter line to send (empty line will terminate program):");

      using (var uc = Flux.Net.UdpCast.CreateTestMulticast())
      {
        uc.DataReceived += Uc_DataReceived;

        while (System.Console.ReadLine() is string line && line.Length > 0)
        {
          uc.SendData(System.Text.UnicodeEncoding.UTF32.GetBytes(line));
        }

        uc.DataReceived -= Uc_DataReceived;
      }
    }

    private static void Uc_DataReceived(object sender, Flux.Net.UdpCast.DataReceivedEventArgs e)
    {
      System.Console.WriteLine(System.Text.UnicodeEncoding.UTF32.GetString(e.Bytes));
    }
  }
*/
