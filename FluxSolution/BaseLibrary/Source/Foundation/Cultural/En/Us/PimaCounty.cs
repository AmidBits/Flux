namespace Flux
{
  public record struct PimaCountyPayPeriod
    : System.IComparable<PimaCountyPayPeriod>
  {
    public static System.DateTime ReferenceDate
      => new(2014, 11, 2);

    private System.DateTime m_targetDate;

    public PimaCountyPayPeriod(System.DateTime target)
      => m_targetDate = new(target.Year, target.Month, target.Day);
    public PimaCountyPayPeriod()
      : this(System.DateTime.Now)
    {
    }

    public System.DateTime TargetDate
    { get => m_targetDate; set => m_targetDate = new(value.Year, value.Month, value.Day); }

    public System.DateTime ApprovalDate
      => StartDate.AddDays(12); // One day before pay period ends.
    public System.DateTime CheckDate
      => StartDate.AddDays(19); // Six days after pay period ends.
    public System.DateTime EndDate
      => StartDate.AddDays(13); // Two weeks.
    public System.DateTime StartDate
      => TargetDate.AddDays((ReferenceDate - TargetDate).Days % 14);

    #region Implemented interfaces
    public int CompareTo(PimaCountyPayPeriod other)
      => m_targetDate.CompareTo(other.m_targetDate);
    #endregion Implemented interfaces
  }
}
