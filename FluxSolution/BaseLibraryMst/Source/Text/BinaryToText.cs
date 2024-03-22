using System.Linq;
using Flux.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Text
{
  [TestClass]
  public partial class BinaryToText
  {
    [TestMethod]
    public void Ascii85()
    {
      // End-to-end
      {
        var text = "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure.";

        text.TryEncodeBase85(out var base85);
        base85.TryDecodeBase85(out var decodedText);

        Assert.AreEqual(text, decodedText);
      }

      // Parts
      {
        var sourceText = "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure.";
        var sourceBytes = System.Text.Encoding.ASCII.GetBytes(sourceText);

        var base85 = Flux.Text.BinaryToText.EncodeBase85(sourceBytes);

        var targetBytes = Flux.Text.BinaryToText.DecodeBase85(base85);
        var targetText = System.Text.Encoding.ASCII.GetString(targetBytes);

        Assert.AreEqual(sourceText, targetText);
      }
    }
  }
}
