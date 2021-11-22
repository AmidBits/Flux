namespace Flux.Cryptography
{
  // http://burtleburtle.net/bob/rand/isaacafa.html
  public sealed class Isaac
    : Randomization.IsaacRandom
  {
    public byte[] Vernam(string text)
    {
      var bytes = new byte[text.Length];

      for (var i = 0; i < text.Length; i++)
      {
        var character = (char)(SampleUInt32() % 95 + 32);

        bytes[i] = (byte)(character ^ text[i]);
      }

      return bytes;
    }

    public byte[] Crypt(byte[] buffer, string key)
    {
      Seed(key, true);

      using var input = new System.IO.MemoryStream(buffer);
      using var output = new System.IO.MemoryStream();

      Crypt(input, output, key);

      output.Position = 0;
      return output.ToArray();
    }

    public int Crypt(System.IO.MemoryStream input, System.IO.MemoryStream output, string key)
    {
      if (input is null) throw new System.ArgumentNullException(nameof(input));
      if (output is null) throw new System.ArgumentNullException(nameof(output));

      Seed(key, true);

      var counter = 0;

      for (var read = input.ReadByte(); read != 0; read = input.ReadByte())
      {
        output.WriteByte((byte)((SampleUInt32() % 256) ^ read));

        counter++;
      }

      return counter;
    }

    public string Decrypt(string text, string key) => System.Text.Encoding.UTF8.GetString(Crypt(System.Convert.FromBase64String(text), key));
    public string Encrypt(string text, string key) => System.Convert.ToBase64String(Crypt(System.Text.Encoding.UTF8.GetBytes(text), key));

    //public static void Main()
    //{
    //  var isaac = new Flux.Cryptography.Isaac();
    //  string msg = "a Top Secret secret";
    //  string key = "this is my secret key";
    //  byte[] xctx = new byte[msg.Length];
    //  byte[] xptx = new byte[msg.Length];
    //  string xtcx = "*******************";
    //  string xtpx = "*******************";
    //  isaac.Seed(key, true);
    //  // XOR encrypt
    //  xctx = isaac.Vernam(msg);
    //  xtcx = System.Text.Encoding.ASCII.GetString(xctx);
    //  // XOR decrypt
    //  isaac.Seed(key, true);
    //  xptx = isaac.Vernam(xtcx);
    //  xtpx = System.Text.Encoding.ASCII.GetString(xptx);
    //  Console.WriteLine("Message: " + msg);
    //  Console.WriteLine("Key    : " + key);
    //  Console.Write("XOR    : ");
    //  // output ciphertext as a string of hexadecimal digits
    //  for (int n = 0; n < xctx.Length; n++) Console.Write("{0:X2}", xctx[n]);
    //  Console.WriteLine("\nXOR dcr: " + xtpx);
    //}
  }
}
