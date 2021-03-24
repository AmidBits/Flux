using System.Linq;

/// <summary>Contains classes to read and evaluate RIFF files, which includes *.WAV files.</summary>
/// <seealso cref="http://tiny.systems/software/soundProgrammer/WavFormatDocs.pdf"/>
/// <seealso cref="http://soundfile.sapp.org/doc/WaveFormat/"/>
/// <seealso cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ee415713(v=vs.85).aspx"/>
/// <seealso cref="https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm"/>
/// <seealso cref="https://sites.google.com/site/musicgapi/technical-documents/wav-file-format"/>
/// <seealso cref="https://joenord.com/audio-wav-file-format"/>
/// <seealso cref="https://johnloomis.org/cpe102/asgn/asgn1/riff.html"/>
namespace Flux.Media.Riff
{
	public static class File
	{
		public static void CreateFile16BitMono(string path, Dsp.Synthesis.Oscillator oscillator, int sampleCount)
		{
			if (oscillator is null) throw new System.ArgumentNullException(nameof(oscillator));

			var fileName = System.IO.Path.Combine(path, $"{oscillator}.wav");

			using var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);

			var rc = new RiffChunk(FormTypeChunk.WaveID);
			var fc = new Wave.FormatChunk(1, (uint)oscillator.SampleRate, 16);
			var dc = new Wave.DataChunk((int)(fc.BytesPerSample * sampleCount));

			rc.ChunkSize = rc.ChunkSizeForFile + fc.ChunkSizeForFile + dc.ChunkSizeForFile;

			rc.WriteTo(fileStream);
			fc.WriteTo(fileStream);
			dc.WriteTo(fileStream);

			foreach (var amplitudeSample in oscillator.GetNext(sampleCount).Select(sample => unchecked((ushort)(short)(sample * short.MaxValue))))
			{
				fileStream.WriteByte((byte)(amplitudeSample & 0xFF));
				fileStream.WriteByte((byte)(amplitudeSample >> 0x8));
			}
		}
		public static void CreateFile16BitStereo(string path, Dsp.Synthesis.Oscillator oscillatorL, Dsp.Synthesis.Oscillator oscillatorR, int sampleCount)
		{
			if (oscillatorL is null) throw new System.ArgumentNullException(nameof(oscillatorL));
			if (oscillatorR is null) throw new System.ArgumentNullException(nameof(oscillatorR));

			var fileName = System.IO.Path.Combine(path, $"{oscillatorL}_{oscillatorR}.wav");

			using var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);

			var rc = new RiffChunk(FormTypeChunk.WaveID);
			var fc = new Wave.FormatChunk(2, (uint)oscillatorL.SampleRate, 16);
			var dc = new Wave.DataChunk((int)(fc.BytesPerSample * sampleCount));

			rc.ChunkSize = rc.ChunkSizeForFile + fc.ChunkSizeForFile + dc.ChunkSizeForFile;

			rc.WriteTo(fileStream);
			fc.WriteTo(fileStream);
			dc.WriteTo(fileStream);

			for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
			{
				var sampleL = unchecked((ushort)(short)(oscillatorL.NextSample() * short.MaxValue));

				fileStream.WriteByte((byte)(sampleL & 0xFF));
				fileStream.WriteByte((byte)(sampleL >> 0x8));

				var sampleR = unchecked((ushort)(short)(oscillatorR.NextSample() * short.MaxValue));

				fileStream.WriteByte((byte)(sampleR & 0xFF));
				fileStream.WriteByte((byte)(sampleR >> 0x8));
			}
		}

		public static System.Collections.Generic.IEnumerable<Chunk> GetChunks(System.IO.Stream stream)
		{
			if (stream is null) throw new System.ArgumentNullException(nameof(stream));

			while (ReadChunk(stream) is var chunk && !(chunk is null))
				yield return chunk;
		}
		public static Chunk? ReadChunk(System.IO.Stream stream)
		{
			if (stream is null) throw new System.ArgumentNullException(nameof(stream));

			try
			{
				var chunk = new Chunk(stream, 8);

				switch (chunk.ChunkID)
				{
					case RiffChunk.ID:
						chunk.ReadBytes(stream, 4);
						chunk = new RiffChunk(chunk);
						break;
					case ListChunk.ID:
						chunk.ReadBytes(stream, 4);
						chunk = new ListChunk(chunk);
						break;
					case Smf.HeaderChunk.ID:
						chunk.ReadBytes(stream, (int)chunk.ChunkSize);
						chunk = new Smf.HeaderChunk(chunk);
						break;
					case Smf.TrackChunk.ID:
						chunk.ReadBytes(stream, (int)chunk.ChunkSize);
						chunk = new Smf.TrackChunk(chunk);
						break;
					case Wave.FormatChunk.ID:
						chunk.ReadBytes(stream, (int)chunk.ChunkSize);
						chunk = new Wave.FormatChunk(chunk);
						break;
					case Wave.DataChunk.ID:
						chunk.ReadBytes(stream, (int)chunk.ChunkSize);
						chunk = new Wave.DataChunk(chunk);
						break;
					case Wave.FactChunk.ID:
						chunk.ReadBytes(stream, (int)chunk.ChunkSize);
						chunk = new Wave.FactChunk(chunk);
						break;
					default:
						chunk.ReadBytes(stream, (int)chunk.ChunkSize);
						break;
				}

				return chunk;
			}
			catch (System.IO.EndOfStreamException)
			{
				return null;
			}
		}
	}

	public class Chunk
	{
		internal byte[] m_buffer = System.Array.Empty<byte>();
		public System.Collections.Generic.IReadOnlyList<byte> Buffer => m_buffer;

		public string ChunkID { get => System.Text.Encoding.ASCII.GetString(m_buffer, 0, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value))).Substring(0, 4)).CopyTo(m_buffer, 0); } }
		[System.CLSCompliant(false)] public uint ChunkSize { get => System.BitConverter.ToUInt32(m_buffer, 4); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 4); } }

		public long PositionInStream { get; set; }

		public Chunk(string chunkID, int chunkDataSize)
		{
			ChunkID = chunkID;
			ChunkSize = (uint)chunkDataSize - 8;

			m_buffer = new byte[ChunkSize];
		}
		public Chunk(int chunkDataSize)
			=> m_buffer = new byte[chunkDataSize];
		public Chunk(byte[] bytes)
			: this((bytes ?? throw new System.ArgumentNullException(nameof(bytes))).Length)
			=> bytes.CopyTo(m_buffer, 0);
		public Chunk(System.IO.Stream stream, int count)
			=> ReadBytes(stream, count);
		public Chunk(Chunk chunk)
		{
			if (chunk is null) throw new System.ArgumentNullException(nameof(chunk));

			m_buffer = chunk.m_buffer;

			PositionInStream = chunk.PositionInStream;
		}

		[System.CLSCompliant(false)] public uint ChunkSizeForFile => this is FormTypeChunk ? ChunkSize : ChunkSize + 8;

		public void ReadBytes(System.IO.Stream stream, int count)
		{
			if (stream is null) throw new System.ArgumentNullException(nameof(stream));

			var read = -1;

			if (m_buffer is null || m_buffer.Length == 0)
			{
				PositionInStream = stream.Position;

				m_buffer = new byte[count];

				read = stream.Read(m_buffer, 0, count);
			}
			else if (m_buffer.Length is var offset)
			{
				System.Array.Resize(ref m_buffer, offset + (int)count);

				read = stream.Read(m_buffer, offset, count);
			}

			if (read == 0) throw new System.IO.EndOfStreamException();
		}

		public virtual void WriteBytes(System.IO.Stream targetStream, int offset, int count)
		{
			if (targetStream is null) throw new System.ArgumentNullException(nameof(targetStream));

			targetStream.Write(m_buffer, offset, count);
		}
		public virtual void WriteTo(System.IO.Stream stream)
		{
			if (stream is null) throw new System.ArgumentNullException(nameof(stream));

			stream.Write(m_buffer, 0, m_buffer.Length);
		}

		public override string ToString()
			=> $"<{GetType().Name} \"{ChunkID}\", 8+{ChunkSize} bytes>";
	}

	public class FormTypeChunk
		: Chunk
	{
		public const string WaveID = @"WAVE";

		public string FormType { get => System.Text.Encoding.ASCII.GetString(m_buffer, 8, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value))).Substring(0, 4)).CopyTo(m_buffer, 8); } }

		public FormTypeChunk(string chunkID, string formType)
			: base(chunkID, 12)
			=> FormType = formType;
		public FormTypeChunk(Chunk chunk)
			: base(12)
		{
			if (chunk is null) throw new System.ArgumentNullException(nameof(chunk));

			System.Array.Copy(chunk.m_buffer, 0, m_buffer, 0, 12);

			PositionInStream = chunk.PositionInStream;
		}

		public override string ToString()
			=> base.ToString().Replace(">", $", \"{FormType}\">", System.StringComparison.Ordinal);
	}

	public class ListChunk
		: FormTypeChunk
	{
		public const string ID = @"LIST";

		public ListChunk(string formType)
			: base(ID, formType)
		{
		}
		public ListChunk(Chunk chunk)
			: base(chunk)
		{
		}
	}

	public class RiffChunk
		: FormTypeChunk
	{
		public const string ID = @"RIFF";

		public RiffChunk(string formType)
			: base(ID, formType)
		{
		}
		public RiffChunk(Chunk chunk)
			: base(chunk)
		{
		}
	}

	/// <summary>Form type 'WAVE' chunks.</summary>
	namespace Wave
	{
		public class FactChunk
			: Chunk
		{
			public const string ID = @"fact";

			[System.CLSCompliant(false)] public uint NumberOfSamples { get => System.BitConverter.ToUInt32(m_buffer, 8); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 8); } }

			public FactChunk(Chunk chunk)
				: base(chunk)
			{
			}

			public override string ToString()
				=> base.ToString().Replace(">", $", {NumberOfSamples}>", System.StringComparison.Ordinal);
		}

		public class FormatChunk
			: Chunk
		{
			public const string ID = @"fmt ";

			[System.CLSCompliant(false)] public ushort Format { get => System.BitConverter.ToUInt16(m_buffer, 8); set { System.BitConverter.GetBytes((ushort)value).CopyTo(m_buffer, 8); } }
			[System.CLSCompliant(false)] public ushort SampleChannels { get => System.BitConverter.ToUInt16(m_buffer, 10); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 10); UpdateBlockAlign(); } }
			[System.CLSCompliant(false)] public uint SampleRate { get => System.BitConverter.ToUInt32(m_buffer, 12); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 12); UpdateAvgBytesPerSec(); } }
			[System.CLSCompliant(false)] public uint AvgBytesPerSec { get => System.BitConverter.ToUInt32(m_buffer, 16); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 16); } }
			[System.CLSCompliant(false)] public ushort BlockAlign { get => System.BitConverter.ToUInt16(m_buffer, 20); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 20); UpdateAvgBytesPerSec(); } }
			[System.CLSCompliant(false)] public ushort SampleBitDepth { get => System.BitConverter.ToUInt16(m_buffer, 22); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 22); UpdateBlockAlign(); } }

			public bool IsExtendedFormat => (m_buffer.Length > 24 && ExtensionSize == 22);
			[System.CLSCompliant(false)] public ushort ExtensionSize { get => System.BitConverter.ToUInt16(m_buffer, 24); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 24); } }
			[System.CLSCompliant(false)] public ushort ValidBitsPerSample { get => System.BitConverter.ToUInt16(m_buffer, 26); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 26); } }
			[System.CLSCompliant(false)] public uint ChannelMask { get => System.BitConverter.ToUInt32(m_buffer, 28); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 28); } }
			public System.Guid SubFormat { get { var bytes = new byte[16]; m_buffer.CopyTo(m_buffer, 32); return new System.Guid(bytes); } set => value.ToByteArray().CopyTo(m_buffer, 32); }

			[System.CLSCompliant(false)] public uint BytesPerSample => (SampleBitDepth / 8U) * SampleChannels;

			public FormatChunk()
				: base(ID, 8 + 16) // Chunk + Structure
			{
				Format = 1; // PCM
			}
			[System.CLSCompliant(false)]
			public FormatChunk(ushort sampleChannels, uint sampleRate, ushort sampleBitDepth)
				: this()
			{
				SampleBitDepth = sampleBitDepth;
				SampleChannels = sampleChannels;
				SampleRate = sampleRate;
			}
			public FormatChunk(Chunk chunk)
				: base(chunk)
			{
			}

			//[System.CLSCompliant(false)]
			private ushort UpdateBlockAlign()
				=> BlockAlign = (ushort)(SampleChannels * (SampleBitDepth / 8));
			//[System.CLSCompliant(false)]
			private uint UpdateAvgBytesPerSec()
				=> AvgBytesPerSec = (SampleRate * BlockAlign);

			public static string GetFormatName(int format)
				=> format switch
				{
					1 => @"PCM",
					_ => throw new System.NotImplementedException()
				};

			public override string ToString()
				=> base.ToString().Replace(">", $", {GetFormatName((int)Format)}, {SampleChannels} ch., {SampleRate} Hz, {SampleBitDepth}-bit>", System.StringComparison.Ordinal);
		}

		public class DataChunk
			: Chunk
		{
			public const string ID = @"data";

			//public short[] SamplesAs16Bit
			//{
			//  get
			//  {
			//    short[] buffer = new short[(int)System.Math.Ceiling(Buffer.Length / 2.0)];

			//    System.Buffer.BlockCopy(Buffer, 0, buffer, 0, Buffer.Length);

			//    return buffer;
			//  }
			//  set
			//  {
			//    System.Array.Resize(ref Buffer, 8 + value.Length * 2);

			//    System.Buffer.BlockCopy(value, 0, Buffer, 8, Buffer.Length);

			//    ChunkDataSize = (uint)Buffer.Length;
			//  }
			//}

			public void SetSampleBuffer(FormatChunk format, int samples)
			{
				if (format is null) throw new System.ArgumentNullException(nameof(format));

				System.Array.Resize(ref m_buffer, 8 + (format.SampleBitDepth / 8 * format.SampleChannels) * samples);

				ChunkSize = (uint)m_buffer.Length - 8;
			}

			public DataChunk(int sampleBufferSize)
				: base(ID, 8 + sampleBufferSize)
			{
			}
			public DataChunk(Chunk chunk)
				: base(chunk)
			{
			}
		}
	}
}