namespace Flux
{
  public static partial class DateTimes
  {
    public static System.TimeSpan OverlapOfRanges(System.DateTime startA, System.DateTime endA, System.DateTime startB, System.DateTime endB)
      => (endA < endB ? endA : endB) - (startA > startB ? startA : startB);

    public static System.TimeSpan OverlapOfRanges(System.DateTime startA, System.TimeSpan lengthA, System.DateTime startB, System.TimeSpan lengthB)
      => OverlapOfRanges(startA, startA.Add(lengthA), startB, startB.Add(lengthB));

    public static bool AreRangesOverlapping(System.DateTime startA, System.DateTime endA, System.DateTime startB, System.DateTime endB)
      => (startA > startB ? startA : startB) <= (endA < endB ? endA : endB);

    public static bool AreRangesOverlapping(System.DateTime startA, System.TimeSpan lengthA, System.DateTime startB, System.TimeSpan lengthB)
      => AreRangesOverlapping(startA, startA.Add(lengthA), startB, startB.Add(lengthB));
  }
}
