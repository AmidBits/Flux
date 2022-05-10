namespace Flux
{
  public class PimaCountyPayPeriod
  {
    public static System.DateTime ReferenceDate
      => new System.DateTime(2014, 11, 2);

    public System.DateTime TargetDate { get; init; }

    public PimaCountyPayPeriod(System.DateTime target)
      => TargetDate = target;

    public System.DateTime ApprovalDate
      => StartDate.AddDays(12); // One day before pay period ends.
    public System.DateTime GetPayPeriodCheckDate(System.DateTime source)
      => StartDate.AddDays(19); // Six days after pay period ends.
    public System.DateTime EndDate(System.DateTime source)
      => StartDate.AddDays(13); // Two weeks.
    public System.DateTime StartDate
      => TargetDate.AddDays((ReferenceDate - TargetDate).Days % 14);
  }
}
