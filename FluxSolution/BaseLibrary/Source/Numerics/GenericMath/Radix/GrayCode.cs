//#if NET7_0_OR_GREATER
//namespace Flux
//{
//	public static partial class GenericMath
//	{
//		/// <summary>Experimental adaption from wikipedia.</summary>
//		public static void BinaryToGray(int radix, int value, int[] gray)
//		{
//			if (gray is null) throw new System.ArgumentNullException(nameof(gray));

//			var baseN = new int[gray.Length]; // Stores the ordinary base-N number, one digit per entry

//			for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
//			{
//				baseN[index] = value % radix;

//				value /= radix;
//			}

//			var shift = 0; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

//			for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
//			{
//				gray[index] = (baseN[index] + shift) % radix;

//				shift = shift + radix - gray[index]; // Subtract from base so shift is positive
//			}
//		}

//		/// <summary>Adaption from wikipedia.</summary>
//		[System.CLSCompliant(false)]
//		public static void BinaryToGray(uint radix, uint value, uint[] gray)
//		{
//			if (gray is null) throw new System.ArgumentNullException(nameof(gray));

//			var baseN = new uint[gray.Length]; // Stores the ordinary base-N number, one digit per entry

//			for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
//			{
//				baseN[index] = value % radix;

//				value /= radix;
//			}

//			var shift = 0U; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

//			for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
//			{
//				gray[index] = (baseN[index] + shift) % radix;

//				shift = shift + radix - gray[index]; // Subtract from base so shift is positive
//			}
//		}
//	}
//}
//#endif
