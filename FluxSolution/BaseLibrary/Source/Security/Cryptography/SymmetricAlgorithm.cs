namespace Flux
{
	public static partial class Xtensions
	{
		public static System.Security.Cryptography.SymmetricAlgorithm SetKeyIV(this System.Security.Cryptography.SymmetricAlgorithm source, byte[] key, byte[] iv)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (key is null) throw new System.ArgumentNullException(nameof(key));
			if (iv is null) throw new System.ArgumentNullException(nameof(iv));

			var keyBytes = new byte[source.KeySize / 8];
			System.Array.Fill(keyBytes, default);
			key.CopyInto(keyBytes);
			source.Key = keyBytes;

			var ivBytes = new byte[source.BlockSize / 8];
			System.Array.Fill(ivBytes, default);
			iv.CopyInto(ivBytes);
			source.IV = ivBytes;

			return source;
		}
		public static System.Security.Cryptography.SymmetricAlgorithm SetKeyIV(this System.Security.Cryptography.SymmetricAlgorithm source, string key, string iv, System.Text.Encoding encoding)
			=> SetKeyIV(source, (encoding ?? throw new System.ArgumentNullException(nameof(encoding))).GetBytes(key), (encoding ?? throw new System.ArgumentNullException(nameof(encoding))).GetBytes(iv));

		/// <summary>Decrypt the source stream to the specified stream using the specified key, salt and symmetric algorithm.</summary>
		public static void Decrypt(this System.Security.Cryptography.SymmetricAlgorithm source, System.IO.Stream input, System.IO.Stream output)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			using var cs = new System.Security.Cryptography.CryptoStream(output, source.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

			input?.CopyTo(cs);
		}
		/// <summary>Encrypt the source stream to the specified stream using the specified key, salt and symmetric algorithm.</summary>
		public static void Encrypt(this System.Security.Cryptography.SymmetricAlgorithm source, System.IO.Stream input, System.IO.Stream output)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			using var cs = new System.Security.Cryptography.CryptoStream(output, source.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

			input?.CopyTo(cs);
		}

#pragma warning disable CA1031 // Do not catch general exception types
		/// <summary>Attempts to decrypt the source byte array to the output byte array with the specified key, salt and symmetric algorithm.</summary>
		public static bool TryDecrypt(this System.Security.Cryptography.SymmetricAlgorithm source, byte[] data, out byte[] result)
		{
			try
			{
				using var input = new System.IO.MemoryStream(data);
				using var output = new System.IO.MemoryStream();
				Decrypt(source, input, output);
				result = output.ToArray();
				return true;
			}
			catch { }

			result = default!;
			return false;
		}
		/// <summary>Attempts to encrypt the source byte array to the output byte array with the specified key, salt and symmetric algorithm.</summary>
		public static bool TryEncrypt(this System.Security.Cryptography.SymmetricAlgorithm source, byte[] data, out byte[] result)
		{
			try
			{
				using var input = new System.IO.MemoryStream(data);
				using var output = new System.IO.MemoryStream();
				Encrypt(source, input, output);
				result = output.ToArray();
				return true;
			}
			catch { }

			result = default!;
			return false;
		}

		/// <summary>Attempts to decrypt the source string (base64) to the output string with the specified key, salt and symmetric algorithm.</summary>
		public static bool TryDecrypt(this System.Security.Cryptography.SymmetricAlgorithm source, string base64, out string text)
		{
			try
			{
				if (TryDecrypt(source, System.Convert.FromBase64String(base64), out var output))
				{
					text = System.Text.Encoding.UTF8.GetString(output);
					return true;
				}
			}
			catch { }

			text = default!;
			return false;
		}
		/// <summary>Attempts to encrypt the source string to the output string (base64) with the specified key, salt and symmetric algorithm.</summary>
		public static bool TryEncrypt(this System.Security.Cryptography.SymmetricAlgorithm source, string text, out string base64)
		{
			try
			{
				if (TryEncrypt(source, System.Text.Encoding.UTF8.GetBytes(text), out var output))
				{
					base64 = System.Convert.ToBase64String(output);
					return true;
				}
			}
			catch { }

			base64 = default!;
			return false;
		}
#pragma warning restore CA1031 // Do not catch general exception types
	}
}
