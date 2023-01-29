using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Text
{
  [TestClass]
  public partial class StringBuilder
  {
    // private readonly string source = "Senor Hugo";
    // private readonly string target = "señor hugo";

    //readonly Flux.StringComparerEx comparerIgnoreCase = Flux.StringComparerEx.CurrentCultureIgnoreCase;
    //readonly Flux.StringComparerEx comparerIgnoreNonSpace = Flux.StringComparerEx.CurrentCultureIgnoreNonSpace;
    //readonly Flux.StringComparerEx comparerNone = Flux.StringComparerEx.Ordinal;

    [TestMethod]
    public void AreIsomorphic()
    {
      var expected = true;
      var actual = new System.Text.StringBuilder("egg").AreIsomorphic("add");
      Assert.AreEqual(expected, actual);
      expected = false;
      actual = new System.Text.StringBuilder("foo").AreIsomorphic("bar");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Copy()
    {
      var expected = "Robertbert";
      var actual = new System.Text.StringBuilder("HugoRobert").Copy(4, 6, 0).ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CountEqualAt()
    {
      var expected = 4;
      var actual = new System.Text.StringBuilder("Robert").CountEqualAt(2, "Hubert", 2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CountEqualAtEnd()
    {
      var expected = 3;
      var actual = new System.Text.StringBuilder("Robert").CountEqualAtEnd("Rupert");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CountEqualAtStart()
    {
      var expected = 2;
      var actual = new System.Text.StringBuilder("Robert").CountEqualAtStart("Rommel");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Duplicate1()
    {
      var expected = "RoobeertRoobeertRoobeertRoobeert";
      var actual = new System.Text.StringBuilder("Roobeert").Duplicate(3).ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Duplicate2()
    {
      var expected = "Roobeert";
      var actual = new System.Text.StringBuilder("Robert").Duplicate("aeiou", 1).ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EndsWith()
    {
      var expected = true;
      var actual = new System.Text.StringBuilder("Robert").EndsWith("ert");
      Assert.AreEqual(expected, actual);

      expected = false;
      actual = new System.Text.StringBuilder("Robert").EndsWith("Bert");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EqualsAnyAt()
    {
      var expected = false;
      var actual = new System.Text.StringBuilder("Robert").EqualsAnyAt(2, 2, new string[] { "do", "re", "mi" });
      Assert.AreEqual(expected, actual);

      expected = true;
      actual = new System.Text.StringBuilder("Robert").EqualsAnyAt(2, 2, new string[] { "bo", "bi", "be" });
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EqualsAt()
    {
      var expected = false;
      var actual = new System.Text.StringBuilder("Robert").EqualsAt(2, "re");
      Assert.AreEqual(expected, actual);

      expected = true;
      actual = new System.Text.StringBuilder("Robert").EqualsAt(2, "be");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void IndexOf()
    {
      var expected = 3;
      var actual = new System.Text.StringBuilder("Robert Serious").IndexOf("er");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void IndexOfAny1()
    {
      var expected = 3;
      var actual = new System.Text.StringBuilder("Robert Serious").IndexOfAny(new char[] { 'e', 'r' });
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void IndexOfAny2()
    {
      var expected = 1;
      var actual = new System.Text.StringBuilder("Robert Serious").IndexOfAny(new string[] { "er", "o" });
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void IndicesOfAny()
    {
      var expected = new System.Collections.Generic.Dictionary<char, int>() { { 'e', 3 }, { 't', 5 }, { 'r', 4 } };
      var actual = new System.Text.StringBuilder("Robert Serious").IndicesOfAny(new char[] { 'e', 't', 'r' }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
      CollectionAssert.AreEquivalent(expected.OrderBy(kvp => kvp.Key).ToArray(), actual.OrderBy(kvp => kvp.Key).ToArray());
    }

    [TestMethod]
    public void InsertOrdinalIndicatorSuffix()
    {
      var expected = @"The 3rd item is before the 13th.";
      var actual = new System.Text.StringBuilder(@"The 3 item is before the 13.").InsertOrdinalIndicatorSuffix().ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void IsPalindrome()
    {
      var palindrome = @"Poor Dan is in a droop".ToStringBuilder().RemoveAll(char.IsWhiteSpace).ToLowerCase();
      var expected = true;
      var actual = palindrome.IsPalindrome();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LastIndexOf()
    {
      var expected = 8;
      var actual = new System.Text.StringBuilder("Robert Serious").LastIndexOf("er");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LastIndexOfAny1()
    {
      var expected = 9;
      var actual = new System.Text.StringBuilder("Robert Serious").LastIndexOfAny(new char[] { 'e', 'r' });
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LastIndexOfAny2()
    {
      var expected = 11;
      var actual = new System.Text.StringBuilder("Robert Serious").LastIndexOfAny(new string[] { "er", "o" });
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LastIndicesOfAny()
    {
      var expected = new System.Collections.Generic.Dictionary<char, int>() { { 'e', 8 }, { 't', 5 }, { 'r', 9 } };
      var actual = new System.Text.StringBuilder("Robert Serious").LastIndicesOfAny(new char[] { 'e', 't', 'r' }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
      CollectionAssert.AreEquivalent(expected.OrderBy(kvp => kvp.Key).ToArray(), actual.OrderBy(kvp => kvp.Key).ToArray());
    }

    [TestMethod]
    public void MakeIntegersFixedLength()
    {
      var expected = @"The 0003 item is before the 0013.";
      var actual = new System.Text.StringBuilder(@"The 3 item is before the 13.").MakeNumbersFixedLength(4).ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void NormalizeAdjacents()
    {
      var expected = @"There is a bee in the soup.";
      var actual = new System.Text.StringBuilder(@"There is aa bbee in the soup.").NormalizeAdjacent(1, 'a', 'b').ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void NormalizeAll()
    {
      var expected = @"There is a bee in the soup.";
      var actual = new System.Text.StringBuilder(@"   There  is a  bee in  the soup.
   ").NormalizeAll(' ', char.IsWhiteSpace).ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PadEven()
    {
      var expected = @"---101----";
      var actual = new System.Text.StringBuilder(@"101").PadEven(10, '-', '-').ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PadLeft()
    {
      var expected = @"00000006";
      var actual = new System.Text.StringBuilder(@"6").PadLeft(8, '0').ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PadRight()
    {
      var expected = @"60000000";
      var actual = new System.Text.StringBuilder(@"6").PadRight(8, '0').ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void RemoveAll()
    {
      var expected = @" There  is  a  new  neat  little  thing  that  eats  soup.";
      var actual = new System.Text.StringBuilder(@"1 There 2 is 3 a 4 new 5 neat 6 little 7 thing 8 that 9 eats 0 soup.").RemoveAll(char.IsDigit).ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReplaceEqualAt()
    {
      var expected = @"It's a test.";
      var actual = new System.Text.StringBuilder(@"It's a bamboozle.").ReplaceIfEqualAt(7, @"bamboozle", @"test").ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Reverse()
    {
      var expected = @"daeheldooD";
      var actual = new System.Text.StringBuilder(@"Doodlehead").Reverse().ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void StartsWith()
    {
      var expected = true;
      var actual = new System.Text.StringBuilder(@"Robs boat.").StartsWith(@"Rob");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Substring_LeftMost()
    {
      var expected = @"Rob";
      var actual = new System.Text.StringBuilder(@"Rob").LeftMost(10);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Substring_Right()
    {
      var expected = @"ob";
      var actual = new System.Text.StringBuilder(@"Rob").Right(2).ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Substring_RightMost()
    {
      var expected = @"Rob";
      var actual = new System.Text.StringBuilder(@"Rob").RightMost(10);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Swap()
    {
      var expected = @"Hobert Rugo";
      var sb = new System.Text.StringBuilder(@"Robert Hugo");
      sb.Swap(7, 0);
      var actual = sb.ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToLowerCase()
    {
      var expected = @"robert hugo";
      var actual = new System.Text.StringBuilder(@"Robert Hugo").ToLowerCase().ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToUpperCase()
    {
      var expected = @"ROBERT HUGO";
      var actual = new System.Text.StringBuilder(@"Robert Hugo").ToUpperCase().ToString();
      Assert.AreEqual(expected, actual);
    }
  }
}
