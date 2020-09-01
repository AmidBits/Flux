namespace Flux
{
  /// <summary>A collection of functions to perform typical 'bit' operations.</summary>
  public static partial class Bitwise
  {
    // And: a & b

    // 1 And 1 = 1
    // 1 And 0 = 0
    // 0 And 1 = 0
    // 0 And 0 = 0

    // Nand, not and: ~(a & b)

    // 1 Nand 1 = 0
    // 1 Nand 0 = 1
    // 0 Nand 1 = 1
    // 0 Nand 0 = 1

    // Nor, not or: ~(a | b)

    // 1 Nor 1 = 0
    // 1 Nor 0 = 0
    // 0 Nor 1 = 0
    // 0 Nor 0 = 1

    // Or: a | b

    // 1 Or 1 = 1
    // 1 Or 0 = 1
    // 0 Or 1 = 1
    // 0 Or 0 = 0

    // Xnor, exclusive not or: ~(a ^ b)

    // 1 Xnor 1 = 1
    // 1 Xnor 0 = 0
    // 0 Xnor 1 = 0
    // 0 Xnor 0 = 1

    // Xor, exclusive or: a ^ b

    // 1 Xor 1 = 0
    // 1 Xor 0 = 1
    // 0 Xor 1 = 1
    // 0 Xor 0 = 0
  }
}
