namespace Flux
{
  public enum AngleNames
  {
    /// <summary>An angle equal to 0° or not turned is called a zero angle.</summary>
    ZeroAngle = 1,
    /// <summary>An angle smaller than a right angle (less than 90°) is called an acute angle ("acute" meaning "sharp").</summary>
    AcuteAngle = 2,
    /// <summary>An angle equal to 1/4 turn (90° or π/2 radians) is called a right angle. Two lines that form a right angle are said to be normal, orthogonal, or perpendicular.</summary>
    RightAngle = 4,
    /// <summary>An angle larger than a right angle and smaller than a straight angle (between 90° and 180°) is called an obtuse angle ("obtuse" meaning "blunt").</summary>
    ObtuseAngle = 8,
    /// <summary>An angle equal to 1/2 turn (180° or π radians) is called a straight angle.</summary>
    StraightAngle = 16,
    /// <summary>An angle larger than a straight angle but less than 1 turn (between 180° and 360°) is called a reflex angle.</summary>
    ReflexAngle = 32,
    /// <summary>An angle equal to 1 turn (360° or 2π radians) is called a full angle, complete angle, round angle or a perigon.</summary>
    PerigonAngle = 64,
    /// <summary>An angle that is not a multiple of a right angle is called an oblique angle.</summary>
    ObliqueAngle = 128
  }
}
