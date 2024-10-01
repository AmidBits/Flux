namespace Flux.Quantities
{
  /// <summary>
  /// <para>MIDI note unit of byte [0, 127], is an integer value in the range [1, 127]. It enables conversions to and from MIDI note numbers and other relative data points, e.g. pitch notations and frequencies.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/MIDI_tuning_standard"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/></para>
  /// </summary>
  public readonly partial record struct MidiNote
    : System.IComparable, System.IComparable<MidiNote>, System.IFormattable, IValueQuantifiable<int>
  {
    public const byte MaxValue = 127;
    public const byte MinValue = 0;

    public static MidiNote MiddleC { get; } = new(60);

    /// <summary>
    /// <para>This is the (MIDI) note number of A4 which represents the audio frequency 440 Hz.</para>
    /// </summary>
    public const byte ReferenceNoteNumberA4 = 69;

    /// <summary>
    /// <para>This is the audio frequency (in hertz) of A4 which represents note number 69.</para>
    /// </summary>
    public const double ReferenceAudioFrequencyA4 = 440;

    private readonly byte m_value;

    public MidiNote(int midiNoteNumber)
      => m_value = midiNoteNumber >= 0 && midiNoteNumber <= 127 ? (byte)midiNoteNumber : throw new System.ArgumentOutOfRangeException(nameof(midiNoteNumber));

    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Pitch_class#Integer_notation"/></para>
    /// </summary>
    public int IntegerNotation => m_value % 12;

    /// <summary>
    /// <para>Determines the octave of the MIDI note.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Octave"/></para>
    /// </summary>
    public int Octave => (m_value / 12) - 1;

    /// <summary>
    /// <para>Convert the specified MIDI note to the corresponding frequency.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/></para>
    /// </summary>
    public Quantities.Frequency ToFrequency() => new(ConvertNoteNumberToFrequency(m_value));

    /// <summary>
    /// <para>Creates a string representing the Scientific pitch notation (SPN), also known as American standard pitch notation (ASPN) and international pitch notation (IPN), a method of specifying musical pitch by combining a musical note name (with accidental if needed) and a number identifying the pitch's octave.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Scientific_pitch_notation"/></para>
    /// </summary>
    public string ToScientificPitchNotationString(bool preferUnicode = false) => GetScientificPitchNotationStrings(preferUnicode)[IntegerNotation];

    #region Static methods

    #region Conversion methods

    /// <summary>
    /// <para>Convert the specified frequency to the corresponding note number depending on the specified reference frequency and note number.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/MIDI_tuning_standard#Frequency_values"/></para>
    /// </summary>
    public static int ConvertFrequencyToNoteNumber(double frequency, double referenceFrequency = ReferenceAudioFrequencyA4, int referenceNoteNumber = ReferenceNoteNumberA4)
      => (int)((double.Log(frequency / referenceFrequency, 2.0) * 12.0) + referenceNoteNumber);

    /// <summary>
    /// <para>Convert the specified note number to the corresponding frequency depending on the specified reference note number and frequency.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/MIDI_tuning_standard#Frequency_values"/></para>
    /// </summary>
    public static double ConvertNoteNumberToFrequency(int noteNumber, int referenceNoteNumber = ReferenceNoteNumberA4, double referenceFrequency = ReferenceAudioFrequencyA4)
      => double.Pow(2, (noteNumber - referenceNoteNumber) / 12.0) * referenceFrequency;

    /// <summary>
    /// <para>Attempts to determine the MIDI note from the specified frequency.</para>
    /// </summary>
    public static bool TryConvertFrequencyToMidiNote(Quantities.Frequency frequency, out MidiNote result)
    {
      try
      {
        result = new(ConvertFrequencyToNoteNumber(frequency.Value));
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    #endregion // Conversion methods

    /// <summary>
    /// <para>Creates an array of scientific pitch notation strings, using Unicode if preferable and available.</para>
    /// <param name="preferUnicode"></param>
    /// </summary>
    public static string[] GetScientificPitchNotationStrings(bool preferUnicode = false)
      => new string[] { @"C", $"C{GetSymbolSharpString(preferUnicode)}/D{GetSymbolFlatString(preferUnicode)}", @"D", $"D{GetSymbolSharpString(preferUnicode)}/E{GetSymbolFlatString(preferUnicode)}", @"E", @"F", $"F{GetSymbolSharpString(preferUnicode)}/G{GetSymbolFlatString(preferUnicode)}", @"G", $"G{GetSymbolSharpString(preferUnicode)}/A{GetSymbolFlatString(preferUnicode)}", @"A", $"A{GetSymbolSharpString(preferUnicode)}/B{GetSymbolFlatString(preferUnicode)}", @"B" };

    /// <summary>
    /// <para>Creates a string representing a flat (note), using Unicode if preferable and available.</para>
    /// </summary>
    /// <param name="preferUnicode"></param>
    /// <returns></returns>
    public static string GetSymbolFlatString(bool preferUnicode = false) => preferUnicode ? "\u266D" : "b";

    /// <summary>
    /// <para>Creates a string representing a sharp (note), using Unicode if preferable and available.</para>
    /// </summary>
    /// <param name="preferUnicode"></param>
    /// <returns></returns>
    public static string GetSymbolSharpString(bool preferUnicode = false) => preferUnicode ? "\u266F" : "#";

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"^([^0-9\-]+)([\-0-9]+)$")]
    private static partial System.Text.RegularExpressions.Regex ScientificPitchNotationRegex();
#else
    private static System.Text.RegularExpressions.Regex ScientificPitchNotationRegex() => new(@"^([^0-9\-]+)([\-0-9]+)$");
#endif

    /// <summary>
    /// <para>Parse the specified SPN string into a MIDI note.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/></para>
    /// </summary>
    public static MidiNote Parse(string scientificPitchNotation)
    {
      var m = ScientificPitchNotationRegex().Match(scientificPitchNotation);

      if (m.Success && m.Groups is var gc && gc.Count >= 3 && gc[1].Success && gc[2].Success)
      {
        var octave = int.Parse(gc[2].Value, System.Globalization.CultureInfo.CurrentCulture);
        var offset = System.Array.FindIndex(GetScientificPitchNotationStrings(), 0, n => n.StartsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase) || n.EndsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase));

        if (octave < -1 && octave > 9 && offset == -1)
          throw new System.ArgumentException($"Invalid note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));

        return new((byte)((octave + 1) * 12 + offset));
      }

      throw new System.ArgumentException($"Cannot parse note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));
    }

    /// <summary>
    /// <para>Attempts to parse the specified SPN string into a MIDI note.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/></para>
    /// </summary>
    public static bool TryParse(string scientificPitchNotation, out MidiNote result)
    {
      try
      {
        result = Parse(scientificPitchNotation);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(MidiNote a, MidiNote b) => a.CompareTo(b) < 0;
    public static bool operator <=(MidiNote a, MidiNote b) => a.CompareTo(b) <= 0;
    public static bool operator >(MidiNote a, MidiNote b) => a.CompareTo(b) > 0;
    public static bool operator >=(MidiNote a, MidiNote b) => a.CompareTo(b) >= 0;

    public static MidiNote operator -(MidiNote v) => new(-v.m_value);
    public static MidiNote operator +(MidiNote a, int b) => new(a.m_value + b);
    public static MidiNote operator +(MidiNote a, MidiNote b) => a + b.m_value;
    public static MidiNote operator /(MidiNote a, int b) => new(a.m_value / b);
    public static MidiNote operator /(MidiNote a, MidiNote b) => a / b.m_value;
    public static MidiNote operator *(MidiNote a, int b) => new(a.m_value * b);
    public static MidiNote operator *(MidiNote a, MidiNote b) => a * b.m_value;
    public static MidiNote operator %(MidiNote a, int b) => new(a.m_value % b);
    public static MidiNote operator %(MidiNote a, MidiNote b) => a % b.m_value;
    public static MidiNote operator -(MidiNote a, int b) => new(a.m_value - b);
    public static MidiNote operator -(MidiNote a, MidiNote b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(MidiNote other) => m_value.CompareTo(other.m_value);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is MidiNote o ? CompareTo(o) : -1;

    // IFormattable
    /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public string ToString(string? format, System.IFormatProvider? formatProvider) => $"{ToScientificPitchNotationString(false)}{Octave}";

    // IQuantifiable<>
    /// <summary>
    /// <para>The <see cref="MidiNote.Value"/> property is a MIDI note number of the closed interval [<see cref="MinValue"/>, <see cref="MaxValue"/>].</para>
    /// </summary>
    public int Value => m_value;

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
