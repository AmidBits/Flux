namespace Flux
{
  public static class BitOps
  {
    #region BitLengthWithinType

    public static TBitLength AssertBitLengthWithinType<TBitLength>(this TBitLength bitLength, string? paramName = "bitLength")
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
  	  {
  		  if(!IsBitLengthWithinType(bitLength))
  			throw new System.ArgumentOutOfRangeException(paramName);
  		  
  		  return bitLength;
  	  }

  	public static bool IsBitLengthWithinType<TBitLength>(this TBitLength bitLength)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
  	=> bitLength >= TBitLength.Zero && bitLength <= TBitLength.CreateChecked(GetBitCount(bitLength));

    #endregion
  }
}
