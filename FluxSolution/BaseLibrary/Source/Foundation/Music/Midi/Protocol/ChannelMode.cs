namespace Flux.Music.Midi.Protocol
{
  public sealed class ChannelMode
  {
    public static byte[] AllSoundOff(int channel)
      => ChannelVoice.ControlChange(channel, ControlChangeController.AllNotesOff, 0);
    public static byte[] ResetAllControllers(int channel)
      => ChannelVoice.ControlChange(channel, ControlChangeController.ResetAllControllers, 0);
    public static byte[] LocalControlOff(int channel)
      => ChannelVoice.ControlChange(channel, ControlChangeController.LocalControl, 0);
    public static byte[] LocalControlOn(int channel)
      => ChannelVoice.ControlChange(channel, ControlChangeController.LocalControl, 127);
    public static byte[] AllNotesOff(int channel)
      => ChannelVoice.ControlChange(channel, ControlChangeController.AllNotesOff, 0);
    public static byte[] OmniModeOff(int channel)
      => ChannelVoice.ControlChange(channel, ControlChangeController.OmniModeOff, 0);
    public static byte[] OmniModeOn(int channel)
      => ChannelVoice.ControlChange(channel, ControlChangeController.OmniModeOn, 0);
    public static byte[] MonoModeOn(int channel, int numberOfChannels)
      => ChannelVoice.ControlChange(channel, ControlChangeController.MonoModeOn, numberOfChannels);
    public static byte[] PolyModeOn(int channel)
      => ChannelVoice.ControlChange(channel, ControlChangeController.PolyModeOn, 0);
  }
}
