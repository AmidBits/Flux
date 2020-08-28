namespace Flux.IO.Hash
{
#pragma warning disable CA5351 // Do Not Use Broken Cryptographic Algorithms
  /// <summary>The LM hash algorithm.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/LAN_Manager#LM_hash_details"/>
  /// <remarks>
  /// reg save HKLM\SAM C:\sam
  /// reg save HKLM\SYSTEM C:\system
  /// </remarks>
  public static class LmHash
  {
    private static readonly byte[] m_defaultHalf = new byte[] { 0xAA, 0xD3, 0xB4, 0x35, 0xB5, 0x14, 0x04, 0xEE };

    private static byte[] ComputeHalf(byte[] half)
    {
      if (half.Length == 0) return (byte[])m_defaultHalf.Clone();

      System.Array.Resize(ref half, 7);

      var sb = new System.Text.StringBuilder();

      for (int index = 0; index < half.Length; index++) sb.Append(System.Convert.ToString(half[index], 2).PadLeft(8, '0'));

      for (int index = 8; index > 0; index--) sb.Insert(index * 7, '0');

      var desKey = new byte[8];

      for (int index = 0; index < 8; index++) desKey[index] = System.Convert.ToByte(sb.ToString(index * 8, 8), 2);

      using var des = new System.Security.Cryptography.DESCryptoServiceProvider
      {
        Key = desKey,
        IV = new byte[8]
      };

      using var ms = new System.IO.MemoryStream();
      using var cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
      using var sw = new System.IO.StreamWriter(cs);
      sw.Write("KGS!@#$%");

      var bytes = ms.ToArray();
      System.Array.Resize(ref bytes, 8);
      return bytes;
    }

    public static byte[] Compute(string password)
    {
      if (password is null) throw new System.ArgumentNullException(nameof(password));

      if (password.Length > 14) password = password.Substring(0, 14);

      var passwordBytes = System.Text.Encoding.ASCII.GetBytes(password?.ToUpper(System.Globalization.CultureInfo.CurrentCulture) ?? string.Empty);

      var passwordBytesHalf1 = passwordBytes.ToArray(0, passwordBytes.Length >= 7 ? 7 : passwordBytes.Length);
      var passwordBytesHalf2 = passwordBytes.Length > 7 ? passwordBytes.ToArray(7, passwordBytes.Length >= 14 ? 7 : passwordBytes.Length - 7) : System.Array.Empty<byte>();

      passwordBytesHalf1 = ComputeHalf(passwordBytesHalf1);
      passwordBytesHalf2 = ComputeHalf(passwordBytesHalf2);

      byte[] hash = new byte[16];
      System.Array.Copy(passwordBytesHalf1, hash, 8);
      System.Array.Copy(passwordBytesHalf2, 0, hash, 8, 8);
      return hash;
    }
  }
#pragma warning restore CA5351 // Do Not Use Broken Cryptographic Algorithms
}
