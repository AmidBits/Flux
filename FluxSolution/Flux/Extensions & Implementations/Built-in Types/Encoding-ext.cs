namespace Flux
{
  public static class EncodingExtensions
  {
    extension(System.Text.Encoding)
    {
      /// <summary>
      /// <para>Base62 consists of 62 letters and digits of ASCII - digits 0-9, capital letters A-Z and lower case letters a-z.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Base62"/></para>
      /// </summary>
      public static string Base62 => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    }
  }
}
