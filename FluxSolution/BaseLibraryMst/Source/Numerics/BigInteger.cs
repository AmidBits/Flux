using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Numerics
{
	[TestClass]
	public class BigInteger
	{
		readonly System.Numerics.BigInteger bi4 = new System.Numerics.BigInteger(4);
		readonly System.Numerics.BigInteger bi6 = new System.Numerics.BigInteger(6);

		readonly System.Numerics.BigInteger bi24 = new System.Numerics.BigInteger(24);
		readonly System.Numerics.BigInteger bi54 = new System.Numerics.BigInteger(54);

		[TestMethod]
		public void Gcd()
		{
			Assert.AreEqual(6, Flux.Maths.GreatestCommonDivisor(bi54.ToInt32(), bi24.ToInt32()));
		}

		[TestMethod]
		public void GetBitLength()
		{
			Assert.AreEqual(6, Flux.BitOps.BitLength(54.ToBigInteger()));
			Assert.AreEqual(5, Flux.BitOps.BitLength(24.ToBigInteger()));
		}

		[TestMethod]
		public void GetDigitSum()
		{
			Assert.AreEqual(9, Flux.Maths.DigitSum(bi54, 10));
			Assert.AreEqual(6, Flux.Maths.DigitSum(bi24, 10));
		}

		[TestMethod]
		public void GetDivisors()
		{
			Assert.IsTrue(new int[] { 1, 54, 2, 27, 3, 18, 6, 9 }.SequenceEqual(Flux.Numerics.Factors.GetDivisors(bi54).Select(bi => (int)bi)));
			Assert.IsTrue(new int[] { 1, 24, 2, 12, 3, 8, 4, 6 }.SequenceEqual(Flux.Numerics.Factors.GetDivisors(bi24).Select(bi => (int)bi)));
		}

		[TestMethod]
		public void GetFactorial()
		{
			Assert.AreEqual(System.Numerics.BigInteger.Parse("230843697339241380472092742683027581083278564571807941132288000000000000"), Flux.Maths.Factorial(bi54));
			Assert.AreEqual(System.Numerics.BigInteger.Parse("620448401733239439360000"), Flux.Maths.Factorial(bi24));
		}

		//CongruentModuloPrimes.GetAscending(startWith, 4, 3)

		[TestMethod]
		public void IsCongruentModuloPrime()
		{
			var bi2 = new System.Numerics.BigInteger(2);
			var bi3 = new System.Numerics.BigInteger(3);
			var bi5 = new System.Numerics.BigInteger(5);

			// Gaussian primes use 4/3
			Assert.AreEqual(false, Flux.Numerics.PrimeNumber.IsAlsoCongruentModuloPrime(bi2, 4, 3) && Flux.Numerics.PrimeNumber.IsPrimeNumber(bi2));
			Assert.AreEqual(true, Flux.Numerics.PrimeNumber.IsAlsoCongruentModuloPrime(bi3, 4, 3) && Flux.Numerics.PrimeNumber.IsPrimeNumber(bi3));

			// Pythagorean primes use 4/1
			Assert.AreEqual(false, Flux.Numerics.PrimeNumber.IsAlsoCongruentModuloPrime(bi3, 4, 1) && Flux.Numerics.PrimeNumber.IsPrimeNumber(bi3));
			Assert.AreEqual(true, Flux.Numerics.PrimeNumber.IsAlsoCongruentModuloPrime(bi5, 4, 1) && Flux.Numerics.PrimeNumber.IsPrimeNumber(bi5));
		}

		[TestMethod]
		public void IsPrime()
		{
			Assert.AreEqual(false, Flux.Numerics.PrimeNumber.IsPrimeNumber(bi54));
			Assert.AreEqual(false, Flux.Numerics.PrimeNumber.IsPrimeNumber(bi24));

			Assert.AreEqual(true, Flux.Numerics.PrimeNumber.IsPrimeNumber(new System.Numerics.BigInteger(3)));
		}

		[TestMethod]
		public void Lcm()
		{
			Assert.AreEqual(12, Flux.Maths.LeastCommonMultiple(bi4.ToInt32(), bi6.ToInt32()));
		}

		[TestMethod]
		public void Sqrt()
		{
			Assert.AreEqual(7, Flux.Maths.ISqrt(bi54));
		}

		[TestMethod]
		public void ToBaseString()
		{
			Assert.AreEqual("110110", bi54.ToRadixString(2));
			Assert.AreEqual("36", bi54.ToRadixString(16));
		}

		[TestMethod]
		public void ToStringOrdinal()
		{
			Assert.AreEqual("54th", bi54.ToString().InsertOrdinalIndicator());
			Assert.AreEqual("24th", bi24.ToString().InsertOrdinalIndicator());
		}

		//[TestMethod]
		//public void BinaryToText()
		//{
		//  Assert.AreEqual("Y", Flux.Text.PositionalNotation.Base64.NumberToText(bi24));
		//  Assert.AreEqual(bi24, Flux.Text.PositionalNotation.Base64.TextToNumber("Y"));

		//  Assert.AreEqual("2", Flux.Text.PositionalNotation.Base64.NumberToText(bi54));
		//  Assert.AreEqual(bi54, Flux.Text.PositionalNotation.Base64.TextToNumber("2"));
		//}
	}
}
