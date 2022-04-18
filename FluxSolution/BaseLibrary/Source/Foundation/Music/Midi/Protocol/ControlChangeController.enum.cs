namespace Flux.Music.Midi.Protocol
{
  public enum ControlChangeController
  {
    #region ControllerNumbers
    BankSelectMsb = 0,
    ModulationWheelMsb = 1,
    BreathControllerMsb = 2,
    FootControllerMsb = 4,
    PortamentoTimeMsb = 5,
    DataEntryMsb = 6,
    ChannelVolumneMsb = 7,
    BalanceMsb = 8,
    PanMsb = 10,
    ExpressionControllerMsb = 11,
    EffectControl1Msb = 12,
    EffectControl2Msb = 13,
    GeneralPurposeController1Msb = 16,
    GeneralPurposeController2Msb = 17,
    GeneralPurposeController3Msb = 18,
    GeneralPurposeController4Msb = 19,

    BankSelectLsb = 32,
    ModulationWheelLsb = 33,
    BreathControllerLsb = 34,
    FootControllerLsb = 36,
    PortamentoTimeLsb = 37,
    DataEntryLsb = 38,
    ChannelVolumneLsb = 39,
    BalanceLsb = 40,
    PanLsb = 42,
    ExpressionControllerLsb = 43,
    EffectControl1Lsb = 44,
    EffectControl2Lsb = 45,
    GeneralPurposeController1Lsb = 48,
    GeneralPurposeController2Lsb = 49,
    GeneralPurposeController3Lsb = 50,
    GeneralPurposeController4Lsb = 51,

    DamperPedalSwitch = 64,
    PortamentoSwitch = 65,
    SostenutoSwitch = 66,
    SoftPedalSwitch = 67,
    LegatoSwitch = 68,
    Hold2Switch = 69,

    GeneralPurposeController5Lsb = 80,
    GeneralPurposeController6Lsb = 81,
    GeneralPurposeController7Lsb = 82,
    GeneralPurposeController8Lsb = 83,

    Effects1Depth = 91,
    Effects2Depth = 92,
    Effects3Depth = 93,
    Effects4Depth = 94,
    Effects5Depth = 95,

    NrpnLsb = 98,
    NrpnMsb = 99,
    RpnLsb = 100,
    RpnMsb = 101,
    #endregion ControllerNumbers

    #region ModeControl
    /// <summary>Mode control: all sound off.</summary>
    AllSoundOff = 120,
    /// <summary>Mode control: reset all controllers.</summary>
    ResetAllControllers = 121,
    /// <summary>Mode control: local control.</summary>
    LocalControl = 122,
    /// <summary>Mode control: all notes off.</summary>
    AllNotesOff = 123,
    /// <summary>Mode control: omni mode off.</summary>
    OmniModeOff = 124,
    /// <summary>Mode control: omni mode on.</summary>
    OmniModeOn = 125,
    /// <summary>Mode control: mono mode on.</summary>
    MonoModeOn = 126,
    /// <summary>Mode control: poly mode on.</summary>
    PolyModeOn = 127
    #endregion ModeControl
  }
}
