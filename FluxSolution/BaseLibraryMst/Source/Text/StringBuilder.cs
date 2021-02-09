using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Flux;

namespace Text
{
	[TestClass]
	public partial class StringBuilder
	{
		readonly string source = "Senor Hugo";
		readonly string target = "señor hugo";

		readonly Flux.StringComparerEx comparerIgnoreCase = Flux.StringComparerEx.CurrentCultureIgnoreCase;
		readonly Flux.StringComparerEx comparerIgnoreNonSpace = Flux.StringComparerEx.CurrentCultureIgnoreNonSpace;
		readonly Flux.StringComparerEx comparerNone = Flux.StringComparerEx.Ordinal;

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
		public void Equals()
		{
			var expected = true;
			var actual = new System.Text.StringBuilder("Robert").Equals("Robert");
			Assert.AreEqual(expected, actual);

			expected = false;
			actual = new System.Text.StringBuilder("Robert").Equals("Hugo");
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
		public void Geminate()
		{
			var expected = "Roobeert";
			var actual = new System.Text.StringBuilder("Robert").Geminate('a', 'e', 'i', 'o', 'u').ToString();
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void IndexOf()
		{
			var expected = 3;
			var actual = new System.Text.StringBuilder("Robert").IndexOf("er");
			Assert.AreEqual(expected, actual);
		}
	}
}
