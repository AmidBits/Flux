namespace Flux.Midi.Protocol
{
  public class ChannelModeMessage
  {
    public static byte[] AllSoundOff(int channel)
      => ChannelVoiceMessage.ControlChange(channel, ControllerChangeNumber.AllNotesOff, 0);
    public static byte[] ResetAllControllers(int channel)
      => ChannelVoiceMessage.ControlChange(channel, ControllerChangeNumber.ResetAllControllers, 0);
    public static byte[] LocalControlOff(int channel)
      => ChannelVoiceMessage.ControlChange(channel, ControllerChangeNumber.LocalControl, 0);
    public static byte[] LocalControlOn(int channel)
      => ChannelVoiceMessage.ControlChange(channel, ControllerChangeNumber.LocalControl, 127);
    public static byte[] AllNotesOff(int channel)
      => ChannelVoiceMessage.ControlChange(channel, ControllerChangeNumber.AllNotesOff, 0);
    public static byte[] OmniModeOff(int channel)
      => ChannelVoiceMessage.ControlChange(channel, ControllerChangeNumber.OmniModeOff, 0);
    public static byte[] OmniModeOn(int channel)
      => ChannelVoiceMessage.ControlChange(channel, ControllerChangeNumber.OmniModeOn, 0);
    public static byte[] MonoModeOn(int channel, int numberOfChannels)
      => ChannelVoiceMessage.ControlChange(channel, ControllerChangeNumber.MonoModeOn, numberOfChannels);
    public static byte[] PolyModeOn(int channel)
      => ChannelVoiceMessage.ControlChange(channel, ControllerChangeNumber.PolyModeOn, 0);
  }
}
