namespace Flux
{
  namespace Quantities
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

#if NET7_0_OR_GREATER
      [System.Text.RegularExpressions.GeneratedRegex(@"^([^0-9\-]+)([\-0-9]+)$")]
      private static partial System.Text.RegularExpressions.Regex ScientificPitchNotationRegex();
#else
    private static System.Text.RegularExpressions.Regex ScientificPitchNotationRegex() => new(@"^([^0-9\-]+)([\-0-9]+)$");
#endif

      /// <summary>Parse the specified SPN string into a MIDI note.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
      public static MidiNote Parse(string scientificPitchNotation)
      {
        var m = ScientificPitchNotationRegex().Match(scientificPitchNotation);

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
      /// <see href="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>

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

      // IFormattable
      /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => $"{GetScientificPitchNotationLabel()}{GetOctave()}";

      // IQuantifiable<>
      /// <summary>
      /// <para>The <see cref="MidiNote.Value"/> property is a MIDI note number of the closed interval [<see cref="MinValue"/>, <see cref="MaxValue"/>].</para>
      /// </summary>
      public int Value => m_number;

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}