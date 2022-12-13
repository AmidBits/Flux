using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Text
{
  [TestClass]
  public partial class Tokenizer
  {
    readonly string s = "3\u00D7(\U0001F92D9\u22126)";

    [TestMethod]
    public void RuneTokenizer()
    {
      var tokenizer = new Flux.Text.RuneTokenizer();

      var tokens = tokenizer.GetTokens(s).ToArray();

      var expected = new System.Text.Rune[] { (System.Text.Rune)51, (System.Text.Rune)215, (System.Text.Rune)40, (System.Text.Rune)0x0001F92D, (System.Text.Rune)57, (System.Text.Rune)0x2212, (System.Text.Rune)54, (System.Text.Rune)41 };
      var actual = tokens.Select(t => t.Value).ToArray();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TextElementTokenizer()
    {
      var tokenizer = new Flux.Text.TextElementTokenizer();

      var tokens = tokenizer.GetTokens(s).ToArray();

      var expected = new string[] { "3", "×", "(", "\U0001F92D", "9", "−", "6", ")" };
      var actual = tokens.Select(t => t.Value.Chars).ToArray();

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
