namespace Flux.Globalization.EnUs.Az.PimaCounty
{
  public readonly record struct PimaCountyPayPeriod
    : System.IComparable<PimaCountyPayPeriod>
  {
    public static System.DateTime ReferenceDate => new(2014, 11, 2); // As of 2023 this was still active.

    private readonly System.DateTime m_targetDate;

    public PimaCountyPayPeriod(System.DateTime target) => m_targetDate = target.Date;
    public PimaCountyPayPeriod() : this(System.DateTime.Now) { }

    public System.DateTime TargetDate { get => m_targetDate; init => m_targetDate = value.Date; }

    public System.DateTime ApprovalDate => StartDate.AddDays(12); // One day before pay period ends.
    public System.DateTime CheckDate => StartDate.AddDays(19); // Six days after pay period ends.
    public System.DateTime EndDate => StartDate.AddDays(13); // Two weeks.
    public System.DateTime StartDate => TargetDate.AddDays((ReferenceDate - TargetDate).Days % 14);

    #region Implemented interfaces
    public int CompareTo(PimaCountyPayPeriod other) => m_targetDate.CompareTo(other.m_targetDate);
    #endregion Implemented interfaces
  }
}
