namespace Flux.Music.Midi
{
  /// <summary>MIDI note unit of byte [0, 127], is an integer value in the range [1, 127]. It enables conversions to and from MIDI note numbers and other relative data points, e.g. pitch notations and frequencies.</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/MIDI_tuning_standard"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
  public readonly struct MidiNote
    : System.IComparable<MidiNote>, System.IConvertible, System.IEquatable<MidiNote>, Quantities.IQuantifiable<int>
  {
    public const byte MaxValue = 127;
    public const byte MinValue = 0;

    public const byte ReferenceNoteNumberA4 = 69;
    public const double ReferenceFrequencyHertz440 = 440;

    private readonly byte m_number;

    public MidiNote(int midiNoteNumber)
      => m_number = IsMidiNote(midiNoteNumber) ? (byte)midiNoteNumber : throw new System.ArgumentOutOfRangeException(nameof(midiNoteNumber));

    /// <summary>Determines the name of the specified MIDI note.</summary>
    public string GetScientificPitchNotationLabel(bool preferUnicode = false) => GetScientificPitchNotationLabels(preferUnicode)[m_number % 12];
    /// <summary>Determines the octave of the MIDI note.</summary>
    public int GetOctave() => (m_number / 12) - 1;

    /// <summary>Convert the specified MIDI note to the corresponding frequency.</summary>
    public Quantities.Frequency ToFrequency() => new(ConvertToFrequency(m_number));

    #region Static methods
    /// <summary>Convert the specified frequency to the corresponding note number depending on the specified reference frequency and note number.</summary>

    public static int ConvertFromFrequency(double frequency, double referenceFrequency, int referenceNoteNumber)
      => (int)((System.Math.Log(frequency / referenceFrequency, 2.0) * 12.0) + referenceNoteNumber);
    /// <summary>Convert the specified frequency to the corresponding MIDI note.</summary>

    public static int ConvertFromFrequency(double frequency)
      => ConvertFromFrequency(frequency, ReferenceFrequencyHertz440, ReferenceNoteNumberA4) is var note && IsMidiNote(note) ? note : throw new System.ArgumentOutOfRangeException(nameof(frequency));
    /// <summary>Convert the specified note number to the corresponding frequency depending on the specified reference note number and frequency.</summary>

    public static double ConvertToFrequency(int noteNumber, int referenceNoteNumber, double referenceFrequency)
      => System.Math.Pow(2, (noteNumber - referenceNoteNumber) / 12.0) * referenceFrequency;
    /// <summary>Convert the specified MIDI note to the corresponding frequency.</summary>

    public static double ConvertToFrequency(int midiNoteNumber)
      => IsMidiNote(midiNoteNumber) ? ConvertToFrequency(midiNoteNumber, ReferenceNoteNumberA4, ReferenceFrequencyHertz440) : throw new System.ArgumentOutOfRangeException(nameof(midiNoteNumber));

    /// <summary>Determines the MIDI note from the specified frequency. An exception is thrown if the frequency is out of range.</summary>

    public static MidiNote FromFrequency(Quantities.Frequency frequency)
      => new(ConvertFromFrequency(frequency.Value));
    /// <summary>Determines the MIDI note from the specified frequency, using the try paradigm.</summary>

    public static bool TryFromFrequency(Quantities.Frequency frequency, out MidiNote result)
    {
      try
      {
        result = FromFrequency(frequency);
        return true;
      }
      catch { }

      result = default;
      return false;
    }


    public static string GetFlatSymbolString(bool preferUnicode = false)
      => preferUnicode ? "\u266D" : "b";

    public static string GetSharpSymbolString(bool preferUnicode = false)
      => preferUnicode ? "\u266F" : "#";
    /// <summary>Determines the scientific pitch notation labels.</summary>

    public static string[] GetScientificPitchNotationLabels(bool preferUnicode = false)
      => new string[] { @"C", $"C{GetSharpSymbolString(preferUnicode)}/D{GetFlatSymbolString(preferUnicode)}", @"D", $"D{GetSharpSymbolString(preferUnicode)}/E{GetFlatSymbolString(preferUnicode)}", @"E", @"F", $"F{GetSharpSymbolString(preferUnicode)}/G{GetFlatSymbolString(preferUnicode)}", @"G", $"G{GetSharpSymbolString(preferUnicode)}/A{GetFlatSymbolString(preferUnicode)}", @"A", $"A{GetSharpSymbolString(preferUnicode)}/B{GetFlatSymbolString(preferUnicode)}", @"B" };

    /// <summary>Determines whether the note number is a valid MIDI note. The MIDI note number has the closed interval of [0, 127].</summary>

    public static bool IsMidiNote(int midiNoteNumber)
      => midiNoteNumber >= 0 && midiNoteNumber <= 127;

    /// <summary>Parse the specified SPN string into a MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>

    public static MidiNote Parse(string scientificPitchNotation)
    {
      var m = System.Text.RegularExpressions.Regex.Match(scientificPitchNotation, @"^([^0-9\-]+)([\-0-9]+)$");

      if (m.Success && m.Groups is var gc && gc.Count >= 3 && gc[1].Success && gc[2].Success)
      {
        var octave = int.Parse(gc[2].Value, System.Globalization.CultureInfo.CurrentCulture);
        var offset = System.Array.FindIndex(GetScientificPitchNotationLabels(), 0, n => n.StartsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase) || n.EndsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase));

        if (octave < -1 && octave > 9 && offset == -1)
          throw new System.ArgumentException($"Invalid note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));

        return new((byte)((octave + 1) * 12 + offset));
      }

      throw new System.ArgumentException($"Cannot parse note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));
    }
    /// <summary>Attempts to parse the specified SPN string into a MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>

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
    public static explicit operator int(MidiNote v) => v.m_number;
    public static explicit operator MidiNote(int v) => new(v);

    public static bool operator <(MidiNote a, MidiNote b) => a.CompareTo(b) < 0;
    public static bool operator <=(MidiNote a, MidiNote b) => a.CompareTo(b) <= 0;
    public static bool operator >(MidiNote a, MidiNote b) => a.CompareTo(b) > 0;
    public static bool operator >=(MidiNote a, MidiNote b) => a.CompareTo(b) >= 0;

    public static bool operator ==(MidiNote a, MidiNote b) => a.Equals(b);
    public static bool operator !=(MidiNote a, MidiNote b) => !a.Equals(b);

    public static MidiNote operator -(MidiNote v) => new(-v.m_number);
    public static MidiNote operator +(MidiNote a, int b) => new(a.m_number + b);
    public static MidiNote operator +(MidiNote a, MidiNote b) => a + b.m_number;
    public static MidiNote operator /(MidiNote a, int b) => new(a.m_number / b);
    public static MidiNote operator /(MidiNote a, MidiNote b) => a / b.m_number;
    public static MidiNote operator *(MidiNote a, int b) => new(a.m_number * b);
    public static MidiNote operator *(MidiNote a, MidiNote b) => a * b.m_number;
    public static MidiNote operator %(MidiNote a, int b) => new(a.m_number % b);
    public static MidiNote operator %(MidiNote a, MidiNote b) => a % b.m_number;
    public static MidiNote operator -(MidiNote a, int b) => new(a.m_number - b);
    public static MidiNote operator -(MidiNote a, MidiNote b) => a - b.m_number;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(MidiNote other) => m_number.CompareTo(other.m_number);
    // IComparable
    public int CompareTo(object? other) => other is not null && other is MidiNote o ? CompareTo(o) : -1;

    #region IConvertible
    public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider) => m_number != 0;
    public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_number);
    public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_number);
    public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_number);
    public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_number);
    public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_number);
    public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_number);
    public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_number);
    public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_number);
    [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_number);
    public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_number);
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_number);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_number, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_number);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_number);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_number);
    #endregion IConvertible

    // IEquatable<>
    public bool Equals(MidiNote other) => m_number == other.m_number;

    // IQuantifiable<>
    public string ToQuantityString(string? format, bool preferUnicode = false, bool useFullName = false)
      => $"{GetScientificPitchNotationLabel(preferUnicode)}{GetOctave()}";

    public int Value
      => m_number;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj) => obj is MidiNote o && Equals(o);
    public override int GetHashCode() => m_number.GetHashCode();
    /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public override string ToString() => $"{GetType().Name} {{ {ToQuantityString(null, false, false)} (#{m_number}) }}";
    #endregion Object overrides
  }
}