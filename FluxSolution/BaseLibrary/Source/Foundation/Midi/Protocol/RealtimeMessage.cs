namespace Flux.Midi.Protocol
{
  public class RealtimeMessage
  {
    public static byte[] TimingClock()
      => new byte[] { (byte)RealtimeStatus.TimingClock };
    public static byte[] Start()
      => new byte[] { (byte)RealtimeStatus.Start };
    public static byte[] Continue()
      => new byte[] { (byte)RealtimeStatus.Continue };
    public static byte[] Stop()
      => new byte[] { (byte)RealtimeStatus.Stop };
    public static byte[] ActiveSensing()
      => new byte[] { (byte)RealtimeStatus.ActiveSensing };
    public static byte[] Reset()
      => new byte[] { (byte)RealtimeStatus.Reset };
  }
}
