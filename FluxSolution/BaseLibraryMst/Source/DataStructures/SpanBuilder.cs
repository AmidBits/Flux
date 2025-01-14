using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Text
{
  [TestClass]
  public partial class SpanBuilder
  {
    // private readonly string source = "Senor Hugo";
    // private readonly string target = "señor hugo";

    //readonly Flux.StringComparerEx comparerIgnoreCase = Flux.StringComparerEx.CurrentCultureIgnoreCase;
    //readonly Flux.StringComparerEx comparerIgnoreNonSpace = Flux.StringComparerEx.CurrentCultureIgnoreNonSpace;
    //readonly Flux.StringComparerEx comparerNone = Flux.StringComparerEx.Ordinal;

    //[TestMethod]
    //public void Count()
    //{
    //  var expected = 2;
    //  var actual = new Flux.SpanBuilder<char>("HugoRobert").Count('o');
    //  Assert.AreEqual(expected, actual);
    //}

    //[TestMethod]
    //public void CountEqualAt()
    //{
    //  var expected = 4;
    //  var actual = new Flux.SpanBuilder<char>("Robert".AsSpan()).CountEqualAt(2, "Hubert", 2);
    //  Assert.AreEqual(expected, actual);
    //}

    [TestMethod]
    public void CommonSuffixLength()
    {
      var expected = 3;
      var actual = new Flux.SpanMaker<char>("Robert").AsReadOnlySpan().CommonSuffixLength(0, "Rupert", null);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Duplicate()
    {
      var expected = "Roobeert";
      var actual = new Flux.SpanMaker<char>("Robert").Duplicate(c => c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u', 1);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void EndsWith()
    {
      var expected = true;
      var actual = new Flux.SpanMaker<char>("Robert").AsReadOnlySpan().EndsWith("ert");
      Assert.AreEqual(expected, actual);

      expected = false;
      actual = new Flux.SpanMaker<char>("Robert").AsReadOnlySpan().EndsWith("Bert");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EqualsAnyAt()
    {
      var expected = false;
      var actual = new Flux.SpanMaker<char>("Robert").AsReadOnlySpan().EqualsAnyAt(2, 2, ["do", "re", "mi"]);
      Assert.AreEqual(expected, actual);

      expected = true;
      actual = new Flux.SpanMaker<char>("Robert").AsReadOnlySpan().EqualsAnyAt(2, 2, ["bo", "bitter", "be"]);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EqualsAt()
    {
      var expected = false;
      var actual = new Flux.SpanMaker<char>("Robert").AsReadOnlySpan().EqualsAt(2, "re");
      Assert.AreEqual(expected, actual);

      expected = true;
      actual = new Flux.SpanMaker<char>("Robert").AsReadOnlySpan().EqualsAt(2, "be");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void IndexOf()
    {
      var expected = 3;
      var actual = new Flux.SpanMaker<char>("Robert Serious").AsReadOnlySpan().IndexOf("er");
      Assert.AreEqual(expected, actual);
    }

    //[TestMethod]
    //public void IndexOfAny1()
    //{
    //  var expected = 3;
    //  var actual = new Flux.SpanBuilder<char>("Robert Serious".AsSpan()).IndexOfAny(new char[] { 'e', 'r' });
    //  Assert.AreEqual(expected, actual);
    //}

    //[TestMethod]
    //public void IndexOfAny2()
    //{
    //  var expected = 1;
    //  var actual = new Flux.SpanBuilder<char>("Robert Serious".AsSpan()).IndexOfAny(new string[] { "er", "o" });
    //  Assert.AreEqual(expected, actual);
    //}

    //[TestMethod]
    //public void IndicesOfAny()
    //{
    //  var expected = new System.Collections.Generic.Dictionary<char, int>() { { 'e', 3 }, { 't', 5 }, { 'r', 4 } };
    //  var actual = new Flux.SpanBuilder<char>("Robert Serious".AsSpan()).IndicesOfAny(new char[] { 'e', 't', 'r' }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    //  CollectionAssert.AreEquivalent(expected.OrderBy(kvp => kvp.Key).ToArray(), actual.OrderBy(kvp => kvp.Key).ToArray());
    //}

    [TestMethod]
    public void InsertOrdinalIndicatorSuffix()
    {
      var expected = @"The 3rd item is before the 13th.";
      var actual = new SpanMaker<char>(@"The 3 item is before the 13.");
      actual.InsertOrdinalIndicatorSuffix();
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void IsIsomorphic()
    {
      var expected = true;
      var actual = new Flux.SpanMaker<char>("egg").AsReadOnlySpan().AreIsomorphic("add");
      Assert.AreEqual(expected, actual);
      expected = false;
      actual = new Flux.SpanMaker<char>("foo").AsReadOnlySpan().AreIsomorphic("bar");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void IsPalindrome()
    {
      var palindrome = (ReadOnlySpan<char>)new SpanMaker<char>("Poor Dan is in a droop").RemoveAll(char.IsWhiteSpace).AsSpan().ToLower();
      var expected = true;
      var actual = palindrome.IsPalindrome();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JoinToCamelCase()
    {
      var expected = @"join1To2Camel3Case4";
      var actual = new Flux.SpanMaker<char>(@"join1 to2 camel3 case4");
      actual.JoinToCamelCase(char.IsWhiteSpace);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void LastIndexOf()
    {
      var expected = 8;
      var actual = new Flux.SpanMaker<char>("Robert Serious").AsReadOnlySpan().LastIndexOf("er");
      Assert.AreEqual(expected, actual);
    }

    //[TestMethod]
    //public void LastIndexOfAny1()
    //{
    //  var expected = 9;
    //  var actual = new Flux.SpanBuilder<char>("Robert Serious").AsReadOnlySpan().LastIndexOfAny(null, 'e', 'r');
    //  Assert.AreEqual(expected, actual);
    //}

    //[TestMethod]
    //public void LastIndexOfAny2()
    //{
    //  var expected = 11;
    //  var actual = new Flux.SpanBuilder<char>("Robert Serious").AsReadOnlySpan().LastIndexOfAny(null, "er", "o");
    //  Assert.AreEqual(expected, actual);
    //}

    //[TestMethod]
    //public void LastIndicesOfAny()
    //{
    //  var expected = new System.Collections.Generic.Dictionary<char, int>() { { 'e', 8 }, { 't', 5 }, { 'r', 9 } };
    //  var actual = new Flux.SpanBuilder<char>("Robert Serious".AsSpan()).LastIndicesOfAny(new char[] { 'e', 't', 'r' }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    //  CollectionAssert.AreEquivalent(expected.OrderBy(kvp => kvp.Key).ToArray(), actual.OrderBy(kvp => kvp.Key).ToArray());
    //}

    [TestMethod]
    public void MakeIntegersFixedLength()
    {
      var expected = @"The 0003 item is before the 0013.";
      var actual = new Flux.SpanMaker<char>(@"The 3 item is before the 13.");
      actual.MakeNumbersFixedLength(4, '0');
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void NormalizeAdjacents()
    {
      var expected = @"There is a bee in the soup.";
      var actual = new Flux.SpanMaker<char>(@"There is aa bbee in the soup.");
      actual.NormalizeAdjacent(1, null, 'a', 'b');
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void NormalizeAll()
    {
      var expected = @"There is a bee in the soup.";
      var actual = new Flux.SpanMaker<char>(@"   There  is a  bee in  the soup.");
      actual.NormalizeAll(char.IsWhiteSpace, ' ');
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void PadEven()
    {
      var expected = @"---101----";
      var actual = new Flux.SpanMaker<char>(@"101");
      actual.PadEven(10, "-", "-", false);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void PadLeft()
    {
      var expected = @"00000006";
      var actual = new Flux.SpanMaker<char>(@"6");
      actual.PadLeft(8, '0'.ToString());
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void PadRight()
    {
      var expected = @"60000000";
      var actual = new Flux.SpanMaker<char>(@"6");
      actual.PadRight(8, '0'.ToString());
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void RemoveAll()
    {
      var expected = @" There  is  a  new  neat  little  thing  that  eats  soup.";
      var actual = new Flux.SpanMaker<char>(@"1 There 2 is 3 a 4 new 5 neat 6 little 7 thing 8 that 9 eats 0 soup.");
      actual.RemoveAll(char.IsDigit);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void Repeat()
    {
      var expected = "RoobeertRoobeertRoobeertRoobeert";
      var actual = new Flux.SpanMaker<char>("Roobeert").Repeat(3);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void ReplaceEqualAt()
    {
      var expected = @"It's a test.";
      var actual = new Flux.SpanMaker<char>(@"It's a bamboozle.").ReplaceIfEqualAt(7, @"bamboozle", @"test");
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void Reverse()
    {
      var expected = @"daeheldooD";
      var actual = new Flux.SpanMaker<char>(@"Doodlehead");
      actual.Reverse();
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void SplitFromCamelCase()
    {
      var expected = @"split from camel2 case1";
      var actual = new Flux.SpanMaker<char>(@"SplitFromCamel2Case1");
      actual.SplitFromCamelCase();
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void StartsWith()
    {
      var expected = true;
      var actual = new Flux.SpanMaker<char>(@"Robs boat.").AsReadOnlySpan().StartsWith(@"Rob");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Substring_LeftMost()
    {
      var expected = @"Rob";
      var actual = new Flux.SpanMaker<char>(@"Rob").AsReadOnlySpan().LeftMost(10);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void Substring_Right()
    {
      var expected = @"ob";
      var actual = new Flux.SpanMaker<char>(@"Rob").AsReadOnlySpan().RightMost(2);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void Substring_RightMost()
    {
      var expected = @"Rob";
      var actual = new Flux.SpanMaker<char>(@"Rob").AsReadOnlySpan().RightMost(10);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void Swap()
    {
      var expected = @"Hobert Rugo";
      var actual = new Flux.SpanMaker<char>(@"Robert Hugo").Swap(7, 0);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void ToLowerCase()
    {
      var expected = @"robert hugo";
      var actual = new Flux.SpanMaker<char>(@"Robert Hugo").AsSpan().ToLower();
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void ToUpperCase()
    {
      var expected = @"ROBERT HUGO";
      var actual = new Flux.SpanMaker<char>(@"Robert Hugo").AsSpan().ToUpper();
      Assert.AreEqual(expected, actual.ToString());
    }
  }
}
